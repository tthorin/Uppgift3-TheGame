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
        private Player Visitor { get; set; }

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
                if (choice == "Visit the inn.") VisitInn();
                else if (choice == "Visit the equipment store.") VisitEquipmentStore();
                else leaving = true;
            } while (!leaving);
        }

        private void VisitInn()
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
        
        private void VisitEquipmentStore()
        {
            Menu equipStore = TownMenus.GenerateEquipmentShop(priceMarkUp, Visitor.EquippedWeapon, Visitor.EquippedArmor, Visitor.Purse());
            bool leaving = false;

            do
            {
                string choice = equipStore.UseMenu();
                if (choice == "Leave.") leaving = true;
                if (!leaving)
                {
                    string buyString = choice.Substring(0, choice.IndexOf('-') - 1);
                    var buying = Equipment.EquipmentList.Find(item => item.Name == buyString);
                    bool canAfford = CheckMoney((int)(buying.Price * priceMarkUp));
                    if (canAfford && buying is Armor buyingArmor)
                    {
                        BuyItem(buyingArmor);
                    }
                    if (canAfford && buying is Weapon buyingWeapon)
                    {
                        BuyItem(buyingWeapon);
                    }

                    equipStore.UpdateMenuItem($"Your current weapon is: {Visitor.EquippedWeapon.Name}", 2);
                    equipStore.UpdateMenuItem($"Your current armor is: {Visitor.EquippedArmor.Name}", 3);
                    equipStore.UpdateMenuItem($"You have {Visitor.Gold} gold coins in your purse.", 4);
                }

            } while (!leaving);
        }

        private void BuyItem(Weapon item)
        {
            if (item.Damage <= Visitor.EquippedWeapon.Damage) BorderPrint("You already have the same or better item!");
            else
            {
                Visitor.EquipWeapon(item);
                Visitor.Pay((int)(item.Price * priceMarkUp));
            }
        }

        private void BuyItem(Armor item)
        {
            if (item.Protection <= Visitor.EquippedArmor.Protection) BorderPrint("You already have the same or better item!");
            else
            {
                Visitor.EquipArmor(item);
                Visitor.Pay((int)(item.Price * priceMarkUp));
            }
        }

        private bool CheckMoney(int price)
        {
            bool canAfford = false;
            if (price <= Visitor.Purse()) canAfford = true;
            else PrintAndHold("You can't afford that! :(");
            return canAfford;
        }
    }
}
