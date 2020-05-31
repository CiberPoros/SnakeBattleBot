using System;
using System.Collections.Generic;
using System.Text;
using SnakeBattle.Api;

namespace SnakeBattle.Logic
{
    public static class GameSettings
    {
        public const bool MakeLog = false;
        public const long AppleWeight = 10 * 8 * 9 * 10 * 11 * 12 * 13 * 14; // поиграться с этим
        public const long EvilPillWeight = 18 * 8 * 9 * 10 * 11 * 12 * 13 * 14; // поиграться с этим
        public const long CoinWeight = 8 * 8 * 9 * 10 * 11 * 12 * 13 * 14; // очки за монету
        public const long StoneWeight = 3 * 8 * 9 * 10 * 11 * 12 * 13 * 14; // очки за камень
        public const long EnemyPartOfBodyWeight = 10 * 8 * 9 * 10 * 11 * 12 * 13 * 14; // по 10 очков за каждый кусман вражины
        public const int StoneLengthReducter = 3; // коэфф уменьшения длины змеи за камень без спидов
        public const int AppleLengthBooster = 1; // коэфф увеличения длины змеи за яблоко
        public const int EvilPillTimeDuration = 10; // время действия одного спида

        public const long EvilWeightReducter = 1 * 8 * 9 * 10 * 11 * 12 * 13 * 14;
        public const int EvilTimeDurationForReductEvilWeight = 15;

        public static readonly Direction[] Directions = new[] 
        { 
            Direction.Down, 
            Direction.Left, 
            Direction.Right, 
            Direction.Up 
        }; 
    }
}
