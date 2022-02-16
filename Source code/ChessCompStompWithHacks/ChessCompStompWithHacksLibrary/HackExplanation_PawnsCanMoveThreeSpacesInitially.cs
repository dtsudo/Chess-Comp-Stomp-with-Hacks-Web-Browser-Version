
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System.Collections.Generic;

	public class HackExplanation_PawnsCanMoveThreeSpacesInitially : HackExplanationFrameUtil.IHackExplanation
	{
		private ChessPiecesRenderer chessPiecesRenderer;
		private ChessPiecesRendererPieceAnimation chessPiecesRendererPieceAnimation;
		
		private int moveCooldown;

		private IDTRandom random;

		private ColorTheme colorTheme;

		private ChessSquarePieceArray board;

		private DTImmutableList<ChessSquare> previousMoveSquares;
		private DTImmutableList<ChessSquare> possibleMoveSquares;

		private ChessPiecesRendererFadeOutFadeIn chessPiecesRendererFadeOutFadeIn;

		public HackExplanation_PawnsCanMoveThreeSpacesInitially(ColorTheme colorTheme, IDTRandom random)
		{
			this.chessPiecesRendererFadeOutFadeIn = null;

			this.colorTheme = colorTheme;

			this.random = random;
			
			this.moveCooldown = HackExplanationFrameUtil.ELAPSED_MICROS_BEFORE_PIECE_MOVES;
			
			this.board = GetInitialBoard();

			this.previousMoveSquares = DTImmutableList<ChessSquare>.EmptyList();
			this.possibleMoveSquares = DTImmutableList<ChessSquare>.EmptyList();

			this.chessPiecesRenderer = ChessPiecesRenderer.GetChessPiecesRenderer(
				pieces: this.board,
				kingInDangerSquare: null,
				previousMoveSquares: this.previousMoveSquares,
				renderFromWhitePerspective: true,
				colorTheme: colorTheme);
			
			this.chessPiecesRendererPieceAnimation = ChessPiecesRendererPieceAnimation.EmptyChessPiecesRendererPieceAnimation();
		}

		private static ChessSquarePieceArray GetInitialBoard()
		{
			ChessSquarePiece[][] boardArray = new ChessSquarePiece[8][];
			for (int i = 0; i < 8; i++)
			{
				boardArray[i] = new ChessSquarePiece[8];
				for (int j = 0; j < 8; j++)
					boardArray[i][j] = ChessSquarePiece.Empty;
			}

			for (int i = 0; i < 8; i++)
				boardArray[i][1] = ChessSquarePiece.WhitePawn;

			boardArray[0][0] = ChessSquarePiece.WhiteRook;
			boardArray[1][0] = ChessSquarePiece.WhiteKnight;
			boardArray[2][0] = ChessSquarePiece.WhiteBishop;
			boardArray[3][0] = ChessSquarePiece.WhiteQueen;
			boardArray[4][0] = ChessSquarePiece.WhiteKing;
			boardArray[5][0] = ChessSquarePiece.WhiteBishop;
			boardArray[6][0] = ChessSquarePiece.WhiteKnight;
			boardArray[7][0] = ChessSquarePiece.WhiteRook;

			return new ChessSquarePieceArray(boardArray);
		}

		private static List<int> GetPawnFilesThatHaveNotYetMoved(ChessSquarePieceArray board)
		{
			List<int> pawnFilesThatHaveNotYetMoved = new List<int>();

			for (int i = 0; i < 8; i++)
			{
				if (board.GetPiece(file: i, rank: 1) == ChessSquarePiece.WhitePawn)
					pawnFilesThatHaveNotYetMoved.Add(i);
			}

			return pawnFilesThatHaveNotYetMoved;
		}
		
		public void ProcessFrame(
			IMouse mouseInput,
			IMouse previousMouseInput,
			IDisplayProcessing<ChessImage> displayProcessing,
			int elapsedMicrosPerFrame)
		{
			if (this.chessPiecesRendererFadeOutFadeIn != null)
			{
				this.chessPiecesRendererFadeOutFadeIn.ProcessFrame(elapsedMicrosPerFrame: elapsedMicrosPerFrame);

				if (this.chessPiecesRendererFadeOutFadeIn.HasFinishedFadingOut() && GetPawnFilesThatHaveNotYetMoved(board: this.board).Count == 0)
				{
					this.board = GetInitialBoard();
					this.previousMoveSquares = DTImmutableList<ChessSquare>.EmptyList();
					this.possibleMoveSquares = DTImmutableList<ChessSquare>.EmptyList();
				}

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

					if (this.possibleMoveSquares.Count == 0)
					{
						List<int> pawnFilesThatHaveNotYetMoved = GetPawnFilesThatHaveNotYetMoved(board: this.board);
						if (pawnFilesThatHaveNotYetMoved.Count > 0)
						{
							int file = pawnFilesThatHaveNotYetMoved[this.random.NextInt(pawnFilesThatHaveNotYetMoved.Count)];
							this.possibleMoveSquares = new DTImmutableList<ChessSquare>(new HashSet<ChessSquare>()
							{
								new ChessSquare(file: file, rank: 2),
								new ChessSquare(file: file, rank: 3),
								new ChessSquare(file: file, rank: 4)
							});
						}
						else
						{
							this.chessPiecesRendererFadeOutFadeIn = new ChessPiecesRendererFadeOutFadeIn(colorTheme: this.colorTheme);
						}
					}
					else
					{
						int endingRank;
						if (this.random.NextInt(100) < 70)
							endingRank = 4;
						else
							endingRank = this.random.NextBool() ? 3 : 2;

						int file = this.possibleMoveSquares[0].File;

						this.chessPiecesRendererPieceAnimation = this.chessPiecesRendererPieceAnimation.AddRawMove(
							startingFile: file,
							startingRank: 1,
							endingFile: file,
							endingRank: endingRank,
							piece: ChessSquarePiece.WhitePawn);

						this.board = this.board.SetPiece(file: file, rank: 1, piece: ChessSquarePiece.Empty);
						this.board = this.board.SetPiece(file: file, rank: endingRank, piece: ChessSquarePiece.WhitePawn);

						this.previousMoveSquares = new DTImmutableList<ChessSquare>(new HashSet<ChessSquare>()
						{
							new ChessSquare(file: file, rank: 1),
							new ChessSquare(file: file, rank: endingRank)
						});
						this.possibleMoveSquares = DTImmutableList<ChessSquare>.EmptyList();
					}
				}
			}
			
			this.chessPiecesRenderer = this.chessPiecesRenderer.ProcessFrame(
				pieces: this.board,
				kingInDangerSquare: null,
				previousMoveSquares: this.previousMoveSquares,
				selectedPieceSquare: this.possibleMoveSquares.Count > 0 ? new ChessSquare(file: this.possibleMoveSquares[0].File, rank: 1) : null,
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
				x: 371,
				y: HackExplanationFrameUtil.TITLE_TEXT_Y_OFFSET,
				text: Hack.PawnsCanMoveThreeSpacesInitially.GetHackNameForHackExplanationPanel(),
				font: ChessFont.ChessFont20Pt,
				color: DTColor.Black());

			string explanation = "The first time a pawn moves," + "\n"
				+ "the pawn may move forward" + "\n"
				+ "3 squares.";

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
