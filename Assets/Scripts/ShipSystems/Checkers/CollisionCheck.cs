using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour {

    public  bool HitWall;
    public bool HitObstacle;

    List<GameObject> _destructibles;


    private void Start()
    {
        _destructibles = new List<GameObject>();
    }

    private void Update()
    {
        Debug.Log("Is " + HitObstacle);
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
            HitWall = true;

        if (other.gameObject.CompareTag("Obstacle"))
        {
     
            HitObstacle = true;
            _destructibles.Add(other.gameObject);
            Debug.Log("Check" + HitObstacle);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
            HitWall = false;
    }


public void ObstacleReset()
    {
        HitObstacle = false;
        for (int i = _destructibles.Count -1; i > -1; i--)
        {
            GameObject toDestroy = _destructibles[i];
            _destructibles.Remove(_destructibles[i]);
            Destroy(toDestroy);
           
        }
    }
    
    

}
