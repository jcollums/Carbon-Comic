using CarbonComic.Properties;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CarbonComic
{
	public class ComicIssue : IComparable
	{
		public int ID;
		public DateTime DateAdded = default(DateTime);
		public string SeriesName = "";
		public int Status = -1;
		public int SeriesType;
		public string GroupName = Settings.Default.UnknownGroup;
		public int GroupID;
		public string PublisherName = Settings.Default.UnknownPublisher;
		public int PublisherID;
		public string UndoFilename = "";
		private string iName = "";
		private int iSeriesID;
		private int iNumber;
		private string iFileName = "";
		private int iFileSize;
		private DateTime iPublished = default(DateTime);
		private int iType;
		private bool iMissing;
		private bool iMarked;
		private int iSeriesVolume = 1;
		private int iPages;
		private string iMD5 = "";
		private string iComments = "";
		private ArrayList Changes = new ArrayList();
		public static string sortBy;
		public static string sortOrder;

		public string Name
		{
			get
			{
				return iName;
			}
			set
			{
				if (value != iName)
				{
					iName = value;
					Changes.Add("Name");
				}
			}
		}

		public string Comments
		{
			get
			{
				return iComments;
			}
			set
			{
				if (value != iComments)
				{
					iComments = value;
					Changes.Add("Comments");
				}
			}
		}

		public string MD5
		{
			get
			{
				return iMD5;
			}
			set
			{
				if (value != iMD5)
				{
					iMD5 = value;
					Changes.Add("MD5");
				}
			}
		}

		public int Pages
		{
			get
			{
				return iPages;
			}
			set
			{
				if (value != iPages)
				{
					iPages = value;
					Changes.Add("Pages");
				}
			}
		}

		public int Number
		{
			get
			{
				return iNumber;
			}
			set
			{
				if (value != iNumber)
				{
					iNumber = value;
					Changes.Add("Number");
				}
			}
		}

		public int SeriesID
		{
			get
			{
				return iSeriesID;
			}
			set
			{
				if (value != iSeriesID)
				{
					iSeriesID = value;
					Changes.Add("SeriesID");
				}
			}
		}

		public bool Marked
		{
			get
			{
				return iMarked;
			}
			set
			{
				if (value != iMarked)
				{
					iMarked = value;
					Changes.Add("Marked");
				}
			}
		}

		public bool Missing
		{
			get
			{
				return iMissing;
			}
			set
			{
				if (value != iMissing)
				{
					iMissing = value;
					Changes.Add("Missing");
				}
			}
		}

		public string FileName
		{
			get
			{
				return iFileName;
			}
			set
			{
				if (value != iFileName)
				{
					UndoFilename = iFileName;
					iFileName = value;
					Changes.Add("FileName");
				}
			}
		}

		public int FileSize
		{
			get
			{
				return iFileSize;
			}
			set
			{
				if (value != iFileSize)
				{
					iFileSize = value;
					Changes.Add("FileSize");
				}
			}
		}

		public DateTime Published
		{
			get
			{
				return iPublished;
			}
			set
			{
				if (value != iPublished)
				{
					iPublished = value;
					Changes.Add("Published");
				}
			}
		}

		public int Type
		{
			get
			{
				return iType;
			}
			set
			{
				if (value != iType)
				{
					iType = value;
					Changes.Add("Type");
				}
			}
		}

		public int SeriesVolume
		{
			get
			{
				return iSeriesVolume;
			}
			set
			{
				if (value != iSeriesVolume)
				{
					iSeriesVolume = value;
					Changes.Add("SeriesVolume");
				}
			}
		}

		public ComicIssue()
		{
			ID = -1;
		}

		public ComicIssue(int ID)
		{
			this.ID = ID;
			Query sqlRow = null;
			GetInfo(ref sqlRow);
		}

		public ComicIssue(ref Query sqlRow)
		{
			GetInfo(ref sqlRow);
		}

		public void GetInfo(ref Query sqlRow)
		{
			if (sqlRow == null)
			{
				sqlRow = CC.SQL.ExecQuery("SELECT i.*, s.*, p.name, g.name FROM issues i, series s, groups g, publishers p WHERE s.id=i.series_id AND s.group_id=g.id AND s.pub_id=p.id AND i.id=" + ID);
				sqlRow.NextResult();
			}
			ID = (int)sqlRow.hash["i.id"];
			SeriesName = (string)sqlRow.hash["s.name"];
			PublisherName = (string)sqlRow.hash["p.name"];
			GroupName = (string)sqlRow.hash["g.name"];
			GroupID = (int)sqlRow.hash["group_id"];
			PublisherID = (int)sqlRow.hash["pub_id"];
			iSeriesID = (int)sqlRow.hash["series_id"];
			iMD5 = (string)sqlRow.hash["md5"];
			iFileName = (string)sqlRow.hash["filename"];
			iName = (string)sqlRow.hash["i.name"];
			iNumber = (int)sqlRow.hash["issue_no"];
			iFileSize = (int)sqlRow.hash["filesize"];
			iType = (int)sqlRow.hash["i.type"];
			iSeriesVolume = (int)sqlRow.hash["series_vol"];
			iMarked = (bool)sqlRow.hash["marked"];
			iMissing = (bool)sqlRow.hash["missing"];
			DateAdded = (DateTime)sqlRow.hash["date_added"];
			iPages = (int)sqlRow.hash["pages"];
			SeriesType = (int)sqlRow.hash["s.type"];
			UndoFilename = (string)sqlRow.hash["undo_filename"];
			iComments = (string)sqlRow.hash["comments"];
			if (sqlRow.hash["published"].ToString() != "")
			{
				Published = (DateTime)sqlRow.hash["published"];
			}
			GetStatus();
		}

		public void GetInfo()
		{
			Query sqlRow = null;
			GetInfo(ref sqlRow);
		}

		public void GetStatus()
		{
			Status = ((!iMissing) ? (iMarked ? 1 : (-1)) : 0);
		}

		//Determine what the filepath of the issue should be
		public string GenerateFilename()
		{
			string libraryDir = Settings.Default.LibraryDir;
			string text = null;
			Query query = null;
			string str = CC.URLize(SeriesName);
			string text2 = CC.URLize(Name);
			string text3 = null;
			int num = 0;
			libraryDir += "\\";
			libraryDir = libraryDir + PublisherName + "\\";
			libraryDir = libraryDir + GroupName + "\\";
			libraryDir = libraryDir + str + "\\";
			libraryDir += str;
			if (SeriesType != 2)
			{
				query = CC.SQL.ExecQuery("SELECT DISTINCT series_vol FROM issues WHERE series_id=" + SeriesID);
				query.NextResult();
				if (query.NextResult())
				{
					libraryDir = libraryDir + " v" + SeriesVolume;
				}
				query.Close();
				switch (Type)
				{
					case 0:
						libraryDir = libraryDir + " #" + Number.ToString("000");
						break;
					case 1:
						libraryDir = libraryDir + " Annual " + Number;
						break;
					case 2:
						if (text2 != "")
						{
							libraryDir = libraryDir + " " + text2;
						}
						break;
				}
				if ((text2 != "") & (Type != 2))
				{
					libraryDir = libraryDir + " (" + text2 + ")";
				}
			}
			text3 = CC.GetArchiveType(FileName);
			text = libraryDir;
			if (FileName != libraryDir + text3)
			{
				while (File.Exists(text + text3))
				{
					num++;
					text = libraryDir + " " + num;
				}
				return text + text3;
			}
			return FileName;
		}

		public void Delete(bool Files)
		{
			CC.SQL.ExecQuery("DELETE FROM issues WHERE id=" + ID);
			CC.SQL.ExecQuery("DELETE FROM readlist_issues WHERE issue_id=" + ID);
			try
			{
				File.Delete(Path.Combine(Application.StartupPath, Settings.Default.CoverDir) + "\\" + ID + ".jpg");
			}
			catch
			{
			}
			if (Files)
			{
				try
				{
					File.Delete(FileName);
				}
				catch
				{
				}
			}
		}

		public void Delete()
		{
			Delete(false);
		}

		public bool SaveChanges()
		{
			SQL sQL = new SQL();
			if (ID != -1)
			{
				if (Changes.Count > 0)
				{
					ArrayList arrayList = new ArrayList();
					if (Changes.Contains("FileSize"))
					{
						arrayList.Add("filesize=" + iFileSize);
						Changes.Remove("FileSize");
					}
					if (Changes.Contains("FileName"))
					{
						arrayList.Add("filename='" + SQL.Prepare(iFileName) + "', undo_filename='" + SQL.Prepare(UndoFilename) + "'");
						Changes.Remove("FileName");
					}
					if (Changes.Contains("Number"))
					{
						arrayList.Add("issue_no=" + iNumber);
						Changes.Remove("Number");
					}
					if (Changes.Contains("Type"))
					{
						arrayList.Add("type=" + iType);
						Changes.Remove("Type");
					}
					if (Changes.Contains("Name"))
					{
						arrayList.Add("name='" + SQL.Prepare(iName) + "'");
						Changes.Remove("Name");
					}
					if (Changes.Contains("Published"))
					{
						arrayList.Add("published='" + iPublished.ToString() + "'");
						Changes.Remove("Published");
					}
					if (Changes.Contains("SeriesVolume"))
					{
						arrayList.Add("series_vol=" + iSeriesVolume);
						Changes.Remove("Published");
					}
					if (Changes.Contains("Missing"))
					{
						arrayList.Add("missing=" + iMissing.ToString());
						Changes.Remove("Missing");
					}
					if (Changes.Contains("Comments"))
					{
						arrayList.Add("comments='" + SQL.Prepare(iComments) + "'");
						Changes.Remove("Comments");
					}
					if (Changes.Contains("Marked"))
					{
						arrayList.Add("marked=" + iMarked.ToString());
						Changes.Remove("Marked");
						Status = ((!iMissing) ? (iMarked ? 1 : (-1)) : 0);
					}
					if (Changes.Contains("Pages"))
					{
						arrayList.Add("pages=" + iPages);
						Changes.Remove("Pages");
					}
					if (Changes.Contains("SeriesID"))
					{
						arrayList.Add("series_id=" + iSeriesID);
						Changes.Remove("SeriesID");
					}
					if (Changes.Contains("MD5"))
					{
						arrayList.Add("MD5='" + iMD5 + "'");
						Changes.Remove("MD5");
					}
					if (arrayList.Count > 0)
					{
						CC.SQL.ExecQuery("UPDATE issues SET " + string.Join(",", CC.StringList(arrayList)) + " WHERE id=" + Convert.ToString(ID));
					}
					GetStatus();
					return true;
				}
				return false;
			}
			string query = "INSERT INTO issues (issue_no, name, series_id, series_vol, filesize, filename, undo_filename, pages, type, date_added, MD5, comments) VALUES (" + Number + ", '" + SQL.Prepare(Name) + "', " + SeriesID + ", " + SeriesVolume + ", " + FileSize + ", '" + SQL.Prepare(FileName) + "', '', " + Pages + ", " + Type + ", NOW(), '" + MD5 + "', '" + SQL.Prepare(Comments) + "')";
			sQL.ExecQuery(query);
			Query query2 = sQL.ExecQuery("SELECT id FROM issues WHERE filename='" + SQL.Prepare(FileName) + "'");
			query2.NextResult();
			ID = (int)query2.hash["id"];
			query2.Close();
			GetInfo();
			return true;
		}

		public void OrganizeFile()
		{
			if ((Settings.Default.OrganizeMethod != 0) & !Changes.Contains("FileName"))
			{
				string text = GenerateFilename();
				if (text != FileName)
				{
					FileName = text;
					CC.SQL.ExecQuery("UPDATE issues SET filename='" + SQL.Prepare(iFileName) + "', undo_filename='" + SQL.Prepare(UndoFilename) + "' WHERE id=" + Convert.ToString(ID));
					string directoryName = Path.GetDirectoryName(FileName);
					if (!Directory.Exists(directoryName))
					{
						Directory.CreateDirectory(directoryName);
					}
					string a = UndoFilename.Substring(0, Settings.Default.LibraryDir.Length);
					if ((Settings.Default.OrganizeMethod == 1) | (a == Settings.Default.LibraryDir))
					{
						File.Move(UndoFilename, FileName);
					}
					else if (Settings.Default.OrganizeMethod == 2)
					{
						File.Copy(UndoFilename, FileName);
					}
				}
			}
			else
			{
				Changes.Remove("FileName");
			}
		}

		private void Locate(bool supress)
		{
			DialogResult dialogResult = supress ? DialogResult.No : MessageBox.Show("Could not find \"" + FileName + "\"." + Environment.NewLine + "Would you like to locate this file now?", "File not found", MessageBoxButtons.YesNo, MessageBoxIcon.Hand);
			if (dialogResult == DialogResult.Yes)
			{
				OpenFileDialog openFileDialog = new OpenFileDialog();
				dialogResult = openFileDialog.ShowDialog();
				if (dialogResult != DialogResult.Cancel)
				{
					FileName = openFileDialog.FileName;
					SaveChanges();
					Missing = false;
				}
				else
				{
					Missing = true;
				}
			}
			else
			{
				Missing = true;
			}
		}

		public void CheckMissing()
		{
			CheckMissing(false);
		}

		public void CheckMissing(bool supress)
		{
			if (Missing && File.Exists(FileName))
			{
				Missing = false;
			}
			else if (!File.Exists(FileName))
			{
				Locate(supress);
			}
		}

		public void AutoTag()
		{
			Regex regex = new Regex("[^0-9]");
			Regex regex2 = new Regex(CC.AutoTag.Pattern);
			Match match = regex2.Match(FileName);
			string value = match.Groups[CC.AutoTag.Matches[0]].Value;
			if (value != "")
			{
				PublisherName = value;
			}
			string value2 = match.Groups[CC.AutoTag.Matches[2]].Value;
			if (value2 != "")
			{
				SeriesName = value2;
			}
			string value3 = match.Groups[CC.AutoTag.Matches[1]].Value;
			if (value3 != "")
			{
				GroupName = value3;
			}
			string value4 = match.Groups[CC.AutoTag.Matches[4]].Value;
			if (value4 != "")
			{
				Name = value4;
			}
			try
			{
				Number = Convert.ToInt16(match.Groups[CC.AutoTag.Matches[5]].Value);
			}
			catch
			{
			}
			try
			{
				string value5 = match.Groups[CC.AutoTag.Matches[3]].Value;
				value5 = regex.Replace(value5, "");
				SeriesVolume = Convert.ToInt16(value5);
			}
			catch
			{
			}
			if ((PublisherName == "") & CC.AutoTag.usePublisher)
			{
				PublisherName = CC.AutoTag.Publisher;
			}
			if ((SeriesName == "") & CC.AutoTag.useSeries)
			{
				SeriesName = CC.AutoTag.Series;
			}
			if ((GroupName == "") & CC.AutoTag.useGroup)
			{
				GroupName = CC.AutoTag.Group;
			}
			if ((Name == "") & CC.AutoTag.usePlot)
			{
				Name = CC.AutoTag.Plot;
			}
			if ((SeriesVolume == 0) & CC.AutoTag.useVolume)
			{
				SeriesVolume = CC.AutoTag.Volume;
			}
			if (SeriesName == "")
			{
				SeriesName = Path.GetFileNameWithoutExtension(FileName);
			}
			if ((SeriesVolume == 0) | (SeriesVolume >= 10))
			{
				SeriesVolume = 1;
			}
			if (Number == 0)
			{
				Type = 2;
			}
			else if (Number.ToString().Length == 4)
			{
				Type = 1;
			}
			else
			{
				Type = 0;
			}
			for (int i = 0; i < Settings.Default.ReplaceKeys.Count; i++)
			{
				SeriesName = SeriesName.Replace(Settings.Default.ReplaceKeys[i], Settings.Default.ReplaceVals[i]);
			}
			PublisherID = ComicPublisher.GetID(PublisherName);
			GroupID = ComicGroup.GetID(GroupName, PublisherID, PublisherName);
			SeriesID = ComicSeries.GetID(SeriesName, GroupID, PublisherID);
		}

		public Exception Process()
		{
			FileStream fileStream = new FileStream(FileName, FileMode.Open);
			FileSize = (int)fileStream.Length;
			MD5 = CC.md5file(fileStream);
			fileStream.Close();
			int num = -1;
			if (Settings.Default.FindDuplicates)
			{
				num = FindDuplicate();
			}
			if (num == -1)
			{
				string archiveType;
				try
				{
					archiveType = CC.GetArchiveType(FileName);
				}
				catch (Exception result)
				{
					return result;
				}
				string text = Path.Combine(Environment.GetEnvironmentVariable("TEMP"), "CarbonImport");
				if (!Directory.Exists(text))
				{
					Directory.CreateDirectory(text);
				}
				SaveChanges();
				ArrayList arrayList = new ArrayList();
				try
				{
					if (archiveType == ".cbr")
					{
						Unrar unrar = new Unrar();
						unrar.ArchivePathName = FileName;
						unrar.Open(Unrar.OpenMode.List);
						while (unrar.ReadHeader())
						{
							if (!unrar.CurrentFile.IsDirectory & CC.IsImageExt(unrar.CurrentFile.FileName))
							{
								arrayList.Add(unrar.CurrentFile.FileName);
							}
							unrar.Skip();
						}
						unrar.Close();
						arrayList.Sort();
						unrar.Open(Unrar.OpenMode.Extract);
						while (unrar.ReadHeader())
						{
							if (unrar.CurrentFile.FileName == (string)arrayList[0])
							{
								if (Settings.Default.ThumbGen)
								{
									text = Path.Combine(text, ID + Path.GetExtension(unrar.CurrentFile.FileName));
									unrar.Extract(text);
								}
								break;
							}
							unrar.Skip();
						}
						unrar.Close();
					}
					else if (archiveType == ".cbz")
					{
						ZipFile zipFile = new ZipFile(FileName);
						foreach (ZipEntry item in zipFile)
						{
							if (item.IsFile & CC.IsImageExt(item.Name) && item.Name.Substring(0, 1) != "." && !item.Name.Contains("/") && !item.Name.Contains("/."))
							{
								arrayList.Add(item.Name);
							}
						}
						zipFile.Close();
						arrayList.Sort();
						if (Settings.Default.ThumbGen)
						{
							text = Path.Combine(text, ID + Path.GetExtension((string)arrayList[0]));
							CC.unzip(FileName, (string)arrayList[0], text);
						}
					}
					if ((Path.GetExtension(text).ToLower() == ".png") | (Path.GetExtension(text).ToLower() == ".gif"))
					{
						text = CC.ConvertJPG(text);
					}
					Pages = arrayList.Count;
					SaveChanges();
				}
				catch (Exception result2)
				{
					return result2;
				}
				if (Settings.Default.OrganizeMethod != 0)
				{
					OrganizeFile();
				}
				if ((archiveType != ".pdf") & Settings.Default.ThumbGen)
				{
					try
					{
						String dst = Path.Combine(Application.StartupPath, Settings.Default.CoverDir) + "\\" + ID + ".jpg";
						CC.CreateThumbnail(text, dst);
						File.Delete(text);
					}
					catch (Exception result3)
					{
						return result3;
					}
				}
			}
			return null;
		}

		public int CompareTo(object o)
		{
			ComicIssue comicIssue = (ComicIssue)o;
			string a;
			if ((a = sortBy) != null && a == "series")
			{
				if (!(sortOrder == "ASC"))
				{
					return comicIssue.SeriesName.CompareTo(SeriesName);
				}
				return SeriesName.CompareTo(comicIssue.SeriesName);
			}
			return 0;
		}

		public int FindDuplicate()
		{
			int result = -1;
			Query query = CC.SQL.ExecQuery("SELECT id, filename FROM issues WHERE (filename='" + SQL.Prepare(FileName) + "' OR md5='" + MD5 + "') AND NOT (id = " + ID + ")");
			if (query.NextResult())
			{
				result = (int)query.hash["id"];
			}
			query.Close();
			return result;
		}
	}
}
