using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldon : MonoBehaviour, Interactable
{
    public SpriteRenderer childRend;

    public void CanBeInteract(bool boolean)
    {
        childRend.enabled = boolean;
    }

    
    public bool interact(PlayerManager player)
    {
        player.inventory.ShowColdon();
        return true;
    }
}
