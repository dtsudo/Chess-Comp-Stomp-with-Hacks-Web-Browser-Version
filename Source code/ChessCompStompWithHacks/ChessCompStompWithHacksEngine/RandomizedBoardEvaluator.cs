
namespace ChessCompStompWithHacksEngine
{
	using DTLibrary;
	using System;

	public class RandomizedBoardEvaluator : IBoardEvaluator
	{
		private IDTRandom random;
		private int pawnValue;
		private int knightValue;
		private int bishopValue;
		private int rookValue;
		private int queenValue;
		private int kingValue;

		public RandomizedBoardEvaluator(IDTRandom random)
		{
			this.random = random;
			this.pawnValue = random.NextInt(61) + 70;
			this.knightValue = random.NextInt(201) + 200;
			this.bishopValue = random.NextInt(201) + 200;
			this.rookValue = random.NextInt(201) + 400;
			this.queenValue = random.NextInt(201) + 800;
			this.kingValue = random.NextInt(201) + 300;
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
							whiteScore += this.pawnValue;

							whiteScore += 10 * (j - 1);
							break;
						case ChessSquarePiece.WhiteKnight:
							whiteScore += this.knightValue;
							break;
						case ChessSquarePiece.WhiteBishop:
							whiteScore += this.bishopValue;
							break;
						case ChessSquarePiece.WhiteRook:
							whiteScore += this.rookValue;
							break;
						case ChessSquarePiece.WhiteQueen:
							whiteScore += this.queenValue;
							break;
						case ChessSquarePiece.WhiteKing:
							whiteScore += this.kingValue;
							break;
						case ChessSquarePiece.BlackPawn:
							blackScore += this.pawnValue;
							blackScore += 10 * (6 - j);
							break;
						case ChessSquarePiece.BlackKnight:
							blackScore += this.knightValue;
							break;
						case ChessSquarePiece.BlackBishop:
							blackScore += this.bishopValue;
							break;
						case ChessSquarePiece.BlackRook:
							blackScore += this.rookValue;
							break;
						case ChessSquarePiece.BlackQueen:
							blackScore += this.queenValue;
							break;
						case ChessSquarePiece.BlackKing:
							blackScore += this.kingValue;
							break;
						case ChessSquarePiece.Empty:
							break;
						default: throw new Exception();
					}
				}
			}

			// Note that neither score can be zero since the king also gives points
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
