using CarbonComic.Properties;
using System;
using System.Collections;
using System.IO;

namespace CarbonComic
{
	public class ComicGroup : IComparable
	{
		public int ID;
		public string iName;
		public ArrayList Changes = new ArrayList();
		public int iPublisherID;
		private int OldPublisherID;
		private string OldName;
		public string PublisherName;

		public int PublisherID
		{
			get
			{
				return iPublisherID;
			}
			set
			{
				if (iPublisherID != value)
				{
					iPublisherID = value;
					Changes.Add("PublisherID");
				}
			}
		}

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
						throw new InvalidInputException("Invalid group name.");
					}
					Query query = CC.SQL.ExecQuery("SELECT * FROM groups WHERE name='" + SQL.Prepare(value) + "' AND NOT (id = " + ID + ")");
					if (query.NextResult())
					{
						throw new ItemExistsException("There is already a group with this name.");
					}
					query.Close();
					OldName = iName;
					iName = value;
					Changes.Add("Name");
				}
			}
		}

		public ComicGroup()
		{
		}

		public ComicGroup(int ID)
		{
			this.ID = ID;
			Query sqlRow = null;
			GetInfo(ref sqlRow);
		}

		public ComicGroup(ref Query sqlRow)
		{
			GetInfo(ref sqlRow);
		}

		public void GetInfo(ref Query sqlRow)
		{
			if (sqlRow == null)
			{
				sqlRow = CC.SQL.ExecQuery("SELECT g.name, g.id, g.pub_id, p.name FROM groups g, publishers p WHERE g.pub_id=p.id AND g.id=" + ID);
				sqlRow.NextResult();
			}
			ID = (int)sqlRow.hash["id"];
			iName = Convert.ToString(sqlRow.hash["g.name"]);
			iPublisherID = (int)sqlRow.hash["pub_id"];
			PublisherName = (string)sqlRow.hash["p.name"];
		}

		public void Delete(bool Files)
		{
			SQL sQL = new SQL();
			Query query = sQL.ExecQuery("SELECT id FROM series WHERE group_id=" + ID);
			while (query.NextResult())
			{
				ComicSeries comicSeries = new ComicSeries((int)query.hash["id"]);
				comicSeries.Delete(Files);
			}
			query.Close();
			sQL.ExecQuery("DELETE FROM groups WHERE id=" + ID);
			if (Settings.Default.OrganizeMethod != 0)
			{
				try
				{
					query = CC.SQL.ExecQuery("SELECT name FROM publishers WHERE id=" + PublisherID);
					string path = (string)query.hash[0];
					query.Close();
					Directory.Delete(Path.Combine(Settings.Default.LibraryDir, Path.Combine(path, Name)));
				}
				catch
				{
				}
			}
		}

		public void AdoptSeries(ArrayList Series)
		{
			SQL sQL = new SQL();
			string text = Settings.Default.LibraryDir + "\\" + CC.URLize(PublisherName) + "\\" + CC.URLize(Name);
			string text2 = "";
			ArrayList arrayList = new ArrayList();
			foreach (ComicSeries item in Series)
			{
				if (Settings.Default.OrganizeMethod != 0)
				{
					text2 = Settings.Default.LibraryDir + "\\" + CC.URLize(item.PublisherName) + "\\" + CC.URLize(item.GroupName);
					sQL.ExecQuery("UPDATE issues SET filename=" + CC.SQLReplaceLeft("filename", text2, text) + " WHERE series_id=" + item.ID);
					try
					{
						Directory.Move(text2 + "\\" + CC.URLize(item.Name), text + "\\" + CC.URLize(item.Name));
					}
					catch
					{
					}
				}
				arrayList.Add(Convert.ToString(item.ID));
				item.GroupID = ID;
				item.GroupName = Name;
			}
			sQL.ExecQuery("UPDATE series SET group_id=" + ID + ", pub_id=" + PublisherID + " WHERE id IN (" + string.Join(",", CC.StringList(arrayList)) + ")");
		}

		public bool SaveChanges()
		{
			ArrayList arrayList = new ArrayList();
			//SQL sQL = new SQL();
			if (Changes.Count > 0)
			{
				if (Changes.Contains("Name"))
				{
					arrayList.Add("name='" + SQL.Prepare(iName) + "'");
				}
				if (Changes.Contains("PublisherID"))
				{
					arrayList.Add("pub_id='" + iPublisherID + "'");
                    CC.SQL.ExecQuery("UPDATE series SET pub_id=" + iPublisherID + " WHERE group_id=" + ID);
				}
				CC.SQL.ExecQuery("UPDATE groups SET " + string.Join(",", CC.StringList(arrayList)) + " WHERE id=" + ID);
				if (Settings.Default.OrganizeMethod != 0)
				{
                    Query query = CC.SQL.ExecQuery("SELECT name FROM publishers WHERE id=" + PublisherID);
                    query.NextResult();

					string name = (string)query.hash[0];
					query.Close();

					if (Changes.Contains("Name"))
					{
						string text = Settings.Default.LibraryDir + "\\" + CC.URLize(name) + "\\" + CC.URLize(OldName) + "\\";
						string text2 = Settings.Default.LibraryDir + "\\" + CC.URLize(name) + "\\" + CC.URLize(Name) + "\\";
                        CC.SQL.ExecQuery("UPDATE issues SET filename=" + CC.SQLReplaceLeft("filename", text, text2));
						CC.Rename(text, text2);
					}
					if (Changes.Contains("PublisherID"))
					{
						query = CC.SQL.ExecQuery("SELECT name FROM publishers WHERE id=" + OldPublisherID);
                        query.NextResult();

						string name2 = (string)query.hash[0];
						query.Close();
						string text = Settings.Default.LibraryDir + "\\" + CC.URLize(name2) + "\\" + CC.URLize(Name) + "\\";
						string text2 = Settings.Default.LibraryDir + "\\" + CC.URLize(name) + "\\" + CC.URLize(Name) + "\\";
                        CC.SQL.ExecQuery("UPDATE issues SET filename=" + CC.SQLReplaceLeft("filename", text, text2));
						CC.Rename(text, text2);
					}
				}
				Changes.Remove("Name");
				Changes.Remove("PublisherID");
				return true;
			}
			return false;
		}

		public static int GetID(string GroupName, int PubID, string PublisherName)
		{
			if (GroupName == "")
			{
				GroupName = PublisherName;
			}
			if (PublisherName == "")
			{
				GroupName = Settings.Default.UnknownGroup;
			}
			Query query = CC.SQL.ExecQuery("SELECT id FROM groups WHERE name='" + SQL.Prepare(GroupName) + "'");
			if (!query.NextResult())
			{
				query.Close();
				CC.SQL.ExecQuery("INSERT INTO groups (name, pub_id) VALUES ('" + SQL.Prepare(GroupName) + "', " + PubID + ")");
				return GetID(GroupName, PubID);
			}
			int result = (int)query.hash["id"];
			query.Close();
			return result;
		}

		public static int GetID(string GroupName, int PubID)
		{
			return GetID(GroupName, PubID, Settings.Default.UnknownPublisher);
		}

		public static int GetID(string GroupName)
		{
			return GetID(GroupName, 0, Settings.Default.UnknownPublisher);
		}

		public int CompareTo(object o)
		{
			ComicGroup comicGroup = (ComicGroup)o;
			return Name.CompareTo(comicGroup.Name);
		}
	}
}
