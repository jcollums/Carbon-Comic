namespace CarbonComic
{
	public class MissingVolumeEventArgs
	{
		public string VolumeName;

		public bool ContinueOperation;

		public MissingVolumeEventArgs(string volumeName)
		{
			VolumeName = volumeName;
		}
	}
}
