using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Game.Game
{
    internal class Snake
    {
        int SegmentIndex;

        int SegmentX;
        int SegmentY;
        public int[] SegmentLocation;

        public ConsoleKey SegmentDirection;
        public ConsoleKey PreveasSegmentDirection;

        public string SegmentMarker = "X";

        public Snake(int Index, int X, int Y)
        {
            SegmentIndex = Index;
            SegmentX = X;
            SegmentY = Y;
            SegmentLocation = new int[2] {SegmentX, SegmentY};

        }
        public void GetSegmentMarker()
        {

        }
    }
}
