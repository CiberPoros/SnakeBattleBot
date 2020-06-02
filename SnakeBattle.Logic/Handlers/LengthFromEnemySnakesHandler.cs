using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using SnakeBattle.Api;
using SnakeBattle.Logic.Extensions;

namespace SnakeBattle.Logic.Handlers
{
    public class LengthFromEnemySnakesHandler
    {
        public static int[,] GetLengthsFromNearEnemySnake(GameBoard gameBoard)
        {
            int size = gameBoard.Size;
            int[,] result = new int[size, size];

            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                {
                    result[i, j] = 100000;
                }

            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                {
                    var point = new BoardPoint(i, j);
                    var element = gameBoard.GetElementAt(point);
                    
                    if (element.IsEnemyActiveHead())
                    {
                        bool[,] used = new bool[size, size];
                        int[,] lengths = new int[size, size];
                        Queue<BoardPoint> q = new Queue<BoardPoint>();

                        q.Enqueue(point);
                        used[point.X, point.Y] = true;
                        lengths[point.X, point.Y] = 1;

                        while (q.Count > 0)
                        {
                            var currentPoint = q.Dequeue();
                            var currentElement = gameBoard.GetElementAt(currentPoint);

                            foreach (var direction in GameSettings.Directions)
                            {
                                var nextPoint = currentPoint.Shift(direction);

                                if (!nextPoint.IsValidPoint(size) || used[nextPoint.X, nextPoint.Y])
                                    continue;

                                var nextElement = gameBoard.GetElementAt(nextPoint);

                                if (nextElement.IsFreeForMove())
                                {
                                    q.Enqueue(nextPoint);
                                    used[nextPoint.X, nextPoint.Y] = true;
                                    lengths[nextPoint.X, nextPoint.Y] = lengths[currentPoint.X, currentPoint.Y] + 1;
                                }
                            }
                        }

                        for (int k = 0; k < size; k++)
                            for (int l = 0; l < size; l++)
                                if (lengths[k, l] > 0)
                                    result[k, l] = Math.Min(result[k, l], lengths[k, l] - 1);
                    }
                }

            return result;
        }
    }
}
