
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
			public MusicFilenameInfo(string defaultFilename, string wavFilename)
			{
				this.DefaultFilename = defaultFilename;
				this.WavFilename = wavFilename;
			}

			public string DefaultFilename { get; private set; }
			public string WavFilename { get; private set; }
		}

		public static MusicFilenameInfo GetMusicFilename(this GameMusic music)
		{
			switch (music)
			{
				case GameMusic.TitleScreen:
					return new MusicFilenameInfo(
						defaultFilename: "JuhaniJunkala/TitleScreen.ogg",
						wavFilename: "JuhaniJunkala/TitleScreen.wav");
				case GameMusic.Level1:
					return new MusicFilenameInfo(
						defaultFilename: "JuhaniJunkala/Level1.ogg",
						wavFilename: "JuhaniJunkala/Level1.wav");
				case GameMusic.Level2:
					return new MusicFilenameInfo(
						defaultFilename: "JuhaniJunkala/Level2.ogg",
						wavFilename: "JuhaniJunkala/Level2.wav");
				case GameMusic.Level3: 
					return new MusicFilenameInfo(
						defaultFilename: "JuhaniJunkala/Level3.ogg",
						wavFilename: "JuhaniJunkala/Level3.wav");
				case GameMusic.Ending: 
					return new MusicFilenameInfo(
						defaultFilename: "JuhaniJunkala/Ending.ogg",
						wavFilename: "JuhaniJunkala/Ending.wav");
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
