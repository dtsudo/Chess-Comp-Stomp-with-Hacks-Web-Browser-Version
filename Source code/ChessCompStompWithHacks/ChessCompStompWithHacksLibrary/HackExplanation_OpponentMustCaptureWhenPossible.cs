
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class HackExplanation_OpponentMustCaptureWhenPossible : HackExplanationFrameUtil.IHackExplanation
	{
		private ChessPiecesRenderer chessPiecesRenderer;
		private ChessPiecesRendererPieceAnimation chessPiecesRendererPieceAnimation;

		private int moveCooldown;

		private IDTRandom random;

		private ColorTheme colorTheme;

		private GameState gameState;

		private Move playerMove;

		private enum Status
		{
			PlayerAboutToClickFirstPiece,
			PlayerAboutToMakeFirstMove,
			OpponentAboutToClickPiece,
			OpponentAboutToMakeMove,
			PlayerAboutToClickSecondPiece,
			PlayerAboutToMakeSecondMove,
			Finished
		}

		private Status status;

		private Move nextMove;
		private DTImmutableList<ChessSquare> previousMoveSquares;
		private DTImmutableList<ChessSquare> possibleMoveSquares;

		private ChessPiecesRendererFadeOutFadeIn chessPiecesRendererFadeOutFadeIn;

		private int? lastRandomValue;
		
		public HackExplanation_OpponentMustCaptureWhenPossible(ColorTheme colorTheme, IDTRandom random)
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
				researchedHacks: new DTImmutableList<Hack>(new HashSet<Hack>() { Hack.OpponentMustCaptureWhenPossible }), 
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
					this.gameState = MoveImplementation.ApplyMove(gameState: this.gameState, move: Move.NormalMove(startingFile: 4, startingRank: 1, endingFile: 4, endingRank: 3));
					this.gameState = MoveImplementation.ApplyMove(gameState: this.gameState, move: Move.NormalMove(startingFile: 4, startingRank: 6, endingFile: 4, endingRank: 4));
					this.playerMove = Move.NormalMove(startingFile: 7, startingRank: 1, endingFile: 7, endingRank: 3);
					break;
				case 1:
					this.gameState = MoveImplementation.ApplyMove(gameState: this.gameState, move: Move.NormalMove(startingFile: 6, startingRank: 0, endingFile: 5, endingRank: 2));
					this.gameState = MoveImplementation.ApplyMove(gameState: this.gameState, move: Move.NormalMove(startingFile: 1, startingRank: 7, endingFile: 2, endingRank: 5));
					this.playerMove = Move.NormalMove(startingFile: 3, startingRank: 1, endingFile: 3, endingRank: 3);
					break;
				case 2:
					this.gameState = MoveImplementation.ApplyMove(gameState: this.gameState, move: Move.NormalMove(startingFile: 1, startingRank: 0, endingFile: 2, endingRank: 2));
					this.gameState = MoveImplementation.ApplyMove(gameState: this.gameState, move: Move.NormalMove(startingFile: 4, startingRank: 6, endingFile: 4, endingRank: 4));
					this.playerMove = Move.NormalMove(startingFile: 0, startingRank: 1, endingFile: 0, endingRank: 2);
					break;
				default:
					throw new Exception();
			}

			this.previousMoveSquares = DTImmutableList<ChessSquare>.EmptyList();
			this.possibleMoveSquares = DTImmutableList<ChessSquare>.EmptyList();

			this.status = Status.PlayerAboutToClickFirstPiece;

			this.nextMove = this.playerMove;
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

					if (this.status == Status.PlayerAboutToMakeSecondMove)
						this.moveCooldown += HackExplanationFrameUtil.ELAPSED_MICROS_BEFORE_PIECE_MOVES / 2;
					
					if (this.moveCooldown <= 0)
						this.moveCooldown = 0;

					switch (this.status)
					{
						case Status.PlayerAboutToClickFirstPiece:
							this.status = Status.PlayerAboutToMakeFirstMove;
							this.possibleMoveSquares = new DTImmutableList<ChessSquare>(ComputeMoves.GetMoves(gameState: this.gameState)
								.Moves
								.Where(x => x.StartingFile.HasValue && x.StartingFile.Value == this.playerMove.StartingFile.Value
									&& x.StartingRank.HasValue && x.StartingRank.Value == this.playerMove.StartingRank.Value)
								.Select(x => new ChessSquare(file: x.EndingFile, rank: x.EndingRank))
								.ToList());
							break;
						case Status.PlayerAboutToMakeFirstMove:
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
							this.nextMove = ComputeMoves.GetMoves(gameState: this.gameState).Moves[0];
							break;
						case Status.OpponentAboutToClickPiece:
							this.status = Status.OpponentAboutToMakeMove;
							break;
						case Status.OpponentAboutToMakeMove:
							this.status = Status.PlayerAboutToClickSecondPiece;
							this.previousMoveSquares = new DTImmutableList<ChessSquare>(new HashSet<ChessSquare>()
							{
								new ChessSquare(file: this.nextMove.StartingFile.Value, rank: this.nextMove.StartingRank.Value),
								new ChessSquare(file: this.nextMove.EndingFile, rank: this.nextMove.EndingRank)
							});
							this.chessPiecesRendererPieceAnimation = this.chessPiecesRendererPieceAnimation.AddMove(
								originalGameState: this.gameState, 
								move: this.nextMove, 
								shouldMoveBeInstant: false);
							this.gameState = MoveImplementation.ApplyMove(gameState: this.gameState, move: this.nextMove);
							List<Move> possibleNextMoves = ComputeMoves.GetMoves(gameState: this.gameState)
								.Moves
								.Where(x => x.EndingFile == this.nextMove.EndingFile && x.EndingRank == this.nextMove.EndingRank)
								.ToList();
							possibleNextMoves.Shuffle(random: this.random);
							this.nextMove = possibleNextMoves[0];
							break;
						case Status.PlayerAboutToClickSecondPiece:
							this.status = Status.PlayerAboutToMakeSecondMove;
							this.possibleMoveSquares = new DTImmutableList<ChessSquare>(ComputeMoves.GetMoves(gameState: this.gameState)
								.Moves
								.Where(x => x.StartingFile.HasValue && x.StartingFile.Value == this.nextMove.StartingFile.Value
									&& x.StartingRank.HasValue && x.StartingRank.Value == this.nextMove.StartingRank.Value)
								.Select(x => new ChessSquare(file: x.EndingFile, rank: x.EndingRank))
								.ToList());
							break;
						case Status.PlayerAboutToMakeSecondMove:
							this.status = Status.Finished;
							this.chessPiecesRendererPieceAnimation = this.chessPiecesRendererPieceAnimation.AddMove(
								originalGameState: this.gameState, 
								move: this.nextMove, 
								shouldMoveBeInstant: false);
							this.gameState = MoveImplementation.ApplyMove(gameState: this.gameState, move: this.nextMove);
							this.possibleMoveSquares = DTImmutableList<ChessSquare>.EmptyList();
							this.previousMoveSquares = new DTImmutableList<ChessSquare>(new HashSet<ChessSquare>()
							{
								new ChessSquare(file: this.nextMove.StartingFile.Value, rank: this.nextMove.StartingRank.Value),
								new ChessSquare(file: this.nextMove.EndingFile, rank: this.nextMove.EndingRank)
							});
							this.nextMove = null;
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
				kingInDangerSquare: null,
				previousMoveSquares: this.previousMoveSquares,
				selectedPieceSquare: this.status == Status.PlayerAboutToMakeFirstMove || this.status == Status.PlayerAboutToMakeSecondMove 
					? new ChessSquare(file: this.nextMove.StartingFile.Value, rank: this.nextMove.StartingRank.Value) 
					: null,
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
				x: 311,
				y: HackExplanationFrameUtil.TITLE_TEXT_Y_OFFSET,
				text: Hack.OpponentMustCaptureWhenPossible.GetHackNameForHackExplanationPanel(),
				font: ChessFont.ChessFont20Pt,
				color: DTColor.Black());

			string explanation = "Capturing is compulsory for" + "\n"
				+ "your opponent (if your" + "\n"
				+ "opponent can capture a piece," + "\n"
				+ "your opponent must capture" + "\n"
				+ "a piece).";

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
