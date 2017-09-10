using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    static class GameSettings
    {
        public static string Title = "Mrowka the Game";

        public static bool TipsOn = true;
        public static bool MusicOn = true;
        public static bool PolishOn = false;

        public static char PlayerAvatar = 'm';
        public static char EnemyAvatar = '*';
        public static char ObstacleAvatar = '#';
        public static char CoinAvatar = '$';
        public static char FinishAvatar = '>';

        public static ConsoleColor CompanyNameColor = ConsoleColor.Magenta;
        public static ConsoleColor[] SplashColors = {
            ConsoleColor.Red,
            ConsoleColor.Yellow,
            ConsoleColor.Green,
            ConsoleColor.Cyan,
            ConsoleColor.Blue,
            ConsoleColor.Magenta
        };
        public static ConsoleColor PlayerColor = ConsoleColor.Green;
        public static ConsoleColor EnemyColor = ConsoleColor.Red;
        public static ConsoleColor ObstacleColor = ConsoleColor.White;
        public static ConsoleColor CoinColor = ConsoleColor.Yellow;
        public static ConsoleColor FinishColor = ConsoleColor.Magenta;
        public static ConsoleColor TipsColor = ConsoleColor.Cyan;

        public static ConsoleColor CompanyNameBackground = ConsoleColor.White;
        public static ConsoleColor Background = ConsoleColor.Black;
    }
}
