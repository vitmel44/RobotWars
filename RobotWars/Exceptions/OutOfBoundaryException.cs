using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotWars.Exceptions
{
	public class OutOfBoundaryException : Exception
	{
		public OutOfBoundaryException(string message)
			: base(message)
		{

		}
	}
}
