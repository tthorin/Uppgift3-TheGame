namespace MainMenu.Helpers
{
    using System;
    using System.Collections.Generic;
    

    public class Menu
    {
        public List<string> MenuItems { get; set; } = new();
        public int HeaderLines { get; set; } = 0;
        public int InfoLines { get; set; } = 0;
        public int MenuHeight { get; private set; } = 0;
        public int MenuWidth { get; private set; } = 0;
        public int HightLightRow { get; private set; } = 0;
        public int StartX { get; private set; } = 0;
        public int StartY { get; private set; } = 0;
        public string topLine { get; private set; } = "";
        public string midLine { get; private set; } = "";
        public string bottomLine { get; private set; } = "";
        public string HelpText { get; private set; } = " Arrowkeys to navigate, Enter to select.";
        public int CurrentY { get; private set; }
    }
}
