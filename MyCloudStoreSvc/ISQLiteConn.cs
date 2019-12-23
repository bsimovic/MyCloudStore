using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCloudStoreSvc
{
	interface ISQLiteConn
	{
		void InsertFile(StoredFile f);
		DataTable ListFiles(string username);
		void DeleteFile(string username, string filename);
		StoredFile GetFile(string username, string filename);

		void InsertUser(User user);
		void DeleteUser(string username);
		bool QueryUser(string username, string passhash);
	}
}
