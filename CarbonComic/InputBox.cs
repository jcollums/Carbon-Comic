using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CarbonComic
{
	//This is a general-purpose InputBox implementation
	public class InputBox : Form
	{
		private IContainer components;
		private TextBox txtInput;
		private Label lblPrompt;
		private Button cmdCancel;
		private Button cmdOK;

		public InputBox()
		{
			InitializeComponent();
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			txtInput.Text = "";
			Close();
		}

		private void InputBox_Load(object sender, EventArgs e)
		{
			Focus();
			txtInput.Focus();
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			Close();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			txtInput = new System.Windows.Forms.TextBox();
			lblPrompt = new System.Windows.Forms.Label();
			cmdCancel = new System.Windows.Forms.Button();
			cmdOK = new System.Windows.Forms.Button();
			SuspendLayout();
			txtInput.Location = new System.Drawing.Point(15, 24);
			txtInput.Name = "txtInput";
			txtInput.Size = new System.Drawing.Size(262, 20);
			txtInput.TabIndex = 1;
			lblPrompt.AutoSize = true;
			lblPrompt.Font = new System.Drawing.Font("Verdana", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			lblPrompt.Location = new System.Drawing.Point(12, 9);
			lblPrompt.Name = "lblPrompt";
			lblPrompt.Size = new System.Drawing.Size(52, 12);
			lblPrompt.TabIndex = 2;
			lblPrompt.Text = "[Prompt]";
			cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			cmdCancel.Location = new System.Drawing.Point(210, 50);
			cmdCancel.Name = "cmdCancel";
			cmdCancel.Size = new System.Drawing.Size(67, 23);
			cmdCancel.TabIndex = 3;
			cmdCancel.Text = "Cancel";
			cmdCancel.UseVisualStyleBackColor = true;
			cmdCancel.Click += new System.EventHandler(cmdCancel_Click);
			cmdOK.Location = new System.Drawing.Point(141, 50);
			cmdOK.Name = "cmdOK";
			cmdOK.Size = new System.Drawing.Size(63, 23);
			cmdOK.TabIndex = 5;
			cmdOK.Text = "OK";
			cmdOK.UseVisualStyleBackColor = true;
			cmdOK.Click += new System.EventHandler(cmdOK_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(293, 82);
			base.Controls.Add(cmdOK);
			base.Controls.Add(txtInput);
			base.Controls.Add(cmdCancel);
			base.Controls.Add(lblPrompt);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "InputBox";
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "[Title]";
			base.TopMost = true;
			base.Load += new System.EventHandler(InputBox_Load);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
