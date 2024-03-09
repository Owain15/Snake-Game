using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Game.Game
{
    internal class Controler
    {
        public ConsoleKey GetInput()
        {
            ConsoleKey Move;
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            Move = keyInfo.Key;
            return Move;
        }
        public int[] GetMoveMade(ConsoleKey Input)
        {

            int[] MoveMade = new int[2] { 0 , 0 };

            switch(Input)
            {
               

                case ConsoleKey.UpArrow:   { MoveMade[1] =-1; } break;
                case ConsoleKey.DownArrow: { MoveMade[1] = 1; } break;
                case ConsoleKey.LeftArrow: { MoveMade[0] =-1; } break;
                case ConsoleKey.RightArrow:{ MoveMade[0] = 1; } break;
            }
            return MoveMade;
        }


    }
}
