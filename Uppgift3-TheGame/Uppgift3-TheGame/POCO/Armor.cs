// -----------------------------------------------------------------------------------------------
//  Armor.cs by Thomas Thorin, Copyright (C) 2021.
//  Published under GNU General Public License v3 (GPL-3)
// -----------------------------------------------------------------------------------------------

namespace Uppgift3_TheGame.POCO
{
    using Interface;
    internal class Armor : ISellable
    {
        public string Name { get; set; } = "";
        public int Protection { get; set; } = 0;
        public int EffectiveValue => Protection;
        public int Price { get; set; } = 0;
        public string[] FlavourTexts { get; set; } = new string[4];
    }
}
