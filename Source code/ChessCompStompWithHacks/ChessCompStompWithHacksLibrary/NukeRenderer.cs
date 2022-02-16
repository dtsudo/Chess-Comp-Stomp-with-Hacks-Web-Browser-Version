
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System;
	using System.Collections.Generic;

	public class NukeRenderer
	{
		private bool hasNukeAbility;
		private bool hasUsedNuke;
		private bool isNukeSelected;
		private Tuple<int, int> isHoverOverNuke;
		private int turnCount;
		private ITimer timer;
		private int? nukeAnimationElapsedMicros;
		private ColorTheme colorTheme;

		private const int ELAPSED_MICROS_TO_FLY_OFF_SCREEN = 300 * 1000;

		private NukeRenderer(
			bool hasNukeAbility,
			bool hasUsedNuke,
			bool isNukeSelected,
			Tuple<int, int> isHoverOverNuke,
			int turnCount,
			ITimer timer,
			int? nukeAnimationElapsedMicros,
			ColorTheme colorTheme)
		{
			this.hasNukeAbility = hasNukeAbility;
			this.hasUsedNuke = hasUsedNuke;
			this.isNukeSelected = isNukeSelected;
			this.isHoverOverNuke = isHoverOverNuke;
			this.turnCount = turnCount;
			this.timer = timer;
			this.nukeAnimationElapsedMicros = nukeAnimationElapsedMicros;
			this.colorTheme = colorTheme;
		}

		public static NukeRenderer GetNukeRenderer(
			bool hasNukeAbility,
			bool hasUsedNuke,
			bool isNukeSelected,
			Tuple<int, int> isHoverOverNuke,
			int turnCount,
			ITimer timer,
			ColorTheme colorTheme)
		{
			return new NukeRenderer(
				hasNukeAbility: hasNukeAbility,
				hasUsedNuke: hasUsedNuke,
				isNukeSelected: isNukeSelected,
				isHoverOverNuke: isHoverOverNuke,
				turnCount: turnCount,
				timer: timer,
				nukeAnimationElapsedMicros: null,
				colorTheme: colorTheme);
		}
		
		public NukeRenderer LaunchNuke()
		{
			return new NukeRenderer(
				hasNukeAbility: this.hasNukeAbility,
				hasUsedNuke: this.hasUsedNuke,
				isNukeSelected: this.isNukeSelected,
				isHoverOverNuke: this.isHoverOverNuke,
				turnCount: this.turnCount,
				timer: this.timer,
				nukeAnimationElapsedMicros: 0,
				colorTheme: this.colorTheme);
		}
		
		public NukeRenderer ProcessFrame(
			bool hasUsedNuke,
			bool isNukeSelected,
			Tuple<int, int> isHoverOverNuke,
			int turnCount,
			int elapsedMicrosPerFrame)
		{
			int? newNukeAnimationElapsedMicros;
			if (this.nukeAnimationElapsedMicros == null)
				newNukeAnimationElapsedMicros = null;
			else
				newNukeAnimationElapsedMicros = this.nukeAnimationElapsedMicros.Value + elapsedMicrosPerFrame;

			if (newNukeAnimationElapsedMicros.HasValue && newNukeAnimationElapsedMicros.Value > ELAPSED_MICROS_TO_FLY_OFF_SCREEN)
				newNukeAnimationElapsedMicros = ELAPSED_MICROS_TO_FLY_OFF_SCREEN + 1;

			return new NukeRenderer(
				hasNukeAbility: this.hasNukeAbility,
				hasUsedNuke: hasUsedNuke,
				isNukeSelected: isNukeSelected,
				isHoverOverNuke: isHoverOverNuke,
				turnCount: turnCount,
				timer: this.timer,
				nukeAnimationElapsedMicros: newNukeAnimationElapsedMicros,
				colorTheme: this.colorTheme);
		}

		public bool HasNukeFlownOffScreen()
		{
			if (this.nukeAnimationElapsedMicros == null)
				return false;

			return this.nukeAnimationElapsedMicros.Value >= ELAPSED_MICROS_TO_FLY_OFF_SCREEN;
		}

		public static bool IsHoverOverNuke(IMouse mouse)
		{
			int mouseX = mouse.GetX();
			int mouseY = mouse.GetY();

			List<Tuple<int, int, int, int>> hitboxes = new List<Tuple<int, int, int, int>>();

			hitboxes.Add(new Tuple<int, int, int, int>(38, 0, 98, 18));
			hitboxes.Add(new Tuple<int, int, int, int>(0, 18, 136, 54));
			hitboxes.Add(new Tuple<int, int, int, int>(0, 54, 136, 67));
			hitboxes.Add(new Tuple<int, int, int, int>(9, 67, 127, 77));
			hitboxes.Add(new Tuple<int, int, int, int>(16, 77, 120, 87));
			hitboxes.Add(new Tuple<int, int, int, int>(23, 87, 113, 97));
			hitboxes.Add(new Tuple<int, int, int, int>(29, 97, 107, 104));
			hitboxes.Add(new Tuple<int, int, int, int>(33, 104, 103, 318));
			hitboxes.Add(new Tuple<int, int, int, int>(40, 318, 96, 324));
			hitboxes.Add(new Tuple<int, int, int, int>(46, 324, 90, 355));
			hitboxes.Add(new Tuple<int, int, int, int>(50, 355, 86, 363));
			hitboxes.Add(new Tuple<int, int, int, int>(57, 363, 79, 366));
			hitboxes.Add(new Tuple<int, int, int, int>(61, 366, 75, 369));

			foreach (Tuple<int, int, int, int> hitbox in hitboxes)
			{
				if (mouseX >= hitbox.Item1 && mouseX <= hitbox.Item3 && mouseY >= hitbox.Item2 && mouseY <= hitbox.Item4)
					return true;
			}
			
			return false;
		}

		public void Render(IDisplayOutput<ChessImage, ChessFont> displayOutput)
		{
			if (!this.hasNukeAbility)
				return;

			if (this.nukeAnimationElapsedMicros == null)
			{
				if (this.hasUsedNuke)
					return;
				
				bool isNukeAvailable = this.turnCount > TacticalNukeUtil.NumberOfMovesPlayedBeforeNukeIsAvailable;

				ChessImage nukeImage;

				if (isNukeAvailable)
				{
					if (this.isNukeSelected)
						nukeImage = ChessImage.Nuke_Selected;
					else if (this.isHoverOverNuke != null)
						nukeImage = ChessImage.Nuke_Hover;
					else
						nukeImage = ChessImage.Nuke_Ready;
				}
				else
				{
					nukeImage = ChessImage.Nuke_NotReady;
				}

				displayOutput.DrawImageRotatedClockwise(
					image: nukeImage,
					x: 0,
					y: 0,
					degreesScaled: 0,
					scalingFactorScaled: 128);

				if (this.isHoverOverNuke != null && !isNukeAvailable)
				{
					int numTurnsUntilNukeAvailable = TacticalNukeUtil.NumberOfMovesPlayedBeforeNukeIsAvailable + 1 - this.turnCount;
					displayOutput.DrawRectangle(
						x: this.isHoverOverNuke.Item1,
						y: this.isHoverOverNuke.Item2,
						width: 335 + (numTurnsUntilNukeAvailable >= 10 ? 8 : 0),
						height: 21,
						color: ColorThemeUtil.GetTextBackgroundColor(colorTheme: this.colorTheme),
						fill: true);

					string text = numTurnsUntilNukeAvailable > 1
						? "Tactical nuke available in " + numTurnsUntilNukeAvailable.ToStringCultureInvariant() + " turns"
						: "Tactical nuke available in 1 turn";

					displayOutput.DrawText(
						x: this.isHoverOverNuke.Item1 + 5,
						y: this.isHoverOverNuke.Item2 + 19,
						text: text,
						font: ChessFont.ChessFont14Pt,
						color: DTColor.Black());
				}
			}
			else
			{
				if (this.nukeAnimationElapsedMicros.Value >= ELAPSED_MICROS_TO_FLY_OFF_SCREEN)
					return;

				int rocketWidth = displayOutput.GetWidth(ChessImage.Nuke_Ready);

				int rocketFireScalingFactor = 256;
				int rocketFireWidthOriginal = displayOutput.GetWidth(ChessImage.Nuke_RocketFire);
				int rocketFireHeightOriginal = displayOutput.GetHeight(ChessImage.Nuke_RocketFire);
				int rocketFireWidthScaled = rocketFireWidthOriginal * rocketFireScalingFactor / 128;
				int rocketFireHeightScaled = rocketFireHeightOriginal * rocketFireScalingFactor / 128;
				int endingY = ChessCompStompWithHacks.WINDOW_HEIGHT + rocketFireHeightScaled;
				int y = (int)(((long)this.nukeAnimationElapsedMicros.Value) * ((long)endingY) / ((long)ELAPSED_MICROS_TO_FLY_OFF_SCREEN));

				displayOutput.DrawImageRotatedClockwise(
					image: ChessImage.Nuke_Ready,
					x: 0,
					y: y,
					degreesScaled: 0,
					scalingFactorScaled: 128);

				displayOutput.DrawImageRotatedClockwise(
					image: ChessImage.Nuke_RocketFire,
					x: (rocketWidth - rocketFireWidthScaled) / 2,
					y: y - rocketFireHeightScaled,
					degreesScaled: 0,
					scalingFactorScaled: rocketFireScalingFactor);
			}
		}
	}
}
