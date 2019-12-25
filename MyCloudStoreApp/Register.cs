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
	public partial class Register : Form
	{
		private ClientController ctrl;
		public Register(ClientController ctrl)
		{
			this.ctrl = ctrl;
			InitializeComponent();
		}

		private void buttonRegister_Click(object sender, EventArgs e)
		{
			string message;
			string caption = "Info";
			if (textBoxUser.Text == "" || textBoxPw1.Text == "" || textBoxPw2.Text == "")
			{
				message = "You have to fill all fields.";
				MessageBox.Show(message, caption, MessageBoxButtons.OK);
			}
			else if (textBoxPw1.Text != textBoxPw2.Text)
			{
				message = "Passwords have to match.";
				MessageBox.Show(message, caption, MessageBoxButtons.OK);
			}
			else
			{
				message = ctrl.Register(textBoxUser.Text, textBoxPw1.Text);
				MessageBox.Show(message, caption, MessageBoxButtons.OK);
				this.Close();
			}
			
		}
	}
}
