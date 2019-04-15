using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace TGPWServer
{
	class Player
	{
		public int id;
		public Global.Faction Faction { get; set; }
		public TcpClient Socket { get; set; }
	}
}
