using Snake_Game.Resorses;
using Snake_Game.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
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
        SaveData Save;


        public Menu( int[] GameLocation)
        {
            GameX = GameLocation[0];
            GameY = GameLocation[1];

            Reff = new Resorses.Resorses();
            Method = new Methods(GameX,GameY,true);
            Save = new SaveData();

            MenuList = BuildMenuList();

        } 


        private List<MenuData> BuildMenuList()
        {
            string ReturnPrompt = "Press Enter for MainMenu";

            string[] MainMenuFields = new string[] {"Start Game","Settings","High Scores","Quit" };
            MenuData MainMenu = new MenuData("MainMenu",null,MainMenuFields);

            string[] SettingsMenuFields = new string[] {"Loop Map.","Game Speed." };
            MenuData SettingsMenu = new MenuData("Settings Menu",ReturnPrompt,SettingsMenuFields);

            string[]HighScoresFilds = new string[] {"Name","Score","Speed","Loop"};
            MenuData HighScoreMenu = new MenuData("High Scores", ReturnPrompt, HighScoresFilds);

            MenuList = new List<MenuData>() { MainMenu, SettingsMenu,HighScoreMenu} ;

            return MenuList;
        }

        public GameSetting Run(GameSetting Settings)
        {
            int MenuIndex = 0;
            int SelectionIndex = 0;
            
            
            ConsoleKey Input;

            DrawPage();

           
            bool MenuFinished = false;


            while (!MenuFinished)
            {
                if (MenuIndex == 0)
                {
                    WrightMenuMain(SelectionIndex);

                    Input = Method.GetInput();


                    if (Input == ConsoleKey.Enter)
                    {
                        if (MenuIndex == 0)
                        {

                            if (SelectionIndex == 0)
                            { MenuFinished = true; Settings.StartGame = true; }
                            else if (SelectionIndex == 1) { MenuIndex = SelectionIndex; } 
                            else if (SelectionIndex == 2) { MenuIndex = SelectionIndex; }
                            else { MenuFinished = true; Settings.ApplicationRunning = false; }
                            SelectionIndex = 0;
                        }
                        else if (MenuIndex == 1) { MenuIndex = 0; }
                    }
                    SelectionIndex = HandelPrimaryInput(SelectionIndex, MenuList[MenuIndex], Input); 

                }
                else if (MenuIndex == 1)
                {
                    
                    WrightMenuSettings(SelectionIndex,Settings.GameSpeedIndex,Settings);

                    Input = Method.GetInput();
                    
                    if (Input == ConsoleKey.Enter)
                    {
                        SelectionIndex = 0;
                        MenuIndex = 0; 
                    }

                    SelectionIndex = HandelPrimaryInput(SelectionIndex, MenuList[1],Input);

                    if (SelectionIndex == 0)
                    {
                        Settings.GameSpeedIndex = HandelGameSpeedInput(Settings.GameSpeedIndex, Input);
                        Settings.GameSpeed = UpdateGameSpeed(Settings.GameSpeedIndex);
                    }
                    if (SelectionIndex == 1) { Settings.WorldLooped = UpdateWorldLooped(Settings.WorldLooped, Input); }
                }
                else if (MenuIndex == 2)
                {
                    WrightMenuScores(Save.GetSavedScores());

                    Input = Method.GetInput();

                    if (Input == ConsoleKey.Enter)
                    {
                        SelectionIndex = 0;
                        MenuIndex = 0;
                    }
                    if (Input == ConsoleKey.Escape)
                    {
                        bool Check = false;
                        Check = CheckReset();
                        if (Check) { Save.ResetHighScore(); } 

                    }
                }

            }
           ClearPage();
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
        private int HandelGameSpeedInput( int GameSpeedIndex, ConsoleKey Input)
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

            
            return GameSpeedIndex;
        }
        private int UpdateGameSpeed(int GameSpeedIndex)
        {
            int GameSpeed = 200;

            switch(GameSpeedIndex)
            {
                case 1: { GameSpeed = 200; } break;
                case 2: { GameSpeed = 175; } break;
                case 3: { GameSpeed = 150; } break;
                case 4: { GameSpeed = 125; } break;
                case 5: { GameSpeed = 100; } break;
                case 6: { GameSpeed = 75; } break;
                case 7: { GameSpeed = 50;  } break;
            }

            return GameSpeed;

        }
        private bool UpdateWorldLooped(bool Worldlooped,ConsoleKey Input)
        {
           
                if (Input == ConsoleKey.LeftArrow)
                { Worldlooped = true; }
                if(Input == ConsoleKey.RightArrow)
                { Worldlooped = false; }
            
            return Worldlooped;
        }
        private int IndexLoopCheck(MenuData Data, int PreposedSelectionIndex)
        {
            if ( PreposedSelectionIndex < 0) { PreposedSelectionIndex = Data.Fields.Length - 1; }
            if ( PreposedSelectionIndex >Data.Fields.Length-1 ) { PreposedSelectionIndex = 0; }

            return PreposedSelectionIndex;
        }
        private void ClearPage()
        {  
          Method.DrawArray(Reff.Border, GameX, GameY);

            WrightString("                   ", 6, -2);
            Method.DrawArray(Reff.Border,GameX,GameY);
              
        }
        private void DrawPage()
        {
            string Title = "Snake";
            Method.DrawArray(Reff.Headder,GameX,GameY-4);
            Method.DrawArray(Reff.Border,GameX,GameY);
            WrightString(Title, CenterText(Title.Length,Reff.Headder.GetLength(1)), -3);
        }
        private void WrightString(string Input, int X, int Y) 
        {
            Console.SetCursorPosition(GameX+X,GameY+Y);
            Console.WriteLine(Input);
        }
        private void WrightTitle(int MenuIndex)
        { WrightString(MenuList[MenuIndex].Title, CenterText(MenuList[MenuIndex].Title.Length,Reff.Border.GetLength(1)) , -2); }
        private void WrightMenuMain(int SelectionIndex)
        {
            ClearPage();

            WrightTitle(0);

            int LoopCount = 0;
            string[] Indicator = {">* "," *<" };
            
            //if(MenuList[0].PagePrompt != null)
            //{ WrightString(MenuList[1].PagePrompt,3,9); }

            foreach (string MenuField in MenuList[0].Fields) 
            {

                if (LoopCount == SelectionIndex)
                {
                    int OffSetY = 2 + (LoopCount * 2);
                    WrightString(Indicator[0] + MenuField + Indicator[1], 
                        CenterText(MenuField.Length + Indicator[0].Length + Indicator[1].Length, Reff.Border.GetLength(1)), OffSetY);
                }
                else
                {
                    int OffSetY = 2 + (LoopCount * 2);
                    WrightString(MenuField, CenterText(MenuField.Length, Reff.Border.GetLength(1)),OffSetY);

                }
                

                LoopCount ++;
            }

        }
        private void WrightMenuSettings( int SelectionIndex, int GameSpeedIndex, GameSetting Settings )
        {
            ClearPage();

            WrightTitle(1);

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
        private void WrightMenuScores(HighScoreCard[] TopThreeScours)
        {
            ClearPage();

            WrightTitle(2);

            HighScoreSetUp();

            HighScoreFillIn(TopThreeScours);

            WrightString(MenuList[2].PagePrompt, 3, 9);
            WrightString("Esc To Reset.", 9, 10);
        }
        private void HighScoreSetUp()
        {
            WrightString(MenuList[2].Fields[0], 6,  1);
            WrightString(MenuList[2].Fields[1], 12, 1);
            WrightString(MenuList[2].Fields[2], 19, 1);
            WrightString(MenuList[2].Fields[3], 25, 1);

            WrightString("1", 2, 3);
            WrightString("2", 2, 5);
            WrightString("3", 2, 7);
        }
        private bool CheckReset()
        {
            bool Result = false;

            ClearPage();

            string Title = "Reset Check";
            
            string Prompt1 = "Are You Sure";
            string Prompt2 = "All Data Will Be Lost";

            WrightString(Title, CenterText(Title.Length,Reff.Border.GetLength(1)), -2);

            WrightString("WARNING!", CenterText(8, Reff.Border.GetLength(1)), 2);

            WrightString(Prompt1,CenterText(Prompt1.Length,Reff.Border.GetLength(1)),4);
            WrightString(Prompt2, CenterText(Prompt2.Length,Reff.Border.GetLength(1)), 5);
            WrightString("Enter To Choose", CenterText(15, Reff.Border.GetLength(1)), 8);

            ConsoleKey Input = ConsoleKey.None;

            while (Input != ConsoleKey.Enter)
            {
                if (!Result) { Console.ForegroundColor = ConsoleColor.Green; }
                WrightString("Keep ", 7, 6);
                if (!Result) { Console.ResetColor(); }
                WrightString("/", 12, 6);
                if (Result) { Console.ForegroundColor = ConsoleColor.Red; }
                WrightString("Delete All", 14, 6);
                if (Result) { Console.ResetColor(); }

                Input = Method.GetInput();

                if(Input == ConsoleKey.LeftArrow) { Result = false; }
                if (Input == ConsoleKey.RightArrow) { Result = true; }
            }
            return Result;
        }
        private void HighScoreFillIn(HighScoreCard[] TopThreeScores)
        {
            int Row = 3;

           foreach (HighScoreCard Card in TopThreeScores)
            {
                WrightString(Card.Name, CenterText(Card.Name.Length,9)+4, Row);
                WrightString(Card.Score.ToString(), CenterText(Card.Score.ToString().Length + 1, 5) + 14, Row);//14
                WrightString(Card.GameSpeedInedx.ToString(), 21, Row);
                string LoopResult = GetWorldLoopString(Card.WorldLooped);
                WrightString(LoopResult, CenterText(LoopResult.Length + 1, 5) + 25, Row);

                Row+=2;
            }

        }
        private string GetWorldLoopString(bool WorledLooped)
        {
            string Result = " ";
           
            if (WorledLooped) { Result = "On"; }
            if (!WorledLooped) { Result = "Off"; }

            return Result;
        }
        
        private int CenterText(int StringLength, int RowLength)
        {
            
            int HalfString = StringLength / 2;
            int HalfRow = RowLength / 2;

            int Result = HalfRow - HalfString;

            return Result;

        }
        public string GetName(int Score)

        {
            string Name = "No Name";
            string ClearBox = "         ";

            string Title = "New High Score";
            DrawPage();
            ClearPage();

            WrightString(Title, CenterText(Title.Length, Reff.Border.GetLength(1)), -2);

            WrightString("Score." + Score, 11, 2);

            WrightString("Type Name, Then Press Enter.", 2, 4);
            WrightString("No Longer Than 9 Characters.", 2, 10);

            Method.DrawArray(Reff.NameBox, GameX + 10, GameY + 6);
            
            ConsoleKey Input = ConsoleKey.UpArrow;
            List<ConsoleKeyInfo> NameLetters = new List<ConsoleKeyInfo>();  

            while(Input != ConsoleKey.Enter)
            {
                if(NameLetters.Count<9) 
                {
                    ConsoleKeyInfo Letter = Console.ReadKey(true);
                   
                    if(Letter.Key == ConsoleKey.Backspace)
                    { NameLetters.RemoveAt(NameLetters.Count - 1); }
                    else { if (Letter.Key != ConsoleKey.Enter) { NameLetters.Add(Letter); } }
                    
                    Input = Letter.Key;

                    Name = ListToString(NameLetters);
                    WrightString(ClearBox, 11, 7);
                    WrightString(Name, CenterText(Name.Length,9)+11, 7);
                }
                else 
                {
                    Input = Console.ReadKey(true).Key;
                    if (Input == ConsoleKey.Backspace) 
                    {
                        NameLetters.RemoveAt(NameLetters.Count - 1);
                        Name = ListToString(NameLetters);
                        WrightString(ClearBox, 11, 7);
                        WrightString(Name, CenterText(Name.Length, 9) + 11, 7);
                    }
                   // if (InputCheck == ConsoleKey.Enter) { Input = ConsoleKey.Enter; } 
                }
            }

            return Name;

        }
        private string ListToString(List<ConsoleKeyInfo> Letters)
        {
            string Result = " ";
            
            int Length = Letters.Count;//-1
           
            //if (Length == 0) { Result = Letters[0].Key.ToString().ToUpper(); }
            //else
            //{
                for (int i = 0; i < Length; i++)
                {
                    if (i == 0) { Result = Letters[0].Key.ToString().ToUpper(); }
                    else { Result = Result + Letters[i].Key.ToString().ToLower(); }
                }
           // }
            
             
            return Result;
        }



        internal class MenuData
        {
            public string Title;
            public string[] Fields;
            public string? PagePrompt;
            public int? SettingSelectionIndex;
 
            public MenuData(string title, string? pagePrompt ,string[] fields)
            {
                Title = title;
                Fields = fields;
                PagePrompt = pagePrompt;
              
            }
        }
   
    }
}
