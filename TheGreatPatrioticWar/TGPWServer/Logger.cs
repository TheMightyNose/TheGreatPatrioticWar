using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGPWServer
{
	static class Logger
	{
		public static void Log(string catagory, string message)
		{
			Console.WriteLine($"{catagory}: {message}");
		}
	}
}
