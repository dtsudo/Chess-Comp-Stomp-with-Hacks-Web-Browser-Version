
namespace ChessCompStompWithHacksEngine
{
	using DTLibrary;
	using System;
	using System.Collections;
	using System.Collections.Generic;

	public class EarlyGameAI : IChessAI
	{
		private long startTimeMicroSeconds;

		private GameState originalGameState;
		private GameState gameStateWithNoNuke;
		private ITimer timer;
		private IDTRandom random;
		private IDTLogger logger;
		private IBoardEvaluator boardEvaluator;

		private IEnumerator alphaBetaProcess;
		private Move bestMoveFoundSoFar;
		private int? depthOfBestMoveFoundSoFar;
		private string bestMoveLogString;

		private List<Move> topLevelMoves;

		public EarlyGameAI(GameState gameState, ITimer timer, IDTRandom random, IDTLogger logger)
		{
			this.startTimeMicroSeconds = timer.GetNumberOfMicroSeconds();

			this.originalGameState = gameState;
			this.gameStateWithNoNuke = gameState.IsPlayerTurn() && gameState.Abilities.HasTacticalNuke && gameState.HasUsedNuke == false
				? gameState
				: GameStateUtil.GetGameStateWithoutNukeAbility(gameState: gameState);
			this.timer = timer;
			this.random = random;
			this.logger = logger;
			this.boardEvaluator = new RandomizedBoardEvaluator(random: random);

			ComputeMoves.Result result = ComputeMoves.GetMoves(gameState: gameState);

			if (result.GameStatus == ComputeMoves.GameStatus.InProgress)
			{
				this.alphaBetaProcess = null;
				this.bestMoveFoundSoFar = result.Moves[random.NextInt(result.Moves.Count)];
				this.depthOfBestMoveFoundSoFar = 0;
				this.bestMoveLogString = "Found best move at depth 0.";

				List<Move> moves = new List<Move>(result.Moves);
				moves.Shuffle(random: random);
				int numberOfMovesToKeep = Math.Max(1, moves.Count * 3 / 4);
				this.topLevelMoves = new List<Move>();
				for (int i = 0; i < numberOfMovesToKeep; i++)
					this.topLevelMoves.Add(moves[i]);
			}
			else
			{
				this.alphaBetaProcess = null;
				this.bestMoveFoundSoFar = null;
				this.depthOfBestMoveFoundSoFar = null;
				this.bestMoveLogString = null;

				this.topLevelMoves = null;
			}
		}

		public long GetStartTimeMicroSeconds()
		{
			return this.startTimeMicroSeconds;
		}

		public Move GetBestMoveFoundSoFar()
		{
			if (this.bestMoveFoundSoFar == null)
				throw new Exception();

			if (this.bestMoveLogString != null)
				this.logger.WriteLine(this.bestMoveLogString);

			return this.bestMoveFoundSoFar;
		}

		public int GetDepthOfBestMoveFoundSoFar()
		{
			if (this.depthOfBestMoveFoundSoFar == null)
				throw new Exception();
			return this.depthOfBestMoveFoundSoFar.Value;
		}

		public bool HasFinishedCalculation()
		{
			return false;
		}

		public void CalculateBestMove(int millisecondsToThink)
		{
			if (this.bestMoveFoundSoFar == null)
				return;

			int count = 0;
			long startingTimeMillis = this.timer.GetNumberOfMicroSeconds() / 1000L;
			while (true)
			{
				count++;
				if (count == 5)
				{
					count = 0;
					long currentTimeMillis = this.timer.GetNumberOfMicroSeconds() / 1000L;
					long elapsedTimeAsLong = currentTimeMillis - startingTimeMillis;
					int elapsedTimeMillis = (int)elapsedTimeAsLong;

					if (elapsedTimeMillis >= millisecondsToThink)
						return;
				}

				if (this.alphaBetaProcess == null)
				{
					this.alphaBetaProcess = this.BeginAlphaBeta(
							gameState: this.gameStateWithNoNuke,
							depth: GetDepthOfNextSearch(this.depthOfBestMoveFoundSoFar.Value))
						.GetEnumerator();
				}
				else
				{
					this.alphaBetaProcess.MoveNext();
					object value = this.alphaBetaProcess.Current;
					if (value != null)
					{
						this.bestMoveFoundSoFar = (Move)value;
						this.depthOfBestMoveFoundSoFar = GetDepthOfNextSearch(this.depthOfBestMoveFoundSoFar.Value);
						this.alphaBetaProcess = null;
					}
				}
			}
		}

