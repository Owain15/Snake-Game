using Snake_Game.Game;
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

        public Methods(int gameX, int gameY)
        {
            GameX = gameX;
            GameY = gameY;

        }
        public void Intro()
        {
            int IntroX = 14;
            int IntroY = 9;

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
                Console.WriteLine(Segment.SegmentMarker);
            }
        }
        public bool MoveSnake(List<Snake> SnakeList, int[] MoveMade)
        {
            bool Result;
          
            int X = SnakeList[0].SegmentLocation[0] + MoveMade[0];
            int Y = SnakeList[0].SegmentLocation[1] + MoveMade[1];

            Snake NewSegment = new Snake(X,Y);
            
            Result = NewSegment.IsMoveOutOfBounds(true);
          
            if (Result == false)
            {
                SnakeList.Insert(0, NewSegment);
                DrawSnakeMove(SnakeList);
                SnakeList.RemoveAt(SnakeList.Count - 1);
            }
            return Result;
        }
        private void DrawSnakeMove(List<Snake> SnakeList)
        {
            Console.SetCursorPosition(SnakeList[0].SegmentLocation[0]+GameX, SnakeList[0].SegmentLocation[1]+GameY);
            Console.WriteLine(SnakeList[0].SegmentMarker);

            Console.SetCursorPosition
                (SnakeList[SnakeList.Count-1].SegmentLocation[0]+GameX, SnakeList[SnakeList.Count-1].SegmentLocation[1]+GameY);
            Console.WriteLine(" ");

            
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
        public string[,] GetGameBord()
        {
            string[,] GameBord = Resource.Border;
            return GameBord;
        }
        public void DrawFood(Food Target)
        {
            Console.SetCursorPosition(Target.Location[0]+GameX, Target.Location[1]+GameY);
            Console.WriteLine(Target.FoodMarker);

        }
        

        
    }
}
