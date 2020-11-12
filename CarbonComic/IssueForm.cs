using CarbonComic.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CarbonComic
{
    //This form shows info for a single issue
	public class IssueForm : Form
	{
        private int CurrentIssue;
		private IContainer components;
		internal ComboBox cboType;
		internal Label Label7;
		internal DateTimePicker datePublished;
		internal ComboBox cboSeries;
		internal Label Label5;
		internal NumericUpDown udNumber;
        internal Label Label4;
		internal Label Label6;
		internal Button cmdPrev;
		internal NumericUpDown udVol;
		internal Label Label3;
		internal TabControl tabInfo;
		internal TabPage TabPage1;
		internal Label lblFileName;
		internal Label lblAdded;
		internal Label lblPublished;
        internal Label lblSize;
		internal Label lblKind;
        internal Label lblSeries;
		internal Label Label14;
		internal Label Label13;
        internal Label lblWhere;
		internal Label Label11;
		internal Label Label10;
		internal Label lblPlot;
        internal Label lblNumber;
		internal PictureBox CoverPreview;
		internal TabPage TabPage2;
		internal CheckBox chkPublished;
		internal Label Label2;
		internal Label Label1;
		internal TextBox txtPlot;
		internal Button cmdNext;
		internal TableLayoutPanel TableLayoutPanel3;
		internal Button Cancel_Button;
		internal TableLayoutPanel TableLayoutPanel1;
		internal Button OK_Button;
		internal Label label8;
		private TextBox txtComments;

		public IssueForm()
		{
			InitializeComponent();
			tabInfo.SelectedIndexChanged += tabInfo_SelectedIndexChanged;
		}

        //Change form height to fit contents in different tabs
        //Hard coded, apparently.
		private void tabInfo_SelectedIndexChanged(object sender, EventArgs e)
		{
			switch (tabInfo.SelectedIndex)
			{
			case 0:
				base.Height = 325;
				break;
			case 1:
				base.Height = 365;
				break;
			}
		}

		private void LoadIssue(int ItemIndex)
		{
			chkPublished.Checked = false;
			CurrentIssue = ItemIndex;
			ComicIssue comicIssue = (ComicIssue)CC.Issues[ItemIndex];
			cmdNext.Enabled = ((CurrentIssue != CC.Issues.Count - 1) ? true : false);
			cmdPrev.Enabled = ((CurrentIssue != 0) ? true : false);
			txtPlot.Text = comicIssue.Name;
			cboSeries.Text = comicIssue.SeriesName;
			udNumber.Value = comicIssue.Number;
			udVol.Value = comicIssue.SeriesVolume;
			cboType.SelectedIndex = comicIssue.Type;
			txtComments.Text = comicIssue.Comments;
			try
			{
				datePublished.Value = comicIssue.Published;
			}
			catch
			{
			}
			lblPlot.Text = comicIssue.Name;
			lblSeries.Text = comicIssue.SeriesName;

			if (comicIssue.SeriesType != 2)
			{
				switch (comicIssue.Type)
				{
				case 0:
					lblNumber.Text = "Issue #" + comicIssue.Number;
					break;
				case 1:
					lblNumber.Text = "Annual " + comicIssue.Number;
					break;
				case 2:
					lblNumber.Text = "Special";
					break;
				}
			}
			else
			{
				lblNumber.Text = "One-shot";
			}
			lblNumber.Text = lblNumber.Text + " (" + comicIssue.Pages + " Pages)";
			switch (Path.GetExtension(comicIssue.FileName))
			{
			case ".cbr":
				lblKind.Text = "Comic Book RAR File";
				break;
			case ".cbz":
				lblKind.Text = "Comic Book Zip File";
				break;
			case ".pdf":
				lblKind.Text = "Comic Book PDF File";
				break;
			}
			lblSize.Text = CC.ByteToString((double)comicIssue.FileSize);
			if (comicIssue.Published.Year != 1)
			{
				lblPublished.Text = comicIssue.Published.ToString("MM yyyy");
			}
			else
			{
				lblPublished.Text = "Unknown";
			}
			lblAdded.Text = comicIssue.DateAdded.ToString("M/d/yyyy h:mm tt");
			CoverPreview.Image = CC.GetIssueCover(comicIssue.ID);
			lblFileName.Text = comicIssue.FileName;

            //If file organization is turned on and the issue is missing, disable everything.
			if (Settings.Default.OrganizeMethod != 0)
			{
				foreach (Control control in tabInfo.TabPages[1].Controls)
				{
					control.Enabled = !comicIssue.Missing;
				}
				OK_Button.Enabled = !comicIssue.Missing;
			}
		}

		private void SaveChanges()
		{
			MainForm mainForm = (MainForm)base.Owner;
			ComicIssue comicIssue = (ComicIssue)CC.Issues[CurrentIssue];
			if (!comicIssue.Missing)
			{
				comicIssue.SeriesID = ComicSeries.GetID(cboSeries.Text, comicIssue.GroupID, comicIssue.PublisherID);
				ComicSeries comicSeries = new ComicSeries(comicIssue.SeriesID);
				comicIssue.GroupName = comicSeries.GroupName;
				comicIssue.PublisherName = comicSeries.PublisherName;
				comicIssue.SeriesName = cboSeries.Text;
				comicIssue.Name = txtPlot.Text;
				comicIssue.SeriesVolume = (int)udVol.Value;
				comicIssue.Number = (int)udNumber.Value;
				comicIssue.Type = cboType.SelectedIndex;
				comicIssue.Comments = txtComments.Text;
				if (chkPublished.Checked)
				{
					comicIssue.Published = datePublished.Value;
				}
				comicIssue.SaveChanges();
				if (Settings.Default.OrganizeMethod != 0)
				{
					comicIssue.OrganizeFile();
				}
				mainForm.IssueList.Refresh();
			}
		}

		private void IssueInfoForm_Load(object sender, EventArgs e)
		{
			MainForm root = MainForm.Root;
			cboSeries.Items.Clear();
			for (int i = 1; i < root.SeriesList.Items.Count; i++)
			{
				cboSeries.Items.Add(root.SeriesList.Items[i].Text);
			}
			datePublished.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
			LoadIssue(root.IssueList.SelectedIndices[0]);
		}

		private void cmdNext_Click(object sender, EventArgs e)
		{
			SaveChanges();
			LoadIssue(CurrentIssue + 1);
		}

		private void cmdPrev_Click(object sender, EventArgs e)
		{
			SaveChanges();
			LoadIssue(CurrentIssue - 1);
		}

		private void OK_Button_Click(object sender, EventArgs e)
		{
			SaveChanges();
			base.DialogResult = DialogResult.OK;
			Close();
		}

		private void Cancel_Button_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			Close();
		}

        //Clicking on the cover gives the option to select a new cover
		private void CoverPreview_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			MainForm mainForm = new MainForm();
			ComicIssue comicIssue = (ComicIssue)CC.Issues[CurrentIssue];
			openFileDialog.Filter = "JPEG Files|*.jpg;*.jpeg|GIF Files|*.gif|PNG Files|*.png|Bitmap Files|*.bmp";
			openFileDialog.ShowDialog();
			if (openFileDialog.FileName != "")
			{
				CC.CreateThumbnail(openFileDialog.FileName, Settings.Default.CoverDir + "\\" + comicIssue.ID + ".jpg");
				mainForm.IssueCovers.Images.RemoveByKey(Convert.ToString(comicIssue.ID));
				CoverPreview.Image = CC.GetIssueCover(comicIssue.ID);
			}
		}

		private void udNumber_ValueChanged(object sender, EventArgs e)
		{
			if (Convert.ToString(udNumber.Value).Length == 4)
			{
				cboType.SelectedIndex = 1;
			}
			else if (udNumber.Value == 0)
			{
				cboType.SelectedIndex = 2;
			}
			else
			{
				cboType.SelectedIndex = 0;
			}
		}

		private void datePublished_ValueChanged(object sender, EventArgs e)
		{
			chkPublished.Checked = true;
		}

		private void txtComments_TextChanged(object sender, EventArgs e)
		{
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
			cboType = new System.Windows.Forms.ComboBox();
			Label7 = new System.Windows.Forms.Label();
			datePublished = new System.Windows.Forms.DateTimePicker();
			cboSeries = new System.Windows.Forms.ComboBox();
			Label5 = new System.Windows.Forms.Label();
			udNumber = new System.Windows.Forms.NumericUpDown();
			Label4 = new System.Windows.Forms.Label();
			Label6 = new System.Windows.Forms.Label();
			cmdPrev = new System.Windows.Forms.Button();
			udVol = new System.Windows.Forms.NumericUpDown();
			Label3 = new System.Windows.Forms.Label();
			tabInfo = new System.Windows.Forms.TabControl();
			TabPage1 = new System.Windows.Forms.TabPage();
			lblFileName = new System.Windows.Forms.Label();
			lblAdded = new System.Windows.Forms.Label();
			lblPublished = new System.Windows.Forms.Label();
			lblSize = new System.Windows.Forms.Label();
			lblKind = new System.Windows.Forms.Label();
			lblSeries = new System.Windows.Forms.Label();
			Label14 = new System.Windows.Forms.Label();
			Label13 = new System.Windows.Forms.Label();
			lblWhere = new System.Windows.Forms.Label();
			Label11 = new System.Windows.Forms.Label();
			Label10 = new System.Windows.Forms.Label();
			lblPlot = new System.Windows.Forms.Label();
			lblNumber = new System.Windows.Forms.Label();
			CoverPreview = new System.Windows.Forms.PictureBox();
			TabPage2 = new System.Windows.Forms.TabPage();
			txtComments = new System.Windows.Forms.TextBox();
			label8 = new System.Windows.Forms.Label();
			txtPlot = new System.Windows.Forms.TextBox();
			chkPublished = new System.Windows.Forms.CheckBox();
			Label2 = new System.Windows.Forms.Label();
			Label1 = new System.Windows.Forms.Label();
			cmdNext = new System.Windows.Forms.Button();
			TableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			Cancel_Button = new System.Windows.Forms.Button();
			TableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			OK_Button = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)udNumber).BeginInit();
			((System.ComponentModel.ISupportInitialize)udVol).BeginInit();
			tabInfo.SuspendLayout();
			TabPage1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)CoverPreview).BeginInit();
			TabPage2.SuspendLayout();
			TableLayoutPanel3.SuspendLayout();
			TableLayoutPanel1.SuspendLayout();
			SuspendLayout();
			cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			cboType.FormattingEnabled = true;
			cboType.Items.AddRange(new object[3]
			{
				"Issue",
				"Annual",
				"Special"
			});
			cboType.Location = new System.Drawing.Point(9, 111);
			cboType.Name = "cboType";
			cboType.Size = new System.Drawing.Size(228, 21);
			cboType.TabIndex = 18;
			Label7.AutoSize = true;
			Label7.Font = new System.Drawing.Font("Verdana", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			Label7.Location = new System.Drawing.Point(6, 95);
			Label7.Name = "Label7";
			Label7.Size = new System.Drawing.Size(33, 12);
			Label7.TabIndex = 17;
			Label7.Text = "Type:";
			datePublished.Location = new System.Drawing.Point(36, 151);
			datePublished.Name = "datePublished";
			datePublished.Size = new System.Drawing.Size(301, 20);
			datePublished.TabIndex = 16;
			datePublished.ValueChanged += new System.EventHandler(datePublished_ValueChanged);
			cboSeries.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			cboSeries.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cboSeries.FormattingEnabled = true;
			cboSeries.Items.AddRange(new object[12]
			{
				"January",
				"February",
				"March",
				"April",
				"May",
				"June",
				"July",
				"August",
				"September",
				"October",
				"November",
				"December"
			});
			cboSeries.Location = new System.Drawing.Point(9, 67);
			cboSeries.Name = "cboSeries";
			cboSeries.Size = new System.Drawing.Size(228, 21);
			cboSeries.TabIndex = 15;
			Label5.AutoSize = true;
			Label5.Font = new System.Drawing.Font("Verdana", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			Label5.Location = new System.Drawing.Point(253, 51);
			Label5.Name = "Label5";
			Label5.Size = new System.Drawing.Size(46, 12);
			Label5.TabIndex = 12;
			Label5.Text = "Volume:";
			udNumber.Location = new System.Drawing.Point(254, 110);
			udNumber.Maximum = new decimal(new int[4]
			{
				10000,
				0,
				0,
				0
			});
			udNumber.Minimum = new decimal(new int[4]
			{
				1,
				0,
				0,
				-2147483648
			});
			udNumber.Name = "udNumber";
			udNumber.Size = new System.Drawing.Size(84, 20);
			udNumber.TabIndex = 9;
			udNumber.ValueChanged += new System.EventHandler(udNumber_ValueChanged);
			Label4.AutoSize = true;
			Label4.Font = new System.Drawing.Font("Verdana", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			Label4.Location = new System.Drawing.Point(7, 135);
			Label4.Name = "Label4";
			Label4.Size = new System.Drawing.Size(57, 12);
			Label4.TabIndex = 7;
			Label4.Text = "Published:";
			Label6.AutoSize = true;
			Label6.Font = new System.Drawing.Font("Verdana", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			Label6.Location = new System.Drawing.Point(6, 51);
			Label6.Name = "Label6";
			Label6.Size = new System.Drawing.Size(39, 12);
			Label6.TabIndex = 14;
			Label6.Text = "Series:";
			cmdPrev.Anchor = System.Windows.Forms.AnchorStyles.None;
			cmdPrev.Location = new System.Drawing.Point(3, 3);
			cmdPrev.Name = "cmdPrev";
			cmdPrev.Size = new System.Drawing.Size(67, 23);
			cmdPrev.TabIndex = 0;
			cmdPrev.Text = "Previous";
			cmdPrev.Click += new System.EventHandler(cmdPrev_Click);
			udVol.Location = new System.Drawing.Point(256, 67);
			udVol.Maximum = new decimal(new int[4]
			{
				1000,
				0,
				0,
				0
			});
			udVol.Name = "udVol";
			udVol.Size = new System.Drawing.Size(82, 20);
			udVol.TabIndex = 13;
			Label3.AutoSize = true;
			Label3.Font = new System.Drawing.Font("Verdana", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			Label3.Location = new System.Drawing.Point(251, 94);
			Label3.Name = "Label3";
			Label3.Size = new System.Drawing.Size(47, 12);
			Label3.TabIndex = 4;
			Label3.Text = "Number:";
			tabInfo.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			tabInfo.Controls.Add(TabPage1);
			tabInfo.Controls.Add(TabPage2);
			tabInfo.Location = new System.Drawing.Point(6, 3);
			tabInfo.Name = "tabInfo";
			tabInfo.SelectedIndex = 0;
			tabInfo.Size = new System.Drawing.Size(354, 258);
			tabInfo.TabIndex = 5;
			TabPage1.Controls.Add(lblFileName);
			TabPage1.Controls.Add(lblAdded);
			TabPage1.Controls.Add(lblPublished);
			TabPage1.Controls.Add(lblSize);
			TabPage1.Controls.Add(lblKind);
			TabPage1.Controls.Add(lblSeries);
			TabPage1.Controls.Add(Label14);
			TabPage1.Controls.Add(Label13);
			TabPage1.Controls.Add(lblWhere);
			TabPage1.Controls.Add(Label11);
			TabPage1.Controls.Add(Label10);
			TabPage1.Controls.Add(lblPlot);
			TabPage1.Controls.Add(lblNumber);
			TabPage1.Controls.Add(CoverPreview);
			TabPage1.Location = new System.Drawing.Point(4, 22);
			TabPage1.Name = "TabPage1";
			TabPage1.Padding = new System.Windows.Forms.Padding(3);
			TabPage1.Size = new System.Drawing.Size(346, 232);
			TabPage1.TabIndex = 0;
			TabPage1.Text = "File";
			TabPage1.UseVisualStyleBackColor = true;
			lblFileName.Location = new System.Drawing.Point(6, 180);
			lblFileName.Name = "lblFileName";
			lblFileName.Size = new System.Drawing.Size(334, 44);
			lblFileName.TabIndex = 13;
			lblFileName.Text = "[FileName]";
			lblAdded.AutoSize = true;
			lblAdded.Location = new System.Drawing.Point(212, 142);
			lblAdded.Name = "lblAdded";
			lblAdded.Size = new System.Drawing.Size(67, 13);
			lblAdded.TabIndex = 12;
			lblAdded.Text = "[DateAdded]";
			lblPublished.AutoSize = true;
			lblPublished.Location = new System.Drawing.Point(212, 121);
			lblPublished.Name = "lblPublished";
			lblPublished.Size = new System.Drawing.Size(59, 13);
			lblPublished.TabIndex = 11;
			lblPublished.Text = "[Published]";
			lblSize.AutoSize = true;
			lblSize.Location = new System.Drawing.Point(212, 99);
			lblSize.Name = "lblSize";
			lblSize.Size = new System.Drawing.Size(33, 13);
			lblSize.TabIndex = 10;
			lblSize.Text = "[Size]";
			lblKind.AutoSize = true;
			lblKind.Location = new System.Drawing.Point(212, 74);
			lblKind.Name = "lblKind";
			lblKind.Size = new System.Drawing.Size(34, 13);
			lblKind.TabIndex = 9;
			lblKind.Text = "[Kind]";
			lblSeries.AutoSize = true;
			lblSeries.Location = new System.Drawing.Point(121, 28);
			lblSeries.Name = "lblSeries";
			lblSeries.Size = new System.Drawing.Size(42, 13);
			lblSeries.TabIndex = 8;
			lblSeries.Text = "[Series]";
			Label14.AutoSize = true;
			Label14.Font = new System.Drawing.Font("Verdana", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			Label14.Location = new System.Drawing.Point(121, 142);
			Label14.Name = "Label14";
			Label14.Size = new System.Drawing.Size(68, 12);
			Label14.TabIndex = 7;
			Label14.Text = "Date Added:";
			Label13.AutoSize = true;
			Label13.Font = new System.Drawing.Font("Verdana", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			Label13.Location = new System.Drawing.Point(121, 121);
			Label13.Name = "Label13";
			Label13.Size = new System.Drawing.Size(57, 12);
			Label13.TabIndex = 6;
			Label13.Text = "Published:";
			lblWhere.AutoSize = true;
			lblWhere.Font = new System.Drawing.Font("Verdana", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			lblWhere.Location = new System.Drawing.Point(6, 159);
			lblWhere.Name = "lblWhere";
			lblWhere.Size = new System.Drawing.Size(42, 12);
			lblWhere.TabIndex = 5;
			lblWhere.Text = "Where:";
			Label11.AutoSize = true;
			Label11.Font = new System.Drawing.Font("Verdana", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			Label11.Location = new System.Drawing.Point(121, 99);
			Label11.Name = "Label11";
			Label11.Size = new System.Drawing.Size(29, 12);
			Label11.TabIndex = 4;
			Label11.Text = "Size:";
			Label10.AutoSize = true;
			Label10.Font = new System.Drawing.Font("Verdana", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			Label10.Location = new System.Drawing.Point(121, 74);
			Label10.Name = "Label10";
			Label10.Size = new System.Drawing.Size(31, 12);
			Label10.TabIndex = 3;
			Label10.Text = "Kind:";
			lblPlot.AutoSize = true;
			lblPlot.Location = new System.Drawing.Point(121, 50);
			lblPlot.Name = "lblPlot";
			lblPlot.Size = new System.Drawing.Size(54, 13);
			lblPlot.TabIndex = 2;
			lblPlot.Text = "[Plot Title]";
			lblNumber.AutoSize = true;
			lblNumber.Location = new System.Drawing.Point(121, 15);
			lblNumber.Name = "lblNumber";
			lblNumber.Size = new System.Drawing.Size(76, 13);
			lblNumber.TabIndex = 1;
			lblNumber.Text = "Title (# Pages)";
			CoverPreview.Cursor = System.Windows.Forms.Cursors.Hand;
			CoverPreview.Location = new System.Drawing.Point(6, 6);
			CoverPreview.Name = "CoverPreview";
			CoverPreview.Size = new System.Drawing.Size(98, 150);
			CoverPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			CoverPreview.TabIndex = 0;
			CoverPreview.TabStop = false;
			CoverPreview.Click += new System.EventHandler(CoverPreview_Click);
			TabPage2.Controls.Add(txtComments);
			TabPage2.Controls.Add(label8);
			TabPage2.Controls.Add(txtPlot);
			TabPage2.Controls.Add(chkPublished);
			TabPage2.Controls.Add(cboType);
			TabPage2.Controls.Add(Label7);
			TabPage2.Controls.Add(datePublished);
			TabPage2.Controls.Add(cboSeries);
			TabPage2.Controls.Add(Label6);
			TabPage2.Controls.Add(udVol);
			TabPage2.Controls.Add(Label5);
			TabPage2.Controls.Add(udNumber);
			TabPage2.Controls.Add(Label4);
			TabPage2.Controls.Add(Label3);
			TabPage2.Controls.Add(Label2);
			TabPage2.Controls.Add(Label1);
			TabPage2.Location = new System.Drawing.Point(4, 22);
			TabPage2.Name = "TabPage2";
			TabPage2.Padding = new System.Windows.Forms.Padding(3);
			TabPage2.Size = new System.Drawing.Size(346, 232);
			TabPage2.TabIndex = 1;
			TabPage2.Text = "Info";
			TabPage2.UseVisualStyleBackColor = true;
			txtComments.Location = new System.Drawing.Point(9, 194);
			txtComments.Multiline = true;
			txtComments.Name = "txtComments";
			txtComments.Size = new System.Drawing.Size(327, 68);
			txtComments.TabIndex = 20;
			txtComments.TextChanged += new System.EventHandler(txtComments_TextChanged);
			label8.AutoSize = true;
			label8.Font = new System.Drawing.Font("Verdana", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label8.Location = new System.Drawing.Point(7, 179);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(61, 12);
			label8.TabIndex = 21;
			label8.Text = "Comments:";
			txtPlot.Location = new System.Drawing.Point(9, 28);
			txtPlot.Name = "txtPlot";
			txtPlot.Size = new System.Drawing.Size(327, 20);
			txtPlot.TabIndex = 0;
			chkPublished.AutoSize = true;
			chkPublished.Location = new System.Drawing.Point(15, 155);
			chkPublished.Name = "chkPublished";
			chkPublished.Size = new System.Drawing.Size(13, 12);
			chkPublished.TabIndex = 19;
			chkPublished.UseVisualStyleBackColor = true;
			Label2.AutoSize = true;
			Label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			Label2.Location = new System.Drawing.Point(253, 115);
			Label2.Name = "Label2";
			Label2.Size = new System.Drawing.Size(39, 13);
			Label2.TabIndex = 2;
			Label2.Text = "Type:";
			Label1.AutoSize = true;
			Label1.Font = new System.Drawing.Font("Verdana", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			Label1.Location = new System.Drawing.Point(6, 12);
			Label1.Name = "Label1";
			Label1.Size = new System.Drawing.Size(56, 12);
			Label1.TabIndex = 1;
			Label1.Text = "Plot Title:";
			cmdNext.Anchor = System.Windows.Forms.AnchorStyles.None;
			cmdNext.Location = new System.Drawing.Point(76, 3);
			cmdNext.Name = "cmdNext";
			cmdNext.Size = new System.Drawing.Size(67, 23);
			cmdNext.TabIndex = 1;
			cmdNext.Text = "Next";
			cmdNext.Click += new System.EventHandler(cmdNext_Click);
			TableLayoutPanel3.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			TableLayoutPanel3.ColumnCount = 2;
			TableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50f));
			TableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50f));
			TableLayoutPanel3.Controls.Add(cmdPrev, 0, 0);
			TableLayoutPanel3.Controls.Add(cmdNext, 1, 0);
			TableLayoutPanel3.Location = new System.Drawing.Point(9, 267);
			TableLayoutPanel3.Name = "TableLayoutPanel3";
			TableLayoutPanel3.RowCount = 1;
			TableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50f));
			TableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
			TableLayoutPanel3.Size = new System.Drawing.Size(146, 29);
			TableLayoutPanel3.TabIndex = 7;
			Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None;
			Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			Cancel_Button.Location = new System.Drawing.Point(76, 3);
			Cancel_Button.Name = "Cancel_Button";
			Cancel_Button.Size = new System.Drawing.Size(67, 23);
			Cancel_Button.TabIndex = 1;
			Cancel_Button.Text = "Cancel";
			Cancel_Button.Click += new System.EventHandler(Cancel_Button_Click);
			TableLayoutPanel1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			TableLayoutPanel1.ColumnCount = 2;
			TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50f));
			TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50f));
			TableLayoutPanel1.Controls.Add(Cancel_Button, 1, 0);
			TableLayoutPanel1.Controls.Add(OK_Button, 0, 0);
			TableLayoutPanel1.Location = new System.Drawing.Point(215, 267);
			TableLayoutPanel1.Name = "TableLayoutPanel1";
			TableLayoutPanel1.RowCount = 1;
			TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50f));
			TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29f));
			TableLayoutPanel1.Size = new System.Drawing.Size(146, 29);
			TableLayoutPanel1.TabIndex = 4;
			OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None;
			OK_Button.Location = new System.Drawing.Point(3, 3);
			OK_Button.Name = "OK_Button";
			OK_Button.Size = new System.Drawing.Size(67, 23);
			OK_Button.TabIndex = 0;
			OK_Button.Text = "OK";
			OK_Button.Click += new System.EventHandler(OK_Button_Click);
			base.ClientSize = new System.Drawing.Size(367, 299);
			base.Controls.Add(tabInfo);
			base.Controls.Add(TableLayoutPanel3);
			base.Controls.Add(TableLayoutPanel1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "IssueForm";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Comic Info";
			base.Load += new System.EventHandler(IssueInfoForm_Load);
			((System.ComponentModel.ISupportInitialize)udNumber).EndInit();
			((System.ComponentModel.ISupportInitialize)udVol).EndInit();
			tabInfo.ResumeLayout(false);
			TabPage1.ResumeLayout(false);
			TabPage1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)CoverPreview).EndInit();
			TabPage2.ResumeLayout(false);
			TabPage2.PerformLayout();
			TableLayoutPanel3.ResumeLayout(false);
			TableLayoutPanel1.ResumeLayout(false);
			ResumeLayout(false);
		}
	}
}
