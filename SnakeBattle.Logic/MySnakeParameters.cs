using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Net;
using System.Text;
using System.Threading;
using SnakeBattle.Api;
using SnakeBattle.Logic.Extensions;

namespace SnakeBattle.Logic
{
    public static class MySnakeParameters
    {
        private static int _evilsDuration;
        private static int _length;
        private static int _stonesCount;
        private static readonly List<BoardPoint> _body = new List<BoardPoint>(41);

        public static int EvilsDuration { 
            get => _evilsDuration; 
            private set
            {
                _evilsDuration = Math.Max(0, value);
            }
        }

        public static int Length
        {
            get => _length;
            private set
            {
                _length = Math.Max(0, value);
            }
        }

        public static int StonesCount
        {
            get => _stonesCount;
            private set
            {
                _stonesCount = Math.Max(0, value);
            }
        }

        public static BoardPoint Tail { get; private set; }
        public static BoardPoint Head { get; private set; }

        public static IReadOnlyList<BoardPoint> Body => _body;

        public static BoardPoint GetNeck() => Body[Body.Count - 2];

        public static void Reset() // при смерти или победе
        {
            _evilsDuration = 0;
            _length = 2;
            _body.Clear();
        }

        public static void UpdateBeforeMove(GameBoard gameBoard)
        {
            _body.Clear();

            UpdateTail(gameBoard);
            UpdateHead(gameBoard);
            UpdateBody(gameBoard, Tail, Tail);
        }

        public static void UpdateAfterMove(GameBoard gameBoard, Direction direction, bool act = false)
        {
            BoardElement boardElement = gameBoard.GetElementAt(Head.Shift(direction));

            if (act)
                StonesCount--;

            EvilsDuration--;

            if (boardElement == BoardElement.FuryPill)
            {
                EvilsDuration += GameSettings.EvilPillTimeDuration;
            }

            if (boardElement == BoardElement.Apple)
                Length += GameSettings.AppleLengthBooster;

            if (boardElement == BoardElement.Stone)
            {
                if (EvilsDuration == 0)
                    Length -= GameSettings.StoneLengthReducter;

                StonesCount++;
            }
        }

        private static void UpdateTail(GameBoard gameBoard)
        {
            int size = gameBoard.Size;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    BoardPoint point = new BoardPoint(i, j);
                    var element = gameBoard.GetElementAt(point);

                    if (element.IsMyTail())
                    {
                        Tail = point;
                        return;
                    }
                }
            }

            throw new Exception("Нет хвоста на карте");
        }

        private static void UpdateHead(GameBoard gameBoard)
        {
            var point = gameBoard.GetMyHead();

            if (point == null)
                throw new Exception("Голова null");

            Head = point.Value;
        }

        private static void UpdateBody(GameBoard gameBoard, BoardPoint prevPoint, BoardPoint point)
        {
            var element = gameBoard.GetElementAt(point);
            _body.Add(point);

            if (element.IsMyNotDeadHead())
            {
                return;
            }

            foreach (var dir in Utils.GetOpotDirections(element))
            {
                var nextPoint = point.Shift(dir);
                if (!nextPoint.IsValidPoint(gameBoard.Size))
                    continue;

                if (nextPoint == prevPoint)
                    continue;

                var nextElement = gameBoard.GetElementAt(nextPoint);
                if (Utils.IsExtensionOfMyBody(element, nextElement))
                {           
                    UpdateBody(gameBoard, point, nextPoint);
                    return;
                }
            }
        }
    }
}
