using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Potion", menuName = "GameJam/Potion", order = 3)]
public class Potion : ItemCptn
{
    public List<Effect> effects = new List<Effect>();
}
