using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SnakeBattle.Api;
using SnakeBattle.Logic.Extensions;

namespace SnakeBattle.Logic.Handlers
{
    public static class FreeDirectionsHandler
    {
        private static readonly BoardElement[] _freeForMoveWithoutEvil = new BoardElement[]
        {
            BoardElement.Apple,
            BoardElement.FuryPill,
            BoardElement.Gold,
            BoardElement.None,
            BoardElement.TailEndDown,
            BoardElement.TailEndLeft,
            BoardElement.TailEndRight,
            BoardElement.TailEndUp,
            BoardElement.TailInactive,
        };

        public static List<Direction> GetFreeDirections(GameBoard gameBoard, Direction[] directions = null)
        {
            List<Direction> res = new List<Direction>();
            bool isEvil = MySnakeParameters.EvilsDuration >= 1;

            Direction[] dirs = directions ?? GameSettings.Directions;
            foreach (var dir in dirs)
            {
                var nextPoint = MySnakeParameters.Head.Shift(dir);
                var element = gameBoard.GetElementAt(nextPoint);
                if (isEvil)
                {
                    if (!element.IsBarrier()
                        && !element.IsMyBody()
                        && nextPoint != MySnakeParameters.GetNeck())
                        res.Add(dir);
                }
                else
                {
                    if (_freeForMoveWithoutEvil.Contains(element)
                        && nextPoint != MySnakeParameters.GetNeck())
                        res.Add(dir);
                }
            }

            return res;
        }
    }
}
