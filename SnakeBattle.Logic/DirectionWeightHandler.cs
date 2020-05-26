using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using SnakeBattle.Api;

namespace SnakeBattle.Logic
{
    internal class DirectionWeightHandler
    {
        private readonly int _deepLimit = 13; // Максимальная глубина рекурсии для подсчета веса
        private GameBoard _gameBoard;

        private readonly HashSet<BoardElement> _havingWeights = new HashSet<BoardElement>()
        { 
            BoardElement.Apple,
            BoardElement.Gold,
            BoardElement.FuryPill,
            BoardElement.EnemyBodyHorizontal,
            BoardElement.EnemyBodyLeftDown,
            BoardElement.EnemyBodyLeftUp,
            BoardElement.EnemyBodyRightDown,
            BoardElement.EnemyBodyRightUp,
            BoardElement.EnemyBodyVertical,
            BoardElement.EnemyHeadDown,
            BoardElement.EnemyHeadLeft,
            BoardElement.EnemyHeadRight,
            BoardElement.EnemyHeadUp,
            BoardElement.EnemyTailEndDown,
            BoardElement.EnemyTailEndLeft,
            BoardElement.EnemyTailEndRight,
            BoardElement.EnemyTailEndUp,
            BoardElement.Stone,
        };

        private readonly Dictionary<BoardElement, long> _elementWeights = new Dictionary<BoardElement, long>()
        {
            { BoardElement.Apple, 6227020800 * 3 },
            { BoardElement.Gold, 6227020800 * 8 },
            { BoardElement.FuryPill, 6227020800 * 9 },
            { BoardElement.EnemyBodyHorizontal, 6227020800 * (-1) },
            { BoardElement.EnemyBodyLeftDown, 6227020800 * (-1) },
            { BoardElement.EnemyBodyLeftUp, 6227020800 * (-1) },
            { BoardElement.EnemyBodyRightDown, 6227020800 * (-1) },
            { BoardElement.EnemyBodyRightUp, 6227020800 * (-1) },
            { BoardElement.EnemyBodyVertical, 6227020800 * (-1) },
            { BoardElement.EnemyHeadDown, 6227020800 * (-1) },
            { BoardElement.EnemyHeadLeft, 6227020800 * (-1) },
            { BoardElement.EnemyHeadRight, 6227020800 * (-1) },
            { BoardElement.EnemyHeadUp, 6227020800 * (-1) },
            { BoardElement.EnemyTailEndDown, 6227020800 * (-1) },
            { BoardElement.EnemyTailEndLeft, 6227020800 * (-1) },
            { BoardElement.EnemyTailEndRight, 6227020800 * (-1) },
            { BoardElement.EnemyTailEndUp, 6227020800 * (-1) },
            { BoardElement.Stone, 6227020800 * 5 },
        };

        private readonly Dictionary<BoardElement, long> _elementWeightsEvil = new Dictionary<BoardElement, long>()
        {
            { BoardElement.Apple, 6227020800 * 3 },
            { BoardElement.Gold, 6227020800 * 8 },
            { BoardElement.FuryPill, 6227020800 * 9 },
            { BoardElement.EnemyBodyHorizontal, 6227020800 * 4 },
            { BoardElement.EnemyBodyLeftDown, 6227020800 * 4 },
            { BoardElement.EnemyBodyLeftUp, 6227020800 * 4 },
            { BoardElement.EnemyBodyRightDown, 6227020800 * 4 },
            { BoardElement.EnemyBodyRightUp, 6227020800 * 4 },
            { BoardElement.EnemyBodyVertical, 6227020800 * 4 },
            { BoardElement.EnemyHeadDown, 6227020800 * 4 },
            { BoardElement.EnemyHeadLeft, 6227020800 * 4 },
            { BoardElement.EnemyHeadRight, 6227020800 * 4 },
            { BoardElement.EnemyHeadUp, 6227020800 * 4 },
            { BoardElement.EnemyTailEndDown, 6227020800 * 0 },
            { BoardElement.EnemyTailEndLeft, 6227020800 * 0 },
            { BoardElement.EnemyTailEndRight, 6227020800 * 0 },
            { BoardElement.EnemyTailEndUp, 6227020800 * 0 },
            { BoardElement.Stone, 6227020800 * 4 },
        };

        public Dictionary<Direction, long> GetWeights(GameBoard gameBoard)
        {
            _gameBoard = gameBoard;
            var result = new Dictionary<Direction, long>()
            {
                { Direction.Down, 0 },
                { Direction.Left, 0 },
                { Direction.Right, 0 },
                { Direction.Up, 0 },
            };

            BoardPoint? head = gameBoard.GetMyHead();
            if (head == null)
                return result;

            foreach (var dir in Utils.MainDirections)
            {
                long weight = 0;
                BoardPoint nextBoardPoint = head.Value.Shift(dir);
                if (_gameBoard.IsFreeForMove(nextBoardPoint))
                    Dfs(nextBoardPoint, dir.Inverse(), 0, ref weight, 1);

                result[dir] = Math.Max(result[dir], weight);
            }

            return result;
        }

        public void Dfs(BoardPoint boardPoint, Direction fromDirection, long currentWeight, ref long maxWeight, long deep)
        {
            var element = _gameBoard.GetElementAt(boardPoint);

            long weight = 0;

            if (IsEvenHandler.IsEven)
                weight = currentWeight + (_havingWeights.Contains(element) ? _elementWeightsEvil[element] / deep : 0);
            else
                weight = currentWeight + (_havingWeights.Contains(element) ? _elementWeights[element] / deep : 0);

            if (deep == _deepLimit)
            {
                maxWeight = Math.Max(maxWeight, weight);

                return;
            }

            foreach (var dir in Utils.MainDirections)
                if (dir != fromDirection)
                {
                    BoardPoint nextBoardPoint = boardPoint.Shift(dir);
                    if (_gameBoard.IsFreeForMove(nextBoardPoint))
                        Dfs(nextBoardPoint, dir.Inverse(), weight, ref maxWeight, deep + 1);
                }
        }
    }
}
