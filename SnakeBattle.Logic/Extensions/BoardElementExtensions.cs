using System;
using System.Collections.Generic;
using System.Text;
using SnakeBattle.Api;

namespace SnakeBattle.Logic.Extensions
{
    internal static class BoardElementExtensions
    {
        public static bool IsBarrier(this BoardElement boardElement) =>
            boardElement == BoardElement.Wall
            || boardElement == BoardElement.StartFloor
            || boardElement == BoardElement.EnemyHeadSleep
            || boardElement == BoardElement.EnemyTailInactive
            || boardElement == BoardElement.TailInactive;

        public static bool IsMySnakePart(this BoardElement boardElement) =>
            IsMyBody(boardElement)
            || IsMyNotDeadHead(boardElement)
            || IsMyTail(boardElement);

        public static bool IsEnemySnakePart(this BoardElement boardElement) =>
            IsEnemyBody(boardElement)
            || IsEnemyNotDeadHead(boardElement)
            || IsEnemyTail(boardElement);

        public static bool IsNonWalkable(this BoardElement boardElement) =>
            boardElement == BoardElement.StartFloor
            || boardElement == BoardElement.Wall;

        public static bool IsMyBody(this BoardElement boardElement) =>
            boardElement == BoardElement.BodyHorizontal
            || boardElement == BoardElement.BodyLeftDown
            || boardElement == BoardElement.BodyLeftUp
            || boardElement == BoardElement.BodyRightDown
            || boardElement == BoardElement.BodyRightUp
            || boardElement == BoardElement.BodyVertical;

        public static bool IsMyNotDeadHead(this BoardElement boardElement) =>
            boardElement == BoardElement.HeadDown
            || boardElement == BoardElement.HeadLeft
            || boardElement == BoardElement.HeadRight
            || boardElement == BoardElement.HeadUp
            || boardElement == BoardElement.HeadSleep
            || boardElement == BoardElement.HeadEvil;

        public static bool IsMyTail(this BoardElement boardElement) =>
            boardElement == BoardElement.TailEndDown
            || boardElement == BoardElement.TailEndLeft
            || boardElement == BoardElement.TailEndRight
            || boardElement == BoardElement.TailEndUp
            || boardElement == BoardElement.TailInactive;

        public static bool IsEnemyBody(this BoardElement boardElement) =>
            boardElement == BoardElement.EnemyBodyHorizontal
            || boardElement == BoardElement.EnemyBodyLeftDown
            || boardElement == BoardElement.EnemyBodyLeftUp
            || boardElement == BoardElement.EnemyBodyRightDown
            || boardElement == BoardElement.EnemyBodyRightUp
            || boardElement == BoardElement.EnemyBodyVertical;

        public static bool IsEnemyNotDeadHead(this BoardElement boardElement) =>
            boardElement == BoardElement.EnemyHeadDown
            || boardElement == BoardElement.EnemyHeadLeft
            || boardElement == BoardElement.EnemyHeadRight
            || boardElement == BoardElement.EnemyHeadUp
            || boardElement == BoardElement.EnemyHeadEvil
            || boardElement == BoardElement.EnemyHeadSleep;

        public static bool IsEnemyActiveHead(this BoardElement boardElement) =>
            boardElement == BoardElement.EnemyHeadDown
            || boardElement == BoardElement.EnemyHeadLeft
            || boardElement == BoardElement.EnemyHeadRight
            || boardElement == BoardElement.EnemyHeadUp
            || boardElement == BoardElement.EnemyHeadEvil;

        public static bool IsEnemyHead(this BoardElement boardElement) =>
            boardElement == BoardElement.EnemyHeadDown
            || boardElement == BoardElement.EnemyHeadLeft
            || boardElement == BoardElement.EnemyHeadRight
            || boardElement == BoardElement.EnemyHeadUp
            || boardElement == BoardElement.EnemyHeadEvil
            || boardElement == BoardElement.EnemyHeadSleep
            || boardElement == BoardElement.EnemyHeadDead;

        public static bool IsEnemyTail(this BoardElement boardElement) =>
            boardElement == BoardElement.EnemyTailEndDown
            || boardElement == BoardElement.EnemyTailEndLeft
            || boardElement == BoardElement.EnemyTailEndRight
            || boardElement == BoardElement.EnemyTailEndUp
            || boardElement == BoardElement.EnemyTailInactive;

        public static bool IsEnemyActiveTail(this BoardElement boardElement) =>
            boardElement == BoardElement.EnemyTailEndDown
            || boardElement == BoardElement.EnemyTailEndLeft
            || boardElement == BoardElement.EnemyTailEndRight
            || boardElement == BoardElement.EnemyTailEndUp;

        public static bool IsFreeForMove(this BoardElement boardElement) =>
            boardElement == BoardElement.Apple
            || boardElement == BoardElement.EnemyTailEndDown
            || boardElement == BoardElement.EnemyTailEndLeft
            || boardElement == BoardElement.EnemyTailEndRight
            || boardElement == BoardElement.EnemyTailEndUp
            || boardElement == BoardElement.FuryPill
            || boardElement == BoardElement.Gold
            || boardElement == BoardElement.None
            || boardElement == BoardElement.TailEndDown
            || boardElement == BoardElement.TailEndLeft
            || boardElement == BoardElement.TailEndRight
            || boardElement == BoardElement.TailEndUp;
    }
}
