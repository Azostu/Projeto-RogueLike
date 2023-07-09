using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
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

    [Header("Level")]
    [SerializeField] TextMeshProUGUI levelTxt;
    string baseTextLevels;
    [SerializeField] Image timer;
    bool isTimer = false;
    float maxTime = 30;
    float time;

    

    void Start()
    {
        healthSlider.maxValue = playerHealth.GetMaxHealth();
        baseTextCoins = "";
        baseTextPotions = "";
        baseTextLevels = "Level ";
        time = maxTime;
        timer.enabled = false;
        
    }

    void Update()
    {
        healthSlider.value = playerHealth.GetHealth();
        coins.text = baseTextCoins + coinCollector.GetCoins();
        potions.text = baseTextPotions + playerHealth.GetPotions();
        if (isTimer)
        {
            if(time == 0)
            {
                isTimer = false;
                timer.enabled = false;
            }
            time -= 1 * Time.deltaTime;
            timer.fillAmount = time / maxTime;
        }
    }

    public void updateLevelText(int level)
    {
        levelTxt.text = baseTextLevels + level;
    }

    public void StartTimer()
    {
        timer.enabled = true;
        time = maxTime;
        isTimer = true;
        
    }

    public void OnDeath()
    {
        StartCoroutine(GameOver());
    }
    private IEnumerator GameOver()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(4);
    }


}
