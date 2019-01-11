using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHP : MonoBehaviour {

    [SerializeField]
    CheckManager _checkManager;

    float _obstacleDamage = 50;
    float _wallDamage = 10;

    public float ShipHitPoints = 1000;

    public bool shipInfiniteHP = false;

    private void Update()
    {
        if (_checkManager.ShipObstacleCollision)
            DoHitObstacle();
        if (_checkManager.ShipWallCollision)
            DoHitWall();

        if(shipInfiniteHP)
        {
            ShipHitPoints = 10000;
        }
    }

    void DoHitWall()
    {
        ShipHitPoints -= _wallDamage;
    }

    void DoHitObstacle()
    {
        ShipHitPoints -= _obstacleDamage;
        Debug.Log("Hit");
    }
    
}
