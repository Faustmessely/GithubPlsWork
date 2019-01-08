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
    public Text fpsText;
    public float deltaTime;
    // Use this for initialization
    void Start() {
        _bulletsInChest = new List<GameObject>();
        _bulletsInCollider = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText.text = "FPS " + Mathf.Ceil(fps).ToString();
       BulletPlacement();

    }

    void AddChestItem()
    {
        //Check of kist al vol is
       if( _bulletsInChest.Count < _maxStack)
        
        //check of er colliders in de lijst zitten dat ni in de kist zitten

        //voeg random bullets toe
        if (!_currentBullet.gameObject.GetComponent<Bullet>().InGebruik && _bulletsInChest.Count < _maxStack)//Als object ni in gebruik is en er is plaats voeg toe aan chest
        {
            _currentBullet
        }
        //als er plaats is direkt toevoegen aan chest
    }

    void BulletPlacement()
    {
            // Debug.Log(_bulletsInChest.Count + " " + _previousStack);
            if (_bulletsInChest.Count > _previousStack)//enkel position aanpassen als chest count verhoogd maar niet als die verlaagd
            {
        
                _currentBullet.GetComponent<Rigidbody>().isKinematic = true;
                _currentBullet.transform.position = positions[_bulletsInChest.Count].transform.position;
                _previousStack = _bulletsInChest.Count;
             //   Debug.Log(_bulletsInChest.Count + " " + _previousStack);
            }

    }

    private void OnTriggerEnter(Collider col)//on trigger enter werkt maar 1keer dus als hij erin gaat moet die direkt gedetecteert worden
    {
       
        if (col.transform.name == "Bullet")
        {
            _bulletsInCollider.Add(col.gameObject);
            if (!col.gameObject.GetComponent<Bullet>().InGebruik && _bulletsInChest.Count < _maxStack)
            {
                _bulletsInChest.Add(_currentBullet.transform.gameObject);
            }          
        }



    }


    private void OnTriggerExit(Collider col)
    {
        //Voeg de distance vector toe van elk collision object waarbij de tag Interactable is en als er nog geen targetObject geselecteerd is  
        if (col.transform.name == "Bullet")
        {
            if(_bulletsInCollider.Contains(col.gameObject))
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

