using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPrefab : MonoBehaviour, Interactable
{
    public Pickable item;
    public SpriteRenderer childRend;
    private SpawnerAbstract spawner;
    public void Init(Vector3 position, Pickable item)
    {
        transform.position = position;
        this.item = item;
        transform.GetComponent<SpriteRenderer>().sprite = item.icon;
        transform.GetComponent<SpriteRenderer>().sortingOrder = 2;
    }

    public void Init(Vector3 position, Pickable item, SpawnerAbstract spawner)
    {
        transform.position = position;
        this.item = item;
        transform.GetComponent<SpriteRenderer>().sprite = item.icon;
        transform.GetComponent<SpriteRenderer>().sortingOrder = 2;
        this.spawner = spawner;
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
