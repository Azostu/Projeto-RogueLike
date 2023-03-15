using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHealth : MonoBehaviour
{

    float health;
    [SerializeField] float maxHealth = 10;
    Dictionary<String, int> items = new Dictionary<String, int>();

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            Application.Quit();
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Item"))
        {
            if (!items.ContainsKey(collision.gameObject.name))
            {
                items.Add(collision.gameObject.name, 1);
            }
            else
            {
                items[collision.gameObject.name]++;
            }

            Destroy(collision.gameObject);
        }
    }

    public void LoseHealth(float healthLosed)
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
                GainHealth(5);
                items["Potion"]--;
            }
        }
    }


}
