namespace MyCloudStoreApp
{
	partial class Register
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxUser = new System.Windows.Forms.TextBox();
			this.textBoxPw1 = new System.Windows.Forms.TextBox();
			this.textBoxPw2 = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.buttonRegister = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(58, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Username:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Password:";
			// 
			// textBoxUser
			// 
			this.textBoxUser.Location = new System.Drawing.Point(111, 6);
			this.textBoxUser.Name = "textBoxUser";
			this.textBoxUser.Size = new System.Drawing.Size(136, 20);
			this.textBoxUser.TabIndex = 2;
			// 
			// textBoxPw1
			// 
			this.textBoxPw1.Location = new System.Drawing.Point(111, 29);
			this.textBoxPw1.Name = "textBoxPw1";
			this.textBoxPw1.PasswordChar = '*';
			this.textBoxPw1.Size = new System.Drawing.Size(136, 20);
			this.textBoxPw1.TabIndex = 3;
			// 
			// textBoxPw2
			// 
			this.textBoxPw2.Location = new System.Drawing.Point(111, 52);
			this.textBoxPw2.Name = "textBoxPw2";
			this.textBoxPw2.PasswordChar = '*';
			this.textBoxPw2.Size = new System.Drawing.Size(136, 20);
			this.textBoxPw2.TabIndex = 4;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 55);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(93, 13);
			this.label3.TabIndex = 5;
			this.label3.Text = "Confirm password:";
			// 
			// buttonRegister
			// 
			this.buttonRegister.Location = new System.Drawing.Point(13, 78);
			this.buttonRegister.Name = "buttonRegister";
			this.buttonRegister.Size = new System.Drawing.Size(234, 23);
			this.buttonRegister.TabIndex = 6;
			this.buttonRegister.Text = "Register";
			this.buttonRegister.UseVisualStyleBackColor = true;
			this.buttonRegister.Click += new System.EventHandler(this.buttonRegister_Click);
			// 
			// Register
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(258, 112);
			this.Controls.Add(this.buttonRegister);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textBoxPw2);
			this.Controls.Add(this.textBoxPw1);
			this.Controls.Add(this.textBoxUser);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Register";
			this.Text = "Register";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxUser;
		private System.Windows.Forms.TextBox textBoxPw1;
		private System.Windows.Forms.TextBox textBoxPw2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button buttonRegister;
	}
}