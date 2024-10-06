
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

		public const int PROMOTION_PANEL_WIDTH_DESKTOP = 293;
		public const int PROMOTION_PANEL_HEIGHT_DESKTOP = 100;

		public const int PROMOTION_PANEL_WIDTH_MOBILE = 410;
		public const int PROMOTION_PANEL_HEIGHT_MOBILE = 150;

		private const int QUEEN_OFFSET_X_DESKTOP = 10;
		private const int ROOK_OFFSET_X_DESKTOP = 10 + (62 + 8);
		private const int KNIGHT_OFFSET_X_DESKTOP = 10 + (62 + 8) * 2;
		private const int BISHOP_OFFSET_X_DESKTOP = 10 + (62 + 8) * 3;
		private const int PIECE_OFFSET_Y_DESKTOP = 8;
		
		private const int QUEEN_OFFSET_X_MOBILE = 15;
		private const int ROOK_OFFSET_X_MOBILE = 15 + (85 + 13);
		private const int KNIGHT_OFFSET_X_MOBILE = 15 + (85 + 13) * 2;
		private const int BISHOP_OFFSET_X_MOBILE = 15 + (85 + 13) * 3;
		private const int PIECE_OFFSET_Y_MOBILE = 12;
		
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
			IDisplayProcessing<GameImage> displayProcessing,
			bool isMobileDisplayType)
		{
			if (isMobileDisplayType)
			{
				int imageWidth = displayProcessing.GetWidth(GameImage.WhitePawn) * GameImageUtil.MobileChessPieceScalingFactor / 128;
				int imageHeight = displayProcessing.GetHeight(GameImage.WhitePawn) * GameImageUtil.MobileChessPieceScalingFactor / 128;

				int mouseX = mouse.GetX();
				int mouseY = mouse.GetY();

				if (mouseY < promotionPanelY - PROMOTION_PANEL_HEIGHT_MOBILE + PIECE_OFFSET_Y_MOBILE)
					return null;
				if (mouseY > promotionPanelY - PROMOTION_PANEL_HEIGHT_MOBILE + PIECE_OFFSET_Y_MOBILE + imageHeight)
					return null;

				int mouseXRelativeToPanel = mouseX - promotionPanelX;

				if (QUEEN_OFFSET_X_MOBILE <= mouseXRelativeToPanel && mouseXRelativeToPanel <= QUEEN_OFFSET_X_MOBILE + imageWidth)
					return Move.PromotionType.PromoteToQueen;
				if (ROOK_OFFSET_X_MOBILE <= mouseXRelativeToPanel && mouseXRelativeToPanel <= ROOK_OFFSET_X_MOBILE + imageWidth)
					return Move.PromotionType.PromoteToRook;
				if (KNIGHT_OFFSET_X_MOBILE <= mouseXRelativeToPanel && mouseXRelativeToPanel <= KNIGHT_OFFSET_X_MOBILE + imageWidth)
					return Move.PromotionType.PromoteToKnight;
				if (BISHOP_OFFSET_X_MOBILE <= mouseXRelativeToPanel && mouseXRelativeToPanel <= BISHOP_OFFSET_X_MOBILE + imageWidth)
					return Move.PromotionType.PromoteToBishop;
				return null;
			}
			else
			{
				int imageWidth = displayProcessing.GetWidth(GameImage.WhitePawn) * GameImageUtil.DesktopChessPieceScalingFactor / 128;
				int imageHeight = displayProcessing.GetHeight(GameImage.WhitePawn) * GameImageUtil.DesktopChessPieceScalingFactor / 128;

				int mouseX = mouse.GetX();
				int mouseY = mouse.GetY();

				if (mouseY < promotionPanelY - PROMOTION_PANEL_HEIGHT_DESKTOP + PIECE_OFFSET_Y_DESKTOP)
					return null;
				if (mouseY > promotionPanelY - PROMOTION_PANEL_HEIGHT_DESKTOP + PIECE_OFFSET_Y_DESKTOP + imageHeight)
					return null;

				int mouseXRelativeToPanel = mouseX - promotionPanelX;

				if (QUEEN_OFFSET_X_DESKTOP <= mouseXRelativeToPanel && mouseXRelativeToPanel <= QUEEN_OFFSET_X_DESKTOP + imageWidth)
					return Move.PromotionType.PromoteToQueen;
				if (ROOK_OFFSET_X_DESKTOP <= mouseXRelativeToPanel && mouseXRelativeToPanel <= ROOK_OFFSET_X_DESKTOP + imageWidth)
					return Move.PromotionType.PromoteToRook;
				if (KNIGHT_OFFSET_X_DESKTOP <= mouseXRelativeToPanel && mouseXRelativeToPanel <= KNIGHT_OFFSET_X_DESKTOP + imageWidth)
					return Move.PromotionType.PromoteToKnight;
				if (BISHOP_OFFSET_X_DESKTOP <= mouseXRelativeToPanel && mouseXRelativeToPanel <= BISHOP_OFFSET_X_DESKTOP + imageWidth)
					return Move.PromotionType.PromoteToBishop;
				return null;
			}
		}

		public static bool IsHoverOverPanel(
			int promotionPanelX,
			int promotionPanelY, 
			IMouse mouse,
			bool isMobileDisplayType)
		{
			int mouseX = mouse.GetX();
			int mouseY = mouse.GetY();

			int panelWidth = isMobileDisplayType ? PROMOTION_PANEL_WIDTH_MOBILE : PROMOTION_PANEL_WIDTH_DESKTOP;
			int panelHeight = isMobileDisplayType ? PROMOTION_PANEL_HEIGHT_MOBILE : PROMOTION_PANEL_HEIGHT_DESKTOP;

			if (mouseX < promotionPanelX)
				return false;
			if (mouseX > promotionPanelX + panelWidth)
				return false;
			if (mouseY < promotionPanelY - panelHeight)
				return false;
			if (mouseY > promotionPanelY)
				return false;
			return true;
		}

		private static int GetXOffset(Move.PromotionType promotionType, bool isMobileDisplayType)
		{
			switch (promotionType)
			{
				case Move.PromotionType.PromoteToQueen:
					return isMobileDisplayType ? QUEEN_OFFSET_X_MOBILE : QUEEN_OFFSET_X_DESKTOP;
				case Move.PromotionType.PromoteToRook:
					return isMobileDisplayType ? ROOK_OFFSET_X_MOBILE : ROOK_OFFSET_X_DESKTOP;
				case Move.PromotionType.PromoteToKnight:
					return isMobileDisplayType ? KNIGHT_OFFSET_X_MOBILE : KNIGHT_OFFSET_X_DESKTOP;
				case Move.PromotionType.PromoteToBishop:
					return isMobileDisplayType ? BISHOP_OFFSET_X_MOBILE : BISHOP_OFFSET_X_DESKTOP;
				default:
					throw new Exception();
			}
		}

		public void Render(IDisplayOutput<GameImage, GameFont> displayOutput, bool isMobileDisplayType)
		{
			if (!this.isOpen)
				return;

			int chessPieceScalingFactor = isMobileDisplayType ? GameImageUtil.MobileChessPieceScalingFactor : GameImageUtil.DesktopChessPieceScalingFactor;

			int imageWidth = displayOutput.GetWidth(GameImage.WhitePawn) * chessPieceScalingFactor / 128;
			int imageHeight = displayOutput.GetHeight(GameImage.WhitePawn) * chessPieceScalingFactor / 128;

			int panelWidth;
			int panelHeight;
			int queenOffsetX;
			int rookOffsetX;
			int knightOffsetX;
			int bishopOffsetX;
			int pieceOffsetY;

			if (isMobileDisplayType)
			{
				panelWidth = PROMOTION_PANEL_WIDTH_MOBILE;
				panelHeight = PROMOTION_PANEL_HEIGHT_MOBILE;
				queenOffsetX = QUEEN_OFFSET_X_MOBILE;
				rookOffsetX = ROOK_OFFSET_X_MOBILE;
				knightOffsetX = KNIGHT_OFFSET_X_MOBILE;
				bishopOffsetX = BISHOP_OFFSET_X_MOBILE;
				pieceOffsetY = PIECE_OFFSET_Y_MOBILE;
			}
			else
			{
				panelWidth = PROMOTION_PANEL_WIDTH_DESKTOP;
				panelHeight = PROMOTION_PANEL_HEIGHT_DESKTOP;
				queenOffsetX = QUEEN_OFFSET_X_DESKTOP;
				rookOffsetX = ROOK_OFFSET_X_DESKTOP;
				knightOffsetX = KNIGHT_OFFSET_X_DESKTOP;
				bishopOffsetX = BISHOP_OFFSET_X_DESKTOP;
				pieceOffsetY = PIECE_OFFSET_Y_DESKTOP;
			}

			displayOutput.DrawRectangle(
				x: this.x,
				y: this.y - panelHeight,
				width: panelWidth,
				height: panelHeight,
				color: ColorThemeUtil.GetTextBackgroundColor(colorTheme: this.colorTheme),
				fill: true);
			
			if (isMobileDisplayType)
			{
				displayOutput.DrawText(
					x: this.x + 126,
					y: this.y - 10,
					text: "Promote to:",
					font: GameFont.GameFont20Pt,
					color: DTColor.Black());
			}
			else
			{
				displayOutput.DrawText(
					x: this.x + 90,
					y: this.y - 10,
					text: "Promote to:",
					font: GameFont.GameFont14Pt,
					color: DTColor.Black());
			}

			displayOutput.DrawImageRotatedClockwise(
				image: this.isWhite ? GameImage.WhiteQueen : GameImage.BlackQueen,
				x: this.x + queenOffsetX,
				y: this.y - panelHeight + pieceOffsetY,
				degreesScaled: 0,
				scalingFactorScaled: chessPieceScalingFactor);
			displayOutput.DrawImageRotatedClockwise(
				image: this.isWhite ? GameImage.WhiteRook : GameImage.BlackRook,
				x: this.x + rookOffsetX,
				y: this.y - panelHeight + pieceOffsetY,
				degreesScaled: 0,
				scalingFactorScaled: chessPieceScalingFactor);
			displayOutput.DrawImageRotatedClockwise(
				image: this.isWhite ? GameImage.WhiteKnight : GameImage.BlackKnight,
				x: this.x + knightOffsetX,
				y: this.y - panelHeight + pieceOffsetY,
				degreesScaled: 0,
				scalingFactorScaled: chessPieceScalingFactor);
			displayOutput.DrawImageRotatedClockwise(
				image: this.isWhite ? GameImage.WhiteBishop : GameImage.BlackBishop,
				x: this.x + bishopOffsetX,
				y: this.y - panelHeight + pieceOffsetY,
				degreesScaled: 0,
				scalingFactorScaled: chessPieceScalingFactor);

			if (!isMobileDisplayType)
			{
				if (this.hoverSquare != null && (this.selectedSquare == null || this.selectedSquare.Value != this.hoverSquare.Value))
				{
					int hoverXOffset = GetXOffset(promotionType: this.hoverSquare.Value, isMobileDisplayType: isMobileDisplayType);

					displayOutput.DrawRectangle(
						x: this.x + hoverXOffset,
						y: this.y - panelHeight + pieceOffsetY,
						width: imageWidth,
						height: imageHeight,
						color: new DTColor(0, 0, 128, 50),
						fill: true);
				}
			}

			if (this.selectedSquare != null)
			{
				int selectedXOffset = GetXOffset(promotionType: this.selectedSquare.Value, isMobileDisplayType: isMobileDisplayType);
				
				displayOutput.DrawRectangle(
					x: this.x + selectedXOffset,
					y: this.y - panelHeight + pieceOffsetY,
					width: imageWidth,
					height: imageHeight,
					color: new DTColor(0, 0, 170, 150),
					fill: true);
			}
		}
	}
}
