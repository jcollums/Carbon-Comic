namespace CarbonComic
{
	public class NewFileEventArgs
	{
		public RARFileInfo fileInfo;

		public NewFileEventArgs(RARFileInfo fileInfo)
		{
			this.fileInfo = fileInfo;
		}
	}
}
