
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
            PC.Name = EverShiftingMaze.Intro();
            Town cave = new() { Name = "the Cave" };
            bool keepPlaying = cave.Enter(PC);

            while (keepPlaying && PC.Level < 10)
            {
                mob = null;
                room = null;

                room = EverShiftingMaze.GetNewRoom(PC, lastMove);
                Encounter();




                string choice = "";
                do
                {
                    room.UpdateMenuItem($"Current health: {PC.CurrentHealth} / {PC.MaxHealth}", 3);

                    if (mob != null && mob.Alive)
                    {
                        room.UpdateMenuItem($"There's a {mob.FullName} in the room!", 4);
                        room.MenuItems.Insert(room.MenuItems.Count - 2, $"Attack {mob.Alias}.");
                    }
                    else
                    {
                        room.UpdateMenuItem($"", 4);
                        if (choice == room.MenuItems.Find(item => item.StartsWith("Attack"))) room.MenuItems.RemoveAt(room.MenuItems.Count - 3);
                    }

                    do
                    {
                        choice = room.UseMenu();
                        if (choice.StartsWith("Show")) PC.ShowStats();

                    } while (choice.StartsWith("Show"));

                    if (choice.StartsWith("Attack")) Fight();

                } while (choice != "Head back to town." && !choice.StartsWith("Go"));

                if (choice == "Head back to town.")
                {
                    EverShiftingMaze.PortalStone();
                    keepPlaying = cave.Enter(PC);
                }
                else lastMove = choice.Substring(choice.IndexOf(' ') + 1, choice.IndexOf('.') - (choice.IndexOf(' ') + 1));

            }
        }

        private void Encounter()
        {
            if (rng.Next(0, 7) > 4) BorderPrint("Amazingly, you encounter.... Nothing!");
            else
            {
                mob = null;
                mob = new Monster(PC.Level);
                BorderPrint($"You encounter a {mob.FullName}!");
                Menu encounter = EverShiftingMaze.EncounterMenu(PC, mob);
                string choice = encounter.UseMenu();
                if (choice == "Fight!") Fight();
                else Flee();
            }
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
            room.UpdateMenuItem($"Level: {PC.Level} Xp: {PC.Xp} / {PC.XpToNextLevel} Gold: {PC.Gold}", 2);
            room.UpdateMenuItem($"Current health: {PC.CurrentHealth} / {PC.MaxHealth}", 3);
        }

        internal static void GameOver()
        {
            throw new NotImplementedException();
        }
    }
}