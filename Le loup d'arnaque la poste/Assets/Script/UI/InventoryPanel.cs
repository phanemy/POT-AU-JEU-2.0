using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
    public SlotScript slotPrefab;
    private Transform slotPanelTransform;

    private List<SlotScript> slots = new List<SlotScript>();

    private void Awake()
    {
        slotPanelTransform = transform.GetChild(0);

        for (int i = 0; i < Utils.NbSlot; ++i)
        {
            SlotScript slot = Instantiate<SlotScript>(slotPrefab, slotPanelTransform);
            slot.Init(true);
            slots.Add(slot);
        }
    }

    public void Show(List<ItemCptn> items)
    {
        for (int i = 0; i < Utils.NbSlot; ++i)
        {
            if (i < items.Count)
            {
                slots[i].SetItem(items[i], i);
            }
            else
            {
                slots[i].Remove();
            }
        }
    }
}
