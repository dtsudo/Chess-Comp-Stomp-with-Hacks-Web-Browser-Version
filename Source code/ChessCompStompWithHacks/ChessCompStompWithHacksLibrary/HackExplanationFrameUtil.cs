
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System;

	public class HackExplanationFrameUtil
	{
		public const int CHESS_PIECES_RENDERER_X_OFFSET = 350;
		public const int CHESS_PIECES_RENDERER_Y_OFFSET = 25;

		public const int TITLE_TEXT_Y_OFFSET = 580;

		public const int EXPLANATION_TEXT_X_OFFSET = 15;
		public const int EXPLANATION_TEXT_Y_OFFSET = 519;

		public const int ELAPSED_MICROS_BEFORE_PIECE_MOVES = 1500 * 1000;
		
		public interface IHackExplanation
		{
			void ProcessFrame(IMouse mouseInput, IMouse previousMouseInput, IDisplayProcessing<GameImage> displayProcessing, int elapsedMicrosPerFrame);
			void Render(IDisplayOutput<GameImage, GameFont> displayOutput);
		}

		public static IHackExplanation GetHackExplanation(Hack hack, ColorTheme colorTheme, IDTRandom random, bool hasExtraPawnFirstHack, ITimer timer, int elapsedMicrosPerFrame)
		{
			switch (hack)
			{
				case Hack.ExtraPawnFirst: return new HackExplanation_ExtraPawnFirst(colorTheme: colorTheme, elapsedMicrosPerFrame: elapsedMicrosPerFrame);
				case Hack.ExtraPawnSecond: return new HackExplanation_ExtraPawnSecond(colorTheme: colorTheme, hasExtraPawnFirstHack: hasExtraPawnFirstHack, elapsedMicrosPerFrame: elapsedMicrosPerFrame);
				case Hack.ExtraQueen: return new HackExplanation_ExtraQueen(colorTheme: colorTheme, elapsedMicrosPerFrame: elapsedMicrosPerFrame);
				case Hack.QueensCanMoveLikeKnights: return new HackExplanation_QueensCanMoveLikeKnights(colorTheme: colorTheme, random: random);
				case Hack.RooksCanMoveLikeBishops: return new HackExplanation_RooksCanMoveLikeBishops(colorTheme: colorTheme, random: random);
				case Hack.TacticalNuke: return new HackExplanation_TacticalNuke(colorTheme: colorTheme, timer: timer, elapsedMicrosPerFrame: elapsedMicrosPerFrame);
				case Hack.PawnsCanMoveThreeSpacesInitially: return new HackExplanation_PawnsCanMoveThreeSpacesInitially(colorTheme: colorTheme, random: random);
				case Hack.KnightsCanMakeLargeKnightsMove: return new HackExplanation_KnightsCanMakeLargeKnightsMove(colorTheme: colorTheme, random: random);
				case Hack.SuperCastling: return new HackExplanation_SuperCastling(colorTheme: colorTheme, random: random);
				case Hack.RooksCanCaptureLikeCannons: return new HackExplanation_RooksCanCaptureLikeCannons(colorTheme: colorTheme, random: random);
				case Hack.AnyPieceCanPromote: return new HackExplanation_AnyPieceCanPromote(colorTheme: colorTheme, random: random);
				case Hack.OpponentMustCaptureWhenPossible: return new HackExplanation_OpponentMustCaptureWhenPossible(colorTheme: colorTheme, random: random);
				case Hack.PawnsDestroyCapturingPiece: return new HackExplanation_PawnsDestroyCapturingPiece(colorTheme: colorTheme, random: random);
				case Hack.SuperEnPassant: return new HackExplanation_SuperEnPassant(colorTheme: colorTheme, random: random);
				case Hack.StalemateIsVictory: return new HackExplanation_StalemateIsVictory(colorTheme: colorTheme, random: random);
				default: throw new Exception();
			}
		}
	}
}
