using System;

namespace Game.Util
{
    class MenuItem
    {
        public string Text { get; set; }
        public Point Pos { get; set; }
        public delegate int ClickEventHandler();
        public event ClickEventHandler OnClick;

        public MenuItem(string text)
        {
            Text = text;
        }

        public void Select()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(Pos.X, Pos.Y);
            Console.Write(Text);
        }

        public void Deselect()
        {
            Console.ForegroundColor = GameSettings.MenuColor;
            Console.SetCursorPosition(Pos.X, Pos.Y);
            Console.Write(Text);
        }

        public int Click()
        {
            return OnClick();
        }
    }
}
