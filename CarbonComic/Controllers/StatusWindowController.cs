//This handles the operation of the progress bars on all windows

using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace CarbonComic
{
	public class StatusWindowController
	{
		private Label WindowLabel;

		private ProgressBar ProgressBAR;

		private PictureBox SwitchButton;

		private PictureBox StopButton;

		private ArrayList Windows = new ArrayList();

		private int Current;

		/// <summary>
		/// Initialize the controller with specific form elements
		/// </summary>
		/// <param name="Window">The label element that will show the name of the task</param>
		/// <param name="Progress">The progress bar element that will show the progress of the task</param>
		/// <param name="Switch">The image button that will serve to switch between multiple tasks</param>
		/// <param name="Stop">The image button that will stop the current task</param>
		public StatusWindowController(Label Window, ProgressBar Progress, PictureBox Switch, PictureBox Stop)
		{
			WindowLabel = Window;
			ProgressBAR = Progress;
			SwitchButton = Switch;
			StopButton = Stop;
		}

		public void setProgress(int index, int val)
		{
			StatusItems statusItems = (StatusItems)Windows[index];
			statusItems.Progress = val;
			Windows[index] = statusItems;
		}

		//???
		public void setProgressStyle(int index, ProgressBarStyle val)
		{
			StatusItems statusItems = (StatusItems)Windows[index];
			statusItems.ProgressStyle = val;
			Windows[index] = statusItems;
		}

		//update the name of a particular task
		public void setTask(int index, string val)
		{
			StatusItems statusItems = (StatusItems)Windows[index];
			statusItems.Task = val;
			Windows[index] = statusItems;
		}

		//???
		public void setShowStop(int index, bool val)
		{
			StatusItems statusItems = (StatusItems)Windows[index];
			statusItems.ShowStop = val;
			Windows[index] = statusItems;
		}

		public void setShowProgress(int index, bool val)
		{
			StatusItems statusItems = (StatusItems)Windows[index];
			statusItems.ShowProgress = val;
			Windows[index] = statusItems;
		}

		public void setDetails(int index, string val)
		{
			StatusItems statusItems = (StatusItems)Windows[index];
			statusItems.Details = val;
			Windows[index] = statusItems;
		}

		public void setIcon(int index, Image val)
		{
			StatusItems statusItems = (StatusItems)Windows[index];
			statusItems.Icon = val;
			Windows[index] = statusItems;
		}

		public void setEventStop(int index, EventHandler val)
		{
			StatusItems statusItems = (StatusItems)Windows[index];
			statusItems.EventStop = val;
			Windows[index] = statusItems;
		}

		public int Add(StatusItems items)
		{
			Windows.Add(items);
			SwitchButton.Visible = (Windows.Count > 1);
			Next();
			return Windows.Count - 1;
		}

		public void Next()
		{
			if (++Current > Windows.Count - 1)
			{
				Current = 0;
			}
			Refresh();
		}

		public void SwitchTo(int index)
		{
			Current = index;
			Refresh();
		}

		public void Refresh(int index)
		{
			if (index == Current)
			{
				StatusItems statusItems = (StatusItems)Windows[index];
				WindowLabel.Image = statusItems.Icon;
				WindowLabel.Text = statusItems.Task + Environment.NewLine + statusItems.Details;
				ProgressBAR.Visible = statusItems.ShowProgress;
				ProgressBAR.Style = statusItems.ProgressStyle;
				ProgressBAR.Value = statusItems.Progress;
				StopButton.Visible = statusItems.ShowStop;
				StopButton.Click += statusItems.EventStop;
			}
		}

		public void Refresh()
		{
			Refresh(Current);
		}

		public void Remove(int index)
		{
			Windows.RemoveAt(index);
			SwitchButton.Visible = (Windows.Count > 1);
			Next();
		}
	}
}
