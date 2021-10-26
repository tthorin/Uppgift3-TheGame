
namespace Uppgift3_TheGame
{
    using System;
    using System.Collections.Generic;
    using Enums;
    using static Helpers.PrintHelpers;
    
    internal static class EverShiftingMaze
    {
        private static Random rng = new();

        internal static Menu GetNewRoom(Player pc)
        {
            string[] roomDescriptions =
            {
                "This room looks oddly familiar...",
                "You could swear you've been here before...",
                "Is that something moving in the corner?",
                "Didn't you just leave this room?",
                "What's that over there?",
                "You are feeling a bit lost...",
                "What was that?",
                "Is this the right way?",
                "You know just where you are."
            };
            List<string> roomList = new() { "The Evershifting Maze,", $"{roomDescriptions[rng.Next(0, roomDescriptions.Length)]}" };
            roomList.Add($"Level: {pc.Level} Xp: {pc.Xp} / {pc.XpToNextLevel}");
            roomList.Add($"Current health: {pc.CurrentHealth} / {pc.MaxHealth}");
            roomList.Add("");
            List<string> exitList = new();
            do
            {
                exitList.Clear();
                Directions exits = (Directions)rng.Next(1, 16);
                if ((exits & Directions.North) == Directions.North) exitList.Add("Go North.");
                if ((exits & Directions.East) == Directions.East) exitList.Add("Go East.");
                if ((exits & Directions.South) == Directions.South) exitList.Add("Go South.");
                if ((exits & Directions.West) == Directions.West) exitList.Add("Go West.");

            } while (exitList.Count <2);
            roomList.AddRange(exitList);

            roomList.Add("Head back to town.");

            Menu room = new Menu(roomList, 2, 3);
            return room;
        }

        internal static Menu EncounterMenu(Player pc, Monster mob)
        {
            List<string> encounterList = new()
            {
                "Encounter!",
                $"You have encountered a {mob.Alias}.",
                $"Your health: {pc.CurrentHealth} / {pc.MaxHealth}",
                "Fight!",
                "Flee!"
            };
            Menu encounterMenu = new Menu(encounterList, 1, 2);
            return encounterMenu;
        }
        internal static void PortalStone()
        {
            string[] portalStone =
                    {
                        "A shimmering portal appears before you as you activate your portal stone.",
                        "You step trough and find yourself back in \"town\"."
                    };
            BorderPrint(portalStone);
        }
    }
}