using System.Data;

namespace MyCloudStoreSvc
{
	interface ISQLiteConn
	{
		string GetToken(string username); // vraca username za dati token, null u suprotnom

		int InsertFile(StoredFile f); // unesi fajl u bazu
		DataTable ListFiles(string username); // izlistiraj fajlove datog korisnika
		int DeleteFile(string username, string filename); // obrisi fajl iz baze
		StoredFile GetFile(string username, string filename); // uzmi fajl iz baze
		bool QueryFile(string username, string filename);

		int InsertUser(string username, string password); // ubaci korisnika u bazu
		bool QueryUser(string username); // proveri da li postoji korisnik sa tim username-om
		bool QueryUser(string username, string password); // proveri da li postoji korisnik sa tim username i password
		int SetToken(string username, string token); // generisi novi token za korisniak
	}
}
