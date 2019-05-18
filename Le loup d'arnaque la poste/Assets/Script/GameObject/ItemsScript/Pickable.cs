using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pickable", menuName = "GameJam/Pickable", order = 2)]
public class Pickable : ItemCptn
{
    public Localisation location;
    public Loot type;
}
