using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    static private ItemPrefab pickablePrefab;
    static public int NbSlot = 16;

    public static void Init()
    {
        pickablePrefab = Resources.Load<GameObject>("Prefab/GameObject/Dropable/DropableItemPrefab").GetComponent<ItemPrefab>();
    }

    public static void InstantiatePickable(Transform transf, Pickable item)
    {
        if(pickablePrefab == null)
            pickablePrefab = Resources.Load<ItemPrefab>("Prefab/GameObject/Dropable/DropableItemPrefab");

        ItemPrefab newGm = GameObject.Instantiate<ItemPrefab>(pickablePrefab);
        newGm.Init(transf.position, item);
        newGm.transform.SetParent(transf.parent);
        BoxCollider2D box = newGm.gameObject.AddComponent<BoxCollider2D>();
        box.isTrigger = true;
    }
}
