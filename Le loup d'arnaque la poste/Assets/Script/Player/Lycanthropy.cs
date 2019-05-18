using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BloodLust))]
public class Lycanthropy : MonoBehaviour
{

    public RectTransform lycanthropyUiSlider;


    float progressTimeFromLast = 0f;
    public float LycanthropyPercent { get; private set;}
    private BloodLust bloodLustComponent;

    public void startGame(BloodLust bloodLustComponent)
    {
        LycanthropyPercent = 0f;
        this.bloodLustComponent = bloodLustComponent;
        UpdateSlider();
    }

    void Update()
    {
        progressTimeFromLast += Time.deltaTime;
        int updateTime = bloodLustComponent.LycanthropeProgressFromBloodLust;
        if (progressTimeFromLast >= updateTime)
        {
            updateLycanthropy();
            progressTimeFromLast %= bloodLustComponent.LycanthropeProgressFromBloodLust;
        }
    }

    private void updateLycanthropy()
    {
        LycanthropyPercent += 0.01f ;
        if (LycanthropyPercent > 1f)
            LycanthropyPercent = 1f;
        else if (LycanthropyPercent < 0f)
            LycanthropyPercent = 0f;

        UpdateSlider();
    }

    void UpdateSlider()
    {
        int newSize = (int)(299 * LycanthropyPercent);
        lycanthropyUiSlider.sizeDelta = new Vector2(1 + newSize, lycanthropyUiSlider.rect.height);
    }
}