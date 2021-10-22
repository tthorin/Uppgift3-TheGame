namespace Uppgift3_TheGame.Helpers
{
    using System;
    using System.Collections.Generic;


    public static class MenuHelper
    {
        public static string DoMenu(Menu menu)
        {
            SetColors();
            int highlightItem = menu.StartSelected;
            Console.Clear();
            string userChoice = "";
            ConsoleKeyInfo input = new();
            do
            {
                UpdateMenu(menu, highlightItem);
                input = Console.ReadKey(true);
                switch (input.Key)
                {
                    case ConsoleKey.LeftArrow or ConsoleKey.UpArrow:
                        if (highlightItem == menu.StartSelected) highlightItem = menu.MenuItems.Count - 1;
                        else highlightItem--;
                        break;
                    case ConsoleKey.RightArrow or ConsoleKey.DownArrow:
                        if (highlightItem == menu.MenuItems.Count - 1) highlightItem = menu.StartSelected;
                        else highlightItem++;
                        break;
                    case ConsoleKey.Enter:
                        userChoice = menu.MenuItems[highlightItem];
                        break;
                    default:
                        break;
                }
            } while (userChoice == "");
            Console.ResetColor();
            Console.Clear();
            return userChoice;
        }

        private static void UpdateMenu(Menu menu, int highlightItem)
        {
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);

            Console.WriteLine(menu.TopLine);

            for (int row = 0; row < menu.MenuItems.Count; row++)
            {

                if (row == highlightItem)
                {
                    Console.Write("║");
                    InvertColors();
                    Console.Write(menu.MenuItems[row].PadRight(menu.MenuWidth));
                    SetColors();
                    Console.WriteLine("║");
                }
                else
                {
                    Console.WriteLine($"║" + menu.MenuItems[row].PadRight(menu.MenuWidth) + "║");
                    if (row == menu.HeaderLines-1 || row == (menu.HeaderLines + menu.InfoLines)-1)
                        Console.WriteLine(menu.MidLine);
                }

            }
            Console.WriteLine(menu.BottomLine);
            Console.WriteLine(menu.HelpText);
        }

        static private void SetColors()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.BackgroundColor = ConsoleColor.Blue;
        }
        static private void InvertColors()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.BackgroundColor = ConsoleColor.Cyan;
        }

    }
}
