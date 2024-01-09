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

        public abstract class Player {
            private Tile[,] pGrid;
            private Tile[,] eGrid;

            public void init_Grid() {
                for(int y = 0; y < 8; y++){
                    for(int x = 0; x < 8; x++){
                        pGrid[x,y] = Tile.Empty;
                        eGrid[x,y] = Tile.Empty;
                    }
                }
            }

            public bool Attack(int x,int y) {
                if (!(pGrid[x,y] == Tile.Hit | pGrid[x,y] == Tile.Empty | pGrid[x,y] == Tile.Missed )) {
                   pGrid[x,y] = Tile.Hit;
                   return true; 
                }
                else {
                    pGrid[x,y] = Tile.Missed;
                    return false;
                }
            }

            public abstract void Turn();

            public bool hasLost() {
                 //TODO   
                return true;
            }
            
        }

}
