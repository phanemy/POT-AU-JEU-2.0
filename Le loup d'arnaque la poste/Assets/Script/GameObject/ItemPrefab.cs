using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPrefab : MonoBehaviour
{
    public Pickable item;

    public void Init(Vector3 position, Pickable item)
    {
        transform.position = position;
        this.item = item;
        transform.GetComponent<SpriteRenderer>().sprite = item.icon;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            Debug.Log("must be pick");
    }
}
