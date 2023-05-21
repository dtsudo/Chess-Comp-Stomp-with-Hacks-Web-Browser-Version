
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
			public SoundFilenameInfo(string defaultFilename, string wavFilename)
			{
				this.DefaultFilename = defaultFilename;
				this.WavFilename = wavFilename;
			}

			public string DefaultFilename { get; private set; }
			public string WavFilename { get; private set; }
		}

		public static SoundFilenameInfo GetSoundFilename(this GameSound sound)
		{
			switch (sound)
			{
				case GameSound.PlayerMove: return new SoundFilenameInfo(defaultFilename: "Kenney/jingles_STEEL16.ogg", wavFilename: "Kenney/jingles_STEEL16.wav");
				case GameSound.AIMove: return new SoundFilenameInfo(defaultFilename: "Kenney/jingles_STEEL00.ogg", wavFilename: "Kenney/jingles_STEEL00.wav");
				case GameSound.Win: return new SoundFilenameInfo(defaultFilename: "Kenney/jingles_STEEL10.ogg", wavFilename: "Kenney/jingles_STEEL10.wav");
				case GameSound.StalemateOrDefeat: return new SoundFilenameInfo(defaultFilename: "Kenney/jingles_STEEL14.ogg", wavFilename: "Kenney/jingles_STEEL14.wav");
				case GameSound.NukeLaunch: return new SoundFilenameInfo(defaultFilename: "Kenney/rumble3.ogg", wavFilename: "Kenney/rumble3.wav");
				case GameSound.NukeExplosion: return new SoundFilenameInfo(defaultFilename: "Kenney/rumble1.ogg", wavFilename: "Kenney/rumble1.wav");
				case GameSound.Click: return new SoundFilenameInfo(defaultFilename: "Kenney/click3_Modified.wav", wavFilename: "Kenney/click3_Modified.wav");
				case GameSound.Woosh: return new SoundFilenameInfo(defaultFilename: "Kenney/woosh2_Modified.wav", wavFilename: "Kenney/woosh2_Modified.wav");
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
