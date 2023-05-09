using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    [Header("Coins")]
    int numberCoins = 0;

    void Start()
    {
        
    }

    
    void Update()
    {
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Coin"))
        {
            numberCoins++;
            Destroy(collision.gameObject);
        }
    }

    public int GetCoins()
    {
        return numberCoins;
    }
}
