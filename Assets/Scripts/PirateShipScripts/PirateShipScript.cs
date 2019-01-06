using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateShipScript : MonoBehaviour {

    public bool Active = false;
    float timer = 0;
    float _attackTimer = 0;
    bool _spawnProcess = false;
    int _ShipHp = 150;
    public bool despawnBoat = false;
    PirateShipSpawningBehaviour _boss;
    Healthpoints _healthPoints;
    Animation anim;
    ShipHP ship;
    int _aanvalRnd;
    public GameObject pirateCannonBallLeft;
    public GameObject pirateCannonBallRight;
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
                anim.Play("Enter");
                _spawnProcess = false;
                _boss.boatsActive += 1;

            }
        }


        if (Active && _ShipHp > 0)
        {
            _attackTimer += Time.deltaTime;
            if (_attackTimer >= _aanvalRnd)//DOE EEN AANVAL ALS DE TENTACEL MERGED IS
            {
                //  Debug.Log("ikvalaan");
                _attackTimer = 0;
                anim.Play("Shoot");
                Active = false;
                _boss.boatsActive -= 1;
                ship = GameObject.FindGameObjectWithTag("Ship").GetComponent<ShipHP>();
                ship.ShipHitPoints -= 150;
                Instantiate(pirateCannonBallLeft, new Vector3(this.gameObject.transform.position.x - 75, this.gameObject.transform.position.y + 55, this.gameObject.transform.position.z + 18), Quaternion.EulerAngles(0, 0, 0));
                Instantiate(pirateCannonBallLeft, new Vector3(this.gameObject.transform.position.x - 75, this.gameObject.transform.position.y + 20, this.gameObject.transform.position.z - 2), Quaternion.EulerAngles(0, 0, 0));
                Instantiate(pirateCannonBallLeft, new Vector3(this.gameObject.transform.position.x - 75, this.gameObject.transform.position.y + 20, this.gameObject.transform.position.z + 45), Quaternion.EulerAngles(0, 0, 0));
                Instantiate(pirateCannonBallRight,new Vector3(this.gameObject.transform.position.x+75, this.gameObject.transform.position.y+55, this.gameObject.transform.position.z+18),Quaternion.EulerAngles(0,0,0));
                Instantiate(pirateCannonBallRight, new Vector3(this.gameObject.transform.position.x+75, this.gameObject.transform.position.y+20, this.gameObject.transform.position.z-2), Quaternion.EulerAngles(0, 0, 0));
                Instantiate(pirateCannonBallRight, new Vector3(this.gameObject.transform.position.x+75, this.gameObject.transform.position.y+20, this.gameObject.transform.position.z+45), Quaternion.EulerAngles(0, 0, 0));
            }
        }
        else if (Active && _ShipHp <= 0)//ALS TENTACLE MERGED IS EN HP IS 0 DAN DESPAWN
        {
            Debug.Log("tentaledood");
            _boss.boatsActive -= 1;
            _ShipHp = 150;
            despawnBoat = true;
        }


        if (despawnBoat)
        {
            //Debug.Log("despawn");
            _attackTimer = 0;
            Active = false;
            despawnBoat = false;
            anim.Play("Leave");
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
            _ShipHp -= 50;
            _healthPoints.maxHealth -= 50;
        }
    }
}
