
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;

	public class ChessPiecesRendererFadeOutFadeIn
	{
		private const int TIME_TO_FADE_IN = 500 * 1000;
		private const int TIME_TO_FADE_OUT = 500 * 1000;

		private int elapsedMicros;
		private ColorTheme colorTheme;

		public ChessPiecesRendererFadeOutFadeIn(ColorTheme colorTheme)
		{
			this.elapsedMicros = 0;
			this.colorTheme = colorTheme;
		}

		public bool HasFinishedFadingIn()
		{
			return this.elapsedMicros >= TIME_TO_FADE_OUT + TIME_TO_FADE_IN;
		}

		public bool HasFinishedFadingOut()
		{
			return this.elapsedMicros >= TIME_TO_FADE_OUT;
		}

		public void ProcessFrame(int elapsedMicrosPerFrame)
		{
			this.elapsedMicros += elapsedMicrosPerFrame;

			if (this.elapsedMicros > TIME_TO_FADE_OUT + TIME_TO_FADE_IN)
				this.elapsedMicros = TIME_TO_FADE_OUT + TIME_TO_FADE_IN + 1;
		}

		public void Render(IDisplayOutput<GameImage, GameFont> displayOutput)
		{
			int width = displayOutput.GetWidth(GameImage.WhitePawn) * GameImageUtil.ChessPieceScalingFactor / 128;
			int height = displayOutput.GetHeight(GameImage.WhitePawn) * GameImageUtil.ChessPieceScalingFactor / 128;

			DTColor darkSquareColor = ChessPiecesRenderer.GetDarkSquareColor(colorTheme: this.colorTheme);
			DTColor lightSquareColor = ChessPiecesRenderer.GetLightSquareColor(colorTheme: this.colorTheme);

			int alpha;
			if (!this.HasFinishedFadingOut())
			{
				alpha = this.elapsedMicros * 255 / TIME_TO_FADE_OUT;
				if (alpha < 0)
					alpha = 0;
				if (alpha > 255)
					alpha = 255;
			}
			else if (!this.HasFinishedFadingIn())
			{
				alpha = 255 - (this.elapsedMicros - TIME_TO_FADE_OUT) * 255 / TIME_TO_FADE_IN;
				if (alpha < 0)
					alpha = 0;
				if (alpha > 255)
					alpha = 255;
			}
			else
				alpha = 0;

			darkSquareColor = new DTColor(r: darkSquareColor.R, g: darkSquareColor.G, b: darkSquareColor.B, alpha: alpha);
			lightSquareColor = new DTColor(r: lightSquareColor.R, g: lightSquareColor.G, b: lightSquareColor.B, alpha: alpha);

			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					displayOutput.DrawRectangle(
						x: i * width,
						y: j * height,
						width: width,
						height: height,
						color: (i + j) % 2 == 0 ? darkSquareColor : lightSquareColor,
						fill: true);
				}
			}
		}
	}
}
