﻿namespace Uppgift3_TheGame.POCO
{
    using Interface;
    public class Weapon:ISellable
    {
        public string Name { get; set; } = "";
        public int Damage { get; set; } = 0;
        public int Price { get; set; } = 0;
        public string[] FlavourTexts { get; set; } = new string[4];
    }
}
