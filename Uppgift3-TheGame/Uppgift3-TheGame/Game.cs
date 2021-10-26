
namespace Uppgift3_TheGame
{
    using System;
    using static Helpers.PrintHelpers;
    
    public class Game
    {
        public Player PC = new() { Name = "Nisse" };
        private static Random rng = new();
        Monster mob;
        Menu room;


        internal void Start()
        {   
            Town cave = new() { Name = "the Cave" };
            bool keepPlaying = cave.Enter(PC);
            
            while (keepPlaying)
            {
                mob = null;
                room = null;
                room = EverShiftingMaze.GetNewRoom(PC);
                if (rng.Next(0, 2) == 1) BorderPrint("Amazingly, you encounter.... Nothing!");

                else Encounter();
                if (mob != null && mob.Alive)
                {
                    room.UpdateMenuItem($"Current health: {PC.CurrentHealth} / {PC.MaxHealth}", 3);
                    room.UpdateMenuItem($"There's a {mob.Alias} in the room!", 4);
                    room.MenuItems.Insert(room.MenuItems.Count - 1, $"Attack {mob.Alias}.");
                }
                string choice = room.UseMenu();
                if (choice.StartsWith("Fight")) Fight();
                else if (choice == "Head back to town.")
                {
                    EverShiftingMaze.PortalStone();
                    keepPlaying = cave.Enter(PC);
                }
            } 


            //int letterCounter = (int)'A';
            //char monsterNameAddon;
            //while (PC.Level < 3)
            //{
            //    monsterNameAddon = (char)letterCounter;
            //    Monster mob = null;
            //    string monsterName = "Monster" + monsterNameAddon;
            //    mob = new(PC.Level) { Name = monsterName, Alias = monsterName };
            //    int round = 10;
            //    while (PC.Alive && mob.Alive)
            //    {
            //        CombatRoundPrint(round);
            //        mob.TakeDamage(PC.Attack());
            //        if (mob.Alive) PC.TakeDamage(mob.Attack());
            //        Console.WriteLine($"{PC.Name}: {PC.CurrentHealth} hp, {mob.Name}: {mob.CurrentHealth} hp.");
            //        round++;
            //        Hold();
            //    }
            //    PC.Loot(mob.Corpse());
            //    Console.WriteLine($"Level: {PC.Level} Xp: {PC.Xp} / {PC.XpToNextLevel}, Gold: {PC.Gold}");
            //    letterCounter++;
            //    round = 1;
            //}
        }

        private void Encounter()
        {
            mob = null;
            mob = new Monster(PC.Level);
            BorderPrint($"You encounter a {mob.Alias}!");
            Menu encounter = EverShiftingMaze.EncounterMenu(PC,mob);
            string choice = encounter.UseMenu();
            if (choice == "Fight!") Fight();
            else Flee();
        }

        private void Flee()
        {
            BorderPrint($"As you turn and flee like a chicken, {mob.Alias} hits you in the back!");
            PC.TakeDamage(mob.Attack());
            Hold();
        }

        private void Fight()
        {
            int round = 1;
            while (PC.Alive && mob.Alive)
            {
                CombatRoundPrint(round);
                mob.TakeDamage(PC.Attack());
                if (mob.Alive) PC.TakeDamage(mob.Attack());
                if (mob.Alive && PC.Alive)
                {
                    Console.WriteLine($"{PC.Name}: {PC.CurrentHealth} hp, {mob.Name}: {mob.CurrentHealth} hp.");
                    round++;
                    Hold();
                }
            }
            PC.Loot(mob.Corpse());
            room.UpdateMenuItem($"Level: {PC.Level} Xp: {PC.Xp} / {PC.XpToNextLevel}", 2);
            room.UpdateMenuItem($"Current health: {PC.CurrentHealth} / {PC.MaxHealth}", 3);
        }
    }
}