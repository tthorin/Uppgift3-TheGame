namespace Uppgift3_TheGame
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        static void Main(string[] args)
        {
            Maze.PrintBorder();
            Console.Clear();
            Console.WriteLine("hepp: ");
            Console.ReadLine();
            Maze.PrintBorder();
            
            Game game = new();
            game.Start();
            
        }
    }
}
