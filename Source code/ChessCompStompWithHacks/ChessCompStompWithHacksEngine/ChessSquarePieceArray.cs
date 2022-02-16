
namespace ChessCompStompWithHacksEngine
{
	using System;
	using System.Collections.Generic;

	public class ChessSquarePieceArray : IEquatable<ChessSquarePieceArray>
	{
		private ChessSquarePiece[][] board;

		private int? hashCode;

		private ChessSquarePieceArray()
		{
			this.board = null;
			this.hashCode = null;
		}

		public ChessSquarePieceArray(ChessSquarePiece[][] board)
		{
			this.board = CopyBoard(board);
			this.hashCode = null;
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

		public override bool Equals(object obj)
		{
			return this.Equals(obj as ChessSquarePieceArray);
		}

		public bool Equals(ChessSquarePieceArray other)
		{
			if (other == null)
				return false;

			if (this == other)
				return true;

			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					if (this.board[i][j] != other.board[i][j])
						return false;
				}
			}

			return true;
		}

		public override int GetHashCode()
		{
			if (this.hashCode.HasValue)
				return this.hashCode.Value;

			int hashCode = 0;

			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					hashCode = unchecked(hashCode * 31);
					int pieceHashCode = this.board[i][j].GetValueForHashCode();
					hashCode = unchecked(hashCode + pieceHashCode);
				}
			}
			
			this.hashCode = hashCode;
			return this.hashCode.Value;
		}
	}
}
