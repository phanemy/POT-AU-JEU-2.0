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
        Debug.Log("interact with cauldon");
        //return true because it isn't collectable the reference in play must be null only if player go aways
        return true;
    }
}
