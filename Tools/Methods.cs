using Snake_Game.Game.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Game.Tools
{
    internal class Methods
    {
        Snake_Game.Resorses.Resorses Resource = new Snake_Game.Resorses.Resorses();
       
        int GameX;
        int GameY;

        public int Score = 0;

        bool WorldLooped;
        string[,] GameBored;

        public Methods(int gameX, int gameY, bool worldLooped)
        {
            GameX = gameX;
            GameY = gameY;
            WorldLooped = worldLooped;
            GameBored = Resource.Border;
        }
        public void Intro()
        {
            int IntroX = 14;
            int IntroY = 5;

            Console.SetCursorPosition( IntroX + GameX, IntroY + GameY );
            Console.WriteLine("3");
            Thread.Sleep(200);
            
            Console.SetCursorPosition( IntroX + GameX + 1, IntroY + GameY );
            Console.WriteLine(".");
            Thread.Sleep(200);

            Console.SetCursorPosition( IntroX + GameX + 2, IntroY + GameY );
            Console.WriteLine(".");
            Thread.Sleep(200);

            Console.SetCursorPosition( IntroX + GameX, IntroY + GameY );
            Console.WriteLine("   ");

            Console.SetCursorPosition( IntroX + GameX, IntroY + GameY );
            Console.WriteLine("2");
            Thread.Sleep(200);

            Console.SetCursorPosition(IntroX + GameX + 1, IntroY + GameY );
            Console.WriteLine(".");
            Thread.Sleep(200);

            Console.SetCursorPosition(IntroX + GameX + 2, IntroY + GameY );
            Console.WriteLine(".");
            Thread.Sleep(200);

            Console.SetCursorPosition(IntroX + GameX, IntroY + GameY );
            Console.WriteLine("   ");

            Console.SetCursorPosition(IntroX + GameX, IntroY + GameY );
            Console.WriteLine("1");
            Thread.Sleep(200);

            Console.SetCursorPosition(IntroX + GameX + 1, IntroY + GameY );
            Console.WriteLine(".");
            Thread.Sleep(200);

            Console.SetCursorPosition(IntroX + GameX + 2, IntroY + GameY );
            Console.WriteLine(".");
            Thread.Sleep(200);

            Console.SetCursorPosition(IntroX + GameX, IntroY + GameY );
            Console.WriteLine("   ");

            Console.SetCursorPosition(IntroX + GameX -1, IntroY + GameY );
            Console.WriteLine("!Go!");
            Thread.Sleep(600);

            Console.SetCursorPosition(IntroX + GameX, IntroY + GameY );
            Console.WriteLine("    ");


        }
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
        public void DrawPage()
        {
            DrawArray(Resource.Headder, GameX, GameY - 4);
            DrawArray(Resource.Border, GameX, GameY );

            Console.SetCursorPosition(GameX + 12, GameY - 3);
            Console.WriteLine("Snake.");

            Console.SetCursorPosition(GameX + 10, GameY - 2);
            Console.WriteLine("Score.");

            UpdateScore();
        }
        public void DrawSnake(List<Snake> SnakeList)
        {
            foreach (Snake Segment in SnakeList)
            {
                Console.SetCursorPosition(Segment.SegmentLocation[0]+GameX, Segment.SegmentLocation[1]+GameY);
                Console.WriteLine(Segment.GetSegmentMarker());
            }
        }
        public bool MoveSnake(List<Snake> SnakeList, ConsoleKey Input ,int[] MoveMade,bool FoodInPlay)
        {
            bool MoveInBounds;
          
            int X = SnakeList[0].SegmentLocation[0] + MoveMade[0];
            int Y = SnakeList[0].SegmentLocation[1] + MoveMade[1];

            Snake NewSegment = new Snake(X,Y,Input);
            SnakeList[0].NextSegmentDirection = NewSegment.SegmentDirection;
            
            MoveInBounds = NewSegment.IsMoveOutOfBounds();
          
            if (!MoveInBounds)
            {
                UpdateSnakeSegmentPosition(SnakeList);
                SnakeList.Insert(0, NewSegment);
               
                DrawSnakeMove(SnakeList);
                if(FoodInPlay) { SnakeList.RemoveAt(SnakeList.Count - 1); }
                
            }
            if (MoveInBounds) 
            { 
             if(WorldLooped)
                {
                    int[] NewLooedSegmentLocation = new int[2];
                    NewLooedSegmentLocation = GetNewLoopedLocation(X,Y);
                    Snake NewLoopedSegment = new Snake(NewLooedSegmentLocation[0], NewLooedSegmentLocation[1],Input);
                    NewLoopedSegment.SegmentDirection = SnakeList[0].NextSegmentDirection ;

                    UpdateSnakeSegmentPosition(SnakeList);
                    SnakeList.Insert(0, NewLoopedSegment);
                    
                    DrawSnakeMove(SnakeList);
                    if (FoodInPlay) { SnakeList.RemoveAt(SnakeList.Count - 1); }
                    MoveInBounds = false;
                }
            }
            return MoveInBounds;
        }
        private void DrawSnakeMove(List<Snake> SnakeList)
        {
            UpdateSnakeMarkers(SnakeList);
            
            Console.SetCursorPosition
                (SnakeList[SnakeList.Count-1].SegmentLocation[0]+GameX, SnakeList[SnakeList.Count-1].SegmentLocation[1]+GameY);
            Console.WriteLine(" ");
            
            Console.SetCursorPosition(SnakeList[0].SegmentLocation[0]+GameX, SnakeList[0].SegmentLocation[1]+GameY);
            Console.WriteLine(SnakeList[0].SegmentMarker);

            Console.SetCursorPosition(SnakeList[1].SegmentLocation[0] + GameX, SnakeList[1].SegmentLocation[1] + GameY);
            Console.WriteLine(SnakeList[1].SegmentMarker);

        }
        public bool CheckSnakeHit(List<Snake> SnakeList)
        {
            bool Result = false;
            
            int X = SnakeList[0].SegmentLocation[0];
            int Y = SnakeList[0].SegmentLocation[1];

            for (int i = 1; i<SnakeList.Count-1; i++)
            {
                if( X == SnakeList[i].SegmentLocation[0] )
                { if (Y == SnakeList[i].SegmentLocation[1]) 
                  { Result = true; }      
                }
                
            }
            return Result;
        }
        public void UpdateScore()
        {
            Console.SetCursorPosition(GameX + 16, GameY - 2);
            Console.WriteLine(Score + ".");
        }
        private void UpdateSnakeSegmentPosition(List<Snake> SnakeList)
        {
            foreach (Snake Segment in SnakeList)
            { Segment.SegmentPosition++; }

        }
        // Is Marker Update Needed?
        private void UpdateSnakeMarkers(List<Snake> SnakeList)
        {
            foreach (Snake Segment in SnakeList)
            { Segment.SegmentMarker = Segment.GetSegmentMarker(); }

        }
        public string[,] GetGameBord()
        {
            string[,] GameBord = Resource.Border;
            return GameBord;
        }
        public void DrawFood(Food Target)
        {
            Console.SetCursorPosition(Target.FoodLocation[0]+GameX, Target.FoodLocation[1]+GameY);
            Console.WriteLine(Target.FoodMarker);

        }
        private int[] GetNewLoopedLocation(int x, int y)
        {
            int[] loopedLocation = new int[2] { x , y };

            if (x == 0) { loopedLocation[0] = GameBored.GetLength(1) - 2; }
            if (x > GameBored.GetLength(1) - 2) { loopedLocation[0] = 1;  }

            if (y == 0) { loopedLocation[1] = GameBored.GetLength(0) - 2; }
            if (y > GameBored.GetLength(0) - 2) { loopedLocation[1] = 1; }
           
            return loopedLocation;
        }
        public ConsoleKey GetInput()
        {
            ConsoleKey Move;
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            Move = keyInfo.Key;
            return Move;
        }
        public int[] GetMoveMade(ConsoleKey Input)
        {

            int[] MoveMade = new int[2] { 0, 0 };

            switch (Input)
            {


                case ConsoleKey.UpArrow: { MoveMade[1] = -1; } break;
                case ConsoleKey.DownArrow: { MoveMade[1] = 1; } break;
                case ConsoleKey.LeftArrow: { MoveMade[0] = -1; } break;
                case ConsoleKey.RightArrow: { MoveMade[0] = 1; } break;
            }
            return MoveMade;
        }



    }
}
