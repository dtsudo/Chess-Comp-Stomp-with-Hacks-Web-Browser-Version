
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System;
	using System.Collections.Generic;

	public class PromotionPanel
	{
		private bool isWhite;
		private bool isOpen;
		private int x;
		private int y;
		private Move.PromotionType? hoverSquare;
		private Move.PromotionType? selectedSquare;
		private ColorTheme colorTheme;

		public const int PROMOTION_PANEL_WIDTH = 293;
		public const int PROMOTION_PANEL_HEIGHT = 100;

		private const int QUEEN_OFFSET_X = 10;
		private const int ROOK_OFFSET_X = 10 + 70;
		private const int KNIGHT_OFFSET_X = 10 + 70 * 2;
		private const int BISHOP_OFFSET_X = 10 + 70 * 3;
		private const int PIECE_OFFSET_Y = 8;

		private PromotionPanel(
			bool isWhite,
			bool isOpen,
			int x,
			int y,
			Move.PromotionType? hoverSquare,
			Move.PromotionType? selectedSquare,
			ColorTheme colorTheme)
		{
			this.isWhite = isWhite;
			this.isOpen = isOpen;
			this.x = x;
			this.y = y;
			this.hoverSquare = hoverSquare;
			this.selectedSquare = selectedSquare;
			this.colorTheme = colorTheme;
		}

		public static PromotionPanel GetPromotionPanel(bool isWhite, ColorTheme colorTheme)
		{
			return new PromotionPanel(
				isWhite: isWhite,
				isOpen: false,
				x: 0,
				y: 0,
				hoverSquare: null,
				selectedSquare: null,
				colorTheme: colorTheme);
		}

		public PromotionPanel ProcessFrame(
			bool isOpen,
			int x,
			int y,
			Move.PromotionType? hoverSquare,
			Move.PromotionType? selectedSquare)
		{
			return new PromotionPanel(
				isWhite: this.isWhite,
				isOpen: isOpen,
				x: x,
				y: y,
				hoverSquare: hoverSquare,
				selectedSquare: selectedSquare,
				colorTheme: this.colorTheme);
		}

		/// <summary>
		/// Returns the piece that the mouse is hovering over, if any
		/// </summary>
		public static Move.PromotionType? IsHoverOverSquare(
			int promotionPanelX,
			int promotionPanelY,
			IMouse mouse,
			IDisplayProcessing<GameImage> displayProcessing)
		{
			int imageWidth = displayProcessing.GetWidth(GameImage.WhitePawn) * GameImageUtil.ChessPieceScalingFactor / 128;
			int imageHeight = displayProcessing.GetHeight(GameImage.WhitePawn) * GameImageUtil.ChessPieceScalingFactor / 128;

			int mouseX = mouse.GetX();
			int mouseY = mouse.GetY();

			if (mouseY < promotionPanelY - PROMOTION_PANEL_HEIGHT + PIECE_OFFSET_Y)
				return null;
			if (mouseY > promotionPanelY - PROMOTION_PANEL_HEIGHT + PIECE_OFFSET_Y + imageHeight)
				return null;

			int mouseXRelativeToPanel = mouseX - promotionPanelX;

			if (QUEEN_OFFSET_X <= mouseXRelativeToPanel && mouseXRelativeToPanel <= QUEEN_OFFSET_X + imageWidth)
				return Move.PromotionType.PromoteToQueen;
			if (ROOK_OFFSET_X <= mouseXRelativeToPanel && mouseXRelativeToPanel <= ROOK_OFFSET_X + imageWidth)
				return Move.PromotionType.PromoteToRook;
			if (KNIGHT_OFFSET_X <= mouseXRelativeToPanel && mouseXRelativeToPanel <= KNIGHT_OFFSET_X + imageWidth)
				return Move.PromotionType.PromoteToKnight;
			if (BISHOP_OFFSET_X <= mouseXRelativeToPanel && mouseXRelativeToPanel <= BISHOP_OFFSET_X + imageWidth)
				return Move.PromotionType.PromoteToBishop;
			return null;
		}

		public static bool IsHoverOverPanel(
			int promotionPanelX,
			int promotionPanelY, 
			IMouse mouse)
		{
			int mouseX = mouse.GetX();
			int mouseY = mouse.GetY();
			
			if (mouseX < promotionPanelX)
				return false;
			if (mouseX > promotionPanelX + PROMOTION_PANEL_WIDTH)
				return false;
			if (mouseY < promotionPanelY - PROMOTION_PANEL_HEIGHT)
				return false;
			if (mouseY > promotionPanelY)
				return false;
			return true;
		}

		private static int GetXOffset(Move.PromotionType promotionType)
		{
			switch (promotionType)
			{
				case Move.PromotionType.PromoteToQueen:
					return QUEEN_OFFSET_X;
				case Move.PromotionType.PromoteToRook:
					return ROOK_OFFSET_X;
				case Move.PromotionType.PromoteToKnight:
					return KNIGHT_OFFSET_X;
				case Move.PromotionType.PromoteToBishop:
					return BISHOP_OFFSET_X;
				default:
					throw new Exception();
			}
		}

		public void Render(IDisplayOutput<GameImage, GameFont> displayOutput)
		{
			if (!this.isOpen)
				return;

			int imageWidth = displayOutput.GetWidth(GameImage.WhitePawn) * GameImageUtil.ChessPieceScalingFactor / 128;
			int imageHeight = displayOutput.GetHeight(GameImage.WhitePawn) * GameImageUtil.ChessPieceScalingFactor / 128;

			displayOutput.DrawRectangle(
				x: this.x,
				y: this.y - PROMOTION_PANEL_HEIGHT,
				width: PROMOTION_PANEL_WIDTH,
				height: PROMOTION_PANEL_HEIGHT,
				color: ColorThemeUtil.GetTextBackgroundColor(colorTheme: this.colorTheme),
				fill: true);

			displayOutput.DrawText(
				x: this.x + 90,
				y: this.y - 10,
				text: "Promote to:",
				font: GameFont.GameFont14Pt,
				color: DTColor.Black());

			displayOutput.DrawImageRotatedClockwise(
				image: this.isWhite ? GameImage.WhiteQueen : GameImage.BlackQueen,
				x: this.x + QUEEN_OFFSET_X,
				y: this.y - PROMOTION_PANEL_HEIGHT + PIECE_OFFSET_Y,
				degreesScaled: 0,
				scalingFactorScaled: GameImageUtil.ChessPieceScalingFactor);
			displayOutput.DrawImageRotatedClockwise(
				image: this.isWhite ? GameImage.WhiteRook : GameImage.BlackRook,
				x: this.x + ROOK_OFFSET_X,
				y: this.y - PROMOTION_PANEL_HEIGHT + PIECE_OFFSET_Y,
				degreesScaled: 0,
				scalingFactorScaled: GameImageUtil.ChessPieceScalingFactor);
			displayOutput.DrawImageRotatedClockwise(
				image: this.isWhite ? GameImage.WhiteKnight : GameImage.BlackKnight,
				x: this.x + KNIGHT_OFFSET_X,
				y: this.y - PROMOTION_PANEL_HEIGHT + PIECE_OFFSET_Y,
				degreesScaled: 0,
				scalingFactorScaled: GameImageUtil.ChessPieceScalingFactor);
			displayOutput.DrawImageRotatedClockwise(
				image: this.isWhite ? GameImage.WhiteBishop : GameImage.BlackBishop,
				x: this.x + BISHOP_OFFSET_X,
				y: this.y - PROMOTION_PANEL_HEIGHT + PIECE_OFFSET_Y,
				degreesScaled: 0,
				scalingFactorScaled: GameImageUtil.ChessPieceScalingFactor);

			if (this.hoverSquare != null && (this.selectedSquare == null || this.selectedSquare.Value != this.hoverSquare.Value))
			{
				int hoverXOffset = GetXOffset(promotionType: this.hoverSquare.Value);

				displayOutput.DrawRectangle(
					x: this.x + hoverXOffset,
					y: this.y - PROMOTION_PANEL_HEIGHT + PIECE_OFFSET_Y,
					width: imageWidth,
					height: imageHeight,
					color: new DTColor(0, 0, 128, 50),
					fill: true);
			}

			if (this.selectedSquare != null)
			{
				int selectedXOffset = GetXOffset(promotionType: this.selectedSquare.Value);
				
				displayOutput.DrawRectangle(
					x: this.x + selectedXOffset,
					y: this.y - PROMOTION_PANEL_HEIGHT + PIECE_OFFSET_Y,
					width: imageWidth,
					height: imageHeight,
					color: new DTColor(0, 0, 170, 150),
					fill: true);
			}
		}
	}
}
