using System.Collections;
using UnityEngine;

public class BloodLust : MonoBehaviour
{
    public float progressTimeInSeconds = 1.5f;
    public float timeStep0_25 = 60;
    public float timeStep25_50 = 40;
    public float timeStep50_75 = 20;
    public float timeStep75_99 = 10;
    public float timeStep100 = 5;
    public RectTransform bloodLustUiSlider;
    public float LycanthropeProgressFromBloodLust
    {
        get {
            if (percent < 0.25f)
                return timeStep0_25;
            else if (percent < 0.50f)
                return timeStep25_50;
            else if (percent < 0.75f)
                return timeStep50_75;
            else if (percent <= 0.99f)
                return timeStep75_99;
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
            if (Menu.inGame)
                addBloodLust(1);
        }
    }

    void UpdateSlider()
    {
        int newSize = (int)(299 * percent);
        bloodLustUiSlider.sizeDelta =new Vector2( 1 + newSize, bloodLustUiSlider.rect.height);
    }
}
