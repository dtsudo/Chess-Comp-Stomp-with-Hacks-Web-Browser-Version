
namespace ChessCompStompWithHacksEngine
{
	public class GameState
	{
		public class PlayerAbilities
		{
			public PlayerAbilities(
				bool canPawnsMoveThreeSpacesInitially,
				bool canSuperEnPassant,
				bool canRooksMoveLikeBishops,
				bool canSuperCastle,
				bool canRooksCaptureLikeCannons,
				bool canKnightsMakeLargeKnightsMove,
				bool canQueensMoveLikeKnights,
				bool hasTacticalNuke,
				bool hasAnyPieceCanPromote,
				bool hasStalemateIsVictory,
				bool hasOpponentMustCaptureWhenPossible,
				bool hasPawnsDestroyCapturingPiece)
			{
				this.CanPawnsMoveThreeSpacesInitially = canPawnsMoveThreeSpacesInitially;
				this.CanSuperEnPassant = canSuperEnPassant;
				this.CanRooksMoveLikeBishops = canRooksMoveLikeBishops;
				this.CanSuperCastle = canSuperCastle;
				this.CanRooksCaptureLikeCannons = canRooksCaptureLikeCannons;
				this.CanKnightsMakeLargeKnightsMove = canKnightsMakeLargeKnightsMove;
				this.CanQueensMoveLikeKnights = canQueensMoveLikeKnights;

				this.HasTacticalNuke = hasTacticalNuke;
				this.HasAnyPieceCanPromote = hasAnyPieceCanPromote;
				this.HasStalemateIsVictory = hasStalemateIsVictory;
				this.HasOpponentMustCaptureWhenPossible = hasOpponentMustCaptureWhenPossible;
				this.HasPawnsDestroyCapturingPiece = hasPawnsDestroyCapturingPiece;
			}

			public bool CanPawnsMoveThreeSpacesInitially { get; private set; }
			public bool CanSuperEnPassant { get; private set; }
			public bool CanRooksMoveLikeBishops { get; private set; }
			public bool CanSuperCastle { get; private set; }
			public bool CanRooksCaptureLikeCannons { get; private set; }
			public bool CanKnightsMakeLargeKnightsMove { get; private set; }
			public bool CanQueensMoveLikeKnights { get; private set; }

			public bool HasTacticalNuke { get; private set; }
			public bool HasAnyPieceCanPromote { get; private set; }
			public bool HasStalemateIsVictory { get; private set; }
			public bool HasOpponentMustCaptureWhenPossible { get; private set; }
			public bool HasPawnsDestroyCapturingPiece { get; private set; }
		}

		/// <summary>
		/// Tracks whether castling is still allowed.
		/// 
		/// Note that this doesn't affect super-castling, which allows the player to castle
		/// regardless of whether the king or rook has ever moved.
		/// </summary>
		public class CastlingRights
		{
			public CastlingRights(
				bool canWhiteCastleKingside,
				bool canWhiteCastleQueenside,
				bool canBlackCastleKingside,
				bool canBlackCastleQueenside)
			{
				this.CanWhiteCastleKingside = canWhiteCastleKingside;
				this.CanWhiteCastleQueenside = canWhiteCastleQueenside;
				this.CanBlackCastleKingside = canBlackCastleKingside;
				this.CanBlackCastleQueenside = canBlackCastleQueenside;
			}

			public bool CanWhiteCastleKingside { get; private set; }
			public bool CanWhiteCastleQueenside { get; private set; }
			public bool CanBlackCastleKingside { get; private set; }
			public bool CanBlackCastleQueenside { get; private set; }
		}

		public GameState(
			ChessSquarePieceArray board,
			UnmovedPawnsArray unmovedPawns,
			int turnCount,
			bool hasUsedNuke,
			bool isPlayerWhite,
			bool isWhiteTurn,
			int? previousPawnMoveFileForEnPassant,
			CastlingRights castlingRights,
			PlayerAbilities playerAbilities)
		{
			this.Board = board;
			this.UnmovedPawns = unmovedPawns;
			this.TurnCount = turnCount;
			this.HasUsedNuke = hasUsedNuke;
			this.IsPlayerWhite = isPlayerWhite;
			this.IsWhiteTurn = isWhiteTurn;
			this.PreviousPawnMoveFileForEnPassant = previousPawnMoveFileForEnPassant;
			this.Castling = castlingRights;
			this.Abilities = playerAbilities;
		}

		public ChessSquarePieceArray Board { get; private set; }
		
		public UnmovedPawnsArray UnmovedPawns { get; private set; }

		// At the beginning of the game, TurnCount = 1.
		public int TurnCount { get; private set; }
		public bool HasUsedNuke { get; private set; }

		public bool IsPlayerWhite { get; private set; }
		public bool IsWhiteTurn { get; private set; }

		public int? PreviousPawnMoveFileForEnPassant { get; private set; }

		public CastlingRights Castling { get; private set; }

		public PlayerAbilities Abilities { get; private set; }

		public bool IsPlayerTurn()
		{
			if (this.IsPlayerWhite && this.IsWhiteTurn)
				return true;
			if (!this.IsPlayerWhite && !this.IsWhiteTurn)
				return true;
			return false;
		}
	}
}
