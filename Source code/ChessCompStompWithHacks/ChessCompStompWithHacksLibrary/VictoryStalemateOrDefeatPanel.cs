
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System;
	using System.Collections.Generic;

	public class VictoryStalemateOrDefeatPanel
	{
		private int x;
		private int y;
		private ComputeMoves.GameStatus gameStatus;
		private bool isPlayerWhite;

		private int? mouseDragXStart;
		private int? mouseDragYStart;

		private Button continueButton;

		private IMouse previousMouseInput;

		private List<Objective> newlyCompletedObjectives;
		private ObjectiveDisplayUtil objectiveDisplayUtil;

		private int elapsedTimeMicros;

		private const int TOTAL_TIME_TO_DISPLAY_COMPLETED_OBJECTIVES = 2 * 1000 * 1000;

		public VictoryStalemateOrDefeatPanel(
			ComputeMoves.GameStatus gameStatus, 
			bool isPlayerWhite,
			HashSet<Objective> completedObjectives,
			HashSet<Objective> objectivesThatWereAlreadyCompletedPriorToThisGame,
			ColorTheme colorTheme)
		{
			List<Objective> newlyCompletedObjectives = new List<Objective>();
			foreach (Objective completedObjective in completedObjectives)
			{
				if (!objectivesThatWereAlreadyCompletedPriorToThisGame.Contains(completedObjective))
					newlyCompletedObjectives.Add(completedObjective);
			}
			newlyCompletedObjectives.Sort(comparer: new ObjectiveUtil.ObjectiveComparer());
			this.newlyCompletedObjectives = newlyCompletedObjectives;

			int width = GetWidth(newlyCompletedObjectives: newlyCompletedObjectives);
			int height = GetHeight(newlyCompletedObjectives: newlyCompletedObjectives);

			this.x = ChessCompStompWithHacks.WINDOW_WIDTH / 2 - width / 2;
			this.y = ChessCompStompWithHacks.WINDOW_HEIGHT / 2 - height / 2;
			this.gameStatus = gameStatus;
			this.isPlayerWhite = isPlayerWhite;
			
			this.mouseDragXStart = null;
			this.mouseDragYStart = null;

			this.previousMouseInput = null;

			this.continueButton = new Button(
				x: (width - 150) / 2,
				y: 55,
				width: 150,
				height: 40,
				backgroundColor: new DTColor(200, 200, 200),
				hoverColor: ColorThemeUtil.GetHoverColor(colorTheme: colorTheme),
				clickColor: ColorThemeUtil.GetClickColor(colorTheme: colorTheme),
				text: "Continue",
				textXOffset: 14,
				textYOffset: 8,
				font: ChessFont.ChessFont20Pt);

			this.objectiveDisplayUtil = new ObjectiveDisplayUtil();

			this.elapsedTimeMicros = 0;
		}

		private static int GetWidth(List<Objective> newlyCompletedObjectives)
		{
			if (newlyCompletedObjectives.Count > 0)
				return 743;
			return 300;
		}

		private static int GetHeight(List<Objective> newlyCompletedObjectives)
		{
			if (newlyCompletedObjectives.Count == 0)
				return 200;

			return 268 + newlyCompletedObjectives.Count * 19;
		}

		private bool IsPlayerVictory()
		{
			return this.gameStatus == ComputeMoves.GameStatus.WhiteVictory && this.isPlayerWhite
				|| this.gameStatus == ComputeMoves.GameStatus.BlackVictory && !this.isPlayerWhite;
		}
		
		public class Result
		{
			public Result(
				bool hasClickedContinueButton,
				bool isHoverOverPanel)
			{
				this.HasClickedContinueButton = hasClickedContinueButton;
				this.IsHoverOverPanel = isHoverOverPanel;
			}

			public bool HasClickedContinueButton { get; private set; }
			public bool IsHoverOverPanel { get; private set; }
		}

		public Result ProcessFrame(IMouse mouseInput, IMouse previousMouseInput, int elapsedMicrosPerFrame)
		{
			this.elapsedTimeMicros += elapsedMicrosPerFrame;
			if (this.elapsedTimeMicros > TOTAL_TIME_TO_DISPLAY_COMPLETED_OBJECTIVES)
				this.elapsedTimeMicros = TOTAL_TIME_TO_DISPLAY_COMPLETED_OBJECTIVES + 1;

			if (this.previousMouseInput != null)
				previousMouseInput = this.previousMouseInput;
			
			this.previousMouseInput = new CopiedMouse(mouse: mouseInput);

			int mouseX = mouseInput.GetX();
			int mouseY = mouseInput.GetY();

			int width = GetWidth(newlyCompletedObjectives: this.newlyCompletedObjectives);
			int height = GetHeight(newlyCompletedObjectives: this.newlyCompletedObjectives);

			IMouse translatedMouse = new TranslatedMouse(mouse: mouseInput, xOffset: -this.x, yOffset: -this.y);

			bool isHoverOverPanel = this.x <= mouseX && mouseX <= this.x + width && this.y <= mouseY && mouseY <= this.y + height;
			
			if (mouseInput.IsLeftMouseButtonPressed() && !previousMouseInput.IsLeftMouseButtonPressed() && isHoverOverPanel && !this.continueButton.IsHover(translatedMouse))
			{
				this.mouseDragXStart = mouseX;
				this.mouseDragYStart = mouseY;
			}

			if (this.mouseDragXStart != null && mouseInput.IsLeftMouseButtonPressed())
			{
				this.x = this.x + (mouseX - this.mouseDragXStart.Value);
				this.y = this.y + (mouseY - this.mouseDragYStart.Value);

				this.mouseDragXStart = mouseX;
				this.mouseDragYStart = mouseY;
			}

			if (!mouseInput.IsLeftMouseButtonPressed() && previousMouseInput.IsLeftMouseButtonPressed())
			{
				this.mouseDragXStart = null;
				this.mouseDragYStart = null;

				if (this.x < 0)
					this.x = 0;

				if (this.y < 0)
					this.y = 0;

				if (this.x > ChessCompStompWithHacks.WINDOW_WIDTH - width)
					this.x = ChessCompStompWithHacks.WINDOW_WIDTH - width;

				if (this.y > ChessCompStompWithHacks.WINDOW_HEIGHT - height)
					this.y = ChessCompStompWithHacks.WINDOW_HEIGHT - height;
			}

			bool isClicked = this.continueButton.ProcessFrame(
				mouseInput: translatedMouse,
				previousMouseInput: new TranslatedMouse(mouse: previousMouseInput, xOffset: -this.x, yOffset: -this.y));

			return new Result(
				hasClickedContinueButton: isClicked,
				isHoverOverPanel: isHoverOverPanel || this.mouseDragXStart != null);
		}

		public void Render(IDisplayOutput<ChessImage, ChessFont> displayOutput)
		{
			int width = GetWidth(newlyCompletedObjectives: this.newlyCompletedObjectives);
			int height = GetHeight(newlyCompletedObjectives: this.newlyCompletedObjectives);

			displayOutput.DrawRectangle(
				x: this.x,
				y: this.y,
				width: width - 1,
				height: height - 1,
				color: DTColor.White(),
				fill: true);

			displayOutput.DrawRectangle(
				x: this.x,
				y: this.y,
				width: width,
				height: height,
				color: DTColor.Black(),
				fill: false);

			string text;
			int textXOffset;
			if (this.IsPlayerVictory())
			{
				text = "Victory!";
				textXOffset = (width / 2) - 86;
			}
			else if (this.gameStatus == ComputeMoves.GameStatus.Stalemate)
			{
				text = "Stalemate!";
				textXOffset = (width / 2) - 112;
			}
			else if (this.gameStatus == ComputeMoves.GameStatus.WhiteVictory || this.gameStatus == ComputeMoves.GameStatus.BlackVictory)
			{
				text = "Defeat!";
				textXOffset = (width / 2) - 80;
			}
			else
				throw new Exception();

			displayOutput.DrawText(
				x: this.x + textXOffset,
				y: this.y + height - 30,
				text: text,
				font: ChessFont.ChessFont32Pt,
				color: DTColor.Black());

			if (this.newlyCompletedObjectives.Count > 0)
			{
				string objectivesText = "";
				foreach (Objective objective in this.newlyCompletedObjectives)
				{
					string objectiveDescription = this.objectiveDisplayUtil.GetObjectiveDescription(objective: objective).DescriptionForVictoryStalemateOrDefeatPanel;
					objectivesText += "Completed objective: " + objectiveDescription + "\n";
				}
				
				int index;
				if (this.elapsedTimeMicros >= TOTAL_TIME_TO_DISPLAY_COMPLETED_OBJECTIVES)
					index = objectivesText.Length;
				else
					index = (int)(((long)this.elapsedTimeMicros) * ((long)objectivesText.Length) / ((long)TOTAL_TIME_TO_DISPLAY_COMPLETED_OBJECTIVES));

				displayOutput.DrawText(
					x: this.x + 40,
					y: this.y + height - 95,
					text: index >= objectivesText.Length ? objectivesText : objectivesText.Substring(0, index),
					font: ChessFont.ChessFont18Pt,
					color: DTColor.Black());
			}

			this.continueButton.Render(displayOutput: new TranslatedDisplayOutput<ChessImage, ChessFont>(display: displayOutput, xOffsetInPixels: this.x, yOffsetInPixels: this.y));
		}
	}
}
