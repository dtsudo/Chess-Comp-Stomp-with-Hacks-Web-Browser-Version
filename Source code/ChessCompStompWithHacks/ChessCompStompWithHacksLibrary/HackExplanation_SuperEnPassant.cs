
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System;
	using System.Collections.Generic;

	public class HackExplanation_SuperEnPassant : HackExplanationFrameUtil.IHackExplanation
	{
		private ChessPiecesRenderer chessPiecesRenderer;
		private ChessPiecesRendererPieceAnimation chessPiecesRendererPieceAnimation;

		private int moveCooldown;

		private IDTRandom random;

		private ColorTheme colorTheme;

		private ChessSquarePieceArray board;

		private ChessSquare pawnSquare;
		private ChessSquare opponentSquare;

		private DTImmutableList<ChessSquare> previousMoveSquares;
		private DTImmutableList<ChessSquare> possibleMoveSquares;

		private ChessPiecesRendererFadeOutFadeIn chessPiecesRendererFadeOutFadeIn;
		
		public HackExplanation_SuperEnPassant(ColorTheme colorTheme, IDTRandom random)
		{
			this.chessPiecesRendererFadeOutFadeIn = null;

			this.colorTheme = colorTheme;

			this.random = random;

			this.moveCooldown = HackExplanationFrameUtil.ELAPSED_MICROS_BEFORE_PIECE_MOVES;

			this.PopulateInitialBoard(random: random);
			
			this.chessPiecesRenderer = ChessPiecesRenderer.GetChessPiecesRenderer(
				pieces: this.board,
				kingInDangerSquare: null,
				previousMoveSquares: this.previousMoveSquares,
				renderFromWhitePerspective: true,
				colorTheme: colorTheme);

			this.chessPiecesRendererPieceAnimation = ChessPiecesRendererPieceAnimation.EmptyChessPiecesRendererPieceAnimation();
		}

		private static List<ChessSquare> GetPossibleMoveSquares(ChessSquare pawnSquare, ChessSquare opponentSquare)
		{
			List<ChessSquare> list = new List<ChessSquare>();

			list.Add(new ChessSquare(file: opponentSquare.File, rank: pawnSquare.Rank + 1));
			list.Add(new ChessSquare(file: pawnSquare.File, rank: pawnSquare.Rank + 1));

			if (pawnSquare.Rank == 1)
				list.Add(new ChessSquare(file: pawnSquare.File, rank: pawnSquare.Rank + 2));

			return list;
		}

		private void PopulateInitialBoard(IDTRandom random)
		{
			int pawnFile = random.NextInt(8);
			int pawnRank = random.NextInt(6) + 1;

			this.pawnSquare = new ChessSquare(file: pawnFile, rank: pawnRank);

			int opponentFile;

			if (pawnFile == 0)
				opponentFile = 1;
			else if (pawnFile == 7)
				opponentFile = 6;
			else
				opponentFile = random.NextBool() ? (pawnFile - 1) : (pawnFile + 1);

			this.opponentSquare = new ChessSquare(file: opponentFile, rank: pawnRank);

			ChessSquarePiece[][] boardArray = new ChessSquarePiece[8][];
			for (int i = 0; i < 8; i++)
			{
				boardArray[i] = new ChessSquarePiece[8];
				for (int j = 0; j < 8; j++)
					boardArray[i][j] = ChessSquarePiece.Empty;
			}

			boardArray[this.pawnSquare.File][this.pawnSquare.Rank] = ChessSquarePiece.WhitePawn;

			ChessSquarePiece opponentPiece;

			switch (random.NextInt(5))
			{
				case 0: opponentPiece = ChessSquarePiece.BlackPawn; break;
				case 1: opponentPiece = ChessSquarePiece.BlackRook; break;
				case 2: opponentPiece = ChessSquarePiece.BlackKnight; break;
				case 3: opponentPiece = ChessSquarePiece.BlackBishop; break;
				case 4: opponentPiece = ChessSquarePiece.BlackQueen; break;
				default: throw new Exception();
			}

			boardArray[this.opponentSquare.File][this.opponentSquare.Rank] = opponentPiece;

			this.board = new ChessSquarePieceArray(boardArray);

			this.previousMoveSquares = DTImmutableList<ChessSquare>.EmptyList();
			this.possibleMoveSquares = DTImmutableList<ChessSquare>.EmptyList();
		}

		public void ProcessFrame(IMouse mouseInput, IMouse previousMouseInput, IDisplayProcessing<ChessImage> displayProcessing, int elapsedMicrosPerFrame)
		{
			if (this.chessPiecesRendererFadeOutFadeIn != null)
			{
				this.chessPiecesRendererFadeOutFadeIn.ProcessFrame(elapsedMicrosPerFrame: elapsedMicrosPerFrame);

				if (this.chessPiecesRendererFadeOutFadeIn.HasFinishedFadingOut() && this.previousMoveSquares.Count > 0)
					this.PopulateInitialBoard(random: this.random);

				if (this.chessPiecesRendererFadeOutFadeIn.HasFinishedFadingIn())
					this.chessPiecesRendererFadeOutFadeIn = null;
			}
			else
			{
				this.moveCooldown -= elapsedMicrosPerFrame;
				if (this.moveCooldown <= 0)
				{
					this.moveCooldown += HackExplanationFrameUtil.ELAPSED_MICROS_BEFORE_PIECE_MOVES;
					if (this.moveCooldown <= 0)
						this.moveCooldown = 0;

					if (this.previousMoveSquares.Count > 0)
						this.chessPiecesRendererFadeOutFadeIn = new ChessPiecesRendererFadeOutFadeIn(colorTheme: this.colorTheme);
					else if (this.possibleMoveSquares.Count == 0)
						this.possibleMoveSquares = new DTImmutableList<ChessSquare>(GetPossibleMoveSquares(pawnSquare: this.pawnSquare, opponentSquare: this.opponentSquare));
					else
					{
						ChessSquare endingSquare = new ChessSquare(file: this.opponentSquare.File, rank: this.pawnSquare.Rank + 1);

						this.chessPiecesRendererPieceAnimation = this.chessPiecesRendererPieceAnimation.AddRawMove(
							startingFile: this.pawnSquare.File,
							startingRank: this.pawnSquare.Rank,
							endingFile: endingSquare.File,
							endingRank: endingSquare.Rank,
							piece: ChessSquarePiece.WhitePawn);

						this.board = this.board.SetPiece(file: this.pawnSquare.File, rank: this.pawnSquare.Rank, piece: ChessSquarePiece.Empty);
						this.board = this.board.SetPiece(file: this.opponentSquare.File, rank: this.opponentSquare.Rank, piece: ChessSquarePiece.Empty);
						this.board = this.board.SetPiece(file: endingSquare.File, rank: endingSquare.Rank, piece: endingSquare.Rank == 7 ? ChessSquarePiece.WhiteQueen : ChessSquarePiece.WhitePawn);

						this.previousMoveSquares = new DTImmutableList<ChessSquare>(new HashSet<ChessSquare>()
						{
							this.pawnSquare,
							endingSquare
						});
						this.possibleMoveSquares = DTImmutableList<ChessSquare>.EmptyList();
					}
				}
			}

			this.chessPiecesRenderer = this.chessPiecesRenderer.ProcessFrame(
				pieces: this.board,
				kingInDangerSquare: null,
				previousMoveSquares: this.previousMoveSquares,
				selectedPieceSquare: this.possibleMoveSquares.Count > 0 ? this.pawnSquare : null,
				possibleMoveSquares: this.possibleMoveSquares,
				potentialNukeSquaresInfo: null,
				hoverSquare: null,
				hoverPieceInfo: null,
				elapsedMicrosPerFrame: elapsedMicrosPerFrame);

			this.chessPiecesRendererPieceAnimation = this.chessPiecesRendererPieceAnimation.ProcessFrame(
				elapsedMicrosPerFrame: elapsedMicrosPerFrame);
		}

		public void Render(IDisplayOutput<ChessImage, ChessFont> displayOutput)
		{
			displayOutput.DrawText(
				x: 333,
				y: HackExplanationFrameUtil.TITLE_TEXT_Y_OFFSET,
				text: Hack.SuperEnPassant.GetHackNameForHackExplanationPanel(),
				font: ChessFont.ChessFont20Pt,
				color: DTColor.Black());

			string explanation = "Your pawns may capture" + "\n"
				+ "enemy pieces that are" + "\n"
				+ "horizontally adjacent to the" + "\n"
				+ "pawn." + "\n"
				+ "\n"
				+ "Super en passant is allowed" + "\n"
				+ "regardless of when or how" + "\n"
				+ "the enemy piece moved." + "\n"
				+ "\n"
				+ "The pawn may capture super" + "\n"
				+ "en passant regardless of" + "\n"
				+ "which rank the pawn is on.";

			displayOutput.DrawText(
				x: HackExplanationFrameUtil.EXPLANATION_TEXT_X_OFFSET,
				y: HackExplanationFrameUtil.EXPLANATION_TEXT_Y_OFFSET,
				text: explanation,
				font: ChessFont.ChessFont16Pt,
				color: DTColor.Black());

			this.chessPiecesRenderer.Render(
				displayOutput: new TranslatedDisplayOutput<ChessImage, ChessFont>(
					display: displayOutput,
					xOffsetInPixels: HackExplanationFrameUtil.CHESS_PIECES_RENDERER_X_OFFSET,
					yOffsetInPixels: HackExplanationFrameUtil.CHESS_PIECES_RENDERER_Y_OFFSET),
				chessPiecesRendererPieceAnimation: this.chessPiecesRendererPieceAnimation);

			if (this.chessPiecesRendererFadeOutFadeIn != null)
				this.chessPiecesRendererFadeOutFadeIn.Render(displayOutput: new TranslatedDisplayOutput<ChessImage, ChessFont>(
					display: displayOutput,
					xOffsetInPixels: HackExplanationFrameUtil.CHESS_PIECES_RENDERER_X_OFFSET,
					yOffsetInPixels: HackExplanationFrameUtil.CHESS_PIECES_RENDERER_Y_OFFSET));
		}
	}
}
