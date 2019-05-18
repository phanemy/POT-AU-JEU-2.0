using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "GameJam/Recipe", order = 2)]
public class Recipe : ScriptableObject
{
    public List<Item> Items = new List<Item>();
}
