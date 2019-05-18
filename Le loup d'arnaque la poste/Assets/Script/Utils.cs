using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    static private ItemPrefab pickablePrefab;

    public static void Init()
    {
        pickablePrefab = Resources.Load<ItemPrefab>("Prefab/GameObject/DropableItem.prefab");
    }

    public static void InstantiatePickable(Vector3 position, Pickable item)
    {
        GameObject.Instantiate<ItemPrefab>(pickablePrefab).Init(position, item);
    }
}
