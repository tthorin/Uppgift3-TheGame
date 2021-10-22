using System;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace Uppgift3_TheGame
{
    public class Character
    {
        public string Name { get; set; } = "";
        public int MaxHealth { get; set; } = 100;
        public int CurrentHealth { get; set; } = 0;
        public int Offense { get; set; } = 10;
        public int Defense { get; set; } = 10;
        public int Damage { get; set; } = 10;
        public int Toughness { get; set; } = 10;
        public int Gold { get; set; } = 0;
        public bool Alive { get; set; } = true;

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
            int blocked = 0;

            if (rng.Next(1, 21) <= Defense) blocked = Toughness;
            else blocked = Toughness / 2;

            damage = damage - blocked < 0 ? 0 : damage - blocked;
            CurrentHealth -= damage;

            if (CurrentHealth <= 0) Alive = false;

            DramaticPrint($"{Name} {(blocked == Toughness ? "expertly blocks" : "stumbles but manages to block")} {blocked} damage");
            Console.WriteLine($"{Name} takes {damage} points of damage ({CurrentHealth}/{MaxHealth})");
            Console.WriteLine();

            return Alive;
        }

        public virtual int Attack()
        {
            int dmg = 0;
            if (rng.Next(1, 21) <= Offense) dmg = Damage / 2;
            else dmg = Damage;
            DramaticPrint($"{Name} {(dmg == Damage ? "hits true for" : "whiffs for")} {dmg} points of damage");
            return dmg;
        }
        protected virtual void DramaticPrint(string msg)
        {
            Console.CursorVisible = false;
            Console.Write(msg);
            for (int i = 0; i < 5; i++)
            {
                Console.Write(".");
                Thread.Sleep(300);
            }
            Console.WriteLine();
        }

    }
}