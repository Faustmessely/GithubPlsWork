using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    public bool activated;
    float _timer = 0;
    public int spawnTime = 10;
    Animation animHook;
    public GameObject prefBullet;
    bool hookDown = false;
    GameObject objectHooked;
    int _previousHookChildCount;
    // Use this for initialization
    Transform _hook;
    void Start ()
    {
        animHook = this.GetComponent<Animation>();
        _hook = this.transform.Find("Bone001");
    }
	
	// Update is called once per frame
	void Update ()
    {

        if (_previousHookChildCount != _hook.childCount)//Als je het geviste object van de haak haalt
        {
            animHook.Play("HookNoWeight");

        }

        if (activated)//activeer bool op input
        {          

            if (transform.childCount <= 3)//Als er geen children zijn en dus niks is opgevist
            {
                if(!hookDown)
                {
                    animHook.Play("HookDown");
                    hookDown = true;
                }
                _timer += Time.deltaTime;
                
            }
        }

        if(_timer >= spawnTime)//als de timer langer dan spawntijd onder water is dan vist de haak iets
        {
            hookDown = false;
            objectHooked = Instantiate(prefBullet, _hook.position, Quaternion.identity);
            objectHooked.transform.SetParent(_hook);
            Bullet ObjectHookedScr = objectHooked.GetComponent<Bullet>();
            ObjectHookedScr.GetComponent<Rigidbody>().isKinematic = true;
            animHook.Play("HookUp");
            _timer = 0;//resettimer
            activated = false;
           
        }

        _previousHookChildCount = _hook.childCount;
    }
}
