using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Potion", menuName = "GameJam/Potion", order = 3)]
public class Potion : ItemCptn
{
    public float life = 0;
    public float bloodLust = 0;
    public float Lycanthropie = 0;

    public float effectTime = 0;
    public float speed = 0;
    public float runSpeed = 0;
    public float attackSpeed = 0;
    public float damage = 0;
    public bool win = false;
}
