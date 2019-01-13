using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersCheck : MonoBehaviour {

    public GameObject[] players;
    public GameObject spawnPoint;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (SceneInfoSave.player1Active)
        {
            Instantiate(players[0], spawnPoint.transform.position, Quaternion.identity);
            SceneInfoSave.player1Active = false;
        }

        if (SceneInfoSave.player2Active)
        {
            Instantiate(players[1], spawnPoint.transform.position,Quaternion.identity);
            SceneInfoSave.player2Active = false;
        }

        if (SceneInfoSave.player3Active)
        {
            Instantiate(players[2], spawnPoint.transform.position, Quaternion.identity);
            SceneInfoSave.player3Active = false;
        }

        if (SceneInfoSave.player4Active)
        {
            Instantiate(players[3], spawnPoint.transform.position, Quaternion.identity);
            SceneInfoSave.player4Active = false;
        }
        
        Destroy(this.gameObject);

    }
}
