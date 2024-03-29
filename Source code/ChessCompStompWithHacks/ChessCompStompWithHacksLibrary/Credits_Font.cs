﻿
namespace ChessCompStompWithHacksLibrary
{
	using DTLibrary;

	public class Credits_Font
	{
		private ColorTheme colorTheme;
		private Button viewLicenseButton;

		private int height;

		public Credits_Font(ColorTheme colorTheme, int height)
		{
			this.colorTheme = colorTheme;

			this.height = height;

			this.viewLicenseButton = new Button(
				x: 10,
				y: height - 203,
				width: 250,
				height: 50,
				backgroundColor: new DTColor(235, 235, 235),
				hoverColor: ColorThemeUtil.GetHoverColor(colorTheme: colorTheme),
				clickColor: ColorThemeUtil.GetClickColor(colorTheme: colorTheme),
				text: "View license text",
				textXOffset: 10,
				textYOffset: 11,
				font: GameFont.GameFont20Pt);
		}

		/// <summary>
		/// Returns true iff the user clicked the "view license" button
		/// </summary>
		public bool ProcessFrame(
			IMouse mouseInput,
			IMouse previousMouseInput,
			ISoundOutput<GameSound> soundOutput)
		{
			bool clickedButton = this.viewLicenseButton.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput);

			return clickedButton;
		}

		public void Render(IDisplayOutput<GameImage, GameFont> displayOutput)
		{
			string text = "The font used in this game was generated by metaflop and then" + "\n"
				+ "slightly modified by dtsudo." + "\n"
				+ "https://www.metaflop.com/modulator" + "\n"
				+ "\n"
				+ "The font is licensed under SIL Open Font License v1.1";

			displayOutput.DrawText(
				x: 10,
				y: this.height - 10,
				text: text,
				font: GameFont.GameFont20Pt,
				color: DTColor.Black());

			this.viewLicenseButton.Render(displayOutput: displayOutput);
		}
	}
}
