namespace Uppgift3_TheGame
{
    //using Helpers;
    using System;
    using System.Collections.Generic;

    public class Menu
    {

        #region Private Fields

        private string bottomLine = "";
        private ConsoleColor currentBackground = Console.BackgroundColor;
        private ConsoleColor currentForeground = Console.ForegroundColor;
        private int headerLines = 0;
        private string HelpText = " Arrow keys to navigate, Enter to select.";
        private int infoLines = 0;
        private int menuwidth = 0;
        private string midLine = "";
        private string topLine = "";

        #endregion Private Fields

        #region Public Constructors

        public Menu(List<string> menuItems, int headerLines)
        {
            SetupMenu(menuItems, headerLines, 0);
        }

        public Menu(List<string> menuItems, int headerLines, int infoLines)
        {
            SetupMenu(menuItems, headerLines, infoLines);
        }

        public Menu(List<string> menuItems)
        {
            SetupMenu(menuItems, 1, 0);
        }

        #endregion Public Constructors

        #region Public Properties

        public List<string> MenuItems { get; set; }

        #endregion Public Properties

        #region Private Properties

        private int HeaderLines { get => headerLines; set => headerLines = value >= MenuItems.Count ? 0 : value; }
        private int InfoLines { get => infoLines; set => infoLines = value + HeaderLines >= MenuItems.Count ? 0 : value; }
        private int StartSelected { get => HeaderLines + InfoLines; }

        #endregion Private Properties

        #region Public Methods

        public void UpdateMenuItem(string update, int index)
        {
            MenuItems[index] = update;
            SetupPrintables();
        }

        public string UseMenu()
        {
            if (MenuItems.Count == 0) MenuItems.Add("Auto added item because of empy menu, check your code.");
            return DoMenu();
        }

        #endregion Public Methods

        #region Private Methods

        private string DoMenu()
        {
            int highlightItem = StartSelected;
            string userChoice = "";
            ConsoleKeyInfo input = new();

            SetColors();
            Console.Clear();

            do
            {
                UpdateMenu(highlightItem);
                input = Console.ReadKey(true);
                switch (input.Key)
                {
                    case ConsoleKey.LeftArrow or ConsoleKey.UpArrow:
                        if (highlightItem == StartSelected) highlightItem = MenuItems.Count - 1;
                        else highlightItem--;
                        break;

                    case ConsoleKey.RightArrow or ConsoleKey.DownArrow:
                        if (highlightItem == MenuItems.Count - 1) highlightItem = StartSelected;
                        else highlightItem++;
                        break;

                    case ConsoleKey.Enter:
                        userChoice = MenuItems[highlightItem];
                        break;

                    default:
                        break;
                }
            } while (userChoice == "");
            Console.ResetColor();
            Console.Clear();
            return userChoice;
        }
        private void InvertColors()
        {
            Console.ForegroundColor = currentBackground;
            Console.BackgroundColor = currentForeground;
        }

        private void SetColors()
        {
            Console.ForegroundColor = currentForeground;
            Console.BackgroundColor = currentBackground;
        }

        private void SetupMenu(List<string> menuItems, int headerLines, int infoLines)
        {
            MenuItems = menuItems;
            HeaderLines = headerLines;
            InfoLines = infoLines;
            SetupPrintables();
        }

        private void SetupPrintables()
        {
            menuwidth = HelpText.Length;
            foreach (var item in MenuItems)
            {
                if (item.Length > menuwidth) menuwidth = item.Length;
            }

            topLine = "╔" + new string('═', menuwidth) + "╗";
            midLine = "╟" + new string('─', menuwidth) + "╢";
            bottomLine = "╚" + new string('═', menuwidth) + "╝";
        }

        private void UpdateMenu(int highlightItem)
        {
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);

            Console.WriteLine(topLine);

            for (int row = 0; row < MenuItems.Count; row++)
            {
                if (row == highlightItem)
                {
                    Console.Write("║");
                    InvertColors();
                    Console.Write(MenuItems[row].PadRight(menuwidth));
                    SetColors();
                    Console.WriteLine("║");
                }
                else
                {
                    Console.WriteLine($"║" + MenuItems[row].PadRight(menuwidth) + "║");
                    if (row == HeaderLines - 1 || row == (HeaderLines + InfoLines) - 1) Console.WriteLine(midLine);
                }
            }
            Console.WriteLine(bottomLine);
            Console.WriteLine(HelpText);
        }

        #endregion Private Methods
    }
}