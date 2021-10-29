// -----------------------------------------------------------------------------------------------
//  Maze.cs by Thomas Thorin, Copyright (C) 2021.
//  Published under GNU General Public License v3 (GPL-3)
// -----------------------------------------------------------------------------------------------

namespace Uppgift3_TheGame
{
    using System;
    using System.Collections.Generic;
    using static Enums.Direction;
    using Enums;
    using static Helpers.PrintHelpers;
    using Uppgift3_TheGame.POCO;
    

    internal static class Maze
    {   
        static int maxWidth = Console.WindowWidth - 1;
        static int maxHeight = Console.WindowHeight - 1;
        static MazeRoom[,] maze = new MazeRoom[maxWidth - 2, maxHeight - 2];
        static Position startPos = new() { X = (maxWidth - 2) / 2, Y = (maxHeight - 2) / 2 };
        static Position currentPos = new() { X = startPos.X, Y = startPos.Y };
        static Random rng = new();
        
        internal static MazeRoom ShowMaze()
        {
            PrintBorder();
            return maze[currentPos.X, currentPos.Y];
        }
        private static void PrintBorder()
        {   
            SetColors();
            Console.Clear();
            for (int row = 0; row < maxHeight; row++)
            {
                if (row == 0) Console.WriteLine("┌" + new string('─', maxWidth - 2) + "┐");
                else if (row > 0 && row < maxHeight - 1) Console.WriteLine("│" + new string(' ', maxWidth - 2) + "│");
                else Console.WriteLine("└" + new string('─', maxWidth - 2) + "┘");
            }
            Console.Write("Arrow keys to navigate, Press <Enter> for menu");
            PrintMaze();
        }

        private static void PrintMaze()
        {
            int northSouthWestEast = 15;
            if (maze[startPos.X, startPos.Y] == null) maze[startPos.X, startPos.Y] = new MazeRoom { Exits = (Direction)northSouthWestEast };
            for (int row = 0; row <= maze.GetUpperBound(1); row++)
            {
                for (int column = 0; column < maze.GetUpperBound(0); column++)
                {
                    Console.SetCursorPosition(column + 1, row + 1);
                    if (maze[column, row] != null)
                    {
                        if (column == currentPos.X && row == currentPos.Y)
                        {
                            InvertColors();
                            Console.Write(maze[column, row].Shape);
                            SetColors();
                        }
                        else Console.Write(maze[column, row].Shape);
                    }

                }

            }
            Move(); //todo: cleanup
        }
        private static void Move()
        {
            Position newPos = new(currentPos);
            Console.CursorVisible = false;
            bool noEncounter = true;
            ConsoleKeyInfo key = new();

            while (key.Key != ConsoleKey.Enter && key.Key != ConsoleKey.Escape && noEncounter)
            {
                key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow: if (maze[currentPos.X, currentPos.Y].Exits.HasFlag(West)) newPos.X--; break;
                    case ConsoleKey.RightArrow: if (maze[currentPos.X, currentPos.Y].Exits.HasFlag(East)) newPos.X++; break;
                    case ConsoleKey.UpArrow: if (maze[currentPos.X, currentPos.Y].Exits.HasFlag(North)) newPos.Y--; break;
                    case ConsoleKey.DownArrow: if (maze[currentPos.X, currentPos.Y].Exits.HasFlag(South)) newPos.Y++; break;
                    default: break;
                }
                if (!newPos.Equals(currentPos))
                {
                    
                    if (maze[newPos.X, newPos.Y] == null) NewRoom(newPos);
                    Console.SetCursorPosition(newPos.X + 1, newPos.Y + 1);
                    InvertColors();
                    Console.Write(maze[newPos.X, newPos.Y].Shape);
                    SetColors();
                    Console.SetCursorPosition(currentPos.X + 1, currentPos.Y + 1);
                    Console.Write(maze[currentPos.X, currentPos.Y].Shape);
                    currentPos.Update(newPos);
                }
                //todo: uncomment
                //if (rng.Next(0, 10) == 9) noEncounter = false;
            }
            //todo: fix so can call Encounter() from here 
            //if (!noEncounter) game.Encounter();
        }

        private static void NewRoom(Position pos)
        {
            Direction exits = new();
            (Direction exits, int counter) cannotHave = CheckSurroundingCant(pos);
            if (cannotHave.counter == 3) exits = SingleExitRoom(cannotHave.exits);
            else
            {
                Direction mustHave = CheckSurroundingMust(pos, cannotHave.exits);
                int exitCounter;
                do
                {
                    exitCounter = 0;
                    exits = (Direction)rng.Next(1, 16);
                    if (exits.HasFlag(North)) exitCounter++;
                    if (exits.HasFlag(South)) exitCounter++;
                    if (exits.HasFlag(East)) exitCounter++;
                    if (exits.HasFlag(West)) exitCounter++;
                } while (exitCounter < 2 || !exits.HasFlag(mustHave) || (exits & cannotHave.exits) != Direction.None);
            }
            maze[pos.X, pos.Y] = new MazeRoom { Exits = exits };
        }

        private static Direction SingleExitRoom(Direction exits)
        {
            Direction singleExit = new();
            if (!exits.HasFlag(North)) singleExit = North;
            else if (!exits.HasFlag(South)) singleExit = South;
            else if (!exits.HasFlag(West)) singleExit = West;
            else singleExit = East;
            return singleExit;
        }

        private static (Direction cannotHave, int cannotCounter) CheckSurroundingCant(Position pos)
        {
            Direction cannotHave = new();
            int cannotCounter = 0;
            if (pos.Y - 1 < 0 || (maze[pos.X, pos.Y - 1] != null && !maze[pos.X, pos.Y - 1].Exits.HasFlag(South)))
            {
                cannotHave = cannotHave | North;
                cannotCounter++;
            }
            if (pos.Y + 1 > maze.GetUpperBound(1) || (maze[pos.X, pos.Y + 1] != null && !maze[pos.X, pos.Y + 1].Exits.HasFlag(North)))
            {
                cannotHave = cannotHave | South;
                cannotCounter++;
            }
            if (pos.X - 1 < 0 || (maze[pos.X - 1, pos.Y] != null && !maze[pos.X - 1, pos.Y].Exits.HasFlag(East)))
            {
                cannotHave = cannotHave | West;
                cannotCounter++;
            }
            if (pos.X + 1 > maze.GetUpperBound(0) || (maze[pos.X + 1, pos.Y] != null && !maze[pos.X + 1, pos.Y].Exits.HasFlag(West)))
            {
                cannotHave = cannotHave | East;
                cannotCounter++;
            }
            return (cannotHave, cannotCounter);
        }

        private static Direction CheckSurroundingMust(Position pos, Direction cannotHave)
        {
            Direction mustHave = new();
            if (!cannotHave.HasFlag(North) && maze[pos.X, pos.Y - 1] != null && maze[pos.X, pos.Y - 1].Exits.HasFlag(South))
                mustHave = mustHave | North;
            if (!cannotHave.HasFlag(South) && maze[pos.X, pos.Y + 1] != null && maze[pos.X, pos.Y + 1].Exits.HasFlag(North))
                mustHave = mustHave | South;
            if (!cannotHave.HasFlag(West) && maze[pos.X - 1, pos.Y] != null && maze[pos.X - 1, pos.Y].Exits.HasFlag(East))
                mustHave = mustHave | West;
            if (!cannotHave.HasFlag(East) && maze[pos.X + 1, pos.Y] != null && maze[pos.X + 1, pos.Y].Exits.HasFlag(West))
                mustHave = mustHave | East;
            return mustHave;
        }
    }
}
