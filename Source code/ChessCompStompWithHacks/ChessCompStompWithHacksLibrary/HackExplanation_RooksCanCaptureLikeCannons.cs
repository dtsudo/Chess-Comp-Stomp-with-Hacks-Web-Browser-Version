
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System;
	using System.Collections.Generic;

	public class HackExplanation_RooksCanCaptureLikeCannons : HackExplanationFrameUtil.IHackExplanation
	{
		private ChessPiecesRenderer chessPiecesRenderer;
		private ChessPiecesRendererPieceAnimation chessPiecesRendererPieceAnimation;

		private int moveCooldown;

		private IDTRandom random;

		private ColorTheme colorTheme;

		private ChessSquarePieceArray board;
		
		private ChessSquare rookSquare;
		private ChessSquare cannonSquare;

		private DTImmutableList<ChessSquare> previousMoveSquares;
		private DTImmutableList<ChessSquare> possibleMoveSquares;

		private ChessPiecesRendererFadeOutFadeIn chessPiecesRendererFadeOutFadeIn;

		public HackExplanation_RooksCanCaptureLikeCannons(ColorTheme colorTheme, IDTRandom random)
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

		private static List<ChessSquare> GetPossibleMoveSquares(ChessSquare rookSquare, ChessSquarePieceArray board)
		{
			List<ChessSquare> list = new List<ChessSquare>();

			List<Tuple<int, int>> directions = new List<Tuple<int, int>>();
			directions.Add(new Tuple<int, int>(1, 0));
			directions.Add(new Tuple<int, int>(-1, 0));
			directions.Add(new Tuple<int, int>(0, 1));
			directions.Add(new Tuple<int, int>(0, -1));

			foreach (Tuple<int, int> direction in directions)
			{
				int i = rookSquare.File;
				int j = rookSquare.Rank;

				bool ranIntoAnotherPiece = false;

				while (true)
				{
					i += direction.Item1;
					j += direction.Item2;

					if (i < 0 || i >= 8 || j < 0 || j >= 8)
						break;

					if (board.GetPiece(file: i, rank: j) == ChessSquarePiece.Empty)
					{
						if (!ranIntoAnotherPiece)
							list.Add(new ChessSquare(file: i, rank: j));
						continue;
					}

					if (board.GetPiece(file: i, rank: j).IsBlack())
					{
						list.Add(new ChessSquare(file: i, rank: j));
						if (ranIntoAnotherPiece)
							break;
						ranIntoAnotherPiece = true;
					}
					else
					{
						if (ranIntoAnotherPiece)
							break;
						ranIntoAnotherPiece = true;
					}
				}
			}

			return list;
		}

		private static ChessSquarePiece GetRandomEnemyPiece(IDTRandom random)
		{
			switch (random.NextInt(4))
			{
				case 0: return ChessSquarePiece.BlackRook;
				case 1: return ChessSquarePiece.BlackKnight;
				case 2: return ChessSquarePiece.BlackBishop;
				case 3: return ChessSquarePiece.BlackQueen;
				default: throw new Exception();
			}
		}

		private static ChessSquarePiece GetRandomPlayerPiece(IDTRandom random)
		{
			switch (random.NextInt(3))
			{
				case 0: return ChessSquarePiece.WhiteKnight;
				case 1: return ChessSquarePiece.WhiteBishop;
				case 2: return ChessSquarePiece.WhiteQueen;
				default: throw new Exception();
			}
		}

		private void PopulateInitialBoard(IDTRandom random)
		{
			ChessSquarePiece[][] boardArray = new ChessSquarePiece[8][];
			for (int i = 0; i < 8; i++)
			{
				boardArray[i] = new ChessSquarePiece[8];
				for (int j = 0; j < 8; j++)
					boardArray[i][j] = ChessSquarePiece.Empty;
			}

			int rookFile = random.NextInt(8);
			int rookRank = random.NextInt(8);

			this.rookSquare = new ChessSquare(file: rookFile, rank: rookRank);

			List<Tuple<int, int>> list = new List<Tuple<int, int>>();
			list.Add(new Tuple<int, int>(1, 0));
			list.Add(new Tuple<int, int>(-1, 0));
			list.Add(new Tuple<int, int>(0, 1));
			list.Add(new Tuple<int, int>(0, -1));

			list.Shuffle(random: random);

			bool hasSetupCannon = false;

			foreach (Tuple<int, int> tuple in list)
			{
				int i = rookFile;
				int j = rookRank;

				int length = 0;

				while (true)
				{
					i += tuple.Item1;
					j += tuple.Item2;

					if (0 <= i && i < 8 && 0 <= j && j < 8)
						length++;
					else
						break;
				}

				if (length >= 4 && !hasSetupCannon)
				{
					hasSetupCannon = true;

					int x = random.NextInt(length - 3) + 4;

					this.cannonSquare = new ChessSquare(file: rookFile + x * tuple.Item1, rank: rookRank + x * tuple.Item2);
					boardArray[this.cannonSquare.File][this.cannonSquare.Rank] = GetRandomEnemyPiece(random: random);

					x = random.NextInt(3) + 1;

					boardArray[rookFile + x * tuple.Item1][rookRank + x * tuple.Item2] = random.NextBool() ? GetRandomPlayerPiece(random: random) : GetRandomEnemyPiece(random: random);
				}
				else
				{
					if (random.NextBool())
						continue;

					if (length == 0)
						continue;

					int x = random.NextInt(length) + 1;

					boardArray[rookFile + x * tuple.Item1][rookRank + x * tuple.Item2] = random.NextBool() ? GetRandomPlayerPiece(random: random) : GetRandomEnemyPiece(random: random);
				}
			}

			boardArray[this.rookSquare.File][this.rookSquare.Rank] = ChessSquarePiece.WhiteRook;

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
						this.possibleMoveSquares = new DTImmutableList<ChessSquare>(GetPossibleMoveSquares(rookSquare: this.rookSquare, board: this.board));
					else
					{
						this.chessPiecesRendererPieceAnimation = this.chessPiecesRendererPieceAnimation.AddRawMove(
							startingFile: this.rookSquare.File,
							startingRank: this.rookSquare.Rank,
							endingFile: this.cannonSquare.File,
							endingRank: this.cannonSquare.Rank,
							piece: ChessSquarePiece.WhiteRook);
						
						this.board = this.board.SetPiece(file: this.rookSquare.File, rank: this.rookSquare.Rank, piece: ChessSquarePiece.Empty);
						this.board = this.board.SetPiece(file: this.cannonSquare.File, rank: this.cannonSquare.Rank, piece: ChessSquarePiece.WhiteRook);

						this.previousMoveSquares = new DTImmutableList<ChessSquare>(new HashSet<ChessSquare>()
						{
							this.rookSquare,
							this.cannonSquare
						});

						this.possibleMoveSquares = DTImmutableList<ChessSquare>.EmptyList();
					}
				}
			}

			this.chessPiecesRenderer = this.chessPiecesRenderer.ProcessFrame(
				pieces: this.board,
				kingInDangerSquare: null,
				previousMoveSquares: this.previousMoveSquares,
				selectedPieceSquare: this.possibleMoveSquares.Count > 0 ? this.rookSquare : null,
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
				x: 382,
				y: HackExplanationFrameUtil.TITLE_TEXT_Y_OFFSET,
				text: Hack.RooksCanCaptureLikeCannons.GetHackNameForHackExplanationPanel(),
				font: ChessFont.ChessFont20Pt,
				color: DTColor.Black());

			string explanation = "Your rooks may capture" + "\n"
				+ "enemy pieces even if there is" + "\n"
				+ "a piece between your rook" + "\n"
				+ "and the piece being captured.";

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
