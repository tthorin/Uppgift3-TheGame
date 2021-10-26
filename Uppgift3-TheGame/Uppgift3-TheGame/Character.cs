// -----------------------------------------------------------------------------------------------
//  Character.cs by Thomas Thorin, Copyright (C) 2021.
//  Published under GNU General Public License v3 (GPL-3)
// -----------------------------------------------------------------------------------------------

namespace Uppgift3_TheGame
{
    using System;
    using POCO;
    using static Helpers.PrintHelper;

    public class Character
    {
        protected CombatMessages msg = new();
        public string Name { get; set; } = "";
        public virtual string Alias { get; set; } = "";
        public int MaxHealth { get; set; } = 100;
        public int CurrentHealth { get; set; } = 0;
        public int Offense { get; set; } = 10;
        public int Defense { get; set; } = 10;
        public int Damage { get; set; } = 10;
        public int Toughness { get; set; } = 10;
        public int Gold { get; set; } = 0;
        public bool Alive { get; set; } = true;


        protected static 
            Random rng = new();


        public Character()
        {
            MaxHealth = 100;
            CurrentHealth = MaxHealth;
            Offense = 10;
            Defense = 10;
            Damage = 20;
            Toughness = 10;
        }
        public virtual bool TakeDamage(int damage)
        {
            (string msg, int blocked) result = DoRoll(false, Defense, Toughness);

            damage = damage - result.blocked < 0 ? 0 : damage - result.blocked;
            CurrentHealth -= damage;

            if (CurrentHealth <= 0) Alive = false;

            DramaticPrint($"{Alias} {result.msg} {result.blocked} damage");
            Console.WriteLine($"{Alias} takes {damage} points of damage ({CurrentHealth}/{MaxHealth})");
            Console.WriteLine();

            return Alive;
        }
        private (string, int) DoRoll(bool attacking, int skillToCheck, int associatedValue)
        {
            var flavourText = attacking ? msg.Hits : msg.Blocks;
            (string message, int value) result;

            var roll = rng.Next(1, 21);

            if (roll <= skillToCheck)
            {
                if (roll == 1) result.value = associatedValue * 2;
                else result.value = associatedValue;
                result.message = result.value == associatedValue ? flavourText[0] : flavourText[1];
            }
            else
            {
                if (roll == 20) result.value = 0;
                else result.value = associatedValue / 2;
                result.message = result.value == 0 ? flavourText[3] : flavourText[2];
            }
            return result;
        }

        public virtual int Attack()
        {
            (string flavourText, int damage) result = DoRoll(true, Offense, Damage);
            DramaticPrint($"{Alias} {result.flavourText} {result.damage} points of damage");
            return result.damage;
        }


    }
}