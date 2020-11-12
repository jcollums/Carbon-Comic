namespace CarbonComic
{
	internal class ItemExistsException : LogException
	{
		public ItemExistsException(string message)
			: base(message)
		{
		}
	}
}
