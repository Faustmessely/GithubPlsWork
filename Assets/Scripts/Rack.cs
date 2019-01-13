using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Rack : MonoBehaviour {

    int _stackCount = 0;
    int _maxStack = 5;
    [SerializeField] GameObject[] positions;
    //[SerializeField] Collider Trigger;
    List<GameObject> _bulletsInChest;
    List<GameObject> _bulletsInCollider;
    List<GameObject> _bulletsInUse;
    int CountInUse = 0;
    GameObject _currentBullet;
    int _previousStack = 0;


    // Use this for initialization
    void Start()
    {
        _bulletsInChest = new List<GameObject>();
        _bulletsInCollider = new List<GameObject>();
        _bulletsInUse = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log("Bulletcollider count dus mogelijke bullets:" + _bulletsInCollider.Count);
        AddChestItem();

    }

    void AddChestItem()
    {
        while (_bulletsInChest.Count < _maxStack && _bulletsInCollider.Count > 0)//Als kist niet vol is en er zitten objecten in de kist trigger blijft objecten in kist steken
        {       
            if (!_bulletsInCollider[0].GetComponent<Bullet>().InGebruik)//Zet object in chest als deze niet in gebruik is
            {
                Debug.Log(_bulletsInCollider.Count);
                Debug.Log("1KEER GEACTIVEERD MAAR FUNCTIE MIS SMEER?");
                _bulletsInChest.Add(_bulletsInCollider[0]);//Voeg toe aan chest lijst
                BulletPlacement(_bulletsInCollider[0]);//Voeg eigenschappen toe voor in de kist te zitten    
                _bulletsInCollider.RemoveAt(0);//Verwijder van collider lijst
            }
            else//Als object wel in gebruik is zet het in de _bulletsInUse lijst
            {
                _bulletsInUse.Add(_bulletsInCollider[0]);//Zet in use lijst    
                _bulletsInCollider.RemoveAt(0);//Verwijder van collider lijst
                   
            }
        }
    }

    void BulletPlacement(GameObject currentBullet)
    {
            if (_bulletsInChest.Count > _previousStack)//enkel position aanpassen als chest count verhoogd maar niet als die verlaagd
            {
                    currentBullet.GetComponent<Rigidbody>().isKinematic = true;
                    currentBullet.transform.position = positions[_bulletsInChest.Count].transform.position;
                    _previousStack = _bulletsInChest.Count;
            }
    }

    private void OnTriggerEnter(Collider col)//on trigger enter werkt maar 1keer dus als hij erin gaat moet die direkt gedetecteert worden
    {      
        if (col.transform.name == "Bullet")
        {
            _bulletsInCollider.Add(col.gameObject);
        }
    }

    private void OnTriggerStay(Collider col)//Als bullet in de trigger blijft en niet meer in gebruik is
    {
        if (col.transform.name == "Bullet")
        {        
            //Als object in collider zit maar niet in gebruik is remove van ingebruik lijst en zet terug in collider lijst
            if (!col.GetComponent<Bullet>().InGebruik && _bulletsInUse.Contains(col.gameObject))
            {
                Debug.Log("tedyffff");
                //Verwijder van list met gebruike items en zet terug in list met normale colliders           
                _bulletsInCollider.Add(col.gameObject);
                _bulletsInUse.Remove(col.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        //Verwijder de bullet uit elke lijst als bullet uit de collider gaat
        if (col.transform.name == "Bullet")
        {
            if (_bulletsInCollider.Contains(col.gameObject))
            {
                _bulletsInCollider.Remove(col.gameObject);
            }

            if (_bulletsInChest.Contains(col.gameObject))
            {
                _bulletsInChest.Remove(col.gameObject);
                _previousStack = _bulletsInChest.Count;
            }

            if (_bulletsInUse.Contains(col.gameObject))
            {
                _bulletsInChest.Remove(col.gameObject);
            }
        }
    }
}

