
namespace ChessCompStompWithHacksLibrary
{
	using DTLibrary;
	using System;

	public class Credits_DesignAndCoding
	{
		private ColorTheme colorTheme;
		private Button viewLicenseButton;

		private int height;

		private bool isWebBrowserVersion;
		private bool isWebPortalVersion;

		private bool isHoverOverGitHubUrl;

		public Credits_DesignAndCoding(ColorTheme colorTheme, int height, bool isWebBrowserVersion, bool isWebPortalVersion)
		{
			this.colorTheme = colorTheme;

			this.height = height;

			this.isWebBrowserVersion = isWebBrowserVersion;
			this.isWebPortalVersion = isWebPortalVersion;

			this.isHoverOverGitHubUrl = false;

			if (isWebPortalVersion)
			{
				this.viewLicenseButton = new Button(
					x: 170,
					y: height - 145,
					width: 235,
					height: 20,
					backgroundColor: new DTColor(235, 235, 235),
					hoverColor: ColorThemeUtil.GetHoverColor(colorTheme: colorTheme),
					clickColor: ColorThemeUtil.GetClickColor(colorTheme: colorTheme),
					text: "View Bridge.NET license text",
					textXOffset: 5,
					textYOffset: 3,
					font: ChessFont.ChessFont12Pt);
			}
			else
			{
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
		}

		private static bool IsHoverOverGitHubUrl(IMouse mouseInput, int height)
		{
			int mouseX = mouseInput.GetX();
			int mouseY = mouseInput.GetY();

			return 107 <= mouseX && mouseX <= 107 + 353
				&& height - 265 <= mouseY && mouseY <= height - 265 + 32;
		}

		public class Result
		{
			public Result(bool clickedButton, string clickUrl)
			{
				this.ClickedButton = clickedButton;
				this.ClickUrl = clickUrl;
			}

			public bool ClickedButton { get; private set; }
			public string ClickUrl { get; private set; }
		}

		public Result ProcessFrame(
			IMouse mouseInput,
			IMouse previousMouseInput,
			ISoundOutput<ChessSound> soundOutput)
		{
			bool clickedButton;
			if (this.isWebBrowserVersion)
				clickedButton = this.viewLicenseButton.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput);
			else
				clickedButton = false;

			if (this.isWebPortalVersion)
				this.isHoverOverGitHubUrl = IsHoverOverGitHubUrl(mouseInput: mouseInput, height: this.height);

			string clickUrl = null;

			if (this.isHoverOverGitHubUrl)
			{
				string a = "htt";
				string b = "/githu";
				string c = "udo";
				string d = "ps:/";
				string e = "dts";
				string f = "b.com/";

				clickUrl = a + d + b + f + e + c;
			}

			return new Result(clickedButton: clickedButton, clickUrl: clickUrl);
		}

		private static string GetInfo()
		{
			string str = "ddit";

			string str2 = "tsudo";

			str = "Re" + str;

			return str + ": /u/d" + str2;
		}

		private static string GetWebBrowserNonWebPortalVersionText()
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

		private static string GetWebPortalVersionText()
		{
			string returnValue = "Design and coding by dtsudo " + "\n"
				+ "\n"
				+ "The source code is written in C#. Bridge.NET is used to transpile the" + "\n"
				+ "C# source code into javascript. Bridge.NET is licensed under Apache" + "\n"
				+ "License 2.0." + "\n"
				+ "\n"
				+ "\n"
				+ GetInfo();

			return returnValue;
		}

		private static string GetDesktopVersionText()
		{
			return "";
		}

		public void Render(IDisplayOutput<ChessImage, ChessFont> displayOutput)
		{
			if (this.isWebBrowserVersion)
			{
				if (this.isWebPortalVersion)
				{
					string text = GetWebPortalVersionText();

					displayOutput.DrawText(
						x: 10,
						y: this.height - 10,
						text: text,
						font: ChessFont.ChessFont20Pt,
						color: DTColor.Black());

					displayOutput.DrawText(
						x: 10,
						y: this.height - 10 - 226,
						text: "GitHub:",
						font: ChessFont.ChessFont20Pt,
						color: DTColor.Black());

					displayOutput.DrawText(
						x: 109,
						y: this.height - 10 - 226,
						text: "https://github.com/dtsudo",
						font: ChessFont.ChessFont20Pt,
						color: this.isHoverOverGitHubUrl ? new DTColor(0, 0, 255) : DTColor.Black());

					this.viewLicenseButton.Render(displayOutput: displayOutput);
				}
				else
				{
					string text = GetWebBrowserNonWebPortalVersionText();

					displayOutput.DrawText(
						x: 10,
						y: this.height - 10,
						text: text,
						font: ChessFont.ChessFont20Pt,
						color: DTColor.Black());
					
					this.viewLicenseButton.Render(displayOutput: displayOutput);
				}
			}
			else
			{
				string text = GetDesktopVersionText();

				displayOutput.DrawText(
					x: 10,
					y: this.height - 10,
					text: text,
					font: ChessFont.ChessFont20Pt,
					color: DTColor.Black());
			}
		}
	}
}
