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
    [SerializeField]
    public SpriteManager spriteManager;

    private Vector3 movement;
    private Transform camTransform;
    private BloodLust bloodLustComponent;
    private Lycanthropy lycanthropyComponent;
    private DirectionEnum dir;

    bool isLose;

    private void Start()
    {
        spriteManager.init(gameObject.GetComponent < SpriteRenderer>());
        bloodLustComponent = gameObject.GetComponent<BloodLust>();
        lycanthropyComponent = gameObject.GetComponent<Lycanthropy>();
        camTransform = Camera.main.transform;
        camTransform.position = new Vector3(transform.position.x, transform.position.y, camTransform.position.z);
        lycanthropyComponent.startGame(bloodLustComponent);
        bloodLustComponent.startGame();
        dir = DirectionEnum.Bottom;
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
                movement = new Vector3(inputX, inputY, 0);
                if (movement.magnitude > 0)
                {
                    updateDir();
                    spriteManager.ActualDir = dir;

                    float run = Input.GetAxis("Run");

                    transform.position = transform.position + movement * Time.deltaTime * (((1 - run) * speed) + (run * runSpeed));
                    if (run == 1)
                        bloodLustComponent.addBloodLust(runBloodLustCost);

                    camTransform.position = new Vector3(transform.position.x, transform.position.y, camTransform.position.z);
                    if (!spriteManager.update)
                      spriteManager.restart();

                    spriteManager.Update();

                }
                else
                    if (spriteManager.update)
                        spriteManager.stop();
            }
        }

    }

    void DisplayLoseScreen()
    {
        statCanvas.SetActive(false);
        loseScreen.SetActive(true);
        Debug.Log(loseScreen.activeSelf);
    }

    void updateDir()
    {
        Vector2 right = new Vector2(1, 0);
        float angle = Vector2.SignedAngle(movement, right);
        if (angle > -45 && angle <= 45)
            dir = DirectionEnum.Right;
        else if (angle > 45 && angle <= 135)
            dir = DirectionEnum.Bottom;
        else if (angle > -135 && angle <= -45)
            dir = DirectionEnum.Top;
        else
            dir = DirectionEnum.Left;
    }

}
