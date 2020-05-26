using SnakeBattle.Api;
using System;
using System.Text;
using SnakeBattle.Logic;

namespace Client
{
    class Program
    {
        const string SERVER_ADDRESS = "http://codebattle-pro-2020s1.westeurope.cloudapp.azure.com/codenjoy-contest/board/player/sm1xfa33fx4rosmaihhs?code=1229689263704639302&gameName=snakebattle";

        //const string SERVER_ADDRESS = "http://epruizhsa0001t2:8080/codenjoy-contest/board/player/4ol1pue9vqijd518yjlb?code=1180389975885916399&gameName=snakebattle";

        private static readonly GameBoardHandler _gameBoardHandler = new GameBoardHandler();

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
            return _gameBoardHandler.Handle(gameBoard);

            var random = new Random();
            var direction = (Direction)random.Next(Enum.GetValues(typeof(Direction)).Length);
            var act = random.Next() % 3;
            return new SnakeAction(act == 0, act == 1, direction);
        }
    }
}