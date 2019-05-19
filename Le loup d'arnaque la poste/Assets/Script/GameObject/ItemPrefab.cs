using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPrefab : MonoBehaviour
{
    public Pickable item;
    public SpriteRenderer childRend;

    public void Init(Vector3 position, Pickable item)
    {
        transform.position = position;
        this.item = item;
        transform.GetComponent<SpriteRenderer>().sprite = item.icon;
    }

    public void CanBeGather(bool boolean)
    {
        Debug.Log(boolean);
        childRend.enabled = boolean;
    }
}
