
namespace ChessCompStompWithHacksEngine
{
	public interface IChessAI
	{
		long GetStartTimeMicroSeconds();
		Move GetBestMoveFoundSoFar();
		int GetDepthOfBestMoveFoundSoFar();
		void CalculateBestMove(int millisecondsToThink);
		bool HasFinishedCalculation();
	}
}
