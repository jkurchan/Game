using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.GameObjects
{
    class TextTip
    {
        public static int SideUp = 1;
        public static int SideDown = 2;

        public string Text { get; set; }
        public Point TextPos { get; set; }
        public int ArrowPos { get; set; }
        public int ArrowSide { get; set; }
        public bool ShowArrow { get; set; }

        public TextTip(string text, Point p, int arrowPos, int arrowSide, bool showArrow)
        {
            Text = text;
            TextPos = p;
            ArrowPos = arrowPos;
            ArrowSide = arrowSide;
            ShowArrow = showArrow;
        }

        public void Paint()
        {
            Console.ForegroundColor = GameSettings.TipsColor;
            Console.SetCursorPosition(TextPos.X, TextPos.Y);
            Console.Write(Text);

            if (!ShowArrow) return;

            if (ArrowSide == SideUp)
            {
                Console.SetCursorPosition(TextPos.X + ArrowPos, TextPos.Y - 1);
                Console.Write("^");
            }
            else
            {
                Console.SetCursorPosition(TextPos.X + ArrowPos, TextPos.Y + 1);
                Console.Write("V");
            }
        }

        public void Remove()
        {
            for (int i = TextPos.X; i < TextPos.X + Text.Length; i++)
            {
                Console.SetCursorPosition(i, TextPos.Y);
                Console.Write(' ');
            }

            if (!ShowArrow) return;

            if (ArrowSide == SideUp)
            {
                Console.SetCursorPosition(TextPos.X + ArrowPos, TextPos.Y - 1);
                Console.Write(" ");
            }
            else
            {
                Console.SetCursorPosition(TextPos.X + ArrowPos, TextPos.Y + 1);
                Console.Write(" ");
            }
        }
    }
}
