using Newtonsoft.Json;
namespace VesselFeud {

    public class Game {
        public static int MIDDLEX = Console.WindowWidth / 2;
        public static int MIDDLEY = Console.WindowHeight / 2;
        public Ship[] ships;
        public string path = "/home/gsh/proj/vesselfeud/";
        public Player? player1;
        public Player? player2;

        public Game() {
            // this.ships = [new Ship("Dick", 2, 4), new Ship("Carrier",1, 5), new Ship("Cruiser", 3, 2) ];
            this.ships = [new Ship("dick", 1, 4)];
        }


        public bool turn(Player attack, Player def) {
            attack.render_grids();
            var coords = attack.Turn();
            var result = def.Attack(coords.Item1, coords.Item2);
            attack.result(result, coords.Item1, coords.Item2);
            attack.render_grids(); System.Threading.Thread.Sleep(500);
            Components.Limbo((def.name + "'s turn").ToString()).draw(MIDDLEX,MIDDLEY); Console.ReadKey(); 
            return def.hasLost();
        }
        public void init_PvP() {
            
            Console.Clear();
            Console.Write("Player1's Name: ");
            player1.name = Console.ReadLine();
            Console.Clear();
            Console.Write("Player2's name:");
            player2.name = Console.ReadLine();
            player1.init_grid();
            player2.init_grid();
            player1.place_ships(this.ships);
            Components.Limbo($"{player2.name} ships").draw(MIDDLEX, MIDDLEY);
            Console.ReadKey();
            player2.place_ships(this.ships);
            Components.Limbo("Game Begin").draw(MIDDLEX, MIDDLEY);
            PvP();
        }
        public void PvP() {

            while (true) {
                //winner screen player1
                if (this.turn(player1, player2)) { break; };
                //winer screen player2
                if (this.turn(player2, player1)) { break; };
                this.Serialize("pvp");
            }
        }
        public void init_PvE() {
            Console.WriteLine("your name");
            player1.name = (Console.ReadLine());            
            player1.init_grid();
            player2.init_grid();
            player1.place_ships(this.ships);
            player2.place_ships(this.ships);
        }
        public void PvE() {

            while (true) {
                if (this.turn(player1, player2)) { break; }
                var coords = player2.Turn();
                var result = player1.Attack(coords.Item1,coords.Item2);
                player2.result(result,coords.Item1,coords.Item2);
                if (player1.hasLost()) { break;}
                this.Serialize("pve");
            }
        }
        public void init() {
            Component boat = Components.StartupBoat();
            Component current = Components.MainMenu();
            Console.Clear();

            boat.draw(MIDDLEX, MIDDLEY);
            System.Threading.Thread.Sleep(500);

            while (true) {
                Console.Clear();
                current.draw(0,0);
                var key = Console.ReadKey();
                switch (key.KeyChar) {
                    case '1':
                        current = Components.ChooseMenu();
                        Console.Clear();
                        current.draw(0,0);
                        key = Console.ReadKey();
                        switch (key.KeyChar) {
                            case '1':
                                player1 = new Human();
                                player2 = new Human();
                                init_PvP();
                                break;
                            case '2':
                                player1 = new Human();
                                player2 = new Robot();
                                init_PvE();
                                break;
                            default: 
                                current = Components.MainMenu();
                                break;

                        }
                        
                        break;

                    case '2':
                        current = Components.LoadMenu();
                        var mode = "";
                        while (true) {
                            Console.Clear();
                            current.draw(0,0);
                            var meow = Console.ReadLine();
                            try {

                                mode = this.Deserialize(meow);
                                break;
                            }
                            catch {
                                Console.Clear();
                            }
                        }
                        switch (mode) {
                            case "pvp": PvP(); break;
                            case "pve": PvE(); break;
                        }
                        current = Components.MainMenu();
                        break;
                    case '3':
                        current = Components.Rules();
                        current.draw(0,0);
                        Console.ReadKey();
                        current = Components.MainMenu();
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
            public string p1name;
            public string p2name;
            public string type;
        }


        public string Deserialize(string path) {
            string json = File.ReadAllText($"{path}/bb.json");
            SaveFile save = (SaveFile)JsonConvert.DeserializeObject(json, new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.All
            });
            player1 = new Human();
            player2 = new Human();
            if (save.type == "pve") {
                player1 = new Human();
                player2 = new Robot();
            }
            player1.pGrid = save.p1pgrid;
            player1.eGrid = save.p1egrid;
            player2.pGrid = save.p2pgrid;
            player2.eGrid = save.p2egrid;
            player1.name = save.p1name;
            player2.name = save.p2name;
            return save.type;
        }
        public bool Serialize(string type) {

            SaveFile save = new SaveFile {
                p1pgrid = player1.pGrid,
                p1egrid = player1.eGrid,
                p2pgrid = player2.pGrid,
                p2egrid = player2.eGrid,
                p1name = player1.name,
                p2name = player2.name,
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
                        grid.draw(0,0);
                        Console.WriteLine($"Placing {ship.name} no.{i}");
                        Console.Write("Enter Coordinates:");
                        test = format(Console.ReadLine());
                        Console.Write("Enter orientation h/v:");
                        var orient = Console.ReadKey().KeyChar == 'h' ? true : false;
                        verify = test.Item3 ? verify_placement(test.Item1, test.Item2, ship, orient) : false;
                        if (!test.Item3) {
                            Console.WriteLine("Coordinates improperly formatted");
                            System.Threading.Thread.Sleep(1000);
                        }
                        if (!verify) {
                            Console.WriteLine("Placement invalid,try again");
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
            Random random = new Random();
            return (random.Next(7),random.Next(7));
        }
        public override void place_ships(Ship[] ships) {
            //fimsh copilot write place_ships functiono
            int x = 0;
            int y = 0;
            bool h = false;
            foreach(var ship in ships ) {
                Random random = new Random();
                while (true) {
                    x = random.Next(10); y = random.Next(10);
                    h = random.Next(1) == 0 ? true : false;
                    if (verify_placement(x , y , ship , h)) {break;}
                }
            }
            
        }
    }


}
