
using Snake_Game.Game;
using Snake_Game.Resorses;

Console.Title = "Snake.";
Console.CursorVisible = false;

int[] GameLocation = new int[2] { 30, 7};
int GameSpeed = 200;
bool WorldLooped = true;

GameLoop Game = new GameLoop(GameLocation[0], GameLocation[1],GameSpeed,WorldLooped);
TestClass Test = new TestClass(GameLocation[0], GameLocation[1], GameSpeed, WorldLooped);

Game.RunStartUp();
Game.RunGame();
//Test.TestSetSnakeMaker();
