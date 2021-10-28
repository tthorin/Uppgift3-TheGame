﻿// -----------------------------------------------------------------------------------------------
//  Player.cs by Thomas Thorin, Copyright (C) 2021.
//  Published under GNU General Public License v3 (GPL-3)
// -----------------------------------------------------------------------------------------------

namespace Uppgift3_TheGame
{
    using System;
    using POCO;
    using static Helpers.PrintHelpers;
    using static POCO.Equipment;

    internal class Player : Character
    {
        internal int Level { get; private set; } = 0;
        internal int Xp { get; private set; } = 0;
        internal override string Alias { get => "You"; }
        internal double XpToNextLevel { get; private set; } = 1;
        internal Weapon EquippedWeapon { get; private set; } = Fists;
        internal Armor EquippedArmor { get; private set; } = BirthdaySuit;
        internal bool GameOver { get; private set; } = false;

        internal Player()
        {
            LevelUp(++Level);
            CurrentHealth = MaxHealth;
            msg.Hits = EquippedWeapon.FlavourTexts;
            msg.Blocks = EquippedArmor.FlavourTexts;
            CurrentHealth = 5;
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
                //todo: add msg for lvl 10 game over
                if (Level == 10) GameOver = true;
                else BorderPrint(lvlUp);
            }
        }
        internal void Loot((int gold, int xp) loot)
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
        internal void EquipWeapon(Weapon weapon)
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
        internal int Purse()
        {
            return Gold;
        }
        internal void Pay(int price)
        {
            Gold -= price;
        }
        internal void ShowStats()
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
        internal override void Die()
        {
            Console.WriteLine(@"                            ,--.
                           {    }
                           K,   }
                          /  ~Y`
                     ,   /   /
                    {_'-K.__/
                      `/-.__L._
                      /  ' /`\_}
                     /  ' /
             ____   /  ' /
      ,-'~~~~    ~~/  ' /_
    ,'             ``~~~  ',
   (                        Y
  {                         I
 {      -                    `,
 |       ',                   )
 |        |   ,..__      __. Y
 |    .,_./  Y ' / ^Y   J   )|
 \           |' /   |   |   ||
  \          L_/    . _ (_,.'(
   \,   ,      ^^""' / |      )
     \_  \          /,L]     /
       '-_~-,       ` `   ./`
          `'{_            )
              ^^\..___,.--`(from https://ascii.co.uk/)"); 
            BorderPrint($"You fall to the ground, dead.");
            GameOver = true;
        }
    }
}