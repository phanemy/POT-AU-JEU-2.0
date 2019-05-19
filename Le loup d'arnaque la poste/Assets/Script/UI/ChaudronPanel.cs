using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaudronPanel : MonoBehaviour
{
    public SlotScript slotPrefab;
    public Inventory inventory;
    private Transform slotPanelTransform;

    private List<SlotScript> slots = new List<SlotScript>();

    void Awake()
    {
        slotPanelTransform = transform.GetChild(0);

        for (int i = 0; i < Utils.NbSlotColdon; ++i)
        {
            SlotScript slot = Instantiate<SlotScript>(slotPrefab, slotPanelTransform);
            slot.Init(inventory, false);
            slots.Add(slot);
        }
    }

    public void Show(List<ItemCptn> items)
    {
        Debug.Log("items size " + items.Count);
        for (int i = 0; i < Utils.NbSlotColdon; ++i)
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
