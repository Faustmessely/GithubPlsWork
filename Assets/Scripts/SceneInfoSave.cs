using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInfoSave : MonoBehaviour
{
    static SceneInfoSave sceneInfo;
    public static bool player1Active = false, player2Active = false, player3Active = false, player4Active = false;
    // Use this for initialization
    void Start () {
        sceneInfo = new SceneInfoSave();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
