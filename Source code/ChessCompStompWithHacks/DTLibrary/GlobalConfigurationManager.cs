
namespace DTLibrary
{
	public class GlobalConfigurationManager
	{
		public class GlobalConfiguration
		{
			public const int DEFAULT_FPS = 60;
			public const int MIN_FPS = 10;
			public const int MAX_FPS = 300;

			public const bool DEFAULT_DEBUG_MODE = false;

			public GlobalConfiguration(int fps, bool debugMode)
			{
				this.Fps = fps;
				this.DebugMode = debugMode;
			}

			public int Fps { get; private set; }
			public bool DebugMode { get; private set; }
		}

		/// <summary>
		/// If no saved configuration is found (or the saved configuration is invalid), returns a default GlobalConfiguration.
		/// </summary>
		public static GlobalConfiguration GetGlobalConfiguration(IFileIO fileIO, int fileId)
		{
			ByteList byteList = fileIO.FetchData(fileId: fileId);

			if (byteList == null)
				return new GlobalConfiguration(fps: GlobalConfiguration.DEFAULT_FPS, debugMode: GlobalConfiguration.DEFAULT_DEBUG_MODE);

			ByteList.Iterator iterator = byteList.GetIterator();

			int fps = GlobalConfiguration.DEFAULT_FPS;
			bool debugMode = GlobalConfiguration.DEFAULT_DEBUG_MODE;

			try
			{
				while (true)
				{
					if (!iterator.HasNextByte())
						break;

					string str = TryDeserializeAsciiLineOfInput(byteListIterator: iterator);

					string[] array = str.Split('=');

					if (array.Length < 2)
						continue;

					if (array[0] == "fps")
					{
						string fpsString = array[1];

						int? possibleFpsValue = StringUtil.TryParseInt(fpsString);

						if (possibleFpsValue.HasValue)
							fps = possibleFpsValue.Value;
					}

					if (array[0] == "debugmode")
					{
						if (array[1] == "true")
							debugMode = true;
						if (array[1] == "false")
							debugMode = false;
					}
				}
			}
			catch (DTDeserializationException)
			{
			}

			if (fps < GlobalConfiguration.MIN_FPS)
				fps = GlobalConfiguration.MIN_FPS;
			if (fps > GlobalConfiguration.MAX_FPS)
				fps = GlobalConfiguration.MAX_FPS;

			return new GlobalConfiguration(fps: fps, debugMode: debugMode);
		}

		public static void SaveGlobalConfiguration(GlobalConfiguration globalConfiguration, IFileIO fileIO, int fileId)
		{
			ByteList.Builder builder = new ByteList.Builder();

			int fps = globalConfiguration.Fps;
			string fpsString = "fps=" + fps.ToStringCultureInvariant();

			SerializeAsciiLineOfInput(asciiString: fpsString, byteListBuilder: builder);

			bool debugMode = globalConfiguration.DebugMode;
			string debugModeString = "debugmode=" + (debugMode ? "true" : "false");

			SerializeAsciiLineOfInput(asciiString: debugModeString, byteListBuilder: builder);

			fileIO.PersistData(fileId: fileId, data: builder.ToByteList());
		}

		/// <summary>
		/// Can possibly throw DTDeserializationException
		/// </summary>
		private static string TryDeserializeAsciiLineOfInput(ByteList.Iterator byteListIterator)
		{
			string line = "";

			while (true)
			{
				if (!byteListIterator.HasNextByte())
					return line;

				byte b = byteListIterator.TryPop();
				char c = (char) b;

				if (c == '\n')
					return line;

				line += c.ToString();
			}
		}

		private static void SerializeAsciiLineOfInput(string asciiString, ByteList.Builder byteListBuilder)
		{
			foreach (char c in asciiString)
			{
				byte b = (byte) c;
				byteListBuilder.Add(b);
			}

			char newline = '\n';
			byte newLineAsByte = (byte) newline;
			byteListBuilder.Add(newLineAsByte);
		}
	}
}
