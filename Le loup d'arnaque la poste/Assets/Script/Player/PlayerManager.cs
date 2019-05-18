using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BloodLust))]
[RequireComponent(typeof(Lycanthropy))]
public class PlayerManager : MonoBehaviour
{
    public float speed = 1f;
    public float runSpeed = 2f;
    public float runBloodLustCost = 0.001f;
    public GameObject loseScreen;
    public GameObject statCanvas;

    private Vector2 movement;
    private Transform camTransform;
    private BloodLust bloodLustComponent;
    private Lycanthropy lycanthropyComponent;
    bool isLose;

    private void Start()
    {
        bloodLustComponent = gameObject.GetComponent<BloodLust>();
        lycanthropyComponent = gameObject.GetComponent<Lycanthropy>();
        camTransform = Camera.main.transform;
        camTransform.position = new Vector3(transform.position.x, transform.position.y, camTransform.position.z);
        lycanthropyComponent.startGame(bloodLustComponent);
        bloodLustComponent.startGame();
    }

    // Update is called once per frame
    void Update()
    {
        if(isLose != true)
        {
            if (lycanthropyComponent.LycanthropyPercent == 1f)
            {
                DisplayLoseScreen();
            }
            else
            {

                float inputX = Input.GetAxis("Horizontal");
                float inputY = Input.GetAxis("Vertical");

                float run = Input.GetAxis("Run");

                transform.position = transform.position + new Vector3(inputX, inputY, 0) * Time.deltaTime * (((1 - run) * speed) + (run * runSpeed));
                if(run == 1)
                    bloodLustComponent.addBloodLust(runBloodLustCost);

                camTransform.position = new Vector3(transform.position.x, transform.position.y, camTransform.position.z);
            }
        }

    }

    void DisplayLoseScreen()
    {
        statCanvas.SetActive(false);
        loseScreen.SetActive(true);
        Debug.Log(loseScreen.activeSelf);
    }


}
