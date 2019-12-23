
namespace MyCloudStoreSvc
{
	class StoredFile
	{
		public string username; // username vlasnika fajla, zajedno sa imenom fajla cini primarni kljuc
		public string filename;	// naziv fajla
		public int size;        // velicina u bajtovima
		public string hash;     // hash vrednost
		public byte[] data;     // sadrzaj fajla

		public StoredFile(string username, string filename, int size, string hash, byte[] data)
		{
			this.username = username;
			this.filename = filename;
			this.size = size;
			this.hash = hash;
			this.data = data;
		}
	}
}
