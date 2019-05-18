using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    static GameObject backGround;
    static private ItemPrefab pickablePrefab;
    static public int NbSlot = 16;

    public static void Init()
    {
        pickablePrefab = Resources.Load<GameObject>("Prefab/GameObject/Dropable/DropableItemPrefab").GetComponent<ItemPrefab>();
        backGround = GameObject.FindWithTag("BackGround");
    }

    public static void InstantiatePickable(Vector3 position, Pickable item)
    {
        if(pickablePrefab == null)
            pickablePrefab = Resources.Load<ItemPrefab>("Prefab/GameObject/Dropable/DropableItemPrefab");

        ItemPrefab newGm = GameObject.Instantiate<ItemPrefab>(pickablePrefab);
        newGm.Init(new Vector3(position.x, position.y, backGround.transform.position.z), item);
        newGm.transform.SetParent(backGround.transform);
        BoxCollider2D box = newGm.gameObject.AddComponent<BoxCollider2D>();
        box.isTrigger = true;
    }
}
