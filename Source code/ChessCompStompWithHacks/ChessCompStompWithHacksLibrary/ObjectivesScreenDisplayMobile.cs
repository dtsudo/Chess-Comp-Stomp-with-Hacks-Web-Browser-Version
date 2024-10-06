
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System;
	using System.Collections.Generic;

	public class ObjectivesScreenDisplayMobile
	{
		private ObjectiveDisplay objectiveDisplay;
		private HashSet<Objective> completedObjectives;

		public ObjectivesScreenDisplayMobile(SessionState sessionState)
		{
			this.objectiveDisplay = new ObjectiveDisplay();
			this.completedObjectives = sessionState.GetCompletedObjectives();
		}

		private bool HasCompletedThreeHiddenObjectives()
		{
			return this.GetCompletedHiddenObjectives().Count == 3;
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

		public void Render(IDisplayOutput<GameImage, GameFont> displayOutput)
		{
			if (displayOutput.IsMobileInLandscapeOrientation())
				this.RenderLandscape(displayOutput: displayOutput);
			else
				this.RenderPortrait(displayOutput: displayOutput);
		}

		private void RenderLandscape(IDisplayOutput<GameImage, GameFont> displayOutput)
		{
			displayOutput.DrawText(
				x: displayOutput.GetMobileScreenWidth() / 2 - 163,
				y: 675,
				text: "Objectives",
				font: GameFont.GameFont48Pt,
				color: DTColor.Black());

			int row1Y;
			int row2Y;
			int row3Y;
			List<Objective> completedHiddenObjectives = this.GetCompletedHiddenObjectives();
			completedHiddenObjectives.Sort(comparer: new ObjectiveUtil.ObjectiveComparer());
			
			if (this.HasCompletedAtLeastOneHiddenObjective())
			{
				row1Y = 505;
				row2Y = row1Y - 105;
				row3Y = row2Y - 105;
			}
			else
			{
				row1Y = 450;
				row2Y = row1Y - 130;
				row3Y = row2Y - 130;
			}

			int spacingWidth = (displayOutput.GetMobileScreenWidth() - 250 * 3) / 4;
			int column1X = spacingWidth;
			int column2X = column1X + 250 + spacingWidth;
			int column3X = column2X + 250 + spacingWidth;

			this.objectiveDisplay.RenderNonFinalObjective(
				x: column1X,
				y: row1Y,
				objective: Objective.DefeatComputer,
				completedObjectives: this.completedObjectives,
				displayOutput: displayOutput);

			this.objectiveDisplay.RenderNonFinalObjective(
				x: column2X,
				y: row1Y,
				objective: Objective.DefeatComputerByPlayingAtMost25Moves,
				completedObjectives: this.completedObjectives,
				displayOutput: displayOutput);

			this.objectiveDisplay.RenderNonFinalObjective(
				x: column3X,
				y: row1Y,
				objective: Objective.DefeatComputerWith5QueensOnTheBoard,
				completedObjectives: this.completedObjectives,
				displayOutput: displayOutput);

			this.objectiveDisplay.RenderNonFinalObjective(
				x: column1X,
				y: row2Y,
				objective: Objective.CheckmateUsingAKnight,
				completedObjectives: this.completedObjectives,
				displayOutput: displayOutput);

			this.objectiveDisplay.RenderNonFinalObjective(
				x: column2X,
				y: row2Y,
				objective: Objective.PromoteAPieceToABishop,
				completedObjectives: this.completedObjectives,
				displayOutput: displayOutput);

			this.objectiveDisplay.RenderNonFinalObjective(
				x: column3X,
				y: row2Y,
				objective: Objective.LaunchANuke,
				completedObjectives: this.completedObjectives,
				displayOutput: displayOutput);

			if (completedHiddenObjectives.Count >= 1)
				this.objectiveDisplay.RenderNonFinalObjective(
					x: column1X,
					y: row3Y,
					objective: completedHiddenObjectives[0],
					completedObjectives: this.completedObjectives,
					displayOutput: displayOutput);
			if (completedHiddenObjectives.Count >= 2)
				this.objectiveDisplay.RenderNonFinalObjective(
					x: column2X,
					y: row3Y,
					objective: completedHiddenObjectives[1],
					completedObjectives: this.completedObjectives,
					displayOutput: displayOutput);
			if (completedHiddenObjectives.Count >= 3)
				this.objectiveDisplay.RenderNonFinalObjective(
					x: column3X,
					y: row3Y,
					objective: completedHiddenObjectives[2],
					completedObjectives: this.completedObjectives,
					displayOutput: displayOutput);

			if (completedHiddenObjectives.Count >= 4)
				throw new Exception();

			this.objectiveDisplay.RenderFinalObjective(
				x: (displayOutput.GetMobileScreenWidth() - 500) / 2,
				y: 190,
				completedObjectives: this.completedObjectives,
				displayOutput: displayOutput);
		}

		private void RenderPortrait(IDisplayOutput<GameImage, GameFont> displayOutput)
		{
			displayOutput.DrawText(
				x: 187,
				y: displayOutput.GetMobileScreenHeight() - 100,
				text: "Objectives",
				font: GameFont.GameFont48Pt,
				color: DTColor.Black());

			int row1Y;
			int row2Y;
			int row3Y;
			int row4Y;
			int row5Y;
			List<Objective> completedHiddenObjectives = this.GetCompletedHiddenObjectives();
			completedHiddenObjectives.Sort(comparer: new ObjectiveUtil.ObjectiveComparer());
			
			if (this.HasCompletedThreeHiddenObjectives())
			{
				int spacing = (displayOutput.GetMobileScreenHeight() - 270 - 290 - 400) / 5;
				row1Y = displayOutput.GetMobileScreenHeight() - 270;
				row2Y = row1Y - 100 - spacing;
				row3Y = row2Y - 100 - spacing;
				row4Y = row3Y - 100 - spacing;
				row5Y = row4Y - 100 - spacing;
			}
			else if (this.HasCompletedAtLeastOneHiddenObjective())
			{
				int spacing = (displayOutput.GetMobileScreenHeight() - 270 - 290 - 300) / 4;
				row1Y = displayOutput.GetMobileScreenHeight() - 270;
				row2Y = row1Y - 100 - spacing;
				row3Y = row2Y - 100 - spacing;
				row4Y = row3Y - 100 - spacing;
				row5Y = row4Y - 100 - spacing;
			}
			else
			{
				int spacing = (displayOutput.GetMobileScreenHeight() - 325 - 290 - 200) / 3;
				row1Y = displayOutput.GetMobileScreenHeight() - 325;
				row2Y = row1Y - 100 - spacing;
				row3Y = row2Y - 100 - spacing;
				row4Y = row3Y - 100 - spacing;
				row5Y = row4Y - 100 - spacing;
			}
			
			this.objectiveDisplay.RenderNonFinalObjective(
				x: 67,
				y: row1Y,
				objective: Objective.DefeatComputer,
				completedObjectives: this.completedObjectives,
				displayOutput: displayOutput);

			this.objectiveDisplay.RenderNonFinalObjective(
				x: 383,
				y: row1Y,
				objective: Objective.DefeatComputerByPlayingAtMost25Moves,
				completedObjectives: this.completedObjectives,
				displayOutput: displayOutput);

			this.objectiveDisplay.RenderNonFinalObjective(
				x: 67,
				y: row2Y,
				objective: Objective.DefeatComputerWith5QueensOnTheBoard,
				completedObjectives: this.completedObjectives,
				displayOutput: displayOutput);

			this.objectiveDisplay.RenderNonFinalObjective(
				x: 383,
				y: row2Y,
				objective: Objective.CheckmateUsingAKnight,
				completedObjectives: this.completedObjectives,
				displayOutput: displayOutput);

			this.objectiveDisplay.RenderNonFinalObjective(
				x: 67,
				y: row3Y,
				objective: Objective.PromoteAPieceToABishop,
				completedObjectives: this.completedObjectives,
				displayOutput: displayOutput);

			this.objectiveDisplay.RenderNonFinalObjective(
				x: 383,
				y: row3Y,
				objective: Objective.LaunchANuke,
				completedObjectives: this.completedObjectives,
				displayOutput: displayOutput);

			if (completedHiddenObjectives.Count >= 1)
				this.objectiveDisplay.RenderNonFinalObjective(
					x: 67,
					y: row4Y,
					objective: completedHiddenObjectives[0],
					completedObjectives: this.completedObjectives,
					displayOutput: displayOutput);
			if (completedHiddenObjectives.Count >= 2)
				this.objectiveDisplay.RenderNonFinalObjective(
					x: 383,
					y: row4Y,
					objective: completedHiddenObjectives[1],
					completedObjectives: this.completedObjectives,
					displayOutput: displayOutput);
			if (completedHiddenObjectives.Count >= 3)
				this.objectiveDisplay.RenderNonFinalObjective(
					x: 67,
					y: row5Y,
					objective: completedHiddenObjectives[2],
					completedObjectives: this.completedObjectives,
					displayOutput: displayOutput);

			if (completedHiddenObjectives.Count >= 4)
				throw new Exception();

			this.objectiveDisplay.RenderFinalObjective(
				x: 100,
				y: 190,
				completedObjectives: this.completedObjectives,
				displayOutput: displayOutput);
		}
	}
}
