using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CarbonComic
{
    //Just a window with the application logo, nothing special.
	public class AboutForm : Form
	{
		private IContainer components;
		private PictureBox pictureBox1;
		private Label label1;
        private Label label2;
		private Label label3;
		private Label label4;
		private Label label5;
		private Button button1;

		public AboutForm()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CarbonComic.AboutForm));
			label1 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			button1 = new System.Windows.Forms.Button();
			pictureBox1 = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			SuspendLayout();
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label1.Location = new System.Drawing.Point(124, 15);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(149, 13);
			label1.TabIndex = 1;
			label1.Text = "Carbon Comic Alpha 1";
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(130, 28);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(116, 13);
			label2.TabIndex = 2;
			label2.Text = "Digital Comic Organizer";
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(130, 77);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(108, 13);
			label3.TabIndex = 3;
			label3.Text = "nonzzero@gmail.com";
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(130, 64);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(72, 13);
			label4.TabIndex = 4;
			label4.Text = "Jared Collums";
			label5.AutoSize = true;
			label5.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label5.Location = new System.Drawing.Point(124, 51);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(78, 13);
			label5.TabIndex = 5;
			label5.Text = "Created By";
			button1.Location = new System.Drawing.Point(24, 72);
			button1.Name = "button1";
			button1.Size = new System.Drawing.Size(75, 23);
			button1.TabIndex = 6;
			button1.Text = "OK";
			button1.UseVisualStyleBackColor = true;
			button1.Click += new System.EventHandler(button1_Click);
			pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			pictureBox1.Image = (System.Drawing.Image)resources.GetObject("pictureBox1.Image");
			pictureBox1.Location = new System.Drawing.Point(12, 12);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new System.Drawing.Size(100, 69);
			pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			pictureBox1.TabIndex = 0;
			pictureBox1.TabStop = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(282, 107);
			base.Controls.Add(label3);
			base.Controls.Add(button1);
			base.Controls.Add(label4);
			base.Controls.Add(label2);
			base.Controls.Add(label1);
			base.Controls.Add(pictureBox1);
			base.Controls.Add(label5);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "AboutForm";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "About";
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
