using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public ItemCptn item;
    private Image imageItem;
    private Image borderImage;
    private int id = -1;
    private bool isInventary;

    private void Start()
    {
        borderImage = GetComponent<Image>();
        imageItem = transform.GetChild(0).GetComponent<Image>();
    }

    public void Init(bool isInventary)
    {
        this.isInventary = isInventary;
    }

    public void SetItem(ItemCptn item, int id)
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
        Inventory.Instance.RemoveItem(id, isInventary);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
        {
            Inventory.Instance.ShowTooltip(transform.position, item.itemName + "\n" + item.description);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Inventory.Instance.HideTooltip();
    }
}
