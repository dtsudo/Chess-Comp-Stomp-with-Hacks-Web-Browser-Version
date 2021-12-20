
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System;
	using System.Collections.Generic;

	public class Credits_Images
	{
		public static void Render(IDisplayOutput<ChessImage, ChessFont> displayOutput, int width, int height)
		{
			string text = "The images of chess pieces were created by Cburnett" + "\n"
				+ "(https://en.wikipedia.org/wiki/User:Cburnett) and are licensed under" + "\n"
				+ "the BSD license." + "\n"
				+ "\n"
				+ "The game also uses sprites from Kenney Asset Pack." + "\n"
				+ "These sprites are licensed under Creative Commons Zero." + "\n"
				+ "(https://www.kenney.nl)";

			displayOutput.DrawText(
				x: 10,
				y: height - 10,
				text: text,
				font: ChessFont.Fetamont20Pt,
				color: DTColor.Black());
		}
	}
}
