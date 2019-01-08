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
    GameObject _currentBullet;
    int _previousStack = 0;
    // Use this for initialization
    void Start() {
        _bulletsInChest = new List<GameObject>();
        _bulletsInCollider = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        AddChestItem();

    }

    void AddChestItem()
    {
        //while( _bulletsInChest.Count < _maxStack && _bulletsInCollider.Count > 0)//Als kist niet vol is en er zitten objecten in de kist trigger blijft objecten in kist steken
        //{
        //    if (!_bulletsInCollider[0].GetComponent<Bullet>().InGebruik)//verplaats object van lijst als het niet in gebruik is en er plaats is in de chest
        //    {
        //        for(int i = 0;i < _bulletsInCollider.Count)
        //        _bulletsInChest.Add(_bulletsInCollider[0]);//Voeg toe aan chest lijst
        //        BulletPlacement(_bulletsInCollider[0]);//Voeg eigenschappen toe voor de kist
        //        _bulletsInCollider.RemoveAt(0);//Verwijder van normale lijst
        //    }
        //    else
        //    {

        //    }
            
        //}
    }

    void BulletPlacement(GameObject currentBullet)
    {
            // Debug.Log(_bulletsInChest.Count + " " + _previousStack);
            if (_bulletsInChest.Count > _previousStack)//enkel position aanpassen als chest count verhoogd maar niet als die verlaagd
            {

            currentBullet.GetComponent<Rigidbody>().isKinematic = true;
            currentBullet.transform.position = positions[_bulletsInChest.Count].transform.position;
            _previousStack = _bulletsInChest.Count;
             //   Debug.Log(_bulletsInChest.Count + " " + _previousStack);
            }

    }

    private void OnTriggerEnter(Collider col)//on trigger enter werkt maar 1keer dus als hij erin gaat moet die direkt gedetecteert worden
    {
       
        if (col.transform.name == "Bullet")
        {
            _bulletsInCollider.Add(col.gameObject);             
        }



    }


    private void OnTriggerExit(Collider col)
    {
        //Voeg de distance vector toe van elk collision object waarbij de tag Interactable is en als er nog geen targetObject geselecteerd is
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
            //  Debug.Log("AAAAAA");
        }
    }
}

