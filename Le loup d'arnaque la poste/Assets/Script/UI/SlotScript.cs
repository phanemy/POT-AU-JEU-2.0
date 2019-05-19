using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotScript : MonoBehaviour
{
    public ItemCptn item;
    private Inventory inventory;
    private Image imageItem;
    private Image borderImage;
    private int id = -1;
    private bool isInventary;

    public void Start()
    {
        borderImage = GetComponent<Image>();
        imageItem = transform.GetChild(0).GetComponent<Image>();
    }

    public void Init(Inventory inv, bool isInventary)
    {
        inventory = inv;
        this.isInventary = isInventary;
    }

    public void Init(ItemCptn item, int id)
    {
        if(item != null && imageItem !=null)
        {
            this.id = id;
            this.item = item;
            imageItem.sprite = item.icon;
            imageItem.color = Color.white;
            switch (item.rarety)
            {
                case Rarete.Commun:
                    borderImage.color = Color.white;
                    break;

                case Rarete.Rare:
                    borderImage.color = Color.yellow;
                    break;

                case Rarete.Epique:
                    borderImage.color = Color.red;
                    break;

                case Rarete.Crafter:
                    borderImage.color = Color.magenta;
                    break;
            }
        }
    }

    public void Remove()
    {
        item = null;
        imageItem.color = Color.clear;
        imageItem.sprite = null;
        borderImage.color = Color.white;
        id = -1;
    }

    public void RemoveFromInventory()
    {
        inventory.RemoveItem(id, isInventary);
    }
}
