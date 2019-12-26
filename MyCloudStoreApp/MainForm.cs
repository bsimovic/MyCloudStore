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

		private void buttonUpload_Click(object sender, EventArgs e)
		{
			Upload f = new Upload(ctrl);
			f.ShowDialog();
		}

		private void buttonDownload_Click(object sender, EventArgs e)
		{
			string filename;
			if (dataView.CurrentRow != null)
			{
				
				filename = dataView.CurrentRow.Cells[0].Value.ToString();
				Download f = new Download(ctrl, filename);
				f.ShowDialog();
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			string filename;
			if (dataView.CurrentRow != null)
			{
				filename = dataView.CurrentRow.Cells[0].Value.ToString();
				DialogResult r = MessageBox.Show("Are you sure you want to delete the selected file?", "Confirm", MessageBoxButtons.YesNo);
				if (r == DialogResult.Yes)
					ctrl.Delete(filename);
			}
		}

		public void ResetView(DataTable data)
		{
			this.dataView.DataSource = data;
			dataView.Refresh();
		}
	}
}
