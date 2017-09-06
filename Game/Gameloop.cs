using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    static class Gameloop
    {
        private static Player Player;

        public static void Start()
        {
            Console.CursorVisible = false;
            Player = Level.CreateLevel();
            ConsoleKeyInfo keyInfo;

            while((keyInfo = Console.ReadKey(true)).Key != ConsoleKey.Escape)
            {
                switch(keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        Player.Move(new Point(0, -1));
                        break;
                    case ConsoleKey.DownArrow:
                        Player.Move(new Point(0, 1));
                        break;
                    case ConsoleKey.LeftArrow:
                        Player.Move(new Point(-1, 0));
                        break;
                    case ConsoleKey.RightArrow:
                        Player.Move(new Point(1, 0));
                        break;
                }
            }
        }
    }
}
