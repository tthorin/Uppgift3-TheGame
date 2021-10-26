
namespace Uppgift3_TheGame
{   
    using POCO;
    using static POCO.Equipment;
    
    public class Player : Character
    {
        public int Level { get; set; } = 0;
        public int Xp { get; set; } = 0;
        public int XpToNextLevel { get; private set; } = 1;
        public Weapon EquippedWeapon { get; private set; } = Fists;
        internal Armor EquippedArmor { get; private set; } = BirthdaySuit;



        public Player()
        {
            Alias = "You";
            LevelUp(++Level);
            CurrentHealth -= 50;
            Gold += 1000;
            EquipWeapon(Stick);
        }

        private void LevelUp(int newLevel)
        {
            XpToNextLevel *= 2;
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