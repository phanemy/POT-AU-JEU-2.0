using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface Interactable
{
    bool interact(PlayerManager player);
    void CanBeInteract(bool boolean);

}
