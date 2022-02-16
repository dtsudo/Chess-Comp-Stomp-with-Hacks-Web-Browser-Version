
namespace ChessCompStompWithHacksLibrary
{
	using System;

	public enum ChessSound
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

	public static class ChessSoundUtil
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

		public static SoundFilenameInfo GetSoundFilename(this ChessSound sound)
		{
			switch (sound)
			{
				case ChessSound.PlayerMove: return new SoundFilenameInfo(defaultFilename: "Kenney/jingles_STEEL16.ogg", wavFilename: "Kenney/jingles_STEEL16.wav");
				case ChessSound.AIMove: return new SoundFilenameInfo(defaultFilename: "Kenney/jingles_STEEL00.ogg", wavFilename: "Kenney/jingles_STEEL00.wav");
				case ChessSound.Win: return new SoundFilenameInfo(defaultFilename: "Kenney/jingles_STEEL10.ogg", wavFilename: "Kenney/jingles_STEEL10.wav");
				case ChessSound.StalemateOrDefeat: return new SoundFilenameInfo(defaultFilename: "Kenney/jingles_STEEL14.ogg", wavFilename: "Kenney/jingles_STEEL14.wav");
				case ChessSound.NukeLaunch: return new SoundFilenameInfo(defaultFilename: "Kenney/rumble3.ogg", wavFilename: "Kenney/rumble3.wav");
				case ChessSound.NukeExplosion: return new SoundFilenameInfo(defaultFilename: "Kenney/rumble1.ogg", wavFilename: "Kenney/rumble1.wav");
				case ChessSound.Click: return new SoundFilenameInfo(defaultFilename: "Kenney/click3_Modified.wav", wavFilename: "Kenney/click3_Modified.wav");
				case ChessSound.Woosh: return new SoundFilenameInfo(defaultFilename: "Kenney/woosh2_Modified.wav", wavFilename: "Kenney/woosh2_Modified.wav");
				default: throw new Exception();
			}
		}

		// From 0 to 100 (both inclusive)
		public static int GetSoundVolume(this ChessSound sound)
		{
			switch (sound)
			{
				case ChessSound.PlayerMove: return 10;
				case ChessSound.AIMove: return 10;
				case ChessSound.Win: return 10;
				case ChessSound.StalemateOrDefeat: return 10;
				case ChessSound.NukeLaunch: return 20;
				case ChessSound.NukeExplosion: return 20;
				case ChessSound.Click: return 30;
				case ChessSound.Woosh: return 20;
				default: throw new Exception();
			}
		}
	}
}
