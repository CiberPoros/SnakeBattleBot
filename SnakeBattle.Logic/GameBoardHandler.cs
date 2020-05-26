using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SnakeBattle.Api;

namespace SnakeBattle.Logic
{
    public class GameBoardHandler
    {
        private static readonly Random _random = new Random();

        private static readonly DirectionWeightHandler _directionWeightHandler = new DirectionWeightHandler();

        public SnakeAction Handle(GameBoard gameBoard)
        {
            DateTime start = DateTime.Now;
            try
            {
                IsEvenHandler.Handle(gameBoard);

                var directions = gameBoard.GetFreeDirections()
                                              .DefaultIfEmpty()
                                              .ToList();

                directions.ForEach(a => Console.Write($"{ a } "));
                Console.WriteLine();

                var weights = _directionWeightHandler.GetWeights(gameBoard);

                var dirs = (from kvp in weights
                            where directions.Contains(kvp.Key)
                            select kvp).ToList();

                dirs.Sort((a, b) => a.Value.CompareTo(b.Value));

                var direction = dirs.Last().Key;

                return new SnakeAction(false, false, direction);
            }
            catch (Exception)
            {
                return new SnakeAction(false, false, Direction.Right);
            }
            finally
            {
                Console.WriteLine((DateTime.Now - start).Milliseconds);
            }
        }
    }
}
