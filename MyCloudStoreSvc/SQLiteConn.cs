using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace MyCloudStoreSvc
{
	class SQLiteConn : ISQLiteConn
	{
		private SQLiteConnection conn;

		public SQLiteConn(string path)
		{
			conn = new SQLiteConnection($"Data Source={path};Version=3;");
		}

		public void InsertFile(StoredFile f)
		{
			conn.Open();

			SQLiteCommand cmd = conn.CreateCommand();
			cmd.CommandText = $"INSERT INTO StoredFile(username, filename, size, hash, data) VALUES ({f.username}, {f.filename}, {f.size}, {f.hash}, @file);";
			cmd.Parameters.Add("@file", DbType.Binary, f.size).Value = f.data;
			cmd.ExecuteNonQuery();

			conn.Close();
		}

		public DataTable ListFiles(string username)
		{

			conn.Open();

			SQLiteCommand cmd = conn.CreateCommand();
			cmd.CommandText = $"SELECT name, size, hash FROM StoredFile WHERE username='{username}';";
			DataTable ds = new DataTable();
			ds.Load(cmd.ExecuteReader());

			conn.Close();

			return ds;
		}

		public void DeleteFile(string username, string filename)
		{
			conn.Open();

			SQLiteCommand cmd = conn.CreateCommand();
			cmd.CommandText = $"DELETE FROM StoredFiles WHERE username={username} AND filename={filename};";
			cmd.ExecuteNonQuery();

			conn.Close();
		}

		public StoredFile GetFile(string username, string filename)
		{
			conn.Open();

			SQLiteCommand cmd = conn.CreateCommand();
			cmd.CommandText = $"SELECT size, hash, data FROM StoredFile WHERE WHERE username={username} AND filename={filename};";
			SQLiteDataReader r = cmd.ExecuteReader();

			int size = r.GetInt32(0);
			string hash = r.GetString(1);
			byte[] data = new byte[size];
			r.GetBytes(2, 0, data, 0, size);

			conn.Close();

			return new StoredFile(username, filename, size, hash, data);
		}

		public void InsertUser(User user)
		{
			conn.Open();

			SQLiteCommand cmd = conn.CreateCommand();
			cmd.CommandText = $"INSERT INTO User(username, passhash) VALUES ({user.username}, {user.passhash});";
			cmd.ExecuteNonQuery();

			conn.Close();

		}

		public void DeleteUser(string username)
		{
			conn.Open();

			SQLiteCommand cmd = conn.CreateCommand();
			cmd.CommandText = $"DELETE FROM User WHERE username={username};";
			cmd.ExecuteNonQuery();

			conn.Close();
		}

		public bool QueryUser(string username, string passhash)
		{
			conn.Open();

			SQLiteCommand cmd = conn.CreateCommand();
			cmd.CommandText = $"SELECT username FROM User WHERE username='{username}' AND passhash='{passhash}';";
			SQLiteDataReader r = cmd.ExecuteReader();
			bool hasRows = r.HasRows;

			conn.Close();

			return hasRows;
		}
	}
}
