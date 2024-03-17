using Snake_Game.Resorses;
using Snake_Game.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Snake_Game.Game
{
    internal class Menu
    {
        int GameX;
        int GameY;

        List<MenuData> MenuList;
        
        Resorses.Resorses Reff;
        Methods Method;


        public Menu( int[] GameLocation)
        {
            GameX = GameLocation[0];
            GameY = GameLocation[1];

            Reff = new Resorses.Resorses();
            Method = new Methods(GameX,GameY,true);

            MenuList = BuildMenuList();

        } 


        private List<MenuData> BuildMenuList()
        {
            string[] MainMenuFields = new string[] {"Start Game","Settings","Quit" };
            MenuData MainMenu = new MenuData("MainMenu.",null,MainMenuFields,null);

            string[] SettingsMenuFields = new string[] {"Loop Map.","Game Speed." };
            MenuData SettingsMenu = new MenuData("Settings Menu.","Press Enter for MainMenu.",SettingsMenuFields,0);

            MenuList = new List<MenuData>() { MainMenu, SettingsMenu} ;

            return MenuList;
        }

        public GameSetting Run(GameSetting Settings)
        {
            int MenuIndex = 0;
            int SelectionIndex = 0;
            int GameSpeedIndex = 4;
            
            ConsoleKey Input;

            DrawPage();

           
            bool MenuFinished = false;

            while (!MenuFinished)
            {
                if (MenuIndex == 0)
                {
                    DrawCurrentMenuState(MenuIndex, SelectionIndex);

                    Input = Method.GetInput();


                    if (Input == ConsoleKey.Enter)
                    {
                        if (MenuIndex == 0)
                        {

                            if (SelectionIndex == 0)
                            { MenuFinished = true; Settings.StartGame = true; }
                            else if (SelectionIndex == 1) { if (MenuIndex == 0) { MenuIndex = SelectionIndex; } }
                            else { MenuFinished = true; Settings.ApplicationRunning = false; }
                            SelectionIndex = 0;
                        }
                        else if (MenuIndex == 1) { MenuIndex = 0; }
                    }
                    SelectionIndex = HandelPrimaryInput(SelectionIndex, MenuList[MenuIndex], Input); 

                }
                else if (MenuIndex == 1)
                {
                    
                    DrawCurrentMenuStateSettings(SelectionIndex,GameSpeedIndex, Settings);

                    Input = Method.GetInput();
                    
                    if (Input == ConsoleKey.Enter)
                    {
                        SelectionIndex = 0;
                        MenuIndex = 0; 
                    }

                    SelectionIndex = HandelPrimaryInput(SelectionIndex, MenuList[1],Input);
                   
                    GameSpeedIndex = HandelGameSpeedInput(GameSpeedIndex,SelectionIndex,Input);
                    Settings.GameSpeed = UpdateGameSpeed(Settings,GameSpeedIndex);
                    
                    Settings.WorldLooped = UpdateWorldLooped(Settings,SelectionIndex,Input);
                }

            }
            Method.DrawArray(Reff.Border,GameX,GameY);
            return Settings;
        }
       
        private int HandelPrimaryInput( int SelectionIndex, MenuData Data , ConsoleKey Input )
        {

            switch(Input)
            {
                case ConsoleKey.UpArrow: { SelectionIndex = SelectionIndex-1; }break;
                case ConsoleKey.DownArrow: {  SelectionIndex = SelectionIndex+1; }break;
            }
           
            int Result = IndexLoopCheck(Data,SelectionIndex);

            return Result;

        }
        private int HandelGameSpeedInput( int GameSpeedIndex, int SelectIndex, ConsoleKey Input)
        {
            if (SelectIndex == 0)
            {
                if(Input == ConsoleKey.LeftArrow)
                { 
                    GameSpeedIndex = GameSpeedIndex - 1; 
                    if(GameSpeedIndex < 1) { GameSpeedIndex = 1; }
                }
                if (Input == ConsoleKey.RightArrow)
                {
                    GameSpeedIndex = GameSpeedIndex + 1;
                    if (GameSpeedIndex > 7) { GameSpeedIndex = 7; }
                }

            }
            return GameSpeedIndex;
        }
        private int UpdateGameSpeed(GameSetting Settings, int GameSpeedIndex)
        {
            int GameSpeed = 200;

            switch(GameSpeedIndex)
            {
                case 1: { GameSpeed = 350; } break;
                case 2: { GameSpeed = 300; } break;
                case 3: { GameSpeed = 250; } break;
                case 4: { GameSpeed = 200; } break;
                case 5: { GameSpeed = 150; } break;
                case 6: { GameSpeed = 100; } break;
                case 7: { GameSpeed = 50;  } break;
            }

            return GameSpeed;

        }
        private bool UpdateWorldLooped(GameSetting Settings, int SelectionIndex,ConsoleKey Input)
        {
            bool Result = Settings.WorldLooped;

            if (SelectionIndex == 1)
            {
                if (Input == ConsoleKey.LeftArrow)
                { Result = true; }
                if(Input == ConsoleKey.RightArrow)
                { Result = false; }
            }
            return Result;
        }
        private int IndexLoopCheck(MenuData Data, int PreposedSelectionIndex)
        {
            if ( PreposedSelectionIndex < 0) { PreposedSelectionIndex = Data.Fields.Length - 1; }
            if ( PreposedSelectionIndex >Data.Fields.Length-1 ) { PreposedSelectionIndex = 0; }

            return PreposedSelectionIndex;
        }
        private void DrawCurrentMenuState( int MenuIndex,int SelectionIndex)
        {  
          
                Method.DrawArray(Reff.Border, GameX, GameY);
                WrightMenu(MenuIndex,SelectionIndex);

        }
        private void DrawCurrentMenuStateSettings( int SelectionIndex, int GameSpeedIndex, GameSetting Settings)
        {
            Method.DrawArray(Reff.Border, GameX, GameY);
            WrightMenuSettings( SelectionIndex, GameSpeedIndex, Settings );
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
            WrightString("                   ", 6, -2);
            WrightString(MenuList[MenuIndex].Title, CenterText(MenuList[MenuIndex].Title.Length,Reff.Headder.GetLength(1)) , - 2);

            int LoopCount = 0;
            string[] Indicator = {">* "," *<" };
            
            if(MenuList[MenuIndex].PagePrompt != null)
            { WrightString(MenuList[MenuIndex].PagePrompt,3,9); }

            foreach (string MenuField in MenuList[MenuIndex].Fields) 
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
        private void WrightMenuSettings( int SelectionIndex, int GameSpeedIndex, GameSetting Settings )
        {
            WrightString("                   ", 6, -2);
            WrightString(MenuList[1].Title, CenterText(MenuList[1].Title.Length, Reff.Headder.GetLength(1)), -2);

            

            string[] Indicator = { ">* ", " *<" };
            
            string GameSpeedReff =  GameSpeedIndex.ToString();

            if (MenuList[1].PagePrompt != null) { WrightString(MenuList[1].PagePrompt, 3, 9); }

            if (SelectionIndex == 0)
            {
                GameSpeedReff = Indicator[0] + MenuList[1].Fields[1]+"Level."+GameSpeedIndex + Indicator[1];
                WrightString(GameSpeedReff, 3, 3); 
            }
            else { WrightString(MenuList[1].Fields[1] + "Level." + GameSpeedIndex, 6, 3); }

            if(SelectionIndex == 1)
            {
                WrightString(Indicator[0] + MenuList[1].Fields[0] + "      " + Indicator[1], 4, 5);
                if (Settings.WorldLooped) { Console.ForegroundColor = ConsoleColor.Green; }
                WrightString("On", 16, 5);
                if(Settings.WorldLooped) { Console.ResetColor();  }
                WrightString("/", 18, 5);
                if (!Settings.WorldLooped) { Console.ForegroundColor = ConsoleColor.Red; }
                WrightString("Off", 19, 5);
                if (!Settings.WorldLooped) { Console.ResetColor(); }
            }
            else 
            {
                WrightString(MenuList[1].Fields[0], 7, 5);
                if (Settings.WorldLooped) { Console.ForegroundColor = ConsoleColor.Green; }
                WrightString("On", 16, 5);
                if (Settings.WorldLooped) { Console.ResetColor(); }
                WrightString("/", 18, 5);
                if (!Settings.WorldLooped) { Console.ForegroundColor = ConsoleColor.Red; }
                WrightString("Off", 19, 5);
                if (!Settings.WorldLooped) { Console.ResetColor(); }
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
            public string? PagePrompt;
            public int? SettingSelectionIndex;
 
            public MenuData(string title, string? pagePrompt ,string[] fields,int? settingSelectionIndex)
            {
                Title = title;
                Fields = fields;
                PagePrompt = pagePrompt;
                SettingSelectionIndex = settingSelectionIndex;
            }
        }
   
    }
}
