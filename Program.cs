
using Snake_Game.Game;
using Snake_Game.Resorses;

Console.Title = "Snake.";
Console.CursorVisible = false;

int[] GameLocation = new int[2] { 30, 7};
int GameSpeed = 200;
bool WorldLooped = true;

GameSetting Settings;

Menu HomePage = new Menu(GameLocation[0], GameLocation[1]);

Settings = HomePage.Run(GameSpeed,WorldLooped);

GameLoop Game = new GameLoop(GameLocation[0], GameLocation[1],Settings.GameSpeed,Settings.WorldLooped);


//Game.RunGame();

//TestClass Test = new TestClass(GameLocation[0], GameLocation[1], GameSpeed, WorldLooped);
//Test.TestSetSnakeMaker();

internal class GameSetting
{
    public int GameSpeed;
    public bool WorldLooped;

    public GameSetting(int gameSpeed, bool worldLooped)
    { 
        GameSpeed = gameSpeed;
        WorldLooped = worldLooped;
    }
}
