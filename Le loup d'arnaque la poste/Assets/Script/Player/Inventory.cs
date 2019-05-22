using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    static private Inventory inventory;
    static public Inventory Instance
    {
        get
        {
            if (inventory == null)
            {
                inventory = FindObjectOfType<Inventory>();
            }
            return inventory;
        }
    }

    private new List<ItemCptn> items = new List<ItemCptn>();
    private new List<ItemCptn> caldonItems = new List<ItemCptn>();
    public PlayerManager player;
    public AudioSource cauldonSound;
    public Potion defaultPotion;

    private InventoryPanel inventoryPanel;
    private ChaudronPanel chaudronPanel;

    public void Start()
    {
        inventoryPanel = GetComponentInChildren<InventoryPanel>();
        chaudronPanel = GetComponentInChildren<ChaudronPanel>();

        inventoryPanel.gameObject.SetActive(false);
        chaudronPanel.gameObject.SetActive(false);
    }

    public void Update()
    {
        if (Input.GetButtonDown("Inventary"))
        {
            Show();
        }
    }

    public bool IsInInventory()
    {
        if (inventoryPanel != null)
        {
            return inventoryPanel.gameObject.activeSelf;
        }
        return false;
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
        chaudronPanel.gameObject.SetActive(false);
        cauldonSound.Stop();
    }

    public void ShowColdon()
    {
        chaudronPanel.gameObject.SetActive(true);
        chaudronPanel.Show(caldonItems);
        cauldonSound.Play();
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
            chaudronPanel.Show(caldonItems);
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
            if (chaudronPanel.gameObject.activeSelf)
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
            chaudronPanel.Show(caldonItems);
            AddItem(item);
        }
    }

    public void CraftItem()
    {
        if (caldonItems.Count == Utils.NbSlotColdon)
        {
            bool ok = false;
            foreach(Recipe recipe in Utils.recipes)
            {
                ok = caldonItems.Except(recipe.Items).Count() == 0;

                if (ok)
                {
                    AddItem(recipe.potion);
                    break;
                }
            }

            if (!ok)
            {
                AddItem(defaultPotion);
            }

            caldonItems.Clear();
            chaudronPanel.Show(caldonItems);
        }
    }

    public void Clear()
    {
        items.Clear();
        inventoryPanel.Show(items);
    }
}
