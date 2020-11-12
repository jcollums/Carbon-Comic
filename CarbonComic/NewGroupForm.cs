using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CarbonComic
{
    //This form create a new group
	public class NewGroupForm : Form
	{
		private IContainer components;
		private Label label1;
		private TextBox txtName;
		private Label label2;
		private ComboBox cboPublisher;
		private Button cmdOK;
		private Button cmdCancel;

		public NewGroupForm()
		{
			InitializeComponent();
		}

		private void NewGroupForm_Load(object sender, EventArgs e)
		{
            //load list of publishers into the combobox
			Query query = CC.SQL.ExecQuery("SELECT * FROM publishers ORDER BY name");
			while (query.NextResult())
			{
				cboPublisher.Items.Add(query.hash["name"]);
			}
			txtName.Focus();
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			try
			{
				ComicGroup comicGroup = new ComicGroup();
				comicGroup.Name = txtName.Text;
			}
			catch (LogException ex)
			{
				base.DialogResult = DialogResult.None;
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
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
			label1 = new System.Windows.Forms.Label();
			txtName = new System.Windows.Forms.TextBox();
			label2 = new System.Windows.Forms.Label();
			cboPublisher = new System.Windows.Forms.ComboBox();
			cmdOK = new System.Windows.Forms.Button();
			cmdCancel = new System.Windows.Forms.Button();
			SuspendLayout();
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("Verdana", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label1.Location = new System.Drawing.Point(12, 9);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(37, 12);
			label1.TabIndex = 0;
			label1.Text = "Name:";
			txtName.Location = new System.Drawing.Point(15, 24);
			txtName.Name = "txtName";
			txtName.Size = new System.Drawing.Size(164, 20);
			txtName.TabIndex = 1;
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("Verdana", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label2.Location = new System.Drawing.Point(12, 51);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(55, 12);
			label2.TabIndex = 2;
			label2.Text = "Publisher:";
			cboPublisher.FormattingEnabled = true;
			cboPublisher.Location = new System.Drawing.Point(15, 66);
			cboPublisher.Name = "cboPublisher";
			cboPublisher.Size = new System.Drawing.Size(164, 21);
			cboPublisher.TabIndex = 3;
			cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			cmdOK.Location = new System.Drawing.Point(15, 103);
			cmdOK.Name = "cmdOK";
			cmdOK.Size = new System.Drawing.Size(77, 23);
			cmdOK.TabIndex = 4;
			cmdOK.Text = "OK";
			cmdOK.UseVisualStyleBackColor = true;
			cmdOK.Click += new System.EventHandler(cmdOK_Click);
			cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			cmdCancel.Location = new System.Drawing.Point(101, 103);
			cmdCancel.Name = "cmdCancel";
			cmdCancel.Size = new System.Drawing.Size(78, 23);
			cmdCancel.TabIndex = 5;
			cmdCancel.Text = "Cancel";
			cmdCancel.UseVisualStyleBackColor = true;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(190, 138);
			base.Controls.Add(cmdCancel);
			base.Controls.Add(cmdOK);
			base.Controls.Add(cboPublisher);
			base.Controls.Add(label2);
			base.Controls.Add(txtName);
			base.Controls.Add(label1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "NewGroupForm";
			base.ShowInTaskbar = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "New Group";
			base.Load += new System.EventHandler(NewGroupForm_Load);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
