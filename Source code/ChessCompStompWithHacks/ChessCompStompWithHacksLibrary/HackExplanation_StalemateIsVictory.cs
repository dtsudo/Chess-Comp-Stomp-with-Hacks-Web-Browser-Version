
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class HackExplanation_StalemateIsVictory : HackExplanationFrameUtil.IHackExplanation
	{
		private ChessPiecesRenderer chessPiecesRenderer;
		private ChessPiecesRendererPieceAnimation chessPiecesRendererPieceAnimation;

		private int moveCooldown;

		private IDTRandom random;

		private ColorTheme colorTheme;

		private GameState gameState;

		private Move playerMove1;
		private Move opponentMove;
		private Move playerMove2;

		private enum Status
		{
			PlayerAboutToClickFirstPiece,
			PlayerAboutToMakeFirstMove,
			OpponentAboutToClickPiece,
			OpponentAboutToMakeMove,
			PlayerAboutToClickSecondPiece,
			PlayerAboutToMakeSecondMove,
			AboutToShowVictoryPanel,
			Finished
		}

		private Status status;
		
		private DTImmutableList<ChessSquare> previousMoveSquares;
		private DTImmutableList<ChessSquare> possibleMoveSquares;

		private ChessSquare kingInDangerSquareAfterPlayerMove1;

		private ChessPiecesRendererFadeOutFadeIn chessPiecesRendererFadeOutFadeIn;

		private int? lastRandomValue;

		public HackExplanation_StalemateIsVictory(ColorTheme colorTheme, IDTRandom random)
		{
			this.lastRandomValue = null;

			this.chessPiecesRendererFadeOutFadeIn = null;

			this.colorTheme = colorTheme;

			this.random = random;

			this.moveCooldown = HackExplanationFrameUtil.ELAPSED_MICROS_BEFORE_PIECE_MOVES / 2;

			this.PopulateInitialBoard(random: random);
			
			this.chessPiecesRenderer = ChessPiecesRenderer.GetChessPiecesRenderer(
				pieces: this.gameState.Board,
				kingInDangerSquare: null,
				previousMoveSquares: this.previousMoveSquares,
				renderFromWhitePerspective: true,
				colorTheme: colorTheme);

			this.chessPiecesRendererPieceAnimation = ChessPiecesRendererPieceAnimation.EmptyChessPiecesRendererPieceAnimation();
		}

		private void PopulateInitialBoard(IDTRandom random)
		{
			if (this.lastRandomValue == null)
				this.lastRandomValue = random.NextInt(3);
			else
			{
				List<int> possibleValues = new List<int>();
				if (this.lastRandomValue.Value != 0)
					possibleValues.Add(0);
				if (this.lastRandomValue.Value != 1)
					possibleValues.Add(1);
				if (this.lastRandomValue.Value != 2)
					possibleValues.Add(2);

				this.lastRandomValue = possibleValues[random.NextInt(possibleValues.Count)];
			}

			ChessSquarePiece[][] board = new ChessSquarePiece[8][];
			for (int i = 0; i < 8; i++)
			{
				board[i] = new ChessSquarePiece[8];
				for (int j = 0; j < 8; j++)
					board[i][j] = ChessSquarePiece.Empty;
			}

			switch (this.lastRandomValue.Value)
			{
				case 0:
					board[3][1] = ChessSquarePiece.WhiteKing;
					board[4][4] = ChessSquarePiece.WhiteQueen;
					board[6][7] = ChessSquarePiece.BlackKing;
					this.playerMove1 = Move.NormalMove(startingFile: 4, startingRank: 4, endingFile: 4, endingRank: 6);
					this.opponentMove = Move.NormalMove(startingFile: 6, startingRank: 7, endingFile: 7, endingRank: 7);
					this.playerMove2 = Move.NormalMove(startingFile: 4, startingRank: 6, endingFile: 5, endingRank: 6);
					this.kingInDangerSquareAfterPlayerMove1 = null;
					break;
				case 1:
					board[0][5] = ChessSquarePiece.WhiteKing;
					board[0][7] = ChessSquarePiece.BlackKing;
					board[1][2] = ChessSquarePiece.BlackPawn;
					board[5][1] = ChessSquarePiece.WhiteRook;
					board[5][3] = ChessSquarePiece.WhitePawn;
					this.playerMove1 = Move.NormalMove(startingFile: 5, startingRank: 3, endingFile: 5, endingRank: 4);
					this.opponentMove = Move.NormalMove(startingFile: 1, startingRank: 2, endingFile: 1, endingRank: 1);
					this.playerMove2 = Move.NormalMove(startingFile: 5, startingRank: 1, endingFile: 1, endingRank: 1);
					this.kingInDangerSquareAfterPlayerMove1 = null;
					break;
				case 2:
					board[0][4] = ChessSquarePiece.WhiteRook;
					board[1][2] = ChessSquarePiece.WhiteRook;
					board[2][4] = ChessSquarePiece.WhiteQueen;
					board[4][5] = ChessSquarePiece.WhiteKing;
					board[6][2] = ChessSquarePiece.BlackPawn;
					board[7][1] = ChessSquarePiece.BlackKing;
					this.playerMove1 = Move.NormalMove(startingFile: 0, startingRank: 4, endingFile: 0, endingRank: 1);
					this.opponentMove = Move.NormalMove(startingFile: 6, startingRank: 2, endingFile: 6, endingRank: 1);
					this.playerMove2 = Move.NormalMove(startingFile: 2, startingRank: 4, endingFile: 2, endingRank: 0);
					this.kingInDangerSquareAfterPlayerMove1 = new ChessSquare(file: 7, rank: 1);
					break;
				default:
					throw new Exception();
			}

			bool[][] unmovedPawnsArray = new bool[8][];
			for (int i = 0; i < 8; i++)
			{
				unmovedPawnsArray[i] = new bool[8];
				for (int j = 0; j < 8; j++)
					unmovedPawnsArray[i][j] = false;
			}

			this.gameState = new GameState(
				board: new ChessSquarePieceArray(board),
				unmovedPawns: new UnmovedPawnsArray(unmovedPawnsArray),
				turnCount: 101,
				hasUsedNuke: false,
				isPlayerWhite: true,
				isWhiteTurn: true,
				previousPawnMoveFileForEnPassant: null,
				previousPawnMoveRankForEnPassant: null,
				castlingRights: new GameState.CastlingRights(canWhiteCastleKingside: false, canWhiteCastleQueenside: false, canBlackCastleKingside: false, canBlackCastleQueenside: false),
				playerAbilities: new GameState.PlayerAbilities(
					canPawnsMoveThreeSpacesInitially: false,
					canSuperEnPassant: false,
					canRooksMoveLikeBishops: false,
					canSuperCastle: false,
					canRooksCaptureLikeCannons: false,
					canKnightsMakeLargeKnightsMove: false,
					canQueensMoveLikeKnights: false,
					hasTacticalNuke: false,
					hasAnyPieceCanPromote: false,
					hasStalemateIsVictory: true,
					hasOpponentMustCaptureWhenPossible: false,
					hasPawnsDestroyCapturingPiece: false));

			this.previousMoveSquares = DTImmutableList<ChessSquare>.EmptyList();
			this.possibleMoveSquares = DTImmutableList<ChessSquare>.EmptyList();

			this.status = Status.PlayerAboutToClickFirstPiece;
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

				if (this.chessPiecesRendererFadeOutFadeIn.HasFinishedFadingOut() && this.status != Status.PlayerAboutToClickFirstPiece)
					this.PopulateInitialBoard(random: this.random);

				if (this.chessPiecesRendererFadeOutFadeIn.HasFinishedFadingIn())
					this.chessPiecesRendererFadeOutFadeIn = null;
			}
			else
			{
				this.moveCooldown -= elapsedMicrosPerFrame;
				if (this.moveCooldown <= 0)
				{
					this.moveCooldown += HackExplanationFrameUtil.ELAPSED_MICROS_BEFORE_PIECE_MOVES / 2;

					if (this.status == Status.PlayerAboutToMakeSecondMove || this.status == Status.AboutToShowVictoryPanel)
						this.moveCooldown += HackExplanationFrameUtil.ELAPSED_MICROS_BEFORE_PIECE_MOVES / 2;

					if (this.moveCooldown <= 0)
						this.moveCooldown = 0;

					switch (this.status)
					{
						case Status.PlayerAboutToClickFirstPiece:
							this.status = Status.PlayerAboutToMakeFirstMove;
							this.possibleMoveSquares = new DTImmutableList<ChessSquare>(ComputeMoves.GetMoves(gameState: this.gameState)
								.Moves
								.Where(x => x.StartingFile.HasValue && x.StartingFile.Value == this.playerMove1.StartingFile.Value
									&& x.StartingRank.HasValue && x.StartingRank.Value == this.playerMove1.StartingRank.Value)
								.Select(x => new ChessSquare(file: x.EndingFile, rank: x.EndingRank))
								.Distinct()
								.ToList());
							break;
						case Status.PlayerAboutToMakeFirstMove:
							this.status = Status.OpponentAboutToClickPiece;
							this.chessPiecesRendererPieceAnimation = this.chessPiecesRendererPieceAnimation.AddMove(
								originalGameState: this.gameState, 
								move: this.playerMove1, 
								shouldMoveBeInstant: false);
							this.gameState = MoveImplementation.ApplyMove(gameState: this.gameState, move: this.playerMove1);
							this.possibleMoveSquares = DTImmutableList<ChessSquare>.EmptyList();
							this.previousMoveSquares = new DTImmutableList<ChessSquare>(new HashSet<ChessSquare>()
							{
								new ChessSquare(file: this.playerMove1.StartingFile.Value, rank: this.playerMove1.StartingRank.Value),
								new ChessSquare(file: this.playerMove1.EndingFile, rank: this.playerMove1.EndingRank)
							});
							break;
						case Status.OpponentAboutToClickPiece:
							this.status = Status.OpponentAboutToMakeMove;
							break;
						case Status.OpponentAboutToMakeMove:
							this.status = Status.PlayerAboutToClickSecondPiece;
							this.previousMoveSquares = new DTImmutableList<ChessSquare>(new HashSet<ChessSquare>()
							{
								new ChessSquare(file: this.opponentMove.StartingFile.Value, rank: this.opponentMove.StartingRank.Value),
								new ChessSquare(file: this.opponentMove.EndingFile, rank: this.opponentMove.EndingRank)
							});
							this.chessPiecesRendererPieceAnimation = this.chessPiecesRendererPieceAnimation.AddMove(
								originalGameState: this.gameState, 
								move: this.opponentMove, 
								shouldMoveBeInstant: false);
							this.gameState = MoveImplementation.ApplyMove(gameState: this.gameState, move: this.opponentMove);
							break;
						case Status.PlayerAboutToClickSecondPiece:
							this.status = Status.PlayerAboutToMakeSecondMove;
							this.possibleMoveSquares = new DTImmutableList<ChessSquare>(ComputeMoves.GetMoves(gameState: this.gameState)
								.Moves
								.Where(x => x.StartingFile.HasValue && x.StartingFile.Value == this.playerMove2.StartingFile.Value
									&& x.StartingRank.HasValue && x.StartingRank.Value == this.playerMove2.StartingRank.Value)
								.Select(x => new ChessSquare(file: x.EndingFile, rank: x.EndingRank))
								.Distinct()
								.ToList());
							break;
						case Status.PlayerAboutToMakeSecondMove:
							this.status = Status.AboutToShowVictoryPanel;
							this.chessPiecesRendererPieceAnimation = this.chessPiecesRendererPieceAnimation.AddMove(
								originalGameState: this.gameState, 
								move: this.playerMove2, 
								shouldMoveBeInstant: false);
							this.gameState = MoveImplementation.ApplyMove(gameState: this.gameState, move: this.playerMove2);
							this.possibleMoveSquares = DTImmutableList<ChessSquare>.EmptyList();
							this.previousMoveSquares = new DTImmutableList<ChessSquare>(new HashSet<ChessSquare>()
							{
								new ChessSquare(file: this.playerMove2.StartingFile.Value, rank: this.playerMove2.StartingRank.Value),
								new ChessSquare(file: this.playerMove2.EndingFile, rank: this.playerMove2.EndingRank)
							});
							break;
						case Status.AboutToShowVictoryPanel:
							this.status = Status.Finished;
							break;
						case Status.Finished:
							this.chessPiecesRendererFadeOutFadeIn = new ChessPiecesRendererFadeOutFadeIn(colorTheme: this.colorTheme);
							break;
						default: throw new Exception();
					}
				}
			}

			this.chessPiecesRenderer = this.chessPiecesRenderer.ProcessFrame(
				pieces: this.gameState.Board,
				kingInDangerSquare: this.status == Status.OpponentAboutToClickPiece || this.status == Status.OpponentAboutToMakeMove
					? this.kingInDangerSquareAfterPlayerMove1
					: null,
				previousMoveSquares: this.previousMoveSquares,
				selectedPieceSquare: this.status == Status.PlayerAboutToMakeFirstMove
					? new ChessSquare(file: this.playerMove1.StartingFile.Value, rank: this.playerMove1.StartingRank.Value) 
					: this.status == Status.PlayerAboutToMakeSecondMove
						? new ChessSquare(file: this.playerMove2.StartingFile.Value, rank: this.playerMove2.StartingRank.Value)
						: null,
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
				x: 349,
				y: HackExplanationFrameUtil.TITLE_TEXT_Y_OFFSET,
				text: Hack.StalemateIsVictory.GetHackNameForHackExplanationPanel(),
				font: GameFont.GameFont20Pt,
				color: DTColor.Black());

			string explanation = "If it is your turn and you have" + "\n"
				+ "no legal moves, you win the" + "\n"
				+ "game." + "\n"
				+ "\n"
				+ "If it is your opponent's turn" + "\n"
				+ "and your opponent has no" + "\n"
				+ "legal moves, you win the" + "\n"
				+ "game.";

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

			if (this.status == Status.Finished)
			{
				displayOutput.DrawRectangle(
					x: HackExplanationFrameUtil.CHESS_PIECES_RENDERER_X_OFFSET + 99,
					y: HackExplanationFrameUtil.CHESS_PIECES_RENDERER_Y_OFFSET + 240,
					width: 299,
					height: 119,
					color: DTColor.White(),
					fill: true);

				displayOutput.DrawRectangle(
					x: HackExplanationFrameUtil.CHESS_PIECES_RENDERER_X_OFFSET + 99,
					y: HackExplanationFrameUtil.CHESS_PIECES_RENDERER_Y_OFFSET + 240,
					width: 300,
					height: 120,
					color: DTColor.Black(),
					fill: false);

				displayOutput.DrawText(
					x: (HackExplanationFrameUtil.CHESS_PIECES_RENDERER_X_OFFSET + 99) + 65,
					y: (HackExplanationFrameUtil.CHESS_PIECES_RENDERER_Y_OFFSET + 240) + 85,
					text: "Victory!",
					font: GameFont.GameFont32Pt,
					color: DTColor.Black());
			}

			if (this.chessPiecesRendererFadeOutFadeIn != null)
				this.chessPiecesRendererFadeOutFadeIn.Render(displayOutput: new TranslatedDisplayOutput<GameImage, GameFont>(
					display: displayOutput,
					xOffsetInPixels: HackExplanationFrameUtil.CHESS_PIECES_RENDERER_X_OFFSET,
					yOffsetInPixels: HackExplanationFrameUtil.CHESS_PIECES_RENDERER_Y_OFFSET));
		}
	}
}
