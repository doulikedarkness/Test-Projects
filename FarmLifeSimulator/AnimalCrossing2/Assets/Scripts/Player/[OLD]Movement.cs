using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 12.0f;
    [SerializeField]
    private float verticalSpeed = 10.0f;
    [SerializeField]
    private GameObject playerObj;
    [SerializeField]
    private float gravity = -32f;

    public Transform groundChecker;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 ourPos;
    bool isGrounded;

    public CharacterController playerController;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(groundChecker.position, groundDistance, groundMask);

        if(isGrounded == true && ourPos.y < 0)
        {
            ourPos.y = -8f;
        }

        float horizontalInput = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
        float verticalInput = Input.GetAxis("Vertical") * verticalSpeed * Time.deltaTime;

        //transform.Translate(horizontalInput, 0, verticalInput);
        Vector3 move = transform.right * horizontalInput + transform.forward * verticalInput;
        playerController.Move(move);

        ourPos.y += gravity * Time.deltaTime;

        playerController.Move(ourPos * Time.deltaTime);

        #region INPUT
        if (Input.GetKey(KeyCode.A))
        {
            LookLeft();
        } 

        if (Input.GetKey(KeyCode.D))
        {
            LookRight();
        }

        if(Input.GetKey(KeyCode.W))
        {
            LookForward();
        }

        if(Input.GetKey(KeyCode.S))
        {
            LookBack();
        }

        if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
        {
            LookWA();
        }

        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            LookSA();
        }

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            LookWD();
        }

        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            LookSD();
        }
        #endregion
    }

    #region LOOKS
    void LookLeft()
    {
        playerObj.transform.localRotation = Quaternion.LookRotation(Vector3.left);
    }

    void LookRight()
    {
        playerObj.transform.localRotation = Quaternion.LookRotation(Vector3.right);
    }

    void LookForward()
    {
        playerObj.transform.localRotation = Quaternion.LookRotation(Vector3.forward);
    }

    void LookBack()
    {
        playerObj.transform.localRotation = Quaternion.LookRotation(Vector3.back);
    }

    void LookWA()
    {
        playerObj.transform.localRotation = Quaternion.LookRotation(new Vector3(-1, 0, 1));
    }

    void LookWD()
    {
        playerObj.transform.localRotation = Quaternion.LookRotation(new Vector3(1, 0, 1));
    }

    void LookSA()
    {
        playerObj.transform.localRotation = Quaternion.LookRotation(new Vector3(-1, 0, -1));
    }

    void LookSD()
    {
        playerObj.transform.localRotation = Quaternion.LookRotation(new Vector3(1, 0, -1));
    }
    #endregion



}
