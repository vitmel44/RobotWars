using RobotWars.Interfaces;

namespace RobotWars.Domain
{
	/// <inheritdoc cref="IArena" />
	public class Arena : IArena
	{
		/// <inheritdoc />
		public int Width { get; set; }

		/// <inheritdoc />
		public int Height { get; set; }
	}
}
