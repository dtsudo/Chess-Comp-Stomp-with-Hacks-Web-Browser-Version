
namespace ChessCompStompWithHacksEngine
{
	using System;
	using System.Collections.Generic;

	public class GameState : IEquatable<GameState>
	{
		public class PlayerAbilities : IEquatable<PlayerAbilities>
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

			public override bool Equals(object obj)
			{
				return this.Equals(obj as PlayerAbilities);
			}

			public bool Equals(PlayerAbilities other)
			{
				if (this == other)
					return true;

				return other != null &&
					   this.CanPawnsMoveThreeSpacesInitially == other.CanPawnsMoveThreeSpacesInitially &&
					   this.CanSuperEnPassant == other.CanSuperEnPassant &&
					   this.CanRooksMoveLikeBishops == other.CanRooksMoveLikeBishops &&
					   this.CanSuperCastle == other.CanSuperCastle &&
					   this.CanRooksCaptureLikeCannons == other.CanRooksCaptureLikeCannons &&
					   this.CanKnightsMakeLargeKnightsMove == other.CanKnightsMakeLargeKnightsMove &&
					   this.CanQueensMoveLikeKnights == other.CanQueensMoveLikeKnights &&
					   this.HasTacticalNuke == other.HasTacticalNuke &&
					   this.HasAnyPieceCanPromote == other.HasAnyPieceCanPromote &&
					   this.HasStalemateIsVictory == other.HasStalemateIsVictory &&
					   this.HasOpponentMustCaptureWhenPossible == other.HasOpponentMustCaptureWhenPossible &&
					   this.HasPawnsDestroyCapturingPiece == other.HasPawnsDestroyCapturingPiece;
			}

			public override int GetHashCode()
			{
				int hashCode = 0;
				if (this.CanPawnsMoveThreeSpacesInitially)
					hashCode = hashCode | 1;
				if (this.CanSuperEnPassant)
					hashCode = hashCode | (1 << 1);
				if (this.CanRooksMoveLikeBishops)
					hashCode = hashCode | (1 << 2);
				if (this.CanSuperCastle)
					hashCode = hashCode | (1 << 3);
				if (this.CanRooksCaptureLikeCannons)
					hashCode = hashCode | (1 << 4);
				if (this.CanKnightsMakeLargeKnightsMove)
					hashCode = hashCode | (1 << 5);
				if (this.CanQueensMoveLikeKnights)
					hashCode = hashCode | (1 << 6);
				if (this.HasTacticalNuke)
					hashCode = hashCode | (1 << 7);
				if (this.HasAnyPieceCanPromote)
					hashCode = hashCode | (1 << 8);
				if (this.HasStalemateIsVictory)
					hashCode = hashCode | (1 << 9);
				if (this.HasOpponentMustCaptureWhenPossible)
					hashCode = hashCode | (1 << 10);
				if (this.HasPawnsDestroyCapturingPiece)
					hashCode = hashCode | (1 << 11);

				return hashCode;
			}
		}

		/// <summary>
		/// Tracks whether castling is still allowed.
		/// 
		/// Note that this doesn't affect super-castling, which allows the player to castle
		/// regardless of whether the king or rook has ever moved.
		/// </summary>
		public class CastlingRights : IEquatable<CastlingRights>
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

			public override bool Equals(object obj)
			{
				return this.Equals(obj as CastlingRights);
			}

			public bool Equals(CastlingRights other)
			{
				return other != null &&
					   this.CanWhiteCastleKingside == other.CanWhiteCastleKingside &&
					   this.CanWhiteCastleQueenside == other.CanWhiteCastleQueenside &&
					   this.CanBlackCastleKingside == other.CanBlackCastleKingside &&
					   this.CanBlackCastleQueenside == other.CanBlackCastleQueenside;
			}

