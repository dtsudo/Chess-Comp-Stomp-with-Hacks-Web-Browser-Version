
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System.Collections.Generic;

	public class AIHackLevelSelectionBoardPreview
	{
		private static ChessSquare GetRenderSquare(int i, int j, bool renderFromWhitePerspective)
		{
			if (renderFromWhitePerspective)
				return new ChessSquare(i, j);

			return new ChessSquare(7 - i, 7 - j);
		}

		public static void Render(
			bool isPlayerWhite,
			DTImmutableList<Hack> researchedHacks,
			SessionState.AIHackLevel aiHackLevel,
			IDisplayOutput<GameImage, GameFont> displayOutput)
		{
			GameState gameState = NewGameCreation.CreateNewGame(
				isPlayerWhite: isPlayerWhite,
				researchedHacks: researchedHacks,
				aiHackLevel: aiHackLevel);

			ChessSquarePieceArray board = gameState.Board;

			int scalingFactorScaled = 128 / 12;

			int width = displayOutput.GetWidth(GameImage.WhitePawn) * scalingFactorScaled / 128;
			int height = displayOutput.GetHeight(GameImage.WhitePawn) * scalingFactorScaled / 128;

			displayOutput.DrawRectangle(
				x: 0,
				y: 0,
				width: width * 8 + 20,
				height: height * 8 + 20,
				color: DTColor.White(),
				fill:true );

			DTColor darkSquareColor = ChessPiecesRenderer.GetDarkSquareColor(colorTheme: ColorTheme.Final);
			DTColor lightSquareColor = ChessPiecesRenderer.GetLightSquareColor(colorTheme: ColorTheme.Final);

			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					ChessSquare renderSquare = GetRenderSquare(i: i, j: j, renderFromWhitePerspective: isPlayerWhite);

					displayOutput.DrawRectangle(
						x: 10 + renderSquare.File * width,
						y: 10 + renderSquare.Rank * height,
						width: width,
						height: height,
						color: (i + j) % 2 == 0 ? darkSquareColor : lightSquareColor,
						fill: true);
				}
			}

			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					ChessSquarePiece square = board.GetPiece(i, j);

					ChessSquare renderSquare = GetRenderSquare(i: i, j: j, renderFromWhitePerspective: isPlayerWhite);

					if (square == ChessSquarePiece.Empty)
						continue;

					displayOutput.DrawImageRotatedClockwise(
						image: GameImageUtil.GetImage(piece: square),
						x: 10 + renderSquare.File * width,
						y: 10 + renderSquare.Rank * height,
						degreesScaled: 0,
						scalingFactorScaled: scalingFactorScaled);
				}
			}
		}
	}
}
