using System.Numerics;

namespace virtual_pet.Core.GameEngine.Tilemap
{
    public class TileMap
    {
        public Vector2 Center { get; set; }
        public Vector2 Size { get; set; }

        public TileInfo[,] Map { get; set; }



        public TileInfo this[int x, int y]
        {
            get
            {
                return Map[x, y];
            }
            set
            {
                Map[x, y] = value;
            }

        }
        public TileInfo this[Vector2 point]
        {
            get
            {
                return Map[(int)point.X, (int)point.Y];
            }
            set
            {
                Map[(int)point.X, (int)point.Y] = value;
            }

        }
    }
}
