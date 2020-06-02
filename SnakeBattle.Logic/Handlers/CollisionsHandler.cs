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
        public static List<Direction> GetFreeForCollisionDirections(GameBoard gameBoard, EnemyPart?[,] enemyParts, Direction[] directions = null)
        {
            List<Direction> res = new List<Direction>();
            bool isEvil = MySnakeParameters.EvilsDuration >= 1;

            Direction[] dirs = directions ?? GameSettings.Directions;
            foreach (var dir in dirs)
            {
                var point = MySnakeParameters.Head.Shift(dir);

                bool canMakeCollision = false;
                foreach (var dir2 in new Direction[] { Direction.Down, Direction.Left, Direction.Right, Direction.Stop, Direction.Up })
                {
                    var nextPoint = point.Shift(dir2);
                    var element = gameBoard.GetElementAt(nextPoint);

                    if (isEvil)
                    {
                        if (element == BoardElement.EnemyHeadEvil)
                        {
                            canMakeCollision = element == BoardElement.EnemyHeadEvil
                                && enemyParts[nextPoint.X, nextPoint.Y] != null
                                && enemyParts[nextPoint.X, nextPoint.Y].Value.DistanceFromTali + 1 > MySnakeParameters.Length - 2;
                        }
                    }
                    else
                    {
                        canMakeCollision = element.IsEnemyHead();
                    }

                    if (canMakeCollision)
                        break;
                }

                if (!canMakeCollision)
                    res.Add(dir);
            }

            return res;
        }
    }
}
