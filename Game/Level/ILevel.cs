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

        Point Create();
        Point Restart();
        void Remove();

        void SpawnWalls();
        void SpawnEnemies();
        void SpawnCoins();
        void SpawnTips();

        void RemoveWalls();
        void RemoveEnemies();
        void RemoveCoins();
        void RemoveTips();

        void MoveEnemies(long time);
        bool CheckWallCollision(Point p);
        bool CheckEnemyCollision(Point p);
        bool CheckCoinCollision(Point p);
    }
}
