namespace Uppgift3_TheGame.POCO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Interface;

    public class Item
    {
        public string Name { get; set; }
        public int Price { get; set; }

        public virtual int EffectiveValue { get; }
        public string[] FlavourTexts { get; set; } = new string[4];
    }
}
