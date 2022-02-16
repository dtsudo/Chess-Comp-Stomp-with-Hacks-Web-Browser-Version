
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class HackExplanation_PawnsDestroyCapturingPiece : HackExplanationFrameUtil.IHackExplanation
	{
		private ChessPiecesRenderer chessPiecesRenderer;
		private ChessPiecesRendererPieceAnimation chessPiecesRendererPieceAnimation;

		private int moveCooldown;

		private IDTRandom random;

		private ColorTheme colorTheme;

		private GameState gameState;

		private Move playerMove;
		private Move opponentMove;

		private enum Status
		{
			PlayerAboutToClickPiece,
			PlayerAboutToMakeMove,
			OpponentAboutToClickPiece,
			OpponentAboutToMakeMove,
			Finished
		}

		private Status status;
		
		private DTImmutableList<ChessSquare> previousMoveSquares;
		private DTImmutableList<ChessSquare> possibleMoveSquares;

		private ChessPiecesRendererFadeOutFadeIn chessPiecesRendererFadeOutFadeIn;

		private int? lastRandomValue;

		public HackExplanation_PawnsDestroyCapturingPiece(ColorTheme colorTheme, IDTRandom random)
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
			this.gameState = NewGameCreation.CreateNewGame(
				isPlayerWhite: true, 
				researchedHacks: new DTImmutableList<Hack>(new HashSet<Hack>() { Hack.PawnsDestroyCapturingPiece }), 
				aiHackLevel: SessionState.AIHackLevel.Initial);

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

			switch (this.lastRandomValue.Value)
			{
				case 0:
					this.gameState = MoveImplementation.ApplyMove(gameState: this.gameState, move: Move.NormalMove(startingFile: 5, startingRank: 1, endingFile: 5, endingRank: 3));
					this.gameState = MoveImplementation.ApplyMove(gameState: this.gameState, move: Move.NormalMove(startingFile: 3, startingRank: 6, endingFile: 3, endingRank: 4));
					this.playerMove = Move.NormalMove(startingFile: 5, startingRank: 3, endingFile: 5, endingRank: 4);
					this.opponentMove = Move.NormalMove(startingFile: 2, startingRank: 7, endingFile: 5, endingRank: 4);
					break;
				case 1:
					this.gameState = MoveImplementation.ApplyMove(gameState: this.gameState, move: Move.NormalMove(startingFile: 4, startingRank: 1, endingFile: 4, endingRank: 3));
					this.gameState = MoveImplementation.ApplyMove(gameState: this.gameState, move: Move.NormalMove(startingFile: 2, startingRank: 6, endingFile: 2, endingRank: 4));
					this.gameState = MoveImplementation.ApplyMove(gameState: this.gameState, move: Move.NormalMove(startingFile: 0, startingRank: 1, endingFile: 0, endingRank: 3));
					this.gameState = MoveImplementation.ApplyMove(gameState: this.gameState, move: Move.NormalMove(startingFile: 1, startingRank: 7, endingFile: 2, endingRank: 5));
					this.playerMove = Move.NormalMove(startingFile: 0, startingRank: 3, endingFile: 0, endingRank: 4);
					this.opponentMove = random.NextBool()
						? Move.NormalMove(startingFile: 3, startingRank: 7, endingFile: 0, endingRank: 4)
						: Move.NormalMove(startingFile: 2, startingRank: 5, endingFile: 0, endingRank: 4);
					break;
				case 2:
					this.gameState = MoveImplementation.ApplyMove(gameState: this.gameState, move: Move.NormalMove(startingFile: 3, startingRank: 1, endingFile: 3, endingRank: 3));
					this.gameState = MoveImplementation.ApplyMove(gameState: this.gameState, move: Move.NormalMove(startingFile: 3, startingRank: 6, endingFile: 3, endingRank: 4));
					this.playerMove = Move.NormalMove(startingFile: 2, startingRank: 1, endingFile: 2, endingRank: 3);
					this.opponentMove = Move.NormalMove(startingFile: 3, startingRank: 4, endingFile: 2, endingRank: 3);
					break;
				default:
					throw new Exception();
			}

			this.previousMoveSquares = DTImmutableList<ChessSquare>.EmptyList();
			this.possibleMoveSquares = DTImmutableList<ChessSquare>.EmptyList();

			this.status = Status.PlayerAboutToClickPiece;
		}

		public void ProcessFrame(IMouse mouseInput, IMouse previousMouseInput, IDisplayProcessing<ChessImage> displayProcessing, int elapsedMicrosPerFrame)
		{
			if (this.chessPiecesRendererFadeOutFadeIn != null)
			{
				this.chessPiecesRendererFadeOutFadeIn.ProcessFrame(elapsedMicrosPerFrame: elapsedMicrosPerFrame);

				if (this.chessPiecesRendererFadeOutFadeIn.HasFinishedFadingOut() && this.status != Status.PlayerAboutToClickPiece)
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

					if (this.status == Status.OpponentAboutToMakeMove)
						this.moveCooldown += HackExplanationFrameUtil.ELAPSED_MICROS_BEFORE_PIECE_MOVES / 2;

					if (this.moveCooldown <= 0)
						this.moveCooldown = 0;

					switch (this.status)
					{
						case Status.PlayerAboutToClickPiece:
							this.status = Status.PlayerAboutToMakeMove;
							this.possibleMoveSquares = new DTImmutableList<ChessSquare>(
								ComputeMoves.GetMoves(gameState: this.gameState)
									.Moves
									.Where(x => x.StartingFile.HasValue && x.StartingFile.Value == this.playerMove.StartingFile.Value
										&& x.StartingRank.HasValue && x.StartingRank.Value == this.playerMove.StartingRank.Value)
									.Select(x => new ChessSquare(file: x.EndingFile, rank: x.EndingRank))
									.ToList());
							break;
						case Status.PlayerAboutToMakeMove:
							this.status = Status.OpponentAboutToClickPiece;
							this.chessPiecesRendererPieceAnimation = this.chessPiecesRendererPieceAnimation.AddMove(
								originalGameState: this.gameState, 
								move: this.playerMove, 
								shouldMoveBeInstant: false);
							this.gameState = MoveImplementation.ApplyMove(gameState: this.gameState, move: this.playerMove);
							this.possibleMoveSquares = DTImmutableList<ChessSquare>.EmptyList();
							this.previousMoveSquares = new DTImmutableList<ChessSquare>(new HashSet<ChessSquare>()
							{
								new ChessSquare(file: this.playerMove.StartingFile.Value, rank: this.playerMove.StartingRank.Value),
								new ChessSquare(file: this.playerMove.EndingFile, rank: this.playerMove.EndingRank)
							});
							break;
						case Status.OpponentAboutToClickPiece:
							this.status = Status.OpponentAboutToMakeMove;
							break;
						case Status.OpponentAboutToMakeMove:
							this.status = Status.Finished;
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
						case Status.Finished:
							this.chessPiecesRendererFadeOutFadeIn = new ChessPiecesRendererFadeOutFadeIn(colorTheme: this.colorTheme);
							break;
						default:
							throw new Exception();
					}
				}
			}

			this.chessPiecesRenderer = this.chessPiecesRenderer.ProcessFrame(
				pieces: this.gameState.Board,
				kingInDangerSquare: null,
				previousMoveSquares: this.previousMoveSquares,
				selectedPieceSquare: this.status == Status.PlayerAboutToMakeMove ? new ChessSquare(file: this.playerMove.StartingFile.Value, rank: this.playerMove.StartingRank.Value) : null,
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
				x: 331,
				y: HackExplanationFrameUtil.TITLE_TEXT_Y_OFFSET,
				text: Hack.PawnsDestroyCapturingPiece.GetHackNameForHackExplanationPanel(),
				font: ChessFont.ChessFont20Pt,
				color: DTColor.Black());

			string explanation = "When any of your pawns are" + "\n"
				+ "captured, the capturing piece" + "\n"
				+ "is also removed from the" + "\n"
				+ "board." + "\n"
				+ "\n"
				+ "Your opponent's king cannot" + "\n"
				+ "capture your pawns.";

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
