
namespace ChessCompStompWithHacksEngine
{
	using DTLibrary;

	public class CompositeAI : IChessAI
	{
		private long startTimeMicroSeconds;
		
		private IChessAI underlyingAI;

		public CompositeAI(GameState gameState, ITimer timer, IDTRandom random, IDTLogger logger)
		{
			this.startTimeMicroSeconds = timer.GetNumberOfMicroSeconds();
			
			if (gameState.TurnCount <= 4)
				this.underlyingAI = new RandomMoveAI(gameState: gameState, timer: timer, random: random);
			else if (gameState.TurnCount <= 20)
				this.underlyingAI = new EarlyGameAI(gameState: gameState, timer: timer, random: random, logger: logger);
			else
				this.underlyingAI = new AlphaBetaAI(gameState: gameState, timer: timer, random: random, logger: logger);
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
