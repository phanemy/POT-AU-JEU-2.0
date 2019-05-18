using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodLust : MonoBehaviour
{
    public float progressTimeInSeconds = 1f;
    public int timeStep0_33 = 40;
    public int timeStep33_66 = 30;
    public int timeStep66_99 = 15;
    public int timeStep100 = 10;
    public RectTransform bloodLustUiSlider;
    public int LycanthropeProgressFromBloodLust
    {
        get {
            if (percent < 0.33f)
                return timeStep0_33;
            else if (percent < 0.66f)
                return timeStep33_66;
            else if (percent < 0.66f)
                return timeStep66_99;
            else
                return timeStep100;
        }
        private set { }
    }
   

    private float percent = 0.0f;

    public void startGame()
    {
        UpdateSlider();
        StartCoroutine(UpadteBloodLust());
    }

    public void  addBloodLust(float value)
    {
        percent += value / 100f;
        if (percent > 1f)
            percent = 1f;
        else if (percent < 0f)
            percent = 0f;

        UpdateSlider();
    }

    IEnumerator UpadteBloodLust()
    {
        while (true)
        {
            yield return new WaitForSeconds(progressTimeInSeconds);
            addBloodLust(1);
        }
    }

    void UpdateSlider()
    {
        int newSize = (int)(299 * percent);
        bloodLustUiSlider.sizeDelta =new Vector2( 1 + newSize, bloodLustUiSlider.rect.height);
    }
}
