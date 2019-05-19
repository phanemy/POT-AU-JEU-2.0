using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractVillager : MonoBehaviour, Interactable
{
    public SpriteRenderer childRend;
    public SpriteRenderer textrender;

    public string text;
    public float displayTime;
    private bool isShow;


    public void CanBeInteract(bool boolean)
    {
        childRend.enabled = boolean;
    }


    public bool interact(PlayerManager player)
    {
        if (!isShow)
        {
            StartCoroutine(displayText());
            return true;
        }
        else
            return false;
    }

    public IEnumerator displayText()
    {
        textrender.enabled = true;
        isShow = true;
         yield return new WaitForSeconds(displayTime);
        isShow = false;
        textrender.enabled = false;

    }
}
