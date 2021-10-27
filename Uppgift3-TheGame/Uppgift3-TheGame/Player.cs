
namespace Uppgift3_TheGame
{   
    using POCO;
    using static POCO.Equipment;
    using static Helpers.PrintHelpers;
    using System;

    public class Player : Character
    {
        public int Level { get; set; } = 0;
        public int Xp { get; set; } = 0;
        public double XpToNextLevel { get; private set; } = 1;
        public Weapon EquippedWeapon { get; private set; } = Fists;
        internal Armor EquippedArmor { get; private set; } = BirthdaySuit;

        //todo: adjust starting stats
        //todo: function to display player stats
        public Player()
        {
            Alias = "You";
            LevelUp(++Level);
            EquipWeapon(Fists);
            EquipArmor(BirthdaySuit);
        }

        private void LevelUp(int newLevel)
        {
            XpToNextLevel = Math.Ceiling(XpToNextLevel * 1.5);
            Xp = 0;
            Offense++;
            Defense++;
            MaxHealth += 50;
            CurrentHealth = MaxHealth;
            Damage = 10 + (newLevel * 5) + EquippedWeapon.Damage;
            Toughness = 10 + (newLevel * 5) + EquippedArmor.Protection;
        }
        public void Loot((int gold,int xp) loot)
        {
            Gold += loot.gold;
            Xp += loot.xp;
            System.Console.WriteLine($"\nYou gain {loot.xp} XP and {loot.gold} gold. Current health: {CurrentHealth} / {MaxHealth} HP.");
            ThinBorderPrint($"You are level {Level}, have {Xp} XP out of {XpToNextLevel} needed for next level and {Gold} gold.");
            
            if (Xp >= XpToNextLevel) LevelUp(Level++);
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
    }
}