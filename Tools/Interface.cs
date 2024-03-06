using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Game.NewFolder
{
    internal interface Interface
    {
        public void DrawArray(string[,] Array, int StartX, int StartY)
        {

            int Rows = Array.GetLength(0);
            int Cols = Array.GetLength(1);

            for (int y = 0; y < Rows; y++)
            {
                for (int x = 0; x < Cols; x++)
                {
                    string Element = Array[y, x];
                    Console.SetCursorPosition(x + StartX, y + StartY);
                    Console.WriteLine(Element);
                }

            }

        }

    }
}
