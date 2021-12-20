
namespace ChessCompStompWithHacksEngine
{
	public interface IBoardEvaluator
	{
		/// <summary>
		/// Positive number means a stronger position.
		/// Negative number means a weaker position.
		/// 
		/// e.g. if isWhite, then a positive number means white has an advantage.
		/// </summary>
		int Evaluate(GameState gameState, bool isWhite);
	}
}
