namespace E_CommerceASP.Helpers
{
	public static class CapitalizeHelper
	{
		public static string Capitalize(string text)
		{
			text = text.ToLower();

			var firstLetter = text.Substring(0, 1);
			var remainLetters = text.Substring(1);

			firstLetter = firstLetter.ToUpper();

			return firstLetter + remainLetters;
		}
	}
}
