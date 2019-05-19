using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    static GameObject backGround;
    static GameObject middleGround;
    static private ItemPrefab pickablePrefab;
    static public int NbSlot = 16;
    static public int NbSlotColdon = 3;

    static public Recipe[] recipes;

    public static void Init()
    {
        pickablePrefab = Resources.Load<GameObject>("Prefab/GameObject/Interactable/DropableItemPrefab").GetComponent<ItemPrefab>();
        backGround = GameObject.FindWithTag("BackGround");
        backGround = GameObject.FindWithTag("MiddleGround");
        recipes = Resources.FindObjectsOfTypeAll<Recipe>();
    }

    public static ItemPrefab InstantiatePickable(Vector3 position, Pickable item)
    {
        if(pickablePrefab == null)
            pickablePrefab = Resources.Load<ItemPrefab>("Prefab/GameObject/Interactable/DropableItemPrefab");

        ItemPrefab newGm = GameObject.Instantiate<ItemPrefab>(pickablePrefab);
        newGm.Init(new Vector3(position.x, position.y, backGround.transform.position.z), item);
        newGm.transform.SetParent(backGround.transform);
        BoxCollider2D box = newGm.gameObject.AddComponent<BoxCollider2D>();
        box.isTrigger = true;
        return newGm;
    }

    public static ItemPrefab InstantiatePickable(Vector3 position, Pickable item, SpawnerAbstract spawn)
    {
        if (pickablePrefab == null)
            pickablePrefab = Resources.Load<ItemPrefab>("Prefab/GameObject/Interactable/DropableItemPrefab");

        ItemPrefab newGm = GameObject.Instantiate<ItemPrefab>(pickablePrefab);
        newGm.Init(new Vector3(position.x, position.y, backGround.transform.position.z), item, spawn);
        newGm.transform.SetParent(backGround.transform);
        BoxCollider2D box = newGm.gameObject.AddComponent<BoxCollider2D>();
        box.isTrigger = true;
        return newGm;

    }

    public static MobBehaviour InstantiateMob(Vector3 position, MobBehaviour mob, SpawnerAbstract spawn)
    {
        MobBehaviour newGm = GameObject.Instantiate<MobBehaviour>(mob);
        newGm.Init(new Vector3(position.x, position.y, backGround.transform.position.z), spawn);
        newGm.transform.SetParent(backGround.transform);
        BoxCollider2D box = newGm.gameObject.AddComponent<BoxCollider2D>();
        box.isTrigger = true;
        return newGm;
    }

}
