
namespace ChessCompStompWithHacksEngine
{
	using DTLibrary;

	public class CompositeAI : IChessAI
	{
		private long startTimeMicroSeconds;
		
		private IChessAI underlyingAI;

		public CompositeAI(GameState gameState, ITimer timer, IDTRandom random, IDTLogger logger, bool useDebugAI)
		{
			this.startTimeMicroSeconds = timer.GetNumberOfMicroSeconds();

			ComputeMoves.Result result = ComputeMoves.GetMoves(gameState: gameState);
			if (result.GameStatus == ComputeMoves.GameStatus.InProgress && result.Moves.Count == 1)
				this.underlyingAI = new OnlyPossibleMoveAI(move: result.Moves[0], timer: timer, logger: logger);
			else if (useDebugAI)
				this.underlyingAI = new RandomMoveAI(gameState: gameState, timer: timer, random: random, logger: logger);
			else
			{
				if (gameState.TurnCount <= 4)
					this.underlyingAI = new RandomMoveAI(gameState: gameState, timer: timer, random: random, logger: logger);
				else if (gameState.TurnCount <= 20)
					this.underlyingAI = new EarlyGameAI(gameState: gameState, timer: timer, random: random, logger: logger);
				else
					this.underlyingAI = new AlphaBetaAI(gameState: gameState, timer: timer, random: random, logger: logger);
			}
		}

		public long GetStartTimeMicroSeconds()
		{
			return this.startTimeMicroSeconds;
		}

		public Move GetBestMoveFoundSoFar()
		{
			return this.underlyingAI.GetBestMoveFoundSoFar();
		}

		public int GetDepthOfBestMoveFoundSoFar()
		{
			return this.underlyingAI.GetDepthOfBestMoveFoundSoFar();
		}

		public bool HasFinishedCalculation()
		{
			return this.underlyingAI.HasFinishedCalculation();
		}

		public void CalculateBestMove(int millisecondsToThink)
		{
			this.underlyingAI.CalculateBestMove(millisecondsToThink: millisecondsToThink);
		}
	}
}
