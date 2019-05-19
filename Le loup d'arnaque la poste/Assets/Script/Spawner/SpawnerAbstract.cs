using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnerAbstract : MonoBehaviour
{
    public bool drawDebug;
    public float delayBeetweenSpawnMin;
    public float delayBeetweenSpawnMax;
    public float delayBeforeStartSpawnMin;
    public float delayBeforeStartSpawnMax;
    public int maxEntity;

    public Color debugColor;

    protected float timeSinceLastSpawn;
    protected int actualNumber;
    protected bool canSpawn = false;
    protected float xScale, yScale;
    protected float delayBeforeStartSpawn;
    protected float delayBeetweenSpawn;


    // Start is called before the first frame update
    protected void Start()
    {
        timeSinceLastSpawn = 0f;
        xScale = (transform.localScale.x / 2);
        yScale = (transform.localScale.y / 2);
        delayBeforeStartSpawn = Random.Range(delayBeforeStartSpawnMin, delayBeforeStartSpawnMax);
    }

    protected void Update()
    {
        if (actualNumber < maxEntity)
        {
            timeSinceLastSpawn += Time.deltaTime;
            if (!canSpawn && timeSinceLastSpawn >= delayBeforeStartSpawn)
            {
                canSpawn = true;
                delayBeetweenSpawn = Random.Range(delayBeetweenSpawnMin, delayBeetweenSpawnMax);
            }
            else if (timeSinceLastSpawn >= delayBeetweenSpawn)
            {
                Spawn();
                delayBeetweenSpawn = Random.Range(delayBeetweenSpawnMin, delayBeetweenSpawnMax);
            }
        }
    }

    public abstract void Spawn();

    public void wasGather()
    {
        if (actualNumber == maxEntity)
        {
            delayBeetweenSpawn = Random.Range(delayBeetweenSpawnMin, delayBeetweenSpawnMax);
            timeSinceLastSpawn = 0f;
        }
        actualNumber--;
    }

    protected void OnDrawGizmos()
    {
        if (drawDebug)
        {
            Gizmos.color = debugColor;
            Gizmos.DrawCube(transform.position, transform.localScale);
        }

    }
}
