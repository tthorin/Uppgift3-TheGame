
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
        string lastMove = "North";
        


        internal void Start()
        {   
            Town cave = new() { Name = "the Cave" };
            bool keepPlaying = cave.Enter(PC);
            //todo: intro
            while (keepPlaying)
            {
                mob = null;
                room = null;
                
                room = EverShiftingMaze.GetNewRoom(PC,lastMove);
                if (rng.Next(0, 7) > 4) BorderPrint("Amazingly, you encounter.... Nothing!");//todo: adjust chance

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
                else lastMove = choice.Substring(choice.IndexOf(' ') + 1, choice.IndexOf('.') - (choice.IndexOf(' ')+1));
                
            } 
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
                    //Hold(); //todo: uncomment before hand-in
                }
            }
            PC.Loot(mob.Corpse());
            room.UpdateMenuItem($"Level: {PC.Level} Xp: {PC.Xp} / {PC.XpToNextLevel} Gold: {PC.Gold}", 2);
            room.UpdateMenuItem($"Current health: {PC.CurrentHealth} / {PC.MaxHealth}", 3);
        }
    }
}