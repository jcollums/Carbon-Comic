using System;
using System.Collections;

namespace CarbonComic
{
	//Handles an issue's assignment to a readlist and its position therein
	public class ReadlistIssue : ComicIssue, IComparable
	{
		public int iPosition;
		public int RowID;
		public int ReadlistID;

		private ArrayList Changes = new ArrayList();

		public int Position
		{
			get
			{
				return iPosition;
			}
			set
			{
				if (iPosition != value)
				{
					iPosition = value;
					Changes.Add("Position");
				}
			}
		}

		public ReadlistIssue(ref Query sqlRow)
			: base(ref sqlRow)
		{
			RowID = (int)sqlRow.hash["ri.id"];
			ReadlistID = (int)sqlRow.hash["list_id"];
			Position = (int)sqlRow.hash["pos"];
		}

		public ReadlistIssue(int ReadlistID, int RowID, int ID, int pos)
			: base(ID)
		{
			this.RowID = RowID;
			this.ReadlistID = ReadlistID;
			Position = pos;
		}

		public new bool SaveChanges()
		{
			base.SaveChanges();
			if (Changes.Count > 0)
			{
				ArrayList arrayList = new ArrayList();
				if (Changes.Contains("Position"))
				{
					arrayList.Add("pos='" + iPosition + "'");
					Changes.Remove("Position");
				}
				CC.SQL.ExecQuery("UPDATE readlist_issues SET " + string.Join(",", CC.StringList(arrayList)) + " WHERE id=" + RowID);
				return true;
			}
			return false;
		}

		public new int CompareTo(object o)
		{
			ReadlistIssue readlistIssue = (ReadlistIssue)o;
			return Position.CompareTo(readlistIssue.Position);
		}
	}
}
