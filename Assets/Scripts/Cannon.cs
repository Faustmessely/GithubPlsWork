using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Cannon : MonoBehaviour {

    public int maxStock = 1;
    int _currentStock = 0;
    public GameObject currentPlayer;
    bool _cannonLoaded;
    public List<GameObject> _bullets = new List<GameObject>();
    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        _cannonLoaded = LoadingCannon();
        //ShootingCannon(_cannonLoaded);
    }

    //private void ShootingCannon(bool CannonLoaded)
    //{
    //    if (CannonLoaded && //druk op schiet knop)
    //    {
    //        foreach (GameObject _bullet in _bullets)
    //        {
    //            //zet positie naar barrel child positie
    //            //zet een force tot de array op is
    //        }
    //        _currentStock = 0;
    //    }
    //}

    private bool LoadingCannon(bool _fullyLoaded = false)
    {      
        if (_currentStock == maxStock)
        {
            return _fullyLoaded = true;
        }
        else
        {
            return _fullyLoaded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("lol");
        _currentStock += 1;// of add aan een list of array
        collision.transform.position = this.gameObject.transform.GetChild(0).position ;
       // _bullets.Add(collision.gameObject);        //voeg toe aan array van kogels voor dit kanon    collision.transform.position = ;
    }
}
