﻿using System;

namespace Uppgift3_TheGame
{
    public class Monster : Character
    {
        public int XpValue { get; set; }
        public override string Alias { get => Name;  }

        public Monster(int powerUp)
        {
            Name = "Pelle";
            Offense += powerUp /2;
            Defense += powerUp /2;
            Damage += powerUp * 2;
            Toughness += powerUp * 2;
            MaxHealth = powerUp * 30;
            CurrentHealth = MaxHealth;
            Gold = rng.Next(1, 11) * powerUp;
            XpValue = powerUp;
        }

        public (int gold,int xp) Corpse()
        {
            (int gold, int xp) loot = (Gold, XpValue);
            return loot;
        }
    }
}