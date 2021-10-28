// -----------------------------------------------------------------------------------------------
//  Player.cs by Thomas Thorin, Copyright (C) 2021.
//  Published under GNU General Public License v3 (GPL-3)
// -----------------------------------------------------------------------------------------------

namespace Uppgift3_TheGame
{
    using System;
    using POCO;
    using static Helpers.PrintHelpers;
    using static POCO.Equipment;

    public class Player : Character
    {
        public int Level { get; private set; } = 0;
        public int Xp { get; private set; } = 0;
        public override string Alias { get => "You"; }
        public double XpToNextLevel { get; private set; } = 1;
        public Weapon EquippedWeapon { get; private set; } = Fists;
        internal Armor EquippedArmor { get; private set; } = BirthdaySuit;

        public Player()
        {
            LevelUp(++Level);
            CurrentHealth = MaxHealth;
            msg.Hits = EquippedWeapon.FlavourTexts;
            msg.Blocks = EquippedArmor.FlavourTexts;
        }

        private void LevelUp(int newLevel)
        {
            XpToNextLevel = Math.Ceiling(XpToNextLevel * 1.5);
            Xp = 0;
            Offense++;
            Defense++;
            MaxHealth += 50;
            Damage = 10 + (newLevel * 5) + EquippedWeapon.Damage;
            Toughness = 10 + (newLevel * 4) + EquippedArmor.Protection;
            if (Level > 1)
            {
                string[] lvlUp =
                {
                    "DING!",
                    "You leveled up!",
                    $"{"Xp to next level:", -18}{Xp, 5} {"New maxhealth:", -15}{MaxHealth, 4}",
                    $"{"Offense:", -18}{Xp,5} {"Defense:", -15}{Defense, 4}",
                    $"{"Damage:", -18}{Xp, 5} {"Toughness:", -15}{Toughness, 4}"
                };
                BorderPrint(lvlUp);
            }
        }
        public void Loot((int gold, int xp) loot)
        {
            Gold += loot.gold;
            Xp += loot.xp;
            if (Xp >= XpToNextLevel) LevelUp(++Level);
            else
            {
                System.Console.WriteLine($"\nYou gain {loot.xp} XP and {loot.gold} gold. Current health: {CurrentHealth} / {MaxHealth} HP.");
                ThinBorderPrint($"You are level {Level}, have {Xp} XP out of {XpToNextLevel} needed for next level and {Gold} gold.");
            }

        }
        public void EquipWeapon(Weapon weapon)
        {
            EquippedWeapon = weapon;
            Damage = 10 + Level * 5 + weapon.Damage;
            msg.Hits = weapon.FlavourTexts;
        }
        internal void EquipArmor(Armor armor)
        {
            EquippedArmor = armor;
            Toughness = 10 + Level * 5 + armor.Protection;
            msg.Blocks = armor.FlavourTexts;
        }
        public int Purse()
        {
            return Gold;
        }
        public void Pay(int price)
        {
            Gold -= price;
        }
        public void ShowStats()
        {
            string[] stats =
            {
                $"Name:".PadRight(8)+$"{Name,25}",
                $"Level:".PadRight(8)+$"{Level,25}",
                "XP:".PadRight(8)+$"{Xp} / {XpToNextLevel}".PadLeft(25),
                "Health:".PadRight(8)+$"{CurrentHealth} / {MaxHealth}".PadLeft(25),
                $"Gold:".PadRight(8)+$"{Gold,25}",
                $"Weapon:".PadRight(8)+$"{EquippedWeapon.Name,25}",
                $"Armor:".PadRight(8)+$"{EquippedArmor.Name,25}"
            };
            BorderPrint(stats);
        }
        public override void Die()
        {
            BorderPrint($"You fall to the ground, dead.");
            Game.GameOver();
        }
    }
}