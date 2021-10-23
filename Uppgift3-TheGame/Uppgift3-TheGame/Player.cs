
namespace Uppgift3_TheGame
{   
    using Enums;
    
    public class Player : Character
    {
        public int Level { get; set; } = 0;
        public int Xp { get; set; } = 0;
        public int XpToNextLevel { get; private set; } = 1;
        public Weapon Weapon { get; set; } = Weapon.Dagger;
        public Armor Armor { get; set; } = Armor.BirthdaySuit;



        public Player()
        {
            LevelUp(++Level);
        }

        private void LevelUp(int newLevel)
        {
            XpToNextLevel *= 2;
            Xp = 0;
            Offense++;
            Defense++;
            MaxHealth += 50;
            CurrentHealth = MaxHealth;
            Damage = 10 + (newLevel * 5) + (int)Weapon;
            Toughness = 10 + (newLevel * 5) + (int)Armor;
        }
        public void Loot(Monster mob)
        {
            Gold += mob.Gold;
            Xp += mob.XpValue;
            if (Xp >= XpToNextLevel) LevelUp(Level++);
        }
    }
}