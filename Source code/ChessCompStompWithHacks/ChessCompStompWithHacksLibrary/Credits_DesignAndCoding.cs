
namespace ChessCompStompWithHacksLibrary
{
	using DTLibrary;

	public class Credits_DesignAndCoding
	{
		private ColorTheme colorTheme;
		private Button viewLicenseButton;

		private int height;

		private bool isWebBrowserVersion;

		public Credits_DesignAndCoding(ColorTheme colorTheme, int height, bool isWebBrowserVersion)
		{
			this.colorTheme = colorTheme;

			this.height = height;

			this.isWebBrowserVersion = isWebBrowserVersion;

			this.viewLicenseButton = new Button(
				x: 10,
				y: height - 320,
				width: 400,
				height: 50,
				backgroundColor: new DTColor(235, 235, 235),
				hoverColor: ColorThemeUtil.GetHoverColor(colorTheme: colorTheme),
				clickColor: ColorThemeUtil.GetClickColor(colorTheme: colorTheme),
				text: "View Bridge.NET license text",
				textXOffset: 11,
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
			bool clickedButton;
			if (this.isWebBrowserVersion)
				clickedButton = this.viewLicenseButton.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput);
			else
				clickedButton = false;

			return clickedButton;
		}

		private static string GetWebBrowserVersionText()
		{
			return "Design and coding by dtsudo (https://github.com/dtsudo)" + "\n"
				+ "\n"
				+ "This game is open source, under the MIT license." + "\n"
				+ "\n"
				+ "The source code is written in C#." + "\n"
				+ "\n"
				+ "Bridge.NET is used to transpile the C# source code into javascript." + "\n"
				+ "Bridge.NET is licensed under Apache License 2.0." + "\n"
				+ "(https://github.com/bridgedotnet/Bridge)";
		}

		private static string GetDesktopVersionText()
		{
			return "";
		}

		public void Render(IDisplayOutput<ChessImage, ChessFont> displayOutput)
		{
			string text = this.isWebBrowserVersion ? GetWebBrowserVersionText() : GetDesktopVersionText();

			displayOutput.DrawText(
				x: 10,
				y: this.height - 10,
				text: text,
				font: ChessFont.ChessFont20Pt,
				color: DTColor.Black());

			if (this.isWebBrowserVersion)
				this.viewLicenseButton.Render(displayOutput: displayOutput);
		}
	}
}
