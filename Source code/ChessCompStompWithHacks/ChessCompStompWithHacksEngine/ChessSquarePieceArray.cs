
namespace ChessCompStompWithHacksEngine
{
	public class ChessSquarePieceArray
	{
		private ChessSquarePiece[][] board;

		private ChessSquarePieceArray()
		{
		}

		public ChessSquarePieceArray(ChessSquarePiece[][] board)
		{
			this.board = CopyBoard(board);
		}

		public static ChessSquarePieceArray EmptyBoard()
		{
			ChessSquarePieceArray returnValue = new ChessSquarePieceArray();

			returnValue.board = new ChessSquarePiece[8][];
			for (int i = 0; i < 8; i++)
			{
				returnValue.board[i] = new ChessSquarePiece[8];
				for (int j = 0; j < 8; j++)
					returnValue.board[i][j] = ChessSquarePiece.Empty;
			}

			return returnValue;
		}

		public ChessSquarePiece GetPiece(int file, int rank)
		{
			return this.board[file][rank];
		}

		public ChessSquarePiece GetPiece(ChessSquare chessSquare)
		{
			return this.board[chessSquare.File][chessSquare.Rank];
		}

		public ChessSquarePieceArray SetPiece(int file, int rank, ChessSquarePiece piece)
		{
			ChessSquarePiece[][] newBoard = new ChessSquarePiece[8][];

			for (int i = 0; i < 8; i++)
			{
				if (i != file)
					newBoard[i] = this.board[i];
				else
				{
					newBoard[i] = new ChessSquarePiece[8];
					for (int j = 0; j < 8; j++)
						newBoard[i][j] = this.board[i][j];
					newBoard[i][rank] = piece;
				}
			}

			ChessSquarePieceArray returnValue = new ChessSquarePieceArray();
			returnValue.board = newBoard;

			return returnValue;
		}

		private static ChessSquarePiece[][] CopyBoard(ChessSquarePiece[][] board)
		{
			ChessSquarePiece[][] newBoard = new ChessSquarePiece[8][];
			for (int i = 0; i < 8; i++)
			{
				newBoard[i] = new ChessSquarePiece[8];
				for (int j = 0; j < 8; j++)
					newBoard[i][j] = board[i][j];
			}

			return newBoard;
		}
	}
}
