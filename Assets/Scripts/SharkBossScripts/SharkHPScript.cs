using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkHPScript : MonoBehaviour {

    public static float Health = 1000;

    public void ReduceHP(float amount)
    {
        Health -= amount;
    }
}
