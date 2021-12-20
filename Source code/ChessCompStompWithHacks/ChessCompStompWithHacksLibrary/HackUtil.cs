
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System;

	public static class HackUtil
	{
		public static int GetHackCost(this Hack hack)
		{
			switch (hack)
			{
				case Hack.ExtraPawnFirst:
				case Hack.ExtraPawnSecond:
				case Hack.StalemateIsVictory:
				case Hack.SuperCastling:
				case Hack.PawnsCanMoveThreeSpacesInitially:
					return 3;

				case Hack.RooksCanMoveLikeBishops:
				case Hack.ExtraQueen: 
				case Hack.RooksCanCaptureLikeCannons: 
				case Hack.KnightsCanMakeLargeKnightsMove:
				case Hack.QueensCanMoveLikeKnights:
					return 5;

				case Hack.SuperEnPassant: 
				case Hack.AnyPieceCanPromote: 
				case Hack.OpponentMustCaptureWhenPossible: 
					return 10;

				case Hack.PawnsDestroyCapturingPiece:
				case Hack.TacticalNuke:
					return 20;

				default: throw new Exception();
			}
		}

		public static string GetHackNameForHackSelectionScreen(this Hack hack)
		{
			switch (hack)
			{
				case Hack.ExtraPawnFirst: return "Extra pawn";
				case Hack.ExtraPawnSecond: return "Another extra" + "\n" + "pawn";
				case Hack.ExtraQueen: return "Extra queen";
				case Hack.PawnsCanMoveThreeSpacesInitially: return "Pawn boost";
				case Hack.SuperEnPassant: return "Super" + "\n" + "en passant";
				case Hack.RooksCanMoveLikeBishops: return "Diagonal rooks";
				case Hack.SuperCastling: return "Super castling";
				case Hack.RooksCanCaptureLikeCannons: return "Cannoning";
				case Hack.KnightsCanMakeLargeKnightsMove: return "Upgraded" + "\n" + "knights";
				case Hack.QueensCanMoveLikeKnights: return "Upgraded queen";
				case Hack.TacticalNuke: return "Tactical nuke";
				case Hack.AnyPieceCanPromote: return "Equitable" + "\n" + "promotions";
				case Hack.StalemateIsVictory: return "Anti-stalemate";
				case Hack.OpponentMustCaptureWhenPossible: return "Mandatory" + "\n" + "captures";
				case Hack.PawnsDestroyCapturingPiece: return "Sacrificial" + "\n" + "pawns";
				default: throw new Exception();
			}
		}

		public class HackDescription
		{
			public HackDescription(string description, int width)
			{
				this.Description = description;
				this.Width = width;
			}

			public string Description { get; private set; }
			public int Width { get; private set; }
		}

		public static HackDescription GetHackDescriptionForHackSelectionScreen(this Hack hack)
		{
			switch (hack)
			{
				case Hack.ExtraPawnFirst: return new HackDescription("Start with an extra pawn.", 301);
				case Hack.ExtraPawnSecond: return new HackDescription("Start with another extra pawn.", 357);
				case Hack.ExtraQueen: return new HackDescription("Start with an extra queen.", 309);
				case Hack.PawnsCanMoveThreeSpacesInitially:
					return new HackDescription("The first time a pawn moves, the pawn may" + "\n"
						+ "move forward 3 squares.",
						460);
				case Hack.SuperEnPassant:
					return new HackDescription("Your pawns may capture enemy pieces that are" + "\n"
						+ "horizontally adjacent to the pawn." + "\n"
						+ "Super en passant is allowed regardless of" + "\n"
						+ "when or how the enemy piece moved." + "\n"
						+ "The pawn may capture super en passant" + "\n"
						+ "regardless of which rank the pawn is on.", 
						500);
				case Hack.RooksCanMoveLikeBishops:
					return new HackDescription("In addition to their normal moves, your rooks" + "\n"
						+ "may also move as if they were bishops.",
						489);
				case Hack.SuperCastling:
					return new HackDescription("You may castle as long as there are no pieces" + "\n"
							+ "between your king and rook." + "\n"
							+ "Super castling is allowed regardless of" + "\n"
							+ "whether the king or rook has previously moved." + "\n"
							+ "You cannot super castle out of, through," + "\n"
							+ "or into check." + "\n"
							+ "Super castling is allowed both horizontally" + "\n"
							+ "and vertically.",
						500);
				case Hack.RooksCanCaptureLikeCannons:
					return new HackDescription("Your rooks may capture enemy pieces even if" + "\n"
						+ "there is a piece between your rook and the" + "\n"
						+ "piece being captured.",
						486);
				case Hack.KnightsCanMakeLargeKnightsMove:
					return new HackDescription("Your knights may make large knight's moves" + "\n"
						+ "(moving forward 3 squares and 1 square to" + "\n"
						+ "the side).",
						472);
				case Hack.QueensCanMoveLikeKnights:
					return new HackDescription("Your queen may also move as" + "\n"
						+ "if it were a knight.", 
						336);
				case Hack.TacticalNuke:
					return new HackDescription("You start each game with a nuke." + "\n"
						+ "The nuke requires " + TacticalNukeUtil.NumberOfMovesPlayedBeforeNukeIsAvailable.ToStringCultureInvariant() + " turns before" + "\n"
						+ "it is operational.",
						380);
				case Hack.AnyPieceCanPromote:
					return new HackDescription("Your rooks, knights, bishops, and queen may" + "\n"
						+ "promote upon reaching the last rank.",
						476);
				case Hack.StalemateIsVictory:
					return new HackDescription("If it is your turn and you have no legal" + "\n"
						+ "moves, you win the game." + "\n"
						+ "If it is your opponent's turn and your" + "\n"
						+ "opponent has no legal moves, you win" + "\n"
						+ "the game.",
						428);
				case Hack.OpponentMustCaptureWhenPossible:
					return new HackDescription("Capturing is compulsory for your opponent" + "\n"
						+ "(if your opponent can capture a piece, your" + "\n"
						+ "opponent must capture a piece).",
						476);
				case Hack.PawnsDestroyCapturingPiece:
					return new HackDescription("When any of your pawns are captured," + "\n"
						+ "the capturing piece is also removed" + "\n"
						+ "from the board.",
						428);
				default: throw new Exception();
			}
		}
	}
}
