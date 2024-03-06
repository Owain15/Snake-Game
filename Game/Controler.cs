using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Game.Game
{
    internal class Controler
    {
        public void HandleInput(ConsoleKey Input)
        {
            switch(Input)
            {
                case ConsoleKey.Escape: { }break;
                case ConsoleKey.UpArrow: { }break;
                case ConsoleKey.DownArrow: { } break;
                case ConsoleKey.LeftArrow: { } break;
                case ConsoleKey.RightArrow: { }break;
            }
        }
    }
}
