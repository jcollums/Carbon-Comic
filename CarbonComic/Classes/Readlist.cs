using System;
using System.Collections;

namespace CarbonComic
{
    //Handles Readlists and maintains a list of its issues
	public class Readlist
	{
		public int ID;
		public string iName;
		public bool Smart;  //For the unimplemented Smart Readlist feature
		private ArrayList DeleteQueue = new ArrayList();    //List of issues to delete
		private ArrayList Changes = new ArrayList();        //List of changes to save
		public ArrayList Issues = new ArrayList();          //List of issues in Readlist

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

		public Readlist(int ID)
		{
			this.ID = ID;
			Query sqlRow = null;
			GetInfo(ref sqlRow);
		}

		public Readlist(ref Query sqlRow)
		{
			GetInfo(ref sqlRow);
		}

		public void DoDelete()
		{
			foreach (ReadlistIssue item in DeleteQueue)
			{
				Issues.Remove(item);
			}
			DeleteQueue.Clear();
		}

		public void GetInfo(ref Query sqlRow)
		{
			if (sqlRow == null)
			{
				sqlRow = CC.SQL.ExecQuery("SELECT * FROM readlists WHERE id=" + ID);
				sqlRow.NextResult();
			}
			ID = (int)sqlRow.hash["id"];
			Smart = Convert.ToBoolean(sqlRow.hash["smart"]);
			iName = Convert.ToString(sqlRow.hash["name"]);
			GetIssues();
		}

		public void GetIssues()
		{
			Issues.Clear();
			Query sqlRow = CC.SQL.ExecQuery("SELECT i.*, s.*, p.name, g.name, ri.pos, ri.id, ri.list_id FROM readlist_issues ri, issues i, series s, groups g, publishers p WHERE i.id=ri.issue_id AND i.series_id=s.id AND s.pub_id=p.id AND s.group_id=g.id AND i.id IN (SELECT issue_id FROM readlist_issues WHERE list_id=" + ID + ") ORDER BY pos");
			while (sqlRow.NextResult())
			{
				Issues.Add(new ReadlistIssue(ref sqlRow));
			}
		}

		public void DeleteIssue(int index)
		{
			ReadlistIssue readlistIssue = (ReadlistIssue)Issues[index];
			CC.SQL.ExecQuery("DELETE FROM readlist_issues WHERE id=" + readlistIssue.RowID);
			DeleteQueue.Add(readlistIssue);
			correctPositions();
			SaveChanges();
		}

		public void correctPositions()
		{
			int num = 0;
			foreach (ReadlistIssue issue in Issues)
			{
				num = (issue.Position = num + 1);
			}
		}

		public void AddIssue(int ID)
		{
			int num = 0;
			num = ((Issues.Count == 0) ? 1 : (Issues.Count + 1));
			CC.SQL.ExecQuery("INSERT INTO readlist_issues (list_id, issue_id, pos) VALUES (" + this.ID + ", " + ID + ", " + num + ")");
			Query query = CC.SQL.ExecQuery("SELECT id FROM readlist_issues WHERE list_id=" + this.ID + " AND issue_id=" + ID);
			query.NextResult();
			int num2 = (int)query.hash["id"];
			Issues.Add(true);
			query.Close();
		}

        /// <summary>
        /// Change the position of an issue within the readlist, shifting surrounding issues automatically
        /// </summary>
        /// <param name="index">Issue to move</param>
        /// <param name="pos">New position</param>
		public void setPosition(int index, int pos)
		{
			ReadlistIssue readlistIssue = (ReadlistIssue)Issues[index];
			for (int i = 0; i < Issues.Count; i++)
			{
				if (i != index)
				{
					ReadlistIssue readlistIssue2 = (ReadlistIssue)Issues[i];
					if (readlistIssue2.Position < readlistIssue.Position)
					{
						if (readlistIssue2.Position >= pos && pos < readlistIssue.Position)
						{
							readlistIssue2.Position++;
						}
					}
					else if (readlistIssue2.Position > readlistIssue.Position && readlistIssue2.Position <= pos && pos > readlistIssue.Position)
					{
						readlistIssue2.Position--;
					}
				}
			}
			readlistIssue.Position = pos;
			Changes.Add("Issues");
		}

        //Unimplemented Smart Readlist feature
		public string SmartSQL()
		{
			if (Smart)
			{
				ArrayList arrayList = new ArrayList();
				SQL sQL = new SQL();
				Query query = sQL.ExecQuery("SELECT * FROM readlist_smart WHERE list_id=" + ID);
				while (query.NextResult())
				{
					arrayList.Add((string)query.hash["prop"] + " " + (string)query.hash["op"] + " " + (string)query.hash["val"]);
				}
				return string.Join(" AND ", CC.StringList(arrayList));
			}
			return "";
		}

        //Save changes if necessary
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
				CC.SQL.ExecQuery("UPDATE readlists SET " + string.Join(", ", CC.StringList(arrayList)) + " WHERE id=" + ID);
				if (Changes.Contains("Issues"))
				{
					foreach (ReadlistIssue issue in Issues)
					{
						issue.SaveChanges();
					}
				}
				return true;
			}
			return false;
		}

        //Delete both readlist and its association with issues
		public void Delete()
		{
			CC.SQL.ExecQuery("DELETE FROM readlist_issues WHERE list_id=" + ID);
			CC.SQL.ExecQuery("DELETE FROM readlists WHERE id=" + ID);
		}

        //Get ID of Readlist based on name
		public static int GetID(string name)
		{
			return GetID(name, false);
		}

        //Get ID of Readlist based on name, create it if it doesn't already exist
		public static int GetID(string name, bool create)
		{
			int result = -1;
			Query query = CC.SQL.ExecQuery("SELECT id FROM readlists WHERE name='" + name + "'");
			if (!query.NextResult())
			{
				query.Close();
				if (create)
				{
					CC.SQL.ExecQuery("INSERT INTO readlists (name) VALUES ('" + SQL.Prepare(name) + "')");
					return GetID(name);
				}
			}
			else
			{
				result = (int)query.hash["id"];
				query.Close();
			}
			return result;
		}
	}
}
