using System;
using System.Collections.Generic;
using System.Text;
using SnakeBattle.Api;

namespace SnakeBattle.Logic.Handlers
{
    public static class StaticWeightsHandler
    {
        public static long[,] GetStaticWeights(GameBoard gameBoard)
        {
            int size = gameBoard.Size;

            long[,] res = new long[size, size];
            var isAchievable = AchievablePointsHandler.GetAchievablePoints(gameBoard);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (isAchievable[i, j])
                    {
                        res[i, j] = GetElementWeight(gameBoard.GetElementAt(new BoardPoint(i, j)));
                    }
                }
            }

            return res;
        }

        private static long GetElementWeight(BoardElement boardElement) =>
            boardElement switch
            {
                BoardElement.Apple      => GameSettings.AppleWeight,
                BoardElement.FuryPill   => GameSettings.EvilPillWeight,
                BoardElement.Gold       => GameSettings.CoinWeight,
                BoardElement.Stone      => GameSettings.StoneWeight,
                _                       => 0,
            };
    }
}
