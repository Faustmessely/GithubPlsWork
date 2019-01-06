using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateShipScript : MonoBehaviour {

    public bool Active = false;
    float timer = 0;
    float _attackTimer = 0;
    bool _spawnProcess = false;
    int _tentacleHp = 50;
    public bool despawnBoat = false;
    PirateShipSpawningBehaviour _boss;
    Healthpoints _healthPoints;
    Animation anim;
    ShipHP ship;
    int _aanvalRnd;
    // Use this for initialization
    void Start()
    {
        anim = this.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {

        _boss = transform.parent.gameObject.GetComponent<PirateShipSpawningBehaviour>();
        _healthPoints = transform.parent.gameObject.GetComponent<Healthpoints>();
        if (Active == false && _spawnProcess == false && _boss.boatsActive < _boss.maxBoatsAllowed)
        {
            timer = 0;
            timer = Random.Range(1, 8);
            _aanvalRnd = Random.Range(3, 6);
            _spawnProcess = true;
        }
        else if (Active == false && _spawnProcess == true && _boss.boatsActive < _boss.maxBoatsAllowed)
        {
            if (timer >= 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                Active = true;
                anim.Play("TentacleEmerge");
                _spawnProcess = false;
                _boss.boatsActive += 1;

            }
        }


        if (Active && _tentacleHp > 0)
        {
            _attackTimer += Time.deltaTime;
            if (_attackTimer >= _aanvalRnd)//DOE EEN AANVAL ALS DE TENTACEL MERGED IS
            {
                //  Debug.Log("ikvalaan");
                _attackTimer = 0;
                anim.Play("ShootLeft");
                anim.Play("ShootRight");
                Active = false;
                _boss.boatsActive -= 1;
                ship = GameObject.FindGameObjectWithTag("Ship").GetComponent<ShipHP>();
                ship.ShipHitPoints -= 150;
            }
        }
        else if (Active && _tentacleHp <= 0)//ALS TENTACLE MERGED IS EN HP IS 0 DAN DESPAWN
        {
            Debug.Log("tentaledood");
            _boss.boatsActive -= 1;
            _tentacleHp = 50;
            despawnBoat = true;
        }


        if (despawnBoat)
        {
            //Debug.Log("despawn");
            _attackTimer = 0;
            Active = false;
            despawnBoat = false;
            anim.Play("TentacleSubmerge");
        }

    }


    //ALS COLLISION ER IS MET BULLET DAN VERLIEST TENTACLE HP INDIEN HIJ LEVEND IS
    //tentacle verliest leven
    //boss hp verliest leven
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Bullet" && Active)
        {
            Debug.Log("dood");
            _tentacleHp -= 50;
            _healthPoints.maxHealth -= 50;
        }
    }
}
