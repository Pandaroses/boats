using System.Text.Json;

namespace VesselFeud {

    public enum Tile {
        // BASIC stuff
        Hit = 'X',
        Missed = 'O',
        Empty = '~',
        //Ship rendering characters: syntax is x_Y where x = orientation, bar c which is center , y = position(start piece,end piece example, v_S is the start piece of a vertical ship, IM SORRY
        v_S = 'A',
        v_E = 'V',

        h_S = '<',
        h_E = '>',

        v_N = '‖',
        h_N = '=',
    }

    public struct Ship {
        public string name;
        public int amount;
        public int length;

        public Ship(string name, int amount, int length) {
            this.name = name;
            this.amount = amount;
            this.length = length;
        }
    }

    public abstract class Player {

        private static Dictionary<char, int> letters = new Dictionary<char, int>{
                {'a',0},
                {'b',1},
                {'c',2},
                {'d',3},
                {'e',4},
                {'f',5},
                {'g',6},
                {'h',7},
            };

        public Tile[,] pGrid = new Tile[8, 8];
        public Tile[,] eGrid = new Tile[8, 8];

        public void init_grid() {
            for (int y = 0; y < 8; y++) {
                for (int x = 0; x < 8; x++) {
                    pGrid[x, y] = Tile.Empty;
                    eGrid[x, y] = Tile.Empty;
                }
            }
        }

        public bool Attack(int x, int y) {
            if (!(pGrid[x, y] == Tile.Hit | pGrid[x, y] == Tile.Empty | pGrid[x, y] == Tile.Missed)) {
                pGrid[x, y] = Tile.Hit;
                return true;
            } else {
                pGrid[x, y] = Tile.Missed;
                return false;
            }
        }
        public void result(bool success, int x, int y) {
            if (success) {
                eGrid[x, y] = Tile.Hit;

            } else {
                eGrid[x, y] = Tile.Missed;
            }
        }
        public abstract (int, int) Turn();

        public bool hasLost() {
            return !pGrid.Cast<Tile>().Any(tile => (tile != Tile.Hit && tile != Tile.Missed && tile != Tile.Empty));
        }

        public abstract void place_ships(Ship[] ships);


        public (int, int, bool) format(string input) {
            switch (input.Length) {
                case < 2:
                    return (-1, 0, false);
                case > 2:
                    return (0, -1, false);
                case 2:
                    int number;
                    char letter;
                    if (Char.IsNumber(input[0]) && Char.IsLetter(input[1])) {
                        number = Convert.ToInt32(input[0].ToString());
                        letter = Char.ToLower(input[1]);
                    } else if (Char.IsNumber(input[1]) && Char.IsLetter(input[0])) {
                        letter = Char.ToLower(input[0]);
                        number = Convert.ToInt32(input[1].ToString());
                    } else break;
                    if (number >= 1 && number <= 8 && letters.ContainsKey(letter))
                        return (number - 1, letters[letter], true);
                    else break;
            }
            return (-1, -1, false);
        }

        public void render_grids() {
            //TODO MAKE PRETTY
            Component p_comp = Components.Grid(this.pGrid);
            Component e_comp = Components.Grid(this.eGrid);
            p_comp.draw(50, 10);
            e_comp.draw(100, 10);
        }

        public bool verify_placement(int x, int y, Ship ship, bool h) {
            if (h && x + ship.length-1 > 7) { return false; } else if (y + ship.length-1 > 7) { return false; }
            for (int i = 0; i < ship.length; i++) {
                if (h) {
                    if (pGrid[x + i, y] != Tile.Empty) {
                        return false;
                    }
                } else {
                    if (pGrid[x, y + i] != Tile.Empty) {
                        return false;
                    }
                }
            }

            for (int i = 0; i < ship.length; i++) {
                if (h) {
                    Tile tile = Tile.h_N;
                    if (i == 0) tile = Tile.h_S;
                    else if (i == ship.length - 1) tile = Tile.h_E;
                    pGrid[x + i, y] = tile;
                } else {
                    Tile tile = Tile.v_N;
                    if (i == 0) tile = Tile.v_S;
                    else if (i == ship.length - 1) tile = Tile.v_E;
                    pGrid[x, y + i] = tile;

                }
            }
            return true;
        }

    }


}
