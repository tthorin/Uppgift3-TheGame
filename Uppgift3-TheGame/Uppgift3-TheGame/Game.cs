
namespace Uppgift3_TheGame
{
    using System;
    using static Helpers.PrintHelpers;

    public class Game
    {
        private Player PC = new() { Name = "Nisse" };
        private static Random rng = new();
        Monster mob;
        Menu room;
        string lastMove = "North";



        public void Start()
        {
            PC.Name = EverShiftingMaze.Intro();
            Town cave = new() { Name = "the Cave" };
            bool keepPlaying = cave.Enter(PC);

            while (keepPlaying && !PC.GameOver)
            {
                room = null;
                Encounter();
                room = EverShiftingMaze.GetNewRoom(PC, lastMove);

                if (!PC.GameOver)
                {
                    keepPlaying = ShowRoom(cave);
                }
                else GameOver();
            }
        }


        private bool ShowRoom(Town town)
        {
            string choice = "";
            bool keepPlaying = true;
            do
            {
                room.UpdateMenuItem($"Current health: {PC.CurrentHealth} / {PC.MaxHealth}", 3);

                if (mob != null) AddMobToRoomMenu();
                else RemoveMobFromRoomMenu();

                choice = room.UseMenu();

                if (choice.StartsWith("Show")) PC.ShowStats();
                else if (choice.StartsWith("Attack")) Fight();

            } while (!PC.GameOver && choice != "Head back to town." && !choice.StartsWith("Go"));

            if (choice == "Head back to town.") keepPlaying = EverShiftingMaze.PortalStone(PC, town);

            else lastMove = choice.Substring(choice.IndexOf(' ') + 1, choice.IndexOf('.') - (choice.IndexOf(' ') + 1));

            return keepPlaying;
        }

        private void RemoveMobFromRoomMenu()
        {
            room.UpdateMenuItem($"", 4);
            if (room.MenuItems.Find(item => item.StartsWith("Attack")) != null) room.MenuItems.RemoveAt(room.MenuItems.Count - 3);
        }

        private void AddMobToRoomMenu()
        {
            room.UpdateMenuItem($"There's a {mob.FullName} in the room!", 4);
            room.MenuItems.Insert(room.MenuItems.Count - 2, $"Attack {mob.Alias}.");
        }

        private void Encounter()
        {
            if (rng.Next(0, 7) > 4) BorderPrint("Amazingly, you encounter.... Nothing!");
            else
            {
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
            if (PC.Alive) PC.Loot(mob.Corpse());
            mob = null;
        }

        private static void GameOver()
        {
            string[] go = { "GAME OVER!", "Thanks for playing, hope you enjoyed it.", "", "Why not have another go?" };
            BorderPrint(go);
        }
    }
}