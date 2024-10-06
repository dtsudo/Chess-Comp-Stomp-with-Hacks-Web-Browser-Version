
namespace ChessCompStompWithHacksLibrary
{
	using System;
	using System.Collections.Generic;

	public enum GameMusic
	{
		TitleScreen,
		Level1,
		Level2,
		Level3,
		Ending
	}

	public static class GameMusicUtil
	{
		public static GameMusic GetGameMusic(ColorTheme colorTheme)
		{
			switch (colorTheme)
			{
				case ColorTheme.Initial: return GameMusic.Level1;
				case ColorTheme.Progress1: return GameMusic.Level2;
				case ColorTheme.Progress2: return GameMusic.Level2;
				case ColorTheme.Progress3: return GameMusic.Level2;
				case ColorTheme.Final: return GameMusic.Level3;
				default: throw new Exception();
			}
		}
		
		public class MusicFilenameInfo
		{
			public MusicFilenameInfo(string oggFilename, string flacFilename)
			{
				this.OggFilename = oggFilename;
				this.FlacFilename = flacFilename;
			}

			public string OggFilename { get; private set; }
			public string FlacFilename { get; private set; }
		}

		public static MusicFilenameInfo GetMusicFilename(this GameMusic music)
		{
			switch (music)
			{
				case GameMusic.TitleScreen:
					return new MusicFilenameInfo(
						oggFilename: "JuhaniJunkala/TitleScreen.ogg",
						flacFilename: "JuhaniJunkala/TitleScreen.flac");
				case GameMusic.Level1:
					return new MusicFilenameInfo(
						oggFilename: "JuhaniJunkala/Level1.ogg",
						flacFilename: "JuhaniJunkala/Level1.flac");
				case GameMusic.Level2:
					return new MusicFilenameInfo(
						oggFilename: "JuhaniJunkala/Level2.ogg",
						flacFilename: "JuhaniJunkala/Level2.flac");
				case GameMusic.Level3: 
					return new MusicFilenameInfo(
						oggFilename: "JuhaniJunkala/Level3.ogg",
						flacFilename: "JuhaniJunkala/Level3.flac");
				case GameMusic.Ending: 
					return new MusicFilenameInfo(
						oggFilename: "JuhaniJunkala/Ending.ogg",
						flacFilename: "JuhaniJunkala/Ending.flac");
				default: throw new Exception();
			}
		}

		// From 0 to 100 (both inclusive)
		public static int GetMusicVolume(this GameMusic music)
		{
			switch (music)
			{
				case GameMusic.TitleScreen: return 7;
				case GameMusic.Level1: return 7;
				case GameMusic.Level2: return 7;
				case GameMusic.Level3: return 7;
				case GameMusic.Ending: return 10;
				default: throw new Exception();
			}
		}
	}
}
