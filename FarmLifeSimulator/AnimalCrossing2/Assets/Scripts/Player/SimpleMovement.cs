using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    [SerializeField] float gravity = -16f;
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float multiplyerSpeed = 0.003f;
    
    [SerializeField] float speed;

    public float groundDistance = 0.4f;
    public float ladderCheckDistance = 1f;
    public float climbingSpeed = 2f;

    public Items itemLadder;

    Vector3 playerPosition;
    Vector3 rayFromLegs;

    bool isGrounded;
    bool isClimbed;

    Ray ray;
    Ray rayLadder;
    RaycastHit hitinfo;

    public CharacterController controller;
    public Animator animator;

    public Transform groundChecker;

    public LayerMask groundMask;
    public LayerMask climbingMask;

    public GameObject playerModel;
    public GameObject facingPoint;

    private void Start()
    {
        //ДЛЯ ТЕСТОВ ОТКРЕПИЛ КУРСОР !!! ПОТОМ НЕ ЗАБУДЬ ЗАКРЕПИТЬ!!!
        //Cursor.lockState = CursorLockMode.Locked; 
    }

    private void FixedUpdate()
    {
        rayFromLegs = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        if (Physics.Raycast(ray, 2f, (1 << 8))) //IF hits layer 8 (ground) with range of 2f
        {
            Debug.Log("МЫ СТОЛКНУЛИСЬ СТЕНОЙ! МОЖНО СТАВИТЬ ЛЕСТНИЦА!");
            ClimbingWithLadder();
        }

        #region GROUNDING CHECK / GETTING AXIS / SIMPLE WASD MOVES
        isGrounded = Physics.CheckSphere(groundChecker.position, groundDistance, groundMask);
        isClimbed = Physics.CheckSphere(groundChecker.position, ladderCheckDistance, climbingMask);
        if (isGrounded == true || isClimbed == true && playerPosition.y < 0)
        {
            playerPosition.y = -8f; //просто немного давления чтобы персонаж не дергался над землей
        }

        playerPosition.y += gravity * Time.deltaTime; //увеличивает гравитацию с временем падения (дольше падаешь = быстрее)

        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            speed += multiplyerSpeed;
            animator.SetFloat("VelocityZ", speed);
        }
        else if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            speed += multiplyerSpeed;
            animator.SetFloat("VelocityZ", speed);
            animator.SetFloat("VelocityX", speed);
        }
        else if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            speed += multiplyerSpeed;
            animator.SetFloat("VelocityZ", speed);
            animator.SetFloat("VelocityX", -speed);
        }
        else
        {
            speed -= multiplyerSpeed;
            animator.SetFloat("VelocityZ", speed);
        }

        speed = Mathf.Clamp(speed, 0f, 10f);

        controller.Move(new Vector3(horizontalInput, 0, verticalInput) * speed * Time.deltaTime);
        controller.Move(playerPosition * Time.deltaTime);

        #endregion
        
        #region INPUT FOR PLAYER MODEL ROTATION
        if (Input.GetKey(KeyCode.A))
        {
            LookLeft();
            ray = new Ray(transform.position, -transform.right * 2f);
            rayLadder = new Ray(rayFromLegs, -transform.right * 1f);
            Debug.DrawRay(transform.position, -transform.right * 2f, Color.magenta);

            if (Physics.Raycast(rayLadder, 1f, (1 << 9)) && Input.GetKey(KeyCode.A))
            {
                Debug.Log("Столкнулись с лестницой");
                Debug.DrawRay(rayFromLegs, -transform.right * 1f, Color.yellow);
                controller.Move(new Vector3(-1 * Time.deltaTime, 1, 0) * climbingSpeed * Time.deltaTime);
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            LookRight();
            ray = new Ray(transform.position, transform.right * 2f);
            rayLadder = new Ray(rayFromLegs, transform.right * 1f);
            Debug.DrawRay(transform.position, transform.right * 2f, Color.magenta);

            if (Physics.Raycast(rayLadder, 1f, (1 << 9)) && Input.GetKey(KeyCode.D))
            {
                Debug.Log("Столкнулись с лестницой");
                Debug.DrawRay(rayFromLegs, transform.right * 1f, Color.yellow);
                controller.Move(new Vector3(1 * Time.deltaTime, 1, 0) * climbingSpeed * Time.deltaTime);
            }
        }

        if (Input.GetKey(KeyCode.W))
        {
            LookForward();
            ray = new Ray(transform.position, transform.forward * 2f);
            rayLadder = new Ray(rayFromLegs, transform.forward * 1f);
            Debug.DrawRay(transform.position, transform.forward * 2f, Color.magenta);

            if (Physics.Raycast(rayLadder, 1f, (1 << 9)) && Input.GetKey(KeyCode.W))
            {
                Debug.Log("Столкнулись с лестницой");
                Debug.DrawRay(rayFromLegs, transform.forward * 1f, Color.yellow);
                controller.Move(new Vector3(0, 1, 1 * Time.deltaTime) * climbingSpeed * Time.deltaTime);
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            LookBack();
            ray = new Ray(transform.position, -transform.forward * 2f);
            rayLadder = new Ray(rayFromLegs, -transform.forward * 1f);
            Debug.DrawRay(transform.position, -transform.forward * 2f, Color.magenta);

            if (Physics.Raycast(rayLadder, 1f, (1 << 9)) && Input.GetKey(KeyCode.S))
            {
                Debug.Log("Столкнулись с лестницой");
                Debug.DrawRay(rayFromLegs, -transform.forward * 1f, Color.yellow);
                controller.Move(new Vector3(0, 1, -1 * Time.deltaTime) * climbingSpeed * Time.deltaTime);
            }
        }

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
        {
            LookWA();
            ray = new Ray(transform.position, (transform.forward + (-transform.right)) * 2f);
            Debug.DrawRay(transform.position, (transform.forward + (-transform.right)), Color.magenta);
        }

        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            LookSA();
            ray = new Ray(transform.position, (-transform.forward + (-transform.right)) * 2f);
            Debug.DrawRay(transform.position, (-transform.forward + (-transform.right)), Color.magenta);
        }

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            LookWD();
            ray = new Ray(transform.position, (transform.forward + transform.right) * 2f);
            Debug.DrawRay(transform.position, (transform.forward + transform.right), Color.magenta);
        }

        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            LookSD();
            ray = new Ray(transform.position, (-transform.forward + transform.right) * 2f);
            Debug.DrawRay(transform.position, (-transform.forward + transform.right), Color.magenta);
        }
        #endregion
    
    }

    void ClimbingWithLadder()
    {
        if (Inventory.instance.itemsInInventory.Contains(itemLadder) && Input.GetButtonDown("Climb"))
        {
            GameObject spawnedLadder = Instantiate(itemLadder.itemToSpawnPlacedOrHanded, facingPoint.transform.position, Quaternion.RotateTowards(playerModel.transform.rotation, facingPoint.transform.rotation, 0f));
            Inventory.instance.Remove(itemLadder);
        }
    }

    #region PLAYER MODEL ROTATION
    void LookLeft()
    {
        playerModel.transform.localRotation = Quaternion.LookRotation(Vector3.left);
    }

    void LookRight()
    {
        playerModel.transform.localRotation = Quaternion.LookRotation(Vector3.right);
    }

    void LookForward()
    {
        playerModel.transform.localRotation = Quaternion.LookRotation(Vector3.forward);
    }

    void LookBack()
    {
        playerModel.transform.localRotation = Quaternion.LookRotation(Vector3.back);
    }

    void LookWA()
    {
        playerModel.transform.localRotation = Quaternion.LookRotation(new Vector3(-1, 0, 1));
    }

    void LookWD()
    {
        playerModel.transform.localRotation = Quaternion.LookRotation(new Vector3(1, 0, 1));
    }

    void LookSA()
    {
        playerModel.transform.localRotation = Quaternion.LookRotation(new Vector3(-1, 0, -1));
    }

    void LookSD()
    {
        playerModel.transform.localRotation = Quaternion.LookRotation(new Vector3(1, 0, -1));
    }
    #endregion
}
