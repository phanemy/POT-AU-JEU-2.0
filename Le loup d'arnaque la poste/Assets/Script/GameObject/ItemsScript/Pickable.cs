using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pickable", menuName = "GameJam/Pickable", order = 1)]
public class Pickable : ItemCptn
{
    public Localisation location;
    public Rarete rarety;
    public Loot type;
}
