
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using System;
	using System.Collections.Generic;

	public static class ObjectiveUtil
	{
		public static bool IsHiddenObjective(this Objective objective)
		{
			switch (objective)
			{
				case Objective.DefeatComputer:
				case Objective.DefeatComputerByPlayingAtMost25Moves:
				case Objective.DefeatComputerWith5QueensOnTheBoard:
				case Objective.CheckmateUsingAKnight:
				case Objective.PromoteAPieceToABishop:
				case Objective.LaunchANuke:
					return false;

				case Objective.WinFinalBattle:
					return false;

				case Objective.PlayAStupidOpening:
				case Objective.NukeYourOwnPieces:
				case Objective.WinByCastlingVeryLongAndPromotingRookToQueen:
					return true;

				default:
					throw new Exception();
			}
		}

		private static List<Tuple<Objective, int>> GetObjectiveIdMapping()
		{
			List<Tuple<Objective, int>> list = new List<Tuple<Objective, int>>();

			list.Add(new Tuple<Objective, int>(Objective.DefeatComputer, 1));
			list.Add(new Tuple<Objective, int>(Objective.DefeatComputerByPlayingAtMost25Moves, 2));
			list.Add(new Tuple<Objective, int>(Objective.DefeatComputerWith5QueensOnTheBoard, 3));
			list.Add(new Tuple<Objective, int>(Objective.CheckmateUsingAKnight, 4));
			list.Add(new Tuple<Objective, int>(Objective.PromoteAPieceToABishop, 5));
			list.Add(new Tuple<Objective, int>(Objective.LaunchANuke, 6));
			list.Add(new Tuple<Objective, int>(Objective.WinFinalBattle, 7));
			list.Add(new Tuple<Objective, int>(Objective.PlayAStupidOpening, 8));
			list.Add(new Tuple<Objective, int>(Objective.NukeYourOwnPieces, 9));
			list.Add(new Tuple<Objective, int>(Objective.WinByCastlingVeryLongAndPromotingRookToQueen, 10));

			return list;
		}

		/// <summary>
		/// Returns null if the objectiveId isn't valid
		/// </summary>
		public static Objective? GetObjectiveFromObjectiveId(int objectiveId)
		{
			List<Tuple<Objective, int>> mapping = GetObjectiveIdMapping();

			foreach (Tuple<Objective, int> tuple in mapping)
			{
				if (tuple.Item2 == objectiveId)
					return tuple.Item1;
			}

			return null;
		}

		/// <summary>
		/// Maps an objective to an integer identifier (in a consistent but arbitrary way)
		/// </summary>
		public static int GetObjectiveId(this Objective objective)
		{
			List<Tuple<Objective, int>> mapping = GetObjectiveIdMapping();

			foreach (Tuple<Objective, int> tuple in mapping)
			{
				if (tuple.Item1 == objective)
					return tuple.Item2;
			}

			throw new Exception();
		}

		/// <summary>
		/// Compares two objectives in a consistent (though arbitrary) way
		/// </summary>
		public class ObjectiveComparer : IComparer<Objective>
		{
			private static int GetOrderId(Objective objective)
			{
				switch (objective)
				{
					case Objective.DefeatComputer: return 1;
					case Objective.DefeatComputerByPlayingAtMost25Moves: return 2;
					case Objective.DefeatComputerWith5QueensOnTheBoard: return 3;
					case Objective.CheckmateUsingAKnight: return 4;
					case Objective.PromoteAPieceToABishop: return 5;
					case Objective.LaunchANuke: return 6;
					case Objective.WinFinalBattle: return 7;
					case Objective.PlayAStupidOpening: return 8;
					case Objective.NukeYourOwnPieces: return 9;
					case Objective.WinByCastlingVeryLongAndPromotingRookToQueen: return 10;
					default: throw new Exception();
				}
			}

			public int Compare(Objective x, Objective y)
			{
				int xOrderId = GetOrderId(objective: x);
				int yOrderId = GetOrderId(objective: y);

				if (xOrderId < yOrderId)
					return -1;
				if (xOrderId > yOrderId)
					return 1;
				return 0;
			}
		}
	}
}
