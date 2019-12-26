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
using System.IO;

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
				if (Data != null)
					main.Display(Data);
				else
				{
					Data = new DataTable();
					main.ResetView(Data);
				}
			}
			catch (FaultException e)
			{
				string msg = e.Message;
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

		public string Upload(string path, string filename, string key, int alg)
		{
			try
			{
				byte[] f = File.ReadAllBytes(path);
				StoredFile sf = new StoredFile();
				sf.username = Username;
				sf.filename = filename;
				sf.hash = MD5.HashString(f);
				switch (alg)
				{
					case 0: 
						sf.data = DoubleTransposition.Encrypt(f, key, sf.hash);
						sf.size = sf.data.Length;
						break;
					case 1: 
						sf.data = XTEA.Encrypt(f, key, sf.hash);
						sf.size = sf.data.Length;
						break;
					default: throw new Exception("You have not chosen an algorithm.");
				}

				svc.Upload(Username, sf, Token);
				Refresh();
				return "File successfuly uploaded.";

			}
			catch (Exception e)
			{
				return e.Message;
			}
		}

		public string Download(string filename, string key, int alg, Download form)
		{
			try
			{
				StoredFile sf = svc.Download(Username, filename, Token);
				byte[] f;
				switch (alg)
				{
					case 0: f = DoubleTransposition.Decrypt(sf.data, key, sf.hash); break;
					case 1: f = XTEA.Decrypt(sf.data, key, sf.hash); break;
					default: throw new Exception("You have not chosen an algorithm.");
				}
				form.Save(filename, f);
				return "File successfuly downloaded.";
			}
			catch (Exception e)
			{
				return e.Message;
			}
		}

		public string Delete(string filename)
		{
			try
			{
				svc.Delete(Username, Token, filename);
				Refresh();
				return "File successfully deleted";
			}
			catch (Exception e)
			{
				return e.Message;
			}
		}
	}
}
