
namespace DTLibrary
{
	using System;

	public class DisplayLogger : IDTLogger
	{
		private string[] lines;
		private int x;
		private int y;

		private const int NUMBER_OF_LINES_TO_LOG = 5;

		public DisplayLogger(int x, int y)
		{
			this.x = x;
			this.y = y;
			this.lines = new string[NUMBER_OF_LINES_TO_LOG + 1];
			for (int i = 0; i < this.lines.Length; i++)
				this.lines[i] = "";
		}

		public void Render<ImageEnum, FontEnum>(IDisplayOutput<ImageEnum, FontEnum> displayOutput, FontEnum font, DTColor color)
		{
			string text = "";

			if (this.lines[this.lines.Length - 1].Length > 0)
			{
				for (int i = 1; i < this.lines.Length; i++)
					text = text + this.lines[i] + "\n";
			}
			else
			{
				for (int i = 0; i < this.lines.Length - 1; i++)
					text = text + this.lines[i] + "\n";
			}
			
			displayOutput.TryDrawText(
				x: this.x,
				y: this.y,
				text: text,
				font: font,
				color: color);
		}

		public void Write(string str)
		{
			while (true)
			{
				int index = str.IndexOf('\n');

				if (index < 0)
				{
					this.lines[this.lines.Length - 1] = this.lines[this.lines.Length - 1] + str;
					break;
				}

				string line = str.Substring(0, index);
				this.lines[this.lines.Length - 1] = this.lines[this.lines.Length - 1] + line;

				for (int i = 1; i < this.lines.Length; i++)
					this.lines[i - 1] = this.lines[i];
				this.lines[this.lines.Length - 1] = "";

				str = str.Substring(index + 1);
			}
		}

		public void WriteLine(string str)
		{
			this.Write(str + "\n");
		}

		public void WriteLine()
		{
			this.Write("\n");
		}
	}
}
