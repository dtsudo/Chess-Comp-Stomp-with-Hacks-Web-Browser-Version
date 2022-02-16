
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System;
	using System.Collections.Generic;

	public class ObjectiveDisplayUtil
	{
		public class ObjectiveDescription
		{
			public ObjectiveDescription(
				string descriptionForObjectiveFrame,
				string descriptionForVictoryStalemateOrDefeatPanel)
			{
				this.DescriptionForObjectiveFrame = descriptionForObjectiveFrame;
				this.DescriptionForVictoryStalemateOrDefeatPanel = descriptionForVictoryStalemateOrDefeatPanel;
			}

			public string DescriptionForObjectiveFrame { get; private set; }
			public string DescriptionForVictoryStalemateOrDefeatPanel { get; private set; }
		}

		private string nameOfCastlingVeryLongAndPromotingToQueenWithCheckmateMove;

		public ObjectiveDisplayUtil()
		{
			this.nameOfCastlingVeryLongAndPromotingToQueenWithCheckmateMove = ObjectiveChecker.GetNameOfCastlingVeryLongAndPromotingToQueenWithCheckmateMove();
		}
		
		public ObjectiveDescription GetObjectiveDescription(Objective objective)
		{
			switch (objective)
			{
				case Objective.DefeatComputer:
					return new ObjectiveDescription(
						descriptionForObjectiveFrame: "Win a game against" + "\n" + "the AI.",
						descriptionForVictoryStalemateOrDefeatPanel: "Win a game against the AI");
				case Objective.DefeatComputerByPlayingAtMost25Moves:
					return new ObjectiveDescription(
						descriptionForObjectiveFrame: "Win by playing at" + "\n" + "most 25 moves.",
						descriptionForVictoryStalemateOrDefeatPanel: "Win by playing at most 25 moves");
				case Objective.DefeatComputerWith5QueensOnTheBoard:
					return new ObjectiveDescription(
						descriptionForObjectiveFrame: "Win with 5 queens on" + "\n" + "the board.",
						descriptionForVictoryStalemateOrDefeatPanel: "Win with 5 queens on the board");
				case Objective.CheckmateUsingAKnight:
					return new ObjectiveDescription(
						descriptionForObjectiveFrame: "Deliver checkmate" + "\n" + "using a knight.",
						descriptionForVictoryStalemateOrDefeatPanel: "Deliver checkmate using a knight");
				case Objective.PromoteAPieceToABishop:
					return new ObjectiveDescription(
						descriptionForObjectiveFrame: "Promote a piece to" + "\n" + "a bishop.",
						descriptionForVictoryStalemateOrDefeatPanel: "Promote a piece to a bishop");
				case Objective.LaunchANuke:
					return new ObjectiveDescription(
						descriptionForObjectiveFrame: "Launch a nuke.",
						descriptionForVictoryStalemateOrDefeatPanel: "Launch a nuke");
				case Objective.WinFinalBattle:
					return new ObjectiveDescription(
						descriptionForObjectiveFrame: "Win the Final Battle against the AI.",
						descriptionForVictoryStalemateOrDefeatPanel: "Win the Final Battle against the AI");
				case Objective.PlayAStupidOpening:
					return new ObjectiveDescription(
						descriptionForObjectiveFrame: "Play a stupid" + "\n" + "opening.",
						descriptionForVictoryStalemateOrDefeatPanel: "Play a stupid opening");
				case Objective.NukeYourOwnPieces:
					return new ObjectiveDescription(
						descriptionForObjectiveFrame: "Carelessly nuke" + "\n" + "your own pieces.",
						descriptionForVictoryStalemateOrDefeatPanel: "Carelessly nuke your own pieces");
				case Objective.WinByCastlingVeryLongAndPromotingRookToQueen:
					return new ObjectiveDescription(
						descriptionForObjectiveFrame: "Play the move" + "\n" + this.nameOfCastlingVeryLongAndPromotingToQueenWithCheckmateMove,
						descriptionForVictoryStalemateOrDefeatPanel: "Play the move " + this.nameOfCastlingVeryLongAndPromotingToQueenWithCheckmateMove);
				default:
					throw new Exception();
			}
		}
	}
}
