
namespace ChessCompStompWithHacksLibrary
{
	using DTLibrary;

	public class Credits_Images
	{
		private ColorTheme colorTheme;
		private Button viewLicenseButton;

		private int height;

		public Credits_Images(ColorTheme colorTheme, int height)
		{
			this.colorTheme = colorTheme;

			this.height = height;

			this.viewLicenseButton = new Button(
				x: 10,
				y: height - 147,
				width: 250,
				height: 50,
				backgroundColor: new DTColor(235, 235, 235),
				hoverColor: ColorThemeUtil.GetHoverColor(colorTheme: colorTheme),
				clickColor: ColorThemeUtil.GetClickColor(colorTheme: colorTheme),
				text: "View license text",
				textXOffset: 10,
				textYOffset: 11,
				font: ChessFont.ChessFont20Pt);
		}

		/// <summary>
		/// Returns true iff the user clicked the "view license" button
		/// </summary>
		public bool ProcessFrame(
			IMouse mouseInput,
			IMouse previousMouseInput,
			ISoundOutput<ChessSound> soundOutput)
		{
			bool clickedButton = this.viewLicenseButton.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput);

			return clickedButton;
		}

		public void Render(IDisplayOutput<ChessImage, ChessFont> displayOutput)
		{
			string text = "The images of chess pieces were created by Cburnett" + "\n"
				+ "(https://en.wikipedia.org/wiki/User:Cburnett) and are licensed under" + "\n"
				+ "the BSD license." + "\n"
				+ "\n"
				+ "\n"
				+ "\n"
				+ "The game also uses sprites from Kenney Asset Pack." + "\n"
				+ "These sprites are licensed under Creative Commons Zero." + "\n"
				+ "(https://www.kenney.nl)";

			displayOutput.DrawText(
				x: 10,
				y: this.height - 10,
				text: text,
				font: ChessFont.ChessFont20Pt,
				color: DTColor.Black());

			this.viewLicenseButton.Render(displayOutput: displayOutput);
		}
	}
}
