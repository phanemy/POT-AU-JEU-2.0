using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pickable", menuName = "GameJam/Pickable", order = 2)]
[System.Serializable]
public class Pickable : ItemCptn
{
    [SerializeField]
    public Localisation location;
    [SerializeField]
    public Loot type;
}
