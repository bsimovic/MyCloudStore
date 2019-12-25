using System.ServiceModel;
using System.Data;
using System.Runtime.Serialization;

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
		void Upload(string username, StoredFile f, string token);

		[OperationContract]
		StoredFile Download(string username, string filename, string token);

		[OperationContract]
		void Delete(string username, string token, string filename);
	}

	[DataContract]
	public class StoredFile
	{ 
		[DataMember]
		public string username; // username vlasnika fajla, zajedno sa imenom fajla cini primarni 

		[DataMember]
		public string filename; // naziv fajla

		[DataMember]
		public int size; // velicina u bajtovima

		[DataMember]
		public string hash; // hash vrednost

		[DataMember]
		public byte[] data; // sadrzaj fajla
	}
}
