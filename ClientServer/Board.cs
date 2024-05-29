using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    public class Cell
    {
        public bool IsOccupied { get; set; } // поле показывает занята ли ячейка

        public bool IsHit { get; set; } // показывает был ли удар


        public Cell()
        {
            IsOccupied = false;

            IsHit = false;
        }
    }
    public class Board
    {
        public Cell[,] Cells { get; private set; }

        public int MapSize = 10;

        


        public Board()
        {
            Cells = new Cell[MapSize, MapSize];

            

            for (int i = 0; i < MapSize; i++)
            {
                for (int j = 0; j < MapSize; j++)
                {
                    Cells[i, j] = new Cell();
                }
            }

        }

       
        
    }
}
