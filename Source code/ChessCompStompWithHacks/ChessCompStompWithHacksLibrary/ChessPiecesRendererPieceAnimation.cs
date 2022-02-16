
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using System.Collections.Generic;

	public class ChessPiecesRendererPieceAnimation
	{
		public class PieceAnimation
		{
			public const int ANIMATION_DURATION_MICROS = 100 * 1000;

			public PieceAnimation(
				ChessSquarePiece piece,
				ChessSquare originSquare,
				int elapsedMicros)
			{
				this.Piece = piece;
				this.OriginSquare = originSquare;
				this.ElapsedMicros = elapsedMicros;
			}

			public ChessSquarePiece Piece { get; private set; }
			public ChessSquare OriginSquare { get; private set; }
			public int ElapsedMicros { get; private set; }
		}

		private PieceAnimation[][] pieceAnimations;

		private ChessPiecesRendererPieceAnimation(PieceAnimation[][] pieceAnimations)
		{
			this.pieceAnimations = pieceAnimations;
		}

		public static ChessPiecesRendererPieceAnimation EmptyChessPiecesRendererPieceAnimation()
		{
			PieceAnimation[][] pieceAnimations = new PieceAnimation[8][];
			for (int i = 0; i < 8; i++)
			{
				pieceAnimations[i] = new PieceAnimation[8];
				for (int j = 0; j < 8; j++)
					pieceAnimations[i][j] = null;
			}
			return new ChessPiecesRendererPieceAnimation(pieceAnimations: pieceAnimations);
		}

		public static ChessPiecesRendererPieceAnimation GetChessPiecesRendererPieceAnimation()
		{
			return EmptyChessPiecesRendererPieceAnimation();
		}

		private static PieceAnimation[][] CopyPieceAnimations(PieceAnimation[][] pieceAnimations)
		{
			PieceAnimation[][] newPieceAnimations = new PieceAnimation[8][];
			for (int i = 0; i < 8; i++)
			{
				newPieceAnimations[i] = new PieceAnimation[8];
				for (int j = 0; j < 8; j++)
					newPieceAnimations[i][j] = pieceAnimations[i][j];
			}

			return newPieceAnimations;
		}

		public ChessPiecesRendererPieceAnimation AddRawMove(int startingFile, int startingRank, int endingFile, int endingRank, ChessSquarePiece piece)
		{
			PieceAnimation[][] newPieceAnimations = CopyPieceAnimations(pieceAnimations: this.pieceAnimations);

			newPieceAnimations[startingFile][startingRank] = null;
			
			newPieceAnimations[endingFile][endingRank] = new PieceAnimation(
				piece: piece,
				originSquare: new ChessSquare(file: startingFile, rank: startingRank),
				elapsedMicros: 0);

			return new ChessPiecesRendererPieceAnimation(pieceAnimations: newPieceAnimations);
		}

		public ChessPiecesRendererPieceAnimation AddMove(GameState originalGameState, DisplayMove displayMove, bool shouldMoveBeInstant)
		{
			if (displayMove.Move.IsCastlingOrSuperCastling(originalBoard: originalGameState.Board))
			{
				PieceAnimation[][] newPieceAnimations = CopyPieceAnimations(pieceAnimations: this.pieceAnimations);
				CastlingUtil.CastlingInfo castlingInfo = CastlingUtil.GetCastlingOrSuperCastlingMoveInfo(originalBoard: originalGameState.Board, castlingMove: displayMove.Move);

				newPieceAnimations[castlingInfo.OriginalLocationOfKing.File][castlingInfo.OriginalLocationOfKing.Rank] = null;
				newPieceAnimations[castlingInfo.OriginalLocationOfRook.File][castlingInfo.OriginalLocationOfRook.Rank] = null;

				newPieceAnimations[castlingInfo.NewLocationOfKing.File][castlingInfo.NewLocationOfKing.Rank] = new PieceAnimation(
					piece: originalGameState.Board.GetPiece(castlingInfo.OriginalLocationOfKing),
					originSquare: castlingInfo.OriginalLocationOfKing,
					elapsedMicros: 0);

				newPieceAnimations[castlingInfo.NewLocationOfRook.File][castlingInfo.NewLocationOfRook.Rank] = new PieceAnimation(
					piece: originalGameState.Board.GetPiece(castlingInfo.OriginalLocationOfRook),
					originSquare: castlingInfo.OriginalLocationOfRook,
					elapsedMicros: 0);

				if (shouldMoveBeInstant)
				{
					if (displayMove.StartingFile.Value == displayMove.Move.StartingFile.Value
						&& displayMove.StartingRank.Value == displayMove.Move.StartingRank.Value
						&& displayMove.EndingFile == displayMove.Move.EndingFile
						&& displayMove.EndingRank == displayMove.Move.EndingRank)
					{
						newPieceAnimations[castlingInfo.NewLocationOfKing.File][castlingInfo.NewLocationOfKing.Rank] = null;
					}
				}

				return new ChessPiecesRendererPieceAnimation(pieceAnimations: newPieceAnimations);
			}

			return this.AddMove(originalGameState: originalGameState, move: displayMove.Move, shouldMoveBeInstant: shouldMoveBeInstant);
		}

		public ChessPiecesRendererPieceAnimation AddMove(GameState originalGameState, Move move, bool shouldMoveBeInstant)
		{
			PieceAnimation[][] newPieceAnimations = CopyPieceAnimations(pieceAnimations: this.pieceAnimations);

			if (move.IsNuke)
			{
				List<ChessSquare> nukedSquares = TacticalNukeUtil.GetNukedSquares(file: move.EndingFile, rank: move.EndingRank);

				foreach (ChessSquare nukedSquare in nukedSquares)
					newPieceAnimations[nukedSquare.File][nukedSquare.Rank] = null;

				return new ChessPiecesRendererPieceAnimation(pieceAnimations: newPieceAnimations);
			}

			if (move.IsCastlingOrSuperCastling(originalBoard: originalGameState.Board))
			{
				CastlingUtil.CastlingInfo castlingInfo = CastlingUtil.GetCastlingOrSuperCastlingMoveInfo(originalBoard: originalGameState.Board, castlingMove: move);

				newPieceAnimations[castlingInfo.OriginalLocationOfKing.File][castlingInfo.OriginalLocationOfKing.Rank] = null;
				newPieceAnimations[castlingInfo.OriginalLocationOfRook.File][castlingInfo.OriginalLocationOfRook.Rank] = null;

				if (shouldMoveBeInstant)
				{
					newPieceAnimations[castlingInfo.NewLocationOfKing.File][castlingInfo.NewLocationOfKing.Rank] = null;
					newPieceAnimations[castlingInfo.NewLocationOfRook.File][castlingInfo.NewLocationOfRook.Rank] = null;
				}
				else
				{
					newPieceAnimations[castlingInfo.NewLocationOfKing.File][castlingInfo.NewLocationOfKing.Rank] = new PieceAnimation(
						piece: originalGameState.Board.GetPiece(castlingInfo.OriginalLocationOfKing),
						originSquare: castlingInfo.OriginalLocationOfKing,
						elapsedMicros: 0);
					
					newPieceAnimations[castlingInfo.NewLocationOfRook.File][castlingInfo.NewLocationOfRook.Rank] = new PieceAnimation(
						piece: originalGameState.Board.GetPiece(castlingInfo.OriginalLocationOfRook),
						originSquare: castlingInfo.OriginalLocationOfRook,
						elapsedMicros: 0);
				}

				return new ChessPiecesRendererPieceAnimation(pieceAnimations: newPieceAnimations);
			}

			newPieceAnimations[move.StartingFile.Value][move.StartingRank.Value] = null;

			if (shouldMoveBeInstant)
				newPieceAnimations[move.EndingFile][move.EndingRank] = null;
			else
				newPieceAnimations[move.EndingFile][move.EndingRank] = new PieceAnimation(
					piece: originalGameState.Board.GetPiece(file: move.StartingFile.Value, rank: move.StartingRank.Value),
					originSquare: new ChessSquare(file: move.StartingFile.Value, rank: move.StartingRank.Value),
					elapsedMicros: 0);

			return new ChessPiecesRendererPieceAnimation(pieceAnimations: newPieceAnimations);
		}

		public PieceAnimation[][] GetPieceAnimations()
		{
			return CopyPieceAnimations(pieceAnimations: this.pieceAnimations);
		}

		public ChessPiecesRendererPieceAnimation ProcessFrame(int elapsedMicrosPerFrame)
		{
			PieceAnimation[][] newPieceAnimations = CopyPieceAnimations(pieceAnimations: this.pieceAnimations);

			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					if (newPieceAnimations[i][j] == null)
						continue;
					int newElapsedMicros = newPieceAnimations[i][j].ElapsedMicros + elapsedMicrosPerFrame;
					if (newElapsedMicros >= PieceAnimation.ANIMATION_DURATION_MICROS)
						newPieceAnimations[i][j] = null;
					else
						newPieceAnimations[i][j] = new PieceAnimation(
							piece: newPieceAnimations[i][j].Piece,
							originSquare: newPieceAnimations[i][j].OriginSquare,
							elapsedMicros: newElapsedMicros);
				}
			}

			return new ChessPiecesRendererPieceAnimation(pieceAnimations: newPieceAnimations);
		}
	}
}
