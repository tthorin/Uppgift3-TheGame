// -----------------------------------------------------------------------------------------------
//  Armor.cs by Thomas Thorin, Copyright (C) 2021.
//  Published under GNU General Public License v3 (GPL-3)
// -----------------------------------------------------------------------------------------------

namespace Uppgift3_TheGame.POCO
{
    using Interface;
    internal class Armor : Item
    {
        
        public int Protection { get; set; } = 0;
        public override int EffectiveValue => Protection;
       
        
    }
}
