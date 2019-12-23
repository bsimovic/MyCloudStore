using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;

namespace MyCloudStoreApp
{
	class ClientController
	{
		public string Address { get; private set; }
		public short Port { get; private set; }

		public long CheckConnection()
		{
			Ping ping = new Ping();
			PingReply reply = ping.Send(Address);
			return reply.RoundtripTime;
		}
	}
}
