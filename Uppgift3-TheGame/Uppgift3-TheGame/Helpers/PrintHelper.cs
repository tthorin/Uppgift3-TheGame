namespace Uppgift3_TheGame.Helpers
{
    using System;
    using System.Threading;

    public static class PrintHelper
    {
        public static void DramaticPrint(string msg)
        {
            Console.CursorVisible = false;
            Console.Write(msg);
            for (int i = 0; i < 5; i++)
            {
                Console.Write(".");
                Thread.Sleep(100);
            }
            Console.WriteLine();
        }
        public static void PrintAndHold(string msg)
        {
            Console.WriteLine(msg);
            Console.WriteLine("Press any key to continue.");
            Console.CursorVisible = false;
            Console.ReadKey(true);
            Console.CursorVisible = true;
        }
        public static void Hold()
        {
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey(true);
        }
        public static void BorderPrint(string msg)
        {   
            Console.WriteLine($"╔{new string('═',msg.Length+2)}╗");
            Console.WriteLine($"║ {msg} ║");
            Console.WriteLine($"╚{new string('═', msg.Length + 2)}╝");
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey(true);
        }
    }
}
