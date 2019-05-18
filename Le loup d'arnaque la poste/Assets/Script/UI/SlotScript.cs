using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotScript : MonoBehaviour
{
    public ItemCptn item;
    public int stack;
    private Image imageItem;
    private Text stackItem;

    public void Start()
    {
        imageItem = GetComponentInChildren<Image>();
        stackItem = GetComponentInChildren<Text>();
    }

    public void Init(ItemCptn item, int nb)
    {
        this.item = item;
        stack = nb;
        imageItem.sprite = item.icon;
        stackItem.text = "" + nb;
    }
}
