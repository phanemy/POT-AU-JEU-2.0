using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : SpawnerAbstract
{


    public MobBehaviour itemToSpawn;
    private int maxTry = 10;

    public override void Spawn()
    {
        float x, y;
        int rayTry = 0;
        RaycastHit2D hit;
        do
        {
            x = Random.Range(transform.position.x - xScale, transform.position.x + xScale);
            y = Random.Range(transform.position.y - yScale, transform.position.y + yScale);

            hit = Physics2D.Raycast(new Vector3(x, y, 0), Vector2.zero,50);
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
}
