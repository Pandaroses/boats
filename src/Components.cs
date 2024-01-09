using System;

namespace VesselFeud
{
    public class Components
    {
        public static Component StartupBoat()
        {
            Component battleship = new Component();
            battleship.WriteLine("                                     |__");
            battleship.WriteLine("                                     |//");
            battleship.WriteLine("                                     ---");
            battleship.WriteLine("                                     / | [");
            battleship.WriteLine("                              !      | |||");
            battleship.WriteLine("                            _/|     _/|-++'");
            battleship.WriteLine("                        +  +--|    |--|--|_ |-");
            battleship.WriteLine("                     { /|__|  |//__|  |--- |||__/");
            battleship.WriteLine("                    +---------------___[}-_===_.'____                 //");
            battleship.WriteLine("                ____`-' ||___-{]_| _[}-  |     |_[___/==--            //   _");
            battleship.WriteLine(" __..._____--==/___]_|__|_____________________________[___/==--____,------' .7");
            battleship.WriteLine("|                                                                     FLOPPA/");
            battleship.WriteLine(" \\_________________________________________________________________________|");
            battleship.WriteLine("                                   VesselFeuds                                ");

            return battleship;
        }
        public static Component MainMenu()
        {
            Component menu = new Component();
            menu.WriteLine("╭──────────────────╮");
            menu.WriteLine("│     Main Menu    │");
            menu.WriteLine("│   1.start game   │");
            menu.WriteLine("│   2.load game    │");
            menu.WriteLine("│   3.rules        │");
            menu.WriteLine("│   4.quit         │");
            menu.WriteLine("╰──────────────────╯");
            return menu;
        }
        public static Component ChooseMenu()
        {
            Component menu = new Component();
            menu.WriteLine("╭──────────────────╮");
            menu.WriteLine("│       Mode       │");
            menu.WriteLine("│   1.PVP          │");
            menu.WriteLine("│   2.PVE          │");
            menu.WriteLine("│   3.back         │");
            menu.WriteLine("╰──────────────────╯");
            return menu;
        }

        //TODO
        public static Component LoadMenu()
        {
            Component menu = new Component();
            menu.WriteLine("╭──────────────────╮");
            menu.WriteLine("│     Load File    │");
            menu.WriteLine("│                  │");
            menu.WriteLine("╰──────────────────╯");
            return menu;
        }


        public static Component Grid(Tile[,] grid)
        {
            Component res = new Component();
            string[] letters = ["A", "B", "C", "D", "E", "F", "G", "H"];
            res.WriteLine("    1 2 3 4 5 6 7 8  ");
            res.WriteLine("  ┌─────────────────┐");
            for (int x = 0; x < 8; x++)
            {
                res.Write($"{letters[x]} │ ");
                for (int y = 0; y < 8; y++)
                {
                    char supplement = ' ';
                    try {
                    if (grid[x, y] == Tile.h_N | grid[x, y] == Tile.h_S) { supplement = (char)(Tile.h_N); } 
                    } catch {
                        supplement = ' ';
                    }
                    res.Write($"{(char)grid[x, y]}{supplement}");
                }
                res.WriteLine("│");
            }
            res.WriteLine("  └─────────────────┘");
            return res;


        }




    }
}

