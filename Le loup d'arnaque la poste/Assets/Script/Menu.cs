using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject LoadingCanvas;
    public GameObject mainCanvas;
    public GameObject helpImage;
    public GameObject startButton;

    public RectTransform loadingBar;
    public Image imFadeOut;
    public GameObject slider;
    public float FadeTime;
    public AudioListener listenerToDeactivate;
    
    private float timeSinceFade;
    public static bool inGame;
    private  bool isLaunch;


    public void LaunchGame()
    {
        inGame = false;
        isLaunch = false;
        LoadingCanvas.SetActive(true);
        mainCanvas.SetActive(false);
        startButton.SetActive(false);
        StartCoroutine(LoadYourAsyncScene());
    }

    static void setInGame(bool b)
    {
        inGame = b;
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
        listenerToDeactivate.enabled = false;

        StartCoroutine(FadeOut());
    }

    public void Update()
    {
        if (isLaunch)
        {
            if(Input.GetKeyDown(KeyCode.Escape)){
                mainCanvas.SetActive(!mainCanvas.activeSelf);
                helpImage.SetActive(false);
                inGame = !mainCanvas.activeSelf;
            }
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public IEnumerator FadeOut()
    {
        timeSinceFade = 0;
        slider.SetActive(false);
        while(timeSinceFade < FadeTime)
        {
            yield return new WaitForSeconds(0.1f);
            timeSinceFade += 0.1f;
            imFadeOut.color = new Color(imFadeOut.color.r, imFadeOut.color.g, imFadeOut.color.b, (FadeTime - timeSinceFade) / FadeTime);
        }
        LoadingCanvas.SetActive(false);
        imFadeOut.color = new Color(imFadeOut.color.r, imFadeOut.color.g, imFadeOut.color.b, 1);
        inGame = true;
        isLaunch = true;
    }

    public void displayHelp()
    {
        helpImage.SetActive(!helpImage.activeSelf);
    }

    public void RestartMenu()
    {
        mainCanvas.SetActive(true);
        startButton.SetActive(true);

        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByBuildIndex(1));
    }
}
