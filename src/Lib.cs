namespace VesselFeud { 


public enum Tile {
        // BASIC stuff
                   Hit = 'X',
                   Missed = 'O',
                   Empty = '~',
          //Ship rendering characters: syntax is x_Y where x = orientation, bar c which is center , y = position(start piece,end piece example, v_S is the start piece of a vertical ship, IM SORRY
                   v_S = '◬',
                   v_E = '▽',

                   h_S = '◁',
                   h_E = '▷',

                   c_N = '□',
        }

        public abstract class Player {
            private Tile[,] pGrid;
            private Tile[,] eGrid;

           
            public bool Attack(int x,int y) {
                if (!(pGrid[x,y] == Tile.Hit | pGrid[x,y] == Tile.Empty | pGrid[x,y] == Tile.Missed )) {
                   return true; 
                }
                else {
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
