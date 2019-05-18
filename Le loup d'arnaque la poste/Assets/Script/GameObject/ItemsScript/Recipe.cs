using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "GameJam/Recipe", order = 4)]
public class Recipe : ScriptableObject
{
    public Item[] Items = new Item[3];
    public Potion potion;
}
