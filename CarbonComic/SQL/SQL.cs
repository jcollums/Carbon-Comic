using CarbonComic.Properties;
using System;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;

namespace CarbonComic
{
	public class SQL
	{
		private string database;

		private OleDbConnection conn;

		public Exception error;

		public SQL()
		{
			database = Path.Combine(Application.StartupPath, Settings.Default.DatabaseFile);
			conn = Connect();
		}

		public SQL(string database)
		{
			this.database = database;
			conn = Connect();
		}

		public OleDbConnection Connect()
		{
			try
			{
                OleDbConnection dbcon = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=\"CarbonComic\\CarbonLib.mdb\"");
				dbcon.Open();
				dbcon.Close();
				return dbcon;
			}
			catch (Exception ex)
			{
				Exception ex2 = error = ex;
                MessageBox.Show(ex.Message);
				return null;
			}
		}

		public Query ExecQuery(string query)
		{
			if (conn == null)
			{
				conn.Open();
			}
			OleDbCommand query2 = new OleDbCommand(query, conn);
			bool q = query.Substring(0, 6) == "SELECT";
			return new Query(conn, query2, q);
		}

		public static string Prepare(string query)
		{
			if (query == null)
			{
				query = "";
			}
			return query.Replace("'", "''");
		}
	}
}
