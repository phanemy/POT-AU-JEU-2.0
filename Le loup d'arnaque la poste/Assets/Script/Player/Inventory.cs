using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public InventoryPanel inventoryPanel;
    public ChaudronPanel ChaudronPanel;
    private new List<ItemCptn> items = new List<ItemCptn>();
    public ItemCptn potion;
    public PlayerManager player;
    private bool caldonVisible = false;

    public void Start()
    {
        inventoryPanel.gameObject.SetActive(false);
        ChaudronPanel.gameObject.SetActive(false);
        items.Add(potion);
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
        caldonVisible = false;
        ChaudronPanel.gameObject.SetActive(false);
    }

    public void ShowColdon()
    {
        caldonVisible = true;
        ChaudronPanel.gameObject.SetActive(true);
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
        Potion pop = item as Potion;
        if (pop != null)
            player.appplyEffect(pop);

        items.Remove(item);
        inventoryPanel.Show(items);

    }

    public void RemoveItem(int id)
    {
        if (id >= 0 && id < items.Count)
        {
            Potion pop = items[id] as Potion;
            if (pop != null)
                player.appplyEffect(pop);
            items.RemoveAt(id);
            inventoryPanel.Show(items);
        }
    }

    public void Clear()
    {
        items.Clear();
        inventoryPanel.Show(items);
    }
}
