
namespace ChessCompStompWithHacksEngine
{
	using DTLibrary;
	using System;

	public class StandardBoardEvaluator : IBoardEvaluator
	{
		private IDTRandom random;

		public StandardBoardEvaluator(IDTRandom random)
		{
			this.random = random;
		}
		
		public int Evaluate(GameState gameState, bool isWhite)
		{
			int whiteScore = 0;
			int blackScore = 0;

			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					switch (gameState.Board.GetPiece(i, j))
					{
						case ChessSquarePiece.WhitePawn:
							whiteScore += 100;

							whiteScore += 10 * (j - 1);
							break;
						case ChessSquarePiece.WhiteKnight:
							whiteScore += 300;
							break;
						case ChessSquarePiece.WhiteBishop:
							whiteScore += 300;
							break;
						case ChessSquarePiece.WhiteRook:
							whiteScore += 500;
							break;
						case ChessSquarePiece.WhiteQueen:
							whiteScore += 900;
							break;
						case ChessSquarePiece.WhiteKing:
							whiteScore += 400;
							break;
						case ChessSquarePiece.BlackPawn:
							blackScore += 100;
							blackScore += 10 * (6 - j);
							break;
						case ChessSquarePiece.BlackKnight:
							blackScore += 300;
							break;
						case ChessSquarePiece.BlackBishop:
							blackScore += 300;
							break;
						case ChessSquarePiece.BlackRook:
							blackScore += 500;
							break;
						case ChessSquarePiece.BlackQueen:
							blackScore += 900;
							break;
						case ChessSquarePiece.BlackKing:
							blackScore += 400;
							break;
						case ChessSquarePiece.Empty:
							break;
						default: throw new Exception();
					}
				}
			}

			// Note that neither score can be zero since the king gives 400 points
			int score;
			if (whiteScore > blackScore)
				score = whiteScore * 10000 / blackScore;
			else if (blackScore > whiteScore)
				score = -blackScore * 10000 / whiteScore;
			else
				score = 0;

			if (isWhite)
				return score;
			else
				return -score;
		}
	}
}
