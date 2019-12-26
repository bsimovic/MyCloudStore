using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyCloudStoreApp
{
	public partial class Download : Form
	{
		ClientController ctrl;
		string filename;
		public Download(ClientController ctrl, string filename)
		{
			this.ctrl = ctrl;
			this.filename = filename;
			InitializeComponent();
			label4.Text = filename;
		}

		private void buttonDownload_Click(object sender, EventArgs e)
		{
			string message;
			if (comboBox1.SelectedItem == null || textBox1.Text == "" || filename == "")
			{
				message = "You have to fill all fields and choose a file.";
				MessageBox.Show(message, "Info", MessageBoxButtons.OK);
			}
			else
			{
				message = ctrl.Download(filename, textBox1.Text, comboBox1.SelectedIndex, this);
				MessageBox.Show(message, "Info", MessageBoxButtons.OK);
				this.Close();
			}
		}

		public void Save(string filename, byte[] data)
		{
			Stream str;
			if (saveFileDialog1.ShowDialog() == DialogResult.OK)
			{
				if ((str = saveFileDialog1.OpenFile()) != null)
				{
					str.Write(data, 0, data.Length);
					str.Close();
				}
			}
		}
	}
}
