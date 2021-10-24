namespace Uppgift3_TheGame.POCO
{
    using System.Collections.Generic;
    using static POCO.Equipment;

    public class TownMenus
    {
        public static Menu TownMainMenu(string name, Player visitor)
        {
            List<string> tmm = new()
            {
                $"Welcome to {name}",
                "The place is bustling and there are several places to visit.",
                "But adventure calls, maybe you should just head out again?",
                $"Health {visitor.CurrentHealth} / {visitor.MaxHealth}, Gold: {visitor.Purse()}",
                $"Weapon: {visitor.EquippedWeapon.Name}",
                $"Armor: {visitor.EquippedWeapon}",
                "Visit the inn.",
                "Visit the weapon store.",
                "Head out on new adventures."
            };
            return new Menu(tmm, 3, 3);
        }

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
