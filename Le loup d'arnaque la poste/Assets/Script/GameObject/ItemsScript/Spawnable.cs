using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spawnable", menuName = "GameJam/Spawnable", order = 1)]
public class Spawnable : ItemCptn
{
    public Localisation location;
    public Rarete rarety;
    public Loot type;
}
