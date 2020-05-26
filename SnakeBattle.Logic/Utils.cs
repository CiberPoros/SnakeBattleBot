using System;
using System.Collections.Generic;
using System.Text;
using SnakeBattle.Api;

namespace SnakeBattle.Logic
{
    internal class Utils
    {
        public static readonly Direction[] MainDirections = new Direction[]
        {
            Direction.Down,
            Direction.Left,
            Direction.Right,
            Direction.Up,
        };
    }
}
