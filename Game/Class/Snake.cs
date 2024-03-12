using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Game.Game.Class
{
    internal class Snake
    {
        int SegmentX;
        int SegmentY;

        public int SegmentPosition = 0;

        public int[] SegmentLocation;

        public ConsoleKey SegmentDirection;
        public ConsoleKey NextSegmentDirection;

        public string SegmentMarker;

        Resorses.Resorses Resource;

        public Snake(int X, int Y, ConsoleKey Input)
        {
            SegmentX = X;
            SegmentY = Y;

            SegmentLocation = new int[2] { SegmentX, SegmentY };

            SegmentDirection = Input;
            NextSegmentDirection = ConsoleKey.RightArrow;

            Resource = new Resorses.Resorses();

            SegmentMarker = "X";


        }
        public string GetSegmentMarker()
        {
            string Marker = "X";

            switch (SegmentDirection)
            {
                case ConsoleKey.UpArrow:
                    {
                        switch (NextSegmentDirection)
                        {
                            case ConsoleKey.UpArrow: { Marker = Resource.SnakeBodyVertical; } break;
                            //case ConsoleKey.DownArrow: { Marker = Resource.SnakeBodyVertical; } break;
                            case ConsoleKey.LeftArrow: { Marker = Resource.SnakeBodyHorizontalDown; } break;
                            case ConsoleKey.RightArrow: { Marker = Resource.SnakeBodyVerticalUpRight; } break;
                        }

                    }
                    break;
                case ConsoleKey.DownArrow:
                    {
                        switch (NextSegmentDirection)
                        {
                            //case ConsoleKey.UpArrow: { Marker = Resource.SnakeBodyVertical; } break;
                            case ConsoleKey.DownArrow: { Marker = Resource.SnakeBodyVertical; } break;
                            case ConsoleKey.LeftArrow: { Marker = Resource.SnakeBodyHorizontalUp; } break;
                            case ConsoleKey.RightArrow: { Marker = Resource.SnakeBodyVerticalDownRight; } break;
                        }
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    {
                        switch (NextSegmentDirection)
                        {
                            case ConsoleKey.UpArrow: { Marker = Resource.SnakeBodyVerticalDownRight; } break;
                            case ConsoleKey.DownArrow: { Marker = Resource.SnakeBodyVerticalUpRight; } break;
                            case ConsoleKey.LeftArrow: { Marker = Resource.SnakeBodyHorizontal; } break;
                                //case ConsoleKey.RightArrow: { Marker = Resource.SnakeBodyHorizontal; } break;
                        }
                    }
                    break;
                case ConsoleKey.RightArrow:
                    {
                        switch (NextSegmentDirection)
                        {
                            case ConsoleKey.UpArrow: { Marker = Resource.SnakeBodyHorizontalUp; } break;
                            case ConsoleKey.DownArrow: { Marker = Resource.SnakeBodyHorizontalDown; } break;
                            //case ConsoleKey.LeftArrow: { Marker = Resource.SnakeBodyHorizontal; } break;
                            case ConsoleKey.RightArrow: { Marker = Resource.SnakeBodyHorizontal; } break;
                        }
                    }
                    break;


            }

            if (SegmentPosition == 0) { Marker = Resource.SnakeHead; }

            return Marker;
        }
        public bool IsMoveOutOfBounds()
        {
            bool Result = true;

            string Element = Resource.Border[SegmentY, SegmentX];
            if (Element == " ") { Result = false; }

            return Result;

        }
    }
}
