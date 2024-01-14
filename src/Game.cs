using Newtonsoft.Json;
namespace VesselFeud {

    public class Game {
        public  static int MIDDLEX = Console.WindowWidth / 2;
        public static int MIDDLEY = Console.WindowHeight / 2;
        public Ship[] ships;
        public string path = "";
        public Player? player1;
        public Player? player2;
        //TODO MAKE CYCLE

        public Game() {
            // this.ships = [new Ship("Dick", 2, 4), new Ship("Carrier",1, 5), new Ship("Cruiser", 3, 2) ];
            this.ships = [new Ship("dick",1,4)];
        }
        public void PvP() {
            player1.init_grid();
            player2.init_grid();
            player1.place_ships(this.ships);
            Components.Limbo("player2's ship placement").draw(MIDDLEX, MIDDLEY);
            Console.ReadKey();
            player2.place_ships(this.ships);
            Components.Limbo("Game Begin").draw(MIDDLEX,MIDDLEY);
            (int,int) coords = (0,0);
            bool result = false;
            while (true) {
                Components.Limbo("player1's turn").draw(MIDDLEX,MIDDLEY);
                Console.ReadKey();
                player1.render_grids();
                coords = player1.Turn();
                 result = player2.Attack(coords.Item1,coords.Item2);
                player1.result(result,coords.Item1,coords.Item2);
                if(player2.hasLost()) {Console.Clear(); Components.Limbo("Player1 won").draw(MIDDLEX,MIDDLEY); Console.ReadKey();break;}
                player1.render_grids();
                System.Threading.Thread.Sleep(500);
                Components.Limbo("player2's turn").draw(MIDDLEX,MIDDLEY);
                Console.ReadKey();
                player2.render_grids();
                 coords = player2.Turn();
                result = player1.Attack(coords.Item1,coords.Item2);
                player2.result(result,coords.Item1,coords.Item2);
                if(player1.hasLost()) {Console.Clear(); Components.Limbo("Player2 won").draw(MIDDLEX,MIDDLEY); Console.ReadKey();break;}
                player2.render_grids();
                System.Threading.Thread.Sleep(500);
                this.Serialize("pvp");
            }
        }
        public void PvE() {
            this.Serialize("pve");
        }
        public void EvE() {
            this.Serialize("eve");
        }
        public void init() {
            Component boat =  Components.StartupBoat();
            Component current =  Components.MainMenu();
            Console.Clear();

            boat.draw(MIDDLEX, MIDDLEY );
            System.Threading.Thread.Sleep(500);

            while (true) {
                Console.Clear();
                current.draw(MIDDLEX , MIDDLEY );
                var key = Console.ReadKey();
                switch (key.KeyChar) {
                    case '1':
                        current = Components.ChooseMenu();
                        Console.Clear();
                        current.draw(MIDDLEX, MIDDLEY);
                        key = Console.ReadKey();
                        switch (key.KeyChar) {
                            case '1':
                               player1 = new Human();
                               player2 = new Human();
                               PvP();
                                break;
                            case '2':
                               PvE();
                                break;
                            case '3':
                                EvE();
                                break;

                        }
                        break;

                    case '2':
                        current = Components.LoadMenu();
                        var mode = "";
                        while (true) {
                            Console.Clear();
                            current.draw(MIDDLEX - current.dimensions.Item1, MIDDLEY - current.dimensions.Item2);
                            var meow = Console.ReadLine();
                            try {
                                
                             mode = this.Deserialize(meow);
                             break;
                            } catch {
                                Console.Clear();                                
                            }
                        }
                        switch (mode) {
                            case "pvp": PvP(); break;
                            case "pve": PvE(); break;
                            case "eve": EvE(); break;
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

            public string type;
        }

        
        public string Deserialize(string path) {
            string json = File.ReadAllText($"{path}/bb.json");
            SaveFile save = (SaveFile)JsonConvert.DeserializeObject(json,new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.All
            });
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
            return save.type;
        }
        public bool Serialize( string type) {
            
            SaveFile save = new SaveFile {
                p1pgrid = player1.pGrid,
                p1egrid = player1.eGrid,
                p2pgrid = player2.pGrid,
                p2egrid = player2.eGrid,
                type = type,
            };
            try {
                File.Create($"{path}/bb.json").Close();
                var res = JsonConvert.SerializeObject(save, new JsonSerializerSettings {
                    TypeNameHandling = TypeNameHandling.All
                });
                File.WriteAllText($"{path}/bb.json", String.Empty);
                File.WriteAllText($"{path}/bb.json", res);
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
                for (int i = 1; i <= ship.amount; i++) {
                    test = (0, 0, false);
                    verify = false;
                    while (test.Item3 == false || verify == false) {
                        grid = Components.Grid(pGrid);
                        Console.Clear();
                        grid.draw(20, 20);
                        Console.WriteLine($"Placing {ship.name} no.{i}");
                        Console.SetCursorPosition(20,32);
                        Console.Write("Enter Coordinates:");
                        test = format(Console.ReadLine());
                        Console.SetCursorPosition(20,33);
                        Console.Write("Enter orientation h/v:");
                        var orient = Console.ReadKey().KeyChar == 'h' ? true : false;
                        verify = verify_placement(test.Item1, test.Item2, ship, orient);
                        Console.SetCursorPosition(20,34);
                        if(!test.Item3) {
                            Console.Write("Coordinates improperly formatted");
                            System.Threading.Thread.Sleep(1000);
                        }
                        if (!verify) {
                            Console.Write("Placement invalid,try again");
                            System.Threading.Thread.Sleep(1000);
                        }
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
