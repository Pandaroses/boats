namespace VesselFeud {

    public class Game {
        public Ship[] ships = { new Ship("balls", 1, 4) };
        //TODO MAKE CYCLE
        public bool PvP() {
            //2 players, has the in between turns screen
            return true;
        }
        public bool PvE() {
            //player vs robot, you can instantly see turns
            return true;
        }
        public bool EvE() {
            // robot vs robot, you can either set timer on turn speeds, or you can have press key to start next turn
            return true;
        }
        //TODO MAKE MENU
        public void init() {
            //this function will allow the user to traverse through the menus, probably make a dedicated menu function so user scan press esc or something to leave the game ifk but yeah
            //menus then start the desired game
            bool ended = false;
            while (ended != true) {
            }
        }

    }

    public class Human : Player {


        public override (int,int) Turn() {
            (int, int, bool) test = (0,0,false);
            while (test.Item3 != true) {
                Console.Clear();
                render_grids();
                test = format(Console.ReadLine());
            }
            return (test.Item1,test.Item2);


        }
        public override void place_ships(Ship[] ships) {
            Component grid =  Components.Grid(pGrid);
            (int,int,bool) test = (0,0,false);
            bool verify = false;
            foreach (Ship ship in ships) {
                for(int i = 0; i < ship.amount; i++){
                    test = (0,0,false);
                    while(test.Item3 != true && verify != true) {
                        Console.Clear();
                        grid.draw(20,20);
                        Console.WriteLine($"Placing {ship.name}");
                        Console.Write("Enter Coordinates:");
                        test = format(Console.ReadLine());
                        Console.Write("Enter orientation h/v");
                        var orient = Console.ReadKey().KeyChar == 'h'? true: false;
                        verify = verify_placement(test.Item1,test.Item2,ship,orient);
                    }
                    
                }
            }
        
        }

    }


    //TODO make exist :3
    public class Robot : Player {
        public override (int,int) Turn() {
            throw new NotImplementedException();
        }
        public override void place_ships(Ship[] ships) {
            throw new NotImplementedException();
        }
    }


}
