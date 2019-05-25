using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : SpawnerAbstract
{


    public MobBehaviour itemToSpawn;
    private int maxTry = 10;
    public Bounds bounds;
    private int layerMask;

    public void Awake()
    {
        bounds = new Bounds(transform.position,transform.localScale);
        layerMask = LayerMask.GetMask("Obstacle");
    }

    public override void Spawn()
    {
        float x, y;
        int rayTry = 0;
        RaycastHit2D hit;
        do
        {
            x = Random.Range(transform.position.x - xScale, transform.position.x + xScale);
            y = Random.Range(transform.position.y - yScale, transform.position.y + yScale);

            hit = Physics2D.Raycast(new Vector3(x, y, 0), Vector2.zero,50, layerMask);
            //if (hit.collider == null)
            //    Debug.Log("nop");
            //else
            //    Debug.Log(hit.collider.gameObject.name);

            rayTry++;
        } while (rayTry < maxTry && hit.collider != null);

        if (hit.collider == null)
        {
            //Debug.Log("create");
            Utils.InstantiateMob(new Vector3(x, y, 0), itemToSpawn, this);
            actualNumber++;
        }
        //else
        //    Debug.Log("nop 2");

    }

    public bool pointIsInSpawner(Vector3 point)
    {
        //Debug.Log(point + " " + bounds.Contains(point));
        return bounds.Contains(point);
    }
}
