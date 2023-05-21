
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System;
	using System.Collections.Generic;

	public class HackExplanation_AnyPieceCanPromote : HackExplanationFrameUtil.IHackExplanation
	{
		private ChessPiecesRenderer chessPiecesRenderer;
		private ChessPiecesRendererPieceAnimation chessPiecesRendererPieceAnimation;
		
		private int moveCooldown;

		private IDTRandom random;

		private ColorTheme colorTheme;

		private ChessSquarePieceArray board;

		private ChessSquare pieceToMove;

		private DTImmutableList<ChessSquare> previousMoveSquares;
		private DTImmutableList<ChessSquare> possibleMoveSquares;

		private ChessPiecesRendererFadeOutFadeIn chessPiecesRendererFadeOutFadeIn;

		public HackExplanation_AnyPieceCanPromote(ColorTheme colorTheme, IDTRandom random)
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

		private void PopulateInitialBoard(IDTRandom random)
		{
			this.previousMoveSquares = DTImmutableList<ChessSquare>.EmptyList();
			this.possibleMoveSquares = DTImmutableList<ChessSquare>.EmptyList();

			ChessSquarePiece[][] boardArray = new ChessSquarePiece[8][];
			for (int i = 0; i < 8; i++)
			{
				boardArray[i] = new ChessSquarePiece[8];
				for (int j = 0; j < 8; j++)
					boardArray[i][j] = ChessSquarePiece.Empty;
			}

			boardArray[random.NextInt(6) + 1][0] = ChessSquarePiece.WhiteKing;

			List<int> pawnList = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7 };
			pawnList.Shuffle(random: random);

			int numPawns = random.NextInt(4) + 3;
			int pawnRank = 1;
			for (int x = 0; x < numPawns; x++)
			{
				if (pawnRank == 1 && x == 2)
					pawnRank++;
				else if (pawnRank < 3)
					pawnRank = random.NextInt(100) < 30 ? pawnRank + 1 : pawnRank;

				boardArray[pawnList[x]][pawnRank] = ChessSquarePiece.WhitePawn;
			}

			if (random.NextInt(100) < 65)
			{
				if (random.NextBool())
				{
					if (boardArray[0][0] == ChessSquarePiece.Empty)
						boardArray[0][0] = ChessSquarePiece.WhiteRook;
				}
				else
				{
					if (boardArray[7][0] == ChessSquarePiece.Empty)
						boardArray[7][0] = ChessSquarePiece.WhiteRook;
				}
			}

			if (random.NextInt(100) < 65)
			{
				List<ChessSquare> possibleLocations = new List<ChessSquare>()
				{
					new ChessSquare(file: 1, rank: 0),
					new ChessSquare(file: 6, rank: 0),
					new ChessSquare(file: 1, rank: 1),
					new ChessSquare(file: 2, rank: 1),
					new ChessSquare(file: 3, rank: 1),
					new ChessSquare(file: 4, rank: 1),
					new ChessSquare(file: 5, rank: 1),
					new ChessSquare(file: 6, rank: 1),
					new ChessSquare(file: 2, rank: 2),
					new ChessSquare(file: 3, rank: 2),
					new ChessSquare(file: 4, rank: 2),
					new ChessSquare(file: 5, rank: 2)
				};

				possibleLocations.Shuffle(random: random);

				foreach (ChessSquare location in possibleLocations)
				{
					if (boardArray[location.File][location.Rank] == ChessSquarePiece.Empty)
					{
						boardArray[location.File][location.Rank] = ChessSquarePiece.WhiteKnight;
						break;
					}
				}
			}

			bool hasEvenBishop = false;
			bool hasOddBishop = false;

			if (random.NextInt(100) < 65)
			{
				List<ChessSquare> possibleLocations = new List<ChessSquare>()
				{
					new ChessSquare(file: 2, rank: 0),
					new ChessSquare(file: 5, rank: 0),
					new ChessSquare(file: 1, rank: 1),
					new ChessSquare(file: 3, rank: 1),
					new ChessSquare(file: 4, rank: 1),
					new ChessSquare(file: 6, rank: 1),
					new ChessSquare(file: 0, rank: 2),
					new ChessSquare(file: 3, rank: 2),
					new ChessSquare(file: 4, rank: 2),
					new ChessSquare(file: 7, rank: 2)
				};

				possibleLocations.Shuffle(random: random);

				foreach (ChessSquare location in possibleLocations)
				{
					if (boardArray[location.File][location.Rank] == ChessSquarePiece.Empty)
					{
						boardArray[location.File][location.Rank] = ChessSquarePiece.WhiteBishop;

						if ((location.File + location.Rank) % 2 == 0)
							hasEvenBishop = true;
						else
							hasOddBishop = true;

						break;
					}
				}
			}

			if (random.NextInt(100) < 65)
			{
				List<ChessSquare> possibleLocations = new List<ChessSquare>()
				{
					new ChessSquare(file: 3, rank: 0),
					new ChessSquare(file: 4, rank: 0),
					new ChessSquare(file: 2, rank: 1),
					new ChessSquare(file: 3, rank: 1),
					new ChessSquare(file: 4, rank: 1),
					new ChessSquare(file: 5, rank: 1)
				};

				possibleLocations.Shuffle(random: random);

				foreach (ChessSquare location in possibleLocations)
				{
					if (boardArray[location.File][location.Rank] == ChessSquarePiece.Empty)
					{
						boardArray[location.File][location.Rank] = ChessSquarePiece.WhiteQueen;
						break;
					}
				}
			}

			if (random.NextInt(3) == 0)
			{
				List<ChessSquare> possibleLocations = new List<ChessSquare>();
				for (int i = 0; i < 8; i++)
				{
					possibleLocations.Add(new ChessSquare(file: i, rank: 4));
					possibleLocations.Add(new ChessSquare(file: i, rank: 5));
				}
				possibleLocations.Shuffle(random: random);

				this.pieceToMove = possibleLocations[0];
				boardArray[this.pieceToMove.File][this.pieceToMove.Rank] = ChessSquarePiece.WhiteRook;
			}
			else if (random.NextBool())
			{
				List<ChessSquare> possibleLocations = new List<ChessSquare>();
				for (int i = 0; i < 8; i++)
				{
					possibleLocations.Add(new ChessSquare(file: i, rank: 4));
					possibleLocations.Add(new ChessSquare(file: i, rank: 5));
				}
				possibleLocations.Shuffle(random: random);

				int index = 0;
				while (true)
				{
					this.pieceToMove = possibleLocations[index];
					index++;

					bool isEven = (this.pieceToMove.File + this.pieceToMove.Rank) % 2 == 0;

					if (isEven && hasEvenBishop || !isEven && hasOddBishop)
						continue;

					boardArray[this.pieceToMove.File][this.pieceToMove.Rank] = ChessSquarePiece.WhiteBishop;
					break;
				}
			}
			else
			{
				List<ChessSquare> possibleLocations = new List<ChessSquare>();
				for (int i = 0; i < 8; i++)
				{
					possibleLocations.Add(new ChessSquare(file: i, rank: 5));
					possibleLocations.Add(new ChessSquare(file: i, rank: 6));
				}
				possibleLocations.Shuffle(random: random);

				this.pieceToMove = possibleLocations[0];
				boardArray[this.pieceToMove.File][this.pieceToMove.Rank] = ChessSquarePiece.WhiteKnight;
			}

			this.board = new ChessSquarePieceArray(board: boardArray);
		}

		private static List<ChessSquare> GetPossibleMoves(ChessSquare pieceToMove, ChessSquarePieceArray board)
		{
			ChessSquarePiece piece = board.GetPiece(pieceToMove);

			if (piece == ChessSquarePiece.WhiteRook || piece == ChessSquarePiece.WhiteBishop)
			{
				List<Tuple<int, int>> directions = new List<Tuple<int, int>>();

				if (piece == ChessSquarePiece.WhiteRook)
				{
					directions.Add(new Tuple<int, int>(1, 0));
					directions.Add(new Tuple<int, int>(-1, 0));
					directions.Add(new Tuple<int, int>(0, 1));
					directions.Add(new Tuple<int, int>(0, -1));
				}
				else if (piece == ChessSquarePiece.WhiteBishop)
				{
					directions.Add(new Tuple<int, int>(1, 1));
					directions.Add(new Tuple<int, int>(1, -1));
					directions.Add(new Tuple<int, int>(-1, 1));
					directions.Add(new Tuple<int, int>(-1, -1));
				}
				else
					throw new Exception();

				List<ChessSquare> possibleMoves = new List<ChessSquare>();

				foreach (Tuple<int, int> direction in directions)
				{
					int i = pieceToMove.File;
					int j = pieceToMove.Rank;

					while (true)
					{
						i += direction.Item1;
						j += direction.Item2;

						if (i < 0 || i >= 8 || j < 0 || j >= 8)
							break;

						if (board.GetPiece(file: i, rank: j) != ChessSquarePiece.Empty)
							break;

						possibleMoves.Add(new ChessSquare(file: i, rank: j));
					}
				}

				return possibleMoves;
			}
			else if (piece == ChessSquarePiece.WhiteKnight)
			{
				List<ChessSquare> potentialMoves = new List<ChessSquare>();

				int i = pieceToMove.File;
				int j = pieceToMove.Rank;

				potentialMoves.Add(new ChessSquare(file: i + 2, rank: j + 1));
				potentialMoves.Add(new ChessSquare(file: i + 2, rank: j - 1));
				potentialMoves.Add(new ChessSquare(file: i - 2, rank: j + 1));
				potentialMoves.Add(new ChessSquare(file: i - 2, rank: j - 1));
				potentialMoves.Add(new ChessSquare(file: i + 1, rank: j + 2));
				potentialMoves.Add(new ChessSquare(file: i + 1, rank: j - 2));
				potentialMoves.Add(new ChessSquare(file: i - 1, rank: j + 2));
				potentialMoves.Add(new ChessSquare(file: i - 1, rank: j - 2));

				List<ChessSquare> validMoves = new List<ChessSquare>();

				foreach (ChessSquare potentialMove in potentialMoves)
				{
					if (potentialMove.File >= 0 && potentialMove.File < 8 && potentialMove.Rank >= 0 && potentialMove.Rank < 8 && board.GetPiece(potentialMove) == ChessSquarePiece.Empty)
						validMoves.Add(potentialMove);
				}

				return validMoves;
			}
			else
				throw new Exception();
		}
				
		public void ProcessFrame(IMouse mouseInput, IMouse previousMouseInput, IDisplayProcessing<GameImage> displayProcessing, int elapsedMicrosPerFrame)
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
						this.possibleMoveSquares = new DTImmutableList<ChessSquare>(GetPossibleMoves(pieceToMove: this.pieceToMove, board: this.board));
					else
					{
						List<ChessSquare> promotionMoves = new List<ChessSquare>();

						for (int x = 0; x < this.possibleMoveSquares.Count; x++)
						{
							if (this.possibleMoveSquares[x].Rank == 7)
								promotionMoves.Add(this.possibleMoveSquares[x]);
						}

						promotionMoves.Shuffle(random: this.random);
						
						this.chessPiecesRendererPieceAnimation = this.chessPiecesRendererPieceAnimation.AddRawMove(
							startingFile: this.pieceToMove.File,
							startingRank: this.pieceToMove.Rank,
							endingFile: promotionMoves[0].File,
							endingRank: promotionMoves[0].Rank,
							piece: this.board.GetPiece(this.pieceToMove));

						this.board = this.board.SetPiece(file: this.pieceToMove.File, rank: this.pieceToMove.Rank, piece: ChessSquarePiece.Empty);
						this.board = this.board.SetPiece(file: promotionMoves[0].File, rank: promotionMoves[0].Rank, piece: ChessSquarePiece.WhiteQueen);

						this.previousMoveSquares = new DTImmutableList<ChessSquare>(new HashSet<ChessSquare>()
						{
							this.pieceToMove,
							promotionMoves[0]
						});
						this.possibleMoveSquares = DTImmutableList<ChessSquare>.EmptyList();
					}
				}
			}
			
			this.chessPiecesRenderer = this.chessPiecesRenderer.ProcessFrame(
				pieces: this.board,
				kingInDangerSquare: null,
				previousMoveSquares: this.previousMoveSquares,
				selectedPieceSquare: this.possibleMoveSquares.Count > 0 ? this.pieceToMove : null,
				possibleMoveSquares: this.possibleMoveSquares,
				potentialNukeSquaresInfo: null,
				hoverSquare: null,
				hoverPieceInfo: null,
				elapsedMicrosPerFrame: elapsedMicrosPerFrame);
			
			this.chessPiecesRendererPieceAnimation = this.chessPiecesRendererPieceAnimation.ProcessFrame(
				elapsedMicrosPerFrame: elapsedMicrosPerFrame);
		}

		public void Render(IDisplayOutput<GameImage, GameFont> displayOutput)
		{
			displayOutput.DrawText(
				x: 305,
				y: HackExplanationFrameUtil.TITLE_TEXT_Y_OFFSET,
				text: Hack.AnyPieceCanPromote.GetHackNameForHackExplanationPanel(),
				font: GameFont.GameFont20Pt,
				color: DTColor.Black());

			string explanation = "Your rooks, knights, bishops," + "\n"
				+ "and queen may promote upon" + "\n"
				+ "reaching the last rank.";

			displayOutput.DrawText(
				x: HackExplanationFrameUtil.EXPLANATION_TEXT_X_OFFSET,
				y: HackExplanationFrameUtil.EXPLANATION_TEXT_Y_OFFSET,
				text: explanation,
				font: GameFont.GameFont16Pt,
				color: DTColor.Black());

			this.chessPiecesRenderer.Render(
				displayOutput: new TranslatedDisplayOutput<GameImage, GameFont>(
					display: displayOutput, 
					xOffsetInPixels: HackExplanationFrameUtil.CHESS_PIECES_RENDERER_X_OFFSET,
					yOffsetInPixels: HackExplanationFrameUtil.CHESS_PIECES_RENDERER_Y_OFFSET),
				chessPiecesRendererPieceAnimation: this.chessPiecesRendererPieceAnimation);

			if (this.chessPiecesRendererFadeOutFadeIn != null)
				this.chessPiecesRendererFadeOutFadeIn.Render(displayOutput: new TranslatedDisplayOutput<GameImage, GameFont>(
					display: displayOutput,
					xOffsetInPixels: HackExplanationFrameUtil.CHESS_PIECES_RENDERER_X_OFFSET,
					yOffsetInPixels: HackExplanationFrameUtil.CHESS_PIECES_RENDERER_Y_OFFSET));
		}
	}
}
