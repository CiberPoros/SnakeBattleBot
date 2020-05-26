using System;
using System.Reflection.Metadata.Ecma335;

namespace SnakeBattle.Api
{
    public class SnakeAction
    {
        private const string ACT_BEFORE_PREFIX = "ACT,";
        private const string ACT_AFTER_POSTFIX = ",ACT";

        private readonly bool _actBefore;
        private readonly bool _actAfter;
        private readonly bool _dead;
        private readonly Direction _direction;

        public SnakeAction(bool actBefore, bool actAfter, Direction direction, bool dead = false)
        {
            if (actBefore && actAfter)
                throw new ArgumentException();

            _dead = dead;
            _actBefore = actBefore;
            _actAfter = actAfter;
            _direction = direction;
        }

        public override string ToString()
        {
            if (_dead)
                return "ACT(0)";
            else
                return $"{ (_actBefore ? ACT_BEFORE_PREFIX : string.Empty) }" +
                       $"{ _direction }" +
                       $"{ (_actAfter ? ACT_AFTER_POSTFIX : string.Empty) }";
        }
    }
}
