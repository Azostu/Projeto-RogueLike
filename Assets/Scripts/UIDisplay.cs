using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] Slider healthSlider;
    [SerializeField] PlayerHealth playerHealth;

    [Header("Coins")]
    [SerializeField] TextMeshProUGUI coins;
    [SerializeField] CoinCollector coinCollector;
    string baseText;

    void Start()
    {
        healthSlider.maxValue = playerHealth.GetMaxHealth();
        baseText = "Coins: ";
        
    }

    void Update()
    {
        healthSlider.value = playerHealth.GetHealth();
        coins.text = baseText + coinCollector.GetCoins();
    }
}
