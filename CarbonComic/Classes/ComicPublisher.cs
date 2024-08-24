using CarbonComic.Properties;
using System.Collections;
using System.IO;

namespace CarbonComic
{
	public class ComicPublisher
	{
		public int ID;
		public string iName;
		public ArrayList Changes = new ArrayList();

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
						throw new InvalidInputException("Invalid publisher name.");
					}
					Query query = CC.SQL.ExecQuery("SELECT * FROM publishers WHERE name='" + SQL.Prepare(value) + "' AND NOT (id = " + ID + ")");
					if (query.NextResult())
					{
						throw new ItemExistsException("There is already a publisher with this name.");
					}
					query.Close();
					iName = value;
					Changes.Add("Name");
				}
			}
		}

		public ComicPublisher(int ID)
		{
			this.ID = ID;
			Query sqlRow = null;
			GetInfo(ref sqlRow);
		}

		public ComicPublisher(ref Query sqlRow)
		{
			GetInfo(ref sqlRow);
		}

		public void GetInfo(ref Query sqlRow)
		{
			if (sqlRow == null)
			{
				sqlRow = CC.SQL.ExecQuery("SELECT id, name FROM publishers WHERE id=" + ID);
				sqlRow.NextResult();
			}
			ID = (int)sqlRow.hash["id"];
			iName = (string)sqlRow.hash["name"];
		}

		//Handle the assignment of groups to publisher, including moving directories
		public void AdoptGroups(ArrayList Groups)
		{
			SQL sQL = new SQL();
			string text = Settings.Default.LibraryDir + "\\" + Name + "\\";
			string text2 = "";
			if (Settings.Default.OrganizeMethod != 0)
			{
				foreach (ComicSeries Group in Groups)
				{
					text2 = Settings.Default.LibraryDir + "\\" + Group.PublisherName + "\\";
					sQL.ExecQuery("UPDATE issues SET filename=" + CC.SQLReplaceLeft("filename", text2, text) + " WHERE series_id=" + Group.ID);
					Directory.Move(text2 + Group.Name + "\\", text + Group.Name + "\\");
				}
			}
			sQL.ExecQuery("UPDATE series SET group_id=" + ID + " WHERE series_id IN (" + string.Join(",", CC.StringList(Groups)) + ")");
		}

		public void Delete(bool Files)
		{
			SQL sQL = new SQL();
			Query query = sQL.ExecQuery("SELECT * FROM groups WHERE pub_id=" + ID);
			while (query.NextResult())
			{
				ComicGroup comicGroup = new ComicGroup((int)query.hash["id"]);
				comicGroup.Delete(Files);
			}
			query.Close();
			sQL.ExecQuery("DELETE FROM publishers WHERE id=" + ID);
			if (Settings.Default.OrganizeMethod != 0)
			{
				try
				{
					Directory.Delete(Path.Combine(Settings.Default.LibraryDir, Name));
				}
				catch
				{
				}
			}
		}

		public bool SaveChanges()
		{
			ArrayList arrayList = new ArrayList();
			if (Changes.Count > 0)
			{
				if (Changes.Contains("Name"))
				{
					arrayList.Add("name='" + SQL.Prepare(iName) + "'");
					Changes.Remove("Name");
				}
				CC.SQL.ExecQuery("UPDATE publishers SET " + string.Join(",", CC.StringList(arrayList)) + " WHERE id=" + ID);
				return true;
			}
			return false;
		}

		public static int GetID(string pubname)
		{
			if (pubname == "")
			{
				pubname = Settings.Default.UnknownPublisher;
			}
			Query query = CC.SQL.ExecQuery("SELECT id FROM publishers WHERE name='" + SQL.Prepare(pubname) + "'");
			if (!query.NextResult())
			{
				query.Close();
				CC.SQL.ExecQuery("INSERT INTO publishers (name) VALUES ('" + SQL.Prepare(pubname) + "')");
				return GetID(pubname);
			}
			int result = (int)query.hash["id"];
			query.Close();
			return result;
		}
	}
}
