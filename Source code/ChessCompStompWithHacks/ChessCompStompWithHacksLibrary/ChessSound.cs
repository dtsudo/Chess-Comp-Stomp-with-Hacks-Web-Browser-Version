
namespace ChessCompStompWithHacksLibrary
{
	using System;

	public enum ChessSound
	{
	}

	public static class ChessSoundUtil
	{
		public static string GetSoundFilename(this ChessSound sound)
		{
			switch (sound)
			{
				default: throw new Exception();
			}
		}

		// From 0 to 100 (both inclusive)
		public static int GetSoundVolume(this ChessSound sound)
		{
			switch (sound)
			{
				default: throw new Exception();
			}
		}
	}
}
