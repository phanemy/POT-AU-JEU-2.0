﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPrefab : MonoBehaviour
{
    public Pickable item;
    public SpriteRenderer childRend;
    private ItemsSpawner spawner;
    public void Init(Vector3 position, Pickable item)
    {
        transform.position = position;
        this.item = item;
        transform.GetComponent<SpriteRenderer>().sprite = item.icon;
    }

    public void Init(Vector3 position, Pickable item, ItemsSpawner spawner)
    {
        transform.position = position;
        this.item = item;
        transform.GetComponent<SpriteRenderer>().sprite = item.icon;
        this.spawner = spawner;
    }


    public void CanBeGather(bool boolean)
    {
        Debug.Log(boolean);
        childRend.enabled = boolean;
    }

    public void Gather()
    {
        if (spawner != null)
            spawner.wasGather();

        Destroy(this.gameObject);
    }
}
