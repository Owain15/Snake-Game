using Snake_Game.Game.Class;
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
        


        public GameLoop(int[] GameLocation, GameSetting Settings)
        {
            GameX = GameLocation[0];
            GameY = GameLocation[1];

            GameSpeed = Settings.GameSpeed;
            WorldLooped = Settings.WorldLooped;
            

            Method = new Methods(GameX,GameY,WorldLooped);
            
        }
        
        

        int Score = 0;
        bool FoodInPlay = false;
        bool GameOver = false;

        List<Snake> SnakeList;
        Food TargetFood;

        private void RunStartUp()
        {

            Method.DrawPage();
            Console.SetCursorPosition(GameX + 3, GameY + 5);
            Console.WriteLine("Press Any Enter To Start.");
            Console.Read();

            Console.SetCursorPosition(GameX + 3, GameY + 5);
            Console.WriteLine("                         ");
            Method.Intro();


            Console.Read();
        }
        public void RunGame()
        {

            Method.DrawPage();
            Method.Intro();

            ConsoleKey Input = ConsoleKey.RightArrow;

            SnakeSetUp();

            while (GameOver == false)
            {
                ScoreUpdate();
                
                FoodCheck();
                GameOver = Method.MoveSnake(SnakeList, Input ,Method.GetMoveMade(Input), FoodInPlay );
                FoodSetUp();
                Input = CheckForInput(Input);
                if(GameOver == false) { GameOver = Method.CheckSnakeHit(SnakeList); }
                Thread.Sleep(GameSpeed);
            } 
            
        }
        private void SnakeSetUp()
        {
            Snake S0 = new Snake(13, 5,ConsoleKey.RightArrow);
            Snake S1 = new Snake(12, 5, ConsoleKey.RightArrow);
            Snake S2 = new Snake(11, 5, ConsoleKey.RightArrow);
            Snake S3 = new Snake(10, 5, ConsoleKey.RightArrow);

            S1.SegmentPosition = 1;
            S2.SegmentPosition = 2;
            S3.SegmentPosition = 3;

            SnakeList = new List<Snake>() { S0, S1, S2, S3 };
            
            Method.DrawSnake(SnakeList);
        }
        private void FoodSetUp()
        {
            if (FoodInPlay == false)
            {
                TargetFood = new Food(SnakeList, Method.GetGameBord());
                Method.DrawFood(TargetFood);
                FoodInPlay = true;
            }
        }
        private void FoodCheck() 
        {
            if (FoodInPlay)
            {
                if (TargetFood.FoodLocation[0] == SnakeList[0].SegmentLocation[0])
                {
                    if (TargetFood.FoodLocation[1] == SnakeList[0].SegmentLocation[1])
                    {
                        Score = Score + TargetFood.Score;
                        FoodInPlay = false;
                    }
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
                PreposedInput = Method.GetInput(); 
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
