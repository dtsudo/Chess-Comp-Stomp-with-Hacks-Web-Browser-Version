
namespace ChessCompStompWithHacksEngine
{
	using DTLibrary;
	using System;

	public class RandomMoveAI : IChessAI
	{
		private long startTimeMicroSeconds;

		private Move bestMove;

		private IDTLogger logger;

		public RandomMoveAI(GameState gameState, ITimer timer, IDTRandom random, IDTLogger logger)
		{
			this.startTimeMicroSeconds = timer.GetNumberOfMicroSeconds();

			ComputeMoves.Result result = ComputeMoves.GetMoves(gameState: gameState);

			if (result.GameStatus == ComputeMoves.GameStatus.InProgress)
				this.bestMove = result.Moves[random.NextInt(result.Moves.Count)];
			else
				this.bestMove = null;

			this.logger = logger;
		}

		public long GetStartTimeMicroSeconds()
		{
			return this.startTimeMicroSeconds;
		}

		public Move GetBestMoveFoundSoFar()
		{
			if (this.bestMove == null)
				throw new Exception();

			this.logger.WriteLine("Found randomly chosen move.");
			return this.bestMove;
		}

		public bool HasFinishedCalculation()
		{
			return true;
		}

		public int GetDepthOfBestMoveFoundSoFar()
		{
			return 0;
		}

		public void CalculateBestMove(int millisecondsToThink)
		{
			// do nothing
		}
	}
}
