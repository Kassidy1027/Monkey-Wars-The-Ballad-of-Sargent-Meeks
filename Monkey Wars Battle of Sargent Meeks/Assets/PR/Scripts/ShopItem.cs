using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public int price;
    public TMPro.TMP_Text priceText;
    public float priceIncreaseMult = 1.0f;
    public GameObject Player;
    public FirstPersonController points;

    void Start()
    {
        FindPlayer();
        UpdatePriceText();
    }

    public void FindPlayer()
    {
        Player = GameObject.FindWithTag("Player");
        points = Player.GetComponent<FirstPersonController>();
    }
    public void UpdatePriceText()
    {
        priceText.text = "Price: " + price;
    }
    public void PriceIncrease()
    {
        price = Mathf.FloorToInt(price * priceIncreaseMult);
        UpdatePriceText();
    }

    public void UpdateStatistics()
    {
        StatisticManager.UpdateStat("Buys", 1);
    }
}
