
namespace ChessCompStompWithHacksEngine
{
	public static class GameStateUtil
	{
		public static GameState GetGameStateWithoutNukeAbility(GameState gameState)
		{
			if (gameState.Abilities.HasTacticalNuke == false)
				return gameState;

			return new GameState(
				board: gameState.Board,
				unmovedPawns: gameState.UnmovedPawns,
				turnCount: gameState.TurnCount,
				hasUsedNuke: false,
				isPlayerWhite: gameState.IsPlayerWhite,
				isWhiteTurn: gameState.IsWhiteTurn,
				previousPawnMoveFileForEnPassant: gameState.PreviousPawnMoveFileForEnPassant,
				castlingRights: gameState.Castling,
				playerAbilities: new GameState.PlayerAbilities(
					canPawnsMoveThreeSpacesInitially: gameState.Abilities.CanPawnsMoveThreeSpacesInitially,
					canSuperEnPassant: gameState.Abilities.CanSuperEnPassant,
					canRooksMoveLikeBishops: gameState.Abilities.CanRooksMoveLikeBishops,
					canSuperCastle: gameState.Abilities.CanSuperCastle,
					canRooksCaptureLikeCannons: gameState.Abilities.CanRooksCaptureLikeCannons,
					canKnightsMakeLargeKnightsMove: gameState.Abilities.CanKnightsMakeLargeKnightsMove,
					canQueensMoveLikeKnights: gameState.Abilities.CanQueensMoveLikeKnights,
					hasTacticalNuke: false,
					hasAnyPieceCanPromote: gameState.Abilities.HasAnyPieceCanPromote,
					hasStalemateIsVictory: gameState.Abilities.HasStalemateIsVictory,
					hasOpponentMustCaptureWhenPossible: gameState.Abilities.HasOpponentMustCaptureWhenPossible,
					hasPawnsDestroyCapturingPiece: gameState.Abilities.HasPawnsDestroyCapturingPiece));
		}
	}
}
