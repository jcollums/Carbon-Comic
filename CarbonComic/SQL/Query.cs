using System;
using System.Data;
using System.Data.OleDb;

namespace CarbonComic
{
	public class Query
	{
		public OleDbDataReader hash;

		private OleDbConnection conn;

		public Exception error;

		public int affected;

		public Query(OleDbConnection conn, OleDbCommand query, bool q)
		{
			this.conn = conn;
			if (conn.State == ConnectionState.Closed)
			{
				conn.Open();
			}
			if (q)
			{
				try
				{
					hash = query.ExecuteReader();
				}
				catch (Exception ex)
				{
					hash = null;
					error = ex;
				}
			}
			else
			{
				try
				{
					affected = query.ExecuteNonQuery();
				}
				catch (Exception ex2)
				{
					Exception ex3 = error = ex2;
				}
				conn.Close();
			}
		}

		public bool NextResult()
		{
			return hash.Read();
		}

		public void Close()
		{
			hash.Close();
			conn.Close();
		}
	}
}
