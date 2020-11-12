using System;
using System.Drawing;
using System.Windows.Forms;

namespace CarbonComic
{
	public struct StatusItems
	{
		public Image Icon;

		public string Task;

		public string Details;

		public bool ShowProgress;

		public int Progress;

		public ProgressBarStyle ProgressStyle;

		public bool ShowStop;

		public EventHandler EventStop;
	}
}
