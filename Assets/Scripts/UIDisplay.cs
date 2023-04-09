using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] Slider healthSlider;
    [SerializeField] PlayerHealth playerHealth;

    void Start()
    {
        healthSlider.maxValue = playerHealth.GetMaxHealth();
        
        Debug.Log(healthSlider.maxValue);
    }

    void Update()
    {
        healthSlider.value = playerHealth.GetHealth();
    }
}
