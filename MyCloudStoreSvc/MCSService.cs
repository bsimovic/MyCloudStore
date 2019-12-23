using System;
using System.Data;
using System.IO;
using System.ServiceModel;

namespace MyCloudStoreSvc
{
	public class MCSService : IMCSService
	{
		private void CheckToken(string username, string token)
		{
			SQLiteConn conn = SQLiteConn.Instance;
			if (!conn.QueryUser(username))
				throw new FaultException("Username does not exist.");
			string usertoken = conn.GetToken(username);
			if (usertoken != token)
				throw new FaultException("Bad token.");
		}

		public void Register(string username, string password)
		{
			SQLiteConn conn = SQLiteConn.Instance;

			if (conn.QueryUser(username))
				throw new FaultException("Username already exists.");

			conn.InsertUser(username, password);
		}

		public string LogIn(string username, string password)
		{
			SQLiteConn conn = SQLiteConn.Instance;

			if (!conn.QueryUser(username, password))
				throw new FaultException("Wrong password or username does not exist.");

			string token = Guid.NewGuid().ToString();
			conn.SetToken(username, token);

			return token;
		}

		public DataTable List(string username, string token)
		{
			SQLiteConn conn = SQLiteConn.Instance;
			CheckToken(username, token);

			DataTable dt = conn.ListFiles(username);
			
			return dt;
		}

		public void Upload(string username, string filename, string token)
		{
			SQLiteConn conn = SQLiteConn.Instance;
			CheckToken(username, token);
			if (conn.QueryFile(username, filename))
				throw new FaultException("File with that name already exsits.");


			// PRIVREMENO
			byte[] data = new byte[3];
			data[0] = 0;
			data[1] = 1;
			data[2] = 2;
			StoredFile f = new StoredFile(username, filename, 23, "asdfgqwer", data);

			conn.InsertFile(f);
			//---
		}

		public byte[] Download(string username, string filename, string token)
		{
			SQLiteConn conn = SQLiteConn.Instance;
			CheckToken(username, token);
			if (!conn.QueryFile(username, filename))
				throw new FaultException("File does not exist.");

			byte[] data = conn.GetFile(username, filename);
			// PRIVEREMENO

			return data;
			//--
		}

		public void Delete(string username, string token, string filename)
		{
			SQLiteConn conn = SQLiteConn.Instance;
			CheckToken(username, token);
			if (!conn.QueryFile(username, filename))
				throw new FaultException("File does not exist.");

			conn.DeleteFile(username, filename);
		}
	}
}
