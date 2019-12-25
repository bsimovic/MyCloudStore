using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using MyCloudStoreApp.MCSService;
using System.ServiceModel;
using MyCloudStoreLib;
using System.Data;
using System.Windows.Forms;

namespace MyCloudStoreApp
{
	public class ClientController
	{
		private MCSServiceClient svc;
		
		public MainForm main { get; set; }

		public string Token { get; private set; }
		public string Username { get; private set; }
		public DataTable Data { get; private set; }

		public ClientController()
		{
			svc = new MCSServiceClient();
			this.Token = "";
			this.Username = "";
			Data = null;
		}

		public void Refresh()
		{
			try
			{
				Data = svc.List(Username, Token);
				main.Display(Data);
			}
			catch (FaultException e)
			{
				
			}
		}

		public string Register(string username, string password)
		{
			try 
			{
				svc.Register(username, password);
				return "You have successfully registered.\nYou can now log in";
			}
			catch (FaultException e)
			{
				return e.Message;
			}	
		}

		public string LogIn(string username, string password)
		{
			try
			{
				this.Token = svc.LogIn(username, password);
				main.LogIn(username);
				this.Username = username;
				this.Refresh();
				
				return "You have successfully logged in.";
			}
			catch (FaultException e)
			{
				return e.Message;
			}
		}

	}
}
