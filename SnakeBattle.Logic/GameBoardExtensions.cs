using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SnakeBattle.Api;

namespace SnakeBattle.Logic
{
    internal static class GameBoardExtensions
    {
        private static readonly BoardElement[] barriers = new BoardElement[]
        {
            BoardElement.BodyHorizontal,
            BoardElement.BodyLeftDown,
            BoardElement.BodyLeftUp,
            BoardElement.BodyRightDown,
            BoardElement.BodyRightUp,
            BoardElement.BodyVertical,
            BoardElement.EnemyHeadEvil,
            BoardElement.EnemyHeadSleep,
            BoardElement.TailInactive,
            BoardElement.EnemyTailInactive,
            BoardElement.StartFloor,  
            BoardElement.Wall,
            //BoardElement.Stone, // убрать потом
        };

        private static readonly BoardElement[] barriersWithStone = new BoardElement[]
        {
            BoardElement.BodyHorizontal,
            BoardElement.BodyLeftDown,
            BoardElement.BodyLeftUp,
            BoardElement.BodyRightDown,
            BoardElement.BodyRightUp,
            BoardElement.BodyVertical,
            BoardElement.EnemyHeadEvil,
            BoardElement.EnemyHeadSleep,
            BoardElement.TailInactive,
            BoardElement.EnemyTailInactive,
            BoardElement.StartFloor,
            BoardElement.Wall,
            BoardElement.Stone, // убрать потом
        };

        public static bool IsFreeForMove(this GameBoard gameBoard, BoardPoint boardPoint)
        {
            if (IsEvenHandler.IsEven)
                return !barriers.Contains(gameBoard.GetElementAt(boardPoint));
            else
                return !barriersWithStone.Contains(gameBoard.GetElementAt(boardPoint));
        }

        public static IReadOnlyCollection<Direction> GetFreeDirections(this GameBoard gameBoard)
        {
            var myHead = gameBoard.GetMyHead();
            if (myHead == null)
                return new List<Direction>();

            return (from direction in Utils.MainDirections 
                    where gameBoard.IsFreeForMove(myHead.Value.Shift(direction))
                    select direction)
                    .ToList();
        }
    }
}
