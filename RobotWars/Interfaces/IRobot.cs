using RobotWars.Exceptions;

namespace RobotWars.Interfaces
{
	/// <summary>
	/// Represents a set of robot functionality methods.
	/// </summary>
	public interface IRobot
	{
		/// <summary>
		/// Initializes an arena for robot and set initial position of the robot on the arena.
		/// </summary>
		/// <param name="arena">Arena</param>
		/// <param name="initialPosition">Comma sepparated string with initial position in format {X,Y,CompassPoint}
		/// If null default value "0,0,N"
		/// </param>
		/// <exception cref="ArgumentNullException">Throws if arena is null</exception>
		/// <exception cref="ArgumentNullException">Throws if initial position is incorrect</exception>
		/// <exception cref="OutOfBoundaryException">Throws if initial position is out of the arena size</exception>
		void PutOnArena(IArena arena, string? initialPosition);

		/// <summary>
		/// Sends movement instruction for the robot
		/// </summary>
		/// <param name="movementInstruction">Char represents movement instruction.
		///     Valid istructions:
		///      'L' and 'R' make the robot spin 90 degrees to the left or right representively.
		///      'M'  - move forvard one grid
		/// </param>
		void SendMovementInstruction(char movementInstruction);

		/// <summary>
		/// Gets current location of the robot
		/// </summary>
		/// <returns>String with current location in format X, Y, CompassPoint</returns>
		string GetCurrentLocation();


		/// <summary>
		/// Gets number of penalties
		/// </summary>
		/// <returns>Number of penalties</returns>
		int GetPenalties();
	}
}
