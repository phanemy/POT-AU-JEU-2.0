﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteManager))]
[RequireComponent(typeof(CombatComponent))]
public class MobBehaviour : MovingEnties
{
    //public int initialLife = 2;
    public float followSpeed = 2;
    public float maxMovementDist = 2;
    public float followPlayerDist = 3;
    [Range(0.1f, 1)]
    public float startMovingChance = 0.1f;
    public bool drawDebug = false;
    [SerializeField]
    public Pickable[] dropItems;
    [SerializeField]
    public SpriteManager spriteManager;
    private CombatComponent combatComponent;

    private DirectionEnum dir;
    private int actualLife;
    private GameObject player;
    private bool isSearching;
    private SpawnerAbstract spawner;
    private void Start()
    {
        spriteManager.init(gameObject.GetComponent<SpriteRenderer>());
        path = null;
        player = GameObject.FindGameObjectWithTag("Player");
        CircleCollider2D circle = gameObject.GetComponent<CircleCollider2D>();
        combatComponent = gameObject.GetComponent<CombatComponent>();
        if (circle != null)
        {
            circle.radius = maxMovementDist;
            circle.isTrigger = true;
        }
        dir = DirectionEnum.Bottom;
    }

    public void Init(Vector3 position, SpawnerAbstract spawner)
    {
        transform.position = position;
        this.spawner = spawner;
        transform.GetComponent<SpriteRenderer>().sortingOrder = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Menu.inGame)
            return;

        if (combatComponent.isDead)
        {
            Die();
        }
        float dist = (Position2D - (Vector2)player.transform.position).magnitude;
        if (dist <= combatComponent.attackDist && combatComponent.CanAttack)
        {
            combatComponent.Attack(dir);
            if (spriteManager.update)
                spriteManager.stop();
        }
        else
        if (dist <= followPlayerDist)
        {

            targetPos = player.transform.position;
            path = pathFinder.FindPath(Position2D, targetPos);
            if (path != null)
            {
                if (!isSearching)
                    indexDest = 0;
                else if (indexDest > path.Length)
                    indexDest = 0;

                isSearching = true;
                isMoving = true;
            }
            else
            {
                isSearching = false;
                isMoving = false;
                indexDest = -1;
            }
        }
        else if (isSearching)
        {
            isSearching = false;
            isMoving = false;
            indexDest = -1;
        }
        if (!combatComponent.isAttacking)
        {
            if (!isMoving && !isSearching)
            {
                if (choseDestination())
                {
                    path = pathFinder.FindPath(Position2D, targetPos, player);
                    if (path != null && path.Length != 0)
                    {
                        isMoving = true;
                        indexDest = 0;
                        //move();
                    }
                }
            }
            //else
            //{
            //    move();
            //}
        }
    }

    protected override void move()
    {
        if (Position2D != targetPos)
        {
            float updateSpeed = (isSearching ? followSpeed : speed) * Time.fixedDeltaTime;
            if (indexDest != -1)
            {
                movement = path[indexDest] - Position2D;
                dir = DirectionEnumMethods.GetDirection(movement);
                spriteManager.ActualDir = dir;

                if (movement.magnitude <= updateSpeed)
                {
                    //transform.position = new Vector3(path[indexDest].x, path[indexDest].y, transform.position.z);
                    indexDest++;
                    if (indexDest == path.Length)
                    {
                        indexDest = -1;
                        isMoving = false;
                        if (isSearching)
                            isSearching = false;
                        spriteManager.stop();
                    }
                }
                movement.Normalize();
                rb.MovePosition(rb.position + movement * updateSpeed);
                //else
                //{
                //    movement.Normalize();
                //    transform.position = transform.position + new Vector3(movement.x, movement.y, 0) * updateSpeed;
                //}
            }
            if (!spriteManager.update)
                spriteManager.start();
            spriteManager.Update();
        }
        else
            if (spriteManager.update)
            spriteManager.stop();
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            move();
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    protected override bool choseDestination()
    {
        if (Random.value < startMovingChance)
        {
            targetPos = Position2D + Random.insideUnitCircle.normalized * maxMovementDist;
            return true;
        }
        else
            return false;
    }

    private void OnDrawGizmos()
    {
        if (drawDebug)
        {
            Gizmos.color = Color.blue;
            if (isMoving)
            {
                for (int i = 1; i < path.Length; i++)
                    Gizmos.DrawLine(path[i - 1], path[i]);
            }
            else
            {
                Gizmos.DrawWireSphere(Position2D, maxMovementDist);
                Gizmos.color = Color.red;

                Gizmos.DrawWireSphere(Position2D, followPlayerDist);
            }
        }
    }

    private void Die()
    {
        PlayerManager pl = player.GetComponent<PlayerManager>();
        pl.mobDie((int)combatComponent.initialLife);
        Utils.InstantiatePickable(transform.position, dropItems[Random.Range(0, dropItems.Length)]);
        if (spawner != null)
            spawner.wasGather();
        Destroy(gameObject);
    }
}
