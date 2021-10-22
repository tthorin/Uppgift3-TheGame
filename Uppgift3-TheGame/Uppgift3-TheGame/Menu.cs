namespace Uppgift3_TheGame
{
    using System;
    using System.Collections.Generic;
    using Helpers;


    public class Menu
    {
        public List<string> MenuItems { get; set; }
        int headerLines = 0;
        public int HeaderLines { get => headerLines; set => headerLines = value >= MenuItems.Count ? 0 : value; }
        int infoLines = 0;
        public int InfoLines { get => infoLines; set => infoLines = value + HeaderLines >= MenuItems.Count ? 0 : value; }
        public int MenuWidth { get; private set; } = 0;
        public int StartSelected { get => HeaderLines + InfoLines; }
        public string TopLine { get; private set; } = "";
        public string MidLine { get; private set; } = "";
        public string BottomLine { get; private set; } = "";
        public string HelpText { get; private set; } = " Arrow keys to navigate, Enter to select.";


        
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

        private void SetupMenu(List<string> menuItems, int headerLines, int infoLines)
        {
            MenuItems = menuItems;
            HeaderLines = headerLines;
            InfoLines = infoLines;
            SetupPrintables();
        }

        private void SetupPrintables()
        {

            MenuWidth = HelpText.Length;
            foreach (var item in MenuItems)
            {
                if (item.Length > MenuWidth) MenuWidth = item.Length;
            }

            TopLine = "╔" + new string('═', MenuWidth) + "╗";
            MidLine = "╟" + new string('─', MenuWidth) + "╢";
            BottomLine = "╚" + new string('═', MenuWidth) + "╝";
        }
        public string Show()
        {
            if (MenuItems.Count == 0) MenuItems.Add("Auto added item because of empy menu, check your code.");
            return MenuHelper.DoMenu(this);
        }
        public void UpdateMenu(string update, int index)
        {
            MenuItems[index] = update;
        }
    }
}
