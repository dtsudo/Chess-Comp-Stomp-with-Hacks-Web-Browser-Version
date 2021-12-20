
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using System;

	public enum ChessImage
	{
		SoundOn,
		SoundOff,
		MusicOn,
		MusicOff,
		Gear,
		GearHover,
		GearSelected,

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

	public static class ChessImageUtil
	{
		public const int ChessPieceScalingFactor = 128 / 8;

		public static ChessImage GetImage(ChessSquarePiece piece)
		{
			switch (piece)
			{
				case ChessSquarePiece.BlackPawn: return ChessImage.BlackPawn;
				case ChessSquarePiece.BlackKnight: return ChessImage.BlackKnight;
				case ChessSquarePiece.BlackBishop: return ChessImage.BlackBishop;
				case ChessSquarePiece.BlackRook: return ChessImage.BlackRook;
				case ChessSquarePiece.BlackQueen: return ChessImage.BlackQueen;
				case ChessSquarePiece.BlackKing: return ChessImage.BlackKing;

				case ChessSquarePiece.WhitePawn: return ChessImage.WhitePawn;
				case ChessSquarePiece.WhiteKnight: return ChessImage.WhiteKnight;
				case ChessSquarePiece.WhiteBishop: return ChessImage.WhiteBishop;
				case ChessSquarePiece.WhiteRook: return ChessImage.WhiteRook;
				case ChessSquarePiece.WhiteQueen: return ChessImage.WhiteQueen;
				case ChessSquarePiece.WhiteKing: return ChessImage.WhiteKing;

				case ChessSquarePiece.Empty: throw new Exception();
				default: throw new Exception();
			}
		}

		public static string GetImageFilename(this ChessImage image)
		{
			switch (image)
			{
				case ChessImage.SoundOn: return "Kenney/SoundOn.png";
				case ChessImage.SoundOff: return "Kenney/SoundOff.png";
				case ChessImage.MusicOn: return "Kenney/MusicOn.png";
				case ChessImage.MusicOff: return "Kenney/MusicOff.png";
				case ChessImage.Gear: return "Kenney/Gear.png";
				case ChessImage.GearHover: return "Kenney/Gear_Hover.png";
				case ChessImage.GearSelected: return "Kenney/Gear_Selected.png";

				case ChessImage.BlackPawn: return "Cburnett/BlackPawn.png";
				case ChessImage.BlackRook: return "Cburnett/BlackRook.png";
				case ChessImage.BlackKnight: return "Cburnett/BlackKnight.png";
				case ChessImage.BlackBishop: return "Cburnett/BlackBishop.png";
				case ChessImage.BlackQueen: return "Cburnett/BlackQueen.png";
				case ChessImage.BlackKing: return "Cburnett/BlackKing.png";

				case ChessImage.WhitePawn: return "Cburnett/WhitePawn.png";
				case ChessImage.WhiteRook: return "Cburnett/WhiteRook.png";
				case ChessImage.WhiteKnight: return "Cburnett/WhiteKnight.png";
				case ChessImage.WhiteBishop: return "Cburnett/WhiteBishop.png";
				case ChessImage.WhiteQueen: return "Cburnett/WhiteQueen.png";
				case ChessImage.WhiteKing: return "Cburnett/WhiteKing.png";

				case ChessImage.Nuke_NotReady: return "Kenney/spaceRockets_004.png";
				case ChessImage.Nuke_Ready: return "Kenney/spaceRockets_004_green.png";
				case ChessImage.Nuke_Hover: return "Kenney/spaceRockets_004_highlighted.png";
				case ChessImage.Nuke_Selected: return "Kenney/spaceRockets_004_yellow.png";
				case ChessImage.Nuke_RocketFire: return "Kenney/spaceEffects_004.png";

				case ChessImage.Nuke_Explosion1: return "Kenney/regularExplosion00.png";
				case ChessImage.Nuke_Explosion2: return "Kenney/regularExplosion01.png";
				case ChessImage.Nuke_Explosion3: return "Kenney/regularExplosion02.png";
				case ChessImage.Nuke_Explosion4: return "Kenney/regularExplosion03.png";
				case ChessImage.Nuke_Explosion5: return "Kenney/regularExplosion04.png";
				case ChessImage.Nuke_Explosion6: return "Kenney/regularExplosion05.png";
				case ChessImage.Nuke_Explosion7: return "Kenney/regularExplosion06.png";
				case ChessImage.Nuke_Explosion8: return "Kenney/regularExplosion07.png";
				case ChessImage.Nuke_Explosion9: return "Kenney/regularExplosion08.png";

				default: throw new Exception();
			}
		}
	}
}
