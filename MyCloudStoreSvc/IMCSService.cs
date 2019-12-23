using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.IO;
using System.Text;

namespace MyCloudStoreSvc
{
	[ServiceContract]
	public interface IMCSService
	{
		[OperationContract]
		string List(string name);

		[OperationContract]
		int Upload(Stream stream);

		[OperationContract]
		byte[] Download(Stream stream);
	}
}
