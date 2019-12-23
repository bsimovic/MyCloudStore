
namespace MyCloudStoreSvc
{
	class User
	{
		public string username;
		public string passhash;

		public User(string username, string passhash)
		{
			this.username = username;
			this.passhash = passhash;
		}
	}
}
