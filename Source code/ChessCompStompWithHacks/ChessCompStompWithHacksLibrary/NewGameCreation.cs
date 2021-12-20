
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System;
	using System.Collections.Generic;

	public class NewGameCreation
	{
		public static GameState CreateNewGame(bool isPlayerWhite, DTImmutableList<Hack> researchedHacks, SessionState.AIHackLevel aiHackLevel)
		{
			HashSet<Hack> hacks = new HashSet<Hack>();
			for (int i = 0; i < researchedHacks.Count; i++)
				hacks.Add(researchedHacks[i]);

			ChessSquarePiece[][] board = new ChessSquarePiece[8][];
			for (int i = 0; i < 8; i++)
			{
				board[i] = new ChessSquarePiece[8];
				for (int j = 0; j < 8; j++)
					board[i][j] = ChessSquarePiece.Empty;
			}

			for (int i = 0; i < 8; i++)
			{
				board[i][1] = ChessSquarePiece.WhitePawn;
				board[i][6] = ChessSquarePiece.BlackPawn;
			}

			board[0][0] = ChessSquarePiece.WhiteRook;
			board[1][0] = ChessSquarePiece.WhiteKnight;
			board[2][0] = ChessSquarePiece.WhiteBishop;
			board[3][0] = ChessSquarePiece.WhiteQueen;
			board[4][0] = ChessSquarePiece.WhiteKing;
			board[5][0] = ChessSquarePiece.WhiteBishop;
			board[6][0] = ChessSquarePiece.WhiteKnight;
			board[7][0] = ChessSquarePiece.WhiteRook;

			board[0][7] = ChessSquarePiece.BlackRook;
			board[1][7] = ChessSquarePiece.BlackKnight;
			board[2][7] = ChessSquarePiece.BlackBishop;
			board[3][7] = ChessSquarePiece.BlackQueen;
			board[4][7] = ChessSquarePiece.BlackKing;
			board[5][7] = ChessSquarePiece.BlackBishop;
			board[6][7] = ChessSquarePiece.BlackKnight;
			board[7][7] = ChessSquarePiece.BlackRook;

			if (hacks.Contains(Hack.ExtraQueen))
			{
				if (isPlayerWhite)
				{
					board[3][1] = ChessSquarePiece.WhiteQueen;
					board[3][2] = ChessSquarePiece.WhitePawn;
				}
				else
				{
					board[3][6] = ChessSquarePiece.BlackQueen;
					board[3][5] = ChessSquarePiece.BlackPawn;
				}
			}

			int numberOfExtraPawns = 0;
			if (hacks.Contains(Hack.ExtraPawnFirst))
				numberOfExtraPawns++;
			if (hacks.Contains(Hack.ExtraPawnSecond))
				numberOfExtraPawns++;

			while (numberOfExtraPawns > 0)
			{
				if (isPlayerWhite)
				{
					if (board[3][2] == ChessSquarePiece.Empty)
						board[3][2] = ChessSquarePiece.WhitePawn;
					else if (board[4][2] == ChessSquarePiece.Empty)
						board[4][2] = ChessSquarePiece.WhitePawn;
					else if (board[2][2] == ChessSquarePiece.Empty)
						board[2][2] = ChessSquarePiece.WhitePawn;
					else
						throw new Exception();
				}
				else
				{
					if (board[3][5] == ChessSquarePiece.Empty)
						board[3][5] = ChessSquarePiece.BlackPawn;
					else if (board[4][5] == ChessSquarePiece.Empty)
						board[4][5] = ChessSquarePiece.BlackPawn;
					else if (board[2][5] == ChessSquarePiece.Empty)
						board[2][5] = ChessSquarePiece.BlackPawn;
					else
						throw new Exception();
				}

				numberOfExtraPawns--;
			}
			
			switch (aiHackLevel)
			{
				case SessionState.AIHackLevel.Initial:
					// make no additional changes to the board
					break;
				case SessionState.AIHackLevel.UpgradedOnce:
					if (isPlayerWhite)
					{
						for (int i = 0; i < 8; i++)
						{
							board[i][5] = ChessSquarePiece.BlackPawn;
						}
					}
					else
					{
						for (int i = 0; i < 8; i++)
						{
							board[i][2] = ChessSquarePiece.WhitePawn;
						}
					}
					break;
				case SessionState.AIHackLevel.UpgradedTwice:
					if (isPlayerWhite)
					{
						for (int i = 0; i < 8; i++)
						{
							board[i][5] = ChessSquarePiece.BlackPawn;
						}

						board[0][6] = ChessSquarePiece.BlackRook;
						board[1][6] = ChessSquarePiece.BlackKnight;
						board[2][6] = ChessSquarePiece.BlackBishop;
						board[3][6] = ChessSquarePiece.BlackQueen;
						board[4][6] = ChessSquarePiece.BlackQueen;
						board[5][6] = ChessSquarePiece.BlackBishop;
						board[6][6] = ChessSquarePiece.BlackKnight;
						board[7][6] = ChessSquarePiece.BlackRook;
					}
					else
					{
						for (int i = 0; i < 8; i++)
						{
							board[i][2] = ChessSquarePiece.WhitePawn;
						}

						board[0][1] = ChessSquarePiece.WhiteRook;
						board[1][1] = ChessSquarePiece.WhiteKnight;
						board[2][1] = ChessSquarePiece.WhiteBishop;
						board[3][1] = ChessSquarePiece.WhiteQueen;
						board[4][1] = ChessSquarePiece.WhiteQueen;
						board[5][1] = ChessSquarePiece.WhiteBishop;
						board[6][1] = ChessSquarePiece.WhiteKnight;
						board[7][1] = ChessSquarePiece.WhiteRook;
					}
					break;
				case SessionState.AIHackLevel.UpgradedThrice:
					if (isPlayerWhite)
					{
						for (int i = 0; i < 8; i++)
						{
							board[i][4] = ChessSquarePiece.BlackPawn;
						}

						for (int j = 5; j <= 6; j++)
						{
							board[0][j] = ChessSquarePiece.BlackRook;
							board[1][j] = ChessSquarePiece.BlackKnight;
							board[2][j] = ChessSquarePiece.BlackBishop;
							board[3][j] = ChessSquarePiece.BlackQueen;
							board[4][j] = ChessSquarePiece.BlackQueen;
							board[5][j] = ChessSquarePiece.BlackBishop;
							board[6][j] = ChessSquarePiece.BlackKnight;
							board[7][j] = ChessSquarePiece.BlackRook;
						}
					}
					else
					{
						for (int i = 0; i < 8; i++)
						{
							board[i][3] = ChessSquarePiece.WhitePawn;
						}

						for (int j = 1; j <= 2; j++)
						{
							board[0][j] = ChessSquarePiece.WhiteRook;
							board[1][j] = ChessSquarePiece.WhiteKnight;
							board[2][j] = ChessSquarePiece.WhiteBishop;
							board[3][j] = ChessSquarePiece.WhiteQueen;
							board[4][j] = ChessSquarePiece.WhiteQueen;
							board[5][j] = ChessSquarePiece.WhiteBishop;
							board[6][j] = ChessSquarePiece.WhiteKnight;
							board[7][j] = ChessSquarePiece.WhiteRook;
						}
					}
					break;
				case SessionState.AIHackLevel.FinalBattle:
					if (isPlayerWhite)
					{
						for (int i = 0; i < 8; i++)
						{
							board[i][4] = ChessSquarePiece.BlackPawn;
						}

						for (int i = 0; i < 8; i++)
						{
							for (int j = 5; j <= 7; j++)
							{
								if (board[i][j] != ChessSquarePiece.BlackKing)
									board[i][j] = ChessSquarePiece.BlackQueen;
							}
						}
					}
					else
					{
						for (int i = 0; i < 8; i++)
						{
							board[i][3] = ChessSquarePiece.WhitePawn;
						}

						for (int i = 0; i < 8; i++)
						{
							for (int j = 0; j <= 2; j++)
							{
								if (board[i][j] != ChessSquarePiece.WhiteKing)
									board[i][j] = ChessSquarePiece.WhiteQueen;
							}
						}
					}
					break;
				default: throw new Exception();
			}

			bool[][] unmovedPawns = new bool[8][];
			for (int i = 0; i < 8; i++)
			{
				unmovedPawns[i] = new bool[8];
				for (int j = 0; j < 8; j++)
					unmovedPawns[i][j] = board[i][j] == ChessSquarePiece.WhitePawn || board[i][j] == ChessSquarePiece.BlackPawn;
			}

			return new GameState(
				board: new ChessSquarePieceArray(board),
				unmovedPawns: new UnmovedPawnsArray(unmovedPawns),
				turnCount: 1,
				hasUsedNuke: false,
				isPlayerWhite: isPlayerWhite,
				isWhiteTurn: true,
				previousPawnMoveFileForEnPassant: null,
				castlingRights: new GameState.CastlingRights(
					canWhiteCastleKingside: true,
					canWhiteCastleQueenside: true,
					canBlackCastleKingside: true,
					canBlackCastleQueenside: true),
				playerAbilities: new GameState.PlayerAbilities(
					canPawnsMoveThreeSpacesInitially: hacks.Contains(Hack.PawnsCanMoveThreeSpacesInitially),
					canSuperEnPassant: hacks.Contains(Hack.SuperEnPassant),
					canRooksMoveLikeBishops: hacks.Contains(Hack.RooksCanMoveLikeBishops),
					canSuperCastle: hacks.Contains(Hack.SuperCastling),
					canRooksCaptureLikeCannons: hacks.Contains(Hack.RooksCanCaptureLikeCannons),
					canKnightsMakeLargeKnightsMove: hacks.Contains(Hack.KnightsCanMakeLargeKnightsMove),
					canQueensMoveLikeKnights: hacks.Contains(Hack.QueensCanMoveLikeKnights),
					hasTacticalNuke: hacks.Contains(Hack.TacticalNuke),
					hasAnyPieceCanPromote: hacks.Contains(Hack.AnyPieceCanPromote),
					hasStalemateIsVictory: hacks.Contains(Hack.StalemateIsVictory),
					hasOpponentMustCaptureWhenPossible: hacks.Contains(Hack.OpponentMustCaptureWhenPossible),
					hasPawnsDestroyCapturingPiece: hacks.Contains(Hack.PawnsDestroyCapturingPiece)));
		}
	}
}
