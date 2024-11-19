using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDamageUp : ShopItem
{
    public void Buy()
    {
        if (points.points >= price)
        {
            WeaponSwap playerWeapon = Player.GetComponent<WeaponSwap>();
            if (playerWeapon.currentWeapon.type == WeaponType.Raycast)
            {
                playerWeapon.currentWeapon.power += (playerWeapon.currentWeapon.power / 2);
            }
            else
            {
                playerWeapon.currentWeapon.bonusDamage += (playerWeapon.currentWeapon.bonusDamage / 2);
            }
            points.UpdatePoints(price * -1);
            PriceIncrease();
        }
    }
}
