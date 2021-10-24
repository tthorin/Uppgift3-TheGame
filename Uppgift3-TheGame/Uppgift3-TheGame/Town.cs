namespace Uppgift3_TheGame
{
    using System;
    using System.Collections.Generic;
    using Enums;
    using POCO;
    using static Helpers.PrintHelper;

    public class Town
    {
        public string Name { get; set; } = "";
        private float priceMarkUp = 1.2f;


        public void VisitInn(Player player)
        {
            int price = 10;
            List<string> innList = new() { "Welcome to the inn!","Would you like to stay the night?", $"Current health : {player.CurrentHealth} / {player.MaxHealth}", $"You have {player.Gold} gp.", $"Rest until fully healed - {price} gp", "Leave" };
            Menu inn = new(innList, 2, 2);
            string choice = inn.UseMenu();
            if (choice.StartsWith("Rest"))
            {
                bool canAfford = CheckMoney(price, player);
                if (canAfford) player.CurrentHealth = player.MaxHealth;
            }
            Console.WriteLine();
        }
        public void VisitWeaponStore(Player player)
        {
            Menu weaponStore = TownMenus.GenerateWeaponShop(priceMarkUp, player.EquippedWeapon, player.Gold);
            bool leaving = false;
            Weapon buying = Equipment.Fists;
            bool canAfford = false;

            do
            {
                string choice = weaponStore.UseMenu();
                if (choice == "Leave.") leaving = true;
                else buying = Equipment.WeaponsList.Find(weapon => weapon.Name.StartsWith(choice.Substring(0, choice.IndexOf('-') - 1)));
                if (!leaving && buying.Damage <= player.EquippedWeapon.Damage) BorderPrint("You already have the same or better weapon!");
                else if (!leaving) canAfford = CheckMoney((int)(buying.Price*priceMarkUp), player);
            } while (!leaving && !canAfford);

            if (canAfford)
            {
                BorderPrint($"You bought a {buying.Name}.");
                player.EquipWeapon(buying);
            }
        }

        private bool CheckMoney(int price, Player player)
        {
            bool canAfford = false;
            if (price <= player.Gold)
            {
                canAfford = true;
                player.Gold -= price;
            }

            else PrintAndHold("You can't afford that! :(");
            return canAfford;
        }
    }
}
