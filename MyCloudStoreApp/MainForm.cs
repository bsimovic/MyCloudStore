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
	public partial class MainForm : Form
	{
		private ClientController ctrl;
		public MainForm(ClientController ctrl)
		{
			this.ctrl = ctrl;
			ctrl.main = this;
			InitializeComponent();
		}

		private void registerButton_Click(object sender, EventArgs e)
		{
			Register f = new Register(ctrl);
			f.ShowDialog();
		}

		private void loginButton_Click(object sender, EventArgs e)
		{
			LogIn f = new LogIn(ctrl);
			f.ShowDialog();
		}

		public void LogIn(string username)
		{
			this.loginButton.Enabled = false;
			this.dataView.Enabled = true;
			this.buttonDownload.Enabled = true;
			this.buttonUpload.Enabled = true;
			this.buttonDownload.Enabled = true;
			this.buttonRefresh.Enabled = true;
			this.labelStatus.Text = $"You are logged in as {username}.";
		}

		public void Display(DataTable data)
		{
			data.Columns["filename"].ColumnName = "Name";
			data.Columns["size"].ColumnName = "Size (in bytes)";
			data.Columns["hash"].ColumnName = "MD5 Hash of original";
			this.dataView.DataSource = data;
		}

		private void buttonRefresh_Click(object sender, EventArgs e)
		{
			ctrl.Refresh();
		}
	}
}
