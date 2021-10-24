namespace Uppgift3_TheGame.POCO
{
    using System.Collections.Generic;
    using static POCO.Equipment;

    public class TownMenus
    {
        public static Menu GenerateWeaponShop(float markUp, Weapon equippedWeapon, int gold)
        {
            List<string> wShopList = new() { "Welcome to the weapon store!", "What would you like to buy today?" };
            wShopList.Add($"Your current weapon is: {equippedWeapon.Name}");
            wShopList.Add($"You have {gold} gold coins in your purse.");
            foreach (Weapon weapon in WeaponsList)
            {
                wShopList.Add($"{weapon.Name} - {(int)(weapon.Price * markUp)} gold.");
            }
            wShopList.Add("Leave.");

            return new Menu(wShopList, 2, 2);
        }



    }
}
