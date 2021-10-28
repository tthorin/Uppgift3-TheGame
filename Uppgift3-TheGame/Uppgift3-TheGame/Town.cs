﻿// -----------------------------------------------------------------------------------------------
//  Town.cs by Thomas Thorin, Copyright (C) 2021.
//  Published under GNU General Public License v3 (GPL-3)
// -----------------------------------------------------------------------------------------------

namespace Uppgift3_TheGame
{
    using System;
    using System.Collections.Generic;
    using POCO;
    using static Helpers.PrintHelpers;

    internal class Town
    {
        private Player Visitor { get; set; }

        internal string Name { get; set; } = "";

        private readonly float priceMarkUp = 1f;

        internal bool Enter(Player player)
        {
            Visitor = player;
            bool leaving = false;
            do
            {
                Menu townmenu = TownHelper.TownMainMenu(Name, Visitor);
                string choice = townmenu.UseMenu();
                if (choice == "Visit the inn.") VisitInn();
                else if (choice == "Visit the equipment store.") VisitEquipmentStore();
                else if (choice == "Show player stats.") Visitor.ShowStats();
                else if (choice == "Exit game.")
                {
                    Menu sure = new Menu(new List<string> { "Exit game", "Are you sure?", "No.", "Yes." }, 1, 1);
                    string exit = sure.UseMenu();
                    if (exit == "Yes.") return false;
                }
                else leaving = true;
            } while (!leaving);
            LeaveTown();
            return true;
        }

        private void LeaveTown()
        {
            string[] description = TownHelper.LeaveTownDesc;
            BorderPrint(description);
            Console.Clear();
        }

        private void VisitInn()
        {
            int price = 3*Visitor.Level;
            Menu inn = TownHelper.InnMenu(Visitor, price);
            string choice = inn.UseMenu();
            if (choice.StartsWith("Rest"))
            {   
                if (CanAfford(price))
                {
                    Visitor.CurrentHealth = Visitor.MaxHealth;
                    Visitor.Pay(price);
                    BorderPrint("You feel well rested and fully healed after a night at the inn.");
                }
            }
        }

        private void VisitEquipmentStore()
        {
            Menu equipStore = TownHelper.GenerateEquipmentShop(priceMarkUp, Visitor.EquippedWeapon, Visitor.EquippedArmor, Visitor.Purse());
            bool leaving = false;

            do
            {
                string choice = equipStore.UseMenu();
                if (choice == "Leave.") leaving = true;
                if (!leaving)
                {
                    string buyString = choice.Substring(0, choice.IndexOf('-') - 1);
                    Item buying = Equipment.EquipmentList.Find(item => item.Name == buyString);
                    int price = (int)(buying.Price * priceMarkUp);
                    if (CanAfford(price))
                    {
                        BuyItem(buying);
                    }
                    equipStore.UpdateMenuItem($"Your current weapon is: {Visitor.EquippedWeapon.Name}", 2);
                    equipStore.UpdateMenuItem($"Your current armor is: {Visitor.EquippedArmor.Name}", 3);
                    equipStore.UpdateMenuItem($"You have {Visitor.Gold} gold coins in your purse.", 4);
                    
                }

            } while (!leaving);
        }

        private void BuyItem(Item item)
        {
            int itemToBuy = item.EffectiveValue;
            int playerWeapon = Visitor.EquippedWeapon.Damage;
            int playerArmor = Visitor.EquippedArmor.Protection;
            bool better = (item is Weapon) ? itemToBuy > playerWeapon : itemToBuy > playerArmor;

            if (!better) BorderPrint("You already have the same or better item!");
            else
            {
                BorderPrint($"You bought a {item.Name}.");

                if (item is Weapon weapon) Visitor.EquipWeapon(weapon);
                if (item is Armor armor) Visitor.EquipArmor(armor);

                Visitor.Pay((int)(item.Price * priceMarkUp));
            }
        }

        private bool CanAfford(int price)
        {
            bool canAfford = false;
            if (price <= Visitor.Purse()) canAfford = true;
            else BorderPrint("You can't afford that! :(");
            return canAfford;
        }
    }
}
