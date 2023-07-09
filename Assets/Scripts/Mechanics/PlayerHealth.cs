using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    float health;
    [SerializeField] float maxHealth = 10;

    [Header("Items")]
    Dictionary<String, int> items = new Dictionary<String, int>();

    [Header("Death")]
    [SerializeField] GameObject canvas;

    void Start()
    {
        health = maxHealth;
        
    }

    void Update()
    {
        if (health <= 0)
        {
           
            canvas.GetComponent<UIDisplay>().OnDeath();
            Destroy(gameObject);

        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Item"))
        {
            if (!items.ContainsKey(LayerMask.LayerToName(collision.gameObject.layer)))
            {
                items.Add(LayerMask.LayerToName(collision.gameObject.layer), 1);
            }
            else
            {
                items[LayerMask.LayerToName(collision.gameObject.layer)]++;
            }

            Destroy(collision.gameObject);
        }
    }

    

    public void TakeDamage(float healthLosed)
    {
        health -= healthLosed;
    }

    public void GainHealth(float healthGain){
        health += healthGain;
        if(health > maxHealth){
            health = maxHealth;
        }
    }

    void OnHeal(InputValue value)
    {
        if (items.ContainsKey("Potion"))
        {
            if (items["Potion"] > 0)
            {
                GainHealth(3);
                items["Potion"]--;
            }
        }
    }

    public float GetHealth(){
        return health;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    internal int GetPotions()
    {
        if (items.ContainsKey("Potion"))
        {
            return items["Potion"];
        }
        return 0;
    }

    public void IncreaseMaxHealth(float multiplier)
    {
        maxHealth *= multiplier;
        health = maxHealth;
    }

    public void BuyPotion()
    {
        if (items.ContainsKey("Potion"))
        {
            items["Potion"]++;
        }
        else
        {
           items.Add("Potion", 1);
        }
    }
}
