using System;

namespace Uppgift3_TheGame
{
    internal class Game
    {
        Player player = new() { Name = "Nisse" };


        internal void Start()
        {
            int counter = (int)'A';
            char monsterNameAddon;
            while (player.Level < 3)
            {
                monsterNameAddon = (char)counter;
                Monster mob = null;
                string monsterName = "Monster" + monsterNameAddon;
                mob = new(player.Level) { Name = monsterName };
                int round = 1;
                while (player.Alive && mob.Alive)
                {
                    Console.WriteLine($"----Round {round}------------------------------------------------------------------");
                    mob.TakeDamage(player.Attack());
                    if (mob.Alive) player.TakeDamage(mob.Attack());
                    Console.WriteLine($"{player.Name}: {player.CurrentHealth} hp, {mob.Name}: {mob.CurrentHealth} hp.");
                    round++;
                }
                player.Loot(mob);
                Console.WriteLine($"Level: {player.Level} Xp: {player.Xp} / {player.XpToNextLevel}, Gold: {player.Gold}");
                counter++;
                round = 1;
            }
        }
    }
}