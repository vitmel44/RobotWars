using Microsoft.Extensions.Configuration;
using RobotWars.Domain;
using RobotWars.Interfaces;

var builder = new ConfigurationBuilder()
	.SetBasePath(Directory.GetCurrentDirectory())
	.AddJsonFile("appsettings.json", optional: false);

IConfiguration config = builder.Build();

var arena = config.GetSection("Arena").Get<Arena>();

if (arena == null)
{
	Console.WriteLine("Arena sould be configured in the appsettings.json file");
	return;
}

string movementInstructions = "MLMRMMMRMMRR";
string initialState = "0,2,E";

IRobot robot = new Robot();
robot.PutOnArena(arena, initialState);

foreach (char instruction in movementInstructions)
{
	robot.SendMovementInstruction(instruction);
}

Console.WriteLine($"Result: location = {robot.GetCurrentLocation()}, Penalties = {robot.GetPenalties()}");

Console.ReadLine();