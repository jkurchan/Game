using Game.GameObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Game
{
    class Level
    {
        [JsonProperty]
        List<Wall> walls;

        [JsonProperty]
        List<Enemy> enemies;

        [JsonProperty]
        List<Coin> coins;

        [JsonProperty]
        Player player;

        [JsonProperty]
        List<Finish> finishes;

        public Level(List<Wall> walls, List<Enemy> enemies, List<Coin> coins, Player player, List<Finish> finishes)
        {
            this.walls = walls;
            this.enemies = enemies;
            this.coins = coins;
            this.player = player;
            this.finishes = finishes;
        }
    }
}
