using Game.Game;
using Newtonsoft.Json;
using System;

namespace Game
{
    class Enemy : IGameObject
    {
        public const int FacingHorizontal = 1;
        public const int FacingVertical = 2;

        [JsonProperty(PropertyName = "pos")]
        public Point Pos { get; set; }

        [JsonProperty(PropertyName = "cooldown")]
        public int Cooldown { get; set; }

        [JsonProperty(PropertyName = "oldPos")]
        public Point OldPos { get; set; }

        [JsonProperty(PropertyName = "facing")]
        public int Facing { get; set; }

        [JsonProperty(PropertyName = "movingPoint")]
        public Point MovingPoint { get; set; }

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
