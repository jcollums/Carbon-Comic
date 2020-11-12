using CarbonComic.Properties;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace CarbonComic
{
    //This form handles setting information for multiple issues at once
	public class IssueMultiForm : Form
	{
		private IContainer components;

		internal TableLayoutPanel TableLayoutPanel1;

		internal Button OK_Button;

		internal Button Cancel_Button;

		internal ComboBox cboType;

		internal DateTimePicker datePublished;

		internal ComboBox cboSeries;

		internal NumericUpDown udVol;

		internal Label Label1;

		internal Label Label6;

		internal Label Label5;

		internal Label Label7;

		internal Label Label4;

		internal CheckBox chkStory;

		internal CheckBox chkSeries;

		internal CheckBox chkVol;

		internal CheckBox chkPublished;

		internal CheckBox chkType;

		internal TextBox txtPlot;

		internal Label Label2;

		internal ComboBox cboAutoDate;

		private CheckBox chkNumber;

		private NumericUpDown udStart;

		private Label label3;

		internal Label label8;

		private TextBox txtComments;

		internal CheckBox chkComments;

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
			datePublished = new System.Windows.Forms.DateTimePicker();
			cboSeries = new System.Windows.Forms.ComboBox();
			udVol = new System.Windows.Forms.NumericUpDown();
			txtPlot = new System.Windows.Forms.TextBox();
			Label1 = new System.Windows.Forms.Label();
			Label6 = new System.Windows.Forms.Label();
			Label5 = new System.Windows.Forms.Label();
			Label7 = new System.Windows.Forms.Label();
			Label4 = new System.Windows.Forms.Label();
			chkStory = new System.Windows.Forms.CheckBox();
			chkSeries = new System.Windows.Forms.CheckBox();
			chkVol = new System.Windows.Forms.CheckBox();
			chkPublished = new System.Windows.Forms.CheckBox();
			chkType = new System.Windows.Forms.CheckBox();
			Label2 = new System.Windows.Forms.Label();
			cboAutoDate = new System.Windows.Forms.ComboBox();
			chkNumber = new System.Windows.Forms.CheckBox();
			udStart = new System.Windows.Forms.NumericUpDown();
			label3 = new System.Windows.Forms.Label();
			label8 = new System.Windows.Forms.Label();
			txtComments = new System.Windows.Forms.TextBox();
			chkComments = new System.Windows.Forms.CheckBox();
			TableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)udVol).BeginInit();
			((System.ComponentModel.ISupportInitialize)udStart).BeginInit();
			SuspendLayout();
			TableLayoutPanel1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			TableLayoutPanel1.ColumnCount = 2;
			TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50f));
			TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50f));
			TableLayoutPanel1.Controls.Add(Cancel_Button, 1, 0);
			TableLayoutPanel1.Controls.Add(OK_Button, 0, 0);
			TableLayoutPanel1.Location = new System.Drawing.Point(213, 253);
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
				"Issue",
				"Annual",
				"Special"
			});
			cboType.Location = new System.Drawing.Point(253, 100);
			cboType.Name = "cboType";
			cboType.Size = new System.Drawing.Size(105, 21);
			cboType.TabIndex = 29;
			datePublished.Location = new System.Drawing.Point(28, 101);
			datePublished.Name = "datePublished";
			datePublished.Size = new System.Drawing.Size(187, 20);
			datePublished.TabIndex = 27;
			datePublished.ValueChanged += new System.EventHandler(datePublished_ValueChanged);
			cboSeries.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			cboSeries.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cboSeries.FormattingEnabled = true;
			cboSeries.Location = new System.Drawing.Point(28, 61);
			cboSeries.Name = "cboSeries";
			cboSeries.Size = new System.Drawing.Size(187, 21);
			cboSeries.TabIndex = 26;
			udVol.Location = new System.Drawing.Point(254, 62);
			udVol.Maximum = new decimal(new int[4]
			{
				10000,
				0,
				0,
				0
			});
			udVol.Name = "udVol";
			udVol.Size = new System.Drawing.Size(104, 20);
			udVol.TabIndex = 24;
			txtPlot.Location = new System.Drawing.Point(28, 25);
			txtPlot.Name = "txtPlot";
			txtPlot.Size = new System.Drawing.Size(334, 20);
			txtPlot.TabIndex = 19;
			Label1.AutoSize = true;
			Label1.Font = new System.Drawing.Font("Verdana", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			Label1.Location = new System.Drawing.Point(25, 9);
			Label1.Name = "Label1";
			Label1.Size = new System.Drawing.Size(52, 12);
			Label1.TabIndex = 20;
			Label1.Text = "Plot Title";
			Label6.AutoSize = true;
			Label6.Font = new System.Drawing.Font("Verdana", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			Label6.Location = new System.Drawing.Point(25, 48);
			Label6.Name = "Label6";
			Label6.Size = new System.Drawing.Size(35, 12);
			Label6.TabIndex = 25;
			Label6.Text = "Series";
			Label5.AutoSize = true;
			Label5.Font = new System.Drawing.Font("Verdana", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			Label5.Location = new System.Drawing.Point(252, 48);
			Label5.Name = "Label5";
			Label5.Size = new System.Drawing.Size(42, 12);
			Label5.TabIndex = 23;
			Label5.Text = "Volume";
			Label7.AutoSize = true;
			Label7.Font = new System.Drawing.Font("Verdana", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			Label7.Location = new System.Drawing.Point(251, 85);
			Label7.Name = "Label7";
			Label7.Size = new System.Drawing.Size(29, 12);
			Label7.TabIndex = 28;
			Label7.Text = "Type";
			Label4.AutoSize = true;
			Label4.Font = new System.Drawing.Font("Verdana", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			Label4.Location = new System.Drawing.Point(25, 85);
			Label4.Name = "Label4";
			Label4.Size = new System.Drawing.Size(53, 12);
			Label4.TabIndex = 30;
			Label4.Text = "Published";
			chkStory.AutoSize = true;
			chkStory.Location = new System.Drawing.Point(7, 25);
			chkStory.Name = "chkStory";
			chkStory.Size = new System.Drawing.Size(15, 14);
			chkStory.TabIndex = 31;
			chkStory.UseVisualStyleBackColor = true;
			chkSeries.AutoSize = true;
			chkSeries.Location = new System.Drawing.Point(7, 61);
			chkSeries.Name = "chkSeries";
			chkSeries.Size = new System.Drawing.Size(15, 14);
			chkSeries.TabIndex = 32;
			chkSeries.UseVisualStyleBackColor = true;
			chkVol.AutoSize = true;
			chkVol.Location = new System.Drawing.Point(232, 61);
			chkVol.Name = "chkVol";
			chkVol.Size = new System.Drawing.Size(15, 14);
			chkVol.TabIndex = 33;
			chkVol.UseVisualStyleBackColor = true;
			chkPublished.AutoSize = true;
			chkPublished.Location = new System.Drawing.Point(7, 101);
			chkPublished.Name = "chkPublished";
			chkPublished.Size = new System.Drawing.Size(15, 14);
			chkPublished.TabIndex = 34;
			chkPublished.UseVisualStyleBackColor = true;
			chkPublished.CheckedChanged += new System.EventHandler(chkPublished_CheckedChanged);
			chkType.AutoSize = true;
			chkType.Location = new System.Drawing.Point(232, 101);
			chkType.Name = "chkType";
			chkType.Size = new System.Drawing.Size(15, 14);
			chkType.TabIndex = 35;
			chkType.UseVisualStyleBackColor = true;
			Label2.AutoSize = true;
			Label2.Font = new System.Drawing.Font("Verdana", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			Label2.Location = new System.Drawing.Point(26, 124);
			Label2.Name = "Label2";
			Label2.Size = new System.Drawing.Size(94, 12);
			Label2.TabIndex = 37;
			Label2.Text = "Sequential Dating";
			cboAutoDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			cboAutoDate.Enabled = false;
			cboAutoDate.FormattingEnabled = true;
			cboAutoDate.Items.AddRange(new object[5]
			{
				"Weekly",
				"Bi-Weekly",
				"Monthly",
				"Bi-Monthly",
				"Yearly"
			});
			cboAutoDate.Location = new System.Drawing.Point(28, 139);
			cboAutoDate.Name = "cboAutoDate";
			cboAutoDate.Size = new System.Drawing.Size(187, 21);
			cboAutoDate.TabIndex = 39;
			chkNumber.AutoSize = true;
			chkNumber.Font = new System.Drawing.Font("Verdana", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			chkNumber.Location = new System.Drawing.Point(232, 133);
			chkNumber.Name = "chkNumber";
			chkNumber.Size = new System.Drawing.Size(133, 16);
			chkNumber.TabIndex = 40;
			chkNumber.Text = "Sequential Numbering";
			chkNumber.UseVisualStyleBackColor = true;
			chkNumber.CheckedChanged += new System.EventHandler(chkNumber_CheckedChanged);
			udStart.Enabled = false;
			udStart.Location = new System.Drawing.Point(315, 155);
			udStart.Maximum = new decimal(new int[4]
			{
				10000,
				0,
				0,
				0
			});
			udStart.Name = "udStart";
			udStart.Size = new System.Drawing.Size(44, 20);
			udStart.TabIndex = 41;
			udStart.Value = new decimal(new int[4]
			{
				1,
				0,
				0,
				0
			});
			label3.AutoSize = true;
			label3.Font = new System.Drawing.Font("Verdana", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label3.Location = new System.Drawing.Point(238, 157);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(74, 12);
			label3.TabIndex = 42;
			label3.Text = "Starting With:";
			label8.AutoSize = true;
			label8.Font = new System.Drawing.Font("Verdana", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label8.Location = new System.Drawing.Point(26, 172);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(61, 12);
			label8.TabIndex = 44;
			label8.Text = "Comments:";
			txtComments.Location = new System.Drawing.Point(28, 187);
			txtComments.Multiline = true;
			txtComments.Name = "txtComments";
			txtComments.Size = new System.Drawing.Size(330, 52);
			txtComments.TabIndex = 43;
			txtComments.TextChanged += new System.EventHandler(txtComments_TextChanged);
			chkComments.AutoSize = true;
			chkComments.Location = new System.Drawing.Point(7, 190);
			chkComments.Name = "chkComments";
			chkComments.Size = new System.Drawing.Size(15, 14);
			chkComments.TabIndex = 45;
			chkComments.UseVisualStyleBackColor = true;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(369, 294);
			base.Controls.Add(txtComments);
			base.Controls.Add(chkComments);
			base.Controls.Add(label8);
			base.Controls.Add(label3);
			base.Controls.Add(udStart);
			base.Controls.Add(chkNumber);
			base.Controls.Add(datePublished);
			base.Controls.Add(txtPlot);
			base.Controls.Add(cboAutoDate);
			base.Controls.Add(Label2);
			base.Controls.Add(chkType);
			base.Controls.Add(chkPublished);
			base.Controls.Add(chkVol);
			base.Controls.Add(chkSeries);
			base.Controls.Add(chkStory);
			base.Controls.Add(Label4);
			base.Controls.Add(cboType);
			base.Controls.Add(Label7);
			base.Controls.Add(cboSeries);
			base.Controls.Add(Label6);
			base.Controls.Add(udVol);
			base.Controls.Add(Label5);
			base.Controls.Add(Label1);
			base.Controls.Add(TableLayoutPanel1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "IssueMultiForm";
			base.ShowInTaskbar = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Comic Info";
			base.Load += new System.EventHandler(frmInfoMulti_Load);
			TableLayoutPanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)udVol).EndInit();
			((System.ComponentModel.ISupportInitialize)udStart).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		public IssueMultiForm()
		{
			InitializeComponent();
			OK_Button.Click += OK_Button_Click;
			Cancel_Button.Click += Cancel_Button_Click;
			datePublished.ValueChanged += datePublished_ValueChanged;
			cboType.SelectedIndexChanged += cboType_SelectedIndexChanged;
			udVol.ValueChanged += udVol_ValueChanged;
			txtPlot.TextChanged += txtStory_TextChanged;
			cboSeries.TextChanged += cboSeries_TextChanged;
		}

		private void OK_Button_Click(object sender, EventArgs e)
		{
			int num = -1;
			DateTime published = datePublished.Value;
			ComicIssue comicIssue = null;
			Cursor = Cursors.WaitCursor;
			for (int i = 0; i < MainForm.Root.IssueList.SelectedIndices.Count; i++)
			{
				comicIssue = (ComicIssue)CC.Issues[MainForm.Root.IssueList.SelectedIndices[i]];

                //set the plot title of the issue if checked
                //(sorry for the three different naming conventions here)
				if (chkStory.Checked)
				{
					comicIssue.Name = txtPlot.Text;
				}
                //set series name of the issue if checked
				if (chkSeries.Checked)
				{
                    //Find the ID of the selected Series
					if (num == -1)
					{
						num = ComicSeries.GetID(cboSeries.Text, comicIssue.GroupID, comicIssue.PublisherID);
					}
					comicIssue.SeriesID = num;
					comicIssue.SeriesName = cboSeries.Text;
				}
                //set comments if checked
				if (chkComments.Checked)
				{
					comicIssue.Comments = txtComments.Text;
				}
                //set volume if checked
				if (chkVol.Checked)
				{
					comicIssue.SeriesVolume = (int)udVol.Value;
				}
                //set published date if checked
				if (chkPublished.Checked)
				{
					comicIssue.Published = published;
                    //If the Auto-Date feature is being used then we're assuming
                    //that the selected issues were published a set amount of time apart, sequentially
					if (cboAutoDate.SelectedIndex != -1)
					{
						switch (cboAutoDate.Text)
						{
						case "Weekly":
							published = published.AddDays(7.0);
							break;
						case "Bi-Weekly":
							published = published.AddDays(14.0);
							break;
						case "Monthly":
							published = published.AddMonths(1);
							break;
						case "Bi-Monthly":
							published = published.AddMonths(2);
							break;
						case "Yearly":
							published = published.AddYears(1);
							break;
						}
					}
				}
                //set issue type if checked
				if (chkType.Checked)
				{
					comicIssue.Type = cboType.SelectedIndex;
				}
                //set issue number if checked
				if (chkNumber.Checked)
				{
                    //we're assuming that the issues were published sequentially after the start value
					comicIssue.Number = (int)((decimal)i + udStart.Value);
					comicIssue.Type = 0;    //Normal Issue
				}
				comicIssue.SaveChanges();
				
                //Since we've made changes, the files may need to be renamed or moved in accordance with user prefs
                if (Settings.Default.OrganizeMethod != 0)
				{
					comicIssue.OrganizeFile();
				}
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

		private void cboType_SelectedIndexChanged(object sender, EventArgs e)
		{
			chkType.Checked = true;
		}

		private void udVol_ValueChanged(object sender, EventArgs e)
		{
			chkVol.Checked = true;
		}

		private void txtStory_TextChanged(object sender, EventArgs e)
		{
			chkStory.Checked = true;
		}

        //set form properties based on selected issues
		private void LoadIssues(ListView.SelectedIndexCollection Indices)
		{
			ComicIssue comicIssue = null;

            //Generic variables names!!!
			bool flag = false;
			bool flag2 = true;
			bool flag3 = true;
			bool flag4 = true;
			bool flag5 = true;
			bool flag6 = true;

			Cursor = Cursors.AppStarting;
			ComicIssue comicIssue2 = (ComicIssue)CC.Issues[Indices[0]]; //get the first issue

            //If information in subsequent issues doesn't match, the field will be flagged and be blank on the form
			for (int i = 1; i <= Indices.Count - 1; i++)
			{
				if (comicIssue2.Missing)
				{
					flag = true;
				}
				comicIssue = (ComicIssue)CC.Issues[Indices[i]];
				if (comicIssue2.Name != comicIssue.Name)
				{
					flag2 = false;
				}
				if (comicIssue2.SeriesName != comicIssue.SeriesName)
				{
					flag3 = false;
				}
				if (comicIssue2.SeriesVolume != comicIssue.SeriesVolume)
				{
					flag4 = false;
				}
				if (comicIssue2.Type != comicIssue.Type)
				{
					flag5 = false;
				}
				bool flag7 = comicIssue2.Published != comicIssue.Published;
				if (comicIssue2.Comments != comicIssue.Comments)
				{
					flag6 = false;
				}
				comicIssue2 = comicIssue;
			}
			txtPlot.Text = (flag2 ? comicIssue.Name : "");
			txtComments.Text = (flag6 ? comicIssue.Comments : "");
			cboSeries.Text = (flag3 ? comicIssue.SeriesName : "");
			udVol.Value = (flag4 ? comicIssue.SeriesVolume : 0);
			cboType.SelectedIndex = (flag5 ? comicIssue.Type : 0);

            //If file organization is turned on and one of the issues is missing, disable everything.
			if (Settings.Default.OrganizeMethod != 0)
			{
				foreach (Control control in base.Controls)
				{
					control.Enabled = !flag;
				}
			}
			Cancel_Button.Enabled = true;
			Cursor = Cursors.Default;
		}

		private void cboSeries_TextChanged(object sender, EventArgs e)
		{
			chkSeries.Checked = true;
		}

		private void frmInfoMulti_Load(object sender, EventArgs e)
		{
			datePublished.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
			LoadIssues(MainForm.Root.IssueList.SelectedIndices);
			chkStory.Checked = false;
			chkSeries.Checked = false;
			chkVol.Checked = false;
			chkPublished.Checked = false;
			chkType.Checked = false;
		}

		private void datePublished_ValueChanged(object sender, EventArgs e)
		{
			chkPublished.Checked = true;
		}

		private void chkPublished_CheckedChanged(object sender, EventArgs e)
		{
			cboAutoDate.Enabled = chkPublished.Checked;
		}

		private void chkNumber_CheckedChanged(object sender, EventArgs e)
		{
			udStart.Enabled = chkNumber.Checked;
		}

		private void txtComments_TextChanged(object sender, EventArgs e)
		{
			chkComments.Checked = !(txtComments.Text == "");
		}
	}
}
