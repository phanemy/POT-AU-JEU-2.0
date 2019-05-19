using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public InventoryPanel inventoryPanel;
    public ChaudronPanel ChaudronPanel;
    private new List<ItemCptn> items = new List<ItemCptn>();
    private new List<ItemCptn> caldonItems = new List<ItemCptn>();
    public PlayerManager player;

    public void Start()
    {
        inventoryPanel.gameObject.SetActive(false);
        ChaudronPanel.gameObject.SetActive(false);
    }

    public void Update()
    {
        if (Input.GetButtonDown("Inventary"))
        {
            Show();
        }
    }

    public void Show()
    {
        if (inventoryPanel.gameObject.activeSelf)
        {
            inventoryPanel.gameObject.SetActive(false);
        }
        else
        {
            inventoryPanel.gameObject.SetActive(true);
            inventoryPanel.Show(items);
        }
        ChaudronPanel.gameObject.SetActive(false);
    }

    public void ShowColdon()
    {
        ChaudronPanel.gameObject.SetActive(true);
        ChaudronPanel.Show(caldonItems);

        inventoryPanel.gameObject.SetActive(true);
        inventoryPanel.Show(items);
    }

    public bool AddItem(ItemCptn item)
    {
        if (items.Count >= Utils.NbSlot)
        {
            return false;
        }
        else
        {
            items.Add(item);
            inventoryPanel.Show(items);
            return true;
        }
    }

    private bool AddCaldonItem(ItemCptn item)
    {
        if (caldonItems.Count >= Utils.NbSlotColdon)
        {
            return false;
        }
        else
        {
            caldonItems.Add(item);
            ChaudronPanel.Show(caldonItems);
            return true;
        }
    }

    public void RemoveItem(ItemCptn item)
    {
        Potion pop = item as Potion;
        if (pop != null)
            player.appplyEffect(pop);

        items.Remove(item);
        inventoryPanel.Show(items);

    }

    public void RemoveItem(int id, bool isInventary)
    {
        if (id >= 0 && id < items.Count && isInventary)
        {
            if (ChaudronPanel.gameObject.activeSelf)
            {
                ItemCptn item = items[id];
                if (AddCaldonItem(item))
                {
                    items.RemoveAt(id);
                    inventoryPanel.Show(items);
                }
            }
            else
            {
                Potion pop = items[id] as Potion;
                if (pop != null)
                    player.appplyEffect(pop);
                items.RemoveAt(id);
                inventoryPanel.Show(items);
            }
        }

        if (id >= 0 && id < caldonItems.Count && !isInventary)
        {
            ItemCptn item = caldonItems[id];
            caldonItems.RemoveAt(id);
            ChaudronPanel.Show(caldonItems);
            AddItem(item);
        }
    }

    public void CraftItem()
    {
        if (caldonItems.Count == Utils.NbSlotColdon)
        {
            foreach(Recipe recipe in Utils.recipes)
            {
                bool ok = caldonItems.Except(recipe.Items).Count() == 0;

                if (ok)
                {
                    AddItem(recipe.potion);
                    break;
                }
            }

            caldonItems.Clear();
            ChaudronPanel.Show(caldonItems);
        }
    }

    public void Clear()
    {
        items.Clear();
        inventoryPanel.Show(items);
    }
}
