using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowWhenWon : MonoBehaviour {

    public GameObject[] UIElements;
    public GameObject boss;
    private float _bossHealth;

    private void Start()
    {
        foreach (GameObject go in UIElements)
        {
            go.SetActive(false);
        }
    }
    void Update()
    {
        _bossHealth = boss.GetComponent<Healthpoints>().maxHealth;

        if (_bossHealth <= 0)
        {
            foreach (GameObject go in UIElements)
            {
                go.SetActive(true);
            }
        }
    }
}