			public override int GetHashCode()
			{
				int hashCode = 0;
				if (this.CanWhiteCastleKingside)
					hashCode = hashCode | 1;
				if (this.CanWhiteCastleQueenside)
					hashCode = hashCode | 2;
				if (this.CanBlackCastleKingside)
					hashCode = hashCode | 4;
				if (this.CanBlackCastleQueenside)
					hashCode = hashCode | 8;
				return hashCode;
			}
		}

		public GameState(
			ChessSquarePieceArray board,
			UnmovedPawnsArray unmovedPawns,
			int turnCount,
			bool hasUsedNuke,
			bool isPlayerWhite,
			bool isWhiteTurn,
			int? previousPawnMoveFileForEnPassant,
			int? previousPawnMoveRankForEnPassant,
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
			this.PreviousPawnMoveRankForEnPassant = previousPawnMoveRankForEnPassant;
			this.Castling = castlingRights;
			this.Abilities = playerAbilities;

			this.hashCode = null;
		}

		public ChessSquarePieceArray Board { get; private set; }
		
		public UnmovedPawnsArray UnmovedPawns { get; private set; }

		// At the beginning of the game, TurnCount = 1.
		public int TurnCount { get; private set; }
		public bool HasUsedNuke { get; private set; }

		public bool IsPlayerWhite { get; private set; }
		public bool IsWhiteTurn { get; private set; }

		public int? PreviousPawnMoveFileForEnPassant { get; private set; }
		public int? PreviousPawnMoveRankForEnPassant { get; private set; }

		public CastlingRights Castling { get; private set; }

		public PlayerAbilities Abilities { get; private set; }

		private int? hashCode;

		public bool IsPlayerTurn()
		{
			if (this.IsPlayerWhite && this.IsWhiteTurn)
				return true;
			if (!this.IsPlayerWhite && !this.IsWhiteTurn)
				return true;
			return false;
		}

		public override bool Equals(object obj)
		{
			return this.Equals(obj as GameState);
		}

		public bool Equals(GameState other)
		{
			if (other == null)
				return false;

			if (this == other)
				return true;

			if (this.TurnCount != other.TurnCount)
				return false;

			this.ComputeHashCode();
			other.ComputeHashCode();

			if (this.hashCode.Value != other.hashCode.Value)
				return false;

			if (!this.Board.Equals(other.Board))
				return false;
			
			if (this.IsPlayerWhite != other.IsPlayerWhite)
				return false;

			if (this.IsWhiteTurn != other.IsWhiteTurn)
				return false;

			if (this.HasUsedNuke != other.HasUsedNuke)
				return false;

			if (this.PreviousPawnMoveFileForEnPassant.HasValue && !other.PreviousPawnMoveFileForEnPassant.HasValue
					|| !this.PreviousPawnMoveFileForEnPassant.HasValue && other.PreviousPawnMoveFileForEnPassant.HasValue
					|| this.PreviousPawnMoveFileForEnPassant.HasValue && other.PreviousPawnMoveFileForEnPassant.HasValue && this.PreviousPawnMoveFileForEnPassant.Value != other.PreviousPawnMoveFileForEnPassant.Value)
				return false;

			if (this.PreviousPawnMoveRankForEnPassant.HasValue && !other.PreviousPawnMoveRankForEnPassant.HasValue
					|| !this.PreviousPawnMoveRankForEnPassant.HasValue && other.PreviousPawnMoveRankForEnPassant.HasValue
					|| this.PreviousPawnMoveRankForEnPassant.HasValue && other.PreviousPawnMoveRankForEnPassant.HasValue && this.PreviousPawnMoveRankForEnPassant.Value != other.PreviousPawnMoveRankForEnPassant.Value)
				return false;

			if (!this.UnmovedPawns.Equals(other.UnmovedPawns))
				return false;

			if (!this.Castling.Equals(other.Castling))
				return false;

			if (!this.Abilities.Equals(other.Abilities))
				return false;

			return true;
		}

		private void ComputeHashCode()
		{
			if (this.hashCode.HasValue)
				return;

			int hashCode = 0;

			if (this.IsPlayerWhite)
				hashCode = hashCode | 1;
			if (this.HasUsedNuke)
				hashCode = hashCode | 2;
			if (this.IsWhiteTurn)
				hashCode = hashCode | 4;

			int unmovedPawnsHashCode = this.UnmovedPawns.GetHashCode();
			hashCode = unchecked(hashCode * 17 + unmovedPawnsHashCode);

			int castlingHashCode = this.Castling.GetHashCode();
			hashCode = unchecked(hashCode * 17 + castlingHashCode);

			int abilitiesHashCode = this.Abilities.GetHashCode();
			hashCode = unchecked(hashCode * 17 + abilitiesHashCode);

			int previousPawnMoveFileForEnPassantHashCode = this.PreviousPawnMoveFileForEnPassant.HasValue
				? this.PreviousPawnMoveFileForEnPassant.Value + 1
				: 0;
			hashCode = unchecked(hashCode * 17 + previousPawnMoveFileForEnPassantHashCode);

			int previousPawnMoveRankForEnPassantHashCode = this.PreviousPawnMoveRankForEnPassant.HasValue
				? this.PreviousPawnMoveRankForEnPassant.Value + 1
				: 0;
			hashCode = unchecked(hashCode * 17 + previousPawnMoveRankForEnPassantHashCode);

			int turnCountHashCode = this.TurnCount;
			hashCode = unchecked(hashCode * 17 + turnCountHashCode);

			int boardHashCode = this.Board.GetHashCode();
			hashCode = unchecked(hashCode * 17 + boardHashCode);

			this.hashCode = hashCode;
		}

		public override int GetHashCode()
		{
			this.ComputeHashCode();
			return this.hashCode.Value;
		}
	}
}
