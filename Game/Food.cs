using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Game.Game
{
    internal class Food
    {
        Random Generate;
        public int[] Location;
        
        string[,] GameBorder;
        List<Snake> SnakeReff;
        
        public int Score;
        public string FoodMarker = "F";
        public bool FoodBeenCaught;

        public Food(List<Snake> SnakeList, string[,] gameBord)
        {
            GameBorder = gameBord;
            SnakeReff = new List<Snake>();
            Score = 20;
            FoodBeenCaught = false;

            Generate = new Random();

            Location = FindLocation(); 
 

        }
        public int[] FindLocation( )
        {
            
            bool ValidLocation = true;

            int PreposedX;
            int PreposedY;

            do
            {
                PreposedX = Generate.Next(0, GameBorder.GetLength(0) - 1);
                PreposedY = Generate.Next(0, GameBorder.GetLength(1) - 1);

                for (int i = 0; i < SnakeReff.Count - 1; i++)
                { if(PreposedX == SnakeReff[i].SegmentLocation[0])
                    { if( PreposedY == SnakeReff[i].SegmentLocation[1])
                        { ValidLocation = false; }      
                    }
                        
                }




            } while (ValidLocation == false);
            
            int[] Location = new int[2] {PreposedX,PreposedY};

            return Location;


        }
        public bool HasFoodBeenCaught( Snake Head)
        {  
            bool Result = false;

            if (Head.SegmentLocation[0] == Location[0])
            {
                if (Head.SegmentLocation[1] == Location[1])
                { Result = true; }
            }
            return Result;
        
        }

    }
}
