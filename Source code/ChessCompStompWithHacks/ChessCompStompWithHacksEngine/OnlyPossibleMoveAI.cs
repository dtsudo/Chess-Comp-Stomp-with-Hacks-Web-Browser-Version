
namespace ChessCompStompWithHacksEngine
{
	using DTLibrary;

	public class OnlyPossibleMoveAI : IChessAI
	{
		private Move move;
		private long startTimeMicroSeconds;
		private IDTLogger logger;

		public OnlyPossibleMoveAI(Move move, ITimer timer, IDTLogger logger)
		{
			this.move = move;
			this.startTimeMicroSeconds = timer.GetNumberOfMicroSeconds();
			this.logger = logger;
		}

		public long GetStartTimeMicroSeconds()
		{
			return this.startTimeMicroSeconds;
		}

		public Move GetBestMoveFoundSoFar()
		{
			this.logger.WriteLine("Found only valid move.");
			return this.move;
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
