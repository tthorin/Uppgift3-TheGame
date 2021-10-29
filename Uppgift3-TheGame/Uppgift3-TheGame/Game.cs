
namespace Uppgift3_TheGame
{
    using Newtonsoft.Json;
    using System;
    using static Helpers.PrintHelpers;
    using POCO;

    public class Game
    {
        private Player player = new() { Name = "Nisse" };
     
        private static Random rng = new();
        Monster mob;
        Menu roomMenu;
        MazeRoom room = new();
        string lastMove = "North";


        public void Start()
        {
            player.Name = EverShiftingMaze.Intro();
            Player boss = (Player)player.Clone(); //todo: fixa boss.
            Town cave = new() { Name = "the Cave" };
            bool keepPlaying = cave.Enter(player);

            while (keepPlaying && !player.GameOver)
            {
                room = Maze.ShowMaze();
                roomMenu = null;
                //Encounter();
                roomMenu = EverShiftingMaze.GetRoomMenu(player, lastMove);

                if (!player.GameOver)
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
                roomMenu.UpdateMenuItem($"Current health: {player.CurrentHealth} / {player.MaxHealth}", 3);

                if (mob != null) AddMobToRoomMenu();
                else RemoveMobFromRoomMenu();

                choice = roomMenu.UseMenu();

                if (choice.StartsWith("Show")) player.ShowStats();
                else if (choice.StartsWith("Attack")) Fight();

            } while (!player.GameOver && choice != "Head back to town." && !choice.StartsWith("Go"));

            if (choice == "Head back to town.") keepPlaying = EverShiftingMaze.PortalStone(player, town);

            else lastMove = choice.Substring(choice.IndexOf(' ') + 1, choice.IndexOf('.') - (choice.IndexOf(' ') + 1));

            return keepPlaying;
        }

        private void RemoveMobFromRoomMenu()
        {
            roomMenu.UpdateMenuItem($"", 4);
            if (roomMenu.MenuItems.Find(item => item.StartsWith("Attack")) != null) roomMenu.MenuItems.RemoveAt(roomMenu.MenuItems.Count - 3);
        }

        private void AddMobToRoomMenu()
        {
            roomMenu.UpdateMenuItem($"There's a {mob.FullName} in the room!", 4);
            roomMenu.MenuItems.Insert(roomMenu.MenuItems.Count - 2, $"Attack {mob.Alias}.");
        }

        //todo:make private and uncomment
        internal void Encounter()
        {
            //if (rng.Next(0, 7) > 4) BorderPrint("Amazingly, you encounter.... Nothing!");
            //else
            //{
                mob = new Monster(player.Level);
                BorderPrint($"You encounter a {mob.FullName}!");
                Menu encounter = EverShiftingMaze.EncounterMenu(player, mob);
                string choice = encounter.UseMenu();
                if (choice == "Fight!") Fight();
                else Flee();
            //}
        }

        private void Flee()
        {
            BorderPrint($"As you turn and flee like a chicken, {mob.Alias} hits you in the back!");
            player.TakeDamage(mob.Attack());
            Hold();
        }

        private void Fight()
        {
            int round = 1;
            while (player.Alive && mob.Alive)
            {
                CombatRoundPrint(round);
                mob.TakeDamage(player.Attack());
                if (mob.Alive) player.TakeDamage(mob.Attack());
                if (mob.Alive && player.Alive)
                {
                    Console.WriteLine($"{player.Name}: {player.CurrentHealth} hp, {mob.Name}: {mob.CurrentHealth} hp.");
                    round++;
                    Hold();
                }
            }
            if (player.Alive) player.Loot(mob.Corpse());
            mob = null;
        }

        private static void GameOver()
        {
            string[] go = { "GAME OVER!", "Thanks for playing, hope you enjoyed it.", "", "Why not have another go?" };
            BorderPrint(go);
        }
    }
}