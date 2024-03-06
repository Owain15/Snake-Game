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
        static int GameX;
        static int GameY;

        static int GameSpeed;
        static bool WorldLooped;

        Methods Method;
        Controler Control;


        public GameLoop(int gameX, int gameY, int gameSpeed, bool worldLooped)
        {
            GameX = gameX;
            GameY = gameY;

            GameSpeed = gameSpeed;
            WorldLooped = worldLooped;

            Method = new Methods(GameX,GameY);
            Control = new Controler();
        }
        
        

        int Score = 0;
        bool GameOver = false;

        List<Snake> SnakeList;

        public void RunStartUp()
        {

            Method.DrawPage();
            Console.SetCursorPosition(GameX + 3, GameY + 9);
            Console.WriteLine("Press Any Enter To Start.");
            Console.Read();

            Console.SetCursorPosition(GameX + 3, GameY + 9);
            Console.WriteLine("                         ");
            Method.Intro();


            Console.Read();
        }
        public void RunGame()
        {
            SetUpSnake();
           
           //Remove DrawPage
            Method.DrawPage();
            
            ConsoleKey Input = ConsoleKey.RightArrow;

            do
            {
                
                GameOver = Method.MoveSnake(SnakeList, Control.GetMoveMade(Input));
                Input = CheckForInput(Input);
                
                Thread.Sleep(GameSpeed);
            } while (GameOver != true);
            
        }
        private void SetUpSnake()
        {
            Snake S1 = new Snake(13, 9);
            Snake S2 = new Snake(12, 9);
            Snake S3 = new Snake(11, 9);
            Snake S4 = new Snake(10, 9);

            SnakeList = new List<Snake>() { S1, S2, S3, S4 };
            
            Method.DrawSnake(SnakeList);
        }
        private ConsoleKey CheckForInput( ConsoleKey Input)
        {
               
                bool InputMade = false;
                InputMade = Console.KeyAvailable;
                if (InputMade == true) { Input = Control.GetInput(); }
                return Input;
               
        }

    }
}
