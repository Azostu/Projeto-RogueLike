using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    
    GameObject player;
    
    GameObject panel;
    
    TextMeshProUGUI priceArmorTxt;
    Button buyArmor;
    int priceArmor = 5;
    TextMeshProUGUI priceGunTxt;
    Button buyGun;
    int priceGun = 5;
    TextMeshProUGUI priceBulletTxt;
    Button buyBullet;
    int priceBullet = 5;
    TextMeshProUGUI pricePotionTxt;
    Button buyPotion;
    int pricePotion = 3;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        panel = GameObject.FindGameObjectWithTag("Panel");
        priceArmorTxt = GameObject.FindGameObjectWithTag("Armor").GetComponent<TextMeshProUGUI>();
        priceBulletTxt = GameObject.FindGameObjectWithTag("Bullet").GetComponent<TextMeshProUGUI>();
        priceGunTxt = GameObject.FindGameObjectWithTag("Gun").GetComponent<TextMeshProUGUI>();
        pricePotionTxt = GameObject.FindGameObjectWithTag("Potion").GetComponent<TextMeshProUGUI>();
        buyArmor = GameObject.FindGameObjectWithTag("BArmor").GetComponent<Button>();
        buyGun = GameObject.FindGameObjectWithTag("BGun").GetComponent<Button>();
        buyBullet = GameObject.FindGameObjectWithTag("BBullet").GetComponent<Button>();
        buyPotion = GameObject.Find("Button (5)").GetComponent<Button>();
        
        buyArmor.onClick.AddListener(BuyArmor);
        buyGun.onClick.AddListener(BuyGun);
        buyBullet.onClick.AddListener(BuyBullet);
        buyPotion.onClick.AddListener(BuyPotion);

        priceArmorTxt.SetText(priceArmor.ToString());
        priceGunTxt.SetText(priceGun.ToString());
        priceBulletTxt.SetText(priceBullet.ToString());
        pricePotionTxt.SetText(pricePotion.ToString());
        panel.SetActive(false);
    }

    
    void Update()
    {
        if(Vector3.Distance(player.transform.position, gameObject.transform.position) <= 6)
        {
            panel.SetActive(true);
        }
        else
        {
            panel.SetActive(false);
        }
    }

    public void BuyArmor()
    {
        if(player.GetComponent<CoinCollector>().GetCoins() >= priceArmor)
        {
            player.GetComponent<CoinCollector>().UseCoins(priceArmor);
            player.GetComponent<PlayerHealth>().IncreaseMaxHealth(1.2f);
            priceArmor *= 2;
            priceArmorTxt.SetText(priceArmor.ToString());
        }
    }

    public void BuyGun()
    {
        if (player.GetComponent<CoinCollector>().GetCoins() >= priceGun)
        {
            player.GetComponent<CoinCollector>().UseCoins(priceGun);
            player.GetComponent<PlayerFire>().MultiplyValues(1.1f);
            priceGun *= 2;
            priceGunTxt.SetText(priceGun.ToString());
        }
    }

    public void BuyBullet()
    {
        if (player.GetComponent<CoinCollector>().GetCoins() >= priceBullet)
        {
            player.GetComponent<CoinCollector>().UseCoins(priceBullet);
            player.GetComponent<PlayerFire>().MultiplyDamage(1.2f);
            priceBullet *= 2;
            priceBulletTxt.SetText(priceBullet.ToString());
        }
    }

    public void BuyPotion()
    {
        if (player.GetComponent<CoinCollector>().GetCoins() >= pricePotion)
        {
            player.GetComponent<CoinCollector>().UseCoins(pricePotion);
            player.GetComponent<PlayerHealth>().BuyPotion();
        }
    }
}
