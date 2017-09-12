using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Level
{
    interface ILevel
    {
        int GetNumber();
        void ShowSplash();

        Point Create();
        Point Restart();
        void Paint();
        void Remove();

        void SpawnWalls();
        void SpawnEnemies();
        void SpawnCoins();
        void SpawnTips();
        void SpawnFinish();

        void RemoveWalls();
        void RemoveEnemies();
        void RemoveCoins();
        void RemoveTips();
        void RemoveFinish();

        void MoveEnemies(long time);
        bool CheckWallCollision(Point p);
        bool CheckEnemyCollision(Point p);
        bool CheckCoinCollision(Point p);
        bool CheckFinishCollision(Point p);
    }
}
