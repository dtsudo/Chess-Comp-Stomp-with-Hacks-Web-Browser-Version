
namespace ChessCompStompWithHacksEngine
{
	using System;

	public class ChessSquare : IEquatable<ChessSquare>
	{
		public ChessSquare(int file, int rank)
		{
			this.File = file;
			this.Rank = rank;
		}

		public int File { get; private set; }
		public int Rank { get; private set; }

		public override bool Equals(object obj)
		{
			return Equals(obj as ChessSquare);
		}

		public bool Equals(ChessSquare other)
		{
			return other != null &&
				   this.File == other.File &&
				   this.Rank == other.Rank;
		}

		public override int GetHashCode()
		{
			return (this.File << 3) + this.Rank;
		}
	}
}
