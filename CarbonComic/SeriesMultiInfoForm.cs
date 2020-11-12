using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace CarbonComic
{
    //This form shows when editing multiple series at once
	public class SeriesMultiInfoForm : Form
	{
		private IContainer components;
		internal TableLayoutPanel TableLayoutPanel1;
		internal Button OK_Button;
		internal Button Cancel_Button;
		internal ComboBox cboType;
		internal Label Label7;
		internal CheckBox chkAuto;
		private Label label1;

		public SeriesMultiInfoForm()
		{
			InitializeComponent();
			OK_Button.Click += OK_Button_Click;
			Cancel_Button.Click += Cancel_Button_Click;
			chkAuto.CheckedChanged += chkAuto_CheckedChanged;
		}

		private void OK_Button_Click(object sender, EventArgs e)
		{
			int num = 0;
			Query query = null;
			Cursor = Cursors.WaitCursor;

            //For each of the selected series
			foreach (int selectedIndex in MainForm.Root.SeriesList.SelectedIndices)
			{
				ComicSeries comicSeries;
                comicSeries = (ComicSeries)CC.Series[selectedIndex - 1];
                //If the checkbox for determining series type is checked
				if (chkAuto.Checked)
				{
                    //get the number of issues in the current series
					query = CC.SQL.ExecQuery("SELECT COUNT(id) FROM issues WHERE series_id=" + Convert.ToString(comicSeries.ID));
					query.NextResult();
					num = (int)query.hash[0];
					query.Close();

                    //If there's only one issue, it's a One-Shot
					if (num == 1)
					{
						cboType.SelectedIndex = 2;
					}
                    //If it's less than 12, it's a limited series
					else if (num <= 12)
					{
						cboType.SelectedIndex = 1;
					}
                    //else, it's a normal series
					else
					{
						cboType.SelectedIndex = 0;
					}
				}

                //save changes
				comicSeries.Type = cboType.SelectedIndex;
				comicSeries.SaveChanges();
			}
			Cursor = Cursors.Default;
			base.DialogResult = DialogResult.OK;
			Close();
		}

		private void Cancel_Button_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			Close();
		}

		private void chkAuto_CheckedChanged(object sender, EventArgs e)
		{
			cboType.Enabled = !chkAuto.Checked;
		}

		[DebuggerNonUserCode]
		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		[System.Diagnostics.DebuggerStepThrough]
		private void InitializeComponent()
		{
			TableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			Cancel_Button = new System.Windows.Forms.Button();
			OK_Button = new System.Windows.Forms.Button();
			cboType = new System.Windows.Forms.ComboBox();
			Label7 = new System.Windows.Forms.Label();
			chkAuto = new System.Windows.Forms.CheckBox();
			label1 = new System.Windows.Forms.Label();
			TableLayoutPanel1.SuspendLayout();
			SuspendLayout();
			TableLayoutPanel1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			TableLayoutPanel1.ColumnCount = 2;
			TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50f));
			TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50f));
			TableLayoutPanel1.Controls.Add(Cancel_Button, 1, 0);
			TableLayoutPanel1.Controls.Add(OK_Button, 0, 0);
			TableLayoutPanel1.Location = new System.Drawing.Point(15, 125);
			TableLayoutPanel1.Name = "TableLayoutPanel1";
			TableLayoutPanel1.RowCount = 1;
			TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50f));
			TableLayoutPanel1.Size = new System.Drawing.Size(146, 29);
			TableLayoutPanel1.TabIndex = 0;
			Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None;
			Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			Cancel_Button.Location = new System.Drawing.Point(76, 3);
			Cancel_Button.Name = "Cancel_Button";
			Cancel_Button.Size = new System.Drawing.Size(67, 23);
			Cancel_Button.TabIndex = 1;
			Cancel_Button.Text = "Cancel";
			OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None;
			OK_Button.Location = new System.Drawing.Point(3, 3);
			OK_Button.Name = "OK_Button";
			OK_Button.Size = new System.Drawing.Size(67, 23);
			OK_Button.TabIndex = 0;
			OK_Button.Text = "OK";
			OK_Button.Click += new System.EventHandler(OK_Button_Click);
			cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			cboType.FormattingEnabled = true;
			cboType.Items.AddRange(new object[3]
			{
				"Series",
				"Limited Series",
				"One-shot"
			});
			cboType.Location = new System.Drawing.Point(13, 25);
			cboType.Name = "cboType";
			cboType.Size = new System.Drawing.Size(147, 21);
			cboType.TabIndex = 39;
			Label7.AutoSize = true;
			Label7.Font = new System.Drawing.Font("Verdana", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			Label7.Location = new System.Drawing.Point(11, 10);
			Label7.Name = "Label7";
			Label7.Size = new System.Drawing.Size(29, 12);
			Label7.TabIndex = 38;
			Label7.Text = "Type";
			chkAuto.AutoSize = true;
			chkAuto.Font = new System.Drawing.Font("Verdana", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			chkAuto.Location = new System.Drawing.Point(13, 52);
			chkAuto.Name = "chkAuto";
			chkAuto.Size = new System.Drawing.Size(102, 16);
			chkAuto.TabIndex = 40;
			chkAuto.Text = "Automatic Type";
			chkAuto.UseVisualStyleBackColor = true;
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("Verdana", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label1.Location = new System.Drawing.Point(11, 71);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(158, 36);
			label1.TabIndex = 41;
			label1.Text = "One-Shot = 1 Issue\r\nLimited Series = 12 or less\r\nSeries = More Than 12 Issues";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(173, 166);
			base.Controls.Add(label1);
			base.Controls.Add(chkAuto);
			base.Controls.Add(cboType);
			base.Controls.Add(Label7);
			base.Controls.Add(TableLayoutPanel1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "SeriesMultiInfoForm";
			base.ShowInTaskbar = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Series Info";
			TableLayoutPanel1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
