using System;

namespace VesselFeud {
    public class Components {

    
        public static Component StartupBoat() {
            Component battleship = new Component();
            battleship.wl("                                     |__");
            battleship.wl("                                     |//");
            battleship.wl("                                     ---");
            battleship.wl("                                     / | [");
            battleship.wl("                              !      | |||");
            battleship.wl("                            _/|     _/|-++'");
            battleship.wl("                        +  +--|    |--|--|_ |-");
            battleship.wl("                     { /|__|  |//__|  |--- |||__/");
            battleship.wl("                    +---------------___[}-_===_.'____                 //");
            battleship.wl("                ____`-' ||___-{]_| _[}-  |     |_[___/==--            //   _");
            battleship.wl(" __..._____--==/___]_|__|_____________________________[___/==--____,------' .7");
            battleship.wl("|                                                                 HMS FLOPPA/");
            battleship.wl(" \\_________________________________________________________________________|");
            battleship.wl("                                   VesselFeuds                                ");

            return battleship;
        }
        public static Component MainMenu() {
            Component menu = new Component();
            menu.wl("╭──────────────────╮");
            menu.wl("│     Main Menu    │");
            menu.wl("│   1.start game   │");
            menu.wl("│   2.load game    │");
            menu.wl("│   3.rules        │");
            menu.wl("│   4.quit         │");
            menu.wl("╰──────────────────╯");
            return menu;
        }
        public static Component ChooseMenu() {
            Component menu = new Component();
            menu.wl("╭──────────────────╮");
            menu.wl("│       Mode       │");
            menu.wl("│   1.PVP          │");
            menu.wl("│   2.PVE          │");
            menu.wl("│   3.EVE          │");
            menu.wl("╰──────────────────╯");
            return menu;
        }

        //TODO make loading work
        public static Component LoadMenu() {
            Component menu = new Component();
            menu.wl("╭──────────────────╮");
            menu.wl("│     Load File    │");
            menu.wl("│     enter path   │");
            menu.wl("╰──────────────────╯");
            menu.w("Path:");
            return menu;
        }


        public static Component Grid(Tile[,] grid) {
            Component res = new Component();
            string[] letters = ["A", "B", "C", "D", "E", "F", "G", "H"];
            res.wl("    1 2 3 4 5 6 7 8  ");
            res.wl("  ┌─────────────────┐");
            for (int y = 0; y < 8; y++) {
                res.w($"{letters[y]} │ ");
                for (int x = 0; x < 8; x++) {
                    char supplement = ' ';
                    try {
                        if (grid[x, y] == Tile.h_N | grid[x, y] == Tile.h_S) { supplement = (char)(Tile.h_N); }
                    }
                    catch {
                        supplement = ' ';
                    }
                    res.w($"{(char)grid[x, y]}{supplement}");
                }
                res.wl("│");
            }
            res.wl("  └─────────────────┘");
            return res;


        }


        //TODO ADD Limbo saying, player 2's turn , press any key to contiinue
        public static Component Limbo(string player) {
            Component res = new Component();
            Console.Clear();
            res.wl($"{player}     ", FgColours.Red);
            res.wl("press any key to continue");
            return res;


        }




    }
}

