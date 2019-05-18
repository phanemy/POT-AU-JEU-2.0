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
        Debug.Log("Init");
        slotPanelTransform = transform.GetChild(0);
        
        for(int i = 0; i < 16; ++i)
        {
            slots.Add(Instantiate<SlotScript>(slotPrefab, slotPanelTransform));
        }
    }

    public void Show(List<ItemCptn> items)
    {
        for (int i = 0; i < items.Count; ++i)
        {
            slots[i].Init(items[i], 0);
        }
    }
}
