using System;

namespace Uppgift3_TheGame
{
    public class Player:Character
    {
        public int Level { get; set; } = 1;
        public int Xp { get; set; } = 0;
        int XpToNextLevel = 3;
        public Weapon Weapon { get; set; } = Weapon.Fists;



        public Player()
        {
            LevelUp(Level);
        }

        private void LevelUp(int newLevel)
        {
            Offense++;
            Defense++;
            MaxHealth += 50;
            CurrentHealth = MaxHealth;
            Damage = 5 + (newLevel * 5) + (int)Weapon;
            Toughness = 5 + (newLevel * 5) + (int)Armor;
        }
    }
}