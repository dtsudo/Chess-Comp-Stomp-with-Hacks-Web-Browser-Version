
namespace DTLibrary
{
	public static class StringUtil
	{
		public static int StringToInt(string str)
		{
			if (str == "-2147483648")
				return int.MinValue;

			if (str[0] == '-')
				return -1 * StringToInt(str.Substring(1));

			if (str.Length == 1)
			{
				if (str == "0") return 0;
				if (str == "1") return 1;
				if (str == "2") return 2;
				if (str == "3") return 3;
				if (str == "4") return 4;
				if (str == "5") return 5;
				if (str == "6") return 6;
				if (str == "7") return 7;
				if (str == "8") return 8;
				if (str == "9") return 9;
			}

			return StringToInt(str.Substring(str.Length - 1)) + 10 * StringToInt(str.Substring(0, str.Length - 1));
		}

		public static string ToStringCultureInvariant(this int i)
		{
			if (i == int.MinValue)
				return "-2147483648";

			return IntToStringHelper(i);
		}

		private static string IntToStringHelper(int i)
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
				return "-" + IntToStringHelper(-i);

			int x = i / 10;
			int y = i % 10;

			return IntToStringHelper(x) + IntToStringHelper(y);
		}
	}
}
