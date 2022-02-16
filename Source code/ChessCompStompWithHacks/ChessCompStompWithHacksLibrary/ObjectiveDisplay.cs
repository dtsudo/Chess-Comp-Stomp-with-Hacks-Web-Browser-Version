
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System;
	using System.Collections.Generic;

	public class ObjectiveDisplay
	{
		private const int NON_FINAL_OBJECTIVE_WIDTH = 250;
		private const int NON_FINAL_OBJECTIVE_HEIGHT = 100;

		private const int FINAL_OBJECTIVE_WIDTH = 500;
		private const int FINAL_OBJECTIVE_HEIGHT = 100;
		
		private ObjectiveDisplayUtil objectiveDisplayUtil;

		public ObjectiveDisplay()
		{
			this.objectiveDisplayUtil = new ObjectiveDisplayUtil();
		}

		public void RenderNonFinalObjective(
			int x,
			int y,
			Objective objective,
			HashSet<Objective> completedObjectives,
			IDisplayOutput<ChessImage, ChessFont> displayOutput)
		{
			bool hasCompletedObjective = completedObjectives.Contains(objective);
			
			displayOutput.DrawRectangle(
				x: x,
				y: y,
				width: NON_FINAL_OBJECTIVE_WIDTH - 1,
				height: NON_FINAL_OBJECTIVE_HEIGHT - 1,
				color: hasCompletedObjective ? new DTColor(201, 255, 196) : new DTColor(255, 211, 161),
				fill: true);

			displayOutput.DrawRectangle(
				x: x,
				y: y,
				width: NON_FINAL_OBJECTIVE_WIDTH,
				height: NON_FINAL_OBJECTIVE_HEIGHT,
				color: new DTColor(110, 110, 110),
				fill: false);

			if (objective == Objective.WinFinalBattle)
				throw new Exception();

			string objectiveDescription = this.objectiveDisplayUtil.GetObjectiveDescription(objective: objective).DescriptionForObjectiveFrame;

			displayOutput.DrawText(
				x: x + 10,
				y: y + 90,
				text: objectiveDescription,
				font: ChessFont.ChessFont16Pt,
				color: DTColor.Black());

			displayOutput.DrawText(
				x: x + 10,
				y: y + 39,
				text: SessionState.NUMBER_OF_HACK_POINTS_PER_OBJECTIVE.ToStringCultureInvariant() + " hack points",
				font: ChessFont.ChessFont14Pt,
				color: new DTColor(128, 128, 128));

			displayOutput.DrawText(
				x: x + 10,
				y: y + 20,
				text: hasCompletedObjective ? "(completed)" : "(incomplete)",
				font: ChessFont.ChessFont12Pt,
				color: new DTColor(128, 128, 128));
		}

		public bool HasUnlockedFinalObjective(HashSet<Objective> completedObjectives)
		{
			return completedObjectives.Contains(Objective.DefeatComputer)
				&& completedObjectives.Contains(Objective.DefeatComputerByPlayingAtMost25Moves)
				&& completedObjectives.Contains(Objective.DefeatComputerWith5QueensOnTheBoard)
				&& completedObjectives.Contains(Objective.CheckmateUsingAKnight)
				&& completedObjectives.Contains(Objective.PromoteAPieceToABishop)
				&& completedObjectives.Contains(Objective.LaunchANuke);
		}

		public void RenderFinalObjective(
			int x,
			int y,
			HashSet<Objective> completedObjectives,
			IDisplayOutput<ChessImage, ChessFont> displayOutput)
		{
			bool hasCompletedObjective = completedObjectives.Contains(Objective.WinFinalBattle);

			bool hasUnlockedFinalObjective = HasUnlockedFinalObjective(completedObjectives: completedObjectives);

			DTColor fillColor;

			if (hasCompletedObjective)
				fillColor = new DTColor(201, 255, 196);
			else if (!hasUnlockedFinalObjective)
				fillColor = new DTColor(179, 179, 179);
			else
				fillColor = new DTColor(252, 185, 149);

			displayOutput.DrawRectangle(
				x: x,
				y: y,
				width: FINAL_OBJECTIVE_WIDTH - 1,
				height: FINAL_OBJECTIVE_HEIGHT - 1,
				color: fillColor,
				fill: true);

			displayOutput.DrawRectangle(
				x: x,
				y: y,
				width: FINAL_OBJECTIVE_WIDTH,
				height: FINAL_OBJECTIVE_HEIGHT,
				color: new DTColor(110, 110, 110),
				fill: false);

			if (hasUnlockedFinalObjective)
			{
				displayOutput.DrawText(
					x: x + 10,
					y: y + 90,
					text: this.objectiveDisplayUtil.GetObjectiveDescription(objective: Objective.WinFinalBattle).DescriptionForObjectiveFrame,
					font: ChessFont.ChessFont16Pt,
					color: DTColor.Black());

				displayOutput.DrawText(
					x: x + 10,
					y: y + 30,
					text: hasCompletedObjective ? "(completed)" : "(incomplete)",
					font: ChessFont.ChessFont14Pt,
					color: new DTColor(128, 128, 128));
			}
			else
			{
				displayOutput.DrawText(
					x: x + 237,
					y: y + 76,
					text: "?",
					font: ChessFont.ChessFont32Pt,
					color: DTColor.Black());
			}
		}
	}
}
