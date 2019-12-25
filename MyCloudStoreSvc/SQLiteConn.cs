using System.Data.SQLite;
using System.Data;

namespace MyCloudStoreSvc
{
	class SQLiteConn : ISQLiteConn
	{
		private SQLiteConnection conn;

		private static SQLiteConn instance;
		private static object locker = true;

		// omoguci uzajamno iskljucivanje
		public static SQLiteConn Instance
		{
			get
			{
				lock (locker)
				{
					if (instance == null)
						instance = new SQLiteConn();
				}
				return instance;
			}
		}

		private SQLiteConn()
		{
			conn = new SQLiteConnection("Data Source=C:\\mcsdb\\mcsdb.db;Version=3;");
		}

		public string GetToken(string username)
		{
			conn.Open();

			SQLiteCommand cmd = conn.CreateCommand();
			cmd.CommandText = $"SELECT token FROM User WHERE username='{username}';";
			string token = cmd.ExecuteScalar().ToString();

			conn.Close();

			return token;
		}


		public int InsertFile(StoredFile f)
		{
			conn.Open();

			SQLiteCommand cmd = conn.CreateCommand();
			cmd.CommandText = $"INSERT INTO StoredFile(username, filename, size, hash, data) VALUES ('{f.username}', '{f.filename}', {f.size}, '{f.hash}', @file);";
			cmd.Parameters.Add("@file", DbType.Binary, f.size).Value = f.data;
			int ret = cmd.ExecuteNonQuery();

			conn.Close();

			return ret;
		}

		public DataTable ListFiles(string username)
		{
			conn.Open();

			SQLiteCommand cmd = conn.CreateCommand();
			cmd.CommandText = $"SELECT filename, size, hash FROM StoredFile WHERE username='{username}';";
			SQLiteDataReader r = cmd.ExecuteReader();
			if (r.HasRows)
			{
				DataTable dt = new DataTable();
				dt.Load(r);

				conn.Close();
				r.Close();
				return dt;
			}
			r.Close();
			conn.Close();
			return null;
		}

		public int DeleteFile(string username, string filename)
		{
			conn.Open();

			SQLiteCommand cmd = conn.CreateCommand();
			cmd.CommandText = $"DELETE FROM StoredFile WHERE username='{username}' AND filename='{filename}';";
			int ret = cmd.ExecuteNonQuery();

			conn.Close();

			return ret;
		}

		public bool QueryFile(string username, string filename)
		{
			conn.Open();

			SQLiteCommand cmd = conn.CreateCommand();
			cmd.CommandText = $"SELECT filename FROM StoredFile WHERE username='{username}' AND filename='{filename}';";
			SQLiteDataReader r = cmd.ExecuteReader();
			bool hasRows = r.HasRows;
			r.Close();

			conn.Close();

			return hasRows;
		}

		public StoredFile GetFile(string username, string filename)
		{
			conn.Open();

			SQLiteCommand cmd = conn.CreateCommand();
			cmd.CommandText = $"SELECT * FROM StoredFile WHERE username='{username}' AND filename='{filename}';";
			SQLiteDataReader r = cmd.ExecuteReader();

			r.Read();
			StoredFile sf = new StoredFile();
			sf.username = r.GetString(0);
			sf.filename = r.GetString(1);
			sf.size = r.GetInt32(2);
			sf.hash = r.GetString(3);

			byte[] data = new byte[sf.size];
			r.GetBytes(4, 0, data, 0, sf.size);

			r.Close();
			conn.Close();

			return sf;
		}

		public int InsertUser(string username, string password)
		{
			conn.Open();

			SQLiteCommand cmd = conn.CreateCommand();
			cmd.CommandText = $"INSERT INTO User(username, password, token) VALUES ('{username}', '{password}', NULL);";
			int ret = cmd.ExecuteNonQuery();

			conn.Close();

			return ret;
		}


		public bool QueryUser(string username)
		{
			conn.Open();

			SQLiteCommand cmd = conn.CreateCommand();
			cmd.CommandText = $"SELECT username FROM User WHERE username='{username}';";
			SQLiteDataReader r = cmd.ExecuteReader();
			bool hasRows = r.HasRows;
			r.Close();

			conn.Close();

			return hasRows;
		}

		public bool QueryUser(string username, string password)
		{
			conn.Open();

			SQLiteCommand cmd = conn.CreateCommand();
			cmd.CommandText = $"SELECT username FROM User WHERE username='{username}' AND password='{password}';";
			SQLiteDataReader r = cmd.ExecuteReader();
			bool hasRows = r.HasRows;
			r.Close();

			conn.Close();

			return hasRows;
		}

		public int SetToken(string username, string token)
		{
			conn.Open();

			SQLiteCommand cmd = conn.CreateCommand();
			cmd.CommandText = $"UPDATE User SET token='{token}' WHERE username='{username}';";
			int ret = cmd.ExecuteNonQuery();

			conn.Close();

			return ret;
	}
	}
}
