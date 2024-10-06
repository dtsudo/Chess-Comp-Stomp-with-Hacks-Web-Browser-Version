﻿
namespace ChessCompStompWithHacksLibrary
{
	using DTLibrary;
	using System;

	public class Credits_DesignAndCodingDesktop
	{
		private ColorTheme colorTheme;
		private Button viewLicenseButton;

		private int height;

		private BuildType buildType;

		private bool isHoverOverGitHubUrl;

		public Credits_DesignAndCodingDesktop(ColorTheme colorTheme, int height, BuildType buildType)
		{
			this.colorTheme = colorTheme;

			this.height = height;

			this.buildType = buildType;

			this.isHoverOverGitHubUrl = false;

			if (buildType == BuildType.WebStandAlone || buildType == BuildType.Electron)
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
					font: GameFont.GameFont20Pt,
					isMobileDisplayType: false);
			}
			else if (buildType == BuildType.WebEmbedded)
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
					font: GameFont.GameFont12Pt,
					isMobileDisplayType: false);
			}
			else
			{
				throw new Exception();
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
			ISoundOutput<GameSound> soundOutput)
		{
			bool clickedButton = this.viewLicenseButton.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput);

			if (this.buildType == BuildType.WebEmbedded)
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

		private static string GetWebStandAloneVersionText()
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

		private static string GetWebEmbeddedVersionText()
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

		private static string GetElectronVersionText()
		{
			return "Design and coding by dtsudo (https://github.com/dtsudo)" + "\n"
				+ "\n"
				+ "This game is open source, under the MIT license." + "\n"
				+ "\n"
				+ "The source code is written in C#." + "\n"
				+ "\n"
				+ "Bridge.NET is used to transpile the C# source code into javascript." + "\n"
				+ "Bridge.NET is licensed under Apache License 2.0." + "\n"
				+ "(https://github.com/bridgedotnet/Bridge)" + "\n"
				+ "\n"
				+ "\n"
				+ "\n"
				+ "This game uses Electron; for Electron's licensing, see";
		}

		private static string GetInfo()
		{
			string str = "ddit";

			string str2 = "tsudo";

			str = "Re" + str;

			return str + ": /u/d" + str2;
		}

		public void Render(IDisplayOutput<GameImage, GameFont> displayOutput)
		{
			if (this.buildType == BuildType.WebStandAlone)
			{
				string text = GetWebStandAloneVersionText();

				displayOutput.DrawText(
					x: 10,
					y: this.height - 10,
					text: text,
					font: GameFont.GameFont20Pt,
					color: DTColor.Black());
			}
			else if (this.buildType == BuildType.WebEmbedded)
			{
				string text = GetWebEmbeddedVersionText();

				displayOutput.DrawText(
					x: 10,
					y: this.height - 10,
					text: text,
					font: GameFont.GameFont20Pt,
					color: DTColor.Black());

				displayOutput.DrawText(
					x: 10,
					y: this.height - 10 - 226,
					text: "GitHub:",
					font: GameFont.GameFont20Pt,
					color: DTColor.Black());

				displayOutput.DrawText(
					x: 109,
					y: this.height - 10 - 226,
					text: "https://github.com/dtsudo",
					font: GameFont.GameFont20Pt,
					color: this.isHoverOverGitHubUrl ? new DTColor(0, 0, 255) : DTColor.Black());
			}
			else if (this.buildType == BuildType.Electron)
			{
				string text = GetElectronVersionText();

				displayOutput.DrawText(
					x: 10,
					y: this.height - 10,
					text: text,
					font: GameFont.GameFont20Pt,
					color: DTColor.Black());

				displayOutput.DrawText(
					x: 10,
					y: this.height - 10 - 370,
					text: "https://github.com/electron/electron/blob/69586684484c05a0078e3b916239186a5c3d749a/LICENSE",
					font: GameFont.GameFont14Pt,
					color: DTColor.Black());
			}
			else
			{
				throw new Exception();
			}

			this.viewLicenseButton.Render(displayOutput: displayOutput);
		}
	}
}
