using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsSpawner : MonoBehaviour
{
    public bool drawDebug;
    public float delayBeetweenSpawn;
    public float delayBeforeStartSpawn;
    public int maxEntity;

    public Pickable itemToSpawn;
    public Color debugColor;
    
    private float timeSinceLastSpawn;
    private int actualNumber;
    private bool canSpawn = false;
    private float xScale, yScale;
    

    // Start is called before the first frame update
    void Start()
    {
        timeSinceLastSpawn = 0f;
        xScale = (transform.localScale.x / 2);
        yScale = (transform.localScale.x / 2);
    }

    private void Update()
    {
        if(actualNumber < maxEntity)
        {
            timeSinceLastSpawn += Time.deltaTime;
            if(!canSpawn && timeSinceLastSpawn >= delayBeforeStartSpawn)
            {
                canSpawn = true;
                Spawn();
            }
            else if(timeSinceLastSpawn >= delayBeetweenSpawn)
            {
                Spawn();
            }
        }
    }

    public void Spawn()
    {

        float x = Random.Range(transform.position.x - xScale, transform.position.x + xScale);
        float y = Random.Range(transform.position.y - yScale, transform.position.y + yScale);

        Utils.InstantiatePickable(new Vector3(x,y,0), itemToSpawn);
        actualNumber++;
    }

    public void wasGather()
    {
        actualNumber--;
    }

    private void OnDrawGizmos()
    {
        if(drawDebug)
        {
            Gizmos.color = debugColor;
            Gizmos.DrawCube(transform.position, transform.localScale);
        }
        
    }
}
