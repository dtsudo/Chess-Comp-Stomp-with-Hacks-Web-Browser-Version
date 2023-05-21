
namespace DTLibrary
{
	using System;
	using System.Collections.Generic;

	public static class StringUtil
	{
		private static bool IsDigit(char c)
		{
			return c == '0'
				|| c == '1'
				|| c == '2'
				|| c == '3'
				|| c == '4'
				|| c == '5'
				|| c == '6'
				|| c == '7'
				|| c == '8'
				|| c == '9';
		}

		public static int? TryParseInt(string str)
		{
			if (str == null)
				return null;

			if (str == "")
				return null;

			if (str[0] != '-' && !IsDigit(str[0]))
				return null;

			if (str == "-")
				return null;

			for (int i = 1; i < str.Length; i++)
			{
				if (!IsDigit(str[i]))
					return null;
			}

			if (str == "-2147483648")
				return int.MinValue;

			if (str[0] == '-')
			{
				int? result = TryParseInt(str.Substring(1));
				if (result == null)
					return null;
				return -(result.Value);
			}

			return TryParseIntHelper(str);
		}

		private static int? TryParseIntHelper(string str)
		{
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

			int? leastSignificantDigit = TryParseIntHelper(str.Substring(str.Length - 1));
			int? restOfNumber = TryParseIntHelper(str.Substring(0, str.Length - 1));

			if (leastSignificantDigit == null || restOfNumber == null)
				return null;

			try
			{
				int number = checked(leastSignificantDigit.Value + 10 * restOfNumber.Value);
				return number;
			}
			catch (OverflowException)
			{
				return null;
			}
		}

		public static int ParseAsIntCultureInvariant(this string str)
		{
			return ParseInt(str);
		}

		public static int ParseInt(string str)
		{
			int? val = TryParseInt(str);

			if (val == null)
				throw new Exception("str does not represent an int: " + str);

			return val.Value;
		}

		/// <summary>
		/// Returns null if the string does not represent a long
		/// </summary>
		public static long? TryParseLong(string str)
		{
			if (str == null)
				return null;

			if (str == "")
				return null;

			if (str[0] != '-' && !IsDigit(str[0]))
				return null;

			if (str == "-")
				return null;
			
			for (int i = 1; i < str.Length; i++)
			{
				if (!IsDigit(str[i]))
					return null;
			}

			if (str == "-9223372036854775808")
				return long.MinValue;

			if (str[0] == '-')
			{
				long? result = TryParseLong(str.Substring(1));
				if (result == null)
					return null;
				return -1 * result.Value;
			}

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

			long? leastSignificantDigit = TryParseLong(str.Substring(str.Length - 1));
			long? restOfNumber = TryParseLong(str.Substring(0, str.Length - 1));

			if (leastSignificantDigit == null || restOfNumber == null)
				return null;

			try
			{
				long number = checked(leastSignificantDigit.Value + 10 * restOfNumber.Value);
				return number;
			}
			catch (OverflowException)
			{
				return null;
			}
		}

		public static string ToUpperCaseCultureInvariant(this string str)
		{
			char[] array = new char[str.Length];

			for (int i = 0; i < array.Length; i++)
			{
				char c = str[i];

				if (c < 'a' || c > 'z')
					array[i] = c;
				else
					array[i] = (char) (c - 32);
			}

			return new string(array);
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

		public static string ToStringCultureInvariant(this long l)
		{
			if (l == long.MinValue)
				return "-9223372036854775808";

			return LongToStringHelper(l);
		}

		private static string LongToStringHelper(long l)
		{
			switch (l)
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

			if (l < 0)
				return "-" + LongToStringHelper(-l);

			long x = l / 10L;
			long y = l % 10L;

			return LongToStringHelper(x) + LongToStringHelper(y);
		}

		public class CultureInvariantComparer : IComparer<string>
		{
			public int Compare(string x, string y)
			{
				if (x == null && y == null)
					return 0;

				if (x == null)
					return -1;

				if (y == null)
					return 1;

				int index = 0;

				while (true)
				{
					if (index == x.Length && index == y.Length)
						return 0;

					if (index == x.Length)
						return -1;

					if (index == y.Length)
						return 1;

					char c1 = x[index];
					char c2 = y[index];

					if (c1 < c2)
						return -1;

					if (c1 > c2)
						return 1;

					index++;
				}
			}
		}
	}
}