		private static int GetDepthOfNextSearch(int depthOfBestMoveFoundSoFar)
		{
			int returnValue = depthOfBestMoveFoundSoFar + 2;

			if (returnValue > 50)
				returnValue = 50;

			return returnValue;
		}

		private IEnumerable BeginAlphaBeta(GameState gameState, int depth)
		{
			int? best = null;
			Move bestMove = null;
			int? alpha = null;
			for (int i = 0; i < this.topLevelMoves.Count; i++)
			{
				Move move = this.topLevelMoves[i];
				IEnumerable invoke = this.AlphaBetaHelper(gameState: MoveImplementation.ApplyMove(gameState: gameState, move: move), depth: depth - 1, alpha: alpha, beta: null, isCurrentPlayer: false);

				int? temp = null;
				foreach (object x in invoke)
				{
					yield return null;
					if (x != null)
						temp = (int?)x;
				}
				if (temp == null)
					throw new Exception();
				int moveValue = temp.Value;

				if (best == null || moveValue > best.Value)
				{
					best = moveValue;
					bestMove = move;
				}

				if (alpha == null || best.Value >= alpha.Value)
					alpha = best.Value;
			}

			if (bestMove == null)
				throw new Exception();

			this.bestMoveLogString = "Found best move at depth " + depth.ToStringCultureInvariant() + " with score: " + (gameState.IsWhiteTurn ? best.Value.ToStringCultureInvariant() : (-best.Value).ToStringCultureInvariant());
			yield return bestMove;
			yield break;
		}

		private IEnumerable AlphaBetaHelper(GameState gameState, int depth, int? alpha, int? beta, bool isCurrentPlayer)
		{
			if (depth == 0)
			{
				int? returnValue;
				returnValue = this.boardEvaluator.Evaluate(gameState: gameState, isWhite: this.originalGameState.IsWhiteTurn);
				yield return returnValue;
				yield break;
			}

			ComputeMoves.Result result = ComputeMoves.GetMoves(gameState: gameState);

			if (result.GameStatus == ComputeMoves.GameStatus.Stalemate)
			{
				int? returnValue = 0;
				yield return returnValue;
				yield break;
			}
			if (result.GameStatus == ComputeMoves.GameStatus.WhiteVictory)
			{
				int? returnValue;
				if (this.originalGameState.IsWhiteTurn)
					returnValue = int.MaxValue - 1000 + depth;
				else
					returnValue = int.MinValue + 1000 - depth;
				yield return returnValue;
				yield break;
			}
			if (result.GameStatus == ComputeMoves.GameStatus.BlackVictory)
			{
				int? returnValue;
				if (this.originalGameState.IsWhiteTurn)
					returnValue = int.MinValue + 1000 - depth;
				else
					returnValue = int.MaxValue - 1000 + depth;
				yield return returnValue;
				yield break;
			}

			if (isCurrentPlayer)
			{
				int? best = null;
				for (int i = 0; i < result.Moves.Count; i++)
				{
					Move move = result.Moves[i];

					IEnumerable invoke = this.AlphaBetaHelper(gameState: MoveImplementation.ApplyMove(gameState: gameState, move: move), depth: depth - 1, alpha: alpha, beta: beta, isCurrentPlayer: false);
										
					int? temp = null;
					foreach (object x in invoke)
					{
						yield return null;
						if (x != null)
							temp = (int?)x;
					}
					if (temp == null)
						throw new Exception();
					int moveValue = temp.Value;
					
					if (best == null || moveValue >= best.Value)
						best = moveValue;

					if (alpha == null || best.Value >= alpha.Value)
						alpha = best.Value;

					if (alpha.HasValue && beta.HasValue && alpha.Value >= beta.Value)
						break;
				}

				if (best == null)
					throw new Exception();

				yield return best;
				yield break;
			}
			else
			{
				int? best = null;
				for (int i = 0; i < result.Moves.Count; i++)
				{
					Move move = result.Moves[i];

					IEnumerable invoke = this.AlphaBetaHelper(gameState: MoveImplementation.ApplyMove(gameState: gameState, move: move), depth: depth - 1, alpha: alpha, beta: beta, isCurrentPlayer: true);

					int? temp = null;
					foreach (object x in invoke)
					{
						yield return null;
						if (x != null)
							temp = (int?)x;
					}
					if (temp == null)
						throw new Exception();
					int moveValue = temp.Value;
					
					if (best == null || moveValue <= best.Value)
						best = moveValue;

					if (beta == null || best.Value <= beta.Value)
						beta = best.Value;

					if (alpha.HasValue && beta.HasValue && beta.Value <= alpha.Value)
						break;
				}

				if (best == null)
					throw new Exception();
				yield return best;
				yield break;
			}
		}
	}
}
