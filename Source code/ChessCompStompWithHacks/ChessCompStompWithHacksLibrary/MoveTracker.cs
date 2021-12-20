
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System.Collections.Generic;

	public class MoveTracker
	{
		public const int MaxNumberOfMovesTracked = 50;

		public class MoveInfo
		{
			public MoveInfo(
				GameState originalGameState,
				GameState newGameState,
				ChessPiecesRenderer newGameStateChessPiecesRenderer,
				NukeRenderer newGameStateNukeRenderer,
				Move move)
			{
				this.OriginalGameState = originalGameState;
				this.NewGameState = newGameState;
				this.NewStateChessPiecesRenderer = newGameStateChessPiecesRenderer;
				this.NewStateNukeRenderer = newGameStateNukeRenderer;
				this.Move = move;
				this.MoveName = MoveNaming.GetNameOfMove(move: move, originalGameState: originalGameState);
			}

			public GameState OriginalGameState { get; private set; }
			public GameState NewGameState { get; private set; }
			public ChessPiecesRenderer NewStateChessPiecesRenderer { get; private set; }
			public NukeRenderer NewStateNukeRenderer { get; private set; }

			public Move Move { get; private set; }
			public string MoveName { get; private set; }
		}
		
		private List<MoveInfo> moves;

		public MoveTracker()
		{
			this.moves = new List<MoveInfo>();
		}

		public MoveTracker AddMove(GameState originalGameState, Move move, ITimer timer)
		{
			GameState newGameState = MoveImplementation.ApplyMove(gameState: originalGameState, move: move);

			MoveTracker newTracker = new MoveTracker();

			newTracker.moves = new List<MoveInfo>(this.moves);

			ChessPiecesRenderer newGameStateChessPiecesRenderer = ChessPiecesRenderer.GetChessPiecesRenderer(
				pieces: newGameState.Board,
				kingInDangerSquare: ChessPiecesRendererUtil.GetKingInDangerSquare(gameState: newGameState),
				previousMoveSquares: ChessPiecesRendererUtil.GetPreviousMoveSquares(originalGameState: originalGameState, move: move),
				renderFromWhitePerspective: newGameState.IsPlayerWhite);

			NukeRenderer newGameStateNukeRenderer = NukeRenderer.GetNukeRenderer(
				hasNukeAbility: newGameState.Abilities.HasTacticalNuke,
				hasUsedNuke: newGameState.HasUsedNuke,
				isNukeSelected: false,
				isHoverOverNuke: null,
				turnCount: newGameState.TurnCount,
				timer: timer);

			newTracker.moves.Add(new MoveInfo(originalGameState: originalGameState, newGameState: newGameState, newGameStateChessPiecesRenderer: newGameStateChessPiecesRenderer, newGameStateNukeRenderer: newGameStateNukeRenderer, move: move));

			if (newTracker.moves.Count > MaxNumberOfMovesTracked)
				newTracker.moves.RemoveAt(0);

			return newTracker;
		}

		public MoveInfo GetMostRecentMove()
		{
			if (this.moves.Count == 0)
				return null;
			return this.moves[this.moves.Count - 1];
		}

		public List<MoveInfo> GetRecentMoves()
		{
			return new List<MoveInfo>(this.moves);
		}
	}
}
