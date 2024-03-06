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
        Snake_Game.Resorses.Resorses Resorse = new Snake_Game.Resorses.Resorses();
     

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
        public void DrawPage(int X, int Y)//30,3
        {
            DrawArray(Resorse.Headder, X, Y);
            DrawArray(Resorse.Border, X, Y + 4);

            Console.SetCursorPosition(X + 12, Y + 1);
            Console.WriteLine("Snake.");

            Console.SetCursorPosition(X + 10, Y + 2);
            Console.WriteLine("Score.");

            UpdateScore(X, Y, 0);
        }
        public void DrawSnake(int GameX, int GameY,List<Snake> SnakeList)
        {
            foreach (Snake Segment in SnakeList)
            {
                Console.SetCursorPosition(Segment.SegmentLocation[0]+GameX, Segment.SegmentLocation[1]+GameY);
                Console.WriteLine(Segment.SegmentMarker);
            }
        }
        public void Intro(int GameX, int GameY)
        {
            int IntroX = 12;
            int IntroY = 9;

            Console.SetCursorPosition(GameX+13, IntroY + GameY );
            Console.WriteLine("3");
            Thread.Sleep(200);
            
            Console.SetCursorPosition(GameX + 14, IntroY + GameY );
            Console.WriteLine(".");
            Thread.Sleep(200);

            Console.SetCursorPosition(GameX + 15, IntroY + GameY );
            Console.WriteLine(".");
            Thread.Sleep(200);

            Console.SetCursorPosition(GameX + 13, IntroY + GameY );
            Console.WriteLine("   ");

            Console.SetCursorPosition(GameX + 13, IntroY + GameY );
            Console.WriteLine("2");
            Thread.Sleep(200);

            Console.SetCursorPosition(GameX + 14, IntroY + GameY );
            Console.WriteLine(".");
            Thread.Sleep(200);

            Console.SetCursorPosition(GameX + 15, IntroY + GameY );
            Console.WriteLine(".");
            Thread.Sleep(200);

            Console.SetCursorPosition(GameX + 13, IntroY + GameY );
            Console.WriteLine("   ");

            Console.SetCursorPosition(GameX + 13, IntroY + GameY );
            Console.WriteLine("1");
            Thread.Sleep(200);

            Console.SetCursorPosition(GameX + 14, IntroY + GameY );
            Console.WriteLine(".");
            Thread.Sleep(200);

            Console.SetCursorPosition(GameX + 15, IntroY + GameY );
            Console.WriteLine(".");
            Thread.Sleep(200);

            Console.SetCursorPosition(GameX + 13, IntroY + GameY );
            Console.WriteLine("   ");

            Console.SetCursorPosition(GameX + 13, IntroY + GameY );
            Console.WriteLine("!Go!");
            Thread.Sleep(600);

            Console.SetCursorPosition(GameX + 13, IntroY + GameY );
            Console.WriteLine("    ");


        }
        public void UpdateScore( int GameX, int GameY, int CurrentScore)
        {
            int Score = CurrentScore;
            Console.SetCursorPosition(GameX + 16, GameY + 2);
            Console.WriteLine(Score + ".");
        }
        public ConsoleKey GetInput()
        {
            ConsoleKey Result= ConsoleKey.A;
         
            ConsoleKeyInfo Input = Console.ReadKey();
            
            
            return Result;
        }
    }
}
