using Snake_Game.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Game.Game
{
    internal class GameLoop
    { 
        Methods Build = new Methods();

        int GameX = 30;
        int GameY = 3;

        int Score = 0;
        bool GameOver = false;

        List<Snake> SnakeList;

        public void RunStartUp()
        {

            Build.DrawPage(GameX, GameY);
            Console.SetCursorPosition(GameX + 3, GameY + 9);
            Console.WriteLine("Press Any Enter To Start.");
            Console.Read();

            Console.SetCursorPosition(GameX + 3, GameY + 9);
            Console.WriteLine("                         ");
            Build.Intro(GameX, GameY);


            Console.Read();
        }
        public void RunGame()
        {
            Snake S1 = new Snake( 1, 14, 9);
            Snake S2 = new Snake( 2, 13, 9);
            Snake S3 = new Snake( 3, 12, 9);
            Snake S4 = new Snake( 4, 11, 9);

            SnakeList = new List<Snake>() { S1,S2,S3,S4 };

            Build.DrawSnake(GameX, GameY,SnakeList);

            Console.Read();
        }

    }
}
