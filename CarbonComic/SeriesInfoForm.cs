using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace CarbonComic
{
	public class SeriesInfoForm : Form
	{
		private MainForm owner;
		private int SeriesIndex;
		private IContainer components;
		internal TableLayoutPanel TableLayoutPanel1;
		internal Button OK_Button;
		internal Button Cancel_Button;
		internal ComboBox cboType;
		internal Label Label7;
		internal Label Label1;
		internal TextBox txtName;
		internal TableLayoutPanel TableLayoutPanel3;
		internal Button cmdPrev;
		internal Button cmdNext;

		public SeriesInfoForm()
		{
			InitializeComponent();
		}

		private void OK_Button_Click(object sender, EventArgs e)
		{
			try
			{
				SaveChanges();
				base.DialogResult = DialogResult.OK;
				Close();
			}
			catch (LogException ex)
			{
				MessageBox.Show(ex.Message, "Series Info", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		private void Cancel_Button_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			Close();
		}

		/// <summary>
		/// Load series info into the form
		/// </summary>
		/// <param name="index">The id of the series to load</param>
		private void LoadSeries(int index)
		{
			SeriesIndex = index;
			ComicSeries comicSeries = (ComicSeries)CC.Series[index];
			cboType.SelectedIndex = comicSeries.Type;
			txtName.Text = comicSeries.Name;
			cmdPrev.Enabled = (index != 0);
			cmdNext.Enabled = (index != CC.Series.Count - 1);
		}

		//Load next series
		private void cmdNext_Click(object sender, EventArgs e)
		{
			try
			{
				SaveChanges();
				LoadSeries(SeriesIndex + 1);
			}
			catch (LogException ex)
			{
				MessageBox.Show(ex.Message, "Series Info", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		//Load previous series
		private void cmdPrev_Click(object sender, EventArgs e)
		{
			try
			{
				SaveChanges();
				LoadSeries(SeriesIndex - 1);
			}
			catch (LogException ex)
			{
				MessageBox.Show(ex.Message, "Series Info", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		//Save changes to the series
		private void SaveChanges()
		{
			ComicSeries comicSeries = (ComicSeries)CC.Series[SeriesIndex];
			comicSeries.Name = txtName.Text;
			comicSeries.Type = cboType.SelectedIndex;
			if (comicSeries.SaveChanges())
			{
				owner.SeriesList.Refresh();
			}
		}

		//Load the series that is selected in the issue llist
		private void SeriesInfoForm_Load(object sender, EventArgs e)
		{
			owner = (MainForm)base.Owner;
			LoadSeries(owner.SeriesList.SelectedIndices[0] - 1);
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
			Label1 = new System.Windows.Forms.Label();
			txtName = new System.Windows.Forms.TextBox();
			TableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			cmdPrev = new System.Windows.Forms.Button();
			cmdNext = new System.Windows.Forms.Button();
			TableLayoutPanel1.SuspendLayout();
			TableLayoutPanel3.SuspendLayout();
			SuspendLayout();
			TableLayoutPanel1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			TableLayoutPanel1.ColumnCount = 2;
			TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50f));
			TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50f));
			TableLayoutPanel1.Controls.Add(Cancel_Button, 1, 0);
			TableLayoutPanel1.Controls.Add(OK_Button, 0, 0);
			TableLayoutPanel1.Location = new System.Drawing.Point(171, 61);
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
			Cancel_Button.Click += new System.EventHandler(Cancel_Button_Click);
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
			cboType.Location = new System.Drawing.Point(173, 25);
			cboType.Name = "cboType";
			cboType.Size = new System.Drawing.Size(144, 21);
			cboType.TabIndex = 22;
			Label7.AutoSize = true;
			Label7.Font = new System.Drawing.Font("Verdana", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			Label7.Location = new System.Drawing.Point(170, 9);
			Label7.Name = "Label7";
			Label7.Size = new System.Drawing.Size(33, 12);
			Label7.TabIndex = 21;
			Label7.Text = "Type:";
			Label1.AutoSize = true;
			Label1.Font = new System.Drawing.Font("Verdana", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			Label1.Location = new System.Drawing.Point(12, 9);
			Label1.Name = "Label1";
			Label1.Size = new System.Drawing.Size(70, 12);
			Label1.TabIndex = 20;
			Label1.Text = "Series Name:";
			txtName.Location = new System.Drawing.Point(12, 26);
			txtName.Name = "txtName";
			txtName.Size = new System.Drawing.Size(146, 20);
			txtName.TabIndex = 19;
			TableLayoutPanel3.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			TableLayoutPanel3.ColumnCount = 2;
			TableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50f));
			TableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50f));
			TableLayoutPanel3.Controls.Add(cmdPrev, 0, 0);
			TableLayoutPanel3.Controls.Add(cmdNext, 1, 0);
			TableLayoutPanel3.Location = new System.Drawing.Point(12, 61);
			TableLayoutPanel3.Name = "TableLayoutPanel3";
			TableLayoutPanel3.RowCount = 1;
			TableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50f));
			TableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29f));
			TableLayoutPanel3.Size = new System.Drawing.Size(146, 29);
			TableLayoutPanel3.TabIndex = 23;
			cmdPrev.Anchor = System.Windows.Forms.AnchorStyles.None;
			cmdPrev.Location = new System.Drawing.Point(3, 3);
			cmdPrev.Name = "cmdPrev";
			cmdPrev.Size = new System.Drawing.Size(67, 23);
			cmdPrev.TabIndex = 0;
			cmdPrev.Text = "Previous";
			cmdPrev.Click += new System.EventHandler(cmdPrev_Click);
			cmdNext.Anchor = System.Windows.Forms.AnchorStyles.None;
			cmdNext.Location = new System.Drawing.Point(76, 3);
			cmdNext.Name = "cmdNext";
			cmdNext.Size = new System.Drawing.Size(67, 23);
			cmdNext.TabIndex = 1;
			cmdNext.Text = "Next";
			cmdNext.Click += new System.EventHandler(cmdNext_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(327, 102);
			base.Controls.Add(TableLayoutPanel3);
			base.Controls.Add(cboType);
			base.Controls.Add(Label7);
			base.Controls.Add(Label1);
			base.Controls.Add(txtName);
			base.Controls.Add(TableLayoutPanel1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "SeriesInfoForm";
			base.ShowInTaskbar = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Series Info";
			base.Load += new System.EventHandler(SeriesInfoForm_Load);
			TableLayoutPanel1.ResumeLayout(false);
			TableLayoutPanel3.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
