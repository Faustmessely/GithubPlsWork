using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckManager : MonoBehaviour {

	[SerializeField]
    public TentacleSpaceChecker LeftChecker;

    [SerializeField]
    public TentacleSpaceChecker RightChecker;

    [SerializeField]
    public ForwardRaycastCheck[] FrontChecks;

    [SerializeField]
    public CollisionCheck Ship;

    public bool AllowLeftTentacle;
    public bool AllowRightTentacle;
    public bool AllowHead;
    public bool Warn;
    public bool ShipWallCollision;
    public bool ShipObstacleCollision;

    private void Update()
    {
        AllowLeftTentacle = LeftChecker.Space;
        AllowRightTentacle = RightChecker.Space;
        ScrollForwardCheckers();
        ShipWallCollision = Ship.HitWall;
        ShipObstacleCollision = Ship.HitObstacle;

        //Debug.Log("Wall " + ShipWallCollision);
    }

    void ScrollForwardCheckers()
    {
        Warn = false;
        AllowHead = true;
        for(int i = 0; i < FrontChecks.Length; i++)
        {
            if(FrontChecks[i].ProhibitHead)
            {
                AllowHead = false;
            }
            if(FrontChecks[i].GiveWarning)
            {
                Warn = true;
            }
        }
    }


}
