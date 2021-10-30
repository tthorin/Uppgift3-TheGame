// -----------------------------------------------------------------------------------------------
//  Game.cs by Thomas Thorin, Copyright (C) 2021.
//  Published under GNU General Public License v3 (GPL-3)
// -----------------------------------------------------------------------------------------------

namespace Uppgift3_TheGame
{
    using POCO;
    using System;
    using static Helpers.PrintHelpers;

    public class Game
    {
        private readonly Player player = new();

        private Monster mob;
        private Menu roomMenu;

        public void Start()
        {
            player.Name = GameHelper.Intro();
            var boss = (Player)player.Clone(); //todo: fixa boss.
            Town cave = new() { Name = "the Cave" };
            var keepPlaying = cave.Enter(player);

            while (keepPlaying && !player.GameOver)
            {
                (MazeRoom room, var noEncounter) = Maze.ShowMaze();
                if (!noEncounter) Encounter();
                roomMenu = null;
                roomMenu = GameHelper.GetRoomMenu(player, room);

                if (!player.GameOver)
                {
                    keepPlaying = ShowRoom(cave);
                }
                else
                {
                    GameOver();
                }
            }
        }


        private bool ShowRoom(Town town)
        {
            var choice = "";
            var keepPlaying = true;
            do
            {
                roomMenu.UpdateMenuItem($"Current health: {player.CurrentHealth} / {player.MaxHealth}", 3);

                if (mob != null) AddMobToRoomMenu();
                else RemoveMobFromRoomMenu();

                choice = roomMenu.UseMenu();

                if (choice.StartsWith("Show")) player.ShowStats();
                else if (choice.StartsWith("Attack")) Fight();

            } while (!player.GameOver && choice != "Head back to town." && !choice.StartsWith("Go"));

            if (choice == "Head back to town.") keepPlaying = GameHelper.PortalStone(player, town);

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

        private void Encounter()
        {
            mob = new Monster(player.Level);
            BorderPrint($"You encounter a {mob.FullName}!");
            Menu encounter = GameHelper.EncounterMenu(player, mob);
            var choice = encounter.UseMenu();
            if (choice == "Fight!") Fight();
            else Flee();
        }

        private void Flee()
        {
            BorderPrint($"As you turn and flee like a chicken, {mob.Alias} hits you in the back!");
            player.TakeDamage(mob.Attack());
            Hold();
        }

        private void Fight()
        {
            var round = 1;
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