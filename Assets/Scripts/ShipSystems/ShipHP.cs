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
            HitObstacle();
        if (_checkManager.ShipWallCollision)
            HitWall();
        if (ShipHitPoints < 0)
            Lose();
    }

    void HitWall()
    {
        ShipHitPoints -= _wallDamage;
    }

    void HitObstacle()
    {
        ShipHitPoints -= _obstacleDamage;
    }

    void Lose()
    {
        Debug.Log("You Lose");
    }
}
