
namespace ChessCompStompWithHacksLibrary
{
	using System;

	public enum GameSound
	{
		PlayerMove,
		AIMove,
		Win,
		StalemateOrDefeat,
		NukeLaunch,
		NukeExplosion,
		Click,
		Woosh
	}

	public static class GameSoundUtil
	{
		public class SoundFilenameInfo
		{
			public SoundFilenameInfo(string oggFilename, string flacFilename)
			{
				this.OggFilename = oggFilename;
				this.FlacFilename = flacFilename;
			}

			public string OggFilename { get; private set; }
			public string FlacFilename { get; private set; }
		}

		public static SoundFilenameInfo GetSoundFilename(this GameSound sound)
		{
			switch (sound)
			{
				case GameSound.PlayerMove: return new SoundFilenameInfo(oggFilename: "Kenney/jingles_STEEL16.ogg", flacFilename: "Kenney/jingles_STEEL16.flac");
				case GameSound.AIMove: return new SoundFilenameInfo(oggFilename: "Kenney/jingles_STEEL00.ogg", flacFilename: "Kenney/jingles_STEEL00.flac");
				case GameSound.Win: return new SoundFilenameInfo(oggFilename: "Kenney/jingles_STEEL10.ogg", flacFilename: "Kenney/jingles_STEEL10.flac");
				case GameSound.StalemateOrDefeat: return new SoundFilenameInfo(oggFilename: "Kenney/jingles_STEEL14.ogg", flacFilename: "Kenney/jingles_STEEL14.flac");
				case GameSound.NukeLaunch: return new SoundFilenameInfo(oggFilename: "Kenney/rumble3.ogg", flacFilename: "Kenney/rumble3.flac");
				case GameSound.NukeExplosion: return new SoundFilenameInfo(oggFilename: "Kenney/rumble1.ogg", flacFilename: "Kenney/rumble1.flac");
				case GameSound.Click: return new SoundFilenameInfo(oggFilename: "Kenney/click3_Modified.ogg", flacFilename: "Kenney/click3_Modified.flac");
				case GameSound.Woosh: return new SoundFilenameInfo(oggFilename: "Kenney/woosh2_Modified.ogg", flacFilename: "Kenney/woosh2_Modified.flac");
				default: throw new Exception();
			}
		}

		// From 0 to 100 (both inclusive)
		public static int GetSoundVolume(this GameSound sound)
		{
			switch (sound)
			{
				case GameSound.PlayerMove: return 10;
				case GameSound.AIMove: return 10;
				case GameSound.Win: return 10;
				case GameSound.StalemateOrDefeat: return 10;
				case GameSound.NukeLaunch: return 20;
				case GameSound.NukeExplosion: return 20;
				case GameSound.Click: return 30;
				case GameSound.Woosh: return 20;
				default: throw new Exception();
			}
		}
	}
}
