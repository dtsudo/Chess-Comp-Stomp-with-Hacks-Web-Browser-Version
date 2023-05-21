
namespace ChessCompStompWithHacksLibrary
{
	using DTLibrary;
	using System.Collections.Generic;
	using System.Linq;

	public class SavedDataMigration_ToV1_02
	{
		public static void MigrateAllDataFromOlderVersionsToV1_02IfNeeded(IFileIO fileIO)
		{
			MigrateSessionStateDataFromOlderVersionsToV1_02IfNeeded(fileIO: fileIO);
			MigrateSoundAndMusicVolumeDataFromOlderVersionsToV1_02IfNeeded(fileIO: fileIO);
		}

		public static void MigrateSessionStateDataFromOlderVersionsToV1_02IfNeeded(IFileIO fileIO)
		{
			List<VersionInfo> versionHistory = VersionHistory.GetVersionHistory();

			VersionInfo version1_01 = versionHistory.Single(x => x.Version == "1.01");
			VersionInfo version1_02 = versionHistory.Single(x => x.Version == "1.02");

			ByteList data1_02 = fileIO.FetchData(fileId: GlobalConstants.FILE_ID_FOR_SESSION_STATE, versionInfo: version1_02);
			if (data1_02 != null)
				return;

			SavedDataMigration_ToV1_01.MigrateSessionStateDataFromOlderVersionsToV1_01IfNeeded(fileIO: fileIO);

			ByteList data1_01 = fileIO.FetchData(fileId: GlobalConstants.FILE_ID_FOR_SESSION_STATE, versionInfo: version1_01);

			// The session state save format hasn't changed from 1.01 to 1.02
			if (data1_01 != null)
				fileIO.PersistData(fileId: GlobalConstants.FILE_ID_FOR_SESSION_STATE, versionInfo: version1_02, data: data1_01);
		}

		public static void MigrateSoundAndMusicVolumeDataFromOlderVersionsToV1_02IfNeeded(IFileIO fileIO)
		{
			List<VersionInfo> versionHistory = VersionHistory.GetVersionHistory();

			VersionInfo version1_01 = versionHistory.Single(x => x.Version == "1.01");
			VersionInfo version1_02 = versionHistory.Single(x => x.Version == "1.02");

			ByteList data1_02 = fileIO.FetchData(fileId: GlobalConstants.FILE_ID_FOR_SOUND_AND_MUSIC_VOLUME, versionInfo: version1_02);
			if (data1_02 != null)
				return;

			SavedDataMigration_ToV1_01.MigrateSoundAndMusicVolumeDataFromOlderVersionsToV1_01IfNeeded(fileIO: fileIO);

			ByteList data1_01 = fileIO.FetchData(fileId: GlobalConstants.FILE_ID_FOR_SOUND_AND_MUSIC_VOLUME, versionInfo: version1_01);

			if (data1_01 != null)
				fileIO.PersistData(fileId: GlobalConstants.FILE_ID_FOR_SOUND_AND_MUSIC_VOLUME, versionInfo: version1_02, data: data1_01);
		}
	}
}
