
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using System;

	public enum GameImage
	{
		SoundOn,
		SoundOff,
		MusicOn,
		MusicOff,
		Gear,
		GearHover,
		GearSelected,
		Cross,
		CrossHover,
		CrossSelected,
		Down,
		Up,

		BlackPawn,
		BlackRook,
		BlackKnight,
		BlackBishop,
		BlackQueen,
		BlackKing,

		WhitePawn,
		WhiteRook,
		WhiteKnight,
		WhiteBishop,
		WhiteQueen,
		WhiteKing,

		Nuke_NotReady,
		Nuke_Ready,
		Nuke_Hover,
		Nuke_Selected,
		Nuke_RocketFire,
		
		Nuke_Explosion1,
		Nuke_Explosion2,
		Nuke_Explosion3,
		Nuke_Explosion4,
		Nuke_Explosion5,
		Nuke_Explosion6,
		Nuke_Explosion7,
		Nuke_Explosion8,
		Nuke_Explosion9
	}

	public static class GameImageUtil
	{
		public const int ChessPieceScalingFactor = 128 / 8;

		public static GameImage GetImage(ChessSquarePiece piece)
		{
			switch (piece)
			{
				case ChessSquarePiece.BlackPawn: return GameImage.BlackPawn;
				case ChessSquarePiece.BlackKnight: return GameImage.BlackKnight;
				case ChessSquarePiece.BlackBishop: return GameImage.BlackBishop;
				case ChessSquarePiece.BlackRook: return GameImage.BlackRook;
				case ChessSquarePiece.BlackQueen: return GameImage.BlackQueen;
				case ChessSquarePiece.BlackKing: return GameImage.BlackKing;

				case ChessSquarePiece.WhitePawn: return GameImage.WhitePawn;
				case ChessSquarePiece.WhiteKnight: return GameImage.WhiteKnight;
				case ChessSquarePiece.WhiteBishop: return GameImage.WhiteBishop;
				case ChessSquarePiece.WhiteRook: return GameImage.WhiteRook;
				case ChessSquarePiece.WhiteQueen: return GameImage.WhiteQueen;
				case ChessSquarePiece.WhiteKing: return GameImage.WhiteKing;

				case ChessSquarePiece.Empty: throw new Exception();
				default: throw new Exception();
			}
		}

		public static string GetImageFilename(this GameImage image)
		{
			switch (image)
			{
				case GameImage.SoundOn: return "Kenney/SoundOn.png";
				case GameImage.SoundOff: return "Kenney/SoundOff.png";
				case GameImage.MusicOn: return "Kenney/MusicOn.png";
				case GameImage.MusicOff: return "Kenney/MusicOff.png";
				case GameImage.Gear: return "Kenney/Gear.png";
				case GameImage.GearHover: return "Kenney/Gear_Hover.png";
				case GameImage.GearSelected: return "Kenney/Gear_Selected.png";

				case GameImage.Cross: return "Kenney/cross.png";
				case GameImage.CrossHover: return "Kenney/cross_Hover.png";
				case GameImage.CrossSelected: return "Kenney/cross_Selected.png";

				case GameImage.Down: return "Kenney/down.png";
				case GameImage.Up: return "Kenney/up.png";

				case GameImage.BlackPawn: return "Cburnett/BlackPawn.png";
				case GameImage.BlackRook: return "Cburnett/BlackRook.png";
				case GameImage.BlackKnight: return "Cburnett/BlackKnight.png";
				case GameImage.BlackBishop: return "Cburnett/BlackBishop.png";
				case GameImage.BlackQueen: return "Cburnett/BlackQueen.png";
				case GameImage.BlackKing: return "Cburnett/BlackKing.png";

				case GameImage.WhitePawn: return "Cburnett/WhitePawn.png";
				case GameImage.WhiteRook: return "Cburnett/WhiteRook.png";
				case GameImage.WhiteKnight: return "Cburnett/WhiteKnight.png";
				case GameImage.WhiteBishop: return "Cburnett/WhiteBishop.png";
				case GameImage.WhiteQueen: return "Cburnett/WhiteQueen.png";
				case GameImage.WhiteKing: return "Cburnett/WhiteKing.png";

				case GameImage.Nuke_NotReady: return "Kenney/spaceRockets_004.png";
				case GameImage.Nuke_Ready: return "Kenney/spaceRockets_004_green.png";
				case GameImage.Nuke_Hover: return "Kenney/spaceRockets_004_highlighted.png";
				case GameImage.Nuke_Selected: return "Kenney/spaceRockets_004_yellow.png";
				case GameImage.Nuke_RocketFire: return "Kenney/spaceEffects_004.png";

				case GameImage.Nuke_Explosion1: return "Kenney/regularExplosion00.png";
				case GameImage.Nuke_Explosion2: return "Kenney/regularExplosion01.png";
				case GameImage.Nuke_Explosion3: return "Kenney/regularExplosion02.png";
				case GameImage.Nuke_Explosion4: return "Kenney/regularExplosion03.png";
				case GameImage.Nuke_Explosion5: return "Kenney/regularExplosion04.png";
				case GameImage.Nuke_Explosion6: return "Kenney/regularExplosion05.png";
				case GameImage.Nuke_Explosion7: return "Kenney/regularExplosion06.png";
				case GameImage.Nuke_Explosion8: return "Kenney/regularExplosion07.png";
				case GameImage.Nuke_Explosion9: return "Kenney/regularExplosion08.png";

				default: throw new Exception();
			}
		}
	}
}
