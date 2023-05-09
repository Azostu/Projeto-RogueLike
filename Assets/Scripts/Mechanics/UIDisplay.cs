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
    [SerializeField] TextMeshProUGUI potions;
    string baseTextPotions;

    [Header("Coins")]
    [SerializeField] TextMeshProUGUI coins;
    [SerializeField] CoinCollector coinCollector;
    string baseTextCoins;

    void Start()
    {
        healthSlider.maxValue = playerHealth.GetMaxHealth();
        baseTextCoins = "Coins: ";
        baseTextPotions = "Potions: ";
        
    }

    void Update()
    {
        healthSlider.value = playerHealth.GetHealth();
        coins.text = baseTextCoins + coinCollector.GetCoins();
        potions.text = baseTextPotions + playerHealth.GetPotions();
    }
}
