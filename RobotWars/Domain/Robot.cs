using RobotWars.Exceptions;
using RobotWars.Interfaces;
using RobotWars.ValueObjects;

namespace RobotWars.Domain;

/// <inheritdoc cref="IRobot" />
public class Robot : IRobot
{
	private int _xCoordinate;
	private int _yCoordinate;
	private CompassPoints _compassPoint;
	private int _penaltyCount;
	private IArena? _arena;

	/// <inheritdoc />
	public void PutOnArena(IArena arena, string? initialPosition)
	{
		_arena = arena ?? throw new ArgumentNullException(nameof(arena));

		if (string.IsNullOrWhiteSpace(initialPosition))
		{
			initialPosition = "0,0,N";
		}

		string[] splitInitialPosition = initialPosition.Split(',').Select(x => x.Trim()).ToArray();

		if (splitInitialPosition.Length < 3)
		{
			throw new ArgumentException(nameof(initialPosition), "Initial position is incorrect");
		}

		if (!int.TryParse(splitInitialPosition[0], out _xCoordinate) ||
			!int.TryParse(splitInitialPosition[1], out _yCoordinate) ||
			!Enum.TryParse(splitInitialPosition[2], out _compassPoint))
		{
			throw new ArgumentException(nameof(initialPosition), "Initial position is incorrect");
		}

		if (IsBoundaryCollision(_xCoordinate, _yCoordinate))
		{
			throw new OutOfBoundaryException("Initial position is out of arena size");
		}
	}

	/// <inheritdoc />
	public string GetCurrentLocation()
	{
		if (_arena is null)
		{
			return "Robot is not on the arena";
		}

		return $"{_xCoordinate}, {_yCoordinate}, {_compassPoint}";
	}

	/// <inheritdoc />
	public int GetPenalties()
	{
		return _penaltyCount;
	}

	/// <inheritdoc />
	public void SendMovementInstruction(char movementInstruction)
	{
		if (_arena is null)
		{
			throw new Exception("Robot is not on arena");
		}

		string validMovementInstructions = "LRM";

		if (!validMovementInstructions.Contains(movementInstruction))
		{
			throw new ArgumentException(nameof(movementInstruction), "Only L, R, M characters are valid movement instructions");
		}

		switch (movementInstruction)
		{
			case 'M':
				this.Move();
				break;
			case 'R':
				this.TurnRight();
				break;
			case 'L':
				this.TurnLeft();
				break;
			default:
				Console.WriteLine("Instruction not recognized");
				break;
		}
	}

	private void ChangeCoordinates(int newX, int newY)
	{
		if (IsBoundaryCollision(newX, newY))
		{
			_penaltyCount++;
			Console.WriteLine("Boundary collision. Penalty points added");
			return;
		}

		_xCoordinate = newX;
		_yCoordinate = newY;
	}

	private bool IsBoundaryCollision(int newX, int newY)
	{
		return (newX < 0 || newX > _arena.Width - 1) || (newY < 0 || newY > _arena.Height - 1);
	}

	private void MakeTurn(int turnStep)
	{
		int compassPoint = (int)_compassPoint;
		compassPoint += turnStep;

		if (compassPoint is > 3)
		{
			compassPoint = 0;
		}

		if (compassPoint < 0)
		{
			compassPoint = 3;
		}

		_compassPoint = (CompassPoints)compassPoint;
	}

	private void TurnRight()
	{
		MakeTurn(1);
	}

	private void TurnLeft()
	{
		MakeTurn(-1);
	}

	private void Move()
	{
		switch (_compassPoint)
		{
			case CompassPoints.N:
				ChangeCoordinates(_xCoordinate, _yCoordinate + 1);
				break;
			case CompassPoints.E:
				ChangeCoordinates(_xCoordinate + 1, _yCoordinate);
				break;
			case CompassPoints.S:
				ChangeCoordinates(_xCoordinate, _yCoordinate - 1);
				break;
			case CompassPoints.W:
				ChangeCoordinates(_xCoordinate - 1, _yCoordinate);
				break;
			default:
				Console.WriteLine("I'm lost. Where to move?");
				break;
		}
	}
}