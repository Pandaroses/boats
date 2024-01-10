namespace VesselFeud {

    public class Game {
        public Ship[] ships = { new Ship("balls", 1, 4) };
        public path = ""
        public Player player1;
        public Player player2;
        //TODO MAKE CYCLE

        
        public void PvP() {
            this.Serialize(path);
        }
        public void PvE() {
            this.Serialize(path);
        }
        public void EvE() {
            this.Serialize(path);
        }
        public void init() {
            Component boat = new Components.StartupBoat();
            Component current = new Components.MainMenu();
            Console.Clear();

            boat.draw(MIDDLEX - boat.dimensions.Item1, MIDDLEY - boat.dimensions.Item2);
            System.Threading.Thread.Sleep(5000);

            while (true) {
                current.draw(MIDDLEX - current.dimensions.Item1, MIDDLEY - current.dimensions.item2);
                var key = Console.ReadKey();
                switch (key.KeyChar) {
                    case '1':
                        current = Components.ChooseMenu();
                        current.draw(MIDDLEX - current.dimensions.Item1, MIDDLEY - current.dimensions.Item2);
                        key = Console.ReadKey();
                        switch (key.KeyChar) {
                            case '1':
                                PVP();
                                break;
                            case '2':
                                PVE();
                                break;
                            case '3':
                                EVE();
                                break;

                        }
                        break;

                    case '2':
                        current = Components.LoadMenu();
                        while (true) {
                            current.draw(MIDDLEX - current.dimensions.Item1, MIDDLEY - current.dimensions.Item2);
                            var meow = Console.ReadLine();
                            this.Deserialize(meow);
                        }
                        break;
                    default: 
                        break;

                }
            }

        }

        struct SaveFile {
            public Tile[,] p1pgrid;
            public Tile[,] p1egrid;

            public Tile[,] p2pgrid;
            public Tile[,] p2egrid;

            //true player 1 false player 2
            public string type;
        }


        public bool Deserialize(string path) {
            string json = File.ReadAllText(path);
            SaveFile save = JsonSerializer.Deserialize<SaveFile>(json);
            Player p1 = new Human();
            Player p2 = new Human();
            if (save.type == "pve") {
                p1 = new Human();
                p2 = new Robot();
            } else if (save.type == "eve") {
                p1 = new Robot();
                p2 = new Robot();
            }
            p1.pGrid = save.p1pgrid;
            p1.eGrid = save.p1egrid;
            p2.pGrid = save.p2pgrid;
            p2.eGrid = save.p2egrid;
            return true;

        }
        public bool Serialize( string type) {
            SaveFile save = new SaveFile {
                p1pgrid = player1.pGrid,
                p1egrid = player1.eGrid,
                p2pgrid = player2.pGrid,
                p2egrid = player2.eGrid,

                type,
                turn,
            };
            try {
                var meow = JsonSerializer.Serialize(save);
                File.Create("./bb.json");
                File.WriteAllText("./bb.json", String.Empty());
                File.WriteAllText("./bb.json", meow);
                return true;

            }
            catch {
                return false;
            }
            
        }

    }

    public class Human : Player {


        public override (int, int) Turn() {
            (int, int, bool) test = (0, 0, false);
            while (test.Item3 != true) {
                Console.Clear();
                render_grids();
                test = format(Console.ReadLine());
            }
            return (test.Item1, test.Item2);


        }
        public override void place_ships(Ship[] ships) {
            Component grid = Components.Grid(pGrid);
            (int, int, bool) test = (0, 0, false);
            bool verify = false;
            foreach (Ship ship in ships) {
                for (int i = 0; i < ship.amount; i++) {
                    test = (0, 0, false);
                    while (test.Item3 != true && verify != true) {
                        Console.Clear();
                        grid.draw(20, 20);
                        Console.WriteLine($"Placing {ship.name}");
                        Console.Write("Enter Coordinates:");
                        test = format(Console.ReadLine());
                        Console.Write("Enter orientation h/v");
                        var orient = Console.ReadKey().KeyChar == 'h' ? true : false;
                        verify = verify_placement(test.Item1, test.Item2, ship, orient);
                    }

                }
            }

        }

    }


    //TODO make exist :3
    public class Robot : Player {
        public override (int, int) Turn() {
            throw new NotImplementedException();
        }
        public override void place_ships(Ship[] ships) {
            throw new NotImplementedException();
        }
    }


}
