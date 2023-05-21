
namespace ChessCompStompWithHacksLibrary
{
	using DTLibrary;
	using System.Collections.Generic;

	public class VersionHistory
	{
		public static VersionInfo GetVersionInfo()
		{
			List<VersionInfo> versionHistory = GetVersionHistory();

			return versionHistory[versionHistory.Count - 1];
		}

		public static List<VersionInfo> GetVersionHistory()
		{
			List<VersionInfo> list = new List<VersionInfo>();

			list.Add(new VersionInfo(version: "1.00", alphanumericVersionGuid: "9958487281526502"));
			list.Add(new VersionInfo(version: "1.01", alphanumericVersionGuid: "4655654740627213"));
			list.Add(new VersionInfo(version: "1.02", alphanumericVersionGuid: "5737942911566923"));

			return list;
		}

		/// <summary>
		/// Returns a guid that doesn't change between versions, but is unique to this game
		/// and isn't used by other games.
		/// </summary>
		public static string GetGuidForGame()
		{
			return "3631295139845259";
		}
	}
}
