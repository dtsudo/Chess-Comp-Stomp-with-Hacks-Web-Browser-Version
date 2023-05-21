
namespace ChessCompStompWithHacksLibrary
{
	using DTLibrary;

	public class SavedDataMigration_ToV1_01
	{
		public static void MigrateSessionStateDataFromOlderVersionsToV1_01IfNeeded(IFileIO fileIO)
		{
			// v1.00 didn't persist any data (the player had to beat the game in a single session),
			// so there is nothing to migrate
		}

		public static void MigrateSoundAndMusicVolumeDataFromOlderVersionsToV1_01IfNeeded(IFileIO fileIO)
		{
			// Nothing to migrate
		}
	}
}
