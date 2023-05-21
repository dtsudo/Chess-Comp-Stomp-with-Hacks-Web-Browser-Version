
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System;
	using System.Collections.Generic;

	public class HackExplanation_SuperCastling : HackExplanationFrameUtil.IHackExplanation
	{
		private ChessPiecesRenderer chessPiecesRenderer;
		private ChessPiecesRendererPieceAnimation chessPiecesRendererPieceAnimation;

		private int moveCooldown;

		private IDTRandom random;

		private ColorTheme colorTheme;

		private ChessSquarePieceArray board;

		private ChessSquare kingSquare;
		private ChessSquare rookSquare;

		private DTImmutableList<ChessSquare> previousMoveSquares;
		private DTImmutableList<ChessSquare> possibleMoveSquares;

		private ChessPiecesRendererFadeOutFadeIn chessPiecesRendererFadeOutFadeIn;

		public HackExplanation_SuperCastling(ColorTheme colorTheme, IDTRandom random)
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
		
		private static List<ChessSquare> GetPossibleMoveSquares(ChessSquare kingSquare, ChessSquare rookSquare)
		{
			HashSet<ChessSquare> set = new HashSet<ChessSquare>();

			set.Add(new ChessSquare(file: kingSquare.File - 1, rank: kingSquare.Rank - 1));
			set.Add(new ChessSquare(file: kingSquare.File - 1, rank: kingSquare.Rank));
			set.Add(new ChessSquare(file: kingSquare.File - 1, rank: kingSquare.Rank + 1));
			set.Add(new ChessSquare(file: kingSquare.File, rank: kingSquare.Rank - 1));
			set.Add(new ChessSquare(file: kingSquare.File, rank: kingSquare.Rank + 1));
			set.Add(new ChessSquare(file: kingSquare.File + 1, rank: kingSquare.Rank - 1));
			set.Add(new ChessSquare(file: kingSquare.File + 1, rank: kingSquare.Rank));
			set.Add(new ChessSquare(file: kingSquare.File + 1, rank: kingSquare.Rank + 1));
			set.Add(rookSquare);

			if (kingSquare.File == rookSquare.File)
			{
				if (kingSquare.Rank < rookSquare.Rank)
					set.Add(new ChessSquare(file: kingSquare.File, rank: kingSquare.Rank + 2));
				else
					set.Add(new ChessSquare(file: kingSquare.File, rank: kingSquare.Rank - 2));
			}
			else
			{
				if (kingSquare.File < rookSquare.File)
					set.Add(new ChessSquare(file: kingSquare.File + 2, rank: kingSquare.Rank));
				else
					set.Add(new ChessSquare(file: kingSquare.File - 2, rank: kingSquare.Rank));
			}

			List<ChessSquare> list = new List<ChessSquare>();

			foreach (ChessSquare square in set)
			{
				if (0 <= square.File && square.File < 8 && 0 <= square.Rank && square.Rank < 8)
					list.Add(square);
			}

			return list;
		}

		private void PopulateInitialBoard(IDTRandom random)
		{
			int kingFile = random.NextInt(8);
			int kingRank = random.NextInt(8);

			this.kingSquare = new ChessSquare(file: kingFile, rank: kingRank);

			List<ChessSquare> potentialRookSquares = new List<ChessSquare>();

			List<Tuple<int, int>> list = new List<Tuple<int, int>>();
			list.Add(new Tuple<int, int>(1, 0));
			list.Add(new Tuple<int, int>(-1, 0));
			list.Add(new Tuple<int, int>(0, 1));
			list.Add(new Tuple<int, int>(0, -1));

			foreach (Tuple<int, int> tuple in list)
			{
				int i = kingFile;
				int j = kingRank;

				int kingEndI = i + tuple.Item1 + tuple.Item1;
				int kingEndJ = j + tuple.Item2 + tuple.Item2;

				if (kingEndI < 0 || kingEndI >= 8 || kingEndJ < 0 || kingEndJ >= 8)
					continue;

				while (true)
				{
					i += tuple.Item1;
					j += tuple.Item2;

					if (0 <= i && i < 8 && 0 <= j && j < 8)
						potentialRookSquares.Add(new ChessSquare(file: i, rank: j));
					else
						break;
				}
			}

			this.rookSquare = potentialRookSquares[random.NextInt(potentialRookSquares.Count)];

			ChessSquarePiece[][] boardArray = new ChessSquarePiece[8][];
			for (int i = 0; i < 8; i++)
			{
				boardArray[i] = new ChessSquarePiece[8];
				for (int j = 0; j < 8; j++)
					boardArray[i][j] = ChessSquarePiece.Empty;
			}

			boardArray[this.kingSquare.File][this.kingSquare.Rank] = ChessSquarePiece.WhiteKing;
			boardArray[this.rookSquare.File][this.rookSquare.Rank] = ChessSquarePiece.WhiteRook;

			this.board = new ChessSquarePieceArray(boardArray);

			this.previousMoveSquares = DTImmutableList<ChessSquare>.EmptyList();
			this.possibleMoveSquares = DTImmutableList<ChessSquare>.EmptyList();
		}

		public void ProcessFrame(
			IMouse mouseInput, 
			IMouse previousMouseInput, 
			IDisplayProcessing<GameImage> displayProcessing, 
			int elapsedMicrosPerFrame)
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
						this.possibleMoveSquares = new DTImmutableList<ChessSquare>(GetPossibleMoveSquares(kingSquare: this.kingSquare, rookSquare: this.rookSquare));
					else
					{
						ChessSquare endingKingSquare;

						if (this.kingSquare.File == this.rookSquare.File)
						{
							if (this.kingSquare.Rank < this.rookSquare.Rank)
								endingKingSquare = new ChessSquare(file: this.kingSquare.File, rank: this.kingSquare.Rank + 2);
							else
								endingKingSquare = new ChessSquare(file: this.kingSquare.File, rank: this.kingSquare.Rank - 2);
						}
						else
						{
							if (this.kingSquare.File < this.rookSquare.File)
								endingKingSquare = new ChessSquare(file: this.kingSquare.File + 2, rank: this.kingSquare.Rank);
							else
								endingKingSquare = new ChessSquare(file: this.kingSquare.File - 2, rank: this.kingSquare.Rank);
						}

						ChessSquare endingRookSquare = new ChessSquare(file: (this.kingSquare.File + endingKingSquare.File) / 2, rank: (this.kingSquare.Rank + endingKingSquare.Rank) / 2);

						this.chessPiecesRendererPieceAnimation = this.chessPiecesRendererPieceAnimation.AddRawMove(
							startingFile: this.kingSquare.File,
							startingRank: this.kingSquare.Rank,
							endingFile: endingKingSquare.File,
							endingRank: endingKingSquare.Rank,
							piece: ChessSquarePiece.WhiteKing);

						this.chessPiecesRendererPieceAnimation = this.chessPiecesRendererPieceAnimation.AddRawMove(
							startingFile: this.rookSquare.File,
							startingRank: this.rookSquare.Rank,
							endingFile: endingRookSquare.File,
							endingRank: endingRookSquare.Rank,
							piece: ChessSquarePiece.WhiteRook);

						this.board = this.board.SetPiece(file: this.kingSquare.File, rank: this.kingSquare.Rank, piece: ChessSquarePiece.Empty);
						this.board = this.board.SetPiece(file: this.rookSquare.File, rank: this.rookSquare.Rank, piece: ChessSquarePiece.Empty);
						this.board = this.board.SetPiece(file: endingKingSquare.File, rank: endingKingSquare.Rank, piece: ChessSquarePiece.WhiteKing);
						this.board = this.board.SetPiece(file: endingRookSquare.File, rank: endingRookSquare.Rank, piece: ChessSquarePiece.WhiteRook);

						HashSet<ChessSquare> previousMoves = new HashSet<ChessSquare>();
						previousMoves.Add(this.kingSquare);
						previousMoves.Add(this.rookSquare);
						previousMoves.Add(endingKingSquare);
						previousMoves.Add(endingRookSquare);

						this.previousMoveSquares = new DTImmutableList<ChessSquare>(set: previousMoves);

						this.possibleMoveSquares = DTImmutableList<ChessSquare>.EmptyList();
					}
				}
			}

			this.chessPiecesRenderer = this.chessPiecesRenderer.ProcessFrame(
				pieces: this.board,
				kingInDangerSquare: null,
				previousMoveSquares: this.previousMoveSquares,
				selectedPieceSquare: this.possibleMoveSquares.Count > 0 ? this.kingSquare : null,
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
				x: 351,
				y: HackExplanationFrameUtil.TITLE_TEXT_Y_OFFSET,
				text: Hack.SuperCastling.GetHackNameForHackExplanationPanel(),
				font: GameFont.GameFont20Pt,
				color: DTColor.Black());

			string explanation = "You may castle as long as" + "\n"
				+ "there are no pieces between" + "\n"
				+ "your king and rook." + "\n"
				+ "\n"
				+ "Super castling is allowed" + "\n"
				+ "regardless of whether the" + "\n"
				+ "king or rook has previously" + "\n"
				+ "moved." + "\n"
				+ "\n"
				+ "You cannot super castle out" + "\n"
				+ "of, through, or into check." + "\n"
				+ "\n"
				+ "Super castling is allowed" + "\n"
				+ "both horizontally and" + "\n"
				+ "vertically.";

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
