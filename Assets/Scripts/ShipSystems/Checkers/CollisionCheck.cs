using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour {

    public  bool HitWall;
    public bool HitObstacle;

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
            HitWall = true;
        if (other.gameObject.CompareTag("Obstacle"))
        {
            HitObstacle = true;
            Debug.Log("Check" + HitObstacle);
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
            HitWall = false;
    }
    
    
}
