using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PlayerMenu : MonoBehaviour
{
    [SerializeField]GameObject[] _players;
    [SerializeField]GameObject[] _noPlayers;
    [SerializeField] EventSystem _eventSystem;
    [SerializeField] GameObject _playerDisplayButton;
  
    void Start () {
  

    }
	

	void Update ()
    {
        if(!SceneInfoSave.player1Active)
        {
            SceneInfoSave.player1Active = true;
        }

        if (_playerDisplayButton.gameObject == _eventSystem.currentSelectedGameObject)
        {
            if (Input.GetButtonDown("Jump_P2"))
            {
                _players[0].SetActive(true);
                _noPlayers[0].SetActive(false);
                SceneInfoSave.player2Active = true;
            }
            else if (Input.GetButtonDown("Interact_P2"))
            {
                _players[0].SetActive(false);
                _noPlayers[0].SetActive(true);
                SceneInfoSave.player2Active = false;
            }

            if (Input.GetButtonDown("Jump_P3"))
            {
                _players[0].SetActive(true);
                _noPlayers[0].SetActive(false);
                SceneInfoSave.player3Active = true;
            }
            else if (Input.GetButtonDown("Interact_P3"))
            {
                _players[0].SetActive(false);
                _noPlayers[0].SetActive(true);
                SceneInfoSave.player3Active = false;
            }

            if (Input.GetButtonDown("Jump_P3"))
            {
                _players[0].SetActive(true);
                _noPlayers[0].SetActive(false);
                SceneInfoSave.player4Active = true;
            }
            else if (Input.GetButtonDown("Interact_P3"))
            {
                _players[0].SetActive(false);
                _noPlayers[0].SetActive(true);
                SceneInfoSave.player3Active = false;
            }
        }
	}
}
