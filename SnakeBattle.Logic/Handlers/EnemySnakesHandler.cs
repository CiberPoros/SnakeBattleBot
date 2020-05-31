using System;
using System.Collections.Generic;
using System.Text;
using SnakeBattle.Api;
using SnakeBattle.Logic.Extensions;

namespace SnakeBattle.Logic.Handlers
{
    public static class EnemySnakesHandler
    {
        private static GameBoard _gameBoard;
        private static int _size;

        public static EnemyPart?[,] GetEnemyPlayersInfo(GameBoard gameBoard)
        {
            _gameBoard = gameBoard;
            _size = gameBoard.Size;

            int size = _size;
            EnemyPart?[,] res = new EnemyPart?[size, size];

            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    res[i, j] = null;

            int id = 1;
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                {
                    var point = new BoardPoint(i, j);
                    var element = gameBoard.GetElementAt(point);
                    if (element.IsEnemyActiveTail())
                    {
                        Stack<BoardPoint> stack = new Stack<BoardPoint>();
                        bool isEvil = false;
                        HandleEnemySnakeRec(point, point, stack, ref isEvil);

                        int snakeLength = stack.Count - 1;

                        for (int k = 0; stack.Count > 0; k++)
                        {
                            var p = stack.Pop();
                            res[p.X, p.Y] = new EnemyPart(snakeLength - k, isEvil, id);
                        }

                        id <<= 1;
                    }
                }

            return res;
        }

        private static void HandleEnemySnakeRec(BoardPoint prevPoint, BoardPoint point, Stack<BoardPoint> stack, ref bool isEvil)
        {
            stack.Push(point);

            var element = _gameBoard.GetElementAt(point);

            if (element.IsEnemyHead())
            {
                if (element == BoardElement.EnemyHeadEvil)
                    isEvil = true;

                return;
            }

            foreach (var dir in Utils.GetEnemyOpotDirections(element))
            {
                var nextPoint = point.Shift(dir);
                if (!nextPoint.IsValidPoint(_size))
                    continue;

                if (nextPoint == prevPoint)
                    continue;

                var nextElement = _gameBoard.GetElementAt(nextPoint);
                if (Utils.IsExtensionOfEnemyBody(element, nextElement))
                {
                    HandleEnemySnakeRec(point, nextPoint, stack, ref isEvil);
                    return;
                }
            }
        }
    }

    public struct EnemyPart
    {
        public int DistanceFromTali { get; }
        public bool IsEvil { get; }
        public int Id { get; }

        public EnemyPart(int distanceFromTali, bool isEvil, int id)
        {
            DistanceFromTali = distanceFromTali;
            IsEvil = isEvil;
            Id = id;
        }

        public override string ToString() => $"d{DistanceFromTali}e{ (IsEvil ? "1" : "0") }id{Id}";
    }
}
