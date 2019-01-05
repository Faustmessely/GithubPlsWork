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
    Animation animCannon;
    // Use this for initialization
    void Start ()
    {
        animCannon = this.GetComponent<Animation>();
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

            animCannon.Play("CannonShoot");
            foreach (GameObject _bullet in _bullets)
            {
                Bullet _bulletScr = _bullet.GetComponent<Bullet>();//Zet bool op true wnr in cannon
                _bulletScr.cannonPower = true;
                _bullet.GetComponent<Rigidbody>().isKinematic = false;
                _bullet.GetComponent<Rigidbody>().AddForce(this.transform.Find("Barrel").forward * 500f, ForceMode.Impulse);               
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

            if(_currentStock < maxStock)
            {

                _currentStock += 1;// of add aan een list of array  


                Physics.IgnoreCollision(this.GetComponent<Collider>(), collision.gameObject.transform.GetChild(0).GetComponent<Collider>(), true);
                collision.transform.GetComponent<Rigidbody>().isKinematic = true;
                collision.transform.position = this.gameObject.transform.GetChild(0).position;
                _bullets.Add(collision.gameObject);        //voeg toe aan array van kogels voor dit kanon    collision.transform.position = ;
            }
            else
            {
                Debug.Log("destroy object");
                Destroy(collision.gameObject);

            }
        }
    }
}
