using Snake_Game.Resorses;
using Snake_Game.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Game.Game
{
    internal class Menu
    {
        int GameX;
        int GameY;

        List<MenuData> Data;
        
        Resorses.Resorses Reff;
        Methods Method;

        public Menu(int gameX, int gameY)
        {
            GameX = gameX;
            GameY = gameY;

            Reff = new Resorses.Resorses();
            Method = new Methods(GameX,GameY,true);

            string[] MainMenuFields = new string[] {"Start Game","Settings","Quit" };
            MenuData MainMenu = new MenuData("MainMenu.",MainMenuFields);

            string[] SettingsMenuFields = new string[] {"Loop Map. On/Off.","Game Speed.","Main Menu" };
            MenuData SettingsMenu = new MenuData("Settings Menu.",SettingsMenuFields );

            Data = new List<MenuData>() { MainMenu, SettingsMenu} ;

        } 

        public GameSetting Run( int GameSpeed, bool WorldLooped)
        {

            GameSetting Settings = new GameSetting(GameSpeed, WorldLooped);

            DrawPage();
            
            RunMenu(WorldLooped,GameSpeed);
           
          
            Console.Read();

            return Settings;
        }

        private void RunMenu(bool WorldLooped, int GameSpeed)
        {
            int MenuIndex = 0;
            bool SelectionMade = false;

            while (!SelectionMade)
            {
                GetMenu(MenuIndex);

            }
           

        }
        private int GetMenu(int MenuIndex) 
        {
            
         switch(MenuIndex) 
         {
            case 0: { MenuIndex = MainMenu(MenuIndex); }break;
         }
            return MenuIndex;
        }
        private int HandelPrimaryMenuInput( MenuData Data, ConsoleKey Input )
        {
            int Result = 0;

            switch(Input)
            {
                case ConsoleKey.UpArrow: { Result = Data.SelectionIndex-1; }break;
                case ConsoleKey.DownArrow: {  Result = Data.SelectionIndex+1; }break;
            }
           
            Result= IndexLoopCheck(Data,Result);

            return Result;

        }
        private int IndexLoopCheck(MenuData Data, int PreposedSelectionIndex)
        {
            if ( PreposedSelectionIndex < 0) { PreposedSelectionIndex = Data.Fields.Length - 1; }
            if ( PreposedSelectionIndex >Data.Fields.Length-1 ) { PreposedSelectionIndex = 0; }

            return PreposedSelectionIndex;
        }
        private int MainMenu(int MenuIndex)
        {
            int Result;
            bool SelectionMade = false;
            ConsoleKey Input;

            while (!SelectionMade)
            {
                Method.DrawArray(Reff.Border, GameX, GameY);
                WrightMenu(MenuIndex, Data[MenuIndex].SelectionIndex);
                Input = Method.GetInput();
                Data[MenuIndex].SelectionIndex = HandelPrimaryMenuInput(Data[MenuIndex], Input );
                
            }
            Result = Data[MenuIndex].SelectionIndex;
            return Result;
        }
        private void DrawPage()
        {
            Method.DrawArray(Reff.Headder,GameX,GameY-4);
            Method.DrawArray(Reff.Border,GameX,GameY);
            WrightString("Snake.", 12, -3);
        }
        private void WrightString(string Input, int X, int Y) 
        {
            Console.SetCursorPosition(GameX+X,GameY+Y);
            Console.WriteLine(Input);
        }
        private void WrightMenu(int MenuIndex, int SelectionIndex)
        {
            WrightString(Data[MenuIndex].Title, CenterText(Data[MenuIndex].Title.Length,Reff.Headder.GetLength(1)) , - 2);

            int LoopCount = 0;
            string[] Indicator = {">* "," *<" };

            foreach (string MenuField in Data[MenuIndex].Fields) 
            {
                if (LoopCount == SelectionIndex)
                {
                    int OffSetY = 3 + (LoopCount * 2);
                    WrightString(Indicator[0] + MenuField + Indicator[1], 
                        CenterText(MenuField.Length + Indicator[0].Length + Indicator[1].Length, Reff.Border.GetLength(1)), OffSetY);
                }
                else
                {
                    int OffSetY = 3 + (LoopCount * 2);
                    WrightString(MenuField, CenterText(MenuField.Length, Reff.Border.GetLength(1)),OffSetY);

                }
                

                LoopCount ++;
            }

        }
        private int CenterText(int StringLength, int RowLength)
        {
            
            int HalfString = StringLength / 2;
            int HalfRow = RowLength / 2;

            int Result = HalfRow - HalfString;

            return Result;

        }
        internal class MenuData
        {
            public string Title;
            public string[] Fields;
            public int SelectionIndex;

            public MenuData(string title, string[] fields)
            {
                Title = title;
                Fields = fields;
                SelectionIndex = 0;

            }
        }

    }
}
