using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMotionBehaviour : MonoBehaviour
{

    public static float MovementSpeed = 5;

    [SerializeField]
    public bool _allowLast = true;
    public static GameObject LastInLine;

    private Vector3 _movement;

    public static void MovementSpeedReset()
    {
        MovementSpeed = 5;
    }


    private void Start()
    {
        if (_allowLast)
        {
            LastInLine = this.gameObject;
            _allowLast = false;
        }
    }

    void Update()
    {
        Move();
        //DetectDespawn();
    }

    private void Move()
    {
        _movement = new Vector3(0, 0, -MovementSpeed * Time.deltaTime);
        //transform.position = Vector3.MoveTowards(transform.position, RandomGeneratorBehaviour.Despawner.transform.position, MovementSpeed * Time.deltaTime);
        transform.position += _movement;
        Debug.DrawLine(transform.position, RandomGeneratorBehaviour.Despawner.transform.position, Color.green);
    }

    //check after all the movement updates
    private void LateUpdate()
    {
        DetectDespawn();
    }



    private void DetectDespawn()
    {
        if (Vector3.Distance(RandomGeneratorBehaviour.Despawner.transform.position, RandomGeneratorBehaviour.Spawner.transform.position)
            <= Vector3.Distance(transform.position, RandomGeneratorBehaviour.Spawner.transform.position))
        //if (transform.position.z <= RandomGeneratorBehaviour.Despawner.transform.position.z)
        {
            //  RandomGeneratorBehaviour.Activate = true;
            RandomGeneratorBehaviour.RandomGenerator.GetComponent<RandomGeneratorBehaviour>().Spawn();
            //Debug.Log(this.name);
            Destroy(this.gameObject);
        }
    }

}
