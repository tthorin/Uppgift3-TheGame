// -----------------------------------------------------------------------------------------------
//  Position.cs by Thomas Thorin, Copyright (C) 2021.
//  Published under GNU General Public License v3 (GPL-3)
// -----------------------------------------------------------------------------------------------

namespace Uppgift3_TheGame.POCO
{
    using System;
    internal class Position:IEquatable<Position>
    {
        internal int X { get; set; }
        internal int Y { get; set; }

        internal Position(Position pos)
        {
            this.X = pos.X;
            this.Y = pos.Y;
        }
        internal Position() { }
        internal void Update(Position pos)
        {
            this.X = pos.X;
            this.Y = pos.Y;
        }

        public bool Equals(Position other)
        {
            bool equal = false;
            if (this.X == other.X && this.Y == other.Y) equal = true;
            return equal;
        }
    }
}
