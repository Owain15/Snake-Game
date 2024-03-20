

using Newtonsoft.Json;
using Snake_Game.Game;
using Snake_Game.Resorses;
using Snake_Game.Tools;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;

Console.Title = "Snake.";
Console.CursorVisible = false;

int[] GameLocation = new int[2] { 30, 7 };
int GameSpeed = 300;
int GameSpeedIndex = 4;
bool WorldLooped = false;

bool SaveHighScore = false;

GameSetting Settings = new GameSetting(GameSpeed,GameSpeedIndex,WorldLooped);
SaveData Save = new SaveData();

Menu HomePage = new Menu(GameLocation);
//HomePage.GetName(240);
while (Settings.ApplicationRunning)
{
        HighScoreCard[] TopThreeScores = new HighScoreCard[3];
        TopThreeScores = Save.GetSavedScores();

    Settings = HomePage.Run(Settings);

    
    if (Settings.StartGame) 
    {
        GameLoop Game = new GameLoop(GameLocation, Settings);
        
        int Score = Game.RunGame();

        

        if (Score > TopThreeScores[2].Score)
        {
            SaveHighScore = true;

            HighScoreCard NewHighScore = new HighScoreCard(HomePage.GetName(Score), Score, Settings.GameSpeedIndex, Settings.WorldLooped);
            
            if (Score > TopThreeScores[1].Score)
            { 
             if(Score > TopThreeScores[0].Score) 
             {
                    TopThreeScores[2] = TopThreeScores[1];
                    TopThreeScores[1] = TopThreeScores[0];
                    TopThreeScores[0] = NewHighScore;
             }
                else 
                { 
                    TopThreeScores[2] = TopThreeScores[1];
                    TopThreeScores[1] = NewHighScore;
                }
            }
            else { TopThreeScores[2] = NewHighScore; }
        }
    }
    if (SaveHighScore) { SaveHighScore = false; Save.SaveTopThreeInJason(TopThreeScores); }
    Settings.StartGame = false;
}

Environment.Exit(0);


internal class GameSetting
{
    public int GameSpeed;
    public int GameSpeedIndex;
    public bool WorldLooped;
    public bool ApplicationRunning;
    public bool StartGame;

    public GameSetting(int gameSpeed,int gameSpeedIndex, bool worldLooped)
    { 
        GameSpeed = gameSpeed;
        GameSpeedIndex = gameSpeedIndex;
        WorldLooped = worldLooped;
        ApplicationRunning = true;
        StartGame = false;
    }
}
internal class HighScoreCard
{
    public string Name;
    public int Score;
    public int GameSpeedInedx;
    public bool WorldLooped;

    public HighScoreCard(string name, int score, int gameSpeedInedx, bool worldLooped)
    {
        Name = name;
        Score = score;
        GameSpeedInedx = gameSpeedInedx;
        WorldLooped = worldLooped;
    }
}
