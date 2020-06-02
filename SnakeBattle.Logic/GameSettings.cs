using System;
using System.Collections.Generic;
using System.Text;
using SnakeBattle.Api;
using System.Configuration;

namespace SnakeBattle.Logic
{
    public static class GameSettings
    {
        public static long MultyWeight = 8 * 9 * 10 * 11 * 12 * 13 * 14 * 15;
        public static bool MakeLog = false;
        public static bool CheckCollisions = true;

        public static long AppleWeight = 10; // поиграться с этим
        public static long EvilPillWeight = 18 * 8 * 9 * 10 * 11 * 12 * 13 * 14; // поиграться с этим
        public static long CoinWeight = 8 * 8 * 9 * 10 * 11 * 12 * 13 * 14; // очки за монету
        public static long StoneWeight = 3 * 8 * 9 * 10 * 11 * 12 * 13 * 14; // очки за камень
        public static long EnemyPartOfBodyWeight = 10 * 8 * 9 * 10 * 11 * 12 * 13 * 14; // по 10 очков за каждый кусман вражины
        public static long EvilWeightReducter = 1 * 8 * 9 * 10 * 11 * 12 * 13 * 14; 

        public static int StoneLengthReducter = 3; // коэфф уменьшения длины змеи за камень без спидов
        public static int AppleLengthBooster = 1; // коэфф увеличения длины змеи за яблоко
        public static int EvilPillTimeDuration = 10; // время действия одного спида
        public static int EvilTimeDurationForReductEvilWeight = 15;

        public static int DeepReducterCoeff = 10;
        public static int DeepLimit = 13;

        public static readonly Direction[] Directions = new[] 
        { 
            Direction.Down, 
            Direction.Left, 
            Direction.Right, 
            Direction.Up 
        }; 

        public static void UpdateSettings()
        {
            ConfigurationManager.RefreshSection("appSettings");

            MultyWeight = long.Parse(ConfigurationManager.AppSettings.Get("MultyWeight"));
            MakeLog = bool.Parse(ConfigurationManager.AppSettings.Get("MakeLog"));
            CheckCollisions = bool.Parse(ConfigurationManager.AppSettings.Get("CheckCollisions"));

            AppleWeight = long.Parse(ConfigurationManager.AppSettings.Get("AppleWeight")) * MultyWeight;
            EvilPillWeight = long.Parse(ConfigurationManager.AppSettings.Get("EvilPillWeight")) * MultyWeight;
            CoinWeight = long.Parse(ConfigurationManager.AppSettings.Get("CoinWeight")) * MultyWeight;
            StoneWeight = long.Parse(ConfigurationManager.AppSettings.Get("StoneWeight")) * MultyWeight;
            EnemyPartOfBodyWeight = long.Parse(ConfigurationManager.AppSettings.Get("EnemyPartOfBodyWeight")) * MultyWeight;
            EvilWeightReducter = long.Parse(ConfigurationManager.AppSettings.Get("EvilWeightReducter")) * MultyWeight;

            StoneLengthReducter = int.Parse(ConfigurationManager.AppSettings.Get("StoneLengthReducter"));
            AppleLengthBooster = int.Parse(ConfigurationManager.AppSettings.Get("AppleLengthBooster"));
            EvilPillTimeDuration = int.Parse(ConfigurationManager.AppSettings.Get("EvilPillTimeDuration"));
            EvilTimeDurationForReductEvilWeight = int.Parse(ConfigurationManager.AppSettings.Get("EvilTimeDurationForReductEvilWeight"));

            DeepReducterCoeff = int.Parse(ConfigurationManager.AppSettings.Get("DeepReducterCoeff"));
            DeepLimit = int.Parse(ConfigurationManager.AppSettings.Get("DeepLimit"));
        }
    }
}
