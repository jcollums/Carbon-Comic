namespace CarbonComic
{
	internal class InvalidInputException : LogException
	{
		public InvalidInputException(string message)
			: base(message)
		{
		}
	}
}
