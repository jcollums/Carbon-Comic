using CarbonComic.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CarbonComic
{
    /// <summary>
    /// This form sets global preferences
    /// Accessed through Settings.Default
    /// </summary>
	public class PreferencesForm : Form
    {
        private IContainer components;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        internal TableLayoutPanel TableLayoutPanel1;
        internal Button Cancel_Button;
        internal Button OK_Button;
        private Button cmdLibrary;
        private TextBox txtLibrary;
        private GroupBox groupBox1;
        private Label label9;
        private NumericUpDown udRatio;
        private Label label8;
        private NumericUpDown udHeight;
        private Label label6;
        private NumericUpDown udWidth;
        private Label label5;
        private NumericUpDown udQuality;
        private GroupBox groupBox2;
        private Label label4;
        private Button cmdPDF;
        private TextBox txtPDF;
        private Label label7;
        private Button cmdZIP;
        private TextBox txtZIP;
        private Label label3;
        private Button cmdRAR;
        private TextBox txtRAR;
        private ComboBox cboViewMode;
        private Label label10;
        private CheckBox chkThumbnail;
        private Label label1;
        private TabPage tabPage3;
        private Label label22;
        private Label label21;
        private CheckBox chkDuplicates;
        internal TabControl tabImport;
        private TabPage tabPage9;
        private Label label20;
        private TabPage tabPage8;
        private GroupBox groupBox3;
        private TextBox textBox1;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
        private ComboBox comboBox1;
        internal TabPage tabPage5;
        private Button cmdOption;
        internal Button cmdConvert;
        internal Button cmdPublisher;
        internal Button cmdGroup;
        internal Button cmdIssue;
        internal Button cmdPlot;
        internal Button cmdSeries;
        internal Button cmdVolume;
        internal TextBox txtKeywords;
        internal TabPage tabPage6;
        internal ComboBox cboTest;
        internal Button cmdTest;
        internal TextBox txtRegexPublisher;
        internal TextBox txtRegexGroup;
        internal TextBox txtPattern;
        internal TextBox txtRegexPlot;
        internal Label label12;
        internal TextBox txtRegexSeries;
        internal Label label13;
        internal Label label14;
        internal Label label15;
        internal Label label16;
        internal TextBox txtRegexIssue;
        internal TextBox txtRegexVolume;
        internal Label label17;
        private TabPage tabPage7;
        private Button button2;
        private Button button1;
        private Label label19;
        private ListBox listBox2;
        private Label label18;
        private ListBox listBox1;
        private Label label2;
        private ComboBox cboOrganizeMethod;
        private Label label23;
        private Label label11;

        public PreferencesForm()
        {
            InitializeComponent();
            tabControl1.SelectedIndexChanged += tabControl1_SelectedIndexChanged;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    base.Height = 275;
                    break;
                case 1:
                    base.Height = 335;
                    break;
                case 2:
                    base.Height = 255;
                    break;
            }
        }

        private void ChangeLibraryDir(string NewDir)
        {
            Settings.Default.LibraryDir = NewDir;
        }

        private void OK_Button_Click(object sender, EventArgs e)
        {
            //Determine all the options that might require the user to Reprocess the library
            bool flag = false;
            if (Settings.Default.OrganizeMethod != cboOrganizeMethod.SelectedIndex && cboOrganizeMethod.SelectedIndex != 0)
            {
                flag = true;
            }
            if (Settings.Default.LibraryDir != txtLibrary.Text)
            {
                flag = true;
            }
            if (Settings.Default.ThumbQuality != (int)udQuality.Value)
            {
                flag = true;
            }
            if (Settings.Default.ThumbHeight != (int)udHeight.Value)
            {
                flag = true;
            }
            if (Settings.Default.ThumbWidth != (int)udWidth.Value)
            {
                flag = true;
            }
            if (flag)
            {
                MessageBox.Show("To apply Comics Library, Organize Method, or Thumbnail changes you must choose \"Reprocess All\" from the Tools menu.", "Changed Settings", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            Settings.Default.OrganizeMethod = cboOrganizeMethod.SelectedIndex;
            ChangeLibraryDir(txtLibrary.Text);
            Settings.Default.ThumbGen = chkThumbnail.Checked;
            Settings.Default.ThumbRatio = udRatio.Value;
            Settings.Default.ThumbQuality = (int)udQuality.Value;
            Settings.Default.ThumbWidth = (int)udWidth.Value;
            Settings.Default.ThumbHeight = (int)udHeight.Value;
            Settings.Default.RAREditor = txtRAR.Text;
            Settings.Default.ZIPEditor = txtZIP.Text;
            Settings.Default.PDFEditor = txtPDF.Text;
            Settings.Default.ViewMode = cboViewMode.SelectedIndex;
            Settings.Default.FindDuplicates = chkDuplicates.Checked;
            Settings.Default.Save();
            base.DialogResult = DialogResult.OK;
            Close();
        }

        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.Cancel;
            Close();
        }

        private void PreferencesForm_Load(object sender, EventArgs e)
        {
            chkDuplicates.Checked = Settings.Default.FindDuplicates;
            cboOrganizeMethod.SelectedIndex = Settings.Default.OrganizeMethod;
            chkThumbnail.Checked = Settings.Default.ThumbGen;
            udRatio.Value = Settings.Default.ThumbRatio;
            udQuality.Value = Settings.Default.ThumbQuality;
            udWidth.Value = Settings.Default.ThumbWidth;
            udHeight.Value = Settings.Default.ThumbHeight;
            txtLibrary.Text = Settings.Default.LibraryDir;
            txtRAR.Text = Settings.Default.RAREditor;
            txtZIP.Text = Settings.Default.ZIPEditor;
            txtPDF.Text = Settings.Default.PDFEditor;
            cboViewMode.SelectedIndex = Settings.Default.ViewMode;
        }

        //Make the thumbnail dimensions propotional
        private void udWidth_ValueChanged(object sender, EventArgs e)
        {
            /*
			int h = (int)(udWidth.Value / udRatio.Value);

            if (h > udHeight.Maximum)
            {
                h = (int)udHeight.Maximum;
            }
            udHeight.Value = h;
            */
        }

        //Make the thumbnail dimensions propotional
        private void udHeight_ValueChanged(object sender, EventArgs e)
        {
            int w = (int)(udHeight.Value * udRatio.Value);


            if (w > udWidth.Maximum)
            {
                w = (int)udWidth.Maximum;
            }
            udWidth.Value = w;
        }


        private void cmdLibrary_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowNewFolderButton = true;
            folderBrowserDialog.SelectedPath = txtLibrary.Text;
            folderBrowserDialog.Description = "Choose a folder to store all of your imported comic files.";
            DialogResult dialogResult = folderBrowserDialog.ShowDialog();
            if (dialogResult != DialogResult.Cancel)
            {
                txtLibrary.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void cmdRAR_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.FileName = txtRAR.Text;
            openFileDialog.Title = "RAR Editor";
            openFileDialog.Filter = "Programs (*.exe)|*.exe";
            DialogResult dialogResult = openFileDialog.ShowDialog();
            if (dialogResult != DialogResult.Cancel)
            {
                txtRAR.Text = openFileDialog.FileName;
            }
        }

        private void cmdZIP_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.FileName = txtZIP.Text;
            openFileDialog.Title = "ZIP Editor";
            openFileDialog.Filter = "Programs (*.exe)|*.exe";
            DialogResult dialogResult = openFileDialog.ShowDialog();
            if (dialogResult != DialogResult.Cancel)
            {
                txtZIP.Text = openFileDialog.FileName;
            }
        }

        private void cmdPDF_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.FileName = txtPDF.Text;
            openFileDialog.Title = "PDF Editor";
            openFileDialog.Filter = "Programs (*.exe)|*.exe";
            DialogResult dialogResult = openFileDialog.ShowDialog();
            if (dialogResult != DialogResult.Cancel)
            {
                txtPDF.Text = openFileDialog.FileName;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreferencesForm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.cboViewMode = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkThumbnail = new System.Windows.Forms.CheckBox();
            this.udRatio = new System.Windows.Forms.NumericUpDown();
            this.udHeight = new System.Windows.Forms.NumericUpDown();
            this.udWidth = new System.Windows.Forms.NumericUpDown();
            this.udQuality = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.cmdLibrary = new System.Windows.Forms.Button();
            this.txtLibrary = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txtPDF = new System.Windows.Forms.TextBox();
            this.txtZIP = new System.Windows.Forms.TextBox();
            this.txtRAR = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmdPDF = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.cmdZIP = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdRAR = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label11 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboOrganizeMethod = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.chkDuplicates = new System.Windows.Forms.CheckBox();
            this.TableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Cancel_Button = new System.Windows.Forms.Button();
            this.OK_Button = new System.Windows.Forms.Button();
            this.tabImport = new System.Windows.Forms.TabControl();
            this.tabPage9 = new System.Windows.Forms.TabPage();
            this.label20 = new System.Windows.Forms.Label();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.cmdOption = new System.Windows.Forms.Button();
            this.cmdConvert = new System.Windows.Forms.Button();
            this.cmdPublisher = new System.Windows.Forms.Button();
            this.cmdGroup = new System.Windows.Forms.Button();
            this.cmdIssue = new System.Windows.Forms.Button();
            this.cmdPlot = new System.Windows.Forms.Button();
            this.cmdSeries = new System.Windows.Forms.Button();
            this.cmdVolume = new System.Windows.Forms.Button();
            this.txtKeywords = new System.Windows.Forms.TextBox();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.cboTest = new System.Windows.Forms.ComboBox();
            this.cmdTest = new System.Windows.Forms.Button();
            this.txtRegexPublisher = new System.Windows.Forms.TextBox();
            this.txtRegexGroup = new System.Windows.Forms.TextBox();
            this.txtPattern = new System.Windows.Forms.TextBox();
            this.txtRegexPlot = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtRegexSeries = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.txtRegexIssue = new System.Windows.Forms.TextBox();
            this.txtRegexVolume = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.label18 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udRatio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udQuality)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.TableLayoutPanel1.SuspendLayout();
            this.tabImport.SuspendLayout();
            this.tabPage9.SuspendLayout();
            this.tabPage8.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(345, 202);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.cboViewMode);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(337, 176);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Appearance";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // cboViewMode
            // 
            this.cboViewMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboViewMode.FormattingEnabled = true;
            this.cboViewMode.Items.AddRange(new object[] {
            "Cover Art",
            "Details"});
            this.cboViewMode.Location = new System.Drawing.Point(145, 136);
            this.cboViewMode.Name = "cboViewMode";
            this.cboViewMode.Size = new System.Drawing.Size(156, 21);
            this.cboViewMode.TabIndex = 16;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(35, 140);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(104, 12);
            this.label10.TabIndex = 15;
            this.label10.Text = "Default View Mode:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkThumbnail);
            this.groupBox1.Controls.Add(this.udRatio);
            this.groupBox1.Controls.Add(this.udHeight);
            this.groupBox1.Controls.Add(this.udWidth);
            this.groupBox1.Controls.Add(this.udQuality);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(17, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(312, 110);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cover Art Thumbnails";
            // 
            // chkThumbnail
            // 
            this.chkThumbnail.AutoSize = true;
            this.chkThumbnail.Checked = true;
            this.chkThumbnail.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkThumbnail.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkThumbnail.Location = new System.Drawing.Point(59, 26);
            this.chkThumbnail.Name = "chkThumbnail";
            this.chkThumbnail.Size = new System.Drawing.Size(204, 16);
            this.chkThumbnail.TabIndex = 17;
            this.chkThumbnail.Text = "Generate Thumbnails Automatically";
            this.chkThumbnail.UseVisualStyleBackColor = true;
            // 
            // udRatio
            // 
            this.udRatio.DecimalPlaces = 2;
            this.udRatio.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.udRatio.Location = new System.Drawing.Point(234, 73);
            this.udRatio.Name = "udRatio";
            this.udRatio.Size = new System.Drawing.Size(63, 18);
            this.udRatio.TabIndex = 14;
            // 
            // udHeight
            // 
            this.udHeight.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.udHeight.Location = new System.Drawing.Point(157, 73);
            this.udHeight.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.udHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udHeight.Name = "udHeight";
            this.udHeight.Size = new System.Drawing.Size(65, 18);
            this.udHeight.TabIndex = 12;
            this.udHeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udHeight.ValueChanged += new System.EventHandler(this.udHeight_ValueChanged);
            // 
            // udWidth
            // 
            this.udWidth.Enabled = false;
            this.udWidth.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.udWidth.Location = new System.Drawing.Point(85, 73);
            this.udWidth.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.udWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udWidth.Name = "udWidth";
            this.udWidth.Size = new System.Drawing.Size(65, 18);
            this.udWidth.TabIndex = 10;
            this.udWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udWidth.ValueChanged += new System.EventHandler(this.udWidth_ValueChanged);
            // 
            // udQuality
            // 
            this.udQuality.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.udQuality.Location = new System.Drawing.Point(9, 73);
            this.udQuality.Name = "udQuality";
            this.udQuality.Size = new System.Drawing.Size(65, 18);
            this.udQuality.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(231, 57);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(36, 12);
            this.label9.TabIndex = 15;
            this.label9.Text = "Ratio:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(154, 57);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(42, 12);
            this.label8.TabIndex = 13;
            this.label8.Text = "Height:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(82, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 12);
            this.label6.TabIndex = 11;
            this.label6.Text = "Width:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "Quality:";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.cmdLibrary);
            this.tabPage1.Controls.Add(this.txtLibrary);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.label23);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(337, 176);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Locations";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // cmdLibrary
            // 
            this.cmdLibrary.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdLibrary.Location = new System.Drawing.Point(250, 29);
            this.cmdLibrary.Name = "cmdLibrary";
            this.cmdLibrary.Size = new System.Drawing.Size(69, 20);
            this.cmdLibrary.TabIndex = 3;
            this.cmdLibrary.Text = "Browse...";
            this.cmdLibrary.UseVisualStyleBackColor = true;
            this.cmdLibrary.Click += new System.EventHandler(this.cmdLibrary_Click);
            // 
            // txtLibrary
            // 
            this.txtLibrary.Location = new System.Drawing.Point(18, 29);
            this.txtLibrary.Name = "txtLibrary";
            this.txtLibrary.Size = new System.Drawing.Size(226, 20);
            this.txtLibrary.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label22);
            this.groupBox2.Controls.Add(this.txtPDF);
            this.groupBox2.Controls.Add(this.txtZIP);
            this.groupBox2.Controls.Add(this.txtRAR);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.cmdPDF);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.cmdZIP);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cmdRAR);
            this.groupBox2.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(8, 55);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(323, 172);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Editors";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(11, 16);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(279, 12);
            this.label22.TabIndex = 27;
            this.label22.Text = "Recommended if you often remove scanner logo pages.";
            // 
            // txtPDF
            // 
            this.txtPDF.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPDF.Location = new System.Drawing.Point(10, 134);
            this.txtPDF.Name = "txtPDF";
            this.txtPDF.Size = new System.Drawing.Size(226, 18);
            this.txtPDF.TabIndex = 24;
            // 
            // txtZIP
            // 
            this.txtZIP.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtZIP.Location = new System.Drawing.Point(10, 95);
            this.txtZIP.Name = "txtZIP";
            this.txtZIP.Size = new System.Drawing.Size(226, 18);
            this.txtZIP.TabIndex = 21;
            // 
            // txtRAR
            // 
            this.txtRAR.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRAR.Location = new System.Drawing.Point(10, 56);
            this.txtRAR.Name = "txtRAR";
            this.txtRAR.Size = new System.Drawing.Size(226, 18);
            this.txtRAR.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 118);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 12);
            this.label4.TabIndex = 26;
            this.label4.Text = "PDF Editor:";
            // 
            // cmdPDF
            // 
            this.cmdPDF.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPDF.Location = new System.Drawing.Point(242, 134);
            this.cmdPDF.Name = "cmdPDF";
            this.cmdPDF.Size = new System.Drawing.Size(69, 20);
            this.cmdPDF.TabIndex = 25;
            this.cmdPDF.Text = "Browse...";
            this.cmdPDF.UseVisualStyleBackColor = true;
            this.cmdPDF.Click += new System.EventHandler(this.cmdPDF_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 79);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 12);
            this.label7.TabIndex = 23;
            this.label7.Text = "ZIP Editor:";
            // 
            // cmdZIP
            // 
            this.cmdZIP.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdZIP.Location = new System.Drawing.Point(242, 95);
            this.cmdZIP.Name = "cmdZIP";
            this.cmdZIP.Size = new System.Drawing.Size(69, 20);
            this.cmdZIP.TabIndex = 22;
            this.cmdZIP.Text = "Browse...";
            this.cmdZIP.UseVisualStyleBackColor = true;
            this.cmdZIP.Click += new System.EventHandler(this.cmdZIP_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 12);
            this.label3.TabIndex = 20;
            this.label3.Text = "RAR Editor:";
            // 
            // cmdRAR
            // 
            this.cmdRAR.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdRAR.Location = new System.Drawing.Point(242, 56);
            this.cmdRAR.Name = "cmdRAR";
            this.cmdRAR.Size = new System.Drawing.Size(69, 20);
            this.cmdRAR.TabIndex = 19;
            this.cmdRAR.Text = "Browse...";
            this.cmdRAR.UseVisualStyleBackColor = true;
            this.cmdRAR.Click += new System.EventHandler(this.cmdRAR_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "Comics Library:";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(102, 14);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(208, 12);
            this.label23.TabIndex = 28;
            this.label23.Text = "This is where your comics are organized.";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label11);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Controls.Add(this.cboOrganizeMethod);
            this.tabPage3.Controls.Add(this.label21);
            this.tabPage3.Controls.Add(this.chkDuplicates);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(337, 176);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Importing";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(22, 92);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(313, 48);
            this.label11.TabIndex = 23;
            this.label11.Text = resources.GetString("label11.Text");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(22, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 12);
            this.label2.TabIndex = 22;
            this.label2.Text = "Import Method:";
            // 
            // cboOrganizeMethod
            // 
            this.cboOrganizeMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOrganizeMethod.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboOrganizeMethod.FormattingEnabled = true;
            this.cboOrganizeMethod.Items.AddRange(new object[] {
            "Do Nothing With Imported Files",
            "Move To Comics Library",
            "Copy To Comics Library"});
            this.cboOrganizeMethod.Location = new System.Drawing.Point(112, 68);
            this.cboOrganizeMethod.Name = "cboOrganizeMethod";
            this.cboOrganizeMethod.Size = new System.Drawing.Size(210, 21);
            this.cboOrganizeMethod.TabIndex = 21;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(39, 35);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(219, 12);
            this.label21.TabIndex = 1;
            this.label21.Text = "This may slow down the importing process.";
            // 
            // chkDuplicates
            // 
            this.chkDuplicates.AutoSize = true;
            this.chkDuplicates.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDuplicates.Location = new System.Drawing.Point(24, 16);
            this.chkDuplicates.Name = "chkDuplicates";
            this.chkDuplicates.Size = new System.Drawing.Size(144, 16);
            this.chkDuplicates.TabIndex = 0;
            this.chkDuplicates.Text = "Don\'t Import Duplicates";
            this.chkDuplicates.UseVisualStyleBackColor = true;
            // 
            // TableLayoutPanel1
            // 
            this.TableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.TableLayoutPanel1.ColumnCount = 2;
            this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel1.Controls.Add(this.Cancel_Button, 1, 0);
            this.TableLayoutPanel1.Controls.Add(this.OK_Button, 0, 0);
            this.TableLayoutPanel1.Location = new System.Drawing.Point(202, 211);
            this.TableLayoutPanel1.Name = "TableLayoutPanel1";
            this.TableLayoutPanel1.RowCount = 1;
            this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.TableLayoutPanel1.Size = new System.Drawing.Size(146, 29);
            this.TableLayoutPanel1.TabIndex = 5;
            // 
            // Cancel_Button
            // 
            this.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel_Button.Location = new System.Drawing.Point(76, 3);
            this.Cancel_Button.Name = "Cancel_Button";
            this.Cancel_Button.Size = new System.Drawing.Size(67, 23);
            this.Cancel_Button.TabIndex = 1;
            this.Cancel_Button.Text = "Cancel";
            // 
            // OK_Button
            // 
            this.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.OK_Button.Location = new System.Drawing.Point(3, 3);
            this.OK_Button.Name = "OK_Button";
            this.OK_Button.Size = new System.Drawing.Size(67, 23);
            this.OK_Button.TabIndex = 0;
            this.OK_Button.Text = "OK";
            this.OK_Button.Click += new System.EventHandler(this.OK_Button_Click);
            // 
            // tabImport
            // 
            this.tabImport.Controls.Add(this.tabPage9);
            this.tabImport.Controls.Add(this.tabPage8);
            this.tabImport.Controls.Add(this.tabPage5);
            this.tabImport.Controls.Add(this.tabPage6);
            this.tabImport.Controls.Add(this.tabPage7);
            this.tabImport.Enabled = false;
            this.tabImport.Location = new System.Drawing.Point(11, 333);
            this.tabImport.Name = "tabImport";
            this.tabImport.SelectedIndex = 0;
            this.tabImport.Size = new System.Drawing.Size(314, 216);
            this.tabImport.TabIndex = 16;
            this.tabImport.Visible = false;
            // 
            // tabPage9
            // 
            this.tabPage9.Controls.Add(this.label20);
            this.tabPage9.Location = new System.Drawing.Point(4, 22);
            this.tabPage9.Name = "tabPage9";
            this.tabPage9.Size = new System.Drawing.Size(306, 190);
            this.tabPage9.TabIndex = 5;
            this.tabPage9.Text = "File Types";
            this.tabPage9.UseVisualStyleBackColor = true;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(108, 83);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(98, 13);
            this.label20.TabIndex = 0;
            this.label20.Text = "This space for rent.";
            // 
            // tabPage8
            // 
            this.tabPage8.Controls.Add(this.groupBox3);
            this.tabPage8.Controls.Add(this.comboBox1);
            this.tabPage8.Location = new System.Drawing.Point(4, 22);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Size = new System.Drawing.Size(306, 190);
            this.tabPage8.TabIndex = 4;
            this.tabPage8.Text = "Errors";
            this.tabPage8.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBox1);
            this.groupBox3.Controls.Add(this.radioButton2);
            this.groupBox3.Controls.Add(this.radioButton1);
            this.groupBox3.Location = new System.Drawing.Point(17, 46);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(271, 129);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Options";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(16, 86);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(238, 20);
            this.textBox1.TabIndex = 2;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(16, 63);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(127, 17);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Add To This Readlist:";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(16, 28);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(91, 17);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Do Not Import";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Unknown Files",
            "Corrupt Files",
            "Failed Thumbnail Generation",
            "Failed Auto-Tag"});
            this.comboBox1.Location = new System.Drawing.Point(17, 19);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(271, 21);
            this.comboBox1.TabIndex = 0;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.cmdOption);
            this.tabPage5.Controls.Add(this.cmdConvert);
            this.tabPage5.Controls.Add(this.cmdPublisher);
            this.tabPage5.Controls.Add(this.cmdGroup);
            this.tabPage5.Controls.Add(this.cmdIssue);
            this.tabPage5.Controls.Add(this.cmdPlot);
            this.tabPage5.Controls.Add(this.cmdSeries);
            this.tabPage5.Controls.Add(this.cmdVolume);
            this.tabPage5.Controls.Add(this.txtKeywords);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(306, 190);
            this.tabPage5.TabIndex = 2;
            this.tabPage5.Text = "Keywords";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // cmdOption
            // 
            this.cmdOption.Location = new System.Drawing.Point(108, 97);
            this.cmdOption.Name = "cmdOption";
            this.cmdOption.Size = new System.Drawing.Size(79, 23);
            this.cmdOption.TabIndex = 7;
            this.cmdOption.Text = "Optional Tag";
            this.cmdOption.UseVisualStyleBackColor = true;
            // 
            // cmdConvert
            // 
            this.cmdConvert.Location = new System.Drawing.Point(75, 152);
            this.cmdConvert.Name = "cmdConvert";
            this.cmdConvert.Size = new System.Drawing.Size(159, 23);
            this.cmdConvert.TabIndex = 9;
            this.cmdConvert.Text = "Convert to Regular Expression";
            this.cmdConvert.UseVisualStyleBackColor = true;
            // 
            // cmdPublisher
            // 
            this.cmdPublisher.Location = new System.Drawing.Point(51, 15);
            this.cmdPublisher.Name = "cmdPublisher";
            this.cmdPublisher.Size = new System.Drawing.Size(95, 22);
            this.cmdPublisher.TabIndex = 1;
            this.cmdPublisher.Text = "Publisher";
            this.cmdPublisher.UseVisualStyleBackColor = true;
            // 
            // cmdGroup
            // 
            this.cmdGroup.Location = new System.Drawing.Point(152, 15);
            this.cmdGroup.Name = "cmdGroup";
            this.cmdGroup.Size = new System.Drawing.Size(95, 22);
            this.cmdGroup.TabIndex = 2;
            this.cmdGroup.Text = "Group";
            this.cmdGroup.UseVisualStyleBackColor = true;
            // 
            // cmdIssue
            // 
            this.cmdIssue.Location = new System.Drawing.Point(51, 71);
            this.cmdIssue.Name = "cmdIssue";
            this.cmdIssue.Size = new System.Drawing.Size(95, 22);
            this.cmdIssue.TabIndex = 5;
            this.cmdIssue.Text = "Issue Number";
            this.cmdIssue.UseVisualStyleBackColor = true;
            // 
            // cmdPlot
            // 
            this.cmdPlot.AccessibleDescription = "";
            this.cmdPlot.Location = new System.Drawing.Point(152, 71);
            this.cmdPlot.Name = "cmdPlot";
            this.cmdPlot.Size = new System.Drawing.Size(95, 22);
            this.cmdPlot.TabIndex = 6;
            this.cmdPlot.Text = "Plot Title";
            this.cmdPlot.UseVisualStyleBackColor = true;
            // 
            // cmdSeries
            // 
            this.cmdSeries.Location = new System.Drawing.Point(51, 43);
            this.cmdSeries.Name = "cmdSeries";
            this.cmdSeries.Size = new System.Drawing.Size(95, 22);
            this.cmdSeries.TabIndex = 3;
            this.cmdSeries.Text = "Series Name";
            this.cmdSeries.UseVisualStyleBackColor = true;
            // 
            // cmdVolume
            // 
            this.cmdVolume.Location = new System.Drawing.Point(152, 43);
            this.cmdVolume.Name = "cmdVolume";
            this.cmdVolume.Size = new System.Drawing.Size(95, 22);
            this.cmdVolume.TabIndex = 4;
            this.cmdVolume.Text = "Series Volume";
            this.cmdVolume.UseVisualStyleBackColor = true;
            // 
            // txtKeywords
            // 
            this.txtKeywords.Location = new System.Drawing.Point(16, 126);
            this.txtKeywords.Name = "txtKeywords";
            this.txtKeywords.Size = new System.Drawing.Size(277, 20);
            this.txtKeywords.TabIndex = 8;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.cboTest);
            this.tabPage6.Controls.Add(this.cmdTest);
            this.tabPage6.Controls.Add(this.txtRegexPublisher);
            this.tabPage6.Controls.Add(this.txtRegexGroup);
            this.tabPage6.Controls.Add(this.txtPattern);
            this.tabPage6.Controls.Add(this.txtRegexPlot);
            this.tabPage6.Controls.Add(this.label12);
            this.tabPage6.Controls.Add(this.txtRegexSeries);
            this.tabPage6.Controls.Add(this.label13);
            this.tabPage6.Controls.Add(this.label14);
            this.tabPage6.Controls.Add(this.label15);
            this.tabPage6.Controls.Add(this.label16);
            this.tabPage6.Controls.Add(this.txtRegexIssue);
            this.tabPage6.Controls.Add(this.txtRegexVolume);
            this.tabPage6.Controls.Add(this.label17);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(306, 190);
            this.tabPage6.TabIndex = 1;
            this.tabPage6.Text = "Reg Expression";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // cboTest
            // 
            this.cboTest.FormattingEnabled = true;
            this.cboTest.Location = new System.Drawing.Point(12, 156);
            this.cboTest.Name = "cboTest";
            this.cboTest.Size = new System.Drawing.Size(222, 21);
            this.cboTest.TabIndex = 8;
            // 
            // cmdTest
            // 
            this.cmdTest.Location = new System.Drawing.Point(240, 156);
            this.cmdTest.Name = "cmdTest";
            this.cmdTest.Size = new System.Drawing.Size(56, 19);
            this.cmdTest.TabIndex = 9;
            this.cmdTest.Text = "Test";
            this.cmdTest.UseVisualStyleBackColor = true;
            // 
            // txtRegexPublisher
            // 
            this.txtRegexPublisher.Location = new System.Drawing.Point(80, 76);
            this.txtRegexPublisher.Name = "txtRegexPublisher";
            this.txtRegexPublisher.Size = new System.Drawing.Size(27, 20);
            this.txtRegexPublisher.TabIndex = 2;
            this.txtRegexPublisher.Text = "-1";
            // 
            // txtRegexGroup
            // 
            this.txtRegexGroup.Location = new System.Drawing.Point(80, 99);
            this.txtRegexGroup.Name = "txtRegexGroup";
            this.txtRegexGroup.Size = new System.Drawing.Size(27, 20);
            this.txtRegexGroup.TabIndex = 4;
            this.txtRegexGroup.Text = "-1";
            // 
            // txtPattern
            // 
            this.txtPattern.Location = new System.Drawing.Point(12, 17);
            this.txtPattern.Multiline = true;
            this.txtPattern.Name = "txtPattern";
            this.txtPattern.Size = new System.Drawing.Size(281, 53);
            this.txtPattern.TabIndex = 1;
            // 
            // txtRegexPlot
            // 
            this.txtRegexPlot.Location = new System.Drawing.Point(214, 102);
            this.txtRegexPlot.Name = "txtRegexPlot";
            this.txtRegexPlot.Size = new System.Drawing.Size(27, 20);
            this.txtRegexPlot.TabIndex = 5;
            this.txtRegexPlot.Text = "-1";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(9, 80);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(63, 13);
            this.label12.TabIndex = 5;
            this.label12.Text = "Publisher:";
            // 
            // txtRegexSeries
            // 
            this.txtRegexSeries.Location = new System.Drawing.Point(79, 122);
            this.txtRegexSeries.Name = "txtRegexSeries";
            this.txtRegexSeries.Size = new System.Drawing.Size(27, 20);
            this.txtRegexSeries.TabIndex = 6;
            this.txtRegexSeries.Text = "-1";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(140, 105);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(62, 13);
            this.label13.TabIndex = 7;
            this.label13.Text = "Plot Title:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(9, 122);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(46, 13);
            this.label14.TabIndex = 8;
            this.label14.Text = "Series:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(140, 126);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(41, 13);
            this.label15.TabIndex = 9;
            this.label15.Text = "Issue:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(9, 99);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(45, 13);
            this.label16.TabIndex = 6;
            this.label16.Text = "Group:";
            // 
            // txtRegexIssue
            // 
            this.txtRegexIssue.Location = new System.Drawing.Point(214, 126);
            this.txtRegexIssue.Name = "txtRegexIssue";
            this.txtRegexIssue.Size = new System.Drawing.Size(27, 20);
            this.txtRegexIssue.TabIndex = 7;
            this.txtRegexIssue.Text = "-1";
            // 
            // txtRegexVolume
            // 
            this.txtRegexVolume.Location = new System.Drawing.Point(214, 76);
            this.txtRegexVolume.Name = "txtRegexVolume";
            this.txtRegexVolume.Size = new System.Drawing.Size(27, 20);
            this.txtRegexVolume.TabIndex = 3;
            this.txtRegexVolume.Text = "-1";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(140, 79);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(52, 13);
            this.label17.TabIndex = 15;
            this.label17.Text = "Volume:";
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.button2);
            this.tabPage7.Controls.Add(this.button1);
            this.tabPage7.Controls.Add(this.label19);
            this.tabPage7.Controls.Add(this.listBox2);
            this.tabPage7.Controls.Add(this.label18);
            this.tabPage7.Controls.Add(this.listBox1);
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Size = new System.Drawing.Size(306, 190);
            this.tabPage7.TabIndex = 3;
            this.tabPage7.Text = "Replace";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(150, 158);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Remove";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(69, 158);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Add";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(168, 15);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(28, 13);
            this.label19.TabIndex = 3;
            this.label19.Text = "Text";
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Items.AddRange(new object[] {
            "\": \""});
            this.listBox2.Location = new System.Drawing.Point(163, 31);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(127, 121);
            this.listBox2.TabIndex = 2;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(10, 15);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(28, 13);
            this.label18.TabIndex = 1;
            this.label18.Text = "Text";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Items.AddRange(new object[] {
            "\" - \""});
            this.listBox1.Location = new System.Drawing.Point(13, 31);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(131, 121);
            this.listBox1.TabIndex = 0;
            // 
            // PreferencesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 242);
            this.Controls.Add(this.tabImport);
            this.Controls.Add(this.TableLayoutPanel1);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PreferencesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Preferences";
            this.Load += new System.EventHandler(this.PreferencesForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udRatio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udQuality)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.TableLayoutPanel1.ResumeLayout(false);
            this.tabImport.ResumeLayout(false);
            this.tabPage9.ResumeLayout(false);
            this.tabPage9.PerformLayout();
            this.tabPage8.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            this.tabPage7.ResumeLayout(false);
            this.tabPage7.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}
