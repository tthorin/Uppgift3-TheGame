namespace Uppgift3_TheGame
{
    using System;
    using System.Collections.Generic;

    public class Program
    {
        public Game game;
        public static void Main(string[] args)
        {
            Game game = new();
            game.Start();
        }
    }
}
