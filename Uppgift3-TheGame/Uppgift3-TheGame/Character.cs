// -----------------------------------------------------------------------------------------------
//  Character.cs by Thomas Thorin, Copyright (C) 2021.
//  Published under GNU General Public License v3 (GPL-3)
// -----------------------------------------------------------------------------------------------

namespace Uppgift3_TheGame
{
    using System;
    using POCO;
    using static Helpers.PrintHelpers;

    internal class Character
    {
        protected CombatMessages msg = new();
        internal string Name { get; set; } = "";
        internal virtual string Alias { get; set; } = "";
        internal int MaxHealth { get; set; } = 100;
        internal int CurrentHealth { get; set; } = 100;
        internal int Offense { get; set; } = 10;
        internal int Defense { get; set; } = 10;
        internal int Damage { get; set; } = 25;
        internal int Toughness { get; set; } = 10;
        internal int Gold { get; set; } = 0;
        internal bool Alive { get; set; } = true;


        protected static Random rng = new();

        internal virtual bool TakeDamage(int damage)
        {
            (string msg, int blocked) result = DoRoll(false, Defense, Toughness);

            damage = damage - result.blocked < 0 ? 0 : damage - result.blocked;
            CurrentHealth -= damage;

            DramaticPrint($"{Alias} {result.msg} {result.blocked} damage");
            Console.WriteLine($"{Alias} takes {damage} points of damage ({CurrentHealth}/{MaxHealth})");
            Console.WriteLine();

            if (CurrentHealth <= 0)
            {
                Alive = false;
                Die();
            }

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

        internal virtual int Attack()
        {
            (string flavourText, int damage) result = DoRoll(true, Offense, Damage);
            DramaticPrint($"{Alias} {result.flavourText} {result.damage} points of damage");
            return result.damage;
        }
        internal virtual void Die()
        {
            BorderPrint($"{Name} falls over, dead.");
        }


    }
}