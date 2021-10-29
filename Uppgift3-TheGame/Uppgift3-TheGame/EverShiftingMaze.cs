
namespace Uppgift3_TheGame
{
    using System;
    using System.Collections.Generic;
    using Enums;
    using static Helpers.PrintHelpers;
    using POCO;

    internal static class EverShiftingMaze
    {
        private static Random rng = new();

        internal static Menu GetRoomMenu(Player pc, MazeRoom room)
        {   
            //Enum.TryParse(lastMove, out Direction last);
            //Direction mustHave = ReverseDirection(last);
            //bool validRoom = false;

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
            roomList.Add($"Level: {pc.Level} Xp: {pc.Xp} / {pc.XpToNextLevel} Gold: {pc.Gold}");
            roomList.Add($"Current health: {pc.CurrentHealth} / {pc.MaxHealth}");
            roomList.Add("");
            List<string> exitList = new();
            do
            {
                exitList.Clear();
                Direction exits = (Direction)rng.Next(1, 16);
                if ((exits & Direction.North) == Direction.North) exitList.Add("Go North.");
                if ((exits & Direction.East) == Direction.East) exitList.Add("Go East.");
                if ((exits & Direction.South) == Direction.South) exitList.Add("Go South.");
                if ((exits & Direction.West) == Direction.West) exitList.Add("Go West.");
                if ((exits & mustHave) == mustHave) validRoom = true;

            } while (!validRoom || exitList.Count < 2);
            roomList.AddRange(exitList);

            roomList.Add("Show player stats.");
            roomList.Add("Head back to town.");

            Menu room = new Menu(roomList, 2, 3);
            return room;
        }

        private static Direction ReverseDirection(Direction last)
        {
            Direction reversed = Direction.None;
            if (last == Direction.North) reversed = Direction.South;
            else if (last == Direction.South) reversed = Direction.North;
            else if (last == Direction.East) reversed = Direction.West;
            else reversed = Direction.East;
            return reversed;
        }

        internal static Menu EncounterMenu(Player pc, Monster mob)
        {
            List<string> encounterList = new()
            {
                "Encounter!",
                $"You have encountered a {mob.FullName}.",
                $"Your health: {pc.CurrentHealth} / {pc.MaxHealth}",
                "Fight!",
                "Flee!"
            };
            Menu encounterMenu = new Menu(encounterList, 1, 2);
            return encounterMenu;
        }
        internal static bool PortalStone(Player pc,Town town)
        {
            string[] portalStone =
                    {
                        "A shimmering portal appears before you as you activate your portal stone.",
                        "You step trough and find yourself back in \"town\"."
                    };
            BorderPrint(portalStone);
            bool keepPlaying = town.Enter(pc);
            return keepPlaying;
        }
        internal static string Intro()
        {
            string[] intro =
            {
                "You, the hero of our story are currently standing in a dark cavern.",
                "Well, when I say \"hero\" what I actually mean is, let's be honest",
                "about it.... \"MONSTER\"! You are hired by the resident Dungeon Lord",
                "to track down and \"expel\" troublesome heroes and other do-gooders who",
                "come poking around, aswell as keeping the local, feral, monster poulation",
                "under control.",
                "",
                "As our story begin, what would you like to be called?"
            };
            BorderPrint(intro, false);
            Console.Write("Name: ");
            return Console.ReadLine().Trim();
        }

    }
}