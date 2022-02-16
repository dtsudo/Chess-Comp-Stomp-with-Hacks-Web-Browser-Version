
namespace ChessCompStompWithHacksEngine
{
	using System;
	using System.Collections.Generic;

	public class UnmovedPawnsArray : IEquatable<UnmovedPawnsArray>
	{
		private bool[][] board;
		private int? hashCode;

		private UnmovedPawnsArray()
		{
			this.board = null;
			this.hashCode = null;
		}

		public UnmovedPawnsArray(bool[][] board)
		{
			this.board = CopyBoard(board);
			this.hashCode = null;
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

		public override bool Equals(object obj)
		{
			return this.Equals(obj as UnmovedPawnsArray);
		}

		public bool Equals(UnmovedPawnsArray other)
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
					hashCode = unchecked(hashCode * 17);
					if (this.board[i][j])
						hashCode = unchecked(hashCode + 1);
				}
			}
			
			this.hashCode = hashCode;
			return this.hashCode.Value;
		}
	}
}
