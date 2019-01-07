using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SharkHealthBarScript : MonoBehaviour {

    float _maxHealth;
    float _currentHealth;

    float _fillAmount;

    [SerializeField]
    Image HealthBar;

    private void Start()
    {
        _maxHealth = SharkHPScript.Health;
    }

    private void Update()
    {
        _currentHealth = SharkHPScript.Health;

        Display();
    }

    void Display()
    {
        _fillAmount = translateValue(_currentHealth, 1, _maxHealth);
        HealthBar.fillAmount = _fillAmount;
    }

    private float translateValue(float Value, float InputMax, float TranslateMax)
    {
        return (Value * InputMax / TranslateMax);
    }

}
