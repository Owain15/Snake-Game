using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Game.Game
{
    internal class Snake
    {
        int SegmentX;
        int SegmentY;
     
        public int[] SegmentLocation;

        public ConsoleKey SegmentDirection;
        public ConsoleKey PreveasSegmentDirection;

        public string SegmentMarker = "X";

        Resorses.Resorses Resource;

        public Snake( int X, int Y)
        {
            SegmentX = X;
            SegmentY = Y;
            SegmentLocation = new int[2] {SegmentX, SegmentY};

            Resource = new Resorses.Resorses();
        }
        private void GetSegmentMarker()
        {
            
        }
        public bool IsMoveOutOfBounds(bool WorldLooped)
        {
            bool Result = true;

            string Element = Resource.Border[SegmentY,SegmentX];
            if (Element == " ") { Result = false; }

            return Result;

        }
    }
}
