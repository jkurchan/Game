using Newtonsoft.Json;

namespace Game
{
    public class Point
    {
        [JsonProperty("x")]
        public int X { get; set; }

        [JsonProperty("y")]
        public int Y { get; set; }

        public Point() { }

        public Point(int posX, int posY)
        {
            X = posX;
            Y = posY;
        }
    }
}
