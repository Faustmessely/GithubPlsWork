using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGeneratorBehaviour : MonoBehaviour {

    [SerializeField]
    GameObject[] _prefabTiles;

    GameObject[] _activeTiles;

    [SerializeField]
    GameObject _spawner;

    [SerializeField]
    GameObject _despawner;

    public static GameObject Despawner;
    public static GameObject Spawner;
    public static GameObject RandomGenerator;

    [SerializeField]
    private float _tileSize = 20;

    private Vector3 _spawnPosition;

    //public static bool Activate;
    private Transform _lastTrans;

    private void Start()
    {
        Despawner = _despawner;
        Spawner = _spawner;
        RandomGenerator = this.gameObject;

        _lastTrans = TileMotionBehaviour.LastInLine.transform;
    }
    

    public void Spawn()
    {
        int tileNumber;
        tileNumber = Random.Range(0, _prefabTiles.Length);



        GameObject obj = Instantiate(_prefabTiles[tileNumber],CalculateSpawnPosition(), Spawner.transform.rotation, Spawner.transform);
        //Activate = false;
        _lastTrans = obj.transform;

        //Debug.Log("test");

    }

    Vector3 CalculateSpawnPosition()
    {
        
            float newAngle = 90 - RotationSystem.Rotation;
            float newZ = _tileSize * Mathf.Sin(newAngle / 360 * 2 * Mathf.PI);
            float newX = _tileSize * Mathf.Cos(newAngle / 360 * 2 * Mathf.PI);
        
        return TileMotionBehaviour.LastInLine.transform.position + new Vector3(newX, 0, newZ);
       // return _lastTrans.position + Vector3.forward * _tileSize;
    }
    
}
