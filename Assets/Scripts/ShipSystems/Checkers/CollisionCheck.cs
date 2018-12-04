using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour {

    public  bool HitWall;
    public bool HitObstacle;

    private void LateUpdate()
    {
        HitObstacle = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
            HitWall = true;
        if (other.gameObject.CompareTag("Obstacle"))
        {
            HitObstacle = true;
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
            HitWall = false;
    }
    
}
