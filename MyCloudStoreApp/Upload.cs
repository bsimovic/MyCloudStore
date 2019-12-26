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
	public partial class Upload : Form
	{
		ClientController ctrl;
		string path;
		public Upload(ClientController ctrl)
		{
			this.ctrl = ctrl;
			path = "";
			InitializeComponent();
		}

		private void buttonBrowse_Click(object sender, EventArgs e)
		{
			if (openFileDialog1.ShowDialog() == DialogResult.OK) 
			{
				path = openFileDialog1.FileName;
				label4.Text = openFileDialog1.SafeFileName;
			}		
		}

		private void buttonEncrypt_Click(object sender, EventArgs e)
		{
			string message;
			if (comboBox1.SelectedItem == null || textBox1.Text == "" || path == "")
			{
				message = "You have to fill all fields and choose a file.";
				MessageBox.Show(message, "Info", MessageBoxButtons.OK);
			}
			else
			{
				message = ctrl.Upload(path, openFileDialog1.SafeFileName, textBox1.Text, comboBox1.SelectedIndex);
				MessageBox.Show(message, "Info", MessageBoxButtons.OK);
				this.Close();
			}
		}
	}
}
