using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyCloudStoreApp
{
	public partial class LogIn : Form
	{
		ClientController ctrl;
		public LogIn(ClientController ctrl)
		{
			this.ctrl = ctrl;
			InitializeComponent();
		}

		private void buttonLogin_Click(object sender, EventArgs e)
		{
			string message;
			string caption = "Info";
			if (textBoxUser.Text == "" || textBoxPw.Text == "")
			{
				message = "You have to fill all fields.";
				MessageBox.Show(message, caption, MessageBoxButtons.OK);
			}
			else
			{
				message = ctrl.LogIn(textBoxUser.Text, textBoxPw.Text);
				MessageBox.Show(message, caption, MessageBoxButtons.OK);
				this.Close();
			}
		}
	}
}
