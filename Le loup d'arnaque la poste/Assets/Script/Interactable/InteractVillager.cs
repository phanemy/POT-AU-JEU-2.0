using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractVillager : MonoBehaviour, Interactable
{
    public SpriteRenderer childRend;
<<<<<<< Updated upstream
    public SpriteRenderer textrender;
=======
    public Text textContainer;
    public GameObject textrender;
>>>>>>> Stashed changes
    public string text;
    public float displayTime;
    private bool isShow;

<<<<<<< Updated upstream

=======
    public void Start()
    {
        textContainer.text = text;
    }
>>>>>>> Stashed changes

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

<<<<<<< Updated upstream
        textrender.enabled = true;
        isShow = true;
         yield return new WaitForSeconds(displayTime);
        isShow = false;
        textrender.enabled = false;
=======
        textrender.SetActive(true);
        isShow = true;
         yield return new WaitForSeconds(displayTime);
        isShow = false;
        textrender.SetActive(false);
>>>>>>> Stashed changes
    }
}
