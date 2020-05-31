using System;
using System.Collections.Generic;
using System.Text;
using SnakeBattle.Api;
using SnakeBattle.Logic.Extensions;

namespace SnakeBattle.Logic.Handlers
{
    public static class AchievablePointsHandler
    {
        public static bool[,] GetAchievablePoints(GameBoard gameBoard)
        {
            int size = gameBoard.Size;

            bool[,] res = new bool[size, size];

            bool[,] used = new bool[size, size];
            Queue<BoardPoint> q = new Queue<BoardPoint>();
            q.Enqueue(MySnakeParameters.Head);
            used[MySnakeParameters.Head.X, MySnakeParameters.Head.Y] = true;

            while (q.Count > 0)
            {
                var point = q.Dequeue();
                res[point.X, point.Y] = true;

                foreach (var dir in GameSettings.Directions)
                {
                    var nextPoint = point.Shift(dir);

                    if (!nextPoint.IsValidPoint(size) || used[nextPoint.X, nextPoint.Y])
                        continue;

                    var element = gameBoard.GetElementAt(nextPoint);

                    if (!element.IsBarrier())
                    {
                        q.Enqueue(nextPoint);
                        used[nextPoint.X, nextPoint.Y] = true;
                    }
                }
            }

            return res;
        }
    }
}
