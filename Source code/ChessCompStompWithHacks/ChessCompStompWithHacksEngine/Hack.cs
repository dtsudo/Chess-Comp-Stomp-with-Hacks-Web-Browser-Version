
namespace ChessCompStompWithHacksEngine
{
	public enum Hack
	{
		ExtraPawnFirst,
		ExtraPawnSecond,
		ExtraQueen,

		PawnsCanMoveThreeSpacesInitially,
		SuperEnPassant,
		RooksCanMoveLikeBishops,
		SuperCastling,
		RooksCanCaptureLikeCannons,
		KnightsCanMakeLargeKnightsMove,
		QueensCanMoveLikeKnights,

		TacticalNuke,
		AnyPieceCanPromote,
		StalemateIsVictory,
		OpponentMustCaptureWhenPossible,
		PawnsDestroyCapturingPiece
	}
}
