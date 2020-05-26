using System;
using System.Collections.Generic;
using System.Text;
using SnakeBattle.Api;

namespace SnakeBattle.Logic
{
    public static class IsEvenHandler
    {
        public static bool IsEven = false;

        public static void Handle(GameBoard gameBoard)
        {
            var head = gameBoard.GetMyHead();

            if (head != null && gameBoard.GetElementAt(head.Value) == BoardElement.HeadEvil)
                IsEven = true;
            else
                IsEven = false;
        }
    }
}
