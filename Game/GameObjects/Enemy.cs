using Game.Game;
using Newtonsoft.Json;
using System;

namespace Game
{
    public class Enemy : IGameObject
    {
        public const int FacingHorizontal = 1;
        public const int FacingVertical = 2;
        
        public long Facing { get; set; }
        public Point OldPos { get; set; }
        public long Cooldown { get; set; }
        public Point MovingPoint { get; set; }
        public Point Pos { get; set; }

        public Enemy() { }

        public Enemy(Point p, int facing)
        {
            Pos = p;
            OldPos = p;
            Cooldown = 3;

            this.Facing = facing;
            if (facing == FacingHorizontal)
                MovingPoint = new Point(1, 0);
            else
                MovingPoint = new Point(0, 1);
        }

        public void Move(Point p, Level level, long time)
        {
            if (time % Cooldown != 0) return;

            Point newPos = new Point(Pos.X + MovingPoint.X, Pos.Y + MovingPoint.Y);
            if (GameRule.CanDraw(newPos, level))
            {
                OldPos = Pos;
                Pos = newPos;
                Paint();
            }
            else
            {
                if (Facing == FacingHorizontal)
                    MovingPoint.X = MovingPoint.X == 1 ? -1 : 1;
                else
                    MovingPoint.Y = MovingPoint.Y == 1 ? -1 : 1;

                if (GameRule.CanDraw(newPos, level))
                {
                    OldPos = Pos;
                    Pos = newPos;
                    Paint();
                }
            }
        }

        public void MoveToPosition(Point p, Level level)
        {
            if (GameRule.CanDraw(p, level))
            {
                OldPos = Pos;
                Pos = p;
                Paint();
            }
        }

        public bool IsOccupying(Point p)
        {
            if (Pos.X == p.X && Pos.Y == p.Y) return true;
            return false;
        }

        public void Paint()
        {
            Console.SetCursorPosition(OldPos.X, OldPos.Y);
            Console.Write(' ');
            Console.SetCursorPosition(Pos.X, Pos.Y);
            Console.ForegroundColor = GameSettings.EnemyColor;
            Console.Write(GameSettings.EnemyAvatar);
        }

        public void Remove()
        {
            Console.SetCursorPosition(Pos.X, Pos.Y);
            Console.Write(' ');
        }
    }
}
