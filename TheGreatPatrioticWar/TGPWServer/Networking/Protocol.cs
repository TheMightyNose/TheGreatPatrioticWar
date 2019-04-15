using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace TGPWServer
{
	//Reading / Writing 

	//GenericPacket
	/* 		
		Name     Size

	   	Type  -  8 
		Size  -  32 
		Data  -  ?
	*/

	//From
	//C == client
	//S == server
	//B == both
	public enum PacketType
	{
		C_Info,
		C_PlacePacket,
		C_MovePacket,

		B_GenericPacket,
		B_PingPacket,

		S_ConnectionAccepted,
		S_ConnectionRejected,
		S_BoardInfo,
	}


	public static class ExtensionNetworkStream
	{
		static readonly Encoding readWriteEncoding = Encoding.ASCII;

		static PacketType ReadPacketType(this NetworkStream n)
		{
			return (PacketType)n.ReadByte();
		}

		static GenericPacket ReadGenericPacket(this NetworkStream n)
		{
			using (BinaryReader br = new BinaryReader(n, readWriteEncoding, true))
			{
				return new GenericPacket()
				{
					blob = br.ReadBytes(br.ReadInt32()),
				};
			}
		}

		static void WriteGenericPacket(this NetworkStream n, GenericPacket p)
		{
			using (BinaryWriter bw = new BinaryWriter(n, readWriteEncoding, true))
			{
				bw.Write(p.blob.Length);
				bw.Write(p.blob);
			}
		}
	}


	public struct GenericPacket
	{
		public byte[] blob;
	}
}
