namespace MyCloudStoreApp
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.loginButton = new System.Windows.Forms.Button();
			this.registerButton = new System.Windows.Forms.Button();
			this.dataView = new System.Windows.Forms.DataGridView();
			this.buttonUpload = new System.Windows.Forms.Button();
			this.buttonDownload = new System.Windows.Forms.Button();
			this.buttonRefresh = new System.Windows.Forms.Button();
			this.labelStatus = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.dataView)).BeginInit();
			this.SuspendLayout();
			// 
			// loginButton
			// 
			this.loginButton.Location = new System.Drawing.Point(15, 4);
			this.loginButton.Name = "loginButton";
			this.loginButton.Size = new System.Drawing.Size(75, 23);
			this.loginButton.TabIndex = 1;
			this.loginButton.Text = "Log in";
			this.loginButton.UseVisualStyleBackColor = true;
			this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
			// 
			// registerButton
			// 
			this.registerButton.Location = new System.Drawing.Point(96, 4);
			this.registerButton.Name = "registerButton";
			this.registerButton.Size = new System.Drawing.Size(75, 23);
			this.registerButton.TabIndex = 2;
			this.registerButton.Text = "Register";
			this.registerButton.UseVisualStyleBackColor = true;
			this.registerButton.Click += new System.EventHandler(this.registerButton_Click);
			// 
			// dataView
			// 
			this.dataView.AllowUserToAddRows = false;
			this.dataView.AllowUserToDeleteRows = false;
			this.dataView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dataView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataView.Enabled = false;
			this.dataView.Location = new System.Drawing.Point(15, 33);
			this.dataView.Name = "dataView";
			this.dataView.ReadOnly = true;
			this.dataView.Size = new System.Drawing.Size(509, 212);
			this.dataView.TabIndex = 3;
			// 
			// buttonUpload
			// 
			this.buttonUpload.Enabled = false;
			this.buttonUpload.Location = new System.Drawing.Point(368, 254);
			this.buttonUpload.Name = "buttonUpload";
			this.buttonUpload.Size = new System.Drawing.Size(75, 23);
			this.buttonUpload.TabIndex = 8;
			this.buttonUpload.Text = "Upload";
			this.buttonUpload.UseVisualStyleBackColor = true;
			// 
			// buttonDownload
			// 
			this.buttonDownload.Enabled = false;
			this.buttonDownload.Location = new System.Drawing.Point(449, 254);
			this.buttonDownload.Name = "buttonDownload";
			this.buttonDownload.Size = new System.Drawing.Size(75, 23);
			this.buttonDownload.TabIndex = 9;
			this.buttonDownload.Text = "Download";
			this.buttonDownload.UseVisualStyleBackColor = true;
			// 
			// buttonRefresh
			// 
			this.buttonRefresh.Enabled = false;
			this.buttonRefresh.Location = new System.Drawing.Point(449, 4);
			this.buttonRefresh.Name = "buttonRefresh";
			this.buttonRefresh.Size = new System.Drawing.Size(75, 23);
			this.buttonRefresh.TabIndex = 11;
			this.buttonRefresh.Text = "Refresh";
			this.buttonRefresh.UseVisualStyleBackColor = true;
			this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
			// 
			// labelStatus
			// 
			this.labelStatus.AutoSize = true;
			this.labelStatus.Location = new System.Drawing.Point(12, 259);
			this.labelStatus.Name = "labelStatus";
			this.labelStatus.Size = new System.Drawing.Size(111, 13);
			this.labelStatus.TabIndex = 12;
			this.labelStatus.Text = "You are not logged in.";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(539, 290);
			this.Controls.Add(this.labelStatus);
			this.Controls.Add(this.buttonRefresh);
			this.Controls.Add(this.buttonDownload);
			this.Controls.Add(this.buttonUpload);
			this.Controls.Add(this.dataView);
			this.Controls.Add(this.registerButton);
			this.Controls.Add(this.loginButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainForm";
			this.Text = "MyCloudStore";
			((System.ComponentModel.ISupportInitialize)(this.dataView)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button loginButton;
		private System.Windows.Forms.Button registerButton;
		private System.Windows.Forms.DataGridView dataView;
		private System.Windows.Forms.Button buttonUpload;
		private System.Windows.Forms.Button buttonDownload;
		private System.Windows.Forms.Button buttonRefresh;
		private System.Windows.Forms.Label labelStatus;
	}
}

