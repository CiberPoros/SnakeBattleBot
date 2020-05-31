using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SnakeBattle.Api;

namespace SnakeBattle.Logic
{
    public static class Utils
    {
        public static bool IsExtensionOfMyBody(BoardElement prevElement, BoardElement boardElement) =>
            prevElement switch
            {
                BoardElement.TailEndDown => _upExtensions.Contains(boardElement),
                BoardElement.TailEndLeft => _rightExtensions.Contains(boardElement),
                BoardElement.TailEndRight => _leftExtensions.Contains(boardElement),
                BoardElement.TailEndUp => _downExtensions.Contains(boardElement),
                BoardElement.BodyHorizontal => _leftExtensions.Contains(boardElement) || _rightExtensions.Contains(boardElement),
                BoardElement.BodyLeftDown => _leftExtensions.Contains(boardElement) || _downExtensions.Contains(boardElement),
                BoardElement.BodyLeftUp => _leftExtensions.Contains(boardElement) || _upExtensions.Contains(boardElement),
                BoardElement.BodyRightDown => _downExtensions.Contains(boardElement) || _rightExtensions.Contains(boardElement),
                BoardElement.BodyRightUp => _rightExtensions.Contains(boardElement) || _upExtensions.Contains(boardElement),
                BoardElement.BodyVertical => _upExtensions.Contains(boardElement) || _downExtensions.Contains(boardElement),
                _ => false,
            };

        public static bool IsExtensionOfEnemyBody(BoardElement prevElement, BoardElement boardElement) =>
            prevElement switch
            {
                BoardElement.EnemyTailEndDown => _upEnemyExtensions.Contains(boardElement),
                BoardElement.EnemyTailEndLeft => _rightEnemyExtensions.Contains(boardElement),
                BoardElement.EnemyTailEndRight => _leftEnemyExtensions.Contains(boardElement),
                BoardElement.EnemyTailEndUp => _downEnemyExtensions.Contains(boardElement),
                BoardElement.EnemyBodyHorizontal => _leftEnemyExtensions.Contains(boardElement) || _rightEnemyExtensions.Contains(boardElement),
                BoardElement.EnemyBodyLeftDown => _leftEnemyExtensions.Contains(boardElement) || _downEnemyExtensions.Contains(boardElement),
                BoardElement.EnemyBodyLeftUp => _leftEnemyExtensions.Contains(boardElement) || _upEnemyExtensions.Contains(boardElement),
                BoardElement.EnemyBodyRightDown => _downEnemyExtensions.Contains(boardElement) || _rightEnemyExtensions.Contains(boardElement),
                BoardElement.EnemyBodyRightUp => _rightEnemyExtensions.Contains(boardElement) || _upEnemyExtensions.Contains(boardElement),
                BoardElement.EnemyBodyVertical => _upEnemyExtensions.Contains(boardElement) || _downEnemyExtensions.Contains(boardElement),
                _ => false, // TODO: посмотреть, как сюда вообще попадает
            };

        public static Direction[] GetOpotDirections(BoardElement boardElement) =>
            boardElement switch
            {
                BoardElement.TailEndDown => new Direction[] { Direction.Up },
                BoardElement.TailEndLeft => new Direction[] { Direction.Right },
                BoardElement.TailEndRight => new Direction[] { Direction.Left },
                BoardElement.TailEndUp => new Direction[] { Direction.Down },
                BoardElement.BodyHorizontal => new Direction[] { Direction.Left, Direction.Right },
                BoardElement.BodyLeftDown => new Direction[] { Direction.Left, Direction.Down },
                BoardElement.BodyLeftUp => new Direction[] { Direction.Left, Direction.Up },
                BoardElement.BodyRightDown => new Direction[] { Direction.Right, Direction.Down },
                BoardElement.BodyRightUp => new Direction[] { Direction.Right, Direction.Up },
                BoardElement.BodyVertical => new Direction[] { Direction.Up, Direction.Down },
                _ => new Direction[] { },
            };

