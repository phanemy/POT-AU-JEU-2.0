using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BloodLust))]
public class Lycanthropy : MonoBehaviour
{

    public RectTransform lycanthropyUiSlider;
    public Potion effect0To1;
    public Potion effect1To2; 

    float progressTimeFromLast = 0f;
    public float LycanthropyPercent { get; private set; }
    private BloodLust bloodLustComponent;


    public int LycanthropyLevel
    {
        get
        {
            if (LycanthropyPercent < 0.33f)
                return 0;
            else if (LycanthropyPercent < 0.66f)
                return 1;
            else if (LycanthropyPercent <= 0.99f)
                return 2;
            else
                return 3;

        }
        private set { }
    }
   
    public void startGame(BloodLust bloodLustComponent)
    {
        LycanthropyPercent = 0f;
        this.bloodLustComponent = bloodLustComponent;
        UpdateSlider();
    }

    void Update()
    {
        if (!Menu.inGame)
            return;

        progressTimeFromLast += Time.deltaTime;
        float updateTime = bloodLustComponent.LycanthropeProgressFromBloodLust;
        if (progressTimeFromLast >= updateTime)
        {
            updateLycanthropy();
            progressTimeFromLast -= bloodLustComponent.LycanthropeProgressFromBloodLust;
        }
    }

    private void updateLycanthropy()
    {
        LycanthropyPercent += 0.01f;
        if (LycanthropyPercent > 1f)
            LycanthropyPercent = 1f;
        else if (LycanthropyPercent < 0f)
            LycanthropyPercent = 0f;

        UpdateSlider();
    }

    public void AddLycanthropyLevel(float count)
    {
        LycanthropyPercent -= count / 100f;
        if (LycanthropyPercent < 0)
            LycanthropyPercent = 0;
        else if (LycanthropyPercent > 1f)
            LycanthropyPercent = 1f;
        UpdateSlider();
    }

    void UpdateSlider()
    {
        int newSize = (int)(299 * LycanthropyPercent);
        lycanthropyUiSlider.sizeDelta = new Vector2(1 + newSize, lycanthropyUiSlider.rect.height);
    }
}