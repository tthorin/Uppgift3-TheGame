using System;

namespace Uppgift3_TheGame
{
    public class Monster : Character
    {
        public int XpValue { get; set; }
        public override string Alias { get => Name;  }
        //todo: adjust constructor - pretty good now
        public Monster(int powerUp)
        {
            Name = "Pelle";
            Offense += powerUp /2;
            Defense += powerUp /2;
            Damage += (powerUp-1) * 7;
            Toughness += (powerUp-1) * 4;
            MaxHealth = powerUp * 30;
            CurrentHealth = MaxHealth;
            Gold = (rng.Next(1, 5)+7) * powerUp;
            XpValue = powerUp;
        }
        //todo: random monster
        public (int gold,int xp) Corpse()
        {
            (int gold, int xp) loot = (Gold, XpValue);
            return loot;
        }
    }
}