using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    static GameObject backGround;
    //static GameObject middleGround;
    static private ItemPrefab pickablePrefab;
    static public int NbSlot = 16;
    static public int NbSlotColdon = 3;

    static public Recipe[] recipes;

    public static void Init()
    {
        pickablePrefab = Resources.Load<GameObject>("Prefab/GameObject/Interactable/DropableItemPrefab").GetComponent<ItemPrefab>();
        backGround = GameObject.FindWithTag("BackGround");
        //middleGround = GameObject.FindWithTag("MiddleGround");
        recipes = InitRecipes();

        foreach (Recipe recipe in recipes)
        {
            Debug.Log(recipe.potion.itemName + " : " + recipe.Items[0].itemName + " " + recipe.Items[1].itemName + " " + recipe.Items[2].itemName);
        }
    }

    private static Recipe[] InitRecipes()
    {
        Resources.LoadAll("Recipes", typeof(Recipe));
        Resources.LoadAll("Items", typeof(ItemCptn));
        Recipe[] allRecipe = Resources.FindObjectsOfTypeAll<Recipe>();
        ItemCptn[] allItems = Resources.FindObjectsOfTypeAll<ItemCptn>();

        List<Recipe> finalRecipes = new List<Recipe>();

        foreach (Recipe recipe in allRecipe)
        {
            bool unique = true;
            Recipe newRecipe;
            do
            {
                newRecipe = GenerateRecipe(allItems, recipe);

                List<ItemCptn> items = new List<ItemCptn>();

                foreach (ItemCptn cptn in newRecipe.Items)
                {
                    items.Add(cptn);
                }

                foreach (Recipe finalRecipe in finalRecipes)
                {
                    if (CheckRecipe(finalRecipe, items))
                    {
                        unique = false;
                        break;
                    }
                }

            } while (!unique);

            finalRecipes.Add(newRecipe);
        }

        return finalRecipes.ToArray();
    }

    private static Recipe GenerateRecipe(ItemCptn[] allItems, Recipe recipe)
    {
        Recipe newRecipe = ScriptableObject.CreateInstance<Recipe>();
        newRecipe.potion = recipe.potion;
        int i = 0;

        foreach (Item item in recipe.Items)
        {
            if (item as RandomItem != null)
            {
                RandomItem randomItem = item as RandomItem;
                newRecipe.Items[i++] = RandomItem(allItems, randomItem.rarity);
            }
            else
            {
                newRecipe.Items[i++] = item;
            }
        }
        System.Array.Sort(newRecipe.Items);

        return newRecipe;
    }

    private static Item RandomItem(ItemCptn[] allItems, Rarete rarety)
    {
        List<ItemCptn> raretyList = new List<ItemCptn>();

        foreach (ItemCptn item in allItems)
        {
            if (item.rarety == rarety)
            {
                raretyList.Add(item);
            }
        }

        int id = Random.Range(0, raretyList.Count);

        return raretyList[id];
    }

    public static ItemPrefab InstantiatePickable(Vector3 position, Pickable item)
    {
        if (pickablePrefab == null)
            pickablePrefab = Resources.Load<ItemPrefab>("Prefab/GameObject/Interactable/DropableItemPrefab");

        ItemPrefab newGm = GameObject.Instantiate<ItemPrefab>(pickablePrefab);
        newGm.Init(new Vector3(position.x, position.y, 5), item);
        newGm.transform.SetParent(backGround.transform, true);
        BoxCollider2D box = newGm.gameObject.AddComponent<BoxCollider2D>();
        box.isTrigger = true;
        return newGm;
    }

    public static ItemPrefab InstantiatePickable(Vector3 position, Pickable item, SpawnerAbstract spawn)
    {
        if (pickablePrefab == null)
            pickablePrefab = Resources.Load<ItemPrefab>("Prefab/GameObject/Interactable/DropableItemPrefab");

        ItemPrefab newGm = GameObject.Instantiate<ItemPrefab>(pickablePrefab);
        newGm.Init(new Vector3(position.x, position.y, 5), item, spawn);
        newGm.transform.SetParent(backGround.transform, true);
        BoxCollider2D box = newGm.gameObject.AddComponent<BoxCollider2D>();
        box.isTrigger = true;
        return newGm;

    }

    public static MobBehaviour InstantiateMob(Vector3 position, MobBehaviour mob, MobSpawner spawn)
    {
        MobBehaviour newGm = GameObject.Instantiate<MobBehaviour>(mob);
        newGm.Init(new Vector3(position.x, position.y, 5), spawn);
        newGm.transform.SetParent(backGround.transform, true);
        BoxCollider2D box = newGm.gameObject.AddComponent<BoxCollider2D>();
        box.isTrigger = true;
        return newGm;
    }

    public static bool CheckRecipe(Recipe recipe, List<ItemCptn> items)
    {
        if (recipe.Items.Length != items.Count)
        {
            return false;
        }

        ItemCptn[] temp = new ItemCptn[items.Count];

        for (int i = 0; i < items.Count; ++i)
        {
            temp[i] = items[i];
        }

        System.Array.Sort(temp);

        for (int i = 0; i < items.Count; ++i)
        {
            if (temp[i] != recipe.Items[i])
            {
                return false;
            }
        }

        return true;
    }
}
