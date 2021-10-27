
namespace Uppgift3_TheGame
{
    using System;
    using Helpers;
    public class Monster : Character
    {
        public int XpValue { get; set; }
        public string FullName { get => $"{Prefix} {Class} called {Name} {Suffix}"; }
        public override string Alias{ get=> $"{Name} {Suffix}"; }
        public string Prefix { get; set; } = "";
        public string Suffix { get; set; } = "";
        public string Class { get; set; } = "";
        
        public Monster(int powerUp)
        {
            MonsterHelper.RandomizeMonster(this);
            Offense += powerUp / 2;
            Defense += powerUp / 2;
            Damage += (powerUp - 1) * 8;
            Toughness += (powerUp - 1) * 4;
            MaxHealth = powerUp * 25;
            CurrentHealth = MaxHealth;
            Gold = (rng.Next(1, 6) + 8) * powerUp;
            XpValue = powerUp;
        }
        
        public (int gold, int xp) Corpse()
        {
            (int gold, int xp) loot = (Gold, XpValue);
            return loot;
        }
    }
}