using System;

namespace Game.Util
{
    class MenuItem
    {
        public string Text { get; set; }
        public Point Pos { get; set; }
        public ConsoleColor SelectedColor { get; set; }
        public ConsoleColor UnselectedColor { get; set; }
        public delegate int ClickEventHandler();
        public event ClickEventHandler OnClick;

        public MenuItem(string text, ConsoleColor selected, ConsoleColor unselected)
        {
            Text = text;
            SelectedColor = selected;
            UnselectedColor = unselected;
        }

        public void Select()
        {
            Console.ForegroundColor = SelectedColor;
            Console.SetCursorPosition(Pos.X, Pos.Y);
            Console.Write(Text);
        }

        public void Deselect()
        {
            Console.ForegroundColor = UnselectedColor;
            Console.SetCursorPosition(Pos.X, Pos.Y);
            Console.Write(Text);
        }

        public int Click()
        {
            return OnClick();
        }
    }
}
