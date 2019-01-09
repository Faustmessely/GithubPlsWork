using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkHPScript : MonoBehaviour {

    [SerializeField]
    float _setMaxHP = 1000;

    public static float Health = 1000;

    private void Start()
    {
        Health = _setMaxHP;
    }

    public void ReduceHP(float amount)
    {
        Health -= amount;
    }
}
