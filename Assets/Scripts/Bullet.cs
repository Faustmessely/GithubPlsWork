﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody _bulletRigidbody;
    public GameObject currentPlayer = null;
    PlayerController _currentPlayerCtrl;
    Transform _currentPlayerTransform;
    Bounds boundsBullet,boundsPlayer;
    float counterDespawn = 0;
    public bool InGebruik = false;
    public int playersLooking = 0;
    public int _bulletDMG;
    public bool cannonPower;
    public bool hooked;
    SphereCollider col;
    float _baseRadius;
    float _radiusOnHook;
    Renderer _bulletRender;
    void Start()
    {
        _bulletRigidbody = this.GetComponent<Rigidbody>();
        Mesh meshBullet = GetComponent<MeshFilter>().mesh;
        boundsBullet = meshBullet.bounds;
        col = this.GetComponent<SphereCollider>();
        _baseRadius = col.radius;
        _radiusOnHook = _baseRadius * 3f;
        _bulletRender = this.GetComponent<Renderer>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //If bullet is out of the camera delete it werkt enkel in build editor heeft zelf ook een camera
        if (!_bulletRender.isVisible)
        {
            Destroy(this.gameObject);
        }

        //Detect bullet DMG State
        DetectBulletDMGState(cannonPower);

        //check of size
        if(hooked && col.radius != _radiusOnHook)
        {
            col.radius = _radiusOnHook;
        }
        else if(!hooked && col.radius != _baseRadius)
        {
            col.radius = _baseRadius;
        }

        //OBJECT OPAKKEN
        if (currentPlayer != null)
        {
            if(hooked)
            {
                hooked = false;
            }
            _currentPlayerCtrl = currentPlayer.GetComponent<PlayerController>();
            _currentPlayerTransform = _currentPlayerCtrl.transform;
            Mesh meshPlayer = currentPlayer.GetComponent<MeshFilter>().mesh;
            boundsPlayer = meshPlayer.bounds;
            //VAN WELKE PLAYER MAG IK CONTROLS ONTVANGEN
            if (_currentPlayerCtrl.objectOpgenomen == false && InGebruik == false)
            {
                //check dat collider goe is
                //  Debug.Log("Object wordt opgenomen");
                Physics.IgnoreLayerCollision(9, 12, true);
                InGebruik = true;
                _bulletRigidbody.isKinematic = true;
                Parent(currentPlayer, this.transform.gameObject);
                this.transform.position = new Vector3(0, boundsPlayer.extents.y * _currentPlayerCtrl.transform.localScale.y,0)+ new Vector3(0, boundsBullet.extents.y * this.transform.localScale.y, 0) + _currentPlayerCtrl.transform.position;
                _currentPlayerCtrl.objectOpgenomen = true;
                //verwijder uit colllider lijst targetobject wordt null
               
            }
            else if (_currentPlayerCtrl.objectOpgenomen && _currentPlayerCtrl.pickup)
            {
                // Debug.Log("Object wordt losgelaten");
                InGebruik = false;
                _bulletRigidbody.isKinematic = false;
                this.transform.position = _currentPlayerTransform.position + (_currentPlayerCtrl.transform.forward * 5f);
                this.transform.parent = null;//parent weg zonder detach   
                _currentPlayerCtrl.objectOpgenomen = false;
                currentPlayer = null;
                _currentPlayerCtrl.pickup = false;
                Physics.IgnoreLayerCollision(9, 12, false);
            }
            else if (_currentPlayerCtrl.objectOpgenomen && _currentPlayerCtrl.throwing)
            {
                //  Debug.Log("Object ffkes weggooien");
                InGebruik = false;
                _bulletRigidbody.isKinematic = false;
                this.transform.parent = null;//parent weg zonder detach
                _bulletRigidbody.AddForce(_currentPlayerCtrl.transform.forward * 500f, ForceMode.Impulse);
                _currentPlayerCtrl.objectOpgenomen = false;
                currentPlayer = null;
                Physics.IgnoreLayerCollision(9, 12, false);
                //Invoke("DestroyBullet", 3);
            }
        }

    }
    void DestroyBullet()
    {
        Destroy(this.gameObject);
    }
   
    void DetectBulletDMGState(bool cannonPower)
    {
        if(cannonPower)
        {
            _bulletDMG = 50;
        }
        else
        {   
            _bulletDMG = 25;
        }
    }

    void Parent(GameObject parentOb, GameObject childOb)
    {
    childOb.transform.parent = parentOb.transform;
    }

}
