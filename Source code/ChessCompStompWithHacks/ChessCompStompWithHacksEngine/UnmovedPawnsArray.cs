
namespace ChessCompStompWithHacksEngine
{
	public class UnmovedPawnsArray
	{
		private bool[][] board;

		private UnmovedPawnsArray()
		{
		}

		public UnmovedPawnsArray(bool[][] board)
		{
			this.board = CopyBoard(board);
		}
		
		public bool HasUnmovedPawn(int file, int rank)
		{
			return this.board[file][rank];
		}

		public UnmovedPawnsArray PawnMoved(int file, int rank)
		{
			bool[][] newBoard = new bool[8][];

			for (int i = 0; i < 8; i++)
			{
				if (i != file)
					newBoard[i] = this.board[i];
				else
				{
					newBoard[i] = new bool[8];
					for (int j = 0; j < 8; j++)
						newBoard[i][j] = this.board[i][j];
					newBoard[i][rank] = false;
				}
			}

			UnmovedPawnsArray returnValue = new UnmovedPawnsArray();
			returnValue.board = newBoard;

			return returnValue;
		}

		private static bool[][] CopyBoard(bool[][] board)
		{
			bool[][] newBoard = new bool[8][];
			for (int i = 0; i < 8; i++)
			{
				newBoard[i] = new bool[8];
				for (int j = 0; j < 8; j++)
					newBoard[i][j] = board[i][j];
			}

			return newBoard;
		}
	}
}
