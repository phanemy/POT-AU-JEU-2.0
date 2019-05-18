using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public InventoryPanel inventoryPanel;
    private new List<ItemCptn> items = new List<ItemCptn>();
    private bool isActive = false;

    public void Show()
    {
        if (isActive)
        {
            inventoryPanel.gameObject.SetActive(false);
            isActive = false;
        }
        else
        {
            inventoryPanel.gameObject.SetActive(true);
            inventoryPanel.Show(items);
            isActive = true;
        }
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

    public void RemoveItem(ItemCptn item)
    {
        items.Remove(item);
    }

    public void RemoveItem(int id)
    {
        items.RemoveAt(id);
    }
}
