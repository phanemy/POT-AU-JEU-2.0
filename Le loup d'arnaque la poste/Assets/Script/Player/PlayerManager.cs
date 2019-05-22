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
    [SerializeField]
    public float initialspeed = 1f;
    [SerializeField]
    public float speed { get; private set; }
    [SerializeField]
    public float initialRunSpeed = 2f;
    [SerializeField]
    public float runSpeed { get; private set; }
    [SerializeField]
    public float runBloodLustCost = 0.001f;
    public float attackBloodLustCost = 0.1f;
    public float gainWithKilling = 0.1f;
    [SerializeField]
    public GameObject loseScreen;
    [SerializeField]
    public GameObject statCanvas;
    [SerializeField]
    public SpriteManagerPlayer spriteManager;
    public AudioSource clipWalk;

    public Collider2D confinedCollider;
    
    private BloodLust bloodLustComponent;
    private Lycanthropy lycanthropyComponent;
    private CombatComponent combatComponent;
    private Rigidbody2D rb;
    public Interactable interactableItem;
    private Vector2 movement;
    private DirectionEnum dir;
    private int previousLevel;
    bool isLose;
    bool isMoving;

    private Cinemachine.CinemachineVirtualCamera cam;

    private void Start()
    {
        previousLevel = 0;
        speed = initialspeed;
        runSpeed = initialRunSpeed;
        dir = DirectionEnum.Bottom;
        spriteManager.init(gameObject.GetComponent < SpriteRenderer>(),dir);
        spriteManager.changeLycanthropieLevel(0);
        bloodLustComponent = gameObject.GetComponent<BloodLust>();
        lycanthropyComponent = gameObject.GetComponent<Lycanthropy>();
        combatComponent = gameObject.GetComponent<CombatComponent>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        lycanthropyComponent.startGame(bloodLustComponent);
        bloodLustComponent.startGame();
        Utils.Init();

        cam = FindObjectOfType<Cinemachine.CinemachineVirtualCamera>();
        cam.Follow = transform;
        FindObjectOfType<Cinemachine.CinemachineConfiner>().m_BoundingShape2D = confinedCollider;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Menu.inGame)
            return;

        if (Inventory.Instance.IsInInventory())
            return;

        if (!isLose && !combatComponent.isDead)
        {
            if (Input.GetButtonDown("Interact") && interactableItem != null )
            {
                //interact = true;
                if (interactableItem.interact(this))
                {
                    if (interactableItem != null)
                    {
                        interactableItem.CanBeInteract(false);
                        interactableItem = null;
                    }
                }
                //interact = false;
            }
            else if (Input.GetButtonDown("Fight") && combatComponent.CanAttack)
            {
                combatComponent.Attack(dir);
                bloodLustComponent.addBloodLust(attackBloodLustCost);
                if (spriteManager.update)
                {
                    clipWalk.Stop();
                    spriteManager.stop();
                }
            }
            else if (!combatComponent.isAttacking)
            {
                if (checkLevel())
                {
                    DisplayLoseScreen("Your Lycanthropy level As reach 100% you Lose ");
                }
                else
                {
                    float inputX = Input.GetAxis("Horizontal");
                    float inputY = Input.GetAxis("Vertical");
                    movement = new Vector3(inputX, inputY).normalized;
                    if (movement.magnitude > 0)
                    {
                        isMoving = true;
                        dir = DirectionEnumMethods.GetDirection(movement);
                        spriteManager.ActualDir = dir;

                        float run = Input.GetAxis("Run");


                        //transform.position = transform.position + movement * Time.deltaTime * (((1 - run) * speed) + (run * runSpeed));
                        movement *=/* Time.deltaTime * */(((1 - run) * speed) + (run * runSpeed));
                        if (run == 1)
                            bloodLustComponent.addBloodLust(runBloodLustCost * Time.deltaTime);

                        if (!spriteManager.update)
                        {
                            isMoving = true;
                            spriteManager.start();
                            clipWalk.Play();
                        }

                        spriteManager.Update();
                    }
                    else
                        if (spriteManager.update)
                        {
                            isMoving = false;
                            clipWalk.Stop();
                            spriteManager.stop();
                        }
                }
            }
        }
        else
        {
            if(!isLose)
            {
                DisplayLoseScreen("Your life as reach 0");
            }
        }
    }

    private void FixedUpdate()
    {
        if(isMoving)
        {
            rb.MovePosition(rb.position  + movement * Time.fixedDeltaTime);
        }
        else
        {
            rb.MovePosition(rb.position);
        }
    }

    void DisplayLoseScreen(string message)
    {
        isLose = true;
        statCanvas.SetActive(false);
        loseScreen.SetActive(true);
        Text text = loseScreen.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        if(text != null)
            text.text =message ;
    }

    private bool checkLevel()
    {
        switch(lycanthropyComponent.LycanthropyLevel)
        {
            case 3:
                return true;
            case 2:
                if (previousLevel != 2)
                {
                    previousLevel =2;
                    spriteManager.changeLycanthropieLevel(2);
                }
                return false;
            case 1:
                if (previousLevel != 1)
                {
                    previousLevel = 1;
                    spriteManager.changeLycanthropieLevel(1);
                }

                return false;
            case 0:
                if (previousLevel != 0)
                {
                    previousLevel = 0;
                    spriteManager.changeLycanthropieLevel(0);
                }
                return false;
            default:
                return false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Interactable")
        {
            if (interactableItem != null)
            {
                interactableItem.CanBeInteract(false);
                interactableItem = null;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Interactable")
        {
            if (interactableItem == null)
            {
                interactableItem = collision.gameObject.GetComponent<Interactable>();
                if (interactableItem != null)
                {
                    interactableItem.CanBeInteract(true);
                }
            }
        }
    }

    public void appplyEffect(Potion potion)
    {
        if (!potion.win)
        {
            combatComponent.health(potion.life);
            lycanthropyComponent.decreaseLevel((potion.Lycanthropie < 0) ? -potion.Lycanthropie : potion.Lycanthropie);

            speed += potion.speed;
            runSpeed += potion.runSpeed;
            combatComponent.attackSpeed += potion.attackSpeed;
            combatComponent.damage += potion.damage;
            if (potion.effectTime > 0)
                StartCoroutine(effectTime(potion));
        }
        else
        {
            DisplayLoseScreen("You have succefully cure yourless :D");
        }
    }

    IEnumerator effectTime(Potion potion)
    {
        yield return new WaitForSeconds(potion.effectTime);
        speed -= potion.speed;
        runSpeed -= potion.runSpeed;
        combatComponent.attackSpeed -= potion.attackSpeed;
        combatComponent.damage -= potion.damage;
    }

    public void InitRestart()
    {
        GameObject.Find("MenuManager").GetComponent<Menu>().RestartMenu();
    }

    public void mobDie(int count)
    {
        bloodLustComponent.addBloodLust(-attackBloodLustCost*(count) + gainWithKilling);
    }

}
