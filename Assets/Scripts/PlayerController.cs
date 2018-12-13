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
    GameObject _targetObject = null;
    bool _hit = false;
    public Vector3 fwd = Vector3.zero;
    RaycastHit _hitInfo;
    public string horizontal = "Horizontal_P1";
    public string vertical = "Vertical_P1";
    public string jump = "Jump_P1";
    public string interactable = "Interact_P1";
    public string throwing = "Throwing_P1";
    public string interact2 = "Action_P1";
    string[] scriptID;
    bool _interactableNearby = false;
   // public GameObject bullet;
    Vector3 oudePos;
    bool currentTargetObjectInGebruik;
    private void Start()
    {
        controller = this.GetComponent<CharacterController>();
        //Maak array aan met alle mogelijke interactable scripts;
        scriptID = new string[] {"Bullet", "Cannon"};
     //   oudePos = bullet.transform.position;

    }


    private void Update()
    {
      

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


    private void Interactables()
    {

        ////Check voor interactables       
        _hitInfo = new RaycastHit();
        fwd = transform.TransformDirection(Vector3.forward);
        // Bit shift the index of the layer (8) to get a bit mask
        int _layerMask = 1 << 10;
        //inverse bitmask layer
        //_layerMask = ~_layerMask;!handig
        _hit = Physics.Raycast(transform.position, fwd, out _hitInfo, 2f, _layerMask);
        Debug.DrawRay(transform.position, transform.forward, Color.green);
        if (_hitInfo.collider)//Als de raycast iets hit & _targetobject verwijst nog niet naar het object
        {
            Debug.Log("werkt");
            if (_targetObject == null)
            {
                _targetObject = _hitInfo.transform.gameObject;
                if (_targetObject.transform.tag == "Interactable")//check of het opgeslagen object interactable is
                {
                  //  Debug.Log("hit2" + _targetObject.name);
                    //Als code al enabled is dan is het item al in gebruik door andere speler
                    MonoBehaviour[] scripts = _targetObject.GetComponents<MonoBehaviour>();

                    foreach (MonoBehaviour script in scripts)
                    {
                        //een array van strings en loopen door de array
                        for (int i = 0; i < scriptID.Length - 1; i++)
                        {
                            if (script == _targetObject.GetComponent(scriptID[0]))
                            {
                                if (_targetObject.GetComponent<Bullet>().InGebruik == false)
                                {
                                    _targetObject.GetComponent<Bullet>().currentPlayer = this.transform.gameObject;//DIT WERKT         
                                }
                                else
                                {
                                    currentTargetObjectInGebruik = true;
                                }
                            }
                            else if (script == _targetObject.GetComponent(scriptID[1]))
                            {
                                _targetObject.GetComponent<Cannon>().currentPlayer = this.transform.gameObject;//DIT WERKT            
                            }

                        }
                        script.enabled = true;
                        //outline effect ON
                        _targetOutline = _targetObject.GetComponent<Outline>();
                        _targetOutline.OutlineWidth = 10f;
                    }
                }
            }
        }       
        else if (_hit == false && _targetObject != null && objectOpgenomen == false && currentTargetObjectInGebruik == false)//delete object verwijzing want er wordt niet meer naar gekeken
        {            
            MonoBehaviour[] scripts = _targetObject.GetComponents<MonoBehaviour>();
            Outline _targetObjectOutline = _targetObject.GetComponent<Outline>();
            //DESACTIVEER CODE VAN INTERACTABLE
            foreach (MonoBehaviour script in scripts)
            {
                if (script != _targetObjectOutline)
                {
                    script.enabled = false;
                }                
            }
            //zet object op null nadat script uit zijn gezet
            _targetObject = null;

            //outline effect OFF
            _targetOutline.OutlineWidth = 0f;
        }
    }

    private void FixedUpdate()
    {
        Movement();
        Interactables();
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    _interactableNearby = true;
    //    if(_targetObject == null)
    //    {
    //        _targetObject = other.transform.gameObject;
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    _interactableNearby = false;
    //}

    //void OnDrawGizmos()
    //{
    //    Handles.Label(transform.position, "x:" + moveDirection.x);
    //    Handles.Label(transform.position + Vector3.down, "y:" + moveDirection.y);
    //    Handles.Label(transform.position + Vector3.down * 2, "z:" + moveDirection.z);
    //}
}
