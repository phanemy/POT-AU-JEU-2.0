using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BloodLust))]
[RequireComponent(typeof(Lycanthropy))]
[RequireComponent(typeof(CombatComponent))]
[System.Serializable]
public class PlayerManager : MonoBehaviour
{
    public float speed = 1f;
    [SerializeField]
    public float runSpeed = 2f;
    [SerializeField]
    public float runBloodLustCost = 0.001f;
    [SerializeField]
    public GameObject loseScreen;
    [SerializeField]
    public GameObject statCanvas;
    [SerializeField]
    public SpriteManager spriteManager;
    [SerializeField]
    public Inventory inventory;

    private Transform camTransform;
    private BloodLust bloodLustComponent;
    private Lycanthropy lycanthropyComponent;
    private CombatComponent combatComponent;
    public ItemPrefab pickableItem;
    private Vector3 movement;
    private DirectionEnum dir;

    bool isLose;

    private void Start()
    {
        spriteManager.init(gameObject.GetComponent < SpriteRenderer>());
        bloodLustComponent = gameObject.GetComponent<BloodLust>();
        lycanthropyComponent = gameObject.GetComponent<Lycanthropy>();
        combatComponent = gameObject.GetComponent<CombatComponent>();
        camTransform = Camera.main.transform;
        camTransform.position = new Vector3(transform.position.x, transform.position.y, camTransform.position.z);
        lycanthropyComponent.startGame(bloodLustComponent);
        bloodLustComponent.startGame();
        dir = DirectionEnum.Bottom;
        Utils.Init();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isLose || !combatComponent.isDead)
        {
            if(Input.GetAxis("Interact") != 0 && pickableItem != null)
            {
                if (inventory.AddItem(pickableItem.item))
                {
                    Destroy(pickableItem);
                    pickableItem = null;
                }
            }
            else if (Input.GetAxis("Fight") != 0 && combatComponent.CanAttack)
            {
                combatComponent.Attack(dir);

                if (spriteManager.update)
                    spriteManager.stop();
            }
            else if (!combatComponent.isAttacking)
            {
                if (lycanthropyComponent.LycanthropyPercent == 1f)
                {
                    DisplayLoseScreen();
                }
                else
                {
                    float inputX = Input.GetAxis("Horizontal");
                    float inputY = Input.GetAxis("Vertical");
                    movement = new Vector3(inputX, inputY, 0).normalized;
                    if (movement.magnitude > 0)
                    {
                        dir = DirectionEnumMethods.GetDirection(movement);
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
        else
        {
            if(!isLose)
            {
                DisplayLoseScreen();
            }
        }
    }

    void DisplayLoseScreen()
    {
        isLose = true;
        statCanvas.SetActive(false);
        loseScreen.SetActive(true);        
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Pickable")
        {
            if (pickableItem != null)
            {
                pickableItem.CanBeGather(false);
                pickableItem = null;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Pickable")
        {
            if (pickableItem == null)
            {
                pickableItem = collision.gameObject.GetComponent<ItemPrefab>();
                if (pickableItem != null)
                    pickableItem.CanBeGather(true);
            }
        }
    }
}
