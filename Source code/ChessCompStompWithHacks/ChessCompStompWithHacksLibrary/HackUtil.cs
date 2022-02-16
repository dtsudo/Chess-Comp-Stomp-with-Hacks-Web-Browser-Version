
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System;
	using System.Collections.Generic;

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

		private static List<Tuple<Hack, int>> GetHackIdMapping()
		{
			List<Tuple<Hack, int>> list = new List<Tuple<Hack, int>>();

			list.Add(new Tuple<Hack, int>(Hack.ExtraPawnFirst, 1));
			list.Add(new Tuple<Hack, int>(Hack.ExtraPawnSecond, 2));
			list.Add(new Tuple<Hack, int>(Hack.ExtraQueen, 3));
			list.Add(new Tuple<Hack, int>(Hack.PawnsCanMoveThreeSpacesInitially, 4));
			list.Add(new Tuple<Hack, int>(Hack.SuperEnPassant, 5));
			list.Add(new Tuple<Hack, int>(Hack.RooksCanMoveLikeBishops, 6));
			list.Add(new Tuple<Hack, int>(Hack.SuperCastling, 7));
			list.Add(new Tuple<Hack, int>(Hack.RooksCanCaptureLikeCannons, 8));
			list.Add(new Tuple<Hack, int>(Hack.KnightsCanMakeLargeKnightsMove, 9));
			list.Add(new Tuple<Hack, int>(Hack.QueensCanMoveLikeKnights, 10));
			list.Add(new Tuple<Hack, int>(Hack.TacticalNuke, 11));
			list.Add(new Tuple<Hack, int>(Hack.AnyPieceCanPromote, 12));
			list.Add(new Tuple<Hack, int>(Hack.StalemateIsVictory, 13));
			list.Add(new Tuple<Hack, int>(Hack.OpponentMustCaptureWhenPossible, 14));
			list.Add(new Tuple<Hack, int>(Hack.PawnsDestroyCapturingPiece, 15));

			return list;
		}

		/// <summary>
		/// Returns null if the hackId isn't valid
		/// </summary>
		public static Hack? GetHackFromHackId(int hackId)
		{
			List<Tuple<Hack, int>> mapping = GetHackIdMapping();

			foreach (Tuple<Hack, int> tuple in mapping)
			{
				if (tuple.Item2 == hackId)
					return tuple.Item1;
			}

			return null;
		}

		/// <summary>
		/// Maps a hack to an integer identifier (in a consistent but arbitrary way)
		/// </summary>
		public static int GetHackId(this Hack hack)
		{
			List<Tuple<Hack, int>> mapping = GetHackIdMapping();

			foreach (Tuple<Hack, int> tuple in mapping)
			{
				if (tuple.Item1 == hack)
					return tuple.Item2;
			}

			throw new Exception();
		}

		private class HackNameInfo
		{
			public HackNameInfo(string hackNameForHackSelectionScreen, string hackNameForHackExplanationPanel)
			{
				this.HackNameForHackSelectionScreen = hackNameForHackSelectionScreen;
				this.HackNameForHackExplanationPanel = hackNameForHackExplanationPanel;
			}

			public string HackNameForHackSelectionScreen { get; private set; }
			public string HackNameForHackExplanationPanel { get; private set; }
		}

		private static HackNameInfo GetHackName(Hack hack)
		{
			string hackNameForHackSelectionScreen;

			switch (hack)
			{
				case Hack.ExtraPawnFirst: hackNameForHackSelectionScreen = "Extra pawn"; break;
				case Hack.ExtraPawnSecond: hackNameForHackSelectionScreen = "Another" + "\n" + "extra pawn"; break;
				case Hack.ExtraQueen: hackNameForHackSelectionScreen = "Extra queen"; break;
				case Hack.PawnsCanMoveThreeSpacesInitially: hackNameForHackSelectionScreen = "Pawn boost"; break;
				case Hack.SuperEnPassant: hackNameForHackSelectionScreen = "Super" + "\n" + "en passant"; break;
				case Hack.RooksCanMoveLikeBishops: hackNameForHackSelectionScreen = "Diagonal" + "\n" + "rooks"; break;
				case Hack.SuperCastling: hackNameForHackSelectionScreen = "Super" + "\n" + "castling"; break;
				case Hack.RooksCanCaptureLikeCannons: hackNameForHackSelectionScreen = "Cannoning"; break;
				case Hack.KnightsCanMakeLargeKnightsMove: hackNameForHackSelectionScreen = "Upgraded" + "\n" + "knights"; break;
				case Hack.QueensCanMoveLikeKnights: hackNameForHackSelectionScreen = "Upgraded" + "\n" + "queen"; break;
				case Hack.TacticalNuke: hackNameForHackSelectionScreen = "Tactical nuke"; break;
				case Hack.AnyPieceCanPromote: hackNameForHackSelectionScreen = "Equitable" + "\n" + "promotions"; break;
				case Hack.StalemateIsVictory: hackNameForHackSelectionScreen = "Anti" + "\n" + "stalemate"; break;
				case Hack.OpponentMustCaptureWhenPossible: hackNameForHackSelectionScreen = "Mandatory" + "\n" + "captures"; break;
				case Hack.PawnsDestroyCapturingPiece: hackNameForHackSelectionScreen = "Sacrificial" + "\n" + "pawns"; break;
				default: throw new Exception();
			}

			string hackNameForHackExplanationPanel;

			switch (hack)
			{
				case Hack.ExtraPawnFirst: hackNameForHackExplanationPanel = "Extra pawn"; break;
				case Hack.ExtraPawnSecond: hackNameForHackExplanationPanel = "Another extra pawn"; break;
				case Hack.ExtraQueen: hackNameForHackExplanationPanel = "Extra queen"; break;
				case Hack.PawnsCanMoveThreeSpacesInitially: hackNameForHackExplanationPanel = "Pawn boost"; break;
				case Hack.SuperEnPassant: hackNameForHackExplanationPanel = "Super en passant"; break;
				case Hack.RooksCanMoveLikeBishops: hackNameForHackExplanationPanel = "Diagonal rooks"; break;
				case Hack.SuperCastling: hackNameForHackExplanationPanel = "Super castling"; break;
				case Hack.RooksCanCaptureLikeCannons: hackNameForHackExplanationPanel = "Cannoning"; break;
				case Hack.KnightsCanMakeLargeKnightsMove: hackNameForHackExplanationPanel = "Upgraded knights"; break;
				case Hack.QueensCanMoveLikeKnights: hackNameForHackExplanationPanel = "Upgraded queen"; break;
				case Hack.TacticalNuke: hackNameForHackExplanationPanel = "Tactical nuke"; break;
				case Hack.AnyPieceCanPromote: hackNameForHackExplanationPanel = "Equitable promotions"; break;
				case Hack.StalemateIsVictory: hackNameForHackExplanationPanel = "Anti stalemate"; break;
				case Hack.OpponentMustCaptureWhenPossible: hackNameForHackExplanationPanel = "Mandatory captures"; break;
				case Hack.PawnsDestroyCapturingPiece: hackNameForHackExplanationPanel = "Sacrificial pawns"; break;
				default: throw new Exception();
			}

			return new HackNameInfo(hackNameForHackSelectionScreen: hackNameForHackSelectionScreen, hackNameForHackExplanationPanel: hackNameForHackExplanationPanel);
		}

		public static string GetHackNameForHackSelectionScreen(this Hack hack)
		{
			return GetHackName(hack: hack).HackNameForHackSelectionScreen;
		}

		public static string GetHackNameForHackExplanationPanel(this Hack hack)
		{
			return GetHackName(hack: hack).HackNameForHackExplanationPanel;
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
					return new HackDescription(
						"Your rooks, knights, bishops, and queen may" + "\n"
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
