
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
			DTImmutableList<ChessSquare> nukedSquares)
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
		}

		public static ChessPiecesRenderer GetChessPiecesRenderer(
			ChessSquarePieceArray pieces,
			ChessSquare kingInDangerSquare,
			DTImmutableList<ChessSquare> previousMoveSquares,
			bool renderFromWhitePerspective)
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
				nukedSquares: null);
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
				nukedSquares: new DTImmutableList<ChessSquare>(nukedSquares));
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
				nukedSquares: this.nukedSquares);
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
			IDisplayProcessing<ChessImage> displayProcessing)
		{
			int width = displayProcessing.GetWidth(ChessImage.WhitePawn) * ChessImageUtil.ChessPieceScalingFactor / 128;
			int height = displayProcessing.GetHeight(ChessImage.WhitePawn) * ChessImageUtil.ChessPieceScalingFactor / 128;

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

		public void Render(IDisplayOutput<ChessImage, ChessFont> displayOutput)
		{
			int width = displayOutput.GetWidth(ChessImage.WhitePawn) * ChessImageUtil.ChessPieceScalingFactor / 128;
			int height = displayOutput.GetHeight(ChessImage.WhitePawn) * ChessImageUtil.ChessPieceScalingFactor / 128;

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
						color: (i + j) % 2 == 0 ? new DTColor(140, 89, 11) : new DTColor(194, 146, 74),
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
					color: new DTColor(128, 128, 128, 128),
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
					color: new DTColor(255, 0, 0, 128),
					fill: true);
			}
			
			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					ChessSquarePiece square = this.pieces.GetPiece(i, j);

					ChessSquare renderSquare = GetRenderSquare(i: i, j: j, renderFromWhitePerspective: this.renderFromWhitePerspective);
					
					if (square == ChessSquarePiece.Empty)
						continue;

					displayOutput.DrawImageRotatedClockwise(
						image: ChessImageUtil.GetImage(piece: square),
						x: renderSquare.File * width,
						y: renderSquare.Rank * height,
						degreesScaled: 0,
						scalingFactorScaled: ChessImageUtil.ChessPieceScalingFactor);
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

			for (int i = 0; i < this.possibleMoveSquares.Count; i++)
			{
				ChessSquare possibleMoveSquare = this.possibleMoveSquares[i];
				ChessSquare renderSquare = GetRenderSquare(square: possibleMoveSquare, renderFromWhitePerspective: this.renderFromWhitePerspective);
				displayOutput.DrawThickRectangle(
					x: renderSquare.File * width,
					y: renderSquare.Rank * height,
					width: width,
					height: height,
					additionalThickness: 1,
					color: new DTColor(0, 128, 0, 128),
					fill: false);
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
					image: ChessImageUtil.GetImage(piece: this.hoverPieceInfo.ChessSquarePiece),
					x: this.hoverPieceInfo.X - width / 2,
					y: this.hoverPieceInfo.Y - height / 2,
					degreesScaled: 0,
					scalingFactorScaled: ChessImageUtil.ChessPieceScalingFactor);
			}
			
			this.RenderNukeAnimation(displayOutput: displayOutput);
		}
		
		private void RenderNukeAnimation(IDisplayOutput<ChessImage, ChessFont> displayOutput)
		{
			if (this.nukeAnimationMicroseconds == null)
				return;
			
			int width = displayOutput.GetWidth(ChessImage.WhitePawn) * ChessImageUtil.ChessPieceScalingFactor / 128;
			int height = displayOutput.GetHeight(ChessImage.WhitePawn) * ChessImageUtil.ChessPieceScalingFactor / 128;

			if (this.nukeAnimationMicroseconds.Value <= NUKE_IMPACT_MICROSECONDS)
			{
				if (this.nukeAnimationMicroseconds.Value >= NUKE_BEGIN_LANDING_MICROSECONDS)
				{
					ChessSquare nukeRenderCenter = GetRenderSquare(this.nukeCenter.File, this.nukeCenter.Rank, this.renderFromWhitePerspective);

					int rocketWidth = displayOutput.GetWidth(ChessImage.Nuke_Ready);

					int rocketFireScalingFactor = 256;
					int rocketFireWidthOriginal = displayOutput.GetWidth(ChessImage.Nuke_RocketFire);
					int rocketFireHeightOriginal = displayOutput.GetHeight(ChessImage.Nuke_RocketFire);
					int rocketFireWidthScaled = rocketFireWidthOriginal * rocketFireScalingFactor / 128;
					int rocketFireHeightScaled = rocketFireHeightOriginal * rocketFireScalingFactor / 128;
					int endingY = nukeRenderCenter.Rank * height + height / 2;
					int startingY = endingY + ChessCompStompWithHacks.WINDOW_HEIGHT;
					int totalDistanceY = startingY - endingY;
					int y = (int)((((long)NUKE_IMPACT_MICROSECONDS) - ((long)this.nukeAnimationMicroseconds.Value)) * ((long)totalDistanceY) / (((long)NUKE_IMPACT_MICROSECONDS) - ((long)NUKE_BEGIN_LANDING_MICROSECONDS)) + ((long)endingY));

					int x = nukeRenderCenter.File * width + width / 2;

					displayOutput.DrawImageRotatedClockwise(
						image: ChessImage.Nuke_Ready,
						x: x - (displayOutput.GetWidth(ChessImage.Nuke_Ready) >> 1),
						y: y,
						degreesScaled: 180 * 128,
						scalingFactorScaled: 128);

					displayOutput.DrawImageRotatedClockwise(
						image: ChessImage.Nuke_RocketFire,
						x: x - (displayOutput.GetWidth(ChessImage.Nuke_Ready) >> 1) + (rocketWidth - rocketFireWidthScaled) / 2,
						y: y + displayOutput.GetHeight(ChessImage.Nuke_Ready),
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

				ChessImage explosionImage;

				switch (spriteNum)
				{
					case 1: explosionImage = ChessImage.Nuke_Explosion1; break;
					case 2: explosionImage = ChessImage.Nuke_Explosion2; break;
					case 3: explosionImage = ChessImage.Nuke_Explosion3; break;
					case 4: explosionImage = ChessImage.Nuke_Explosion4; break;
					case 5: explosionImage = ChessImage.Nuke_Explosion5; break;
					case 6: explosionImage = ChessImage.Nuke_Explosion6; break;
					case 7: explosionImage = ChessImage.Nuke_Explosion7; break;
					case 8: explosionImage = ChessImage.Nuke_Explosion8; break;
					case 9: explosionImage = ChessImage.Nuke_Explosion9; break;
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
