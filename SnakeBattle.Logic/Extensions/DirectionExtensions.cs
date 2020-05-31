using System;
using System.Collections.Generic;
using System.Text;
using SnakeBattle.Api;

namespace SnakeBattle.Logic.Extensions
{
    internal static class DirectionExtensions
    {
        public static Direction Inverse(this Direction direction) =>
            direction switch
            {
                Direction.Down   => Direction.Up,
                Direction.Up     => Direction.Down,
                Direction.Left   => Direction.Right,
                Direction.Right  => Direction.Left,
                Direction.Stop   => Direction.Stop,
                _                => throw new Exception(),
            };
    }
}
