using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaudronPanel : MonoBehaviour
{
    public SlotScript slotPrefab;
    private Transform slotPanelTransform;

    private List<SlotScript> slots = new List<SlotScript>();

    private void Awake()
    {
        slotPanelTransform = transform.GetChild(0);

        for (int i = 0; i < Utils.NbSlotColdon; ++i)
        {
            SlotScript slot = Instantiate<SlotScript>(slotPrefab, slotPanelTransform);
            slot.Init(false);
            slots.Add(slot);
        }
    }

    public void Show(List<ItemCptn> items)
    {
        for (int i = 0; i < Utils.NbSlotColdon; ++i)
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
