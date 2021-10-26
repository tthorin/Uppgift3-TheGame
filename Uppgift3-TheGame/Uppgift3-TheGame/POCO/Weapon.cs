// -----------------------------------------------------------------------------------------------
//  Weapon.cs by Thomas Thorin, Copyright (C) 2021.
//  Published under GNU General Public License v3 (GPL-3)
// -----------------------------------------------------------------------------------------------

namespace Uppgift3_TheGame.POCO
{
    using Interface;
    public class Weapon : ISellable
    {
        public string Name { get; set; } = "";
        public int Damage { get; set; } = 0;
        public int Price { get; set; } = 0;
        public string[] FlavourTexts { get; set; } = new string[4];

        public int EffectiveValue => Damage;
    }
}
