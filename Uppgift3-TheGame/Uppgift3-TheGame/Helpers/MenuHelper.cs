namespace MainMenu
{
    using System;
    using System.Collections.Generic;
    using MainMenu.Helpers;

    public static class MenuHelper
    {
        


        
        public static string DoMenu(Menu menu)
        {
            UpdateMdi();
            SetColors();
            int highlightItem = headerLines + infoLines;
            Console.Clear();
            string userChoice = "";
            ConsoleKeyInfo input = new();
            do
            {
                DisplayMenu(menu, headerLines, infoLines, highlightItem);
                input = Console.ReadKey(true);
                switch (input.Key)
                {
                    case ConsoleKey.LeftArrow or ConsoleKey.UpArrow:
                        if (mdi.HightLightRow == 0) mdi.HightLightRow = MenuOptions.Count - 1;
                        else mdi.HightLightRow--;
                        break;
                    case ConsoleKey.RightArrow or ConsoleKey.DownArrow:
                        if (mdi.HightLightRow == MenuOptions.Count - 1) mdi.HightLightRow = 0;
                        else mdi.HightLightRow++;
                        break;
                    case ConsoleKey.Enter:
                        userChoice = mdi.HightLightRow;
                        break;
                    default:
                        break;
                }
            } while (userChoice == "");
            Console.ResetColor();
            Console.Clear();
            return userChoice;
        }

        private static void UpdateMenu(string[] menu, int numberOfHeaderLines, int numberOfInfoLines, int highlightItem)
        {
            Console.CursorVisible = false;
            mdi.CurrentY = mdi.StartY;
            CenterPrintDecoration(mdi.topLine);
            CenterPrintDecoration("║" + Title.PadRight(mdi.MenuWidth) + "║");
            CenterPrintDecoration(mdi.midLine);

            for (int row = 0; row < MenuOptions.Count; row++)
            {
                Console.SetCursorPosition(mdi.StartX, mdi.CurrentY);
                if (mdi.HightLightRow == row)
                {

                    Console.Write("║");
                    InvertColors();
                    Console.Write(MenuOptions[row].PadRight(mdi.MenuWidth));
                    SetColors();
                    Console.Write("║");
                }
                else
                    Console.Write($"║" + MenuOptions[row].PadRight(mdi.MenuWidth) + "║");
                mdi.CurrentY++;
            }
            CenterPrintDecoration(mdi.bottomLine);
            CenterPrintDecoration(mdi.HelpText);
        }

        private void CenterPrintDecoration(string stringToPrint)
        {
            Console.SetCursorPosition(mdi.StartX, mdi.CurrentY);
            Console.Write(stringToPrint);
            mdi.CurrentY++;

        }

        private void UpdateMdi()
        {
            mdi.MenuWidth = mdi.HelpText.Length;
            foreach (var row in MenuOptions)
            {
                if (row.Length > mdi.MenuWidth) mdi.MenuWidth = row.Length;
            }

            const int extraRowsFromTitleAndDecoration = 5;
            mdi.MenuHeight = MenuOptions.Count + extraRowsFromTitleAndDecoration;

            mdi.StartX = Console.WindowWidth / 2 - (mdi.MenuWidth + 2) / 2;
            mdi.StartY = Console.WindowHeight / 2 - mdi.MenuHeight / 2;

            mdi.topLine = "╔" + new string('═', mdi.MenuWidth) + "╗";
            mdi.midLine = "╟" + new string('─', mdi.MenuWidth) + "╢";
            mdi.bottomLine = "╚" + new string('═', mdi.MenuWidth) + "╝";
        }
        private void SetColors()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.BackgroundColor = ConsoleColor.Blue;
        }
        private void InvertColors()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.BackgroundColor = ConsoleColor.Cyan;
        }

    }
}
