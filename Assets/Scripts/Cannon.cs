using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Cannon : MonoBehaviour {

    public int maxStock = 1;
    int _currentStock = 0;
    public GameObject currentPlayer;
    bool _cannonLoaded;
    public List<GameObject> _bullets = new List<GameObject>();
    public bool activated;
    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        _cannonLoaded = LoadingCannon();
        ShootingCannon(_cannonLoaded);
    }

    private void ShootingCannon(bool CannonLoaded)
    {
        if (CannonLoaded && activated)
        {
            Debug.Log("SHOOOOOT");
            foreach (GameObject _bullet in _bullets)
            {
                _bullet.GetComponent<Rigidbody>().isKinematic = false;
                _bullet.GetComponent<Rigidbody>().AddForce(this.transform.GetChild(0).forward * 500f, ForceMode.Impulse);               
            }
            _bullets.Clear();
            activated = false;
            _currentStock = 0;
        }
        activated = false;
    }

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
        if (collision.gameObject.tag == "Interactable")
        {


            if (_currentStock >= maxStock)
            {
                Debug.Log("destroy object");
                Destroy(collision.gameObject);
            }
            else
            {

                _currentStock += 1;// of add aan een list of array  
                Debug.Log(_currentStock + "/" + maxStock);

                Physics.IgnoreCollision(this.GetComponent<Collider>(), collision.gameObject.transform.GetChild(0).GetComponent<Collider>(), true);
                collision.transform.GetComponent<Rigidbody>().isKinematic = true;
                collision.transform.position = this.gameObject.transform.GetChild(0).position;
                _bullets.Add(collision.gameObject);        //voeg toe aan array van kogels voor dit kanon    collision.transform.position = ;
            }
        }
    }
}
