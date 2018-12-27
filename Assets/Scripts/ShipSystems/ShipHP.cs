using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHP : MonoBehaviour {

    [SerializeField]
    CheckManager _checkManager;

    float _obstacleDamage = 50;
    float _wallDamage = 10;

    public float ShipHitPoints = 1000;

    private void Update()
    {
        if (_checkManager.ShipObstacleCollision)
            DoHitObstacle();
        if (_checkManager.ShipWallCollision)
            DoHitWall();
        if (ShipHitPoints < 0)
            Lose();
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

    void Lose()
    {
        Debug.Log("You Lose");
    }
}
