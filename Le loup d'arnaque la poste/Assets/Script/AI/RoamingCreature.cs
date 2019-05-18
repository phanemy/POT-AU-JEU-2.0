using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamingCreature : MovingEnties
{
    public int initialLife = 2;
    public float distMaxMovement = 2;
    [Range(0.1f,1)]
    public float startMovingChance = 0.5f;
    public bool drawDebug = false;
       
    private int actualLife;
    private GameObject player;
    private bool isFleeing;

    private void Start()
    {
        path = null;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving && !isFleeing)
        {
            if(choseDestination())
            {
                path = pathFinder.FindPath(Position2D, targetPos, player);
                if (path != null && path.Length != 0)
                {
                    isMoving = true;
                    indexDest = 0;
                    move();
                }
            }
        }
        else
        {
            move();
        }
    }

    protected override void move()
    {
        if (Position2D != targetPos)
        {
            Vector2 axis;
            float updateSpeed = speed * Time.deltaTime;

            if (indexDest != -1)
            {
                axis = path[indexDest] - Position2D;
                if (axis.magnitude <= updateSpeed)
                {
                    transform.position = new Vector3(path[indexDest].x, path[indexDest].y, transform.position.z);
                    indexDest++;
                    if (indexDest == path.Length)
                    {
                        indexDest = -1;
                        isMoving = false;
                    }
                }
                else
                {
                    axis.Normalize();
                    transform.position = transform.position + new Vector3(axis.x, axis.y, 0) * updateSpeed;
                }
            }
            //else
            //{
            //    axis = targetPos - Position2D;
            //    if (axis.magnitude <= updateSpeed)
            //    {
            //        transform.position = new Vector3(targetPos.x, targetPos.y, transform.position.z);
            //        isMoving = false;
            //    }
            //    else
            //    {
            //        axis.Normalize();
            //        transform.position = transform.position + new Vector3(axis.x, axis.y, 0) * updateSpeed;
            //    }
            //}
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(collision.tag == "Player")
    //    {
    //        isMoving = true;

    //    }
    //}

    protected override bool choseDestination() {
        if (Random.value < startMovingChance)
        {
            targetPos = Random.insideUnitCircle.normalized * distMaxMovement;
            return true;
        }
        else
            return false;
    }


    private void OnDrawGizmos()
    {
        if(drawDebug)
        {
            Gizmos.color = Color.blue;
            if (isMoving)
            {
                //Gizmos.DrawLine(path[0], Position2D);
                for (int i = 1; i < path.Length; i++)
                    Gizmos.DrawLine(path[i - 1], path[i]);
            }
            else
                Gizmos.DrawWireSphere(Position2D, distMaxMovement*2);
        }
    }

}
