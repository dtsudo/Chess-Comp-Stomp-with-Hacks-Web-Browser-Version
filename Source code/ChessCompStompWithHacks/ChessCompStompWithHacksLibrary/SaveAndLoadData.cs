
namespace ChessCompStompWithHacksLibrary
{
	using DTLibrary;
	using System.Collections.Generic;

	public class SaveAndLoadData
	{
		private ByteList sessionStateByteList;
		private int? soundVolume;
		private int? musicVolume;

		private IFileIO fileIO;

		public SaveAndLoadData(IFileIO fileIO)
		{
			this.fileIO = fileIO;

			this.sessionStateByteList = null;
			this.soundVolume = null;
			this.musicVolume = null;
		}

		public void SaveData(SessionState sessionState, int soundVolume, int musicVolume)
		{
			this.SaveSessionState(sessionState: sessionState);

			this.SaveSoundAndMusicVolume(soundVolume: soundVolume, musicVolume: musicVolume);
		}

		private void SaveSoundAndMusicVolume(int soundVolume, int musicVolume)
		{
			if (this.soundVolume.HasValue && this.musicVolume.HasValue && this.soundVolume.Value == soundVolume && this.musicVolume.Value == musicVolume)
				return;

			this.soundVolume = soundVolume;
			this.musicVolume = musicVolume;

			ByteList.Builder listBuilder = new ByteList.Builder();

			listBuilder.AddInt(soundVolume);
			listBuilder.AddInt(musicVolume);
			
			this.fileIO.PersistData(fileId: ChessCompStompWithHacks.FILE_ID_FOR_SOUND_AND_MUSIC_VOLUME, data: listBuilder.ToByteList());
		}

		private void SaveSessionState(SessionState sessionState)
		{
			ByteList.Builder listBuilder = new ByteList.Builder();
			sessionState.SerializeEverythingExceptGameLogic(list: listBuilder);

			ByteList byteList = listBuilder.ToByteList();

			if (this.sessionStateByteList != null && this.sessionStateByteList.Equals(byteList))
				return;

			this.sessionStateByteList = byteList;

			this.fileIO.PersistData(fileId: ChessCompStompWithHacks.FILE_ID_FOR_SESSION_STATE, data: this.sessionStateByteList);
		}

		public void LoadSessionState(SessionState sessionState)
		{
			ByteList list = this.fileIO.FetchData(fileId: ChessCompStompWithHacks.FILE_ID_FOR_SESSION_STATE);

			if (list == null)
			{
				sessionState.ClearData();
				return;
			}
			
			try
			{
				ByteList.Iterator iterator = list.GetIterator();
				sessionState.TryDeserializeEverythingExceptGameLogic(iterator: iterator);

				if (iterator.HasNextByte())
					throw new DTDeserializationException();
			}
			catch (DTDeserializationException)
			{
				sessionState.ClearData();
			}
		}

		public int? LoadSoundVolume()
		{
			ByteList list = this.fileIO.FetchData(fileId: ChessCompStompWithHacks.FILE_ID_FOR_SOUND_AND_MUSIC_VOLUME);

			if (list == null)
				return null;
			
			try
			{
				ByteList.Iterator iterator = list.GetIterator();
				int soundVolume = iterator.TryPopInt();
				iterator.TryPopInt();
				if (iterator.HasNextByte())
					throw new DTDeserializationException();
				
				if (soundVolume >= 0 && soundVolume <= 100)
					return soundVolume;
				return null;
			}
			catch (DTDeserializationException)
			{
				return null;
			}
		}

		public int? LoadMusicVolume()
		{
			ByteList list = this.fileIO.FetchData(fileId: ChessCompStompWithHacks.FILE_ID_FOR_SOUND_AND_MUSIC_VOLUME);

			if (list == null)
				return null;
			
			try
			{
				ByteList.Iterator iterator = list.GetIterator();

				iterator.TryPopInt();
				int musicVolume = iterator.TryPopInt();
				if (iterator.HasNextByte())
					throw new DTDeserializationException();
				
				if (musicVolume >= 0 && musicVolume <= 100)
					return musicVolume;
				return null;
			}
			catch (DTDeserializationException)
			{
				return null;
			}
		}
	}
}
