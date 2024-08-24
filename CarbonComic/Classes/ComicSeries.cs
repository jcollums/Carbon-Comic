using CarbonComic.Properties;
using System;
using System.Collections;
using System.IO;

namespace CarbonComic
{
	public class ComicSeries : IComparable
	{
		public int ID;
		public string iName;
		public int iType;
		public int PublisherID;
		public int GroupID;
		public ArrayList Changes = new ArrayList(); //Changes to save
		public string PublisherName;
		public string GroupName;
		private string OldName;     //When renaming the Series we keep track of what its old name was

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
					if (value == "")
					{
						throw new InvalidInputException("Invalid series name.");
					}
					Query query = CC.SQL.ExecQuery("SELECT * FROM series WHERE name='" + SQL.Prepare(value) + "' AND NOT (id = " + ID + ")");
					if (query.NextResult())
					{
						throw new ItemExistsException("There is already a series with this name.");
					}
					query.Close();
					OldName = iName;
					iName = value;
					Changes.Add("Name");
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

		//constructor based on ID
		public ComicSeries(int ID)
		{
			Query sqlRow = CC.SQL.ExecQuery("SELECT s.*, p.name, g.name FROM series s, publishers p, groups g WHERE s.pub_id=p.id AND s.group_id=g.id AND s.id=" + ID);
			sqlRow.NextResult();
			GetInfo(ref sqlRow);
			sqlRow.Close();
		}

		//constructor that loads from an already-fetched SQL row
		public ComicSeries(ref Query sqlRow)
		{
			GetInfo(ref sqlRow);
		}

		//set vars from sql row
		public void GetInfo(ref Query sqlRow)
		{
			ID = (int)sqlRow.hash["id"];
			iName = (string)sqlRow.hash["s.name"];
			PublisherID = (int)sqlRow.hash["pub_id"];
			iType = (int)sqlRow.hash["type"];
			GroupID = (int)sqlRow.hash["group_id"];
			PublisherName = (string)sqlRow.hash["p.name"];
			GroupName = (string)sqlRow.hash["g.name"];
		}

		//Delete a series and its issues
		public void Delete(bool Files)
		{
			SQL sQL = new SQL();
			Query query = sQL.ExecQuery("SELECT id FROM issues WHERE series_id=" + ID);
			while (query.NextResult())
			{
				ComicIssue comicIssue = new ComicIssue((int)query.hash["id"]);
				comicIssue.Delete(Files);
			}
			sQL.ExecQuery("DELETE FROM series WHERE id=" + ID);
			if (Settings.Default.OrganizeMethod != 0)
			{
				try
				{
					//Delete folder for series if file org is turned on
					Directory.Delete(Settings.Default.LibraryDir + "\\" + CC.URLize(PublisherName) + "\\" + CC.URLize(GroupName) + "\\" + CC.URLize(Name));
				}
				catch
				{
				}
			}
		}

		//Determine if the series has issues assigned to it
		public bool HasIssues()
		{
			Query query = null;
			bool flag = false;
			query = CC.SQL.ExecQuery("SELECT id FROM issues WHERE series_id=" + ID);
			flag = query.NextResult();
			query.Close();
			return flag;
		}

		public bool SaveChanges()
		{
			ArrayList arrayList = new ArrayList();
			if (Changes.Count > 0)
			{
				if (Changes.Contains("Name"))
				{
					arrayList.Add("name='" + SQL.Prepare(iName) + "'");
				}
				if (Changes.Contains("Type"))
				{
					arrayList.Add("type=" + iType);
					Changes.Remove("Type");
				}
				CC.SQL.ExecQuery("UPDATE series SET " + string.Join(",", CC.StringList(arrayList)) + " WHERE id=" + ID);
				if (Settings.Default.OrganizeMethod != 0)
				{
					if (Changes.Contains("Name"))
					{
						//Rename folders
						string text = Settings.Default.LibraryDir + "\\" + CC.URLize(PublisherName) + "\\" + CC.URLize(GroupName) + "\\" + CC.URLize(OldName) + "\\";
						string text2 = Settings.Default.LibraryDir + "\\" + CC.URLize(PublisherName) + "\\" + CC.URLize(GroupName) + "\\" + CC.URLize(Name) + "\\";
						CC.SQL.ExecQuery("UPDATE issues SET filename=" + CC.SQLReplaceLeft("filename", text, text2));
						CC.Rename(text, text2);
					}
					Changes.Remove("Name");
				}
				return true;
			}
			return false;
		}

		public static int GetID(string SeriesName)
		{
			return GetID(SeriesName, ComicGroup.GetID(Settings.Default.UnknownGroup));
		}

		public static int GetID(string SeriesName, int GroupID)
		{
			return GetID(SeriesName, GroupID, ComicPublisher.GetID(Settings.Default.UnknownPublisher));
		}

		public static int GetID(string SeriesName, int GroupID, int PubID)
		{
			if (SeriesName == "")
			{
				SeriesName = Settings.Default.UnknownSeries;
			}
			Query query = CC.SQL.ExecQuery("SELECT id FROM series WHERE name='" + SQL.Prepare(SeriesName) + "'");
			if (!query.NextResult())
			{
				query.Close();
				//Create series if it doesn't exist.
				CC.SQL.ExecQuery("INSERT INTO series (name, group_id, pub_id, type) VALUES ('" + SQL.Prepare(SeriesName) + "', " + GroupID + ", " + PubID + ", 0)");
				return GetID(SeriesName, GroupID, PubID);
			}
			int result = (int)query.hash["id"];
			query.Close();
			return result;
		}

		//When comparing series we only care about its name
		public int CompareTo(object o)
		{
			ComicSeries comicSeries = (ComicSeries)o;
			return Name.CompareTo(comicSeries.Name);
		}
	}
}
