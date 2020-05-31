using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using SnakeBattle.Api;
using SnakeBattle.Logic.Extensions;

namespace SnakeBattle.Logic.Handlers
{
    public static class DynamicWeightsHandler
    {
        private const int _deepLimit = 15;
        private static int _size;
        private static GameBoard _gameBoard;
        private static long[,] _staticWeights;
        private static EnemyPart?[,] _enemyParts;

        public static int[,] DistanceFromMyTail;

        public static Dictionary<Direction, long> GetWeights(GameBoard gameBoard, Direction[] directions = null)
        {
            _gameBoard = gameBoard;
            _size = gameBoard.Size;

            _staticWeights = StaticWeightsHandler.GetStaticWeights(gameBoard);
            _enemyParts = EnemySnakesHandler.GetEnemyPlayersInfo(gameBoard);

            Dictionary<Direction, long> result = new Dictionary<Direction, long>();
            Direction[] dirs = directions ?? GameSettings.Directions;
            foreach (var dir in dirs)
            {
                long maxWeight = 0;
                DistanceFromMyTail = GetDistanceFromMyTail();

                SnakeParameters snakeParameters = new SnakeParameters(MySnakeParameters.Length, MySnakeParameters.StonesCount, MySnakeParameters.EvilsDuration);

                CalcMaxWeight(snakeParameters, MySnakeParameters.Head, MySnakeParameters.Head.Shift(dir), ref maxWeight);

                result.Add(dir, maxWeight);
            }

            return result;
        }

        // Суперкритична скорость, мб придумаю что-нибудь с оптимизацией
        private static void CalcMaxWeight(SnakeParameters snakeParameters, 
            BoardPoint prevPoint, BoardPoint point, ref long maxWeight, 
            int attackedSnakesIdMask = 0, int deltaLen = 0, long currentWeight = 0, int deep = 1)
        {
            var element = _gameBoard.GetElementAt(point);

            snakeParameters.EvilsDuration = Math.Max(0, snakeParameters.EvilsDuration - 1);
            if (element == BoardElement.FuryPill)
            {
                snakeParameters.EvilsDuration += GameSettings.EvilPillTimeDuration;
                currentWeight += Math.Max(0, (GameSettings.EvilPillWeight
                    - GameSettings.EvilWeightReducter
                    * Math.Max(0, snakeParameters.EvilsDuration - GameSettings.EvilTimeDurationForReductEvilWeight))) / deep;
            }
            if (element == BoardElement.Apple)
            {
                snakeParameters.Length += GameSettings.AppleLengthBooster;
                currentWeight += GameSettings.AppleWeight / deep;
                deltaLen++;
            }
            if (element == BoardElement.Stone)
            {
                if (snakeParameters.EvilsDuration == 0)
                    snakeParameters.Length = Math.Max(snakeParameters.Length - GameSettings.StoneLengthReducter, 0);

                currentWeight += GameSettings.StoneWeight / deep;
                snakeParameters.StonesCount++;
            }
            if (element == BoardElement.Gold)
            {
                currentWeight += GameSettings.CoinWeight / deep;
            }
            if ((element.IsEnemyBody() || element.IsEnemyActiveHead())
                && _enemyParts[point.X, point.Y] != null
                && (attackedSnakesIdMask & _enemyParts[point.X, point.Y].Value.Id) == 0)
            {
                attackedSnakesIdMask |= _enemyParts[point.X, point.Y].Value.Id;
                var coeff = _enemyParts[point.X, point.Y].Value.DistanceFromTali - deep;
                if (snakeParameters.EvilsDuration > 0)
                    currentWeight += coeff * GameSettings.EnemyPartOfBodyWeight / deep;
            }

            if (deep >= _deepLimit)
            {
                maxWeight = Math.Max(maxWeight, currentWeight);
                return;
            }

            var tempDistance = DistanceFromMyTail[point.X, point.Y];
            DistanceFromMyTail[point.X, point.Y] = DistanceFromMyTail[prevPoint.X, prevPoint.Y] + 1;

            foreach (var dir in GameSettings.Directions)
            {
                var nextPoint = point.Shift(dir);
                var nextElement = _gameBoard.GetElementAt(nextPoint);

                if (nextElement.IsBarrier()
                    || nextPoint == prevPoint
                    || DistanceFromMyTail[nextPoint.X, nextPoint.Y] - deep + deltaLen > 0)
                    continue;

                if (snakeParameters.EvilsDuration <= 1)
                {
                    if (nextElement == BoardElement.Apple
                        || nextElement == BoardElement.FuryPill
                        || nextElement == BoardElement.Gold
                        || nextElement == BoardElement.None
                        || (nextElement.IsEnemySnakePart() && (_enemyParts[nextPoint.X, nextPoint.Y] == null || _enemyParts[nextPoint.X, nextPoint.Y].Value.DistanceFromTali - deep <= 0)))
                        CalcMaxWeight(snakeParameters, point, nextPoint, ref maxWeight, attackedSnakesIdMask, deltaLen, currentWeight, deep + 1);
                }
                else
                {
                    CalcMaxWeight(snakeParameters, point, nextPoint, ref maxWeight, attackedSnakesIdMask, deltaLen, currentWeight, deep + 1);
                }
            }

            DistanceFromMyTail[point.X, point.Y] = tempDistance;
        }

        private static int[,] GetDistanceFromMyTail()
        {
            int[,] res = new int[_size, _size];

            for (int i = 0; i < _size; i++)
                for (int j = 0; j < _size; j++)
                    res[i, j] = -10000;

            for (int i = 0; i < MySnakeParameters.Body.Count; i++)
                res[MySnakeParameters.Body[i].X, MySnakeParameters.Body[i].Y] = i;

            return res;
        }
    }

    public struct SnakeParameters // попробовать на структурку поменять
    {
        public int Length;
        public int StonesCount;
        public int EvilsDuration;

        public SnakeParameters(int length, int stonesCount, int evilsDuration)
        {
            Length = length;
            StonesCount = stonesCount;
            EvilsDuration = evilsDuration;
        }
    }
}
