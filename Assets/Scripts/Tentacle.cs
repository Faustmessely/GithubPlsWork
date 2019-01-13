using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Tentacle : MonoBehaviour
{
    public bool Merged = false;
    float timer = 0;
    float _attackTimer = 0;
    bool _spawnProcess = false;
   public int _tentacleHp = 50;
    public bool despawnTentacle = false;
    BossSpawningBehavior _boss;
    Healthpoints _healthPoints;
    Animation anim;
    ShipHP ship;
    int _aanvalRnd;

    // Use this for initialization
    void Start ()
    {
        anim = this.GetComponent<Animation>();
    }
	
	// Update is called once per frame
	void Update ()
    {

        _boss = transform.parent.gameObject.GetComponent<BossSpawningBehavior>();
        _healthPoints = transform.parent.gameObject.GetComponent<Healthpoints>();
        if (Merged == false && _spawnProcess == false && _boss.tentaclesMerged < _boss.maxTentaclesMergedAllowed)
        {
            timer = 0;
            timer = Random.Range(1, 8);
            _aanvalRnd = Random.Range(6, 12);
            _spawnProcess = true;
        }
        else if(Merged == false && _spawnProcess == true && _boss.tentaclesMerged < _boss.maxTentaclesMergedAllowed)
        {
            if (timer >= 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                Merged = true;
                anim.Play("TentacleEmerge");
                _spawnProcess = false;
                _boss.tentaclesMerged += 1;
               
            }
        }

    
        if (Merged && _tentacleHp > 0)
        {
            _attackTimer += Time.deltaTime;
            if (_attackTimer >= _aanvalRnd)//DOE EEN AANVAL ALS DE TENTACEL MERGED IS
            {
              //  Debug.Log("ikvalaan");
                _attackTimer = 0;
                anim.Play("TentacleAttack");
                Merged = false;
                _boss.tentaclesMerged -= 1;
                ship = GameObject.FindGameObjectWithTag("Ship").GetComponent<ShipHP>();
                ship.ShipHitPoints -= 50;
            }
        }
        else if(Merged && _tentacleHp <= 0)//ALS TENTACLE MERGED IS EN HP IS 0 DAN DESPAWN
        {
            Debug.Log("tentaledood");
            _boss.tentaclesMerged -= 1;
            _tentacleHp = 50;
            despawnTentacle = true;
        }
        

       if(despawnTentacle)
        {
            //Debug.Log("despawn");
            _attackTimer = 0;
            Merged = false;
            despawnTentacle = false;
            anim.Play("TentacleSubmerge");
        }
                                                   
    }


    //ALS COLLISION ER IS MET BULLET DAN VERLIEST TENTACLE HP INDIEN HIJ LEVEND IS
    //tentacle verliest leven
    //boss hp verliest leven
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Bullet" && Merged)
        {
            Debug.Log("dood");
            _tentacleHp -= other.GetComponent<Bullet>()._bulletDMG;
            if (_tentacleHp < 0)
            {
                _healthPoints.maxHealth -= 25;
            }
            else
            {
                _healthPoints.maxHealth -= other.GetComponent<Bullet>()._bulletDMG;
            }
        }
    }
}
