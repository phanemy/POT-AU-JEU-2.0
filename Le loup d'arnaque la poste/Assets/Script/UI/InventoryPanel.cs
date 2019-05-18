using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
    public GameObject slotPrefab;
    private Transform slotPanelTransform;

    private List<SlotScript> slots = new List<SlotScript>();

    void Start()
    {
        slotPanelTransform = transform.GetChild(0);
        
        for(int i = 0; i < 16; ++i)
        {
            slots.Add(Instantiate<GameObject>(slotPrefab, slotPanelTransform).GetComponent<SlotScript>());
        }
    }

    
}
