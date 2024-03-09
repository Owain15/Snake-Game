using Snake_Game.Resorses;
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
        bool FoodInPlay = false;
        bool GameOver = false;

        List<Snake> SnakeList;
        Food TargetFood;
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
           //Remove DrawPage
            Method.DrawPage();

            ConsoleKey Input = ConsoleKey.RightArrow;

            SnakeSetUp();

            while (GameOver == false)
            {
                ScoreUpdate();
                FoodSetUp();
                GameOver = Method.MoveSnake(SnakeList, Control.GetMoveMade(Input));
                FoodCheck();
                Input = CheckForInput(Input);
                if(GameOver == false) { GameOver = Method.CheckSnakeHit(SnakeList); }
                Thread.Sleep(GameSpeed);
            } 
            
        }
        private void SnakeSetUp()
        {
            Snake S1 = new Snake(13, 5);
            Snake S2 = new Snake(12, 5);
            Snake S3 = new Snake(11, 5);
            Snake S4 = new Snake(10, 5);

            Snake S5 = new Snake(9, 5);
            Snake S6 = new Snake(8, 5);
            Snake S7 = new Snake(7, 5);

            SnakeList = new List<Snake>() { S1, S2, S3, S4,S5,S6,S7 };
            
            Method.DrawSnake(SnakeList);
        }
        private void FoodSetUp()
        {
            TargetFood = new Food(SnakeList, Method.GetGameBord());

            if (FoodInPlay == false)
            {
                Method.DrawFood(TargetFood);
                FoodInPlay = true;
            }
        }
        private void FoodCheck() 
        {
            if (TargetFood.Location[0] == SnakeList[0].SegmentLocation[0])
            {
                if (TargetFood.Location[1] == SnakeList[0].SegmentLocation[1])
                {
                    Score = Score + TargetFood.Score;
                    FoodInPlay = false;
                }
            }
        }
        private ConsoleKey CheckForInput( ConsoleKey Input)
        {
               
            bool InputMade = false;
            ConsoleKey PreposedInput;
       
            InputMade = Console.KeyAvailable;
            if (InputMade == true) 
            { 
                PreposedInput = Control.GetInput(); 
                bool InputCheck = CheckInput(Input,PreposedInput);
                if(InputCheck == true) { Input = PreposedInput; }
            }
               
            return Input;
               
        }
        private bool CheckInput( ConsoleKey Input, ConsoleKey PreposedInput)
        {
            bool Result = true;

            switch(Input)
            {
                case ConsoleKey.UpArrow:   { if (PreposedInput == ConsoleKey.DownArrow) { Result = false; } } break;
                case ConsoleKey.DownArrow: { if (PreposedInput == ConsoleKey.UpArrow)   { Result = false; } } break;
                case ConsoleKey.LeftArrow: { if (PreposedInput == ConsoleKey.RightArrow){ Result = false; } } break;
                case ConsoleKey.RightArrow:{ if (PreposedInput == ConsoleKey.LeftArrow) { Result = false; } } break;
            }


            return Result;
        }
        private void ScoreUpdate()
        {
            if (Score != Method.Score) 
            { 
                Method.Score = Score;
                Method.UpdateScore();
            }
        }

    }
}
