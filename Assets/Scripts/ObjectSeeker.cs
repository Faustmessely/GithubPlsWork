﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSeeker : MonoBehaviour {

    //public
    public Vector3 fwd = Vector3.zero;
    public LayerMask mask;

    //private
    List<Collider> _currentInteractableColliderList;
    Vector3 _smallestInteractableDistance = Vector3.zero;
    Vector3 oudePos;
    int _playerMask;
    GameObject _previousTargetObject = null;
    bool _targetChange = false;
    Outline _targetObjectOutline; 
    GameObject _targetObject;
    bool _hit = false;   
    RaycastHit _hitInfo;
    PlayerController _player;

    // Use this for initialization
    void Start ()
    {
        _currentInteractableColliderList = new List<Collider>();
        _targetObject = null;
        _player = this.transform.parent.GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //if (!_player.objectOpgenomen)//if objectisopgenomen true targetobject is != opgenomen object
        //{
            SearchForTargetObject();
            IsPlayerLookingAtInteractable(_targetObject);
            GiveThisPlayerToTarget(_targetObject);
        //}
    }

    private void SearchForTargetObject()
    {
        //Raycast settings    
        _hitInfo = new RaycastHit();
        _hit = Physics.Raycast(this.transform.parent.position, this.transform.parent.forward, out _hitInfo, 1000f, mask);

        //checken dat er geen gedelete object in de list zit
        if(_currentInteractableColliderList.Contains(null))
        {
            _currentInteractableColliderList.Remove(null);
        }

        //Zoek mogelijke targets om op te nemen
        if (!_hit && _currentInteractableColliderList.Contains(this.GetComponentInParent<PlayerController>().lastObjectHit) && this.GetComponentInParent<PlayerController>().newCollissionCounter > 0)//Als player collission heeft & dit object zit ook in de view altijd de selectie
        {
           // Debug.Log("1");
            this.GetComponentInParent<PlayerController>().newCollissionCounter = 0;
            if (this.GetComponentInParent<PlayerController>().lastObjectHit != null)
            {
                _targetObject = this.GetComponentInParent<PlayerController>().lastObjectHit.gameObject;
            }
        }
        if (_currentInteractableColliderList.Count > 0 && _targetObject == null)//Er is nog geen object geselecteerd maar er zit wel een object in de view
        {
           // Debug.Log("2");
            foreach (Collider _colliderInteractable in _currentInteractableColliderList)
            {
                Vector3 interactableDistance = _colliderInteractable.transform.position - this.transform.parent.position;

                if (_smallestInteractableDistance == Vector3.zero)
                {
                    _smallestInteractableDistance = interactableDistance;
                }

                if (interactableDistance.magnitude <= _smallestInteractableDistance.magnitude)
                {
               
                    _smallestInteractableDistance = interactableDistance;
                    _targetObject = _colliderInteractable.transform.gameObject;
                   // Debug.Log(_targetObject.GetInstanceID());
                }
            }
            //reset waarden
            _smallestInteractableDistance = Vector3.zero;
        }
        else if (_currentInteractableColliderList.Contains(_hitInfo.collider))//Verschuif selectie of maak selectie als er nog geen is _hitInfo.collider && speler niet in een object zit
        {
           
            if (_hitInfo.transform.gameObject != _targetObject)//check of het verschillend is van huidige selectie
            {
               // Debug.Log("3");
                _targetObject = _hitInfo.transform.gameObject;
            }
        }
        else if (_targetObject != null)//&& _player.objectOpgenomen == false
        {
          
            if (!_currentInteractableColliderList.Contains(_targetObject.GetComponent<Collider>()) || _currentInteractableColliderList.Count <= 0)//als huidige target ni in de view zit dan deselecteer dit object
            {
               // Debug.Log("4");
                _targetObject = null;//TargetObject OFF
               // _smallestInteractableDistance = Vector3.zero;
            }
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

        if (TargetObject.GetComponent<Bullet>() && _player.pickup)
        {
            Bullet TargetObjectScr = TargetObject.GetComponent<Bullet>();
            TargetObjectScr.currentPlayer = this.transform.parent.gameObject;
        }

        if(TargetObject.GetComponent<Cannon>() && _player.action)
        {
            Cannon TargetObjectScr = TargetObject.GetComponent<Cannon>();
            TargetObjectScr.activated = true;
        }

        if (TargetObject.GetComponent<Hook>() && _player.action)
        {
            Hook TargetObjectScr = TargetObject.GetComponent<Hook>();
            TargetObjectScr.activated = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Voeg de distance vector toe van elk collision object waarbij de tag Interactable is en als er nog geen targetObject geselecteerd is  
        if (other.tag == "Interactable")
        {
            //if (other.GetComponent<Bullet>() != null)
            //{
            //    if (!other.transform.GetComponent<Bullet>().InGebruik)
            //    {
                    _currentInteractableColliderList.Add(other);
            //    }
            //}
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Voeg de distance vector toe van elk collision object waarbij de tag Interactable is en als er nog geen targetObject geselecteerd is  
        if (other.tag == "Interactable")
        {
            _currentInteractableColliderList.Remove(other);
         //   Physics.IgnoreCollision(other.GetComponent<Collider>(), this.GetComponent<Collider>(), false);
        }
    }
}
