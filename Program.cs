
using Snake_Game.Game;

Console.Title = "Snake.";
Console.CursorVisible = false;

int[] GameLocation = new int[2] { 30, 7};
int GameSpeed = 200;
bool WorldLooped = false;

GameLoop Game = new GameLoop(GameLocation[0], GameLocation[1],GameSpeed,WorldLooped);

//Game.RunStartUp();
Game.RunGame();
