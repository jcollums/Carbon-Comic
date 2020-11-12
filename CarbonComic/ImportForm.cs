using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CarbonComic
{
	public class ImportForm : Form
	{
		private int CurrentPreset = -1;

		private IContainer components;

		internal TableLayoutPanel TableLayoutPanel1;

		internal Button OK_Button;

		internal Button Cancel_Button;

		internal TextBox txtPattern;

		internal Label Label2;

		internal Label Label3;

		internal Label Label4;

		internal Label Label5;

		internal Label Label6;

		internal Label Label7;

		internal Button cmdTest;

		internal TabControl tabImport;

		internal TabPage TabPage1;

		internal TabPage TabPage2;

		internal ComboBox cboDefaultPublisher;

		internal CheckBox chkVol;

		internal CheckBox chkSeries;

		internal ComboBox cboDefaultSeries;

		internal CheckBox chkGroup;

		internal ComboBox cboDefaultGroup;

		internal CheckBox chkPublisher;

		internal TextBox txtDefaultPlot;

		internal NumericUpDown udDefaultVolume;

		internal CheckBox chkPlot;

		internal TabPage TabPage3;

		internal Button cmdIssue;

		internal Button cmdPlot;

		internal Button cmdSeries;

		internal Button cmdVolume;

		internal TextBox txtKeywords;

		internal Button cmdPublisher;

		internal Button cmdGroup;

		internal Button cmdConvert;

		internal ComboBox cboTest;

		private Button cmdOption;

		private ComboBox cboPresets;

		private Label label1;

		private Label label8;

		private Label label9;

		private NumericUpDown udRegexIssue;

		private NumericUpDown udRegexPlot;

		private NumericUpDown udRegexVolume;

		private NumericUpDown udRegexSeries;

		private NumericUpDown udRegexGroup;

		private NumericUpDown udRegexPublisher;

		private Button cmdBuffer;

		public ImportForm()
		{
			InitializeComponent();
			cboDefaultPublisher.TextChanged += cboDefaultPublisher_TextChanged;
			cboDefaultGroup.TextChanged += cboDefaultGroup_TextChanged;
			cboDefaultSeries.TextChanged += cboDefaultSeries_TextChanged;
			cboPresets.TextChanged += cboPresets_TextChanged;
		}

		private void cboPresets_TextChanged(object sender, EventArgs e)
		{
		}

		private void cboDefaultSeries_TextChanged(object sender, EventArgs e)
		{
			chkSeries.Checked = (cboDefaultSeries.Text != "");
		}

		private void cboDefaultGroup_TextChanged(object sender, EventArgs e)
		{
			chkGroup.Checked = (cboDefaultGroup.Text != "");
		}

		private void cboDefaultPublisher_TextChanged(object sender, EventArgs e)
		{
			chkPublisher.Checked = (cboDefaultPublisher.Text != "");
		}

		private void cmdPublisher_Click(object sender, EventArgs e)
		{
			txtKeywords.Text = txtKeywords.Text.Insert(txtKeywords.SelectionStart, "[Publisher]");
		}

		private void Cancel_Button_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			Close();
		}

		private void cmdTest_Click(object sender, EventArgs e)
		{
			try
			{
				Regex regex = new Regex(txtPattern.Text);
				Match match = regex.Match(cboTest.Text);
				MessageBox.Show("Publisher: " + match.Groups[(int)udRegexPublisher.Value].Value + Environment.NewLine + "Groups: " + match.Groups[(int)udRegexGroup.Value].Value + Environment.NewLine + "Volume: " + match.Groups[(int)udRegexVolume.Value].Value + Environment.NewLine + "Series: " + match.Groups[(int)udRegexSeries.Value].Value + Environment.NewLine + "Plot: " + match.Groups[(int)udRegexPlot.Value].Value + Environment.NewLine + "Issue: " + match.Groups[(int)udRegexIssue.Value].Value, "Match Results", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			catch
			{
				MessageBox.Show("Invalid regular expression pattern.");
			}
		}

		private void udDefaultVolume_ValueChanged(object sender, EventArgs e)
		{
			chkVol.Checked = (udDefaultVolume.Value > 1m);
		}

		private void txtDefaultPlot_TextChanged(object sender, EventArgs e)
		{
			chkPlot.Checked = (txtDefaultPlot.Text != "");
		}

		private void cmdGroup_Click(object sender, EventArgs e)
		{
			txtKeywords.Text = txtKeywords.Text.Insert(txtKeywords.SelectionStart, "[Group]");
		}

		private void cmdSeries_Click(object sender, EventArgs e)
		{
			txtKeywords.Text = txtKeywords.Text.Insert(txtKeywords.SelectionStart, "[Series]");
		}

		private void cmdVolume_Click(object sender, EventArgs e)
		{
			txtKeywords.Text = txtKeywords.Text.Insert(txtKeywords.SelectionStart, "[Volume]");
		}

		private void cmdIssue_Click(object sender, EventArgs e)
		{
			txtKeywords.Text = txtKeywords.Text.Insert(txtKeywords.SelectionStart, "[Number]");
		}

		private void cmdPlot_Click(object sender, EventArgs e)
		{
			txtKeywords.Text = txtKeywords.Text.Insert(txtKeywords.SelectionStart, "[Plot]");
		}

		private void cmdConvert_Click(object sender, EventArgs e)
		{
			string pattern = "\\[Publisher\\]|\\[Group\\]|\\[Series\\]|\\[Volume\\]|\\[Number\\]|\\[Plot\\]|\\[Buffer\\]";
			Regex regex = new Regex(pattern);
			new Hashtable();
			int num = 0;
			Hashtable hashtable = new Hashtable();
			hashtable["[Publisher]"] = "([^\\\\]+)";
			hashtable["[Group]"] = "([^\\\\]+)";
			hashtable["[Series]"] = "([^\\\\#]+)";
			hashtable["[Volume]"] = "(\\d+)";
			hashtable["[Number]"] = "(\\d+)";
			hashtable["[Plot]"] = "([^\\\\]+)";
			hashtable["[Buffer]"] = "[^\\\\]*";
			Hashtable hashtable2 = new Hashtable();
			hashtable2["[Publisher]"] = udRegexPublisher;
			hashtable2["[Group]"] = udRegexGroup;
			hashtable2["[Series]"] = udRegexSeries;
			hashtable2["[Volume]"] = udRegexVolume;
			hashtable2["[Number]"] = udRegexIssue;
			hashtable2["[Plot]"] = udRegexPlot;
			foreach (string key in hashtable2.Keys)
			{
				NumericUpDown numericUpDown = (NumericUpDown)hashtable2[key];
				numericUpDown.Text = "-1";
			}
			txtPattern.Text = txtKeywords.Text;
			txtPattern.Text = txtPattern.Text.Replace("\\", "\\\\");
			txtPattern.Text = txtPattern.Text.Replace("(", "\\(");
			txtPattern.Text = txtPattern.Text.Replace(")", "\\)");
			txtPattern.Text = txtPattern.Text.Replace(".", "\\.");
			txtPattern.Text = txtPattern.Text.Replace("<", "(");
			txtPattern.Text = txtPattern.Text.Replace(">", ")?");
			foreach (Match item in regex.Matches(txtKeywords.Text))
			{
				num++;
				NumericUpDown numericUpDown = (NumericUpDown)hashtable2[item.Value];
				if (numericUpDown != null)
				{
					numericUpDown.Value = num;
				}
				txtPattern.Text = txtPattern.Text.Replace(item.Value, Convert.ToString(hashtable[item.Value]));
			}
			cboPresets.Text = txtKeywords.Text;
			tabImport.SelectTab(2);
		}

		private void ImportForm_Load(object sender, EventArgs e)
		{
			cboDefaultPublisher.Items.Clear();
			cboDefaultPublisher.Items.Add("");
			Query query = CC.SQL.ExecQuery("SELECT name FROM publishers ORDER BY name");
			while (query.NextResult())
			{
				cboDefaultPublisher.Items.Add(query.hash["name"]);
			}
			cboDefaultPublisher_SelectedIndexChanged(this, null);
			foreach (string importFile in CC.ImportFiles)
			{
				cboTest.Items.Add(importFile);
			}
			cboTest.SelectedIndex = 0;
			Query query2 = CC.SQL.ExecQuery("SELECT * FROM regex ORDER BY label");
			cboPresets.Items.Clear();
			while (query2.NextResult())
			{
				cboPresets.Items.Add(query2.hash["label"]);
			}
		}

		private void OK_Button_Click(object sender, EventArgs e)
		{
			if (txtKeywords.Text != "" && txtPattern.Text == "")
			{
				cmdConvert_Click(this, null);
			}
			CC.AutoTag.Pattern = txtPattern.Text;
			CC.AutoTag.Matches = new int[6];
			CC.AutoTag.Matches[0] = (int)udRegexPublisher.Value;
			CC.AutoTag.Matches[1] = (int)udRegexGroup.Value;
			CC.AutoTag.Matches[2] = (int)udRegexSeries.Value;
			CC.AutoTag.Matches[3] = (int)udRegexVolume.Value;
			CC.AutoTag.Matches[4] = (int)udRegexPlot.Value;
			CC.AutoTag.Matches[5] = (int)udRegexIssue.Value;
			CC.AutoTag.usePublisher = chkPublisher.Checked;
			CC.AutoTag.Publisher = cboDefaultPublisher.Text;
			CC.AutoTag.useGroup = chkGroup.Checked;
			CC.AutoTag.Group = cboDefaultGroup.Text;
			CC.AutoTag.useSeries = chkSeries.Checked;
			CC.AutoTag.Series = cboDefaultSeries.Text;
			CC.AutoTag.useVolume = chkVol.Checked;
			CC.AutoTag.Volume = (int)udDefaultVolume.Value;
			CC.AutoTag.usePlot = chkPlot.Checked;
			CC.AutoTag.Plot = txtDefaultPlot.Text;
			base.DialogResult = DialogResult.OK;
			Close();
		}

		private void cmdOption_Click(object sender, EventArgs e)
		{
			txtKeywords.Text = txtKeywords.Text.Substring(0, txtKeywords.SelectionStart) + "<" + txtKeywords.SelectedText + ">" + txtKeywords.Text.Substring(txtKeywords.SelectionStart + txtKeywords.SelectionLength);
		}

		private void cboDefaultPublisher_SelectedIndexChanged(object sender, EventArgs e)
		{
			string str = "";
			if (cboDefaultPublisher.Text != "")
			{
				int iD = ComicPublisher.GetID(cboDefaultPublisher.Text);
				str = "WHERE pub_id=" + iD;
			}
			cboDefaultGroup.Items.Clear();
			cboDefaultGroup.Items.Add("");
			Query query = CC.SQL.ExecQuery("SELECT name FROM groups " + str);
			while (query.NextResult())
			{
				cboDefaultGroup.Items.Add(query.hash["name"]);
			}
			cboDefaultGroup_SelectedIndexChanged(this, null);
		}

		private void cboDefaultGroup_SelectedIndexChanged(object sender, EventArgs e)
		{
			ArrayList arrayList = new ArrayList();
			if (cboDefaultGroup.Text != "")
			{
				int iD = ComicGroup.GetID(cboDefaultGroup.Text);
				arrayList.Add("group_id=" + iD);
			}
			if (cboDefaultPublisher.Text != "")
			{
				int iD2 = ComicPublisher.GetID(cboDefaultPublisher.Text);
				arrayList.Add("pub_id=" + iD2);
			}
			cboDefaultSeries.Items.Clear();
			cboDefaultSeries.Items.Add("");
			string str = "";
			if (arrayList.Count > 0)
			{
				str = "WHERE " + string.Join(" AND ", CC.StringList(arrayList));
			}
			Query query = CC.SQL.ExecQuery("SELECT name FROM series " + str);
			while (query.NextResult())
			{
				cboDefaultSeries.Items.Add(query.hash["name"]);
			}
		}

		private void cboPresets_SelectedIndexChanged(object sender, EventArgs e)
		{
			Query query = CC.SQL.ExecQuery("SELECT * FROM regex WHERE label='" + cboPresets.Text + "'");
			if (query.NextResult())
			{
				CurrentPreset = (int)query.hash["ID"];
				txtPattern.Text = (string)query.hash["pattern"];
				udRegexPublisher.Value = (int)query.hash["publisher"];
				udRegexGroup.Value = (int)query.hash["group"];
				udRegexSeries.Value = (int)query.hash["series"];
				udRegexVolume.Value = (int)query.hash["volume"];
				udRegexPlot.Value = (int)query.hash["plot"];
				udRegexIssue.Value = (int)query.hash["issue"];
			}
		}

		private void cmdAdd_Click(object sender, EventArgs e)
		{
		}

		private void cmdSave_Click(object sender, EventArgs e)
		{
		}

		private void cmdDelete_Click(object sender, EventArgs e)
		{
		}

		private void cmdBuffer_Click(object sender, EventArgs e)
		{
			txtKeywords.Text = txtKeywords.Text.Insert(txtKeywords.SelectionStart, "[Buffer]");
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
			txtPattern = new System.Windows.Forms.TextBox();
			Label2 = new System.Windows.Forms.Label();
			Label3 = new System.Windows.Forms.Label();
			Label4 = new System.Windows.Forms.Label();
			Label5 = new System.Windows.Forms.Label();
			Label6 = new System.Windows.Forms.Label();
			Label7 = new System.Windows.Forms.Label();
			cmdTest = new System.Windows.Forms.Button();
			tabImport = new System.Windows.Forms.TabControl();
			TabPage1 = new System.Windows.Forms.TabPage();
			label1 = new System.Windows.Forms.Label();
			txtDefaultPlot = new System.Windows.Forms.TextBox();
			udDefaultVolume = new System.Windows.Forms.NumericUpDown();
			chkPlot = new System.Windows.Forms.CheckBox();
			chkVol = new System.Windows.Forms.CheckBox();
			chkSeries = new System.Windows.Forms.CheckBox();
			cboDefaultSeries = new System.Windows.Forms.ComboBox();
			chkGroup = new System.Windows.Forms.CheckBox();
			cboDefaultGroup = new System.Windows.Forms.ComboBox();
			chkPublisher = new System.Windows.Forms.CheckBox();
			cboDefaultPublisher = new System.Windows.Forms.ComboBox();
			TabPage3 = new System.Windows.Forms.TabPage();
			cmdBuffer = new System.Windows.Forms.Button();
			label8 = new System.Windows.Forms.Label();
			cmdOption = new System.Windows.Forms.Button();
			cmdConvert = new System.Windows.Forms.Button();
			cmdPublisher = new System.Windows.Forms.Button();
			cmdGroup = new System.Windows.Forms.Button();
			cmdIssue = new System.Windows.Forms.Button();
			cmdPlot = new System.Windows.Forms.Button();
			cmdSeries = new System.Windows.Forms.Button();
			cmdVolume = new System.Windows.Forms.Button();
			txtKeywords = new System.Windows.Forms.TextBox();
			TabPage2 = new System.Windows.Forms.TabPage();
			udRegexIssue = new System.Windows.Forms.NumericUpDown();
			udRegexPlot = new System.Windows.Forms.NumericUpDown();
			udRegexVolume = new System.Windows.Forms.NumericUpDown();
			udRegexSeries = new System.Windows.Forms.NumericUpDown();
			udRegexGroup = new System.Windows.Forms.NumericUpDown();
			udRegexPublisher = new System.Windows.Forms.NumericUpDown();
			cboPresets = new System.Windows.Forms.ComboBox();
			cboTest = new System.Windows.Forms.ComboBox();
			label9 = new System.Windows.Forms.Label();
			TableLayoutPanel1.SuspendLayout();
			tabImport.SuspendLayout();
			TabPage1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)udDefaultVolume).BeginInit();
			TabPage3.SuspendLayout();
			TabPage2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)udRegexIssue).BeginInit();
			((System.ComponentModel.ISupportInitialize)udRegexPlot).BeginInit();
			((System.ComponentModel.ISupportInitialize)udRegexVolume).BeginInit();
			((System.ComponentModel.ISupportInitialize)udRegexSeries).BeginInit();
			((System.ComponentModel.ISupportInitialize)udRegexGroup).BeginInit();
			((System.ComponentModel.ISupportInitialize)udRegexPublisher).BeginInit();
			SuspendLayout();
			TableLayoutPanel1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			TableLayoutPanel1.ColumnCount = 2;
			TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50f));
			TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50f));
			TableLayoutPanel1.Controls.Add(Cancel_Button, 1, 0);
			TableLayoutPanel1.Controls.Add(OK_Button, 0, 0);
			TableLayoutPanel1.Location = new System.Drawing.Point(193, 286);
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
			Cancel_Button.TabIndex = 15;
			Cancel_Button.Text = "Cancel";
			Cancel_Button.Click += new System.EventHandler(Cancel_Button_Click);
			OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None;
			OK_Button.DialogResult = System.Windows.Forms.DialogResult.OK;
			OK_Button.Location = new System.Drawing.Point(3, 3);
			OK_Button.Name = "OK_Button";
			OK_Button.Size = new System.Drawing.Size(67, 23);
			OK_Button.TabIndex = 14;
			OK_Button.Text = "OK";
			OK_Button.Click += new System.EventHandler(OK_Button_Click);
			txtPattern.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			txtPattern.Location = new System.Drawing.Point(16, 55);
			txtPattern.Multiline = true;
			txtPattern.Name = "txtPattern";
			txtPattern.Size = new System.Drawing.Size(281, 61);
			txtPattern.TabIndex = 1;
			Label2.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			Label2.AutoSize = true;
			Label2.Font = new System.Drawing.Font("Verdana", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			Label2.Location = new System.Drawing.Point(14, 128);
			Label2.Name = "Label2";
			Label2.Size = new System.Drawing.Size(55, 12);
			Label2.TabIndex = 5;
			Label2.Text = "Publisher:";
			Label3.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			Label3.AutoSize = true;
			Label3.Font = new System.Drawing.Font("Verdana", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			Label3.Location = new System.Drawing.Point(14, 150);
			Label3.Name = "Label3";
			Label3.Size = new System.Drawing.Size(38, 12);
			Label3.TabIndex = 6;
			Label3.Text = "Group:";
			Label4.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			Label4.AutoSize = true;
			Label4.Font = new System.Drawing.Font("Verdana", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			Label4.Location = new System.Drawing.Point(123, 150);
			Label4.Name = "Label4";
			Label4.Size = new System.Drawing.Size(56, 12);
			Label4.TabIndex = 7;
			Label4.Text = "Plot Title:";
			Label5.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			Label5.AutoSize = true;
			Label5.Font = new System.Drawing.Font("Verdana", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			Label5.Location = new System.Drawing.Point(14, 171);
			Label5.Name = "Label5";
			Label5.Size = new System.Drawing.Size(39, 12);
			Label5.TabIndex = 8;
			Label5.Text = "Series:";
			Label6.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			Label6.AutoSize = true;
			Label6.Font = new System.Drawing.Font("Verdana", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			Label6.Location = new System.Drawing.Point(123, 171);
			Label6.Name = "Label6";
			Label6.Size = new System.Drawing.Size(36, 12);
			Label6.TabIndex = 9;
			Label6.Text = "Issue:";
			Label7.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			Label7.AutoSize = true;
			Label7.Font = new System.Drawing.Font("Verdana", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			Label7.Location = new System.Drawing.Point(123, 128);
			Label7.Name = "Label7";
			Label7.Size = new System.Drawing.Size(46, 12);
			Label7.TabIndex = 15;
			Label7.Text = "Volume:";
			cmdTest.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			cmdTest.Location = new System.Drawing.Point(244, 202);
			cmdTest.Name = "cmdTest";
			cmdTest.Size = new System.Drawing.Size(58, 21);
			cmdTest.TabIndex = 9;
			cmdTest.Text = "Test";
			cmdTest.UseVisualStyleBackColor = true;
			cmdTest.Click += new System.EventHandler(cmdTest_Click);
			tabImport.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			tabImport.Controls.Add(TabPage1);
			tabImport.Controls.Add(TabPage3);
			tabImport.Controls.Add(TabPage2);
			tabImport.Location = new System.Drawing.Point(11, 12);
			tabImport.Name = "tabImport";
			tabImport.SelectedIndex = 0;
			tabImport.Size = new System.Drawing.Size(328, 266);
			tabImport.TabIndex = 13;
			TabPage1.Controls.Add(label1);
			TabPage1.Controls.Add(txtDefaultPlot);
			TabPage1.Controls.Add(udDefaultVolume);
			TabPage1.Controls.Add(chkPlot);
			TabPage1.Controls.Add(chkVol);
			TabPage1.Controls.Add(chkSeries);
			TabPage1.Controls.Add(cboDefaultSeries);
			TabPage1.Controls.Add(chkGroup);
			TabPage1.Controls.Add(cboDefaultGroup);
			TabPage1.Controls.Add(chkPublisher);
			TabPage1.Controls.Add(cboDefaultPublisher);
			TabPage1.Location = new System.Drawing.Point(4, 22);
			TabPage1.Name = "TabPage1";
			TabPage1.Padding = new System.Windows.Forms.Padding(3);
			TabPage1.Size = new System.Drawing.Size(320, 240);
			TabPage1.TabIndex = 0;
			TabPage1.Text = "Default";
			TabPage1.UseVisualStyleBackColor = true;
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("Verdana", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label1.Location = new System.Drawing.Point(27, 13);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(252, 24);
			label1.TabIndex = 11;
			label1.Text = "Carbon Comic will use these tags if it cannot find \r\na match for your pattern.";
			label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			txtDefaultPlot.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			txtDefaultPlot.Location = new System.Drawing.Point(104, 196);
			txtDefaultPlot.Name = "txtDefaultPlot";
			txtDefaultPlot.Size = new System.Drawing.Size(188, 20);
			txtDefaultPlot.TabIndex = 10;
			txtDefaultPlot.TextChanged += new System.EventHandler(txtDefaultPlot_TextChanged);
			udDefaultVolume.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			udDefaultVolume.Location = new System.Drawing.Point(104, 158);
			udDefaultVolume.Name = "udDefaultVolume";
			udDefaultVolume.Size = new System.Drawing.Size(47, 20);
			udDefaultVolume.TabIndex = 8;
			udDefaultVolume.Value = new decimal(new int[4]
			{
				1,
				0,
				0,
				0
			});
			udDefaultVolume.ValueChanged += new System.EventHandler(udDefaultVolume_ValueChanged);
			chkPlot.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			chkPlot.AutoSize = true;
			chkPlot.Font = new System.Drawing.Font("Verdana", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			chkPlot.Location = new System.Drawing.Point(16, 199);
			chkPlot.Name = "chkPlot";
			chkPlot.Size = new System.Drawing.Size(73, 16);
			chkPlot.TabIndex = 9;
			chkPlot.Text = "Plot Title:";
			chkPlot.UseVisualStyleBackColor = true;
			chkVol.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			chkVol.AutoSize = true;
			chkVol.Font = new System.Drawing.Font("Verdana", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			chkVol.Location = new System.Drawing.Point(16, 160);
			chkVol.Name = "chkVol";
			chkVol.Size = new System.Drawing.Size(63, 16);
			chkVol.TabIndex = 7;
			chkVol.Text = "Volume:";
			chkVol.UseVisualStyleBackColor = true;
			chkSeries.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			chkSeries.AutoSize = true;
			chkSeries.Font = new System.Drawing.Font("Verdana", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			chkSeries.Location = new System.Drawing.Point(16, 125);
			chkSeries.Name = "chkSeries";
			chkSeries.Size = new System.Drawing.Size(56, 16);
			chkSeries.TabIndex = 5;
			chkSeries.Text = "Series:";
			chkSeries.UseVisualStyleBackColor = true;
			cboDefaultSeries.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			cboDefaultSeries.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cboDefaultSeries.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cboDefaultSeries.FormattingEnabled = true;
			cboDefaultSeries.Location = new System.Drawing.Point(104, 122);
			cboDefaultSeries.Name = "cboDefaultSeries";
			cboDefaultSeries.Size = new System.Drawing.Size(188, 21);
			cboDefaultSeries.TabIndex = 6;
			chkGroup.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			chkGroup.AutoSize = true;
			chkGroup.Font = new System.Drawing.Font("Verdana", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			chkGroup.Location = new System.Drawing.Point(16, 91);
			chkGroup.Name = "chkGroup";
			chkGroup.Size = new System.Drawing.Size(55, 16);
			chkGroup.TabIndex = 3;
			chkGroup.Text = "Group:";
			chkGroup.UseVisualStyleBackColor = true;
			cboDefaultGroup.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			cboDefaultGroup.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cboDefaultGroup.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cboDefaultGroup.FormattingEnabled = true;
			cboDefaultGroup.Location = new System.Drawing.Point(104, 88);
			cboDefaultGroup.Name = "cboDefaultGroup";
			cboDefaultGroup.Size = new System.Drawing.Size(188, 21);
			cboDefaultGroup.TabIndex = 4;
			cboDefaultGroup.SelectedIndexChanged += new System.EventHandler(cboDefaultGroup_SelectedIndexChanged);
			chkPublisher.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			chkPublisher.AutoSize = true;
			chkPublisher.Font = new System.Drawing.Font("Verdana", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			chkPublisher.Location = new System.Drawing.Point(17, 55);
			chkPublisher.Name = "chkPublisher";
			chkPublisher.Size = new System.Drawing.Size(72, 16);
			chkPublisher.TabIndex = 1;
			chkPublisher.Text = "Publisher:";
			chkPublisher.UseVisualStyleBackColor = true;
			cboDefaultPublisher.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			cboDefaultPublisher.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cboDefaultPublisher.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cboDefaultPublisher.FormattingEnabled = true;
			cboDefaultPublisher.Location = new System.Drawing.Point(104, 52);
			cboDefaultPublisher.Name = "cboDefaultPublisher";
			cboDefaultPublisher.Size = new System.Drawing.Size(188, 21);
			cboDefaultPublisher.TabIndex = 2;
			cboDefaultPublisher.SelectedIndexChanged += new System.EventHandler(cboDefaultPublisher_SelectedIndexChanged);
			TabPage3.Controls.Add(cmdBuffer);
			TabPage3.Controls.Add(label8);
			TabPage3.Controls.Add(cmdOption);
			TabPage3.Controls.Add(cmdConvert);
			TabPage3.Controls.Add(cmdPublisher);
			TabPage3.Controls.Add(cmdGroup);
			TabPage3.Controls.Add(cmdIssue);
			TabPage3.Controls.Add(cmdPlot);
			TabPage3.Controls.Add(cmdSeries);
			TabPage3.Controls.Add(cmdVolume);
			TabPage3.Controls.Add(txtKeywords);
			TabPage3.Location = new System.Drawing.Point(4, 22);
			TabPage3.Name = "TabPage3";
			TabPage3.Size = new System.Drawing.Size(320, 240);
			TabPage3.TabIndex = 2;
			TabPage3.Text = "Keywords";
			TabPage3.UseVisualStyleBackColor = true;
			cmdBuffer.Location = new System.Drawing.Point(74, 133);
			cmdBuffer.Name = "cmdBuffer";
			cmdBuffer.Size = new System.Drawing.Size(80, 23);
			cmdBuffer.TabIndex = 13;
			cmdBuffer.Text = "Buffer";
			cmdBuffer.UseVisualStyleBackColor = true;
			cmdBuffer.Click += new System.EventHandler(cmdBuffer_Click);
			label8.AutoSize = true;
			label8.Font = new System.Drawing.Font("Verdana", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label8.Location = new System.Drawing.Point(17, 11);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(279, 24);
			label8.TabIndex = 12;
			label8.Text = "If your comic files are similarly named, this will enable \r\nyou to create a pattern to match each filename.";
			label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			cmdOption.Location = new System.Drawing.Point(160, 133);
			cmdOption.Name = "cmdOption";
			cmdOption.Size = new System.Drawing.Size(79, 23);
			cmdOption.TabIndex = 7;
			cmdOption.Text = "Optional Tag";
			cmdOption.UseVisualStyleBackColor = true;
			cmdOption.Click += new System.EventHandler(cmdOption_Click);
			cmdConvert.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmdConvert.Location = new System.Drawing.Point(80, 197);
			cmdConvert.Name = "cmdConvert";
			cmdConvert.Size = new System.Drawing.Size(159, 23);
			cmdConvert.TabIndex = 9;
			cmdConvert.Text = "Convert to Regular Expression";
			cmdConvert.UseVisualStyleBackColor = true;
			cmdConvert.Click += new System.EventHandler(cmdConvert_Click);
			cmdPublisher.Location = new System.Drawing.Point(59, 49);
			cmdPublisher.Name = "cmdPublisher";
			cmdPublisher.Size = new System.Drawing.Size(95, 22);
			cmdPublisher.TabIndex = 1;
			cmdPublisher.Text = "Publisher";
			cmdPublisher.UseVisualStyleBackColor = true;
			cmdPublisher.Click += new System.EventHandler(cmdPublisher_Click);
			cmdGroup.Location = new System.Drawing.Point(160, 49);
			cmdGroup.Name = "cmdGroup";
			cmdGroup.Size = new System.Drawing.Size(95, 22);
			cmdGroup.TabIndex = 2;
			cmdGroup.Text = "Group";
			cmdGroup.UseVisualStyleBackColor = true;
			cmdGroup.Click += new System.EventHandler(cmdGroup_Click);
			cmdIssue.Location = new System.Drawing.Point(59, 105);
			cmdIssue.Name = "cmdIssue";
			cmdIssue.Size = new System.Drawing.Size(95, 22);
			cmdIssue.TabIndex = 5;
			cmdIssue.Text = "Issue Number";
			cmdIssue.UseVisualStyleBackColor = true;
			cmdIssue.Click += new System.EventHandler(cmdIssue_Click);
			cmdPlot.AccessibleDescription = "";
			cmdPlot.Location = new System.Drawing.Point(160, 105);
			cmdPlot.Name = "cmdPlot";
			cmdPlot.Size = new System.Drawing.Size(95, 22);
			cmdPlot.TabIndex = 6;
			cmdPlot.Text = "Plot Title";
			cmdPlot.UseVisualStyleBackColor = true;
			cmdPlot.Click += new System.EventHandler(cmdPlot_Click);
			cmdSeries.Location = new System.Drawing.Point(59, 77);
			cmdSeries.Name = "cmdSeries";
			cmdSeries.Size = new System.Drawing.Size(95, 22);
			cmdSeries.TabIndex = 3;
			cmdSeries.Text = "Series Name";
			cmdSeries.UseVisualStyleBackColor = true;
			cmdSeries.Click += new System.EventHandler(cmdSeries_Click);
			cmdVolume.Location = new System.Drawing.Point(160, 77);
			cmdVolume.Name = "cmdVolume";
			cmdVolume.Size = new System.Drawing.Size(95, 22);
			cmdVolume.TabIndex = 4;
			cmdVolume.Text = "Series Volume";
			cmdVolume.UseVisualStyleBackColor = true;
			cmdVolume.Click += new System.EventHandler(cmdVolume_Click);
			txtKeywords.Location = new System.Drawing.Point(19, 171);
			txtKeywords.Name = "txtKeywords";
			txtKeywords.Size = new System.Drawing.Size(277, 20);
			txtKeywords.TabIndex = 8;
			TabPage2.Controls.Add(udRegexIssue);
			TabPage2.Controls.Add(udRegexPlot);
			TabPage2.Controls.Add(udRegexVolume);
			TabPage2.Controls.Add(udRegexSeries);
			TabPage2.Controls.Add(udRegexGroup);
			TabPage2.Controls.Add(udRegexPublisher);
			TabPage2.Controls.Add(cboPresets);
			TabPage2.Controls.Add(cboTest);
			TabPage2.Controls.Add(cmdTest);
			TabPage2.Controls.Add(txtPattern);
			TabPage2.Controls.Add(Label2);
			TabPage2.Controls.Add(Label4);
			TabPage2.Controls.Add(Label5);
			TabPage2.Controls.Add(Label6);
			TabPage2.Controls.Add(Label3);
			TabPage2.Controls.Add(Label7);
			TabPage2.Controls.Add(label9);
			TabPage2.Location = new System.Drawing.Point(4, 22);
			TabPage2.Name = "TabPage2";
			TabPage2.Padding = new System.Windows.Forms.Padding(3);
			TabPage2.Size = new System.Drawing.Size(320, 240);
			TabPage2.TabIndex = 1;
			TabPage2.Text = "Regular Expression";
			TabPage2.UseVisualStyleBackColor = true;
			udRegexIssue.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			udRegexIssue.Location = new System.Drawing.Point(188, 169);
			udRegexIssue.Maximum = new decimal(new int[4]
			{
				99,
				0,
				0,
				0
			});
			udRegexIssue.Minimum = new decimal(new int[4]
			{
				1,
				0,
				0,
				-2147483648
			});
			udRegexIssue.Name = "udRegexIssue";
			udRegexIssue.Size = new System.Drawing.Size(42, 20);
			udRegexIssue.TabIndex = 25;
			udRegexIssue.Value = new decimal(new int[4]
			{
				1,
				0,
				0,
				-2147483648
			});
			udRegexPlot.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			udRegexPlot.Location = new System.Drawing.Point(188, 148);
			udRegexPlot.Maximum = new decimal(new int[4]
			{
				99,
				0,
				0,
				0
			});
			udRegexPlot.Minimum = new decimal(new int[4]
			{
				1,
				0,
				0,
				-2147483648
			});
			udRegexPlot.Name = "udRegexPlot";
			udRegexPlot.Size = new System.Drawing.Size(42, 20);
			udRegexPlot.TabIndex = 24;
			udRegexPlot.Value = new decimal(new int[4]
			{
				1,
				0,
				0,
				-2147483648
			});
			udRegexVolume.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			udRegexVolume.Location = new System.Drawing.Point(188, 126);
			udRegexVolume.Maximum = new decimal(new int[4]
			{
				99,
				0,
				0,
				0
			});
			udRegexVolume.Minimum = new decimal(new int[4]
			{
				1,
				0,
				0,
				-2147483648
			});
			udRegexVolume.Name = "udRegexVolume";
			udRegexVolume.Size = new System.Drawing.Size(42, 20);
			udRegexVolume.TabIndex = 23;
			udRegexVolume.Value = new decimal(new int[4]
			{
				1,
				0,
				0,
				-2147483648
			});
			udRegexSeries.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			udRegexSeries.Location = new System.Drawing.Point(75, 169);
			udRegexSeries.Maximum = new decimal(new int[4]
			{
				99,
				0,
				0,
				0
			});
			udRegexSeries.Minimum = new decimal(new int[4]
			{
				1,
				0,
				0,
				-2147483648
			});
			udRegexSeries.Name = "udRegexSeries";
			udRegexSeries.Size = new System.Drawing.Size(42, 20);
			udRegexSeries.TabIndex = 22;
			udRegexSeries.Value = new decimal(new int[4]
			{
				1,
				0,
				0,
				-2147483648
			});
			udRegexGroup.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			udRegexGroup.Location = new System.Drawing.Point(75, 148);
			udRegexGroup.Maximum = new decimal(new int[4]
			{
				99,
				0,
				0,
				0
			});
			udRegexGroup.Minimum = new decimal(new int[4]
			{
				1,
				0,
				0,
				-2147483648
			});
			udRegexGroup.Name = "udRegexGroup";
			udRegexGroup.Size = new System.Drawing.Size(42, 20);
			udRegexGroup.TabIndex = 21;
			udRegexGroup.Value = new decimal(new int[4]
			{
				1,
				0,
				0,
				-2147483648
			});
			udRegexPublisher.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			udRegexPublisher.Location = new System.Drawing.Point(75, 126);
			udRegexPublisher.Maximum = new decimal(new int[4]
			{
				99,
				0,
				0,
				0
			});
			udRegexPublisher.Minimum = new decimal(new int[4]
			{
				1,
				0,
				0,
				-2147483648
			});
			udRegexPublisher.Name = "udRegexPublisher";
			udRegexPublisher.Size = new System.Drawing.Size(42, 20);
			udRegexPublisher.TabIndex = 20;
			udRegexPublisher.Value = new decimal(new int[4]
			{
				1,
				0,
				0,
				-2147483648
			});
			cboPresets.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			cboPresets.FormattingEnabled = true;
			cboPresets.Location = new System.Drawing.Point(16, 28);
			cboPresets.Name = "cboPresets";
			cboPresets.Size = new System.Drawing.Size(281, 21);
			cboPresets.Sorted = true;
			cboPresets.TabIndex = 16;
			cboPresets.SelectedIndexChanged += new System.EventHandler(cboPresets_SelectedIndexChanged);
			cboTest.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			cboTest.FormattingEnabled = true;
			cboTest.Location = new System.Drawing.Point(16, 202);
			cboTest.Name = "cboTest";
			cboTest.Size = new System.Drawing.Size(222, 21);
			cboTest.TabIndex = 8;
			label9.AutoSize = true;
			label9.Font = new System.Drawing.Font("Verdana", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label9.Location = new System.Drawing.Point(14, 13);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(288, 12);
			label9.TabIndex = 19;
			label9.Text = "More advanced users can input their own regex patterns.";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(349, 323);
			base.Controls.Add(tabImport);
			base.Controls.Add(TableLayoutPanel1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ImportForm";
			base.ShowInTaskbar = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Import Auto-Tag";
			base.Load += new System.EventHandler(ImportForm_Load);
			TableLayoutPanel1.ResumeLayout(false);
			tabImport.ResumeLayout(false);
			TabPage1.ResumeLayout(false);
			TabPage1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)udDefaultVolume).EndInit();
			TabPage3.ResumeLayout(false);
			TabPage3.PerformLayout();
			TabPage2.ResumeLayout(false);
			TabPage2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)udRegexIssue).EndInit();
			((System.ComponentModel.ISupportInitialize)udRegexPlot).EndInit();
			((System.ComponentModel.ISupportInitialize)udRegexVolume).EndInit();
			((System.ComponentModel.ISupportInitialize)udRegexSeries).EndInit();
			((System.ComponentModel.ISupportInitialize)udRegexGroup).EndInit();
			((System.ComponentModel.ISupportInitialize)udRegexPublisher).EndInit();
			ResumeLayout(false);
		}
	}
}
