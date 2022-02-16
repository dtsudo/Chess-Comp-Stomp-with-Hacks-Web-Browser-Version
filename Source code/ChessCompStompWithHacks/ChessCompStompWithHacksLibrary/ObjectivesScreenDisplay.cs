
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System;
	using System.Collections.Generic;

	public class ObjectivesScreenDisplay
	{
		private ObjectiveDisplay objectiveDisplay;
		private HashSet<Objective> completedObjectives;

		public ObjectivesScreenDisplay(SessionState sessionState)
		{
			this.objectiveDisplay = new ObjectiveDisplay();
			this.completedObjectives = sessionState.GetCompletedObjectives();
		}

		private bool HasCompletedAtLeastOneHiddenObjective()
		{
			return this.GetCompletedHiddenObjectives().Count > 0;
		}

		private List<Objective> GetCompletedHiddenObjectives()
		{
			List<Objective> completedHiddenObjectives = new List<Objective>();

			foreach (Objective completedObjective in this.completedObjectives)
			{
				if (completedObjective.IsHiddenObjective())
					completedHiddenObjectives.Add(completedObjective);
			}

			return completedHiddenObjectives;
		}

		public void Render(IDisplayOutput<ChessImage, ChessFont> displayOutput)
		{
			displayOutput.DrawText(
				x: 389,
				y: 675,
				text: "Objectives",
				font: ChessFont.ChessFont32Pt,
				color: DTColor.Black());

			int row1Y;
			int row2Y;
			int row3Y;
			List<Objective> completedHiddenObjectives = this.GetCompletedHiddenObjectives();
			completedHiddenObjectives.Sort(comparer: new ObjectiveUtil.ObjectiveComparer());

			if (this.HasCompletedAtLeastOneHiddenObjective())
			{
				row1Y = 520;
				row2Y = row1Y - 110;
				row3Y = row2Y - 110;
			}
			else
			{
				row1Y = 450;
				row2Y = row1Y - 130;
				row3Y = row2Y - 130;
			}

			this.objectiveDisplay.RenderNonFinalObjective(
				x: 62,
				y: row1Y,
				objective: Objective.DefeatComputer,
				completedObjectives: this.completedObjectives,
				displayOutput: displayOutput);

			this.objectiveDisplay.RenderNonFinalObjective(
				x: 375,
				y: row1Y,
				objective: Objective.DefeatComputerByPlayingAtMost25Moves,
				completedObjectives: this.completedObjectives,
				displayOutput: displayOutput);

			this.objectiveDisplay.RenderNonFinalObjective(
				x: 687,
				y: row1Y,
				objective: Objective.DefeatComputerWith5QueensOnTheBoard,
				completedObjectives: this.completedObjectives,
				displayOutput: displayOutput);

			this.objectiveDisplay.RenderNonFinalObjective(
				x: 62,
				y: row2Y,
				objective: Objective.CheckmateUsingAKnight,
				completedObjectives: this.completedObjectives,
				displayOutput: displayOutput);

			this.objectiveDisplay.RenderNonFinalObjective(
				x: 375,
				y: row2Y,
				objective: Objective.PromoteAPieceToABishop,
				completedObjectives: this.completedObjectives,
				displayOutput: displayOutput);

			this.objectiveDisplay.RenderNonFinalObjective(
				x: 687,
				y: row2Y,
				objective: Objective.LaunchANuke,
				completedObjectives: this.completedObjectives,
				displayOutput: displayOutput);

			if (completedHiddenObjectives.Count >= 1)
				this.objectiveDisplay.RenderNonFinalObjective(
					x: 62,
					y: row3Y,
					objective: completedHiddenObjectives[0],
					completedObjectives: this.completedObjectives,
					displayOutput: displayOutput);
			if (completedHiddenObjectives.Count >= 2)
				this.objectiveDisplay.RenderNonFinalObjective(
					x: 375,
					y: row3Y,
					objective: completedHiddenObjectives[1],
					completedObjectives: this.completedObjectives,
					displayOutput: displayOutput);
			if (completedHiddenObjectives.Count >= 3)
				this.objectiveDisplay.RenderNonFinalObjective(
					x: 687,
					y: row3Y,
					objective: completedHiddenObjectives[2],
					completedObjectives: this.completedObjectives,
					displayOutput: displayOutput);

			if (completedHiddenObjectives.Count >= 4)
				throw new Exception();

			this.objectiveDisplay.RenderFinalObjective(
				x: 250,
				y: 190,
				completedObjectives: this.completedObjectives,
				displayOutput: displayOutput);
		}
	}
}
