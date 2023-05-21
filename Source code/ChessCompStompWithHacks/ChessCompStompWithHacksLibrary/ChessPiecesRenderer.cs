
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System;
	using System.Collections.Generic;
	
	public class ChessPiecesRenderer
	{
		public class PotentialNukeSquaresInfo
		{
			public PotentialNukeSquaresInfo(ChessSquare nukeCenter, bool isNukeLocationValid)
			{
				List<ChessSquare> potentialNukedSquares = TacticalNukeUtil.GetNukedSquares(nukeCenter);

				this.PotentialNukeSquares = new DTImmutableList<ChessSquare>(potentialNukedSquares);
				this.IsNukeLocationValid = isNukeLocationValid;
			}

			public DTImmutableList<ChessSquare> PotentialNukeSquares { get; private set; }
			public bool IsNukeLocationValid { get; private set; }
		}

		public class HoverPieceInfo
		{
			public HoverPieceInfo(
				ChessSquarePiece chessSquarePiece,
				int x,
				int y)
			{
				this.ChessSquarePiece = chessSquarePiece;
				this.X = x;
				this.Y = y;
			}

			public ChessSquarePiece ChessSquarePiece { get; private set; }
			public int X { get; private set; }
			public int Y { get; private set; }
		}

		private ChessSquarePieceArray pieces;
		private ChessSquare kingInDangerSquare;
		private DTImmutableList<ChessSquare> previousMoveSquares;
		private ChessSquare selectedPieceSquare;
		private DTImmutableList<ChessSquare> possibleMoveSquares;
		private PotentialNukeSquaresInfo potentialNukeSquaresInfo;
		private ChessSquare hoverSquare;
		private HoverPieceInfo hoverPieceInfo;
		private bool renderFromWhitePerspective;

		private int? nukeAnimationMicroseconds;
		private ChessSquare nukeCenter;
		private DTImmutableList<ChessSquare> nukedSquares;

		private ColorTheme colorTheme;

		private const int NUKE_BEGIN_LANDING_MICROSECONDS = 1000 * 1000;
		private const int NUKE_IMPACT_MICROSECONDS = 1300 * 1000;
		private const int NUKE_EXPLOSION_FINISHED_MICROSECONDS = 1600 * 1000;
		private const int NUKE_ANIMATION_COMPLETED_MICROSECONDS = 1650 * 1000;

		private ChessPiecesRenderer(
			ChessSquarePieceArray pieces,
			ChessSquare kingInDangerSquare,
			DTImmutableList<ChessSquare> previousMoveSquares,
			ChessSquare selectedPieceSquare,
			DTImmutableList<ChessSquare> possibleMoveSquares,
			PotentialNukeSquaresInfo potentialNukeSquaresInfo,
			ChessSquare hoverSquare,
			HoverPieceInfo hoverPieceInfo,
			bool renderFromWhitePerspective,
			int? nukeAnimationMicroseconds,
			ChessSquare nukeCenter,
			DTImmutableList<ChessSquare> nukedSquares,
			ColorTheme colorTheme)
		{
			this.pieces = pieces;
			this.kingInDangerSquare = kingInDangerSquare;
			this.previousMoveSquares = previousMoveSquares;
			this.selectedPieceSquare = selectedPieceSquare;
			this.possibleMoveSquares = possibleMoveSquares;
			this.potentialNukeSquaresInfo = potentialNukeSquaresInfo;
			this.hoverSquare = hoverSquare;
			this.hoverPieceInfo = hoverPieceInfo;
			this.renderFromWhitePerspective = renderFromWhitePerspective;
			this.nukeAnimationMicroseconds = nukeAnimationMicroseconds;
			this.nukeCenter = nukeCenter;
			this.nukedSquares = nukedSquares;
			this.colorTheme = colorTheme;
		}

		public static ChessPiecesRenderer GetChessPiecesRenderer(
			ChessSquarePieceArray pieces,
			ChessSquare kingInDangerSquare,
			DTImmutableList<ChessSquare> previousMoveSquares,
			bool renderFromWhitePerspective,
			ColorTheme colorTheme)
		{
			return new ChessPiecesRenderer(
				pieces: pieces,
				kingInDangerSquare: kingInDangerSquare,
				previousMoveSquares: previousMoveSquares,
				selectedPieceSquare: null,
				possibleMoveSquares: DTImmutableList<ChessSquare>.EmptyList(),
				potentialNukeSquaresInfo: null,
				hoverSquare: null,
				hoverPieceInfo: null,
				renderFromWhitePerspective: renderFromWhitePerspective,
				nukeAnimationMicroseconds: null,
				nukeCenter: null,
				nukedSquares: null,
				colorTheme: colorTheme);
		}

		public ChessPiecesRenderer LandNuke(ChessSquare nukeCenter)
		{
			List<ChessSquare> nukedSquares = TacticalNukeUtil.GetNukedSquares(nukeCenter);

			return new ChessPiecesRenderer(
				pieces: this.pieces,
				kingInDangerSquare: this.kingInDangerSquare,
				previousMoveSquares: this.previousMoveSquares,
				selectedPieceSquare: this.selectedPieceSquare,
				possibleMoveSquares: this.possibleMoveSquares,
				potentialNukeSquaresInfo: this.potentialNukeSquaresInfo,
				hoverSquare: this.hoverSquare,
				hoverPieceInfo: this.hoverPieceInfo,
				renderFromWhitePerspective: this.renderFromWhitePerspective,
				nukeAnimationMicroseconds: 0,
				nukeCenter: nukeCenter,
				nukedSquares: new DTImmutableList<ChessSquare>(nukedSquares),
				colorTheme: this.colorTheme);
		}
		
		public ChessPiecesRenderer ProcessFrame(
			ChessSquarePieceArray pieces,
			ChessSquare kingInDangerSquare,
			DTImmutableList<ChessSquare> previousMoveSquares,
			ChessSquare selectedPieceSquare,
			DTImmutableList<ChessSquare> possibleMoveSquares,
			PotentialNukeSquaresInfo potentialNukeSquaresInfo,
			ChessSquare hoverSquare,
			HoverPieceInfo hoverPieceInfo,
			int elapsedMicrosPerFrame)
		{
			int? newNukeAnimationMicroseconds;
			if (this.nukeAnimationMicroseconds == null)
				newNukeAnimationMicroseconds = null;
			else
				newNukeAnimationMicroseconds = this.nukeAnimationMicroseconds.Value + elapsedMicrosPerFrame;

			if (newNukeAnimationMicroseconds.HasValue && newNukeAnimationMicroseconds.Value > NUKE_ANIMATION_COMPLETED_MICROSECONDS)
				newNukeAnimationMicroseconds = NUKE_ANIMATION_COMPLETED_MICROSECONDS + 1;

			return new ChessPiecesRenderer(
				pieces: pieces,
				kingInDangerSquare: kingInDangerSquare,
				previousMoveSquares: previousMoveSquares,
				selectedPieceSquare: selectedPieceSquare,
				possibleMoveSquares: possibleMoveSquares,
				potentialNukeSquaresInfo: potentialNukeSquaresInfo,
				hoverSquare: hoverSquare,
				hoverPieceInfo: hoverPieceInfo,
				renderFromWhitePerspective: this.renderFromWhitePerspective,
				nukeAnimationMicroseconds: newNukeAnimationMicroseconds,
				nukeCenter: this.nukeCenter,
				nukedSquares: this.nukedSquares,
				colorTheme: this.colorTheme);
		}

		public bool HasNukeLanded()
		{
			return this.nukeAnimationMicroseconds.HasValue && this.nukeAnimationMicroseconds.Value >= NUKE_IMPACT_MICROSECONDS;
		}

		public bool HasNukeFinished()
		{
			return this.nukeAnimationMicroseconds.HasValue && this.nukeAnimationMicroseconds.Value >= NUKE_ANIMATION_COMPLETED_MICROSECONDS;
		}
		
		public static ChessSquare GetHoverSquare(
			IMouse mouseInput, 
			bool renderFromWhitePerspective,
			IDisplayProcessing<GameImage> displayProcessing)
		{
			int width = displayProcessing.GetWidth(GameImage.WhitePawn) * GameImageUtil.ChessPieceScalingFactor / 128;
			int height = displayProcessing.GetHeight(GameImage.WhitePawn) * GameImageUtil.ChessPieceScalingFactor / 128;

			int mouseX = mouseInput.GetX();
			int mouseY = mouseInput.GetY();

			if (mouseX < 0 || mouseY < 0)
				return null;

			int i = mouseX / width;
			int j = mouseY / height;

			if (0 <= i && i < 8 && 0 <= j && j < 8)
			{
				if (renderFromWhitePerspective)
					return new ChessSquare(i, j);
				else
					return new ChessSquare(7 - i, 7 - j);
			}

			return null;
		}
		
		private static ChessSquare GetRenderSquare(int i, int j, bool renderFromWhitePerspective)
		{
			if (renderFromWhitePerspective)
				return new ChessSquare(i, j);

			return new ChessSquare(7 - i, 7 - j);
		}

		private static ChessSquare GetRenderSquare(ChessSquare square, bool renderFromWhitePerspective)
		{
			return GetRenderSquare(i: square.File, j: square.Rank, renderFromWhitePerspective: renderFromWhitePerspective);
		}

		public static DTColor GetDarkSquareColor(ColorTheme colorTheme)
		{
			switch (colorTheme)
			{
				case ColorTheme.Initial: return new DTColor(140, 89, 11);
				case ColorTheme.Progress1: return new DTColor(140, 80, 11);
				case ColorTheme.Progress2: return new DTColor(140, 71, 11);
				case ColorTheme.Progress3: return new DTColor(140, 63, 11);
				case ColorTheme.Final: return new DTColor(140, 54, 11);
				default: throw new Exception();
			}
		}

		public static DTColor GetLightSquareColor(ColorTheme colorTheme)
		{
			switch (colorTheme)
			{
				case ColorTheme.Initial: return new DTColor(194, 146, 74);
				case ColorTheme.Progress1: return new DTColor(194, 138, 74);
				case ColorTheme.Progress2: return new DTColor(194, 130, 74);
				case ColorTheme.Progress3: return new DTColor(194, 122, 74);
				case ColorTheme.Final: return new DTColor(194, 114, 74);
				default: throw new Exception();
			}
		}

		private static DTColor GetPossibleMoveSquareColor(ColorTheme colorTheme)
		{
			switch (colorTheme)
			{
				case ColorTheme.Initial: return new DTColor(0, 128, 0);
				case ColorTheme.Progress1: return new DTColor(0, 128, 0);
				case ColorTheme.Progress2: return new DTColor(226, 255, 94);
				case ColorTheme.Progress3: return new DTColor(226, 255, 94);
				case ColorTheme.Final: return new DTColor(226, 255, 94);
				default: throw new Exception();
			}
		}

		public void Render(IDisplayOutput<GameImage, GameFont> displayOutput, ChessPiecesRendererPieceAnimation chessPiecesRendererPieceAnimation)
		{
			int width = displayOutput.GetWidth(GameImage.WhitePawn) * GameImageUtil.ChessPieceScalingFactor / 128;
			int height = displayOutput.GetHeight(GameImage.WhitePawn) * GameImageUtil.ChessPieceScalingFactor / 128;

			DTColor darkSquareColor = GetDarkSquareColor(colorTheme: this.colorTheme);
			DTColor lightSquareColor = GetLightSquareColor(colorTheme: this.colorTheme);

			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
				{					
					ChessSquare renderSquare = GetRenderSquare(i: i, j: j, renderFromWhitePerspective: this.renderFromWhitePerspective);

					displayOutput.DrawRectangle(
						x: renderSquare.File * width,
						y: renderSquare.Rank * height,
						width: width,
						height: height,
						color: (i + j) % 2 == 0 ? darkSquareColor : lightSquareColor,
						fill: true);
				}
			}

			for (int i = 0; i < this.previousMoveSquares.Count; i++)
			{
				ChessSquare previousMoveSquare = this.previousMoveSquares[i];
				ChessSquare renderSquare = GetRenderSquare(square: previousMoveSquare, renderFromWhitePerspective: this.renderFromWhitePerspective);
				displayOutput.DrawRectangle(
					x: renderSquare.File * width,
					y: renderSquare.Rank * height,
					width: width,
					height: height,
					color: (previousMoveSquare.File + previousMoveSquare.Rank) % 2 == 0 ? new DTColor(134, 109, 70) : new DTColor(161, 137, 101),
					fill: true);
			}
			
			if (this.kingInDangerSquare != null)
			{
				ChessSquare renderSquare = GetRenderSquare(square: this.kingInDangerSquare, renderFromWhitePerspective: this.renderFromWhitePerspective);
				displayOutput.DrawRectangle(
					x: renderSquare.File * width,
					y: renderSquare.Rank * height,
					width: width,
					height: height,
					color: (this.kingInDangerSquare.File + this.kingInDangerSquare.Rank) % 2 == 0 ? new DTColor(198, 44, 5) : new DTColor(225, 73, 37),
					fill: true);
			}

			ChessPiecesRendererPieceAnimation.PieceAnimation[][] pieceAnimations = chessPiecesRendererPieceAnimation.GetPieceAnimations();

			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					if (pieceAnimations[i][j] != null)
					{
						continue;
					}

					ChessSquarePiece square = this.pieces.GetPiece(i, j);

					ChessSquare renderSquare = GetRenderSquare(i: i, j: j, renderFromWhitePerspective: this.renderFromWhitePerspective);

					if (square == ChessSquarePiece.Empty)
						continue;

					displayOutput.DrawImageRotatedClockwise(
						image: GameImageUtil.GetImage(piece: square),
						x: renderSquare.File * width,
						y: renderSquare.Rank * height,
						degreesScaled: 0,
						scalingFactorScaled: GameImageUtil.ChessPieceScalingFactor);
				}
			}

			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					if (pieceAnimations[i][j] == null)
					{
						continue;
					}

					ChessSquare originRenderSquare = GetRenderSquare(square: pieceAnimations[i][j].OriginSquare, renderFromWhitePerspective: this.renderFromWhitePerspective);
					ChessSquare destinationRenderSquare = GetRenderSquare(i: i, j: j, renderFromWhitePerspective: this.renderFromWhitePerspective);

					long originX = originRenderSquare.File * width;
					long originY = originRenderSquare.Rank * height;

					long destinationX = destinationRenderSquare.File * width;
					long destinationY = destinationRenderSquare.Rank * height;

					int renderX = (int) (originX + (destinationX - originX) * ((long)pieceAnimations[i][j].ElapsedMicros) / ((long)ChessPiecesRendererPieceAnimation.PieceAnimation.ANIMATION_DURATION_MICROS));
					int renderY = (int) (originY + (destinationY - originY) * ((long)pieceAnimations[i][j].ElapsedMicros) / ((long)ChessPiecesRendererPieceAnimation.PieceAnimation.ANIMATION_DURATION_MICROS));

					displayOutput.DrawImageRotatedClockwise(
						image: GameImageUtil.GetImage(piece: pieceAnimations[i][j].Piece),
						x: renderX,
						y: renderY,
						degreesScaled: 0,
						scalingFactorScaled: GameImageUtil.ChessPieceScalingFactor);
				}
			}

			if (this.selectedPieceSquare != null)
			{
				ChessSquare renderSquare = GetRenderSquare(square: this.selectedPieceSquare, renderFromWhitePerspective: this.renderFromWhitePerspective);
				displayOutput.DrawRectangle(
					x: renderSquare.File * width,
					y: renderSquare.Rank * height,
					width: width,
					height: height,
					color: new DTColor(0, 128, 0, 128),
					fill: true);
			}

			DTColor possibleMoveSquareColor = GetPossibleMoveSquareColor(colorTheme: this.colorTheme);
			for (int i = 0; i < this.possibleMoveSquares.Count; i++)
			{
				ChessSquare possibleMoveSquare = this.possibleMoveSquares[i];
				ChessSquare renderSquare = GetRenderSquare(square: possibleMoveSquare, renderFromWhitePerspective: this.renderFromWhitePerspective);
				for (int x = 0; x < 7; x++)
				{
					int rectangleWidth = width + 1 - 2 * x;
					int rectangleHeight = height + 1 - 2 * x;

					if (rectangleWidth > 0 && rectangleHeight > 0)
						displayOutput.DrawRectangle(
							x: renderSquare.File * width + x,
							y: renderSquare.Rank * height + x,
							width: rectangleWidth,
							height: rectangleHeight,
							color: new DTColor(possibleMoveSquareColor.R, possibleMoveSquareColor.G, possibleMoveSquareColor.B, 128 - 20 * x),
							fill: false);
				}
			}

			if (this.hoverSquare != null)
			{
				ChessSquare renderSquare = GetRenderSquare(square: this.hoverSquare, renderFromWhitePerspective: this.renderFromWhitePerspective);
				displayOutput.DrawRectangle(
					x: renderSquare.File * width,
					y: renderSquare.Rank * height,
					width: width,
					height: height,
					color: new DTColor(0, 0, 128, 128),
					fill: true);
			}
			
			if (this.potentialNukeSquaresInfo != null)
			{
				for (int i = 0; i < this.potentialNukeSquaresInfo.PotentialNukeSquares.Count; i++)
				{
					ChessSquare potentialNukeSquare = this.potentialNukeSquaresInfo.PotentialNukeSquares[i];
					ChessSquare renderSquare = GetRenderSquare(square: potentialNukeSquare, renderFromWhitePerspective: this.renderFromWhitePerspective);
					displayOutput.DrawRectangle(
						x: renderSquare.File * width,
						y: renderSquare.Rank * height,
						width: width,
						height: height,
						color: this.potentialNukeSquaresInfo.IsNukeLocationValid ? new DTColor(0, 200, 0, 200) : new DTColor(200, 0, 0, 200),
						fill: true);
				}
			}

			if (this.hoverPieceInfo != null)
			{
				displayOutput.DrawImageRotatedClockwise(
					image: GameImageUtil.GetImage(piece: this.hoverPieceInfo.ChessSquarePiece),
					x: this.hoverPieceInfo.X - width / 2,
					y: this.hoverPieceInfo.Y - height / 2,
					degreesScaled: 0,
					scalingFactorScaled: GameImageUtil.ChessPieceScalingFactor);
			}
			
			this.RenderNukeAnimation(displayOutput: displayOutput);
		}
		
		private void RenderNukeAnimation(IDisplayOutput<GameImage, GameFont> displayOutput)
		{
			if (this.nukeAnimationMicroseconds == null)
				return;
			
			int width = displayOutput.GetWidth(GameImage.WhitePawn) * GameImageUtil.ChessPieceScalingFactor / 128;
			int height = displayOutput.GetHeight(GameImage.WhitePawn) * GameImageUtil.ChessPieceScalingFactor / 128;

			if (this.nukeAnimationMicroseconds.Value <= NUKE_IMPACT_MICROSECONDS)
			{
				if (this.nukeAnimationMicroseconds.Value >= NUKE_BEGIN_LANDING_MICROSECONDS)
				{
					ChessSquare nukeRenderCenter = GetRenderSquare(this.nukeCenter.File, this.nukeCenter.Rank, this.renderFromWhitePerspective);

					int rocketWidth = displayOutput.GetWidth(GameImage.Nuke_Ready);

					int rocketFireScalingFactor = 256;
					int rocketFireWidthOriginal = displayOutput.GetWidth(GameImage.Nuke_RocketFire);
					int rocketFireHeightOriginal = displayOutput.GetHeight(GameImage.Nuke_RocketFire);
					int rocketFireWidthScaled = rocketFireWidthOriginal * rocketFireScalingFactor / 128;
					int rocketFireHeightScaled = rocketFireHeightOriginal * rocketFireScalingFactor / 128;
					int endingY = nukeRenderCenter.Rank * height + height / 2;
					int startingY = endingY + GlobalConstants.WINDOW_HEIGHT;
					int totalDistanceY = startingY - endingY;
					int y = (int)((((long)NUKE_IMPACT_MICROSECONDS) - ((long)this.nukeAnimationMicroseconds.Value)) * ((long)totalDistanceY) / (((long)NUKE_IMPACT_MICROSECONDS) - ((long)NUKE_BEGIN_LANDING_MICROSECONDS)) + ((long)endingY));

					int x = nukeRenderCenter.File * width + width / 2;

					displayOutput.DrawImageRotatedClockwise(
						image: GameImage.Nuke_Ready,
						x: x - (displayOutput.GetWidth(GameImage.Nuke_Ready) >> 1),
						y: y,
						degreesScaled: 180 * 128,
						scalingFactorScaled: 128);

					displayOutput.DrawImageRotatedClockwise(
						image: GameImage.Nuke_RocketFire,
						x: x - (displayOutput.GetWidth(GameImage.Nuke_Ready) >> 1) + (rocketWidth - rocketFireWidthScaled) / 2,
						y: y + displayOutput.GetHeight(GameImage.Nuke_Ready),
						degreesScaled: 180 * 128,
						scalingFactorScaled: rocketFireScalingFactor);
				}
			}
			else if (this.nukeAnimationMicroseconds.Value <= NUKE_EXPLOSION_FINISHED_MICROSECONDS)
			{
				int elapsedTime = this.nukeAnimationMicroseconds.Value - NUKE_IMPACT_MICROSECONDS;
				int totalTime = NUKE_EXPLOSION_FINISHED_MICROSECONDS - NUKE_IMPACT_MICROSECONDS;

				int spriteNum = elapsedTime * 9 / totalTime + 1;
				if (spriteNum < 1)
					spriteNum = 1;
				if (spriteNum > 9)
					spriteNum = 9;

				GameImage explosionImage;

				switch (spriteNum)
				{
					case 1: explosionImage = GameImage.Nuke_Explosion1; break;
					case 2: explosionImage = GameImage.Nuke_Explosion2; break;
					case 3: explosionImage = GameImage.Nuke_Explosion3; break;
					case 4: explosionImage = GameImage.Nuke_Explosion4; break;
					case 5: explosionImage = GameImage.Nuke_Explosion5; break;
					case 6: explosionImage = GameImage.Nuke_Explosion6; break;
					case 7: explosionImage = GameImage.Nuke_Explosion7; break;
					case 8: explosionImage = GameImage.Nuke_Explosion8; break;
					case 9: explosionImage = GameImage.Nuke_Explosion9; break;
					default: throw new Exception();
				}

				ChessSquare nukeRenderCenter = GetRenderSquare(this.nukeCenter.File, this.nukeCenter.Rank, this.renderFromWhitePerspective);

				int explosionScalingFactor = 128 * 2;

				displayOutput.DrawImageRotatedClockwise(
					image: explosionImage,
					x: nukeRenderCenter.File * width + width / 2 - displayOutput.GetWidth(explosionImage) * explosionScalingFactor / 128 / 2,
					y: nukeRenderCenter.Rank * height + height / 2 - displayOutput.GetHeight(explosionImage) * explosionScalingFactor / 128 / 2,
					degreesScaled: 0,
					scalingFactorScaled: explosionScalingFactor);
			}
		}
	}
}
