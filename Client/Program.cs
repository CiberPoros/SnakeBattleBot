using SnakeBattle.Api;
using System;
using System.Text;
using SnakeBattle.Logic;
using System.Configuration;

namespace Client
{
    class Program
    {
        // const string SERVER_ADDRESS = "http://codebattle-pro-2020s1.westeurope.cloudapp.azure.com/codenjoy-contest/board/player/sm1xfa33fx4rosmaihhs?code=1229689263704639302&gameName=snakebattle";

        const string SERVER_ADDRESS = "http://codebattle-pro-2020s1.westeurope.cloudapp.azure.com/codenjoy-contest/board/player/fefebim3m5hbgpdrei6w?code=1527008534051302152&gameName=snakebattle";

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            var client = new SnakeBattleClient(SERVER_ADDRESS);
            client.Run(DoRun);

            Console.ReadKey();
            client.InitiateExit();
        }

        private static SnakeAction DoRun(GameBoard gameBoard)
        {
            return Solver.Solve(gameBoard);

            var random = new Random();
            var direction = (Direction)random.Next(Enum.GetValues(typeof(Direction)).Length);
            var act = random.Next() % 3;
            return new SnakeAction(act == 0, act == 1, direction);
        }
    }
}