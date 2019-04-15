using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace TGPWServer
{
	static class Server
	{
		static readonly List<Player> players = new List<Player>();
		static TcpListener listener;

		//> Wait for players
		//> Stop waiting for players 
		//> Wait for players to send player info

		public static void Setup()
		{
			Logger.Log("Server", "Starting server...");
			listener = new TcpListener(IPAddress.Parse("0.0.0.0"),Settings.Current.server_port);
			listener.Start();
		}
	}
}
