using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPrefab : MonoBehaviour, Interactable
{
    public Pickable item;
    public SpriteRenderer childRend;
    public float minDistMag;
    public float magSpeedMin;
    public float magSpeedMax;
    public float magIncreaseSpeed;
    private float offset = 0;
    private SpawnerAbstract spawner;
    private Transform player;


    public void Init(Vector3 position, Pickable item)
    {
        transform.position = position;
        this.item = item;
        transform.GetComponent<SpriteRenderer>().sprite = item.icon;
        transform.GetComponent<SpriteRenderer>().sortingOrder = 2;
        player = GameObject.FindWithTag("Player").transform;
        Debug.Log(player);
    }

    public void Init(Vector3 position, Pickable item, SpawnerAbstract spawner)
    {
        transform.position = position;
        this.item = item;
        transform.GetComponent<SpriteRenderer>().sprite = item.icon;
        transform.GetComponent<SpriteRenderer>().sortingOrder = 2;
        this.spawner = spawner;
        player = GameObject.FindWithTag("Player").transform;
        Debug.Log(player);
    }

    public void Update()
    {
        if(Inventory.Instance.HavePlace)
        {
            //Debug.Log(Vector2.Distance(transform.position, player.position));
            if (Vector2.Distance(transform.position, player.position) < minDistMag)
            {
                Vector3 dir = player.position - transform.position;
                dir.Normalize();
                transform.position += (dir * Time.deltaTime * (((1 - offset) /** (1 - offset)*/ * magSpeedMin) + (offset * offset * magSpeedMax)));
                if (offset < 1)
                    offset += magIncreaseSpeed * Time.deltaTime;
                if (offset > 1)
                    offset = 1;
            }
        }
    }

    public void CanBeInteract(bool boolean)
    {
        childRend.enabled = boolean;
    }

    public void Gather()
    {
        if (spawner != null)
        {
            spawner.wasGather();
        }

        Destroy(this.gameObject);
    }

    public bool interact(PlayerManager player)
    {
        bool b = Inventory.Instance.AddItem(item);
        //DebugConsoleBuild.Log(b.ToString(), 1);
        if (b)
        {
            this.Gather();
            return true;
        }
        else
            return false;
    }
}
