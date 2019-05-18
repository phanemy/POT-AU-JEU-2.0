using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
    public SlotScript slotPrefab;
    private Transform slotPanelTransform;

    private List<SlotScript> slots = new List<SlotScript>();

    void Start()
    {
        slotPanelTransform = transform.GetChild(0);
        
        for(int i = 0; i < Utils.NbSlot; ++i)
        {
            slots.Add(Instantiate<SlotScript>(slotPrefab, slotPanelTransform));
        }
    }

    public void Show(List<ItemCptn> items)
    {
        for (int i = 0; i < Utils.NbSlot; ++i)
        {
            if (i < items.Count)
            {
                slots[i].Init(items[i], i);
            }
            else
            {
                slots[i].Remove();
            }
        }
    }
}
