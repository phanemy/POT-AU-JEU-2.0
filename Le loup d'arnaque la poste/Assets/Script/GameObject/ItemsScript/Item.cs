using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject, IComparable
{
    public string itemName;

    public int CompareTo(object obj)
    {
        Item other = obj as Item;
        if (other != null)
        {
            return itemName.CompareTo(other.itemName);
        }
        return -1;
    }
}
