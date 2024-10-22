using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ShopItemBuyManager : MonoBehaviour
{
    public string playerStatToChange;   // the player stat to change
    public int price;                   // price to purchase item
    public float priceIncreaseMulti = 1.0f;    // how much the price increases on purchase
    public GameObject player;
    public TMPro.TMP_Text priceText;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");

        UpdateText();
    }


    public void Buy()
    {
        Health playerHealth = player.GetComponent<Health>();

        playerHealth.GetType().GetField(playerStatToChange).SetValue(playerHealth, true);


        // buy code here
        PriceIncrease();
    }

    private void PriceIncrease()
    {
        price = Mathf.FloorToInt(price * priceIncreaseMulti);
    }

    private void UpdateText()
    {
        priceText.text = "Price: " + price;
    }
}
