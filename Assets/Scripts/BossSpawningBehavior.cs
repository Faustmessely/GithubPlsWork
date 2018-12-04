using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawningBehavior : MonoBehaviour
{
    public short TentaclesOnSquid = 4;
    public GameObject[] tentacles;
    public GameObject squidHead;
    bool _allowLeftTentacle;
    bool _allowRightTentacle;
    bool _allowSquidHead;
    int _tentacleSubmergedCondition = 4;
    public int tentaclesMerged = 0;
    public int maxTentaclesMergedAllowed = 2;
    public int SquidMaxHeatlh = 1000;
    CheckManager _checkSpawn;
    // Use this for initialization
    void Start ()
    {       
        
    }

    // Update is called once per frame
    void Update()
    {
        //ronde 1 spawn alle tentacles - als tentacle 10sec ni geraakt wordt val aan 50hp kanon doet 50 los object 25
        //Laad bools in die aantonen of de tentacles of head kunnen komen
        _checkSpawn = GameObject.FindGameObjectWithTag("CheckManager").GetComponent<CheckManager>();
        _allowLeftTentacle = _checkSpawn.AllowLeftTentacle;
        _allowRightTentacle = _checkSpawn.AllowRightTentacle;
        _allowSquidHead = _checkSpawn.AllowHead;

        if (_allowLeftTentacle == false)
        {
            if (tentacles[2].GetComponent<Tentacle>().Merged)
            {
                tentacles[2].GetComponent<Tentacle>().despawnTentacle = true;
            }

            if (tentacles[3].GetComponent<Tentacle>().Merged)
            {
                tentacles[3].GetComponent<Tentacle>().despawnTentacle = true;
            }
        }

        if (_allowRightTentacle == false)
        {
            if (tentacles[0].GetComponent<Tentacle>().Merged)
            {
                tentacles[0].GetComponent<Tentacle>().despawnTentacle = true;
            }

            if (tentacles[1].GetComponent<Tentacle>().Merged)
            {
                tentacles[1].GetComponent<Tentacle>().despawnTentacle = true;
            }
        }

        if (_allowSquidHead)
        {
            //submerge head
            //alive false
        }
       
        //ifhead ga naar beneden
        //foreach (GameObject tentacle in tentacles)
        //{
        //  if(squidheadalive = false)
        //    {
        //        //submerge everything
        //        //alive false
        //    }
        //}
        //checkSpawn
        //game begint kies random plaatsen uit links & rechts
        //if(alserplaatsis && kies aantal
        //als er links plaats is spawn

        //Bosmanager
        //random.range 0-4
        //enkel collision mogelijk als alive = true

        //hoofd spawnt op basis van voorwaarden voldaan


        //als tentacles ni worden
    }
}
//elke tussen 20-60sec als tentacle dood is beslis of hij wel of niet terug levend wordt
//kies random Aantal levende maar welke mogen levend 


    //idee elke keer als boss kop onder gaat kies nieuw aantal max levenden mogelijk levenden

    //alleen collision als die alive is