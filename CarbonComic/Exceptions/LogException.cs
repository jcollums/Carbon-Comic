using System;
using System.Collections;

namespace CarbonComic
{
	internal class LogException : ApplicationException
	{
		public static ArrayList ErrList = new ArrayList();

		public LogException(string message)
			: base(message)
		{
			ErrList.Add(ToString());
		}
	}
}
