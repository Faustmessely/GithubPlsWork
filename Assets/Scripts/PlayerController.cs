using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerController : MonoBehaviour
{
    public float speed = 3.0f;
    public float jumpSpeed = 4.0f;
    public float gravity = 20f;
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
    public string horizontal = "Horizontal_P1";
    public string vertical = "Vertical_P1";
    public string jump = "Jump_P1";
    public string interactable = "Interact_P1";
    public string throwing = "Throwing_P1";
    public string interact2 = "Action_P1";
    bool _interactableNearby = false;
    List<Collider> _currentInteractableColliderList;
    Vector3 _smallestInteractableDistance = Vector3.zero;
    // public GameObject bullet;
    Vector3 oudePos;
    int _playerMask;
    GameObject _previousTargetObject = null;
    bool _currentTarObjUsedByOtherPlayer = false;
    bool _targetChange = false;

    private void Start()
    {
        controller = this.GetComponent<CharacterController>();
        //   oudePos = bullet.transform.position;
        _currentInteractableColliderList = new List<Collider>();
        //_playerMask = ~(1 << 10);//Geen raycast op de player layer(9)
        _targetObject = null;
    }


    private void Update()
    {
        SearchForTargetObject();
        InteractableBehavior(_targetObject);

        //if (Input.GetButtonDown(interactable))
        //{

        //    GameObject newBullet = Instantiate(bullet, oudePos, Quaternion.identity);
        //    newBullet.name = bullet.name;

        //}

    }

    private void Movement()
    {
        //horizontal movement

        _horizontalMovement = new Vector3(Input.GetAxis(horizontal), 0f, Input.GetAxis(vertical));
        if (_horizontalMovement.magnitude > 1f) { _horizontalMovement.Normalize(); }
        _horizontalMovement *= (speed * Time.deltaTime);
        //vertical movement(grav)
        if (controller.isGrounded)
        {
            if (Input.GetButtonDown(jump))
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
        fwd = transform.TransformDirection(Vector3.forward);
        _hit = Physics.Raycast(transform.position, fwd, out _hitInfo, 1000f);
        // Debug.DrawRay(transform.position, transform.forward, Color.red);

        //Zoek mogelijke targets om op te nemen
        if (_hitInfo.collider == false && _targetObject == null && _currentInteractableColliderList.Count > 0)//Selecteer dichtste object als er nog geen object is
        {
            Debug.Log("1ste Target");
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

        }//als ge naar niks kijkt maar er is wel een object in de
        else if (_hitInfo.collider && _currentInteractableColliderList.Count > 0)//Verschuif selectie of maak selectie als er nog geen is
        {
            if (_hitInfo.transform.gameObject != _targetObject)
            {
                Debug.Log("Target Change");
                _targetObject = _hitInfo.transform.gameObject;
            }
        }
        else if (_hit == false && _currentInteractableColliderList.Count <= 0 && _targetObject != null && objectOpgenomen == false && _currentTarObjUsedByOtherPlayer == false)
        {
            Debug.Log("ik kijk naar niks dus reset reset");
            Debug.Log(_currentInteractableColliderList.Count);
            //Zet alleen op null als er echt niks geselecteerd is
            _targetObject = null;//TargetObject OFF
            _smallestInteractableDistance = Vector3.zero;
        }

     
        
    }

    private void InteractableBehavior(GameObject TargetObject)
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
          
            if (_currentTarObjUsedByOtherPlayer == false && _previousTargetObject != null)
            {
                //Outline OFF //zet ouline uit van vorige target
                _targetOutline = _previousTargetObject.GetComponent<Outline>();
                _targetOutline.OutlineWidth = 0;

                //if (_previousTargetObject.GetComponent<Bullet>() != null)//Als het script bestaat
                //{
                //    if (_previousTargetObject.GetComponent<Bullet>().InGebruik == false)//Als het object nog niet in gebruik is
                //    {
                //        _previousTargetObject.GetComponent<Bullet>().enabled = false;
                //        _previousTargetObject.GetComponent<Bullet>().currentPlayer = null;
                //    }
                //}
            }

            if (TargetObject != null)
            {
                //Outline ON
                _targetOutline = TargetObject.GetComponent<Outline>();
                _targetOutline.OutlineWidth = 10;

                if (TargetObject.GetComponent<Bullet>() != null)//Als het script bestaat
                {
                    if (_targetObject.GetComponent<Bullet>().currentPlayer == null)//Als het object nog niet in gebruik is
                    {
                        _currentTarObjUsedByOtherPlayer = false;
                        _targetObject.GetComponent<Bullet>().enabled = true;
                        _targetObject.GetComponent<Bullet>().currentPlayer = this.transform.gameObject;
                    }
                    else
                    {
                        _currentTarObjUsedByOtherPlayer = true;
                    }
                }
            }
            _previousTargetObject = TargetObject;
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
            _currentInteractableColliderList.Add(other);          
        }


        //check op empty list
        //maak list empty als er geen target meer is
        //als er geen collision meer is of raycasthit 
    }

    private void OnTriggerExit(Collider other)
    {
        //Voeg de distance vector toe van elk collision object waarbij de tag Interactable is en als er nog geen targetObject geselecteerd is  
        if (other.tag == "Interactable")
        {
            _currentInteractableColliderList.Remove(other);
        }


        //check op empty list
        //maak list empty als er geen target meer is
        //als er geen collision meer is of raycasthit 
    }

    //void OnDrawGizmos()
    //{
    //    Handles.Label(transform.position, "x:" + moveDirection.x);
    //    Handles.Label(transform.position + Vector3.down, "y:" + moveDirection.y);
    //    Handles.Label(transform.position + Vector3.down * 2, "z:" + moveDirection.z);
    //}
}
