
namespace Uppgift3_TheGame
{
    using System;
    using static Helpers.PrintHelper;
    public class Game
    {
        public Player PC = new() { Name = "Nisse" };


        internal void Start()
        {
            int counter = (int)'A';
            char monsterNameAddon;

            Town cave = new() { Name = "Cave" };
            cave.Enter(PC);


            while (PC.Level < 3)
            {
                monsterNameAddon = (char)counter;
                Monster mob = null;
                string monsterName = "Monster" + monsterNameAddon;
                mob = new(PC.Level) { Name = monsterName, Alias = monsterName };
                int round = 1;
                while (PC.Alive && mob.Alive)
                {
                    Console.WriteLine($"----Round {round}------------------------------------------------------------------");
                    mob.TakeDamage(PC.Attack());
                    if (mob.Alive) PC.TakeDamage(mob.Attack());
                    Console.WriteLine($"{PC.Name}: {PC.CurrentHealth} hp, {mob.Name}: {mob.CurrentHealth} hp.");
                    round++;
                    Hold();
                }
                PC.Loot(mob.Corpse());
                Console.WriteLine($"Level: {PC.Level} Xp: {PC.Xp} / {PC.XpToNextLevel}, Gold: {PC.Gold}");
                counter++;
                round = 1;
            }
        }
    }
}