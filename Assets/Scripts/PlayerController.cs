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
    Outline _targetOutline = new Outline();
    public bool objectOpgenomen = false;
    GameObject _targetObject;
    bool _hit = false;
    public Vector3 fwd = Vector3.zero;
    RaycastHit _hitInfo;
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
    List<Collider> _currentInteractableColliderList;
    Vector3 _smallestInteractableDistance = Vector3.zero;
    // public GameObject bullet;
    Vector3 oudePos;
    int _playerMask;
    GameObject _previousTargetObject = null;
    bool _currentTarObjUsedByOtherPlayer = false;
    bool _targetChange = false;
    Outline _targetObjectOutline;
    public LayerMask mask;

    private void Start()
    {
        controller = this.GetComponent<CharacterController>();
        _currentInteractableColliderList = new List<Collider>();
        _targetObject = null;
    }


    private void Update()
    {
        InputActive(isInputActive);//Checken of er input active is

        if (!objectOpgenomen)
        {
            SearchForTargetObject();
            IsPlayerLookingAtInteractable(_targetObject);
            GiveThisPlayerToTarget(_targetObject);
        }
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


    private void SearchForTargetObject()
    {
        //Raycast settings    
        _hitInfo = new RaycastHit();       
        _hit = Physics.Raycast(transform.position, transform.forward, out _hitInfo, 1000f, mask);

        //Zoek mogelijke targets om op te nemen
        if (_hitInfo.collider == false && _targetObject == null && _currentInteractableColliderList.Count > 0)//Selecteer dichtste object als er nog geen object is
        {
    
            foreach (Collider _colliderInteractable in _currentInteractableColliderList)
            {
                Vector3 interactableDistance = _colliderInteractable.transform.position - this.transform.position;

                if (_smallestInteractableDistance == Vector3.zero)
                {
                    _smallestInteractableDistance = interactableDistance;
                }

                if (interactableDistance.magnitude <= _smallestInteractableDistance.magnitude)
                {
                    _smallestInteractableDistance = interactableDistance;
                    _targetObject = _colliderInteractable.transform.gameObject;
                }
            }
        }
        else if (_hitInfo.collider && _currentInteractableColliderList.Count > 0)//Verschuif selectie of maak selectie als er nog geen is
        {
            if (_hitInfo.transform.gameObject != _targetObject)
            {
         
                _targetObject = _hitInfo.transform.gameObject;
            }
        }
        else if (_hit == false && _currentInteractableColliderList.Count <= 0 && _targetObject != null && objectOpgenomen == false)
        {
            _targetObject = null;//TargetObject OFF
            _smallestInteractableDistance = Vector3.zero;
        }

    }

    private void IsPlayerLookingAtInteractable(GameObject TargetObject)
    {
        //Current Target Changed
        if (_targetObject != _previousTargetObject)
        {
            _targetChange = true;
        }
        else
        {
            _targetChange = false;
        }

        if (_targetChange)
        {
            if (_previousTargetObject != null)
            {
                //Outline OFF //zet outline uit van vorige target
                _targetObjectOutline.playersLooking--;
            }

            if (TargetObject != null)
            {
                //Outline ON //zet outline aan van current target
                _targetObjectOutline = TargetObject.GetComponent<Outline>();
                _targetObjectOutline.playersLooking++;
            }          
            _previousTargetObject = TargetObject;
        }
    }

    private void GiveThisPlayerToTarget(GameObject TargetObject)
    {
        if (TargetObject == null) return;

        if (TargetObject.GetComponent<Bullet>() && pickup)
        {
            Bullet TargetObjectScr = TargetObject.GetComponent<Bullet>();
            TargetObjectScr.currentPlayer = this.gameObject;
        }

    }

    private void FixedUpdate()
    {
        Movement();       
    }

    private void OnTriggerEnter(Collider other)
    {
        //Voeg de distance vector toe van elk collision object waarbij de tag Interactable is en als er nog geen targetObject geselecteerd is  
        if (other.tag == "Interactable")
        {
            if (other.GetComponent<Bullet>() != null)
            {
                if (!other.transform.GetComponent<Bullet>().InGebruik)
                {
                    _currentInteractableColliderList.Add(other);
                }
            }
                Physics.IgnoreCollision(other.GetComponent<Collider>(), this.GetComponent<Collider>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Voeg de distance vector toe van elk collision object waarbij de tag Interactable is en als er nog geen targetObject geselecteerd is  
        if (other.tag == "Interactable")
        {
            _currentInteractableColliderList.Remove(other);
            Physics.IgnoreCollision(other.GetComponent<Collider>(), this.GetComponent<Collider>(),false);
        }
    }

}
