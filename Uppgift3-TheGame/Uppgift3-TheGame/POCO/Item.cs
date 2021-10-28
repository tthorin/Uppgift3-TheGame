namespace Uppgift3_TheGame.POCO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    

    internal class Item
    {
        internal string Name { get; set; }
        internal int Price { get; set; }

        internal virtual int EffectiveValue { get; }
        internal string[] FlavourTexts { get; set; } = new string[4];
    }
}
