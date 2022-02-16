
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
			/// 0 means the first row, first col
			/// 1 means the first rol, second col
			/// 2 means the second row, first col
			/// 3 means the second row, second col
			/// etc
			/// </summary>
			public int PositionIndex { get; private set; }
			public int X { get; private set; }
			public int Y { get; private set; }

			public const int WIDTH = 125;
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
		public static int? GetHoverOverMove(IMouse mouseInput)
		{
			List<MoveDisplay> moveDisplays = GetMoveDisplays();

			int mouseX = mouseInput.GetX();
			int mouseY = mouseInput.GetY();

			foreach (MoveDisplay moveDisplay in moveDisplays)
			{
				if (moveDisplay.X <= mouseX && mouseX <= moveDisplay.X + MoveDisplay.WIDTH && moveDisplay.Y <= mouseY && mouseY <= moveDisplay.Y + MoveDisplay.HEIGHT)
					return moveDisplay.PositionIndex;
			}

			return null;
		}

		public static MoveTracker.MoveInfo GetMoveInfoForHover(int positionIndex, MoveTracker moveTracker)
		{
			List<Tuple<MoveDisplay, MoveTracker.MoveInfo>> moveDisplayMapping = GetMoveDisplayMapping(moveTracker: moveTracker);

			foreach (Tuple<MoveDisplay, MoveTracker.MoveInfo> x in moveDisplayMapping)
			{
				MoveDisplay moveDisplay = x.Item1;
				MoveTracker.MoveInfo moveInfo = x.Item2;

				if (moveDisplay.PositionIndex == positionIndex)
					return moveInfo;
			}

			return null;
		}

		private static List<MoveDisplay> GetMoveDisplays()
		{
			List<MoveDisplay> list = new List<MoveDisplay>();

			bool isWhite = true;
			int y = (MoveDisplay.HEIGHT - 1) * 9;

			for (int i = 0; i < 20; i++)
			{
				if (isWhite)
					list.Add(new MoveDisplay(positionIndex: i, x: 0, y: y));
				else
				{
					list.Add(new MoveDisplay(positionIndex: i, x: MoveDisplay.WIDTH - 1, y: y));
					y = y - (MoveDisplay.HEIGHT - 1);
				}

				isWhite = !isWhite;
			}

			return list;
		}

		private static List<Tuple<MoveDisplay, MoveTracker.MoveInfo>> GetMoveDisplayMapping(MoveTracker moveTracker)
		{
			List<MoveTracker.MoveInfo> moveInfos = moveTracker.GetRecentMoves();

			if (moveInfos.Count == 0)
				return new List<Tuple<MoveDisplay, MoveTracker.MoveInfo>>();

			int index = moveInfos.Count;
			index = index - 20; // show last 20 moves
			if (index < 0)
				index = 0;

			if (!moveInfos[index].OriginalGameState.IsWhiteTurn)
			{
				index++;
				if (index == moveInfos.Count)
					return new List<Tuple<MoveDisplay, MoveTracker.MoveInfo>>();
			}

			List<MoveDisplay> moveDisplays = GetMoveDisplays();
			int moveDisplayIndex = 0;

			List<Tuple<MoveDisplay, MoveTracker.MoveInfo>> returnValue = new List<Tuple<MoveDisplay, MoveTracker.MoveInfo>>();

			while (true)
			{
				if (index == moveInfos.Count)
				{
					if (moveInfos[moveInfos.Count - 1].OriginalGameState.IsWhiteTurn)
						returnValue.Add(new Tuple<MoveDisplay, MoveTracker.MoveInfo>(moveDisplays[moveDisplayIndex], null));
					return returnValue;
				}

				MoveDisplay moveDisplay = moveDisplays[moveDisplayIndex];

				returnValue.Add(new Tuple<MoveDisplay, MoveTracker.MoveInfo>(moveDisplay, moveInfos[index]));
				
				moveDisplayIndex++;
				index++;
			}
		}

		public void Render(IDisplayOutput<ChessImage, ChessFont> displayOutput)
		{
			List<Tuple<MoveDisplay, MoveTracker.MoveInfo>> moveDisplayMapping = GetMoveDisplayMapping(moveTracker: this.moveTracker);

			Dictionary<int, int> hoverInfoMapping = new Dictionary<int, int>();
			foreach (HoverInfo hoverInfo in this.hoverInfos)
				hoverInfoMapping[hoverInfo.PositionIndex] = hoverInfo.ElapsedMicros;

			foreach (Tuple<MoveDisplay, MoveTracker.MoveInfo> entry in moveDisplayMapping)
			{
				MoveDisplay moveDisplay = entry.Item1;
				MoveTracker.MoveInfo moveInfo = entry.Item2;
				displayOutput.DrawRectangle(
					x: moveDisplay.X,
					y: moveDisplay.Y,
					width: MoveDisplay.WIDTH - 1,
					height: MoveDisplay.HEIGHT - 1,
					color: ColorThemeUtil.GetTextBackgroundColor(colorTheme: this.colorTheme),
					fill: true);
				displayOutput.DrawRectangle(
					x: moveDisplay.X,
					y: moveDisplay.Y,
					width: MoveDisplay.WIDTH,
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
						width: MoveDisplay.WIDTH - 1,
						height: MoveDisplay.HEIGHT - 1,
						color: fadeColor,
						fill: true);
				}

				if (moveInfo != null)
					displayOutput.DrawText(
						x: moveDisplay.X + 2,
						y: moveDisplay.Y + 28,
						text: moveInfo.MoveName,
						font: ChessFont.ChessFont14Pt,
						color: DTColor.Black());
			}
		}
	}
}
