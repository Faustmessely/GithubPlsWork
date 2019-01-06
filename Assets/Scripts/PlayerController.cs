using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerController : MonoBehaviour
{
    public float speed = 60.0f;
    public float jumpSpeed = 3.0f;
    public float gravity = 9f;
    private Vector3 moveDirection = Vector3.zero;
    Vector3 _horizontalMovement = Vector3.zero;
    private CharacterController controller;
    Quaternion transformold;
    Vector3 _verticalMovement = Vector3.zero;
    public int collissionCounter = 0;
    public int newCollissionCounter = 0;
    public string inpHorizontal = "Horizontal_P1";
    public string inpVertical = "Vertical_P1";
    public string inpJump = "Jump_P1";
    public string inpPickup = "Interact_P1";
    public string inpThrowing = "Throwing_P1";
    public string inpAction = "Action_P1";
    public float horizontal;
    public float vertical;
    public bool jump;
    public bool pickup;
    public bool throwing;
    public bool action;
    public bool isInputActive = true;
    bool _interactableNearby = false;
    public bool objectOpgenomen = false;

    private void Start()
    {
        controller = this.GetComponent<CharacterController>();

    }


    private void Update()
    {
        InputActive(isInputActive);//Checken of er input active is
    }

    private void InputActive(bool isInputActive)
    {
        if (isInputActive)
        {         
            horizontal = Input.GetAxis(inpHorizontal);
            vertical = Input.GetAxis(inpVertical);
            jump = Input.GetButtonDown(inpJump);
            pickup = Input.GetButtonDown(inpPickup);
            throwing = Input.GetButtonDown(inpThrowing);
            action = Input.GetButtonDown(inpAction);
            Time.timeScale = 1;
        }
        else
        {          
            horizontal = 0;
            vertical = 0;
            jump = false;
            pickup = false;
            throwing = false;
            action = false;
            Time.timeScale = 0;
        }
    }

    private void Movement()
    {
        //horizontal movement

        _horizontalMovement = new Vector3(horizontal, 0f, vertical);
        if (_horizontalMovement.magnitude > 1f) { _horizontalMovement.Normalize(); }
        _horizontalMovement *= (speed * Time.deltaTime);
        //vertical movement(grav)
        if (controller.isGrounded)
        {
            if (jump)
            {
                _verticalMovement.y = jumpSpeed;
            }
            else
            {
                _verticalMovement.y = 0f; //GRAVITY
            }
        }
        _verticalMovement.y -= gravity * Time.deltaTime;

        //final movement vector
        moveDirection = _horizontalMovement + _verticalMovement;


        //look direction
        if (_horizontalMovement != Vector3.zero && _horizontalMovement.sqrMagnitude > 0f)
        {
            transform.rotation = Quaternion.Slerp(transformold, Quaternion.LookRotation(_horizontalMovement), 0.2f);
        }
        //final movement
        controller.Move(moveDirection);

        transformold = transform.rotation;
    }


    private void FixedUpdate()
    {
        Movement();       
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Interactable")
        {
            collissionCounter += 1;
            newCollissionCounter += 1;
        }
      
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Interactable")
        {
            collissionCounter -= 1;

            if(newCollissionCounter > 0)
            {
                newCollissionCounter -= 1;
            }
        }
    }
}
