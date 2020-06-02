using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using SnakeBattle.Api;
using SnakeBattle.Logic.Handlers;

namespace SnakeBattle.Logic
{
    public static class Solver
    {
        private static readonly Random _random = new Random();
        private const string _logfile = @"C:\Users\USER\source\repos\SnakeBattleBot\Client\bin\Debug\netcoreapp3.1\log.txt";

        public static SnakeAction Solve(GameBoard gameBoard)
        {
            DateTime start = DateTime.Now;
            GameSettings.UpdateSettings();

            if (DeadAndSleepHandler.IsDeadOrSleep(gameBoard))
            {
                MySnakeParameters.Reset();

                return new SnakeAction(false, false, Direction.Up);
            }

            MySnakeParameters.UpdateBeforeMove(gameBoard);
            Log(MySnakeParameters.Body, "My snake body");

            var freeDirections = FreeDirectionsHandler.GetFreeDirections(gameBoard);
            Log(freeDirections, "Free directions");

            var enemyPlayersInfo = EnemySnakesHandler.GetEnemyPlayersInfo(gameBoard);
            Log(enemyPlayersInfo, "Enemy players info");

            var nonCollisionDirections = CollisionsHandler.GetFreeForCollisionDirections(gameBoard, enemyPlayersInfo, freeDirections.ToArray());
            Log(nonCollisionDirections, "Non collision directions");

            var staticWeights = StaticWeightsHandler.GetStaticWeights(gameBoard);
            Log(staticWeights, "Static weights");

            var dynamicWeights = DynamicWeightsHandler.GetWeights(gameBoard, nonCollisionDirections.ToArray());
            Log(dynamicWeights, "Dynamic weights");

            Direction direction = Direction.Up;
            if (freeDirections.Count > 0)
                direction = freeDirections[_random.Next(freeDirections.Count)];
            if (nonCollisionDirections.Count > 0)
                direction = nonCollisionDirections[_random.Next(nonCollisionDirections.Count)];

            long w = 0;
            foreach (var kvp in dynamicWeights)
                if (kvp.Value > w)
                {
                    w = kvp.Value;
                    direction = kvp.Key;
                }

            Console.WriteLine("Tick number: " + MySnakeParameters.TickNumber);
            Console.WriteLine("Lingth before: " + MySnakeParameters.Length);
            Console.WriteLine("Evil time before: " + MySnakeParameters.EvilsDuration);
            MySnakeParameters.UpdateAfterMove(gameBoard, direction);
            Console.WriteLine("Lingth after: " + MySnakeParameters.Length);
            Console.WriteLine("Evil time after: " + MySnakeParameters.EvilsDuration);

            Console.WriteLine((DateTime.Now - start).Milliseconds);
            return new SnakeAction(false, false, direction);
        }

        private static void Log<T>(IEnumerable<T> collection, string logInfo = "logInfo")
        {
            if (!GameSettings.MakeLog)
                return;

            var s = $"{ Environment.NewLine }{ logInfo }: { Environment.NewLine }";

            foreach (var val in collection.Select(a => a.ToString()))
                s += val != null ? string.Format("{0,-8}", val) : string.Format("{0,-8}", "null");
            s += Environment.NewLine;

            File.AppendAllText(_logfile, s, Encoding.Default);
        }

        private static void Log<T>(T[,] collection, string logInfo = "logInfo")
        {
            if (!GameSettings.MakeLog)
                return;

            var s = $"{ Environment.NewLine }{ logInfo }: { Environment.NewLine }";

            for (int i = 0; i < collection.GetLength(0); i++)
            {
                for (int j = 0; j < collection.GetLength(1); j++)
                {
                    s += (collection[j, i] != null) ? string.Format("{0,-3}", collection[j, i]) : string.Format("{0,-3}", "uk"); 
                }
                s += Environment.NewLine;
            }
            s += Environment.NewLine;

            File.AppendAllText(_logfile, s, Encoding.Default);
        }
    }
}
