
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System;
	using System.Collections.Generic;

	public class AIHackLevelSelectionButton
	{
		private int x;
		private int y;
		private int width;
		private int height;
		private string text;
		private int textXOffset;
		private int textYOffset;
		private GameFont font;

		private bool isPlayerWhite;
		private DTImmutableList<Hack> researchedHacks;

		private SessionState.AIHackLevel? hoveredHackLevel;
		private SessionState.AIHackLevel? clickedHackLevel;

		private IMouse previousMouseInput;

		public AIHackLevelSelectionButton(
			int x,
			int y,
			int width,
			int height,
			string text,
			int textXOffset,
			int textYOffset,
			GameFont font,
			bool isPlayerWhite,
			DTImmutableList<Hack> researchedHacks)
		{
			this.x = x;
			this.y = y;
			this.width = width;
			this.height = height;
			this.text = text;
			this.textXOffset = textXOffset;
			this.textYOffset = textYOffset;
			this.font = font;

			this.isPlayerWhite = isPlayerWhite;
			this.researchedHacks = researchedHacks;

			this.hoveredHackLevel = null;
			this.clickedHackLevel = null;

			this.previousMouseInput = null;
		}

		private SessionState.AIHackLevel? IsHover(IMouse mouseInput)
		{
			int mouseX = mouseInput.GetX();
			int mouseY = mouseInput.GetY();
			bool isHover = this.x <= mouseX && mouseX <= this.x + this.width && this.y <= mouseY && mouseY <= this.y + this.height;

			if (!isHover)
				return null;

			int offset = mouseX - this.x;

			if (offset < this.width / 4)
				return SessionState.AIHackLevel.Initial;
			if (offset < this.width / 2)
				return SessionState.AIHackLevel.UpgradedOnce;
			if (offset < this.width * 3 / 4)
				return SessionState.AIHackLevel.UpgradedTwice;
			return SessionState.AIHackLevel.UpgradedThrice;
		}

		public SessionState.AIHackLevel? ProcessFrame(
			IMouse mouseInput,
			IMouse previousMouseInput)
		{
			if (this.previousMouseInput != null)
				previousMouseInput = this.previousMouseInput;

			this.previousMouseInput = new CopiedMouse(mouse: mouseInput);

			SessionState.AIHackLevel? hoveredAIHackLevel = this.IsHover(mouseInput);

			this.hoveredHackLevel = hoveredAIHackLevel;

			if (mouseInput.IsLeftMouseButtonPressed() && !previousMouseInput.IsLeftMouseButtonPressed())
			{
				if (hoveredAIHackLevel.HasValue)
					this.clickedHackLevel = hoveredHackLevel.Value;
			}
			
			if (this.clickedHackLevel.HasValue && !mouseInput.IsLeftMouseButtonPressed() && previousMouseInput.IsLeftMouseButtonPressed())
			{			
				if (hoveredAIHackLevel.HasValue && hoveredAIHackLevel.Value == this.clickedHackLevel.Value)
				{
					this.clickedHackLevel = null;
					return hoveredAIHackLevel.Value;
				}

				this.clickedHackLevel = null;
			}

			return null;
		}

		public void RenderButton(IDisplayOutput<GameImage, GameFont> displayOutput)
		{
			displayOutput.DrawRectangle(
				x: this.x,
				y: this.y,
				width: this.width - 1,
				height: this.height - 1,
				color: new DTColor(200, 200, 200),
				fill: true);

			if (this.hoveredHackLevel.HasValue)
			{
				int offset;
				ColorTheme colorTheme;

				switch (this.hoveredHackLevel.Value)
				{
					case SessionState.AIHackLevel.Initial:
						offset = 0;
						colorTheme = ColorTheme.Initial;
						break;
					case SessionState.AIHackLevel.UpgradedOnce:
						offset = this.width / 4;
						colorTheme = ColorTheme.Progress1;
						break;
					case SessionState.AIHackLevel.UpgradedTwice:
						offset = this.width / 2;
						colorTheme = ColorTheme.Progress2;
						break;
					case SessionState.AIHackLevel.UpgradedThrice:
						offset = this.width * 3 / 4;
						colorTheme = ColorTheme.Progress3;
						break;
					case SessionState.AIHackLevel.FinalBattle:
					default:
						throw new Exception();
				}

				displayOutput.DrawRectangle(
					x: this.x + offset,
					y: this.y,
					width: this.width / 4 - 1,
					height: this.height - 1,
					color: ColorThemeUtil.GetHoverColor(colorTheme: colorTheme),
					fill: true);
			}

			if (this.clickedHackLevel.HasValue)
			{
				int offset;
				ColorTheme colorTheme;

				switch (this.clickedHackLevel.Value)
				{
					case SessionState.AIHackLevel.Initial:
						offset = 0;
						colorTheme = ColorTheme.Initial;
						break;
					case SessionState.AIHackLevel.UpgradedOnce:
						offset = this.width / 4;
						colorTheme = ColorTheme.Progress1;
						break;
					case SessionState.AIHackLevel.UpgradedTwice:
						offset = this.width / 2;
						colorTheme = ColorTheme.Progress2;
						break;
					case SessionState.AIHackLevel.UpgradedThrice:
						offset = this.width * 3 / 4;
						colorTheme = ColorTheme.Progress3;
						break;
					case SessionState.AIHackLevel.FinalBattle:
					default:
						throw new Exception();
				}

				displayOutput.DrawRectangle(
					x: this.x + offset,
					y: this.y,
					width: this.width / 4 - 1,
					height: this.height - 1,
					color: ColorThemeUtil.GetClickColor(colorTheme: colorTheme),
					fill: true);
			}

			displayOutput.DrawRectangle(
				x: this.x,
				y: this.y,
				width: this.width,
				height: this.height,
				color: DTColor.Black(),
				fill: false);

			displayOutput.DrawText(
				x: this.x + this.textXOffset,
				y: this.y + this.height - this.textYOffset,
				text: this.text,
				font: this.font,
				color: DTColor.Black());
		}

		public void RenderBoardPreview(IDisplayOutput<GameImage, GameFont> displayOutput)
		{
			SessionState.AIHackLevel? previewAIHackLevel;

			if (this.clickedHackLevel.HasValue)
				previewAIHackLevel = this.clickedHackLevel.Value;
			else if (this.hoveredHackLevel.HasValue)
				previewAIHackLevel = this.hoveredHackLevel.Value;
			else
				previewAIHackLevel = null;

			if (previewAIHackLevel.HasValue)
			{
				AIHackLevelSelectionBoardPreview.Render(
					isPlayerWhite: this.isPlayerWhite,
					researchedHacks: this.researchedHacks,
					aiHackLevel: previewAIHackLevel.Value,
					displayOutput: new TranslatedDisplayOutput<GameImage, GameFont>(
						display: displayOutput,
						xOffsetInPixels: this.x + 34,
						yOffsetInPixels: this.y + this.height + 10));
			}
		}
	}
}
