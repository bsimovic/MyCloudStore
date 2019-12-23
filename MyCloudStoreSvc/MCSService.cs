using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MyCloudStoreSvc
{
	public class MCSService : IMCSService
	{
		private bool logged;
		private string username;

		public string List(string name)
		{
			throw new NotImplementedException();
		}

		public byte[] Download(Stream stream)
		{
			throw new NotImplementedException();
		}

		public int Upload(Stream stream)
		{
			throw new NotImplementedException();
		}
	}
}
