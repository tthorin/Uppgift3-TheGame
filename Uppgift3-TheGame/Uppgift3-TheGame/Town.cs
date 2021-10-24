//--------------------------------------------------------------------------------------------------------------
//Town.cs by Thomas Thorin 2021-10-24 Licensed under MIT License.
//--------------------------------------------------------------------------------------------------------------

namespace Uppgift3_TheGame
{
    using POCO;
    using System;
    using System.Collections.Generic;
    using static Helpers.PrintHelper;

    public class Town
    {
        public Player Visitor { get; private set; }

        public string Name { get; set; } = "";

        private float priceMarkUp = 1.2f;

        public void Enter(Player player)
        {
            Visitor = player;
            bool leaving = false;
            do
            {
                Menu townmenu = TownMenus.TownMainMenu(this.Name, Visitor);
                string choice = townmenu.UseMenu();
                if (choice == "Visit Inn.") VisitInn();
                else if (choice == "Visit Weapon store.") VisitWeaponStore();
                else leaving = true;
            } while (!leaving);
        }

        public void VisitInn()
        {
            int price = (int)(5 + (5 * Visitor.Level * priceMarkUp));
            List<string> innList = new() { "Welcome to the inn!", "Would you like to stay the night?", $"Current health : {Visitor.CurrentHealth} / {Visitor.MaxHealth}", $"You have {Visitor.Gold} gp.", $"Rest until fully healed - {price} gp", "Leave" };
            Menu inn = new(innList, 2, 2);
            string choice = inn.UseMenu();
            if (choice.StartsWith("Rest"))
            {
                bool canAfford = CheckMoney(price);
                if (canAfford) Visitor.CurrentHealth = Visitor.MaxHealth;
            }
            Console.WriteLine();
        }

        public void VisitWeaponStore()
        {
            Menu weaponStore = TownMenus.GenerateWeaponShop(priceMarkUp, Visitor.EquippedWeapon, Visitor.Purse());
            bool leaving = false;
            Weapon buying = Equipment.Fists;
            bool canAfford = false;

            do
            {
                string choice = weaponStore.UseMenu();
                if (choice == "Leave.") leaving = true;
                else buying = Equipment.WeaponsList.Find(weapon => weapon.Name.StartsWith(choice.Substring(0, choice.IndexOf('-') - 1)));
                if (!leaving && buying.Damage <= Visitor.EquippedWeapon.Damage) BorderPrint("You already have the same or better weapon!");
                else if (!leaving) canAfford = CheckMoney((int)(buying.Price * priceMarkUp));
            } while (!leaving && !canAfford);

            if (canAfford)
            {
                BorderPrint($"You bought a {buying.Name}.");
                Visitor.EquipWeapon(buying);
            }
        }

        private bool CheckMoney(int price)
        {
            bool canAfford = false;
            if (price <= Visitor.Purse())
            {
                canAfford = true;
                Visitor.Pay(price);
            }

            else PrintAndHold("You can't afford that! :(");
            return canAfford;
        }
    }
}
