using System;
using System.Collections.Generic;
using System.Text;
using SnakeBattle.Api;

namespace SnakeBattle.Logic.Handlers
{
    public static class DeadAndSleepHandler
    {
        public static bool IsDeadOrSleep(GameBoard gameBoard) 
        {
            var point = gameBoard.GetMyHead();

            if (point == null)
                return true;

            var element = gameBoard.GetElementAt(point.Value);

            return element == BoardElement.HeadDead || element == BoardElement.HeadSleep;
        }
    }
}
