using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public int price;
    public TMPro.TMP_Text priceText;
    public float priceIncreaseMult = 1.0f;
    public GameObject Player;

    void Start()
    {
        FindPlayer();
        UpdatePriceText();
    }

    public void FindPlayer()
    {
        Player = GameObject.FindWithTag("Player");
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
}
