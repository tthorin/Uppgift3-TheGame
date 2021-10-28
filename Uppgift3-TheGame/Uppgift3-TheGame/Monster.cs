
namespace Uppgift3_TheGame
{
    using System;
    using Helpers;
    internal class Monster : Character
    {
        internal int XpValue { get; set; }
        internal string FullName { get => $"{Prefix} {Class} called {Name} {Suffix}"; }
        internal override string Alias{ get=> $"{Name} {Suffix}"; }
        internal string Prefix { get; set; } = "";
        internal string Suffix { get; set; } = "";
        internal string Class { get; set; } = "";
        
        internal Monster(int powerUp)
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
        
        internal (int gold, int xp) Corpse()
        {
            (int gold, int xp) loot = (Gold, XpValue);
            return loot;
        }
    }
}