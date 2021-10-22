using System;

namespace Uppgift3_TheGame
{
    internal class Game
    {
        Player player = new() { Name = "Nisse" };
        
        Character monster = new() { Name = "Super Evil Mega Monster mk. III" };
        internal void Start()
        {
            int round = 1;
            while(player.Alive && monster.Alive)
            {   
                Console.WriteLine($"----Round {round}------------------------------------------------------------------");
                monster.TakeDamage(player.Attack());
                if (monster.Alive)player.TakeDamage(monster.Attack());
                Console.WriteLine($"{player.Name}: {player.CurrentHealth} hp, {monster.Name}: {monster.CurrentHealth} hp.");
                round++;
            }
        }
    }
}