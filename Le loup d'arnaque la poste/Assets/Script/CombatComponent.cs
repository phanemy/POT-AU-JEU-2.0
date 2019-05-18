using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatComponent : MonoBehaviour
{
    public float initialLife = 2f;
    public float actualLife = 2f;
    public float attackSpeed = 1f;
    public float damage = 1f;
    public float knockBackDist = 1f;

    public BoxCollider2D ownCollider;
    public Animator att;
    public Transform weaponParent;

    public bool CanAttack
    {
        get
        {
            return !isAttacking && timeSinceLastAttack >= attackSpeed;
        }
        private set { }
    }

    public bool isDead;

    public float timeSinceLastAttack = 0f;
    public bool isKnockBack { get; private set; }
    public bool isAttacking;/*{ get; private set; }*/

    public void OnEnable()
    {
        isKnockBack = false;
        isAttacking = false;
        isDead = false;
    }

    public void Update()
    {
        if(!isAttacking && !isDead)
            timeSinceLastAttack += Time.deltaTime;
    }

    public void Attack(DirectionEnum dir)
    {
        if (!isAttacking && timeSinceLastAttack >= attackSpeed)
        {
            timeSinceLastAttack = 0;
            isAttacking = true;
            switch (dir)
            {
                case DirectionEnum.Bottom:
                    weaponParent.localRotation = DirectionEnumMethods.GetQuaternionFromDirection(DirectionEnum.Bottom);
                    att.SetBool("isAttacking", true);
                    break;
                case DirectionEnum.Left:
                    weaponParent.localRotation = DirectionEnumMethods.GetQuaternionFromDirection(DirectionEnum.Left);
                    att.SetBool("isAttacking", true);

                    break;
                case DirectionEnum.Top:
                    weaponParent.localRotation = DirectionEnumMethods.GetQuaternionFromDirection(DirectionEnum.Top);
                    att.SetBool("isAttacking", true);

                    break;
                case DirectionEnum.Right:
                    weaponParent.localRotation = DirectionEnumMethods.GetQuaternionFromDirection(DirectionEnum.Right);
                    att.SetBool("isAttacking", true);

                    break;
                default: break;
            }
        }
    }

    public bool takeDamage(float takenDamage)
    {
        actualLife -= takenDamage;
        return (actualLife <= 0);
    }

    public void CombatEnd()
    {
        att.SetBool("isAttacking", false);
        isAttacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isDead)
        {
            if (collision.tag == "Weapon" && collision != ownCollider)
            {
                CombatComponent cbc = collision.gameObject.transform.parent.parent.GetComponent<CombatComponent>();
                if (cbc != null)
                    if (takeDamage(cbc.damage))
                        isDead = true;
            }
        }
    }
}
