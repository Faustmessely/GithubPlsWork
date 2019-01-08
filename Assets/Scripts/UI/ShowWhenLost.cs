using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowWhenLost : MonoBehaviour {

    public GameObject[] UIElements;
    public GameObject ship;
    private float _shipHealth;

    private void Start()
    {
        foreach (GameObject go in UIElements)
        {
            go.SetActive(false);
        }
    }
    void Update () {
        _shipHealth = ship.GetComponent<ShipHP>().ShipHitPoints;

        if (_shipHealth <= 0)
        {
            foreach (GameObject go in UIElements)
            {
                go.SetActive(true);
            }
        }
	}
}
