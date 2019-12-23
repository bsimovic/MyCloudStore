using System.ServiceModel;
using System.IO;
using System.Data;

namespace MyCloudStoreSvc
{
	[ServiceContract]
	public interface IMCSService
	{
		[OperationContract]
		void Register(string username, string password);

		[OperationContract]
		string LogIn(string username, string password);

		[OperationContract]
		DataTable List(string username, string token);

		[OperationContract]
		void Upload(string username, string filename, string token);

		[OperationContract]
		byte[] Download(string username, string filename, string token);

		[OperationContract]
		void Delete(string username, string token, string filename);
	}
}
