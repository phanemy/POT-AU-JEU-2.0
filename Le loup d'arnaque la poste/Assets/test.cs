using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    private Inventory inventaire;
    public InventoryPanel panel;

    // Start is called before the first frame update
    void Start()
    {
        Utils.Init();
        inventaire = new Inventory();
        inventaire.inventoryPanel = panel;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            inventaire.Show();
        }
    }
}
