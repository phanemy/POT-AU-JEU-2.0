using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    private Inventory inventaire;
    public InventoryPanel panel;
    public ItemCptn[] cptn = new ItemCptn[4];

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
        else if(Input.GetKeyDown(KeyCode.C))
        {
            inventaire.AddItem(cptn[0]);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            inventaire.AddItem(cptn[1]);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            inventaire.AddItem(cptn[2]);
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            inventaire.AddItem(cptn[3]);
        }
    }
}
