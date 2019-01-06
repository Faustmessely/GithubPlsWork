using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateShipSpawningBehaviour : MonoBehaviour
{
    public short PirateShipDirections = 2;
    public GameObject[] directions;
    bool _allowLeftShip;
    bool _allowRightShip;
    int _BoatsInactiveCondition = 4;
    public int boatsActive = 0;
    public int maxBoatsAllowed = 2;

    CheckManager _checkSpawn;

    // Update is called once per frame
    void Update()
    {
        //ronde 1 spawn alle tentacles - als tentacle 10sec ni geraakt wordt val aan 50hp kanon doet 50 los object 25
        //Laad bools in die aantonen of de tentacles of head kunnen komen
        _checkSpawn = GameObject.FindGameObjectWithTag("CheckManager").GetComponent<CheckManager>();
        _allowLeftShip = _checkSpawn.AllowLeftTentacle;
        _allowRightShip = _checkSpawn.AllowRightTentacle;

        if (_allowLeftShip == false)
        {
            if (directions[1].GetComponent<PirateShipScript>().Active)
            {
                directions[1].GetComponent<PirateShipScript>().despawnBoat = true;
            }
        }

        if (_allowRightShip == false)
        {
            if (directions[0].GetComponent<PirateShipScript>().Active)
            {
                directions[0].GetComponent<PirateShipScript>().despawnBoat = true;
            }            
        }
    }
}
