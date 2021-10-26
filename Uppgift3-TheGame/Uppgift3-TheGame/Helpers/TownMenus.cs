namespace Uppgift3_TheGame.POCO
{
    using System.Collections.Generic;
    using static POCO.Equipment;

    internal class TownMenus
    {
        internal static Menu TownMainMenu(string name, Player visitor)
        {
            List<string> tmm = new()
            {
                $"Welcome to {name}",
                "The place is bustling and there are several places to visit.",
                "But adventure calls, maybe you should just head out again?",
                $"Health {visitor.CurrentHealth} / {visitor.MaxHealth}, Gold: {visitor.Purse()}",
                $"Weapon: {visitor.EquippedWeapon.Name}",
                $"Armor: {visitor.EquippedArmor.Name}",
                "Visit the inn.",
                "Visit the equipment store.",
                "Head out on new adventures."
            };
            return new Menu(tmm, 3, 3);
        }
        
        internal static Menu GenerateEquipmentShop(float markUp, Weapon equippedWeapon, Armor equippedArmor, int gold)
        {
            List<string> aShopList = new() { "Welcome to the armor store!", "What would you like to buy today?" };
            aShopList.Add($"Your current weapon is: {equippedWeapon.Name}");
            aShopList.Add($"Your current armor is: {equippedArmor.Name}");
            aShopList.Add($"You have {gold} gold coins in your purse.");
            foreach (var item in EquipmentList)
            {
                aShopList.Add($"{item.Name} - {(int)(item.Price * markUp)} gold.");
            }
            aShopList.Add("Leave.");

            return new Menu(aShopList, 2, 3);
        }



    }
}
