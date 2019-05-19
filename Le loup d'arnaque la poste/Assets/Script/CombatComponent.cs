using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class CombatComponent : MonoBehaviour
{
    public float initialLife = 2f;
    [HideInInspector]
    public float actualLife;
    public float initialAttackSpeed = 1f;
    [HideInInspector]
    public float attackSpeed;
    public float initialDamage = 1f;
    [HideInInspector]
    public float damage;
    public float attackDist = 1f;

    public float AttckUID { get; private set; }
    private float LastAttckUIDReceive;
    public RectTransform lifeSlider;
    public int sliderMaxSizeInPixel;

    public BoxCollider2D ownCollider;
    public Animator att;
    public Transform weaponParent;
    public AudioSource clip;

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
    public bool isAttacking;/*{ get; private set; }*/

    public void Awake()
    {
        isAttacking = false;
        isDead = false;
        actualLife = initialLife;
        attackSpeed = initialAttackSpeed;
        damage = initialDamage;
    }

    public void Start()
    {
        if (lifeSlider != null)
            sliderMaxSizeInPixel = (int)lifeSlider.sizeDelta.x;
    }

    public void Update()
    {
        if (!isAttacking && !isDead)
            timeSinceLastAttack += Time.deltaTime;
    }

    public void Attack(DirectionEnum dir)
    {
        if (!isAttacking && timeSinceLastAttack >= attackSpeed)
        {
            timeSinceLastAttack = 0;
            isAttacking = true;
            AttckUID = Random.value;
            clip.Play();
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
        if(lifeSlider != null)
            updateSlider();
        return (actualLife <= 0);
    }

    private void updateSlider()
    {
        float percent = (initialLife - actualLife) / initialLife;
        percent = 1 - percent;
        lifeSlider.sizeDelta = new Vector2(percent * sliderMaxSizeInPixel, lifeSlider.sizeDelta.y);
    }

    public void health(float healtCount)
    {
        actualLife += healtCount;
        if (actualLife > initialLife)
            actualLife = initialLife;
        updateSlider();
    }

    public void CombatEnd()
    {
        clip.Stop();
        att.SetBool("isAttacking", false);
        isAttacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isDead)
        {
            if (collision.tag == "Weapon" && collision != ownCollider)
            {
                CombatComponent cbc = collision.gameObject.transform.parent.parent.GetComponent<CombatComponent>();
                if (cbc != null && cbc.AttckUID != LastAttckUIDReceive)
                {
                    LastAttckUIDReceive = cbc.AttckUID;
                    if (takeDamage(cbc.damage))
                        isDead = true;
                }

            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, attackDist);
    }
}
