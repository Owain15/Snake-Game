using Snake_Game.Game;
using Snake_Game.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Game.Resorses
{
    internal class TestClass
    {

        static int GameX;
        static int GameY;

        static int GameSpeed;
        static bool WorldLooped;

        Methods Method;
        Controler Control;

        public TestClass(int gameX, int gameY, int gameSpeed, bool worldLooped)
        {
            GameX = gameX;
            GameY = gameY;

            GameSpeed = gameSpeed;
            WorldLooped = worldLooped;


            Method = new Methods(GameX, GameY, WorldLooped);
            Control = new Controler();

        }



        int Score = 0;
        bool FoodInPlay = false;
        bool GameOver = false;

        List<Snake> SnakeList;
        Food TargetFood;

        public void TestFoodSpawn()
        {
            Method.DrawPage();

            Console.SetCursorPosition(GameX + 3, GameY + 12);
            Console.WriteLine("Press Enter To Spawn Food.");

            int TestNumber = 1;
            int ReadOutX = GameX + 35;
            int ReadOutY = 0;

            while (GameOver == false)
            {
                Console.Read();
                TestFoodSetUp(TestNumber, ReadOutX, ReadOutY);

                ReadOutY++;
                TestNumber++;

                Thread.Sleep(GameSpeed);
            }

        }
        private void TestFoodSetUp(int TestNumber, int ReadOutX, int ReadOutY)
        {
            TargetFood = new Food(SnakeList, Method.GetGameBord());

            Method.DrawFood(TargetFood);
            Console.SetCursorPosition(ReadOutX, ReadOutY);
            Console.WriteLine("Test " + TestNumber + ". " + TargetFood.FoodLocation[0] + "." + TargetFood.FoodLocation[1]);

        }


        public void TestFoodCaught()
        {
            Method.DrawPage();

            //int TestSpeed = GameSpeed * 4;

            int ReadOutX = GameX + 35;
            int ReadOutY = 0;

            ConsoleKey Input = ConsoleKey.RightArrow;
            TestFoodCaughtReadOutSetUp(ReadOutX, ReadOutY);
            SnakeSetUp();

            while (GameOver == false)
            {

                ScoreUpdate();
                FoodSetUp();
                GameOver = Method.MoveSnake(SnakeList, Input, Control.GetMoveMade(Input), FoodInPlay);
                Score = Score + FoodCheck();
                // Input = CheckForInput(Input);
                if (GameOver == false) { GameOver = Method.CheckSnakeHit(SnakeList); }

                TestFoodCaughtReadOutUpdate(ReadOutX, ReadOutY);
                Input = GetInput(Input);

                //Thread.Sleep(TestSpeed);
            }
        }
        private void TestFoodCaughtReadOutSetUp(int ReadOutX, int ReadOutY)
        {
            Console.SetCursorPosition(ReadOutX, ReadOutY);
            Console.WriteLine("Snakes Head. ");
            Console.SetCursorPosition(ReadOutX, ReadOutY + 1);
            Console.WriteLine("Food. ");
        }
        private void TestFoodCaughtReadOutUpdate(int ReadOutX, int ReadOutY)
        {
            Console.SetCursorPosition(ReadOutX + 13, ReadOutY);
            Console.WriteLine("               ");
            Console.SetCursorPosition(ReadOutX + 6, ReadOutY + 1);
            Console.WriteLine("               ");

            Console.SetCursorPosition(ReadOutX + 13, ReadOutY);
            Console.WriteLine(SnakeList[0].SegmentLocation[0] + "." + SnakeList[0].SegmentLocation[1]);
            Console.SetCursorPosition(ReadOutX + 6, ReadOutY + 1);
            Console.WriteLine(TargetFood.FoodLocation[0] + "." + TargetFood.FoodLocation[1]);
        }

        public void TestSnakeMakerSet()
        {
            Method.DrawPage();

            //int TestSpeed = GameSpeed * 4;

            int ReadOutX = GameX + 35;
            int ReadOutY = 0;

            ConsoleKey Input = ConsoleKey.RightArrow;
            TestSnakeMarkerSetUp(ReadOutX, ReadOutY);
            SnakeSetUp();

            while (GameOver == false)
            {

                ScoreUpdate();
                FoodSetUp();
                GameOver = Method.MoveSnake(SnakeList, Input, Control.GetMoveMade(Input), FoodInPlay);
                Score = Score + FoodCheck();
                // Input = CheckForInput(Input);
                if (GameOver == false) { GameOver = Method.CheckSnakeHit(SnakeList); }

                TestFoodCaughtReadOutUpdate(ReadOutX, ReadOutY);
                Input = GetInput(Input);

                //Thread.Sleep(TestSpeed
            }
        }
        private void TestSnakeMarkerSetUp(int ReadOutX, int ReadOutY)
        {

        }




        private void SnakeSetUp()
        {
            Snake S1 = new Snake(13, 5,ConsoleKey.RightArrow);
            Snake S2 = new Snake(12, 5, ConsoleKey.RightArrow);
            Snake S3 = new Snake(11, 5, ConsoleKey.RightArrow);
            Snake S4 = new Snake(10, 5, ConsoleKey.RightArrow);

            Snake S5 = new Snake(9, 5, ConsoleKey.RightArrow);
            Snake S6 = new Snake(8, 5, ConsoleKey.RightArrow);
            Snake S7 = new Snake(7, 5, ConsoleKey.RightArrow);

            SnakeList = new List<Snake>() { S1, S2, S3, S4, S5, S6, S7 };

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
        private ConsoleKey CheckForInput(ConsoleKey Input)
        {

            bool InputMade = false;
            ConsoleKey PreposedInput;

            InputMade = Console.KeyAvailable;
            if (InputMade == true)
            {
                PreposedInput = Control.GetInput();
                bool InputCheck = CheckInput(Input, PreposedInput);
                if (InputCheck == true) { Input = PreposedInput; }
            }

            return Input;

        }
        private ConsoleKey GetInput(ConsoleKey Input)
        {

            ConsoleKey PreposedInput;

            
                PreposedInput = Control.GetInput();
                bool InputCheck = CheckInput(Input, PreposedInput);
                if (InputCheck == true) { Input = PreposedInput; }
            

            return Input;

        }
        private bool CheckInput(ConsoleKey Input, ConsoleKey PreposedInput)
        {
            bool Result = true;

            switch (Input)
            {
                case ConsoleKey.UpArrow: { if (PreposedInput == ConsoleKey.DownArrow) { Result = false; } } break;
                case ConsoleKey.DownArrow: { if (PreposedInput == ConsoleKey.UpArrow) { Result = false; } } break;
                case ConsoleKey.LeftArrow: { if (PreposedInput == ConsoleKey.RightArrow) { Result = false; } } break;
                case ConsoleKey.RightArrow: { if (PreposedInput == ConsoleKey.LeftArrow) { Result = false; } } break;
            }


            return Result;
        }
        private int FoodCheck() 
        {
            int Result = 0;

            if (TargetFood.FoodLocation[0] == SnakeList[0].SegmentLocation[0])
            {
                if (TargetFood.FoodLocation[1] == SnakeList[0].SegmentLocation[1])
                {
                    //Score = Score + TargetFood.Score;
                    Result = TargetFood.Score;
                    FoodInPlay = false;
                }
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
