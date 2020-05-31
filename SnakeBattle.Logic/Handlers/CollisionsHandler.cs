using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using SnakeBattle.Api;
using SnakeBattle.Logic.Extensions;

namespace SnakeBattle.Logic.Handlers
{
    public static class CollisionsHandler
    {
        public static List<Direction> GetFreeForCollisionDirections(GameBoard gameBoard, Direction[] directions = null)
        {
            List<Direction> res = new List<Direction>();
            bool isEvil = MySnakeParameters.EvilsDuration >= 1;

            Direction[] dirs = directions ?? GameSettings.Directions;
            foreach (var dir in dirs)
            {
                var point = MySnakeParameters.Head.Shift(dir);

                bool canMakeCollision = false;
                foreach (var dir2 in dirs)
                {
                    var element = gameBoard.GetElementAt(point.Shift(dir2));

                    if (isEvil)
                    {
                        canMakeCollision = element == BoardElement.EnemyHeadEvil;
                    }
                    else
                    {
                        canMakeCollision = element.IsEnemyHead();
                    }
                }

                if (!canMakeCollision)
                    res.Add(dir);
            }

            return res;
        }
    }
}
