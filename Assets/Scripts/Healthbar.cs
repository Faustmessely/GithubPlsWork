﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{

    private float _fillAmount;
    private float _maxHealth;
    private float _currentHealth;

    public Image HealthBar;
    public GameObject GameObject;

    // Use this for initialization
    void Start()
    {
        _maxHealth = GameObject.GetComponent<Healthpoints>().maxHealth;
    }
    //
    // Update is called once per frame
    void Update()
    {
        _currentHealth = GameObject.GetComponent<Healthpoints>().maxHealth;
        _fillAmount = translateValue(_currentHealth, 1, _maxHealth);
        HealthBar.fillAmount = _fillAmount;
    }

    private float translateValue(float Value, float InputMax, float TranslateMax)
    {
        return (Value * InputMax / TranslateMax);
    }
}
