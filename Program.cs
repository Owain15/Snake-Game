
using Snake_Game.Game;
using Snake_Game.Resorses;

Console.Title = "Snake.";
Console.CursorVisible = false;

int[] GameLocation = new int[2] { 30, 7 };
int GameSpeed = 300;
bool WorldLooped = true;

GameSetting Settings = new GameSetting(GameSpeed,WorldLooped);

Menu HomePage = new Menu(GameLocation);

while (Settings.ApplicationRunning)
{
    Settings = HomePage.Run(Settings);

    
    if (Settings.StartGame) 
    {
        GameLoop Game = new GameLoop(GameLocation, Settings);
        Game.RunGame();
    }
    Settings.StartGame = false;
}

Environment.Exit(0);
        

//Game.RunGame();


//TestClass Test = new TestClass(GameLocation[0], GameLocation[1], GameSpeed, WorldLooped);
//Test.TestSetSnakeMaker();

internal class GameSetting
{
    public int GameSpeed;
    public bool WorldLooped;
    public bool ApplicationRunning;
    public bool StartGame;

    public GameSetting(int gameSpeed, bool worldLooped)
    { 
        GameSpeed = gameSpeed;
        WorldLooped = worldLooped;
        ApplicationRunning = true;
        StartGame = false;
    }
}
