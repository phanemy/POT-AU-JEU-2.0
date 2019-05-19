using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject LoadingCanvas;
    public GameObject mainCanvas;
    public RectTransform loadingBar;
    public Image imFadeOut;
    public GameObject slider;
    public float FadeTime;
    public AudioListener listenerToDeactivate;
    private float timeSinceFade;
    
    public void LaunchGame()
    {
        LoadingCanvas.SetActive(true);
        mainCanvas.SetActive(false);
        StartCoroutine(LoadYourAsyncScene());
    }

    IEnumerator LoadYourAsyncScene()
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            loadingBar.sizeDelta = new Vector2(asyncLoad.progress * 400, loadingBar.sizeDelta.y);
            yield return null;
        }

        StartCoroutine(FadeOut());
    }

    public IEnumerator FadeOut()
    {
        timeSinceFade = 0;
        slider.SetActive(false);
        while(timeSinceFade < FadeTime)
        {

            yield return new WaitForSeconds(0.1f);
            timeSinceFade += 0.1f;
            imFadeOut.color = new Color(imFadeOut.color.r, imFadeOut.color.g, imFadeOut.color.b, (1 - (FadeTime / timeSinceFade)));
        }
        LoadingCanvas.SetActive(false);
        listenerToDeactivate.enabled = false;
    }
}
