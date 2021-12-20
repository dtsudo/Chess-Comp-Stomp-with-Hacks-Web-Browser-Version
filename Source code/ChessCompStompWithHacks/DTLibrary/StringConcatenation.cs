
namespace DTLibrary
{
	public class StringConcatenation
	{
		public static string Concat(string s, int i)
		{
			return s + StringUtil.ToStringCultureInvariant(i);
		}
	}
}
