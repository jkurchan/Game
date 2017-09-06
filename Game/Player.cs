using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Player
    {
        public Point Position { get; set; }
        public Point OldPosition { get; set; }

        public Player(Point position)
        {
            Position = position;
            OldPosition = position;
            paint();
        }

        public void Move(Point c)
        {
            Point newPosition = new Point(c.PosX + Position.PosX, c.PosY + Position.PosY);
            if (GameRule.CanDraw(newPosition))
            {
                OldPosition = Position;
                Position = newPosition;
                paint();
            }
        }

        public void MoveToPosition(Point c)
        {
            Point newPosition = new Point(c.PosX + Position.PosX, c.PosY + Position.PosY);
            if (GameRule.CanDraw(newPosition))
            {
                OldPosition.PosX = Position.PosX;
                OldPosition.PosY = Position.PosY;
                Position.PosX = c.PosX;
                Position.PosY = c.PosY;
                paint();
            }
        }

        private void paint()
        {
            Console.SetCursorPosition(OldPosition.PosX, OldPosition.PosY);
            Console.Write(' ');
            Console.SetCursorPosition(Position.PosX, Position.PosY);
            Console.Write(GameSettings.PlayerAvatar);
        }
    }
}
