using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMoveSpeed : ShopItem
{
    public void Buy()
    {
        if (points.points >= price)
        {
            FirstPersonController speed = Player.GetComponent<FirstPersonController>();
            speed.walkSpeed++;
            speed.sprintSpeed++;
            speed.crouchSpeed++;
            points.UpdatePoints(price * -1);
            PriceIncrease();
        }
    }
}
