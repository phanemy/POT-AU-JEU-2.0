using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public InventoryPanel inventoryPanel;
    public List<ItemCptn> items = new List<ItemCptn>();
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
}
