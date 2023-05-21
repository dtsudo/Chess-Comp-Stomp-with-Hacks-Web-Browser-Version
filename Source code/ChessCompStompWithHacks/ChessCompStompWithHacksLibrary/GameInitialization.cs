
namespace ChessCompStompWithHacksLibrary
{
	using DTLibrary;
	using System;

	public class GameInitialization
	{
		public static IFrame<GameImage, GameFont, GameSound, GameMusic> GetFirstFrame(GlobalState globalState)
		{
			VersionInfo versionInfo = VersionHistory.GetVersionInfo();

			if (versionInfo.Version == "1.02")
				SavedDataMigration_ToV1_02.MigrateAllDataFromOlderVersionsToV1_02IfNeeded(fileIO: globalState.FileIO);
			else
				throw new Exception();

			var frame = new InitialLoadingScreenFrame(globalState: globalState);
			return frame;
		}
	}
}
