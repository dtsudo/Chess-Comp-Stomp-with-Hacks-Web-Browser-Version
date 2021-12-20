
namespace DTLibrary
{
	public class GuidGenerator
	{
		private int currentValue1;
		private int currentValue2;
		private string guidString;

		public GuidGenerator(string guidString)
		{
			this.currentValue1 = 0;
			this.currentValue2 = 0;
			this.guidString = guidString;
		}

		public string NextGuid()
		{
			if (this.currentValue1 == int.MaxValue)
			{
				this.currentValue1 = 0;
				this.currentValue2 = this.currentValue2 + 1;
			}
			else
				this.currentValue1 = this.currentValue1 + 1;

			string currentValue1AsString = IntToString(this.currentValue1);
			string currentValue2AsString = this.currentValue2 == 0
				? "0"
				: IntToString(this.currentValue2);
			return "g=" + this.guidString + "," + currentValue1AsString + "," + currentValue2AsString;
		}

		// Does not handle int.MinValue
		private static string IntToString(int i)
		{
			switch (i)
			{
				case 0: return "0";
				case 1: return "1";
				case 2: return "2";
				case 3: return "3";
				case 4: return "4";
				case 5: return "5";
				case 6: return "6";
				case 7: return "7";
				case 8: return "8";
				case 9: return "9";
			}

			if (i < 0)
				return "-" + IntToString(-i);

			int x = i / 10;
			int y = i % 10;

			return IntToString(x) + IntToString(y);
		}
	}
}
