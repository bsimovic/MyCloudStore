using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyCloudStoreApp
{
	static class Program
	{
		[STAThread]
		static void Main()
		{
			ClientController ctrl = new ClientController();
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm(ctrl));
		}
	}
}