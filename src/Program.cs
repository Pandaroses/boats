using System;

namespace VesselFeud
{
    internal class Program
    {
        public static int MIDDLEX = Console.WindowWidth / 2;
        public static int MIDDLEY = Console.WindowHeight / 2;
        static void Main(string[] args)
        {
            Console.Clear();
            Tile[,] meow = new Tile[8, 8];
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    meow[x, y] = Tile.Empty;
                }
            }

            meow[2, 4] = Tile.v_S;
            meow[3, 4] = Tile.v_N;
            meow[4, 4] = Tile.v_N;
            meow[5, 4] = Tile.v_E;

             
            meow[1, 2] = Tile.h_S;
            meow[1, 3] = Tile.h_N;
            meow[1, 4] = Tile.h_N;
            meow[1, 5] = Tile.h_E;

            Component mrrp = Components.Grid(meow);
            mrrp.Render(MIDDLEX, MIDDLEY);

        }


    }
}
