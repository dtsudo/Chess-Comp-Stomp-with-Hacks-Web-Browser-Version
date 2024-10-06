
namespace ChessCompStompWithHacksLibrary
{
	using DTLibrary;
	using System;

	public class Credits_DesignAndCodingMobile
	{		
		private static string GetWebVersionText(bool isLandscape)
		{
			if (isLandscape)
				return "Design and coding by dtsudo (https://github.com/dtsudo)" + "\n"
					+ "\n"
					+ "This game is open source, under the MIT license." + "\n"
					+ "\n"
					+ "Bridge.NET is used to transpile the C# source code into javascript." + "\n"
					+ "Bridge.NET is licensed under Apache License 2.0." + "\n"
					+ "\n"
					+ "See the source code for more information (including licensing details).";
			else
				return "Design and coding by dtsudo" + "\n"
					+ "(https://github.com/dtsudo)" + "\n"
					+ "\n"
					+ "This game is open source, under the MIT license." + "\n"
					+ "\n"
					+ "Bridge.NET is used to transpile the C# source" + "\n"
					+ "code into javascript. Bridge.NET is licensed" + "\n"
					+ "under Apache License 2.0." + "\n"
					+ "\n"
					+ "See the source code for more information" + "\n"
					+ "(including licensing details).";
		}

		private static string GetElectronVersionText(bool isLandscape)
		{
			if (isLandscape)
				return "Design and coding by dtsudo (https://github.com/dtsudo)" + "\n"
					+ "\n"
					+ "This game is open source, under the MIT license." + "\n"
					+ "\n"
					+ "Bridge.NET is used to transpile the C# source code into javascript." + "\n"
					+ "Bridge.NET is licensed under Apache License 2.0." + "\n"
					+ "\n"
					+ "This game uses the Electron framework." + "\n"
					+ "\n"
					+ "See the source code for more information (including licensing details).";
			else
				return "Design and coding by dtsudo" + "\n"
					+ "(https://github.com/dtsudo)" + "\n"
					+ "\n"
					+ "This game is open source, under the MIT license." + "\n"
					+ "\n"
					+ "Bridge.NET is used to transpile the C# source" + "\n"
					+ "code into javascript. Bridge.NET is licensed" + "\n"
					+ "under Apache License 2.0." + "\n"
					+ "\n"
					+ "This game uses the Electron framework." + "\n"
					+ "\n"
					+ "See the source code for more information" + "\n"
					+ "(including licensing details).";
		}

		public static void Render(
			IDisplayOutput<GameImage, GameFont> displayOutput,
			BuildType buildType,
			int width,
			int height)
		{
			bool isLandscape = displayOutput.IsMobileInLandscapeOrientation();
			
			if (buildType == BuildType.WebStandAlone || buildType == BuildType.WebEmbedded)
			{
				string text = GetWebVersionText(isLandscape: isLandscape);

				displayOutput.DrawText(
					x: 10,
					y: height - 10,
					text: text,
					font: GameFont.GameFont20Pt,
					color: DTColor.Black());
			}
			else if (buildType == BuildType.Electron)
			{
				string text = GetElectronVersionText(isLandscape: isLandscape);

				displayOutput.DrawText(
					x: 10,
					y: height - 10,
					text: text,
					font: GameFont.GameFont20Pt,
					color: DTColor.Black());
			}
			else
			{
				throw new Exception();
			}
		}
	}
}
