using Game.Game;

namespace Game
{
    interface IGameObject
    {
        void Move(Point p, Level level, long time);
        void MoveToPosition(Point p, Level level);
        bool IsOccupying(Point p);
        void Paint();
        void Remove();
    }
}