        public static Direction[] GetEnemyOpotDirections(BoardElement boardElement) =>
            boardElement switch
            {
                BoardElement.EnemyTailEndDown => new Direction[] { Direction.Up },
                BoardElement.EnemyTailEndLeft => new Direction[] { Direction.Right },
                BoardElement.EnemyTailEndRight => new Direction[] { Direction.Left },
                BoardElement.EnemyTailEndUp => new Direction[] { Direction.Down },
                BoardElement.EnemyBodyHorizontal => new Direction[] { Direction.Left, Direction.Right },
                BoardElement.EnemyBodyLeftDown => new Direction[] { Direction.Left, Direction.Down },
                BoardElement.EnemyBodyLeftUp => new Direction[] { Direction.Left, Direction.Up },
                BoardElement.EnemyBodyRightDown => new Direction[] { Direction.Right, Direction.Down },
                BoardElement.EnemyBodyRightUp => new Direction[] { Direction.Right, Direction.Up },
                BoardElement.EnemyBodyVertical => new Direction[] { Direction.Up, Direction.Down },
                _ => new Direction[] { },
            };

        #region MY_AND_ENEMY_EXTENSIONS
        private static readonly BoardElement[] _rightExtensions = new BoardElement[]
        {
            BoardElement.BodyHorizontal,
            BoardElement.BodyLeftDown,
            BoardElement.BodyLeftUp,
            BoardElement.HeadEvil,
            BoardElement.HeadRight,
        };

        private static readonly BoardElement[] _leftExtensions = new BoardElement[]
        {
            BoardElement.BodyHorizontal,
            BoardElement.BodyRightDown,
            BoardElement.BodyRightUp,
            BoardElement.HeadEvil,
            BoardElement.HeadLeft,
        };

        private static readonly BoardElement[] _upExtensions = new BoardElement[]
        {
            BoardElement.BodyVertical,
            BoardElement.BodyLeftDown,
            BoardElement.BodyRightDown,
            BoardElement.HeadEvil,
            BoardElement.HeadUp,
        };

        private static readonly BoardElement[] _downExtensions = new BoardElement[]
        {
            BoardElement.BodyVertical,
            BoardElement.BodyLeftUp,
            BoardElement.BodyRightUp,
            BoardElement.HeadEvil,
            BoardElement.HeadDown,
        };

        private static readonly BoardElement[] _rightEnemyExtensions = new BoardElement[]
        {
            BoardElement.EnemyBodyHorizontal,
            BoardElement.EnemyBodyLeftDown,
            BoardElement.EnemyBodyLeftUp,
            BoardElement.EnemyHeadDead,
            BoardElement.EnemyHeadEvil,
            BoardElement.EnemyHeadRight,
        };

        private static readonly BoardElement[] _leftEnemyExtensions = new BoardElement[]
        {
            BoardElement.EnemyBodyHorizontal,
            BoardElement.EnemyBodyRightDown,
            BoardElement.EnemyBodyRightUp,
            BoardElement.EnemyHeadDead,
            BoardElement.EnemyHeadEvil,
            BoardElement.EnemyHeadLeft,
        };

        private static readonly BoardElement[] _upEnemyExtensions = new BoardElement[]
        {
            BoardElement.EnemyBodyVertical,
            BoardElement.EnemyBodyLeftDown,
            BoardElement.EnemyBodyRightDown,
            BoardElement.EnemyHeadDead,
            BoardElement.EnemyHeadEvil,
            BoardElement.EnemyHeadUp,
        };

        private static readonly BoardElement[] _downEnemyExtensions = new BoardElement[]
        {
            BoardElement.EnemyBodyVertical,
            BoardElement.EnemyBodyLeftUp,
            BoardElement.EnemyBodyRightUp,
            BoardElement.EnemyHeadDead,
            BoardElement.EnemyHeadEvil,
            BoardElement.EnemyHeadDown,
        };
        #endregion
    }
}
