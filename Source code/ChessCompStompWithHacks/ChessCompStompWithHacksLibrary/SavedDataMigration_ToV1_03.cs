
namespace ChessCompStompWithHacksLibrary
{
	using DTLibrary;
	using System.Collections.Generic;
	using System.Linq;

	public class SavedDataMigration_ToV1_03
	{
		public static void MigrateAllDataFromOlderVersionsToV1_03IfNeeded(IFileIO fileIO)
		{
			MigrateSessionStateDataFromOlderVersionsToV1_03IfNeeded(fileIO: fileIO);
			MigrateSoundAndMusicVolumeDataFromOlderVersionsToV1_03IfNeeded(fileIO: fileIO);
		}

		public static void MigrateSessionStateDataFromOlderVersionsToV1_03IfNeeded(IFileIO fileIO)
		{
			List<VersionInfo> versionHistory = VersionHistory.GetVersionHistory();

			VersionInfo version1_02 = versionHistory.Single(x => x.Version == "1.02");
			VersionInfo version1_03 = versionHistory.Single(x => x.Version == "1.03");

			ByteList data1_03 = fileIO.FetchData(fileId: GlobalConstants.FILE_ID_FOR_SESSION_STATE, versionInfo: version1_03);

			if (data1_03 != null)
				return;

			SavedDataMigration_ToV1_02.MigrateSessionStateDataFromOlderVersionsToV1_02IfNeeded(fileIO: fileIO);

			ByteList data1_02 = fileIO.FetchData(fileId: GlobalConstants.FILE_ID_FOR_SESSION_STATE, versionInfo: version1_02);

			if (data1_02 != null)
			{
				try
				{
					ByteList.Iterator iterator = data1_02.GetIterator();

					ByteList.Builder listBuilder = new ByteList.Builder();

					listBuilder.AddNullableLong(iterator.TryPopNullableLong());
					listBuilder.AddInt(iterator.TryPopInt());
					listBuilder.AddNullableBool(iterator.TryPopNullableBool());
					listBuilder.AddBool(iterator.TryPopBool());
					listBuilder.AddBool(iterator.TryPopBool());
					listBuilder.AddBool(iterator.TryPopBool());
					listBuilder.AddIntSet(iterator.TryPopIntSet());
					listBuilder.AddIntSet(iterator.TryPopIntSet());
					listBuilder.AddInt(iterator.TryPopInt());
					listBuilder.AddInt(HackSelectionScreenMobileTab.Tactics.GetTabId());

					if (iterator.HasNextByte())
						throw new DTDeserializationException();

					ByteList byteList = listBuilder.ToByteList();

					fileIO.PersistData(fileId: GlobalConstants.FILE_ID_FOR_SESSION_STATE, versionInfo: version1_03, data: byteList);
				}
				catch (DTDeserializationException)
				{
				}
			}
		}

		public static void MigrateSoundAndMusicVolumeDataFromOlderVersionsToV1_03IfNeeded(IFileIO fileIO)
		{
			List<VersionInfo> versionHistory = VersionHistory.GetVersionHistory();

			VersionInfo version1_02 = versionHistory.Single(x => x.Version == "1.02");
			VersionInfo version1_03 = versionHistory.Single(x => x.Version == "1.03");

			ByteList data1_03 = fileIO.FetchData(fileId: GlobalConstants.FILE_ID_FOR_SOUND_AND_MUSIC_VOLUME, versionInfo: version1_03);
			if (data1_03 != null)
				return;

			SavedDataMigration_ToV1_02.MigrateSoundAndMusicVolumeDataFromOlderVersionsToV1_02IfNeeded(fileIO: fileIO);

			ByteList data1_02 = fileIO.FetchData(fileId: GlobalConstants.FILE_ID_FOR_SOUND_AND_MUSIC_VOLUME, versionInfo: version1_02);

			if (data1_02 != null)
				fileIO.PersistData(fileId: GlobalConstants.FILE_ID_FOR_SOUND_AND_MUSIC_VOLUME, versionInfo: version1_03, data: data1_02);
		}
	}
}
