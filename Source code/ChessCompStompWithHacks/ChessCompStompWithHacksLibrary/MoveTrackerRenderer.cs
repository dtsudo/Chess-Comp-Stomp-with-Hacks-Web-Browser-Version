﻿
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System;
	using System.Collections.Generic;

	public class MoveTrackerRenderer
	{
		private class MoveDisplay
		{
			public MoveDisplay(
				int positionIndex,
				int x,
				int y)
			{
				this.PositionIndex = positionIndex;
				this.X = x;
				this.Y = y;
			}

			/// <summary>
			/// Desktop:
			/// 	0 means the first row, first col
			/// 	1 means the first rol, second col
			/// 	2 means the second row, first col
			/// 	3 means the second row, second col
			/// 	etc
			/// 
			/// Mobile:
			/// 	0 means first row
			/// 	1 means second row
			/// 	etc
			/// </summary>
			public int PositionIndex { get; private set; }
			public int X { get; private set; }
			public int Y { get; private set; }

			public const int DESKTOP_WIDTH = 125;
			public const int MOBILE_WIDTH = 140;
			public const int HEIGHT = 35;
		}

		private class HoverInfo
		{
			public HoverInfo(int positionIndex, int elapsedMicros)
			{
				this.PositionIndex = positionIndex;
				this.ElapsedMicros = elapsedMicros;
			}
			
			public int PositionIndex { get; private set; }
			public int ElapsedMicros { get; private set; }
		}

		private MoveTracker moveTracker;
		private List<HoverInfo> hoverInfos;

		private ColorTheme colorTheme;

		private const int HOVER_HIGHLIGHT_DURATION_MICROS = 1000 * 1000;

		private MoveTrackerRenderer(
			MoveTracker moveTracker,
			List<HoverInfo> hoverInfos,
			ColorTheme colorTheme)
		{
			this.moveTracker = moveTracker;
			this.hoverInfos = new List<HoverInfo>(hoverInfos);

			this.colorTheme = colorTheme;
		}

		public static MoveTrackerRenderer GetMoveTrackerRenderer(MoveTracker moveTracker, ColorTheme colorTheme)
		{
			return new MoveTrackerRenderer(moveTracker: moveTracker, hoverInfos: new List<HoverInfo>(), colorTheme: colorTheme);
		}

		public MoveTrackerRenderer ProcessFrame(
			MoveTracker moveTracker,
			int? hoverPositionIndex,
			int elapsedMicrosPerFrame)
		{
			List<HoverInfo> newHoverInfos = new List<HoverInfo>();

			foreach (HoverInfo hoverInfo in this.hoverInfos)
			{
				int positionIndex = hoverInfo.PositionIndex;
				int elapsedMicros = hoverInfo.ElapsedMicros + elapsedMicrosPerFrame;

				if (elapsedMicros <= HOVER_HIGHLIGHT_DURATION_MICROS && (hoverPositionIndex == null || hoverPositionIndex.Value != positionIndex))
					newHoverInfos.Add(new HoverInfo(positionIndex: positionIndex, elapsedMicros: elapsedMicros));
			}

			if (hoverPositionIndex != null)
				newHoverInfos.Add(new HoverInfo(positionIndex: hoverPositionIndex.Value, elapsedMicros: 0));

			return new MoveTrackerRenderer(
				moveTracker: moveTracker,
				hoverInfos: newHoverInfos,
				colorTheme: this.colorTheme);
		}

		/// <summary>
		/// Returns the positionIndex of the move the mouse is hovering over.
		/// Returns null if the mouse isn't hovering over the MoveTrackerRenderer.
		/// </summary>
		public static int? GetHoverOverMove(IMouse mouseInput, bool isMobileDisplayType)
		{
			if (isMobileDisplayType && !mouseInput.IsLeftMouseButtonPressed())
				return null;

			List<MoveDisplay> moveDisplays = GetMoveDisplays(isMobileDisplayType: isMobileDisplayType);

			int mouseX = mouseInput.GetX();
			int mouseY = mouseInput.GetY();

			int width = isMobileDisplayType ? MoveDisplay.MOBILE_WIDTH : MoveDisplay.DESKTOP_WIDTH;

			foreach (MoveDisplay moveDisplay in moveDisplays)
			{
				if (moveDisplay.X <= mouseX && mouseX <= moveDisplay.X + width && moveDisplay.Y <= mouseY && mouseY <= moveDisplay.Y + MoveDisplay.HEIGHT)
					return moveDisplay.PositionIndex;
			}

			return null;
		}

		public static MoveTracker.MoveInfo GetMoveInfoForHover(int positionIndex, MoveTracker moveTracker, bool isMobileDisplayType)
		{
			List<Tuple<MoveDisplay, MoveTracker.MoveInfo>> moveDisplayMapping = GetMoveDisplayMapping(moveTracker: moveTracker, isMobileDisplayType: isMobileDisplayType);

			foreach (Tuple<MoveDisplay, MoveTracker.MoveInfo> x in moveDisplayMapping)
			{
				MoveDisplay moveDisplay = x.Item1;
				MoveTracker.MoveInfo moveInfo = x.Item2;

				if (moveDisplay.PositionIndex == positionIndex)
					return moveInfo;
			}

			return null;
		}

		private static List<MoveDisplay> GetMoveDisplays(bool isMobileDisplayType)
		{
			List<MoveDisplay> list = new List<MoveDisplay>();

			if (isMobileDisplayType)
			{
				for (int i = 0; i < 5; i++)
				{
					list.Add(new MoveDisplay(positionIndex: i, x: 0, y: (4 - i) * MoveDisplay.HEIGHT));
				}
			}
			else
			{
				bool isWhite = true;
				int y = (MoveDisplay.HEIGHT - 1) * 9;

				for (int i = 0; i < 20; i++)
				{
					if (isWhite)
						list.Add(new MoveDisplay(positionIndex: i, x: 0, y: y));
					else
					{
						list.Add(new MoveDisplay(positionIndex: i, x: MoveDisplay.DESKTOP_WIDTH - 1, y: y));
						y = y - (MoveDisplay.HEIGHT - 1);
					}

					isWhite = !isWhite;
				}
			}

			return list;
		}

		private static List<Tuple<MoveDisplay, MoveTracker.MoveInfo>> GetMoveDisplayMapping(MoveTracker moveTracker, bool isMobileDisplayType)
		{
			List<MoveTracker.MoveInfo> moveInfos = moveTracker.GetRecentMoves();

			if (moveInfos.Count == 0)
				return new List<Tuple<MoveDisplay, MoveTracker.MoveInfo>>();

			int index;

			if (isMobileDisplayType)
			{
				index = moveInfos.Count;
				index = index - 5; // show last 5 moves
				if (index < 0)
					index = 0;
			}
			else
			{
				index = moveInfos.Count;
				index = index - 20; // show last 20 moves
				if (index < 0)
					index = 0;

				if (!moveInfos[index].OriginalGameState.IsWhiteTurn)
				{
					index++;
					if (index == moveInfos.Count)
						return new List<Tuple<MoveDisplay, MoveTracker.MoveInfo>>();
				}
			}

			List<MoveDisplay> moveDisplays = GetMoveDisplays(isMobileDisplayType: isMobileDisplayType);
			int moveDisplayIndex = 0;

			List<Tuple<MoveDisplay, MoveTracker.MoveInfo>> returnValue = new List<Tuple<MoveDisplay, MoveTracker.MoveInfo>>();

			while (true)
			{
				if (index == moveInfos.Count)
				{
					if (!isMobileDisplayType && moveInfos[moveInfos.Count - 1].OriginalGameState.IsWhiteTurn)
						returnValue.Add(new Tuple<MoveDisplay, MoveTracker.MoveInfo>(moveDisplays[moveDisplayIndex], null));
					return returnValue;
				}

				MoveDisplay moveDisplay = moveDisplays[moveDisplayIndex];

				returnValue.Add(new Tuple<MoveDisplay, MoveTracker.MoveInfo>(moveDisplay, moveInfos[index]));
				
				moveDisplayIndex++;
				index++;
			}
		}

		public void Render(IDisplayOutput<GameImage, GameFont> displayOutput, bool isMobileDisplayType)
		{
			List<Tuple<MoveDisplay, MoveTracker.MoveInfo>> moveDisplayMapping = GetMoveDisplayMapping(moveTracker: this.moveTracker, isMobileDisplayType: isMobileDisplayType);

			Dictionary<int, int> hoverInfoMapping = new Dictionary<int, int>();
			foreach (HoverInfo hoverInfo in this.hoverInfos)
				hoverInfoMapping[hoverInfo.PositionIndex] = hoverInfo.ElapsedMicros;

			int width = isMobileDisplayType ? MoveDisplay.MOBILE_WIDTH : MoveDisplay.DESKTOP_WIDTH;

			foreach (Tuple<MoveDisplay, MoveTracker.MoveInfo> entry in moveDisplayMapping)
			{
				MoveDisplay moveDisplay = entry.Item1;
				MoveTracker.MoveInfo moveInfo = entry.Item2;
				displayOutput.DrawRectangle(
					x: moveDisplay.X,
					y: moveDisplay.Y,
					width: width,
					height: MoveDisplay.HEIGHT,
					color: ColorThemeUtil.GetTextBackgroundColor(colorTheme: this.colorTheme),
					fill: true);
				displayOutput.DrawRectangle(
					x: moveDisplay.X,
					y: moveDisplay.Y,
					width: width,
					height: MoveDisplay.HEIGHT,
					color: DTColor.Black(),
					fill: false);

				if (hoverInfoMapping.ContainsKey(moveDisplay.PositionIndex))
				{
					int elapsedMicros = hoverInfoMapping[moveDisplay.PositionIndex];
					int alpha = (HOVER_HIGHLIGHT_DURATION_MICROS - elapsedMicros) * 150 / HOVER_HIGHLIGHT_DURATION_MICROS;
					if (alpha > 255)
						alpha = 255;
					if (alpha < 0)
						alpha = 0;
					DTColor fadeColor = new DTColor(255, 255, 255, alpha);
					
					displayOutput.DrawRectangle(
						x: moveDisplay.X,
						y: moveDisplay.Y,
						width: width,
						height: MoveDisplay.HEIGHT,
						color: fadeColor,
						fill: true);
				}

				if (moveInfo != null)
					displayOutput.DrawText(
						x: moveDisplay.X + 2,
						y: moveDisplay.Y + 28,
						text: moveInfo.MoveName,
						font: GameFont.GameFont14Pt,
						color: DTColor.Black());
			}
		}
	}
}
