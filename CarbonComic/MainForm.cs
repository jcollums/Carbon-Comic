using CarbonComic.Properties;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CarbonComic
{
    public class MainForm : Form
    {
        private IContainer components;
        internal ColumnHeader colSeries;
        internal ContextMenuStrip BrowseMenu;
        internal ToolStripMenuItem BrowseInfoMenu;
        internal ToolStripMenuItem BrowsePublisherMenu;
        internal ToolStripMenuItem BrowseGroupMenu;
        internal ToolStripMenuItem BrowseMergeMenu;
        internal ToolStripMenuItem BrowseClearMenu;
        internal SplitContainer SourceSplit;
        internal ListView SourceList;
        internal ColumnHeader colSource;
        internal ContextMenuStrip SourceMenu;
        internal ToolStripMenuItem SourceRenameMenu;
        internal ToolStripMenuItem SourceClearMenu;
        internal ImageList SourceIcons;
        internal PictureBox CoverPreview;
        internal SplitContainer ContentSplit;
        internal VScrollBar vsSeries;
        internal ListView SeriesList;
        internal VScrollBar vsGroups;
        internal VScrollBar vsPublishers;
        internal ListView GroupList;
        internal ColumnHeader colGroups;
        internal ListView PublisherList;
        internal ColumnHeader colPublishers;
        internal ListView IssueList;
        internal ColumnHeader StatCol;
        internal ColumnHeader IssueCol;
        internal ColumnHeader NameCol;
        internal ColumnHeader SeriesCol;
        internal ColumnHeader VolCol;
        internal ColumnHeader PubCol;
        internal ColumnHeader AddedCol;
        internal ColumnHeader FileNameCol;
        internal ColumnHeader FileSizeCol;
        internal ContextMenuStrip IssueMenu;
        internal ToolStripMenuItem IssueInfoMenu;
        internal ToolStripMenuItem IssueEditMenu;
        internal ToolStripMenuItem IssueShowFileMenu;
        internal ToolStripMenuItem IssueClearMenu;
        internal ToolStripMenuItem IssueReadlistMenu;
        internal ImageList IssueCovers;
        internal ImageList StatIcons;
        internal ToolStripMenuItem SearchSeriesMenu;
        internal ToolStripMenuItem SearchHeaderMenu;
        internal ToolStripMenuItem SearchPlotMenu;
        internal ToolStripMenuItem SearchGroupMenu;
        internal SplitContainer MainSplit;
        internal ToolStripMenuItem ViewModeList;
        internal ToolStripMenuItem ViewModeCovers;
        internal ToolStripDropDownButton ViewModeButton;
        internal ToolStripMenuItem EditUnmarkMenu;
        internal ToolStripMenuItem EditSelectAllMenu;
        internal ToolStripSeparator ToolStripSeparator3;
        internal ToolStripMenuItem EditMenu;
        internal ToolStripMenuItem EditCopyMenu;
        internal ToolStripMenuItem EditMarkMenu;
        internal ToolStripMenuItem EditUndoRenameMenu;
        internal ToolStripMenuItem EditReprocessMenu;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem EditCoverMenu;
        private ToolStripMenuItem EditBrowserMenu;
        private ToolStripMenuItem EditSourceMenu;
        internal CheckBox chkSearch;
        internal ToolStripMenuItem ToolsReprocessMenu;
        internal ToolStripMenuItem ToolsConvertMenu;
        internal ToolStripMenuItem ToolsConvertCBRMenu;
        internal ToolStripMenuItem ToolsConvertCBZMenu;
        internal ToolStripMenuItem ToolsConvertPDFMenu;
        internal ToolStripMenuItem ToolsMenu;
        internal ToolStripMenuItem ToolsDuplicatesMenu;
        internal ToolStripMenuItem ToolsMissingMenu;
        internal StatusStrip statStrip;
        internal ToolStripStatusLabel lblStats;
        internal Label lblInfo;
        internal ToolStripSeparator ToolStripSeparator4;
        internal ToolStripMenuItem FileExitMenu;
        internal ToolStripMenuItem FileAddFolderMenu;
        internal ToolStripSeparator ToolStripSeparator1;
        internal ToolStripMenuItem FileAddFilesMenu;
        internal ToolStripMenuItem HelpMenu;
        internal ToolStripMenuItem HelpAboutMenu;
        internal ContextMenuStrip SearchMenu;
        internal ToolStripMenuItem SearchPublisherMenu;
        internal ToolStripMenuItem SeriesLimitedMenu;
        internal ToolStripMenuItem SeriesOneshotsMenu;
        internal ProgressBar Progress;
        internal TextBox txtSearch;
        internal ToolStripMenuItem SeriesNormalMenu;
        internal ToolStripMenuItem FileMenu;
        internal ToolStripMenuItem FileNewReadlistMenu;
        internal MenuStrip MainMenu;
        internal ContextMenuStrip SeriesMenu;
        internal ToolStripMenuItem SeriesAllMenu;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripMenuItem EditPrefMenu;
        public ImageList PublisherLogos;
        private ToolStripMenuItem SourceOpenMenu;
        private BackgroundWorker Importer;
        private PictureBox StatusStop;
        private PictureBox StatusSwitch;
        private BackgroundWorker Reprocessor;
        private ToolStripMenuItem DeleteFromLibrary;
        private ToolStripMenuItem SearchFilenameMenu;
        private ToolStripMenuItem autoTagToolStripMenuItem1;
        public Readlist CurrentReadlist;
        public ArrayList BrowseIssues = new ArrayList();
        public ArrayList BrowseGroups = new ArrayList();
        public ArrayList BrowseSeries = new ArrayList();
        public ArrayList BrowsePublishers = new ArrayList();
        public string BrowseTotalSize;
        public ArrayList SearchIssues = new ArrayList();
        public ArrayList SearchGroups = new ArrayList();
        public ArrayList SearchSeries = new ArrayList();
        public ArrayList SearchPublishers = new ArrayList();
        public string SearchTotalSize;
        private static MainForm pointer;

        public string OrderMode = "DESC";

        public string[] OrdersASC = new string[9]
		{
			"IIF(missing, 0, IIF(marked, 1, -1))",
			"i.issue_no",
			"i.name",
			"s.name, i.series_vol, i.type, i.issue_no, i.published",
			"i.series_vol",
			"i.published",
			"i.date_added",
			"i.filename",
			"i.filesize"
		};

        public string[] OrdersDESC = new string[9]
		{
			"IIF(missing, 0, IIF(marked, 1, -1)) DESC",
			"i.issue_no DESC",
			"i.name DESC",
			"s.name DESC, i.series_vol, i.type, i.issue_no, i.published",
			"i.series_vol DESC",
			"i.published DESC",
			"i.date_added DESC",
			"i.filename DESC",
			"i.filesize DESC"
		};

        public string OrderSQL = "s.name, i.series_vol, i.type, i.issue_no, i.published";

        public int OrderCol = 3;
        public bool Shift;
        public int SeriesMode = -1;
        public StatusWindowController StatusWindow;
        public int StatusLogo;
        public int StatusImport;
        public int StatusReprocess;
        private int currentReprocess = 1;
        private ArrayList ReIssues;
        private ArrayList ErrList = new ArrayList();
        internal ToolStripMenuItem HelpUpdatesMenu;
        private int CurrentImport;

        public static MainForm Root
        {
            get
            {
                return pointer;
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Library", 0);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.colSeries = new System.Windows.Forms.ColumnHeader();
            this.BrowseMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.BrowseInfoMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.BrowsePublisherMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.BrowseGroupMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.BrowseMergeMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.BrowseClearMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.SourceSplit = new System.Windows.Forms.SplitContainer();
            this.SourceList = new System.Windows.Forms.ListView();
            this.colSource = new System.Windows.Forms.ColumnHeader();
            this.SourceIcons = new System.Windows.Forms.ImageList(this.components);
            this.CoverPreview = new System.Windows.Forms.PictureBox();
            this.SourceMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SourceOpenMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.SourceRenameMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.SourceClearMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ContentSplit = new System.Windows.Forms.SplitContainer();
            this.vsSeries = new System.Windows.Forms.VScrollBar();
            this.SeriesList = new System.Windows.Forms.ListView();
            this.vsGroups = new System.Windows.Forms.VScrollBar();
            this.vsPublishers = new System.Windows.Forms.VScrollBar();
            this.GroupList = new System.Windows.Forms.ListView();
            this.colGroups = new System.Windows.Forms.ColumnHeader();
            this.PublisherList = new System.Windows.Forms.ListView();
            this.colPublishers = new System.Windows.Forms.ColumnHeader();
            this.IssueList = new System.Windows.Forms.ListView();
            this.StatCol = new System.Windows.Forms.ColumnHeader("(none)");
            this.IssueCol = new System.Windows.Forms.ColumnHeader();
            this.NameCol = new System.Windows.Forms.ColumnHeader("(none)");
            this.SeriesCol = new System.Windows.Forms.ColumnHeader();
            this.VolCol = new System.Windows.Forms.ColumnHeader();
            this.PubCol = new System.Windows.Forms.ColumnHeader();
            this.AddedCol = new System.Windows.Forms.ColumnHeader();
            this.FileNameCol = new System.Windows.Forms.ColumnHeader();
            this.FileSizeCol = new System.Windows.Forms.ColumnHeader();
            this.IssueMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.IssueInfoMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.IssueEditMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.IssueShowFileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteFromLibrary = new System.Windows.Forms.ToolStripMenuItem();
            this.IssueClearMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.IssueReadlistMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.IssueCovers = new System.Windows.Forms.ImageList(this.components);
            this.StatIcons = new System.Windows.Forms.ImageList(this.components);
            this.SearchSeriesMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.SearchHeaderMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.SearchPlotMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.SearchGroupMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.MainSplit = new System.Windows.Forms.SplitContainer();
            this.EditUnmarkMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.EditSelectAllMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.EditMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.EditCopyMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.EditMarkMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.EditUndoRenameMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.EditReprocessMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.EditCoverMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.EditBrowserMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.EditSourceMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.EditPrefMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.chkSearch = new System.Windows.Forms.CheckBox();
            this.ToolsReprocessMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolsConvertMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolsConvertCBRMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolsConvertCBZMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolsConvertPDFMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolsDuplicatesMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolsMissingMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.autoTagToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.PublisherLogos = new System.Windows.Forms.ImageList(this.components);
            this.statStrip = new System.Windows.Forms.StatusStrip();
            this.lblStats = new System.Windows.Forms.ToolStripStatusLabel();
            this.ViewModeButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.ViewModeList = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewModeCovers = new System.Windows.Forms.ToolStripMenuItem();
            this.lblInfo = new System.Windows.Forms.Label();
            this.ToolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.FileExitMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.FileAddFolderMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.FileAddFilesMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpAboutMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.SearchMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SearchPublisherMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.SearchFilenameMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.SeriesLimitedMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.SeriesOneshotsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.Progress = new System.Windows.Forms.ProgressBar();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.SeriesNormalMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.FileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.FileNewReadlistMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.SeriesMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SeriesAllMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.Importer = new System.ComponentModel.BackgroundWorker();
            this.Reprocessor = new System.ComponentModel.BackgroundWorker();
            this.StatusStop = new System.Windows.Forms.PictureBox();
            this.StatusSwitch = new System.Windows.Forms.PictureBox();
            this.HelpUpdatesMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.BrowseMenu.SuspendLayout();
            this.SourceSplit.Panel1.SuspendLayout();
            this.SourceSplit.Panel2.SuspendLayout();
            this.SourceSplit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CoverPreview)).BeginInit();
            this.SourceMenu.SuspendLayout();
            this.ContentSplit.Panel1.SuspendLayout();
            this.ContentSplit.Panel2.SuspendLayout();
            this.ContentSplit.SuspendLayout();
            this.IssueMenu.SuspendLayout();
            this.MainSplit.Panel1.SuspendLayout();
            this.MainSplit.Panel2.SuspendLayout();
            this.MainSplit.SuspendLayout();
            this.statStrip.SuspendLayout();
            this.SearchMenu.SuspendLayout();
            this.MainMenu.SuspendLayout();
            this.SeriesMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StatusStop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StatusSwitch)).BeginInit();
            this.SuspendLayout();
            // 
            // colSeries
            // 
            this.colSeries.Text = "Name";
            this.colSeries.Width = 160;
            // 
            // BrowseMenu
            // 
            this.BrowseMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BrowseInfoMenu,
            this.BrowsePublisherMenu,
            this.BrowseGroupMenu,
            this.BrowseMergeMenu,
            this.BrowseClearMenu});
            this.BrowseMenu.Name = "BrowseMenu";
            this.BrowseMenu.Size = new System.Drawing.Size(128, 114);
            // 
            // BrowseInfoMenu
            // 
            this.BrowseInfoMenu.Name = "BrowseInfoMenu";
            this.BrowseInfoMenu.Size = new System.Drawing.Size(127, 22);
            this.BrowseInfoMenu.Text = "Get Info";
            this.BrowseInfoMenu.Click += new System.EventHandler(this.BrowseInfoMenu_Click);
            // 
            // BrowsePublisherMenu
            // 
            this.BrowsePublisherMenu.Name = "BrowsePublisherMenu";
            this.BrowsePublisherMenu.Size = new System.Drawing.Size(127, 22);
            this.BrowsePublisherMenu.Text = "Publisher";
            // 
            // BrowseGroupMenu
            // 
            this.BrowseGroupMenu.Name = "BrowseGroupMenu";
            this.BrowseGroupMenu.Size = new System.Drawing.Size(127, 22);
            this.BrowseGroupMenu.Text = "Group";
            // 
            // BrowseMergeMenu
            // 
            this.BrowseMergeMenu.Name = "BrowseMergeMenu";
            this.BrowseMergeMenu.Size = new System.Drawing.Size(127, 22);
            this.BrowseMergeMenu.Text = "Merge Into";
            // 
            // BrowseClearMenu
            // 
            this.BrowseClearMenu.Name = "BrowseClearMenu";
            this.BrowseClearMenu.Size = new System.Drawing.Size(127, 22);
            this.BrowseClearMenu.Text = "Delete";
            this.BrowseClearMenu.Click += new System.EventHandler(this.BrowseClearMenu_Click);
            // 
            // SourceSplit
            // 
            this.SourceSplit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SourceSplit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SourceSplit.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.SourceSplit.IsSplitterFixed = true;
            this.SourceSplit.Location = new System.Drawing.Point(0, 0);
            this.SourceSplit.Margin = new System.Windows.Forms.Padding(0);
            this.SourceSplit.Name = "SourceSplit";
            this.SourceSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // SourceSplit.Panel1
            // 
            this.SourceSplit.Panel1.Controls.Add(this.SourceList);
            // 
            // SourceSplit.Panel2
            // 
            this.SourceSplit.Panel2.AllowDrop = true;
            this.SourceSplit.Panel2.Controls.Add(this.CoverPreview);
            this.SourceSplit.Size = new System.Drawing.Size(125, 424);
            this.SourceSplit.SplitterDistance = 268;
            this.SourceSplit.TabIndex = 24;
            // 
            // SourceList
            // 
            this.SourceList.AllowDrop = true;
            this.SourceList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SourceList.BackColor = System.Drawing.Color.WhiteSmoke;
            this.SourceList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SourceList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colSource});
            this.SourceList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SourceList.FullRowSelect = true;
            this.SourceList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.SourceList.HideSelection = false;
            this.SourceList.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.SourceList.Location = new System.Drawing.Point(8, 18);
            this.SourceList.MultiSelect = false;
            this.SourceList.Name = "SourceList";
            this.SourceList.Size = new System.Drawing.Size(102, 241);
            this.SourceList.SmallImageList = this.SourceIcons;
            this.SourceList.TabIndex = 20;
            this.SourceList.UseCompatibleStateImageBehavior = false;
            this.SourceList.View = System.Windows.Forms.View.Details;
            this.SourceList.SelectedIndexChanged += new System.EventHandler(this.SourceList_SelectedIndexChanged);
            // 
            // colSource
            // 
            this.colSource.Width = 84;
            // 
            // SourceIcons
            // 
            this.SourceIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("SourceIcons.ImageStream")));
            this.SourceIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.SourceIcons.Images.SetKeyName(0, "Library");
            this.SourceIcons.Images.SetKeyName(1, "Readlist");
            // 
            // CoverPreview
            // 
            this.CoverPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.CoverPreview.Image = ((System.Drawing.Image)(resources.GetObject("CoverPreview.Image")));
            this.CoverPreview.Location = new System.Drawing.Point(8, 19);
            this.CoverPreview.Name = "CoverPreview";
            this.CoverPreview.Size = new System.Drawing.Size(102, 116);
            this.CoverPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.CoverPreview.TabIndex = 24;
            this.CoverPreview.TabStop = false;
            // 
            // SourceMenu
            // 
            this.SourceMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SourceOpenMenu,
            this.SourceRenameMenu,
            this.SourceClearMenu});
            this.SourceMenu.Name = "SourceMenu";
            this.SourceMenu.Size = new System.Drawing.Size(114, 70);
            // 
            // SourceOpenMenu
            // 
            this.SourceOpenMenu.Name = "SourceOpenMenu";
            this.SourceOpenMenu.Size = new System.Drawing.Size(113, 22);
            this.SourceOpenMenu.Text = "Open";
            this.SourceOpenMenu.Click += new System.EventHandler(this.SourceOpenMenu_Click);
            // 
            // SourceRenameMenu
            // 
            this.SourceRenameMenu.Name = "SourceRenameMenu";
            this.SourceRenameMenu.Size = new System.Drawing.Size(113, 22);
            this.SourceRenameMenu.Text = "Rename";
            this.SourceRenameMenu.Click += new System.EventHandler(this.SourceRenameMenu_Click);
            // 
            // SourceClearMenu
            // 
            this.SourceClearMenu.Name = "SourceClearMenu";
            this.SourceClearMenu.Size = new System.Drawing.Size(113, 22);
            this.SourceClearMenu.Text = "Delete";
            this.SourceClearMenu.Click += new System.EventHandler(this.SourceClearMenu_Click);
            // 
            // ContentSplit
            // 
            this.ContentSplit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ContentSplit.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.ContentSplit.Location = new System.Drawing.Point(0, 0);
            this.ContentSplit.Name = "ContentSplit";
            this.ContentSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // ContentSplit.Panel1
            // 
            this.ContentSplit.Panel1.Controls.Add(this.vsSeries);
            this.ContentSplit.Panel1.Controls.Add(this.SeriesList);
            this.ContentSplit.Panel1.Controls.Add(this.vsGroups);
            this.ContentSplit.Panel1.Controls.Add(this.vsPublishers);
            this.ContentSplit.Panel1.Controls.Add(this.GroupList);
            this.ContentSplit.Panel1.Controls.Add(this.PublisherList);
            // 
            // ContentSplit.Panel2
            // 
            this.ContentSplit.Panel2.Controls.Add(this.IssueList);
            this.ContentSplit.Size = new System.Drawing.Size(676, 424);
            this.ContentSplit.SplitterDistance = 141;
            this.ContentSplit.TabIndex = 0;
            this.ContentSplit.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.ContentSplit_SplitterMoved);
            // 
            // vsSeries
            // 
            this.vsSeries.Enabled = false;
            this.vsSeries.LargeChange = 1;
            this.vsSeries.Location = new System.Drawing.Point(579, 21);
            this.vsSeries.Maximum = 0;
            this.vsSeries.Name = "vsSeries";
            this.vsSeries.Size = new System.Drawing.Size(16, 93);
            this.vsSeries.TabIndex = 27;
            // 
            // SeriesList
            // 
            this.SeriesList.AutoArrange = false;
            this.SeriesList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colSeries});
            this.SeriesList.ContextMenuStrip = this.BrowseMenu;
            this.SeriesList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SeriesList.ForeColor = System.Drawing.Color.Black;
            this.SeriesList.FullRowSelect = true;
            this.SeriesList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.SeriesList.HideSelection = false;
            this.SeriesList.Location = new System.Drawing.Point(417, 19);
            this.SeriesList.Name = "SeriesList";
            this.SeriesList.Size = new System.Drawing.Size(180, 95);
            this.SeriesList.TabIndex = 26;
            this.SeriesList.UseCompatibleStateImageBehavior = false;
            this.SeriesList.View = System.Windows.Forms.View.Details;
            this.SeriesList.VirtualMode = true;
            this.SeriesList.SelectedIndexChanged += new System.EventHandler(this.SeriesList_SelectedIndexChanged);
            // 
            // vsGroups
            // 
            this.vsGroups.Enabled = false;
            this.vsGroups.LargeChange = 1;
            this.vsGroups.Location = new System.Drawing.Point(376, 21);
            this.vsGroups.Maximum = 0;
            this.vsGroups.Name = "vsGroups";
            this.vsGroups.Size = new System.Drawing.Size(16, 93);
            this.vsGroups.TabIndex = 25;
            // 
            // vsPublishers
            // 
            this.vsPublishers.Enabled = false;
            this.vsPublishers.LargeChange = 1;
            this.vsPublishers.Location = new System.Drawing.Point(175, 21);
            this.vsPublishers.Maximum = 0;
            this.vsPublishers.Name = "vsPublishers";
            this.vsPublishers.Size = new System.Drawing.Size(16, 93);
            this.vsPublishers.TabIndex = 19;
            // 
            // GroupList
            // 
            this.GroupList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colGroups});
            this.GroupList.ContextMenuStrip = this.BrowseMenu;
            this.GroupList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupList.FullRowSelect = true;
            this.GroupList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.GroupList.HideSelection = false;
            this.GroupList.Location = new System.Drawing.Point(212, 21);
            this.GroupList.Name = "GroupList";
            this.GroupList.ShowItemToolTips = true;
            this.GroupList.Size = new System.Drawing.Size(180, 93);
            this.GroupList.TabIndex = 24;
            this.GroupList.UseCompatibleStateImageBehavior = false;
            this.GroupList.View = System.Windows.Forms.View.Details;
            this.GroupList.VirtualMode = true;
            this.GroupList.SelectedIndexChanged += new System.EventHandler(this.GroupList_SelectedIndexChanged);
            // 
            // colGroups
            // 
            this.colGroups.Text = "Name";
            this.colGroups.Width = 160;
            // 
            // PublisherList
            // 
            this.PublisherList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colPublishers});
            this.PublisherList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PublisherList.FullRowSelect = true;
            this.PublisherList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.PublisherList.HideSelection = false;
            this.PublisherList.Location = new System.Drawing.Point(13, 21);
            this.PublisherList.Name = "PublisherList";
            this.PublisherList.Size = new System.Drawing.Size(180, 95);
            this.PublisherList.TabIndex = 18;
            this.PublisherList.UseCompatibleStateImageBehavior = false;
            this.PublisherList.View = System.Windows.Forms.View.Details;
            this.PublisherList.VirtualMode = true;
            this.PublisherList.SelectedIndexChanged += new System.EventHandler(this.PublisherList_SelectedIndexChanged);
            // 
            // colPublishers
            // 
            this.colPublishers.Text = "Name";
            this.colPublishers.Width = 160;
            // 
            // IssueList
            // 
            this.IssueList.Alignment = System.Windows.Forms.ListViewAlignment.Default;
            this.IssueList.AllowColumnReorder = true;
            this.IssueList.AllowDrop = true;
            this.IssueList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.IssueList.BackColor = System.Drawing.Color.White;
            this.IssueList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.StatCol,
            this.IssueCol,
            this.NameCol,
            this.SeriesCol,
            this.VolCol,
            this.PubCol,
            this.AddedCol,
            this.FileNameCol,
            this.FileSizeCol});
            this.IssueList.ContextMenuStrip = this.IssueMenu;
            this.IssueList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IssueList.ForeColor = System.Drawing.Color.Black;
            this.IssueList.FullRowSelect = true;
            this.IssueList.GridLines = true;
            this.IssueList.HideSelection = false;
            this.IssueList.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.IssueList.LargeImageList = this.IssueCovers;
            this.IssueList.Location = new System.Drawing.Point(13, 16);
            this.IssueList.Name = "IssueList";
            this.IssueList.ShowItemToolTips = true;
            this.IssueList.Size = new System.Drawing.Size(651, 247);
            this.IssueList.SmallImageList = this.StatIcons;
            this.IssueList.TabIndex = 19;
            this.IssueList.UseCompatibleStateImageBehavior = false;
            this.IssueList.View = System.Windows.Forms.View.Details;
            this.IssueList.VirtualMode = true;
            this.IssueList.SelectedIndexChanged += new System.EventHandler(this.IssueList_SelectedIndexChanged);
            // 
            // StatCol
            // 
            this.StatCol.Text = "";
            this.StatCol.Width = 20;
            // 
            // IssueCol
            // 
            this.IssueCol.Text = "#";
            this.IssueCol.Width = 73;
            // 
            // NameCol
            // 
            this.NameCol.Text = "Plot Title";
            this.NameCol.Width = 148;
            // 
            // SeriesCol
            // 
            this.SeriesCol.Text = "Series";
            this.SeriesCol.Width = 165;
            // 
            // VolCol
            // 
            this.VolCol.Text = "Vol.";
            this.VolCol.Width = 35;
            // 
            // PubCol
            // 
            this.PubCol.Text = "Published";
            this.PubCol.Width = 71;
            // 
            // AddedCol
            // 
            this.AddedCol.Text = "Date Added";
            this.AddedCol.Width = 80;
            // 
            // FileNameCol
            // 
            this.FileNameCol.Text = "File Name";
            this.FileNameCol.Width = 278;
            // 
            // FileSizeCol
            // 
            this.FileSizeCol.Text = "Size";
            // 
            // IssueMenu
            // 
            this.IssueMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.IssueInfoMenu,
            this.IssueEditMenu,
            this.IssueShowFileMenu,
            this.DeleteFromLibrary,
            this.IssueClearMenu,
            this.IssueReadlistMenu});
            this.IssueMenu.Name = "IssueMenu";
            this.IssueMenu.Size = new System.Drawing.Size(169, 136);
            // 
            // IssueInfoMenu
            // 
            this.IssueInfoMenu.Name = "IssueInfoMenu";
            this.IssueInfoMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.IssueInfoMenu.Size = new System.Drawing.Size(168, 22);
            this.IssueInfoMenu.Text = "Get Info";
            this.IssueInfoMenu.Click += new System.EventHandler(this.IssueInfoMenu_Click);
            // 
            // IssueEditMenu
            // 
            this.IssueEditMenu.Name = "IssueEditMenu";
            this.IssueEditMenu.Size = new System.Drawing.Size(168, 22);
            this.IssueEditMenu.Text = "Edit";
            this.IssueEditMenu.Click += new System.EventHandler(this.IssueEditMenu_Click);
            // 
            // IssueShowFileMenu
            // 
            this.IssueShowFileMenu.Name = "IssueShowFileMenu";
            this.IssueShowFileMenu.Size = new System.Drawing.Size(168, 22);
            this.IssueShowFileMenu.Text = "Show File";
            this.IssueShowFileMenu.Click += new System.EventHandler(this.IssueShowFileMenu_Click);
            // 
            // DeleteFromLibrary
            // 
            this.DeleteFromLibrary.Name = "DeleteFromLibrary";
            this.DeleteFromLibrary.Size = new System.Drawing.Size(168, 22);
            this.DeleteFromLibrary.Text = "Delete From Library";
            this.DeleteFromLibrary.Visible = false;
            this.DeleteFromLibrary.Click += new System.EventHandler(this.DeleteFromLibrary_Click);
            // 
            // IssueClearMenu
            // 
            this.IssueClearMenu.Name = "IssueClearMenu";
            this.IssueClearMenu.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.IssueClearMenu.Size = new System.Drawing.Size(168, 22);
            this.IssueClearMenu.Text = "Delete";
            this.IssueClearMenu.Click += new System.EventHandler(this.IssueClearMenu_Click);
            // 
            // IssueReadlistMenu
            // 
            this.IssueReadlistMenu.Name = "IssueReadlistMenu";
            this.IssueReadlistMenu.Size = new System.Drawing.Size(168, 22);
            this.IssueReadlistMenu.Text = "Add To Readlist";
            // 
            // IssueCovers
            // 
            this.IssueCovers.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("IssueCovers.ImageStream")));
            this.IssueCovers.TransparentColor = System.Drawing.Color.Transparent;
            this.IssueCovers.Images.SetKeyName(0, "default.jpg");
            // 
            // StatIcons
            // 
            this.StatIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("StatIcons.ImageStream")));
            this.StatIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.StatIcons.Images.SetKeyName(0, "missing.bmp");
            this.StatIcons.Images.SetKeyName(1, "marked.bmp");
            // 
            // SearchSeriesMenu
            // 
            this.SearchSeriesMenu.Checked = true;
            this.SearchSeriesMenu.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SearchSeriesMenu.Name = "SearchSeriesMenu";
            this.SearchSeriesMenu.Size = new System.Drawing.Size(117, 22);
            this.SearchSeriesMenu.Text = "Series";
            // 
            // SearchHeaderMenu
            // 
            this.SearchHeaderMenu.Enabled = false;
            this.SearchHeaderMenu.Name = "SearchHeaderMenu";
            this.SearchHeaderMenu.Size = new System.Drawing.Size(117, 22);
            this.SearchHeaderMenu.Text = "Criteria";
            // 
            // SearchPlotMenu
            // 
            this.SearchPlotMenu.Checked = true;
            this.SearchPlotMenu.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SearchPlotMenu.Name = "SearchPlotMenu";
            this.SearchPlotMenu.Size = new System.Drawing.Size(117, 22);
            this.SearchPlotMenu.Text = "Plot";
            // 
            // SearchGroupMenu
            // 
            this.SearchGroupMenu.Checked = true;
            this.SearchGroupMenu.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SearchGroupMenu.Name = "SearchGroupMenu";
            this.SearchGroupMenu.Size = new System.Drawing.Size(117, 22);
            this.SearchGroupMenu.Text = "Group";
            // 
            // MainSplit
            // 
            this.MainSplit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.MainSplit.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.MainSplit.Location = new System.Drawing.Point(0, 72);
            this.MainSplit.Name = "MainSplit";
            // 
            // MainSplit.Panel1
            // 
            this.MainSplit.Panel1.Controls.Add(this.SourceSplit);
            // 
            // MainSplit.Panel2
            // 
            this.MainSplit.Panel2.Controls.Add(this.ContentSplit);
            this.MainSplit.Size = new System.Drawing.Size(803, 424);
            this.MainSplit.SplitterDistance = 125;
            this.MainSplit.SplitterWidth = 2;
            this.MainSplit.TabIndex = 45;
            this.MainSplit.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.MainSplit_SplitterMoved);
            // 
            // EditUnmarkMenu
            // 
            this.EditUnmarkMenu.Name = "EditUnmarkMenu";
            this.EditUnmarkMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U)));
            this.EditUnmarkMenu.Size = new System.Drawing.Size(185, 22);
            this.EditUnmarkMenu.Text = "Mark Unread";
            this.EditUnmarkMenu.Visible = false;
            this.EditUnmarkMenu.Click += new System.EventHandler(this.EditUnmarkMenu_Click);
            // 
            // EditSelectAllMenu
            // 
            this.EditSelectAllMenu.Name = "EditSelectAllMenu";
            this.EditSelectAllMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.EditSelectAllMenu.Size = new System.Drawing.Size(185, 22);
            this.EditSelectAllMenu.Text = "Select All";
            this.EditSelectAllMenu.Click += new System.EventHandler(this.EditSelectAllMenu_Click);
            // 
            // ToolStripSeparator3
            // 
            this.ToolStripSeparator3.Name = "ToolStripSeparator3";
            this.ToolStripSeparator3.Size = new System.Drawing.Size(182, 6);
            // 
            // EditMenu
            // 
            this.EditMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EditCopyMenu,
            this.EditMarkMenu,
            this.EditUnmarkMenu,
            this.EditSelectAllMenu,
            this.ToolStripSeparator3,
            this.EditUndoRenameMenu,
            this.EditReprocessMenu,
            this.toolStripSeparator5,
            this.EditCoverMenu,
            this.EditBrowserMenu,
            this.EditSourceMenu,
            this.toolStripSeparator6,
            this.EditPrefMenu});
            this.EditMenu.Name = "EditMenu";
            this.EditMenu.Size = new System.Drawing.Size(37, 20);
            this.EditMenu.Text = "Edit";
            // 
            // EditCopyMenu
            // 
            this.EditCopyMenu.Name = "EditCopyMenu";
            this.EditCopyMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.EditCopyMenu.Size = new System.Drawing.Size(185, 22);
            this.EditCopyMenu.Text = "Copy";
            this.EditCopyMenu.Click += new System.EventHandler(this.EditCopyMenu_Click);
            // 
            // EditMarkMenu
            // 
            this.EditMarkMenu.Name = "EditMarkMenu";
            this.EditMarkMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.EditMarkMenu.Size = new System.Drawing.Size(185, 22);
            this.EditMarkMenu.Text = "Mark Read";
            this.EditMarkMenu.Visible = false;
            this.EditMarkMenu.Click += new System.EventHandler(this.EditMarkMenu_Click);
            // 
            // EditUndoRenameMenu
            // 
            this.EditUndoRenameMenu.Name = "EditUndoRenameMenu";
            this.EditUndoRenameMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.EditUndoRenameMenu.Size = new System.Drawing.Size(185, 22);
            this.EditUndoRenameMenu.Text = "Undo Rename";
            this.EditUndoRenameMenu.Visible = false;
            this.EditUndoRenameMenu.Click += new System.EventHandler(this.EditUndoRenameMenu_Click);
            // 
            // EditReprocessMenu
            // 
            this.EditReprocessMenu.Name = "EditReprocessMenu";
            this.EditReprocessMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.EditReprocessMenu.Size = new System.Drawing.Size(185, 22);
            this.EditReprocessMenu.Text = "Reprocess";
            this.EditReprocessMenu.Click += new System.EventHandler(this.EditReprocessMenu_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(182, 6);
            // 
            // EditCoverMenu
            // 
            this.EditCoverMenu.Name = "EditCoverMenu";
            this.EditCoverMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.A)));
            this.EditCoverMenu.Size = new System.Drawing.Size(185, 22);
            this.EditCoverMenu.Text = "Show Cover Art";
            this.EditCoverMenu.Click += new System.EventHandler(this.EditCoverMenu_Click);
            // 
            // EditBrowserMenu
            // 
            this.EditBrowserMenu.Name = "EditBrowserMenu";
            this.EditBrowserMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.B)));
            this.EditBrowserMenu.Size = new System.Drawing.Size(185, 22);
            this.EditBrowserMenu.Text = "Show Browser";
            this.EditBrowserMenu.Click += new System.EventHandler(this.EditBrowserMenu_Click);
            // 
            // EditSourceMenu
            // 
            this.EditSourceMenu.Name = "EditSourceMenu";
            this.EditSourceMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.S)));
            this.EditSourceMenu.Size = new System.Drawing.Size(185, 22);
            this.EditSourceMenu.Text = "Show Sources";
            this.EditSourceMenu.Click += new System.EventHandler(this.EditSourceMenu_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(182, 6);
            // 
            // EditPrefMenu
            // 
            this.EditPrefMenu.Name = "EditPrefMenu";
            this.EditPrefMenu.ShortcutKeyDisplayString = "";
            this.EditPrefMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.EditPrefMenu.Size = new System.Drawing.Size(185, 22);
            this.EditPrefMenu.Text = "Preferences";
            this.EditPrefMenu.Click += new System.EventHandler(this.EditPrefMenu_Click);
            // 
            // chkSearch
            // 
            this.chkSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkSearch.AutoSize = true;
            this.chkSearch.Location = new System.Drawing.Point(776, 30);
            this.chkSearch.Name = "chkSearch";
            this.chkSearch.Size = new System.Drawing.Size(15, 14);
            this.chkSearch.TabIndex = 46;
            this.chkSearch.UseVisualStyleBackColor = true;
            this.chkSearch.CheckedChanged += new System.EventHandler(this.chkSearch_CheckedChanged);
            // 
            // ToolsReprocessMenu
            // 
            this.ToolsReprocessMenu.Name = "ToolsReprocessMenu";
            this.ToolsReprocessMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.ToolsReprocessMenu.Size = new System.Drawing.Size(233, 22);
            this.ToolsReprocessMenu.Text = "Reprocess All";
            this.ToolsReprocessMenu.Click += new System.EventHandler(this.ToolsReprocessMenu_Click);
            // 
            // ToolsConvertMenu
            // 
            this.ToolsConvertMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolsConvertCBRMenu,
            this.ToolsConvertCBZMenu,
            this.ToolsConvertPDFMenu});
            this.ToolsConvertMenu.Enabled = false;
            this.ToolsConvertMenu.Name = "ToolsConvertMenu";
            this.ToolsConvertMenu.Size = new System.Drawing.Size(233, 22);
            this.ToolsConvertMenu.Text = "Convert To";
            this.ToolsConvertMenu.Visible = false;
            // 
            // ToolsConvertCBRMenu
            // 
            this.ToolsConvertCBRMenu.Name = "ToolsConvertCBRMenu";
            this.ToolsConvertCBRMenu.Size = new System.Drawing.Size(94, 22);
            this.ToolsConvertCBRMenu.Text = "CBR";
            // 
            // ToolsConvertCBZMenu
            // 
            this.ToolsConvertCBZMenu.Name = "ToolsConvertCBZMenu";
            this.ToolsConvertCBZMenu.Size = new System.Drawing.Size(94, 22);
            this.ToolsConvertCBZMenu.Text = "CBZ";
            // 
            // ToolsConvertPDFMenu
            // 
            this.ToolsConvertPDFMenu.Name = "ToolsConvertPDFMenu";
            this.ToolsConvertPDFMenu.Size = new System.Drawing.Size(94, 22);
            this.ToolsConvertPDFMenu.Text = "PDF";
            // 
            // ToolsMenu
            // 
            this.ToolsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolsDuplicatesMenu,
            this.ToolsMissingMenu,
            this.ToolsReprocessMenu,
            this.ToolsConvertMenu,
            this.autoTagToolStripMenuItem1});
            this.ToolsMenu.Name = "ToolsMenu";
            this.ToolsMenu.Size = new System.Drawing.Size(44, 20);
            this.ToolsMenu.Text = "Tools";
            // 
            // ToolsDuplicatesMenu
            // 
            this.ToolsDuplicatesMenu.Name = "ToolsDuplicatesMenu";
            this.ToolsDuplicatesMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.ToolsDuplicatesMenu.Size = new System.Drawing.Size(233, 22);
            this.ToolsDuplicatesMenu.Text = "Find Duplicates";
            this.ToolsDuplicatesMenu.Click += new System.EventHandler(this.ToolsDuplicatesMenu_Click);
            // 
            // ToolsMissingMenu
            // 
            this.ToolsMissingMenu.Name = "ToolsMissingMenu";
            this.ToolsMissingMenu.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.F)));
            this.ToolsMissingMenu.Size = new System.Drawing.Size(233, 22);
            this.ToolsMissingMenu.Text = "Find Missing Issues";
            this.ToolsMissingMenu.Click += new System.EventHandler(this.ToolsMissingMenu_Click);
            // 
            // autoTagToolStripMenuItem1
            // 
            this.autoTagToolStripMenuItem1.Name = "autoTagToolStripMenuItem1";
            this.autoTagToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.autoTagToolStripMenuItem1.Size = new System.Drawing.Size(233, 22);
            this.autoTagToolStripMenuItem1.Text = "Auto-Tag";
            this.autoTagToolStripMenuItem1.Click += new System.EventHandler(this.autoTagToolStripMenuItem1_Click);
            // 
            // PublisherLogos
            // 
            this.PublisherLogos.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("PublisherLogos.ImageStream")));
            this.PublisherLogos.TransparentColor = System.Drawing.Color.Transparent;
            this.PublisherLogos.Images.SetKeyName(0, "Carbon");
            this.PublisherLogos.Images.SetKeyName(1, "Amalgam");
            this.PublisherLogos.Images.SetKeyName(2, "Dark Horse");
            this.PublisherLogos.Images.SetKeyName(3, "DC");
            this.PublisherLogos.Images.SetKeyName(4, "IDW");
            this.PublisherLogos.Images.SetKeyName(5, "Marvel");
            this.PublisherLogos.Images.SetKeyName(6, "Top Cow");
            // 
            // statStrip
            // 
            this.statStrip.BackColor = System.Drawing.SystemColors.Control;
            this.statStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStats,
            this.ViewModeButton});
            this.statStrip.Location = new System.Drawing.Point(0, 497);
            this.statStrip.Name = "statStrip";
            this.statStrip.Size = new System.Drawing.Size(803, 22);
            this.statStrip.TabIndex = 47;
            // 
            // lblStats
            // 
            this.lblStats.BackColor = System.Drawing.SystemColors.Control;
            this.lblStats.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStats.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lblStats.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblStats.Name = "lblStats";
            this.lblStats.Size = new System.Drawing.Size(759, 17);
            this.lblStats.Spring = true;
            this.lblStats.Text = "[Stats]";
            // 
            // ViewModeButton
            // 
            this.ViewModeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ViewModeButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewModeList,
            this.ViewModeCovers});
            this.ViewModeButton.Image = ((System.Drawing.Image)(resources.GetObject("ViewModeButton.Image")));
            this.ViewModeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ViewModeButton.Name = "ViewModeButton";
            this.ViewModeButton.Size = new System.Drawing.Size(29, 20);
            this.ViewModeButton.Text = "ToolStripDropDownButton1";
            // 
            // ViewModeList
            // 
            this.ViewModeList.Checked = true;
            this.ViewModeList.CheckOnClick = true;
            this.ViewModeList.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ViewModeList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ViewModeList.Name = "ViewModeList";
            this.ViewModeList.Size = new System.Drawing.Size(121, 22);
            this.ViewModeList.Text = "Details";
            this.ViewModeList.Click += new System.EventHandler(this.ViewModeList_Click);
            // 
            // ViewModeCovers
            // 
            this.ViewModeCovers.CheckOnClick = true;
            this.ViewModeCovers.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ViewModeCovers.Name = "ViewModeCovers";
            this.ViewModeCovers.Size = new System.Drawing.Size(121, 22);
            this.ViewModeCovers.Text = "Cover Art";
            this.ViewModeCovers.Click += new System.EventHandler(this.ViewModeCovers_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblInfo.BackColor = System.Drawing.Color.White;
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfo.Location = new System.Drawing.Point(215, 9);
            this.lblInfo.MaximumSize = new System.Drawing.Size(365, 55);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.lblInfo.Size = new System.Drawing.Size(365, 55);
            this.lblInfo.TabIndex = 43;
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ToolStripSeparator4
            // 
            this.ToolStripSeparator4.Name = "ToolStripSeparator4";
            this.ToolStripSeparator4.Size = new System.Drawing.Size(244, 6);
            // 
            // FileExitMenu
            // 
            this.FileExitMenu.Name = "FileExitMenu";
            this.FileExitMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.FileExitMenu.Size = new System.Drawing.Size(247, 22);
            this.FileExitMenu.Text = "Exit";
            this.FileExitMenu.Click += new System.EventHandler(this.FileExitMenu_Click);
            // 
            // FileAddFolderMenu
            // 
            this.FileAddFolderMenu.Name = "FileAddFolderMenu";
            this.FileAddFolderMenu.ShortcutKeyDisplayString = "";
            this.FileAddFolderMenu.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.O)));
            this.FileAddFolderMenu.Size = new System.Drawing.Size(247, 22);
            this.FileAddFolderMenu.Text = "Add Folder To Library";
            this.FileAddFolderMenu.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.FileAddFolderMenu.Click += new System.EventHandler(this.FileAddFolderMenu_Click);
            // 
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(244, 6);
            // 
            // FileAddFilesMenu
            // 
            this.FileAddFilesMenu.Name = "FileAddFilesMenu";
            this.FileAddFilesMenu.ShortcutKeyDisplayString = "";
            this.FileAddFilesMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.FileAddFilesMenu.Size = new System.Drawing.Size(247, 22);
            this.FileAddFilesMenu.Text = "Add Files To Library";
            this.FileAddFilesMenu.Click += new System.EventHandler(this.FileAddFilesMenu_Click);
            // 
            // HelpMenu
            // 
            this.HelpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.HelpUpdatesMenu,
            this.HelpAboutMenu});
            this.HelpMenu.Name = "HelpMenu";
            this.HelpMenu.Size = new System.Drawing.Size(40, 20);
            this.HelpMenu.Text = "Help";
            // 
            // HelpAboutMenu
            // 
            this.HelpAboutMenu.Name = "HelpAboutMenu";
            this.HelpAboutMenu.Size = new System.Drawing.Size(172, 22);
            this.HelpAboutMenu.Text = "About Carbon Comic";
            this.HelpAboutMenu.Click += new System.EventHandler(this.HelpAboutMenu_Click);
            // 
            // SearchMenu
            // 
            this.SearchMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SearchHeaderMenu,
            this.SearchPlotMenu,
            this.SearchGroupMenu,
            this.SearchSeriesMenu,
            this.SearchPublisherMenu,
            this.SearchFilenameMenu});
            this.SearchMenu.Name = "SearchMenu";
            this.SearchMenu.Size = new System.Drawing.Size(118, 136);
            // 
            // SearchPublisherMenu
            // 
            this.SearchPublisherMenu.Checked = true;
            this.SearchPublisherMenu.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SearchPublisherMenu.Name = "SearchPublisherMenu";
            this.SearchPublisherMenu.Size = new System.Drawing.Size(117, 22);
            this.SearchPublisherMenu.Text = "Publisher";
            // 
            // SearchFilenameMenu
            // 
            this.SearchFilenameMenu.Checked = true;
            this.SearchFilenameMenu.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SearchFilenameMenu.Name = "SearchFilenameMenu";
            this.SearchFilenameMenu.Size = new System.Drawing.Size(117, 22);
            this.SearchFilenameMenu.Text = "Filename";
            // 
            // SeriesLimitedMenu
            // 
            this.SeriesLimitedMenu.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.SeriesLimitedMenu.Name = "SeriesLimitedMenu";
            this.SeriesLimitedMenu.Size = new System.Drawing.Size(139, 22);
            this.SeriesLimitedMenu.Text = "Limited Series";
            this.SeriesLimitedMenu.Click += new System.EventHandler(this.SeriesLimitedMenu_Click);
            // 
            // SeriesOneshotsMenu
            // 
            this.SeriesOneshotsMenu.Name = "SeriesOneshotsMenu";
            this.SeriesOneshotsMenu.Size = new System.Drawing.Size(139, 22);
            this.SeriesOneshotsMenu.Text = "One-Shots";
            this.SeriesOneshotsMenu.Click += new System.EventHandler(this.SeriesOneshotsMenu_Click);
            // 
            // Progress
            // 
            this.Progress.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Progress.BackColor = System.Drawing.SystemColors.Control;
            this.Progress.ForeColor = System.Drawing.Color.DimGray;
            this.Progress.Location = new System.Drawing.Point(237, 45);
            this.Progress.Name = "Progress";
            this.Progress.Size = new System.Drawing.Size(312, 12);
            this.Progress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.Progress.TabIndex = 44;
            this.Progress.Visible = false;
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.ContextMenuStrip = this.SearchMenu;
            this.txtSearch.Location = new System.Drawing.Point(636, 27);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(134, 20);
            this.txtSearch.TabIndex = 41;
            // 
            // SeriesNormalMenu
            // 
            this.SeriesNormalMenu.Name = "SeriesNormalMenu";
            this.SeriesNormalMenu.Size = new System.Drawing.Size(139, 22);
            this.SeriesNormalMenu.Text = "Series";
            this.SeriesNormalMenu.Click += new System.EventHandler(this.SeriesNormalMenu_Click);
            // 
            // FileMenu
            // 
            this.FileMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.FileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileNewReadlistMenu,
            this.ToolStripSeparator1,
            this.FileAddFilesMenu,
            this.FileAddFolderMenu,
            this.ToolStripSeparator4,
            this.FileExitMenu});
            this.FileMenu.Name = "FileMenu";
            this.FileMenu.Size = new System.Drawing.Size(35, 20);
            this.FileMenu.Text = "File";
            // 
            // FileNewReadlistMenu
            // 
            this.FileNewReadlistMenu.Name = "FileNewReadlistMenu";
            this.FileNewReadlistMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.FileNewReadlistMenu.Size = new System.Drawing.Size(247, 22);
            this.FileNewReadlistMenu.Text = "New Readlist";
            this.FileNewReadlistMenu.Click += new System.EventHandler(this.FileNewReadlistMenu_Click);
            // 
            // MainMenu
            // 
            this.MainMenu.BackColor = System.Drawing.Color.Transparent;
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenu,
            this.EditMenu,
            this.ToolsMenu,
            this.HelpMenu});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(803, 24);
            this.MainMenu.TabIndex = 42;
            this.MainMenu.Text = "MenuStrip1";
            // 
            // SeriesMenu
            // 
            this.SeriesMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SeriesAllMenu,
            this.SeriesNormalMenu,
            this.SeriesLimitedMenu,
            this.SeriesOneshotsMenu});
            this.SeriesMenu.Name = "SeriesMenu";
            this.SeriesMenu.Size = new System.Drawing.Size(140, 92);
            // 
            // SeriesAllMenu
            // 
            this.SeriesAllMenu.Checked = true;
            this.SeriesAllMenu.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SeriesAllMenu.Name = "SeriesAllMenu";
            this.SeriesAllMenu.Size = new System.Drawing.Size(139, 22);
            this.SeriesAllMenu.Text = "All";
            this.SeriesAllMenu.Click += new System.EventHandler(this.SeriesAllMenu_Click);
            // 
            // Importer
            // 
            this.Importer.WorkerReportsProgress = true;
            this.Importer.WorkerSupportsCancellation = true;
            this.Importer.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Importer_DoWork);
            // 
            // Reprocessor
            // 
            this.Reprocessor.WorkerReportsProgress = true;
            this.Reprocessor.WorkerSupportsCancellation = true;
            this.Reprocessor.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Reprocesser_DoWork);
            // 
            // StatusStop
            // 
            this.StatusStop.Image = ((System.Drawing.Image)(resources.GetObject("StatusStop.Image")));
            this.StatusStop.Location = new System.Drawing.Point(587, 32);
            this.StatusStop.Name = "StatusStop";
            this.StatusStop.Size = new System.Drawing.Size(13, 13);
            this.StatusStop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.StatusStop.TabIndex = 48;
            this.StatusStop.TabStop = false;
            this.StatusStop.Visible = false;
            // 
            // StatusSwitch
            // 
            this.StatusSwitch.Image = ((System.Drawing.Image)(resources.GetObject("StatusSwitch.Image")));
            this.StatusSwitch.Location = new System.Drawing.Point(202, 32);
            this.StatusSwitch.Name = "StatusSwitch";
            this.StatusSwitch.Size = new System.Drawing.Size(11, 11);
            this.StatusSwitch.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.StatusSwitch.TabIndex = 49;
            this.StatusSwitch.TabStop = false;
            this.StatusSwitch.Visible = false;
            this.StatusSwitch.Click += new System.EventHandler(this.StatusSwitch_Click);
            // 
            // HelpUpdatesMenu
            // 
            this.HelpUpdatesMenu.Enabled = false;
            this.HelpUpdatesMenu.Name = "HelpUpdatesMenu";
            this.HelpUpdatesMenu.Size = new System.Drawing.Size(172, 22);
            this.HelpUpdatesMenu.Text = "Check For Updates";
            this.HelpUpdatesMenu.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 519);
            this.Controls.Add(this.StatusStop);
            this.Controls.Add(this.MainSplit);
            this.Controls.Add(this.chkSearch);
            this.Controls.Add(this.statStrip);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.StatusSwitch);
            this.Controls.Add(this.Progress);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.MainMenu);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Carbon Comic";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.BrowseMenu.ResumeLayout(false);
            this.SourceSplit.Panel1.ResumeLayout(false);
            this.SourceSplit.Panel2.ResumeLayout(false);
            this.SourceSplit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CoverPreview)).EndInit();
            this.SourceMenu.ResumeLayout(false);
            this.ContentSplit.Panel1.ResumeLayout(false);
            this.ContentSplit.Panel2.ResumeLayout(false);
            this.ContentSplit.ResumeLayout(false);
            this.IssueMenu.ResumeLayout(false);
            this.MainSplit.Panel1.ResumeLayout(false);
            this.MainSplit.Panel2.ResumeLayout(false);
            this.MainSplit.ResumeLayout(false);
            this.statStrip.ResumeLayout(false);
            this.statStrip.PerformLayout();
            this.SearchMenu.ResumeLayout(false);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.SeriesMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.StatusStop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StatusSwitch)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public MainForm()
        {
            InitializeComponent();
            if (Root == null)
            {
                pointer = this;
            }
            base.Resize += MainForm_Resize;
            SourceList.DoubleClick += SourceList_DoubleClick;
            SourceList.MouseUp += SourceList_MouseUp;
            PublisherList.MouseUp += PublisherList_MouseUp;
            GroupList.MouseUp += GroupList_MouseUp;
            SeriesList.KeyDown += SeriesList_KeyDown;
            SeriesList.KeyUp += SeriesList_KeyUp;
            SeriesList.MouseUp += SeriesList_MouseUp;
            SeriesList.RetrieveVirtualItem += SeriesList_RetrieveVirtualItem;
            IssueList.ColumnClick += IssueList_ColumnClick;
            IssueList.RetrieveVirtualItem += IssueList_RetrieveVirtualItem;
            IssueList.MouseDoubleClick += IssueList_MouseDoubleClick;
            IssueList.MouseUp += IssueList_MouseUp;
            IssueList.MouseDown += IssueList_MouseDown;
            SearchMenu.ItemClicked += SearchMenu_ItemClicked;
            txtSearch.KeyDown += txtSearch_KeyDown;
            EditMenu.MouseDown += EditMenu_MouseDown;
            Importer.ProgressChanged += Importer_ProgressChanged;
            Importer.RunWorkerCompleted += Importer_RunWorkerCompleted;
            Reprocessor.RunWorkerCompleted += Reprocesser_RunWorkerCompleted;
            Reprocessor.ProgressChanged += Reprocesser_ProgressChanged;
            StatusStop.MouseDown += StatusStop_MouseDown;
            StatusStop.MouseUp += StatusStop_MouseUp;
            StatusSwitch.MouseDown += StatusSwitch_MouseDown;
            StatusSwitch.MouseUp += StatusSwitch_MouseUp;
            PublisherList.RetrieveVirtualItem += PublisherList_RetrieveVirtualItem;
            GroupList.RetrieveVirtualItem += GroupList_RetrieveVirtualItem;
            MainSplit.DoubleClick += MainSplit_DoubleClick;
            SourceSplit.DoubleClick += SourceSplit_DoubleClick;
            ContentSplit.DoubleClick += ContentSplit_DoubleClick;
            IssueList.DragEnter += IssueList_DragEnter;
            IssueList.ItemDrag += IssueList_ItemDrag;
            IssueList.DragDrop += IssueList_DragDrop;
            IssueList.DragOver += IssueList_DragOver;
        }

        private void IssueList_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (SourceList.SelectedIndices[0] != 0 && !chkSearch.Checked)
            {
                IssueList.DoDragDrop(IssueList.SelectedIndices, DragDropEffects.Move);
            }
        }

        private void IssueList_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void IssueList_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
            Point point = IssueList.PointToClient(new Point(e.X, e.Y));
            ListViewItem itemAt = IssueList.GetItemAt(point.X, point.Y);
            itemAt.EnsureVisible();
        }

        private void IssueList_DragDrop(object sender, DragEventArgs e)
        {
            Point point = IssueList.PointToClient(new Point(e.X, e.Y));
            int pos = IssueList.GetItemAt(point.X, point.Y).Index + 1;
            IssueCovers.Images.Clear();
            Readlist readlist = (Readlist)CC.Readlists[SourceList.SelectedIndices[0] - 1];
            ArrayList arrayList = new ArrayList();
            foreach (int selectedIndex in IssueList.SelectedIndices)
            {
                readlist.setPosition(selectedIndex, pos);
                arrayList.Add(readlist.Issues[selectedIndex]);
            }
            readlist.Issues.Sort();
            CC.Issues = readlist.Issues;
            for (int i = 0; i < readlist.Issues.Count; i++)
            {
                ReadlistIssue item = (ReadlistIssue)readlist.Issues[i];
                if (arrayList.Contains(item))
                {
                    IssueList.Items[i].Selected = true;
                }
                else
                {
                    IssueList.Items[i].Selected = false;
                }
            }
            IssueList.Refresh();
        }

        private void ContentSplit_DoubleClick(object sender, EventArgs e)
        {
            ContentSplit.Panel1Collapsed = true;
            Reposition();
        }

        private void SourceSplit_DoubleClick(object sender, EventArgs e)
        {
            SourceSplit.Panel2Collapsed = true;
            Reposition();
        }

        private void MainSplit_DoubleClick(object sender, EventArgs e)
        {
            MainSplit.Panel1Collapsed = true;
            Reposition();
        }

        private void GroupList_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            if (e.ItemIndex == 0)
            {
                e.Item = new ListViewItem("All (" + CC.Groups.Count + " Groups)");
            }
            else
            {
                ComicGroup comicGroup = (ComicGroup)CC.Groups[e.ItemIndex - 1];
                e.Item = new ListViewItem(comicGroup.Name.ToString());
            }
        }

        private void PublisherList_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            if (e.ItemIndex == 0)
            {
                e.Item = new ListViewItem("All (" + CC.Publishers.Count + " Publishers)");
            }
            else
            {
                ComicPublisher comicPublisher = (ComicPublisher)CC.Publishers[e.ItemIndex - 1];
                e.Item = new ListViewItem(comicPublisher.Name.ToString());
            }
        }

        private void StatusSwitch_MouseUp(object sender, MouseEventArgs e)
        {
            StatusSwitch.Image = Resources.StatusSwitch;
        }

        private void StatusSwitch_MouseDown(object sender, MouseEventArgs e)
        {
            StatusSwitch.Image = Resources.StatusSwitchPressed;
        }

        private void StatusStop_MouseUp(object sender, MouseEventArgs e)
        {
            StatusStop.Image = Resources.StatusStop;
        }

        private void StatusStop_MouseDown(object sender, MouseEventArgs e)
        {
            StatusStop.Image = Resources.StatusStopPressed;
        }

        private void Reprocesser_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            currentReprocess = 0;
            StatusWindow.Remove(StatusReprocess);
            FormInputMode(true);
            IssueList.Refresh();
        }

        private void Reprocesser_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (currentReprocess < ReIssues.Count)
            {
                currentReprocess++;
                ComicIssue comicIssue = (ComicIssue)ReIssues[currentReprocess - 1];
                StatusWindow.setDetails(StatusReprocess, Path.GetFileName(comicIssue.FileName) + " (" + currentReprocess + " of " + ReIssues.Count + ")");
                StatusWindow.setProgress(StatusReprocess, e.ProgressPercentage);
                StatusWindow.Refresh(StatusReprocess);
            }
        }

        private void Importer_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            CC.SQL = new SQL(Path.Combine(Application.StartupPath, Settings.Default.DatabaseFile));
            if (ErrList.Count > 0)
            {
                DialogResult dialogResult = MessageBox.Show("There were errors importing some files.  Would you like to save the error log?", "Errors", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    DialogResult dialogResult2 = DialogResult.None;
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Title = "Save List";
                    saveFileDialog.FileName = "Import Errors.txt";
                    saveFileDialog.Filter = "Text Files|(*.txt)";
                    do
                    {
                        dialogResult2 = saveFileDialog.ShowDialog(Root);
                    }
                    while (!(saveFileDialog.FileName != ""));
                    if (dialogResult2 != DialogResult.Cancel)
                    {
                        File.WriteAllLines(saveFileDialog.FileName, CC.StringList(ErrList));
                    }
                }
                ErrList.Clear();
                RefreshSources();
            }
            CurrentImport = 0;
            StatusWindow.Remove(StatusImport);
            FormInputMode(true);
            RefreshPublishers();
        }

        private void Importer_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (CurrentImport < CC.ImportFiles.Count)
            {
                StatusWindow.setDetails(StatusImport, Path.GetFileName((string)CC.ImportFiles[CurrentImport++]) + " (" + CurrentImport + " of " + CC.ImportFiles.Count + ")");
                StatusWindow.setProgress(StatusImport, e.ProgressPercentage);
                StatusWindow.Refresh(StatusImport);
            }
        }

        private void SourceList_MouseUp(object sender, MouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Right) & !SourceList.SelectedIndices.Contains(0))
            {
                SourceList.ContextMenuStrip = SourceMenu;
            }
            else
            {
                SourceList.ContextMenuStrip = null;
            }
            if (SourceList.SelectedIndices.Count == 0)
            {
                SourceList.Items[0].Selected = true;
                SourceList.Select();
                SourceList.ContextMenuStrip = null;
            }
        }

        private void SourceList_DoubleClick(object sender, EventArgs e)
        {
            if (!SourceList.SelectedIndices.Contains(0))
            {
                NewWindow(SourceList.SelectedIndices[0]);
            }
        }

        private void EditMenu_MouseDown(object sender, MouseEventArgs e)
        {
            if (SourceSplit.Panel2Collapsed)
            {
                EditCoverMenu.Text = "Show Cover Art";
            }
            else
            {
                EditCoverMenu.Text = "Hide Cover Art";
            }
            if (ContentSplit.Panel1Collapsed)
            {
                EditBrowserMenu.Text = "Show Browser";
            }
            else
            {
                EditBrowserMenu.Text = "Hide Browser";
            }
            if (MainSplit.Panel1Collapsed)
            {
                EditSourceMenu.Text = "Show Sources";
            }
            else
            {
                EditSourceMenu.Text = "Hide Sources";
            }
        }

        public void UpdateInfo(string size)
        {
            lblStats.Text = CC.Issues.Count + " Issues, " + size;
        }

        private void Reposition()
        {
            PublisherList.Location = new Point(0, 0);
            PublisherList.Size = new Size(ContentSplit.Panel1.Width / 3, ContentSplit.Panel1.Height);
            colPublishers.Width = PublisherList.Width - 5 - vsPublishers.Width;
            vsPublishers.Location = new Point(PublisherList.Left + PublisherList.Width - vsPublishers.Width - 2, PublisherList.Top + 2);
            vsPublishers.Size = new Size(vsPublishers.Width, PublisherList.Height - 4);
            if (PublisherList.Items.Count != 0)
            {
                if (PublisherList.GetItemRect(PublisherList.Items.Count - 1).Y > PublisherList.Height - 4)
                {
                    vsPublishers.Visible = false;
                }
                if (((PublisherList.TopItem.Index > 0) & (PublisherList.TopItem.Bounds.Y == 0)) | (PublisherList.GetItemRect(PublisherList.Items.Count - 1).Y < PublisherList.Height - 4))
                {
                    vsPublishers.Visible = true;
                }
            }
            GroupList.Location = new Point(PublisherList.Left + PublisherList.Width, 0);
            GroupList.Size = new Size(ContentSplit.Panel1.Width / 3, ContentSplit.Panel1.Height);
            colGroups.Width = GroupList.Width - 5 - vsGroups.Width;
            vsGroups.Location = new Point(GroupList.Left + GroupList.Width - vsGroups.Width - 2, GroupList.Top + 2);
            vsGroups.Size = new Size(vsGroups.Width, GroupList.Height - 4);
            if (GroupList.Items.Count != 0)
            {
                if (GroupList.GetItemRect(GroupList.Items.Count - 1).Y > GroupList.Height - 4)
                {
                    vsGroups.Visible = false;
                }
                else
                {
                    vsGroups.Visible = true;
                }
            }
            SeriesList.Location = new Point(GroupList.Left + GroupList.Width, 0);
            SeriesList.Size = new Size(ContentSplit.Panel1.Width / 3, ContentSplit.Panel1.Height);
            colSeries.Width = SeriesList.Width - 5 - vsSeries.Width;
            vsSeries.Location = new Point(SeriesList.Left + SeriesList.Width - vsSeries.Width - 2, SeriesList.Top + 2);
            vsSeries.Size = new Size(vsSeries.Width, SeriesList.Height - 4);
            if (SeriesList.Items.Count != 0)
            {
                if (SeriesList.GetItemRect(SeriesList.Items.Count - 1).Y > SeriesList.Height - 4)
                {
                    vsSeries.Visible = false;
                }
                else
                {
                    vsSeries.Visible = true;
                }
            }
            VScrollBar vScrollBar = new VScrollBar();
            if (SourceList.GetItemRect(SourceList.Items.Count - 1).Y > SourceList.Height)
            {
                colSource.Width = SourceList.Width - vScrollBar.Width - 2;
            }
            else
            {
                colSource.Width = SourceList.Width - 2;
            }
            IssueList.Location = new Point(0, 0);
            IssueList.Size = new Size(ContentSplit.Panel2.Width, ContentSplit.Panel2.Height + 2);
            SourceList.Location = new Point(-1, -1);
            SourceList.Size = new Size(SourceSplit.Panel1.Width, SourceSplit.Panel1.Height);
            CoverPreview.Location = new Point(0, 0);
            CoverPreview.Size = new Size(SourceSplit.Panel2.Width, SourceSplit.Panel2.Height);
            lblInfo.Left = base.Width / 2 - lblInfo.Width / 2;
            Progress.Left = base.Width / 2 - Progress.Width / 2;
            StatusStop.Left = lblInfo.Left + lblInfo.Width - 25;
            StatusSwitch.Left = lblInfo.Left + 10;
        }

        public void RefreshPublishers(string ToSelect)
        {
            Query sqlRow = null;
            ComicPublisher comicPublisher = null;
            int index = 0;
            PublisherList.Items.Clear();
            BrowsePublishers.Clear();
            sqlRow = CC.SQL.ExecQuery("SELECT id,name FROM publishers ORDER BY name");
            int num = 0;
            while (sqlRow.NextResult())
            {
                num++;
                comicPublisher = new ComicPublisher(ref sqlRow);
                BrowsePublishers.Add(comicPublisher);
                if (Convert.ToString(comicPublisher.Name) == ToSelect)
                {
                    index = num;
                }
            }
            sqlRow.Close();
            CC.Publishers = BrowsePublishers;
            PublisherList.VirtualListSize = CC.Publishers.Count + 1;
            PublisherList.SelectedIndices.Clear();
            PublisherList.Items[index].Selected = true;
            PublisherList.Select();
        }

        public void RefreshPublishers()
        {
            RefreshPublishers("");
        }

        private void PublisherList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PublisherList.SelectedIndices.Count > 0)
            {
                if (PublisherList.SelectedIndices[0] != 0)
                {
                    ComicPublisher comicPublisher = (ComicPublisher)CC.Publishers[PublisherList.SelectedIndices[0] - 1];
                    if (PublisherLogos.Images.ContainsKey(comicPublisher.Name))
                    {
                        StatusWindow.setIcon(StatusLogo, PublisherLogos.Images[comicPublisher.Name]);
                    }
                    else
                    {
                        StatusWindow.setIcon(StatusLogo, PublisherLogos.Images["Carbon"]);
                    }
                }
                else
                {
                    StatusWindow.setIcon(StatusLogo, PublisherLogos.Images["Carbon"]);
                }
                StatusWindow.Refresh(StatusLogo);
                if (PublisherList.SelectedIndices.Contains(0) & (PublisherList.SelectedIndices.Count > 1))
                {
                    PublisherList.Items[0].Selected = false;
                    PublisherList.Select();
                }
                if (PublisherList.SelectedIndices.Count > 0)
                {
                    RefreshGroups();
                    Reposition();
                }
                PublisherList.Focus();
            }
        }

        private void PublisherList_MouseUp(object sender, MouseEventArgs e)
        {
            Image image = null;
            ToolStripMenuItem toolStripMenuItem = null;
            ListView.SelectedIndexCollection selectedIndexCollection = null;
            ComicPublisher comicPublisher = null;
            if ((e.Button == MouseButtons.Right) & !PublisherList.SelectedIndices.Contains(0))
            {
                PublisherList.ContextMenuStrip = BrowseMenu;
                BrowseGroupMenu.Visible = false;
                BrowsePublisherMenu.Visible = false;
                BrowseMergeMenu.Visible = (PublisherList.SelectedIndices.Count > 1);
                if (PublisherList.SelectedIndices.Count > 1)
                {
                    BrowseMergeMenu.DropDownItems.Clear();
                    selectedIndexCollection = PublisherList.SelectedIndices;
                    foreach (int item in selectedIndexCollection)
                    {
                        comicPublisher = (ComicPublisher)CC.Publishers[item - 1];
                        toolStripMenuItem = new ToolStripMenuItem(PublisherList.Items[item].Text, image, BrowseMergeMenuSelect);
                        toolStripMenuItem.Tag = comicPublisher.ID;
                        BrowseMergeMenu.DropDownItems.Add(toolStripMenuItem);
                    }
                    toolStripMenuItem = new ToolStripMenuItem("New..", image, BrowseMergeMenuSelect);
                    toolStripMenuItem.Tag = 0;
                    BrowseMergeMenu.DropDownItems.Add(toolStripMenuItem);
                }
            }
            else
            {
                PublisherList.ContextMenuStrip = null;
            }
            if (PublisherList.SelectedIndices.Count == 0)
            {
                PublisherList.ContextMenuStrip = null;
                PublisherList.Items[0].Selected = true;
                PublisherList.Select();
            }
        }

        public void RefreshGroups(string ToSelect)
        {
            int index = 0;
            int num = 0;
            ArrayList arrayList = new ArrayList();
            string str = "";
            Query sqlRow = null;
            if (!PublisherList.SelectedIndices.Contains(0))
            {
                string[] array = new string[PublisherList.SelectedIndices.Count];
                foreach (int selectedIndex in PublisherList.SelectedIndices)
                {
                    ComicPublisher comicPublisher = (ComicPublisher)CC.Publishers[selectedIndex - 1];
                    array[num++] = Convert.ToString(comicPublisher.ID);
                }
                arrayList.Add("g.id IN (SELECT group_id FROM series WHERE pub_id IN (" + string.Join(",", array) + "))");
            }
            if (arrayList.Count > 0)
            {
                str = " AND " + string.Join(" AND ", CC.StringList(arrayList));
            }
            GroupList.Items.Clear();
            BrowseGroups.Clear();
            sqlRow = CC.SQL.ExecQuery("SELECT g.name, g.id, g.pub_id, p.name FROM groups g, publishers p WHERE g.pub_id=p.id " + str + " ORDER BY g.name");
            int num4 = 0;
            while (sqlRow.NextResult())
            {
                num4++;
                ComicGroup value = new ComicGroup(ref sqlRow);
                BrowseGroups.Add(value);
                if (ToSelect == Convert.ToString(sqlRow.hash["g.name"]))
                {
                    index = num4;
                }
            }
            sqlRow.Close();
            CC.Groups = BrowseGroups;
            GroupList.VirtualListSize = CC.Groups.Count + 1;
            GroupList.SelectedIndices.Clear();
            GroupList.Items[index].Selected = true;
            GroupList.Select();
        }

        public void RefreshGroups()
        {
            RefreshGroups("");
        }

        private void GroupList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GroupList.SelectedIndices.Contains(0) & (GroupList.SelectedIndices.Count > 1))
            {
                GroupList.Items[0].Selected = false;
                GroupList.Select();
            }
            if (GroupList.SelectedIndices.Count > 0)
            {
                RefreshSeries("");
                Reposition();
            }
            GroupList.Focus();
        }

        private void GroupList_MouseUp(object sender, MouseEventArgs e)
        {
            Image image = null;
            ToolStripMenuItem toolStripMenuItem = null;
            int num = 0;
            ComicPublisher comicPublisher = null;
            ComicGroup comicGroup = null;
            if (e.Button == MouseButtons.Right && !GroupList.SelectedIndices.Contains(0))
            {
                GroupList.ContextMenuStrip = BrowseMenu;
                BrowseGroupMenu.Visible = false;
                BrowsePublisherMenu.Visible = true;
                BrowseMergeMenu.DropDownItems.Clear();
                BrowsePublisherMenu.DropDownItems.Clear();
                BrowseMergeMenu.Visible = (GroupList.SelectedIndices.Count > 1);
                if (GroupList.SelectedIndices.Count > 1)
                {
                    foreach (int selectedIndex in GroupList.SelectedIndices)
                    {
                        comicGroup = (ComicGroup)CC.Groups[selectedIndex - 1];
                        toolStripMenuItem = new ToolStripMenuItem(GroupList.Items[selectedIndex].Text, image, BrowseMergeMenuSelect);
                        toolStripMenuItem.Tag = comicGroup.ID;
                        BrowseMergeMenu.DropDownItems.Add(toolStripMenuItem);
                    }
                    toolStripMenuItem = new ToolStripMenuItem("New..", image, BrowseMergeMenuSelect);
                    toolStripMenuItem.Tag = 0;
                    BrowseMergeMenu.DropDownItems.Add(toolStripMenuItem);
                }
                for (int i = 1; i < PublisherList.Items.Count; i++)
                {
                    comicPublisher = (ComicPublisher)CC.Publishers[i - 1];
                    toolStripMenuItem = new ToolStripMenuItem(PublisherList.Items[i].Text, image, BrowsePublisherMenuSelect);
                    toolStripMenuItem.Tag = comicPublisher.ID;
                    num = 0;
                    foreach (int selectedIndex2 in GroupList.SelectedIndices)
                    {
                        comicGroup = (ComicGroup)CC.Groups[selectedIndex2 - 1];
                        if (comicGroup.PublisherID == comicPublisher.ID)
                        {
                            num++;
                        }
                    }
                    if (num == GroupList.SelectedIndices.Count)
                    {
                        toolStripMenuItem.Checked = true;
                    }
                    BrowsePublisherMenu.DropDownItems.Add(toolStripMenuItem);
                }
                toolStripMenuItem = new ToolStripMenuItem("New..", image, BrowsePublisherMenuSelect);
                toolStripMenuItem.Tag = 0;
                BrowsePublisherMenu.DropDownItems.Add(toolStripMenuItem);
            }
            else
            {
                GroupList.ContextMenuStrip = null;
            }
            if (GroupList.SelectedIndices.Count == 0)
            {
                GroupList.ContextMenuStrip = null;
                GroupList.Items[0].Selected = true;
                GroupList.Select();
            }
        }

        public void RefreshSeries(int Type)
        {
            SeriesMode = Type;
            RefreshSeries("");
        }

        public void RefreshSeries(string ToSelect)
        {
            ComicSeries comicSeries = null;
            int index = 0;
            int num = 0;
            ArrayList arrayList = new ArrayList();
            Query sqlRow = null;
            if (!PublisherList.SelectedIndices.Contains(0))
            {
                string[] array = new string[PublisherList.SelectedIndices.Count];
                num = 0;
                foreach (int selectedIndex in PublisherList.SelectedIndices)
                {
                    ComicPublisher comicPublisher = (ComicPublisher)CC.Publishers[selectedIndex - 1];
                    array[num++] = Convert.ToString(comicPublisher.ID);
                }
                arrayList.Add("s.pub_id IN (" + string.Join(",", array) + ")");
            }
            if (!GroupList.SelectedIndices.Contains(0))
            {
                string[] array = new string[GroupList.SelectedIndices.Count];
                num = 0;
                foreach (int selectedIndex2 in GroupList.SelectedIndices)
                {
                    ComicGroup comicGroup = (ComicGroup)CC.Groups[selectedIndex2 - 1];
                    array[num++] = Convert.ToString(comicGroup.ID);
                }
                arrayList.Add("s.group_id IN (" + string.Join(",", array) + ")");
            }
            if (SeriesMode != -1)
            {
                arrayList.Add("s.type=" + Convert.ToString(SeriesMode));
            }
            arrayList.Add("s.pub_id=p.id AND s.group_id=g.id");
            sqlRow = CC.SQL.ExecQuery("SELECT s.*,p.name,g.name FROM series s, groups g, publishers p WHERE " + string.Join(" AND ", CC.StringList(arrayList)) + " ORDER BY s.name");
            BrowseSeries.Clear();
            int num6 = 0;
            while (sqlRow.NextResult())
            {
                num6++;
                comicSeries = new ComicSeries(ref sqlRow);
                BrowseSeries.Add(comicSeries);
                if (Convert.ToString(sqlRow.hash["s.name"]) == ToSelect)
                {
                    index = num6;
                }
            }
            sqlRow.Close();
            CC.Series = BrowseSeries;
            try
            {
                SeriesList.VirtualListSize = num6 + 1;
            }
            catch
            {
            }
            SeriesList.SelectedIndices.Clear();
            SeriesList.Items[index].Selected = true;
            SeriesList.Items[index].EnsureVisible();
            SeriesList.Select();
            SeriesList.Refresh();
        }

        public void RefreshSeries()
        {
            RefreshSeries("");
        }

        public void SelectSeries()
        {
            if (SeriesList.SelectedIndices.Count > 0)
            {
                RefreshIssues();
                if (CC.Issues.Count > 0)
                {
                    if (SeriesList.SelectedIndices.Contains(0))
                    {
                        CoverPreview.Image = Resources.DefaultCover;
                    }
                    else
                    {
                        ComicIssue comicIssue = (ComicIssue)CC.Issues[0];
                        CoverPreview.Image = CC.GetIssueCover(comicIssue.ID);
                    }
                    IssueList.Items[0].EnsureVisible();
                }
            }
            SeriesList.Focus();
        }

        private void SeriesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SeriesList.SelectedIndices.Contains(0) & (SeriesList.SelectedIndices.Count > 1))
            {
                SeriesList.Items[0].Selected = false;
                SeriesList.Select();
            }
            SelectSeries();
        }

        private void SeriesList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Shift)
            {
                Shift = true;
            }
        }

        private void SeriesList_KeyUp(object sender, KeyEventArgs e)
        {
            Shift = false;
        }

        private void SeriesList_MouseUp(object sender, MouseEventArgs e)
        {
            Image image = null;
            ToolStripMenuItem toolStripMenuItem = null;
            ComicSeries comicSeries = null;
            if (SeriesList.Items.Count > 0)
            {
                if ((e.Button == MouseButtons.Left) & Shift)
                {
                    SelectSeries();
                }
                if ((e.Button == MouseButtons.Right) & !SeriesList.SelectedIndices.Contains(0))
                {
                    SeriesList.ContextMenuStrip = BrowseMenu;
                    SeriesList.ContextMenuStrip = BrowseMenu;
                    BrowseGroupMenu.Visible = true;
                    BrowsePublisherMenu.Visible = false;
                    BrowseMergeMenu.DropDownItems.Clear();
                    BrowseGroupMenu.DropDownItems.Clear();
                    BrowseMergeMenu.Visible = (SeriesList.SelectedIndices.Count > 1);
                    if (SeriesList.SelectedIndices.Count > 1)
                    {
                        toolStripMenuItem = new ToolStripMenuItem("New..", image, BrowseMergeMenuSelect);
                        toolStripMenuItem.Tag = 0;
                        BrowseMergeMenu.DropDownItems.Add(toolStripMenuItem);
                        foreach (int selectedIndex in SeriesList.SelectedIndices)
                        {
                            comicSeries = (ComicSeries)CC.Series[selectedIndex - 1];
                            toolStripMenuItem = new ToolStripMenuItem(SeriesList.Items[selectedIndex].Text, image, BrowseMergeMenuSelect);
                            toolStripMenuItem.Tag = comicSeries.ID;
                            BrowseMergeMenu.DropDownItems.Add(toolStripMenuItem);
                        }
                    }
                    toolStripMenuItem = new ToolStripMenuItem("New..", image, BrowseGroupMenuSelect);
                    toolStripMenuItem.Tag = -1;
                    BrowseGroupMenu.DropDownItems.Add(toolStripMenuItem);
                    int num2 = 0;
                    Query query = CC.SQL.ExecQuery("SELECT * FROM groups ORDER BY name");
                    while (query.NextResult())
                    {
                        toolStripMenuItem = new ToolStripMenuItem((string)query.hash["name"], image, BrowseGroupMenuSelect);
                        toolStripMenuItem.Tag = (int)query.hash["id"];
                        num2 = 0;
                        foreach (int selectedIndex2 in SeriesList.SelectedIndices)
                        {
                            comicSeries = (ComicSeries)CC.Series[selectedIndex2 - 1];
                            if (comicSeries.GroupID == (int)query.hash["id"])
                            {
                                num2++;
                            }
                        }
                        if (num2 == SeriesList.SelectedIndices.Count)
                        {
                            toolStripMenuItem.Checked = true;
                        }
                        BrowseGroupMenu.DropDownItems.Add(toolStripMenuItem);
                    }
                    query.Close();
                }
                else
                {
                    SeriesList.ContextMenuStrip = SeriesMenu;
                }
                if (SeriesList.SelectedIndices.Count == 0)
                {
                    SeriesList.ContextMenuStrip = null;
                    SeriesList.Items[0].Selected = true;
                    SeriesList.Select();
                }
            }
        }

        private void SeriesList_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            if (e.ItemIndex == 0)
            {
                string text;
                switch (SeriesMode)
                {
                    case 1:
                        text = "Limited Series";
                        break;
                    case 2:
                        text = "One-Shots";
                        break;
                    default:
                        text = "Series";
                        break;
                }
                e.Item = new ListViewItem("All (" + CC.Series.Count + " " + text + ")");
            }
            else
            {
                ComicSeries comicSeries = null;
                comicSeries = (ComicSeries)CC.Series[e.ItemIndex - 1];
                e.Item = new ListViewItem(comicSeries.Name.ToString());
            }
        }

        public void RefreshIssues()
        {
            Query sqlRow = null;
            int num = 0;
            ArrayList arrayList = new ArrayList();
            ArrayList arrayList2 = new ArrayList();
            ComicIssue comicIssue = null;
            Cursor = Cursors.AppStarting;
            foreach (int selectedIndex in IssueList.SelectedIndices)
            {
                comicIssue = (ComicIssue)CC.Issues[selectedIndex];
                arrayList.Add(comicIssue.ID);
            }
            IssueList.SelectedIndices.Clear();
            if (CurrentReadlist != null)
            {
                CurrentReadlist.GetIssues();
            }
            else if ((SeriesList.SelectedIndices.Count > 0) & (PublisherList.SelectedIndices.Count > 0) & (GroupList.SelectedIndices.Count > 0))
            {
                string[] array = null;
                ArrayList arrayList3 = new ArrayList();
                if (!PublisherList.SelectedIndices.Contains(0))
                {
                    ComicPublisher comicPublisher = null;
                    array = new string[PublisherList.SelectedIndices.Count];
                    num = 0;
                    foreach (int selectedIndex2 in PublisherList.SelectedIndices)
                    {
                        comicPublisher = (ComicPublisher)CC.Publishers[selectedIndex2 - 1];
                        array[num++] = Convert.ToString(comicPublisher.ID);
                    }
                    arrayList3.Add("i.series_id IN (SELECT id FROM series WHERE pub_id IN (" + string.Join(",", array) + "))");
                }
                if (!GroupList.SelectedIndices.Contains(0))
                {
                    ComicGroup comicGroup = null;
                    array = new string[GroupList.SelectedIndices.Count];
                    num = 0;
                    foreach (int selectedIndex3 in GroupList.SelectedIndices)
                    {
                        comicGroup = (ComicGroup)CC.Groups[selectedIndex3 - 1];
                        array[num++] = Convert.ToString(comicGroup.ID);
                    }
                    arrayList3.Add("i.series_id IN (SELECT id FROM series WHERE group_id IN (" + string.Join(",", array) + "))");
                }
                if (!SeriesList.SelectedIndices.Contains(0))
                {
                    ComicSeries comicSeries = null;
                    array = new string[SeriesList.SelectedIndices.Count];
                    num = 0;
                    foreach (int selectedIndex4 in SeriesList.SelectedIndices)
                    {
                        comicSeries = (ComicSeries)CC.Series[selectedIndex4 - 1];
                        array[num++] = Convert.ToString(comicSeries.ID);
                    }
                    arrayList3.Add("i.series_id IN (" + string.Join(",", array) + ")");
                }
                if (chkSearch.Checked && SearchIssues.Count > 0)
                {
                    array = new string[SearchIssues.Count];
                    num = 0;
                    foreach (ComicIssue searchIssue in SearchIssues)
                    {
                        array[num++] = Convert.ToString(searchIssue.ID);
                    }
                    arrayList3.Add("i.id IN (" + string.Join(",", array) + ")");
                }
                if (SeriesMode != -1)
                {
                    arrayList3.Add("s.type=" + Convert.ToString(SeriesMode));
                }
                string str = (arrayList3.Count > 0) ? string.Join(" AND ", CC.StringList(arrayList3)) : "1";
                string query = "SELECT i.*, s.*, p.name, g.name FROM issues i, series s, groups g, publishers p WHERE i.series_id=s.id AND s.pub_id=p.id AND s.group_id=g.id AND " + str + " ORDER BY " + OrderSQL;
                sqlRow = CC.SQL.ExecQuery(query);
            }
            BrowseIssues.Clear();
            IssueCovers.Images.Clear();
            if (CurrentReadlist != null)
            {
                BrowseIssues = (ArrayList)CurrentReadlist.Issues.Clone();
                CC.Issues = BrowseIssues;
                try
                {
                    IssueList.VirtualListSize = CC.Issues.Count;
                }
                catch
                {
                }
            }
            else if (sqlRow != null)
            {
                num = 0;
                double num9 = 0.0;
                while (sqlRow.NextResult())
                {
                    comicIssue = new ComicIssue(ref sqlRow);
                    BrowseIssues.Add(comicIssue);
                    if (arrayList.Contains(comicIssue.ID))
                    {
                        arrayList2.Add(num);
                    }
                    num++;
                    num9 += (double)comicIssue.FileSize;
                }
                sqlRow.Close();
                CC.Issues = BrowseIssues;
                try
                {
                    IssueList.VirtualListSize = num;
                }
                catch
                {
                }
                BrowseTotalSize = CC.ByteToString(num9);
                if (arrayList2.Count > 0)
                {
                    foreach (int item in arrayList2)
                    {
                        IssueList.Items[item].Selected = true;
                    }
                    IssueList.Items[(int)arrayList2[0]].EnsureVisible();
                }
                IssueList.Select();
            }
            IssueList.Refresh();
            UpdateInfo(BrowseTotalSize);
            Cursor = Cursors.Default;
        }

        private void IssueList_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (OrderMode == "ASC")
            {
                OrderSQL = OrdersASC[e.Column];
                OrderMode = "DESC";
            }
            else if (OrderMode == "DESC")
            {
                OrderSQL = OrdersDESC[e.Column];
                OrderMode = "ASC";
            }
            OrderCol = e.Column;
            RefreshIssues();
        }

        private void IssueList_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            string text = null;
            int num = 0;
            ComicIssue comicIssue = (ComicIssue)CC.Issues[e.ItemIndex];
            if (IssueList.View == View.LargeIcon)
            {
                CC.GetIssueCover(comicIssue.ID, this);
                num = IssueCovers.Images.IndexOfKey(Convert.ToString(comicIssue.ID));
            }
            else
            {
                num = comicIssue.Status;
            }
            if (comicIssue.SeriesType == 2)
            {
                text = "One-shot";
            }
            else if (comicIssue.Type == 0)
            {
                text = "Issue #" + comicIssue.Number.ToString();
            }
            else if (comicIssue.Type == 1)
            {
                text = "Annual " + comicIssue.Number.ToString();
            }
            else if (comicIssue.Type == 2)
            {
                text = "Special";
            }
            string text2 = (comicIssue.Published.Year != 1) ? comicIssue.Published.ToString("MMM yyyy") : "";
            string text3 = (comicIssue.SeriesVolume < 1) ? "N/A" : Convert.ToString(comicIssue.SeriesVolume);
            string[] items = new string[9]
			{
				"",
				text,
				comicIssue.Name,
				comicIssue.SeriesName,
				text3,
				text2,
				comicIssue.DateAdded.ToString("M/d/yyyy h:mm tt"),
				comicIssue.FileName,
				CC.ByteToString((double)comicIssue.FileSize)
			};
            Color backColor = (e.ItemIndex % 2 != 0) ? Color.White : Color.WhiteSmoke;
            e.Item = new ListViewItem(items, num, Color.Black, backColor, new Font("Lucidia Grande", 8f));
            e.Item.ToolTipText = text + Environment.NewLine + comicIssue.SeriesName;
        }

        private void IssueList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (IssueList.SelectedIndices.Count == 1)
            {
                ComicIssue comicIssue = null;
                comicIssue = (ComicIssue)CC.Issues[IssueList.SelectedIndices[0]];
                if (!CC.ExecuteFile(comicIssue.FileName.ToString()))
                {
                    comicIssue.CheckMissing();
                }
                if (comicIssue.SaveChanges())
                {
                    IssueList.Refresh();
                }
            }
        }

        private void BrowsePublisherMenuSelect(object sender, EventArgs e)
        {
            string text = null;
            ComicGroup comicGroup = null;
            ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)sender;
            if ((int)toolStripMenuItem.Tag == 0)
            {
                text = CC.InputBox("Enter the name of the new publisher:", "Create New Publisher", "");
                if (text == "")
                {
                    return;
                }
                toolStripMenuItem.Tag = ComicPublisher.GetID(text);
            }
            foreach (int selectedIndex in GroupList.SelectedIndices)
            {
                comicGroup = (ComicGroup)CC.Groups[selectedIndex - 1];
                comicGroup.PublisherID = (int)toolStripMenuItem.Tag;
                comicGroup.SaveChanges();
            }
            RefreshPublishers();
        }

        private void BrowseMergeMenuSelect(object sender, EventArgs e)
        {
            string text = null;
            ComicPublisher comicPublisher = null;
            ComicGroup comicGroup = null;
            ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)sender;
            if (PublisherList.Focused)
            {
                if ((int)toolStripMenuItem.Tag == 0)
                {
                    text = CC.InputBox("Enter the name of the new publisher:", "Merge Into New Publisher", "");
                    if (text == "")
                    {
                        return;
                    }
                    toolStripMenuItem.Text = text;
                    toolStripMenuItem.Tag = ComicPublisher.GetID(text);
                }
                foreach (int selectedIndex in PublisherList.SelectedIndices)
                {
                    comicPublisher = (ComicPublisher)CC.Publishers[selectedIndex - 1];
                    if (comicPublisher.ID != (int)toolStripMenuItem.Tag)
                    {
                        CC.SQL.ExecQuery("UPDATE series SET pub_id=" + toolStripMenuItem.Tag + " WHERE pub_id=" + comicPublisher.ID);
                        CC.SQL.ExecQuery("UPDATE groups SET pub_id=" + toolStripMenuItem.Tag + " WHERE pub_id=" + comicPublisher.ID);
                        CC.SQL.ExecQuery("DELETE FROM publishers WHERE id=" + comicPublisher.ID);
                    }
                }
                RefreshPublishers(toolStripMenuItem.Text);
            }
            else if (GroupList.Focused)
            {
                if ((int)toolStripMenuItem.Tag == 0)
                {
                    text = CC.InputBox("Enter the name of the new group:", "Merge Into New Group", "");
                    if (text == "")
                    {
                        return;
                    }
                    comicGroup = (ComicGroup)CC.Groups[GroupList.SelectedIndices[0] - 1];
                    toolStripMenuItem.Text = text;
                    toolStripMenuItem.Tag = ComicGroup.GetID(text, comicGroup.PublisherID);
                }
                foreach (int selectedIndex2 in GroupList.SelectedIndices)
                {
                    comicGroup = (ComicGroup)CC.Groups[selectedIndex2 - 1];
                    if ((double)comicGroup.ID != Convert.ToDouble(toolStripMenuItem.Tag))
                    {
                        CC.SQL.ExecQuery("UPDATE series SET group_id=" + toolStripMenuItem.Tag + " WHERE group_id=" + comicGroup.ID);
                        CC.SQL.ExecQuery("DELETE FROM groups WHERE id=" + comicGroup.ID);
                    }
                }
                RefreshGroups(toolStripMenuItem.Text);
            }
            else if (SeriesList.Focused)
            {
                ComicSeries comicSeries = null;
                ComicSeries comicSeries2 = null;
                if ((int)toolStripMenuItem.Tag == 0)
                {
                    comicSeries2 = (ComicSeries)CC.Series[SeriesList.SelectedIndices[0] - 1];
                    text = CC.InputBox("Enter the name of the new series:", "Merge Into New Series", comicSeries2.Name, MergeNewValidation);
                    if (text == "")
                    {
                        return;
                    }
                    comicSeries = new ComicSeries(ComicSeries.GetID(text, comicSeries2.GroupID, comicSeries2.PublisherID));
                }
                if (comicSeries == null)
                {
                    comicSeries = new ComicSeries((int)toolStripMenuItem.Tag);
                }
                ArrayList arrayList = new ArrayList();
                foreach (int selectedIndex3 in SeriesList.SelectedIndices)
                {
                    comicSeries2 = (ComicSeries)CC.Series[selectedIndex3 - 1];
                    if (comicSeries2.ID != comicSeries.ID)
                    {
                        arrayList.Add(comicSeries2);
                        CC.SQL.ExecQuery("UPDATE issues SET series_id=" + comicSeries.ID + " WHERE series_id=" + comicSeries2.ID);
                        CC.SQL.ExecQuery("DELETE FROM series WHERE id=" + comicSeries2.ID);
                    }
                }
                foreach (ComicSeries item in arrayList)
                {
                    SeriesList.VirtualListSize--;
                    CC.Series.Remove(item);
                }
                if (text != null)
                {
                    bool flag = true;
                    for (int i = 0; i < CC.Series.Count; i++)
                    {
                        comicSeries2 = (ComicSeries)CC.Series[i];
                        if (comicSeries2.ID == comicSeries.ID)
                        {
                            flag = false;
                        }
                    }
                    if (flag)
                    {
                        CC.Series.Add(comicSeries);
                        SeriesList.VirtualListSize++;
                        CC.Series.Sort();
                    }
                }
                SeriesList.SelectedIndices.Clear();
                int num4 = 0;
                while (true)
                {
                    if (num4 >= CC.Series.Count)
                    {
                        return;
                    }
                    comicSeries2 = (ComicSeries)CC.Series[num4];
                    if (comicSeries2.Name == comicSeries.Name)
                    {
                        break;
                    }
                    num4++;
                }
                SeriesList.Items[num4 + 1].Selected = true;
                SeriesList.Items[num4 + 1].EnsureVisible();
            }
        }

        private void MergeNewValidation(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            InputBox inputBox = (InputBox)button.Parent;
            inputBox.DialogResult = DialogResult.None;
            string text = inputBox.Controls["txtInput"].Text;
            try
            {
                ComicGroup comicGroup = new ComicGroup();
                comicGroup.Name = text;
                inputBox.DialogResult = DialogResult.OK;
                inputBox.Close();
            }
            catch (LogException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void BrowseGroupMenuSelect(object sender, EventArgs e)
        {
            ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)sender;
            ComicGroup comicGroup = null;
            if ((int)toolStripMenuItem.Tag == -1)
            {
                ComicSeries comicSeries = (ComicSeries)CC.Series[SeriesList.SelectedIndices[0] - 1];
                NewGroupForm newGroupForm = new NewGroupForm();
                newGroupForm.Controls["cboPublisher"].Text = comicSeries.PublisherName;
                newGroupForm.ShowDialog();
                if (newGroupForm.DialogResult == DialogResult.Cancel)
                {
                    return;
                }
                string text = newGroupForm.Controls["txtName"].Text;
                int iD = ComicPublisher.GetID(newGroupForm.Controls["cboPublisher"].Text);
                int iD2 = ComicGroup.GetID(text, iD, newGroupForm.Controls["cboPublisher"].Text);
                comicGroup = new ComicGroup(iD2);
            }
            Cursor = Cursors.WaitCursor;
            if (comicGroup == null)
            {
                comicGroup = new ComicGroup((int)toolStripMenuItem.Tag);
            }
            ArrayList arrayList = new ArrayList();
            foreach (int selectedIndex in SeriesList.SelectedIndices)
            {
                arrayList.Add((ComicSeries)CC.Series[selectedIndex - 1]);
            }
            comicGroup.AdoptSeries(arrayList);
            if (!GroupList.SelectedIndices.Contains(0))
            {
                ArrayList arrayList2 = new ArrayList();
                foreach (int selectedIndex2 in SeriesList.SelectedIndices)
                {
                    arrayList2.Add(CC.Series[selectedIndex2 - 1]);
                    SeriesList.VirtualListSize--;
                }
                SeriesList.SelectedIndices.Clear();
                foreach (ComicSeries item in arrayList2)
                {
                    CC.Series.Remove(item);
                }
            }
            if ((int)toolStripMenuItem.Tag == -1)
            {
                CC.Groups.Add(comicGroup);
                CC.Groups.Sort();
                GroupList.VirtualListSize++;
                ArrayList arrayList3 = new ArrayList();
                foreach (int selectedIndex3 in GroupList.SelectedIndices)
                {
                    arrayList3.Add(CC.Groups[selectedIndex3 - 1]);
                }
                GroupList.SelectedIndices.Clear();
                for (int i = 0; i < CC.Groups.Count; i++)
                {
                    GroupList.Items[i + 1].Selected = arrayList3.Contains(CC.Groups[i]);
                }
                GroupList.Select();
            }
            Cursor = Cursors.Default;
        }

        private void IssueReadlistMenuSelect(object sender, EventArgs e)
        {
            ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)sender;
            Readlist readlist = (Readlist)CC.Readlists[(int)toolStripMenuItem.Tag];
            ComicIssue comicIssue = null;
            Cursor = Cursors.AppStarting;
            foreach (int selectedIndex in IssueList.SelectedIndices)
            {
                comicIssue = (ComicIssue)CC.Issues[selectedIndex];
                readlist.AddIssue(comicIssue.ID);
            }
            Cursor = Cursors.Default;
        }

        private void IssueInfoMenu_Click(object sender, EventArgs e)
        {
            bool flag = false;
            foreach (int selectedIndex in IssueList.SelectedIndices)
            {
                ComicIssue comicIssue = (ComicIssue)CC.Issues[selectedIndex];
                comicIssue.CheckMissing();
                if (comicIssue.SaveChanges())
                {
                    flag = true;
                }
            }
            if (flag)
            {
                IssueList.Refresh();
            }
            if (IssueList.SelectedIndices.Count == 1)
            {
                IssueForm issueForm = new IssueForm();
                issueForm.ShowDialog(this);
            }
            else if (IssueList.SelectedIndices.Count > 1)
            {
                IssueMultiForm issueMultiForm = new IssueMultiForm();
                issueMultiForm.ShowDialog(this);
            }
            if (IssueList.Items.Count == 0)
            {
                SeriesList.Items[0].Selected = true;
                SeriesList.Select();
            }
        }

        private void BrowseClearMenu_Click(object sender, EventArgs e)
        {
            bool files = false;
            if (PublisherList.Focused)
            {
                DialogResult dialogResult;
                if (Settings.Default.OrganizeMethod != 0)
                {
                    dialogResult = MessageBox.Show("Do you want to delete the files associated with this publisher?", "Delete Publisher", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        files = true;
                    }
                }
                else
                {
                    dialogResult = MessageBox.Show("Are you sure you want to delete this publisher?", "Delete Publisher", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                }
                if (dialogResult != DialogResult.Cancel)
                {
                    ArrayList arrayList = new ArrayList();
                    foreach (int selectedIndex in PublisherList.SelectedIndices)
                    {
                        ComicPublisher comicPublisher = (ComicPublisher)CC.Publishers[selectedIndex - 1];
                        comicPublisher.Delete(files);
                        arrayList.Add(comicPublisher);
                    }
                    PublisherList.VirtualListSize -= arrayList.Count;
                    foreach (ComicPublisher item in arrayList)
                    {
                        CC.Publishers.Remove(item);
                    }
                    PublisherList.Items[0].Selected = true;
                }
            }
            else if (GroupList.Focused)
            {
                DialogResult dialogResult;
                if (Settings.Default.OrganizeMethod != 0)
                {
                    dialogResult = MessageBox.Show("Do you want to delete the files associated with this group?", "Delete Group", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        files = true;
                    }
                }
                else
                {
                    dialogResult = MessageBox.Show("Are you sure you want to delete this group?", "Delete Group", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                }
                if (dialogResult != DialogResult.Cancel)
                {
                    foreach (int selectedIndex2 in GroupList.SelectedIndices)
                    {
                        ComicGroup comicGroup = (ComicGroup)CC.Groups[selectedIndex2 - 1];
                        comicGroup.Delete(files);
                    }
                    RefreshGroups();
                }
            }
            else if (SeriesList.Focused)
            {
                DialogResult dialogResult;
                if (Settings.Default.OrganizeMethod != 0)
                {
                    dialogResult = MessageBox.Show("Do you want to delete the files associated with this series?", "Delete Series", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        files = true;
                    }
                }
                else
                {
                    dialogResult = MessageBox.Show("Are you sure you want to delete these series?", "Delete Series", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                }
                if (dialogResult != DialogResult.Cancel)
                {
                    Cursor = Cursors.WaitCursor;
                    ArrayList arrayList2 = new ArrayList();
                    foreach (int selectedIndex3 in SeriesList.SelectedIndices)
                    {
                        ComicSeries comicSeries = (ComicSeries)CC.Series[selectedIndex3 - 1];
                        comicSeries.Delete(files);
                        arrayList2.Add(selectedIndex3);
                    }
                    SeriesList.SelectedIndices.Clear();
                    SeriesList.VirtualListSize -= arrayList2.Count;
                    foreach (int item2 in arrayList2)
                    {
                        CC.Series.RemoveAt(item2 - 1);
                    }
                    Cursor = Cursors.Default;
                }
            }
        }

        private void FileAddFolderMenu_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowNewFolderButton = false;
            folderBrowserDialog.SelectedPath = "";
            folderBrowserDialog.Description = "Choose a folder to import into Carbon Comic.";
            DialogResult dialogResult = folderBrowserDialog.ShowDialog(this);
            if (dialogResult != DialogResult.Cancel)
            {
                Cursor = Cursors.AppStarting;
                CC.ImportFiles.Clear();
                StatusItems items = default(StatusItems);
                items.Task = "Finding Comics..";
                items.ShowProgress = true;
                items.ProgressStyle = ProgressBarStyle.Marquee;
                StatusImport = StatusWindow.Add(items);
                FindComics(folderBrowserDialog.SelectedPath);
                Cursor = Cursors.Default;
                if (CC.ImportFiles.Count != 0)
                {
                    StatusWindow.setTask(StatusImport, "Waiting...");
                    StatusWindow.setDetails(StatusImport, "Found " + CC.ImportFiles.Count + " Comics");
                    StatusWindow.Refresh(StatusImport);
                    ImportForm importForm = new ImportForm();
                    importForm.ShowDialog(this);
                    if (importForm.DialogResult == DialogResult.OK)
                    {
                        DoImportFiles();
                    }
                    else
                    {
                        StatusWindow.Remove(StatusImport);
                    }
                }
                else
                {
                    MessageBox.Show("No comics found!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    StatusWindow.Remove(StatusImport);
                }
            }
        }

        private void FileAddFilesMenu_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Path.Combine(Application.StartupPath, Settings.Default.LibraryDir);
            openFileDialog.Filter = "Comic Archives (CBR, CBZ)|*.cbr;*cbz|PDF Files|*.pdf|All Files|*";
            openFileDialog.Multiselect = true;
            openFileDialog.ShowDialog(this);
            if (openFileDialog.FileNames.Length > 0)
            {
                CC.ImportFiles.Clear();
                StatusItems items = default(StatusItems);
                items.ShowProgress = true;
                items.Task = "Finding Comics...";
                items.ProgressStyle = ProgressBarStyle.Marquee;
                StatusImport = StatusWindow.Add(items);
                int num = 0;
                string[] fileNames = openFileDialog.FileNames;
                foreach (string text in fileNames)
                {
                    CC.ImportFiles.Add(text);
                    FileInfo fileInfo = new FileInfo(text);
                    StatusWindow.setDetails(StatusImport, fileInfo.Name + " (" + ++num + " of " + openFileDialog.FileNames.Length + ")");
                    StatusWindow.Refresh(StatusImport);
                    try
                    {
                        Application.DoEvents();
                    }
                    catch
                    {
                    }
                }
                if (CC.ImportFiles.Count != 0)
                {
                    StatusWindow.setTask(StatusImport, "Waiting...");
                    StatusWindow.setDetails(StatusImport, "Found " + CC.ImportFiles.Count + " Comics");
                    StatusWindow.Refresh(StatusImport);
                    ImportForm importForm = new ImportForm();
                    importForm.ShowDialog(this);
                    if (importForm.DialogResult != DialogResult.Cancel)
                    {
                        DoImportFiles();
                    }
                    else
                    {
                        StatusWindow.Remove(StatusImport);
                    }
                }
                else
                {
                    MessageBox.Show("No new comics found!");
                }
            }
        }

        private void FileNewReadlistMenu_Click(object sender, EventArgs e)
        {
            string text = null;
            text = CC.InputBox("Name of the readlist:", "New Readlist", "");
            if (text != "")
            {
                Readlist.GetID(text, true);
                RefreshSources();
            }
        }

        private void EditReprocessMenu_Click(object sender, EventArgs e)
        {
            ReIssues = new ArrayList();
            foreach (int selectedIndex in IssueList.SelectedIndices)
            {
                ReIssues.Add(CC.Issues[selectedIndex]);
            }
            if (ReIssues.Count > 0)
            {
                DoReprocess();
            }
        }

        private void IssueList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IssueList.SelectedIndices.Count > 0)
            {
          
                ComicIssue comicIssue = (ComicIssue)CC.Issues[IssueList.SelectedIndices[0]];
                if (!SourceSplit.Panel2Collapsed)
                {
                    CoverPreview.Image = CC.GetIssueCover(comicIssue.ID);
                }
            }

        }

        private void NewWindow(int SourceIndex)
        {
        }

        public int FindComics(string path)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            DirectoryInfo[] array = null;
            FileInfo[] array2 = null;
            int num = 0;
            Application.DoEvents();
            try
            {
                array = directoryInfo.GetDirectories();
                DirectoryInfo[] array3 = array;
                foreach (DirectoryInfo directoryInfo2 in array3)
                {
                    Application.DoEvents();
                    StatusWindow.setDetails(StatusImport, directoryInfo2.Parent.Name + "\\" + directoryInfo2.Name);
                    StatusWindow.Refresh(StatusImport);
                    num += FindComics(directoryInfo2.FullName);
                }
                string[] array4 = new string[3]
				{
					"*.cbr",
					"*.cbz",
					"*.pdf"
				};
                string[] array5 = array4;
                foreach (string searchPattern in array5)
                {
                    array2 = directoryInfo.GetFiles(searchPattern);
                    FileInfo[] array6 = array2;
                    foreach (FileInfo fileInfo in array6)
                    {
                        Application.DoEvents();
                        new FileStream(fileInfo.FullName, FileMode.Open);
                        num++;
                        CC.ImportFiles.Add(fileInfo.FullName);
                    }
                }
                return num;
            }
            catch
            {
                return num;
            }
        }

        private void FormInputMode(bool input)
        {
            PublisherList.Enabled = input;
            GroupList.Enabled = input;
            SeriesList.Enabled = input;
            SourceList.Enabled = input;
            EditMenu.Enabled = input;
            IssueEditMenu.Enabled = input;
            IssueList.ContextMenuStrip.Enabled = input;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            string path = Path.Combine(Application.StartupPath, Settings.Default.CoverDir);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            StatusWindow = new StatusWindowController(lblInfo, Progress, StatusSwitch, StatusStop);
            StatusItems items = default(StatusItems);
            items.Icon = PublisherLogos.Images["Carbon"];
            StatusLogo = StatusWindow.Add(items);
            IssueCovers.ImageSize = new Size(Settings.Default.ThumbWidth, Settings.Default.ThumbHeight);
            RefreshSources();
            if (Root == this)
            {
                IssueList.View = (View)Settings.Default.ViewMode;
                RefreshPublishers();
            }
            else
            {
                ContentSplit.Panel1Collapsed = true;
                ViewModeCovers_Click(this, null);
                MainSplit.Panel1Collapsed = true;
            }

            SizeCoverPeview();
            Reposition();


        }

        private void ToolsDuplicatesMenu_Click(object sender, EventArgs e)
        {
            int num = 0;
            int num2 = 0;
            ComicIssue comicIssue = null;
            ComicIssue comicIssue2 = null;
            double num3 = 0.0;
            chkSearch.Checked = false;
            SearchIssues.Clear();
            Cursor = Cursors.WaitCursor;
            for (num = 0; num <= BrowseIssues.Count - 1; num++)
            {
                comicIssue = (ComicIssue)BrowseIssues[num];
                for (num2 = 0; num2 <= BrowseIssues.Count - 1; num2++)
                {
                    if (num2 != num)
                    {
                        comicIssue2 = (ComicIssue)BrowseIssues[num2];
                        if ((comicIssue2.MD5 == comicIssue.MD5) | ((comicIssue2.Number == comicIssue.Number) & (comicIssue2.SeriesID == comicIssue.SeriesID) & (comicIssue2.SeriesVolume == comicIssue.SeriesVolume) & (comicIssue.Type == comicIssue2.Type) & (((double)comicIssue.Type == Convert.ToDouble(CC.IssueType.Normal)) | ((double)comicIssue.Type == Convert.ToDouble(CC.IssueType.Annual)))))
                        {
                            num3 += (double)comicIssue2.FileSize;
                            if (!SearchIssues.Contains(comicIssue2))
                            {
                                SearchIssues.Add(comicIssue2);
                            }
                            if (!SearchIssues.Contains(comicIssue))
                            {
                                SearchIssues.Add(comicIssue);
                            }
                        }
                    }
                }
            }
            Cursor = Cursors.Default;
            if (SearchIssues.Count > 0)
            {
                IssueList.VirtualListSize = 0;
                CC.Issues = SearchIssues;
                IssueList.VirtualListSize = SearchIssues.Count;
                IssueList.Refresh();
                UpdateInfo(Convert.ToString(CC.ByteToString(num3)));
            }
            else
            {
                MessageBox.Show("No duplicate issues found.", "Find Duplicates", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void EditUndoRenameMenu_Click(object sender, EventArgs e)
        {
            ComicIssue comicIssue = null;
            foreach (int selectedIndex in IssueList.SelectedIndices)
            {
                comicIssue = (ComicIssue)CC.Issues[selectedIndex];
                CC.Rename(comicIssue.FileName, comicIssue.UndoFilename);
                comicIssue.FileName = comicIssue.UndoFilename;
                comicIssue.UndoFilename = "";
                comicIssue.SaveChanges();
                IssueList.Refresh();
            }
        }

        private void SearchMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)e.ClickedItem;
            toolStripMenuItem.Checked = !toolStripMenuItem.Checked;
            if (chkSearch.Checked)
            {
                SearchForIssues();
            }
        }

        private void EditSelectAllMenu_Click(object sender, EventArgs e)
        {
            int num = 0;
            Cursor = Cursors.AppStarting;
            if (PublisherList.Focused)
            {
                PublisherList.SelectedIndices.Clear();
                PublisherList.Items[0].Selected = true;
                PublisherList.Select();
            }
            else if (GroupList.Focused)
            {
                GroupList.SelectedIndices.Clear();
                GroupList.Items[0].Selected = true;
                GroupList.Select();
            }
            else if (SeriesList.Focused)
            {
                SeriesList.SelectedIndices.Clear();
                SeriesList.Items[0].Selected = true;
                SeriesList.Select();
            }
            else if (IssueList.Focused)
            {
                Cursor = Cursors.AppStarting;
                for (num = 0; num <= IssueList.Items.Count - 1; num++)
                {
                    IssueList.Items[num].Selected = true;
                }
                IssueList.Select();
            }
            Cursor = Cursors.Default;
        }

        private void EditUnmarkMenu_Click(object sender, EventArgs e)
        {
            MarkIssue(false);
        }

        private void EditMarkMenu_Click(object sender, EventArgs e)
        {
            MarkIssue(true);
        }

        public void MarkIssue(bool value)
        {
            foreach (int selectedIndex in IssueList.SelectedIndices)
            {
                ComicIssue comicIssue = (ComicIssue)CC.Issues[selectedIndex];
                comicIssue.Marked = value;
                comicIssue.SaveChanges();
                CC.GetIssueCover(comicIssue.ID);
            }
            IssueList_MouseUp(this, new MouseEventArgs(MouseButtons.Left, 0, 0, 0, 0));
            IssueList.Refresh();
        }

        private void IssueList_MouseUp(object sender, MouseEventArgs e)
        {
            bool flag = true;
            bool flag2 = false;
            try
            {
                Cursor = Cursors.AppStarting;
                if (IssueList.SelectedIndices.Count > 0 && e.Button == MouseButtons.Left)
                {
                    ComicIssue comicIssue = (ComicIssue)CC.Issues[IssueList.SelectedIndices[0]];
                    flag2 = (comicIssue.UndoFilename != "");
                    for (int i = 1; i <= IssueList.SelectedIndices.Count - 1; i++)
                    {
                        comicIssue = (ComicIssue)CC.Issues[IssueList.SelectedIndices[i]];
                        ComicIssue comicIssue2 = (ComicIssue)CC.Issues[IssueList.SelectedIndices[i - 1]];
                        if (comicIssue.Marked != comicIssue2.Marked)
                        {
                            flag = false;
                        }
                        if (comicIssue.UndoFilename != "")
                        {
                            flag2 = true;
                        }
                    }
                    EditUndoRenameMenu.Visible = flag2;
                    if (!flag)
                    {
                        EditMarkMenu.Visible = true;
                        EditUnmarkMenu.Visible = true;
                    }
                    else
                    {
                        EditMarkMenu.Visible = !comicIssue.Marked;
                        EditUnmarkMenu.Visible = comicIssue.Marked;
                    }
                }
                Cursor = Cursors.Default;
            }
            catch
            {
            }
        }

        private void IssueList_MouseDown(object sender, MouseEventArgs e)
        {
            int num = 0;
            if (e.Button == MouseButtons.Right)
            {
                //IssueMenu.Enabled = (IssueList.SelectedIndices.Count > 0);
                    
                ToolStripMenuItem issueReadlistMenu = IssueReadlistMenu;
                if (CC.Readlists.Count > 0)
                {
                    issueReadlistMenu.Visible = true;
                    issueReadlistMenu.DropDownItems.Clear();
                    foreach (Readlist readlist in CC.Readlists)
                    {
                        ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(readlist.Name);
                        toolStripMenuItem.Tag = num++;
                        issueReadlistMenu.DropDownItems.Add(toolStripMenuItem);
                        toolStripMenuItem.Click += IssueReadlistMenuSelect;
                    }
                }
                else
                {
                    issueReadlistMenu.Visible = false;
                }
            }
        }

        private void IssueEditMenu_Click(object sender, EventArgs e)
        {
            if (IssueList.SelectedIndices.Count > 0)
            {
                ComicIssue comicIssue = (ComicIssue)CC.Issues[IssueList.SelectedIndices[0]];
                comicIssue.CheckMissing();
                comicIssue.SaveChanges();
                if (!comicIssue.Missing)
                {
                    Process process = new Process();
                    switch (Path.GetExtension(comicIssue.FileName))
                    {
                        case ".cbr":
                            process.StartInfo.FileName = Settings.Default.RAREditor;
                            break;
                        case ".cbz":
                            process.StartInfo.FileName = Settings.Default.ZIPEditor;
                            break;
                        case ".pdf":
                            process.StartInfo.FileName = Settings.Default.PDFEditor;
                            break;
                    }
                    if (process.StartInfo.FileName == "")
                    {
                        MessageBox.Show("You must set an editor in preferences.", "Editor Not Found", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    else
                    {
                        process.StartInfo.Arguments = "\"" + comicIssue.FileName + "\"";
                        process.StartInfo.UseShellExecute = true;
                        process.StartInfo.RedirectStandardOutput = false;
                        process.Start();
                    }
                }
                else
                {
                    IssueList.Refresh();
                }
            }
        }

        private void EditCopyMenu_Click(object sender, EventArgs e)
        {
            StringCollection stringCollection = new StringCollection();
            ComicIssue comicIssue = null;
            foreach (int selectedIndex in IssueList.SelectedIndices)
            {
                comicIssue = (ComicIssue)CC.Issues[selectedIndex];
                stringCollection.Add(comicIssue.FileName);
            }
            Clipboard.SetFileDropList(stringCollection);
        }

        private void ToolsReprocessMenu_Click(object sender, EventArgs e)
        {
            RefreshIssues();
            ReIssues = (ArrayList)BrowseIssues.Clone();
            if (ReIssues.Count > 0)
            {
                DoReprocess();
            }
        }

        private void DoReprocess()
        {
            FormInputMode(false);
            StatusItems items = default(StatusItems);
            items.ShowProgress = true;
            items.ShowStop = true;
            items.EventStop = CancelReprocess;
            items.Task = "Reprocessing Comics..";
            StatusReprocess = StatusWindow.Add(items);
            Reprocessor.RunWorkerAsync();
        }

        private void ToolsMissingMenu_Click(object sender, EventArgs e)
        {
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            int num5 = 0;
            int num6 = 0;
            string text = "";
            ArrayList arrayList = new ArrayList();
            bool flag = false;
            int num7 = 0;
            ComicIssue comicIssue = null;
            for (num2 = 0; num2 <= CC.Issues.Count - 2; num2++)
            {
                comicIssue = (ComicIssue)CC.Issues[num2];
                ComicIssue comicIssue2 = (ComicIssue)CC.Issues[num2 + 1];
                num = comicIssue.Number;
                num7 = comicIssue2.Number;
                if (comicIssue.SeriesName != text)
                {
                    num7 = num;
                    flag = true;
                    text = comicIssue.SeriesName;
                    if (num > 1)
                    {
                        for (num3 = 1; num3 <= num - 1; num3++)
                        {
                            arrayList.Add(text + " v" + comicIssue.SeriesVolume + " #" + num3);
                        }
                    }
                }
                comicIssue = (ComicIssue)CC.Issues[num2 + 1];
                num4 = ((comicIssue.Type == 0) ? (num7 - num) : 0);
                if ((num4 > 1) & (num4 < 1000))
                {
                    comicIssue = (ComicIssue)CC.Issues[num2 + 1];
                    num5 = comicIssue.Number - num4 + 1;
                    num6 = comicIssue.Number - 1;
                    for (num3 = num5; num3 <= num6; num3++)
                    {
                        comicIssue = (ComicIssue)CC.Issues[num2];
                        if (num3 != 0)
                        {
                            arrayList.Add(text + " v" + comicIssue.SeriesVolume + " #" + num3);
                        }
                    }
                }
            }
            if (arrayList.Count == 0)
            {
                MessageBox.Show("No missing issues found.", "Find Missing Issues", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                DialogResult dialogResult = DialogResult.None;
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Title = "Save List";
                saveFileDialog.FileName = "Missing Issues.txt";
                if (!flag)
                {
                    saveFileDialog.FileName = text + " " + saveFileDialog.FileName;
                }
                saveFileDialog.Filter = "Text Files|(*.txt)";
                do
                {
                    dialogResult = saveFileDialog.ShowDialog();
                }
                while (!(saveFileDialog.FileName != ""));
                if (dialogResult != DialogResult.Cancel)
                {
                    File.WriteAllLines(saveFileDialog.FileName, CC.StringList(arrayList));
                }
            }
        }

        private void ViewModeCovers_Click(object sender, EventArgs e)
        {
            ViewModeList.Checked = false;
            ViewModeCovers.Checked = true;
            IssueList.BackColor = Color.Black;
            IssueList.View = View.LargeIcon;
            IssueList.ForeColor = Color.White;
            SourceSplit.Panel2Collapsed = true;
            Reposition();
        }

        private void ViewModeList_Click(object sender, EventArgs e)
        {
            ViewModeList.Checked = true;
            ViewModeCovers.Checked = false;
            IssueList.View = View.Details;
            IssueList.BackColor = Color.White;
            IssueList.ForeColor = Color.Black;
            SourceSplit.Panel2Collapsed = false;
            Reposition();
        }

        private void SourceRenameMenu_Click(object sender, EventArgs e)
        {
            string text = null;
            text = CC.InputBox("Enter new readlist name:", "Rename Readlist", "");
            Readlist readlist = null;
            readlist = (Readlist)CC.Readlists[SourceList.SelectedIndices[0] - 1];
            if (text != "")
            {
                readlist.Name = text;
                readlist.SaveChanges();
                RefreshSources();
            }
        }

        private void SourceClearMenu_Click(object sender, EventArgs e)
        {
            int num = SourceList.SelectedIndices[0];
            SourceList.Items.RemoveAt(num);
            Readlist readlist = (Readlist)CC.Readlists[num - 1];
            readlist.Delete();
            CC.Readlists.Remove(readlist);
            SourceList.Items[0].Selected = true;
        }

        private void BrowseInfoMenu_Click(object sender, EventArgs e)
        {
            if (PublisherList.Focused)
            {
                if (PublisherList.SelectedIndices.Count > 0)
                {
                    ComicPublisher comicPublisher = (ComicPublisher)CC.Publishers[PublisherList.SelectedIndices[0] - 1];
                    CC.InputBox("Name:", "Publisher Info", comicPublisher.Name, PublisherInfoValidation);
                }
            }
            else if (GroupList.Focused)
            {
                if (GroupList.SelectedIndices.Count > 0)
                {
                    ComicGroup comicGroup = (ComicGroup)CC.Groups[GroupList.SelectedIndices[0] - 1];
                    CC.InputBox("Name:", "Group Info", comicGroup.Name, GroupInfoValidation);
                }
            }
            else if (SeriesList.Focused)
            {
                if (SeriesList.SelectedIndices.Count == 1)
                {
                    SeriesInfoForm seriesInfoForm = new SeriesInfoForm();
                    seriesInfoForm.ShowDialog(this);
                }
                else
                {
                    SeriesMultiInfoForm seriesMultiInfoForm = new SeriesMultiInfoForm();
                    seriesMultiInfoForm.ShowDialog(this);
                }
                RefreshIssues();
            }
        }

        private void GroupInfoValidation(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            InputBox inputBox = (InputBox)button.Parent;
            inputBox.DialogResult = DialogResult.None;
            ComicGroup comicGroup = (ComicGroup)CC.Groups[GroupList.SelectedIndices[0] - 1];
            string text = inputBox.Controls["txtInput"].Text;
            try
            {
                if (comicGroup.Name != text)
                {
                    comicGroup.Name = text;
                    comicGroup.SaveChanges();
                    GroupList.Refresh();
                }
                inputBox.DialogResult = DialogResult.OK;
                inputBox.Close();
            }
            catch (LogException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void PublisherInfoValidation(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            InputBox inputBox = (InputBox)button.Parent;
            inputBox.DialogResult = DialogResult.None;
            ComicPublisher comicPublisher = (ComicPublisher)CC.Publishers[PublisherList.SelectedIndices[0] - 1];
            string text = inputBox.Controls["txtInput"].Text;
            try
            {
                if (comicPublisher.Name != text)
                {
                    comicPublisher.Name = text;
                    comicPublisher.SaveChanges();
                    PublisherList.Refresh();
                }
                inputBox.DialogResult = DialogResult.OK;
                inputBox.Close();
            }
            catch (LogException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void RefreshSources()
        {
            SourceList.Items.Clear();
            SourceList.Items.Add("Library", "Library");
            CC.Readlists.Clear();
            Query sqlRow = CC.SQL.ExecQuery("SELECT * FROM readlists ORDER BY name");
            while (sqlRow.NextResult())
            {
                Readlist readlist = new Readlist(ref sqlRow);
                CC.Readlists.Add(readlist);
                SourceList.Items.Add(Convert.ToString(readlist.Name), "Readlist");
            }
            sqlRow.Close();
            SourceList.Items[0].Selected = true;
            SourceList.Select();
        }

        public void SaveReadlists()
        {
            foreach (Readlist readlist in CC.Readlists)
            {
                readlist.SaveChanges();
            }
        }

        private void SourceList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SourceList.SelectedIndices.Count > 0)
            {
                Cursor = Cursors.AppStarting;
                if (SourceList.SelectedIndices[0] != 0)
                {
                    CurrentReadlist = (Readlist)CC.Readlists[SourceList.SelectedIndices[0] - 1];
                    CurrentReadlist.GetIssues();
                }
                else
                {
                    CurrentReadlist = null;
                }
                SaveReadlists();
                RefreshIssues();
                if (SourceList.SelectedIndices.Contains(0))
                {
                    ContentSplit.Panel1Collapsed = false;
                }
                else
                {
                    txtSearch.Text = "";
                    chkSearch.Checked = false;
                    ContentSplit.Panel1Collapsed = true;
                }
                DeleteFromLibrary.Visible = ((!SourceList.SelectedIndices.Contains(0)) ? true : false);
                Reposition();
                Cursor = Cursors.Default;
            }
        }

        private void IssueClearMenu_Click(object sender, EventArgs e)
        {
            if (IssueList.SelectedIndices.Count > 0)
            {
                DialogResult dialogResult = DialogResult.No;
                ArrayList arrayList = new ArrayList();
                if (CurrentReadlist == null || sender == DeleteFromLibrary)
                {
                    Cursor = Cursors.AppStarting;
                    dialogResult = MessageBox.Show("Do you want to keep the files associated with these issues?", "Keep Issues", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                }
                if (dialogResult != DialogResult.Cancel)
                {
                    foreach (int selectedIndex in IssueList.SelectedIndices)
                    {
                        ComicIssue comicIssue = (ComicIssue)CC.Issues[selectedIndex];
                        if (CurrentReadlist == null || sender == DeleteFromLibrary)
                        {
                            CoverPreview.Image.Dispose();
                            IssueCovers.Images.RemoveByKey(Convert.ToString(comicIssue.ID));
                            comicIssue.Delete((dialogResult == DialogResult.Yes) ? false : true);
                        }
                        if (CurrentReadlist != null)
                        {
                            CurrentReadlist.DeleteIssue(selectedIndex);
                        }
                        arrayList.Add(comicIssue);
                    }
                    if (arrayList.Count > 0)
                    {
                        foreach (ComicIssue item in arrayList)
                        {
                            CC.Issues.Remove(item);
                            IssueList.VirtualListSize--;
                        }
                        if (CurrentReadlist != null)
                        {
                            CurrentReadlist.DoDelete();
                        }
                        Cursor = Cursors.Default;
                        IssueList.SelectedIndices.Clear();
                        IssueList.Refresh();
                    }
                }
            }
        }

        public void SearchForIssues()
        {
            IssueList.VirtualListSize = 0;
            CC.Issues = BrowseIssues;
            CC.Publishers = BrowsePublishers;
            CC.Series = BrowseSeries;
            CC.Groups = BrowseGroups;
            IssueList.VirtualListSize = CC.Issues.Count;
            PublisherList.VirtualListSize = CC.Publishers.Count + 1;
            GroupList.VirtualListSize = CC.Groups.Count + 1;
            SeriesList.VirtualListSize = CC.Series.Count + 1;
            int num = 0;
            double num2 = 0.0;
            ArrayList arrayList = new ArrayList();
            bool flag = false;
            SearchIssues.Clear();
            SearchPublishers.Clear();
            SearchGroups.Clear();
            SearchSeries.Clear();
            IssueCovers.Images.Clear();
            string[] array = txtSearch.Text.Split(char.Parse(" "));
            for (int i = 0; i <= BrowseIssues.Count - 1; i++)
            {
                ComicIssue comicIssue = (ComicIssue)BrowseIssues[i];
                arrayList.Clear();
                if (SearchPlotMenu.Checked)
                {
                    arrayList.Add(comicIssue.Name);
                }
                if (SearchSeriesMenu.Checked)
                {
                    arrayList.Add(comicIssue.SeriesName);
                }
                if (SearchGroupMenu.Checked)
                {
                    arrayList.Add(comicIssue.GroupName);
                }
                if (SearchPublisherMenu.Checked)
                {
                    arrayList.Add(comicIssue.PublisherName);
                }
                if (SearchFilenameMenu.Checked)
                {
                    arrayList.Add(comicIssue.FileName);
                }
                num = 0;
                string[] array2 = array;
                foreach (string value in array2)
                {
                    flag = false;
                    foreach (string item in arrayList)
                    {
                        if (item.IndexOf(value, StringComparison.CurrentCultureIgnoreCase) > -1)
                        {
                            flag = true;
                        }
                    }
                    if (flag)
                    {
                        num++;
                    }
                }
                if (num == array.Length)
                {
                    SearchIssues.Add(comicIssue);
                    if (CC.FindGroupByID(comicIssue.GroupID, SearchGroups) == null)
                    {
                        SearchGroups.Add(CC.FindGroupByID(comicIssue.GroupID, CC.Groups));
                    }
                    if (CC.FindPublisherByID(comicIssue.PublisherID, SearchPublishers) == null)
                    {
                        SearchPublishers.Add(CC.FindPublisherByID(comicIssue.PublisherID, CC.Publishers));
                    }
                    if (CC.FindSeriesByID(comicIssue.SeriesID, SearchSeries) == null)
                    {
                        SearchSeries.Add(CC.FindSeriesByID(comicIssue.SeriesID, CC.Series));
                    }
                    num2 += (double)comicIssue.FileSize;
                }
            }
            SearchTotalSize = Convert.ToString(CC.ByteToString(num2));
            try
            {
                IssueList.VirtualListSize = 0;
                CC.Issues = SearchIssues;
                CC.Groups = SearchGroups;
                CC.Series = SearchSeries;
                CC.Publishers = SearchPublishers;
                IssueList.VirtualListSize = SearchIssues.Count;
                PublisherList.VirtualListSize = SearchPublishers.Count + 1;
                GroupList.VirtualListSize = SearchGroups.Count + 1;
                SeriesList.VirtualListSize = SearchSeries.Count + 1;
            }
            catch
            {
            }
            Reposition();
            UpdateInfo(SearchTotalSize);
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                if (chkSearch.Checked)
                {
                    SearchForIssues();
                }
                chkSearch.Checked = true;
            }
        }

        private void chkSearch_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkSearch.Checked)
            {
                IssueList.VirtualListSize = 0;
                CC.Issues = BrowseIssues;
                CC.Publishers = BrowsePublishers;
                CC.Series = BrowseSeries;
                CC.Groups = BrowseGroups;
                IssueList.VirtualListSize = CC.Issues.Count;
                PublisherList.VirtualListSize = CC.Publishers.Count + 1;
                GroupList.VirtualListSize = CC.Groups.Count + 1;
                SeriesList.VirtualListSize = CC.Series.Count + 1;
                SearchIssues.Clear();
                txtSearch.Clear();
                UpdateInfo(BrowseTotalSize);
                IssueList.Refresh();
            }
            else if (txtSearch.Text != "")
            {
                SearchForIssues();
            }
        }

        private void ContentSplit_SplitterMoved(object sender, SplitterEventArgs e)
        {
            Reposition();
        }

        private void IssueShowFileMenu_Click(object sender, EventArgs e)
        {
            if (IssueList.SelectedIndices.Count > 0)
            {
                ComicIssue comicIssue = (ComicIssue)CC.Issues[IssueList.SelectedIndices[0]];
                comicIssue.CheckMissing();
                comicIssue.SaveChanges();
                if (!comicIssue.Missing)
                {
                    string directoryName = Path.GetDirectoryName(comicIssue.FileName);
                    if (File.Exists(comicIssue.FileName))
                    {
                        CC.ExecuteFile(directoryName);
                    }
                }
                else
                {
                    IssueList.Refresh();
                }
            }
        }

        private void FileExitMenu_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        public void SizeCoverPeview()
        {
            SourceSplit.SplitterDistance = (int)((decimal)MainSplit.Height - (decimal)CoverPreview.Width / Settings.Default.ThumbRatio);
        }

        private void MainSplit_SplitterMoved(object sender, SplitterEventArgs e)
        {
            SizeCoverPeview();
            Reposition();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            Reposition();
        }

        private void EditCoverMenu_Click(object sender, EventArgs e)
        {
            SourceSplit.Panel2Collapsed = !SourceSplit.Panel2Collapsed;
            Reposition();
        }

        private void EditBrowserMenu_Click(object sender, EventArgs e)
        {
            ContentSplit.Panel1Collapsed = !ContentSplit.Panel1Collapsed;
            Reposition();
        }

        private void EditSourceMenu_Click(object sender, EventArgs e)
        {
            MainSplit.Panel1Collapsed = !MainSplit.Panel1Collapsed;
            Reposition();
        }

        private void SourceOpenMenu_Click(object sender, EventArgs e)
        {
            NewWindow(SourceList.SelectedIndices[0]);
        }

        private void EditPrefMenu_Click(object sender, EventArgs e)
        {
            PreferencesForm preferencesForm = new PreferencesForm();
            DialogResult dialogResult = preferencesForm.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {
                RefreshIssues();
            }
        }

        private void DoImportFiles()
        {
            FormInputMode(false);
            StatusWindow.setProgressStyle(StatusImport, ProgressBarStyle.Continuous);
            StatusWindow.setShowStop(StatusImport, true);
            StatusWindow.setTask(StatusImport, "Processing Comics...");
            StatusWindow.setEventStop(StatusImport, CancelImport);
            StatusWindow.Refresh(StatusImport);
            Importer.RunWorkerAsync();
        }

        private void Importer_DoWork(object sender, DoWorkEventArgs e)
        {
            Readlist readlist = null;
            Importer.ReportProgress(0);
            foreach (string importFile in CC.ImportFiles)
            {
                if (Importer.CancellationPending)
                {
                    break;
                }
                ComicIssue comicIssue = new ComicIssue();
                comicIssue.FileName = importFile;
                comicIssue.AutoTag();
                Exception ex = comicIssue.Process();
                if (ex != null)
                {
                    ErrList.Add(ex.ToString());
                    if (readlist == null)
                    {
                        readlist = new Readlist(Readlist.GetID("Import Errors", true));
                    }
                    comicIssue.Comments = ex.ToString();
                    comicIssue.SaveChanges();
                    readlist.AddIssue(comicIssue.ID);
                }
                int percentProgress = (int)Math.Floor((double)CurrentImport / (double)CC.ImportFiles.Count * 100.0);
                Importer.ReportProgress(percentProgress);
            }
        }

        private void CancelImport(object sender, EventArgs e)
        {
            Importer.CancelAsync();
        }

        private void CancelReprocess(object sender, EventArgs e)
        {
            Reprocessor.CancelAsync();
        }

        private void SeriesAllMenu_Click(object sender, EventArgs e)
        {
            SeriesAllMenu.Checked = true;
            SeriesNormalMenu.Checked = false;
            SeriesLimitedMenu.Checked = false;
            SeriesOneshotsMenu.Checked = false;
            RefreshSeries(-1);
        }

        private void SeriesNormalMenu_Click(object sender, EventArgs e)
        {
            SeriesAllMenu.Checked = false;
            SeriesNormalMenu.Checked = true;
            SeriesLimitedMenu.Checked = false;
            SeriesOneshotsMenu.Checked = false;
            RefreshSeries(0);
        }

        private void SeriesLimitedMenu_Click(object sender, EventArgs e)
        {
            SeriesAllMenu.Checked = false;
            SeriesNormalMenu.Checked = false;
            SeriesLimitedMenu.Checked = true;
            SeriesOneshotsMenu.Checked = false;
            RefreshSeries(1);
        }

        private void SeriesOneshotsMenu_Click(object sender, EventArgs e)
        {
            SeriesAllMenu.Checked = false;
            SeriesNormalMenu.Checked = false;
            SeriesLimitedMenu.Checked = false;
            SeriesOneshotsMenu.Checked = true;
            RefreshSeries(2);
        }

        private void StatusSwitch_Click(object sender, EventArgs e)
        {
            StatusWindow.Next();
        }

        private void Reprocesser_DoWork(object sender, DoWorkEventArgs e)
        {
            Reprocessor.ReportProgress(0);
            for (int i = 0; i < ReIssues.Count; i++)
            {
                if (Reprocessor.CancellationPending)
                {
                    break;
                }
                ComicIssue comicIssue = (ComicIssue)ReIssues[i];
                comicIssue.CheckMissing();
                comicIssue.SaveChanges();
                if (!comicIssue.Missing)
                {
                    comicIssue.Process();
                    IssueCovers.Images.RemoveByKey(Convert.ToString(comicIssue.ID));
                }
                int percentProgress = (int)Math.Floor((double)currentReprocess / (double)ReIssues.Count * 100.0);
                Reprocessor.ReportProgress(percentProgress);
            }
        }

        private void DeleteFromLibrary_Click(object sender, EventArgs e)
        {
            IssueClearMenu_Click(DeleteFromLibrary, null);
        }

        private void HelpAboutMenu_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.ShowDialog();
        }

        private void autoTagToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            foreach (int selectedIndex in IssueList.SelectedIndices)
            {
                ComicIssue comicIssue = (ComicIssue)CC.Issues[selectedIndex];
                CC.ImportFiles.Add(comicIssue.FileName);
            }
            ImportForm importForm = new ImportForm();
            importForm.ShowDialog(this);
            if (importForm.DialogResult != DialogResult.Cancel)
            {
                Cursor = Cursors.AppStarting;
                foreach (int selectedIndex2 in IssueList.SelectedIndices)
                {
                    ComicIssue comicIssue = (ComicIssue)CC.Issues[selectedIndex2];
                    comicIssue.AutoTag();
                    comicIssue.SaveChanges();
                }
                Cursor = Cursors.Default;
            }
            CC.ImportFiles.Clear();
            IssueList.Refresh();
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            Reposition();

        }

    }
}
