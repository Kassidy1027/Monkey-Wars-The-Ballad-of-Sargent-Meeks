using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAmmo : ShopItem
{ 
    public void Buy()
    {
        if (points.points >= price)
        {
            WeaponSwap playerWeapon = Player.GetComponent<WeaponSwap>();
            if (playerWeapon.currentWeapon.type == WeaponType.Raycast)
            {
                playerWeapon.currentWeapon.reserveAmmo += 100;
            }
            else if (playerWeapon.currentWeapon.type == WeaponType.Projectile)
            {
                playerWeapon.currentWeapon.reserveAmmo += 16;
            }
            points.UpdatePoints(price * -1);
            PriceIncrease();
        }
    }
}
