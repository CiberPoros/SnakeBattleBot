using System;
using System.Collections.Generic;
using System.Text;
using SnakeBattle.Api;

namespace SnakeBattle.Logic
{
    internal static class BoardPointExtensions
    {
        public static BoardPoint Shift(this BoardPoint boardPoint, Direction direction) =>
            direction switch
            {
                Direction.Down  => boardPoint.ShiftBottom(),
                Direction.Left  => boardPoint.ShiftLeft(),
                Direction.Right => boardPoint.ShiftRight(),
                Direction.Up    => boardPoint.ShiftTop(),
                Direction.Stop  => boardPoint,
                _               => throw new ArgumentException(),
            };
    }
}
