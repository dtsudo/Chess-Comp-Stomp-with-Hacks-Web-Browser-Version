
namespace ChessCompStompWithHacksLibrary
{
	using DTLibrary;

	public class GlobalState
	{
		public const int DEFAULT_VOLUME = 50;

		public GlobalState(
			int fps,
			IDTRandom rng,
			GuidGenerator guidGenerator,
			IDTLogger logger,
			ITimer timer,
			IFileIO fileIO,
			BuildType buildType,
			bool debugMode,
			bool useDebugAI,
			int? initialMusicVolume)
		{
			this.Fps = fps;
			this.Rng = rng;
			this.GuidGenerator = guidGenerator;
			this.Logger = logger;
			this.Timer = timer;
			this.FileIO = fileIO;
			this.BuildType = buildType;
			this.DebugMode = debugMode;
			this.desiredMusicVolume = initialMusicVolume ?? GlobalState.DEFAULT_VOLUME;
			this.currentMusicVolume = this.desiredMusicVolume;

			int elapsedMicrosPerFrame = 1000 * 1000 / fps;

			this.MusicPlayer = new MusicPlayer(elapsedMicrosPerFrame: elapsedMicrosPerFrame);
			this.ElapsedMicrosPerFrame = elapsedMicrosPerFrame;

			this.saveAndLoadData = new SaveAndLoadData(fileIO: fileIO, versionInfo: VersionHistory.GetVersionInfo());

			this.UseDebugAI = useDebugAI;
		}

		public int Fps { get; private set; }
		public IDTRandom Rng { get; private set; }
		public GuidGenerator GuidGenerator { get; private set; }
		public IDTLogger Logger { get; private set; }
		public ITimer Timer { get; private set; }
		public IFileIO FileIO { get; private set; }

		public BuildType BuildType { get; private set; }

		public bool DebugMode { get; private set; }

		private SaveAndLoadData saveAndLoadData;

		public bool UseDebugAI { get; set; }

		private int desiredMusicVolume;
		private int currentMusicVolume;
		public int MusicVolume
		{
			get { return this.desiredMusicVolume; }
			set { this.desiredMusicVolume = value; }
		}

		public MusicPlayer MusicPlayer { get; private set; }

		public int ElapsedMicrosPerFrame { get; private set; }

		public void ProcessMusic()
		{
			this.MusicPlayer.ProcessFrame();
			this.currentMusicVolume = VolumeUtil.GetVolumeSmoothed(
				elapsedMicrosPerFrame: this.ElapsedMicrosPerFrame,
				currentVolume: this.currentMusicVolume,
				desiredVolume: this.desiredMusicVolume);
		}

		public void RenderMusic(IMusicOutput<GameMusic> musicOutput)
		{
			this.MusicPlayer.RenderMusic(musicOutput: musicOutput, userVolume: this.currentMusicVolume);
		}

		public void SaveData(SessionState sessionState, int soundVolume)
		{
			this.saveAndLoadData.SaveData(sessionState: sessionState, soundVolume: soundVolume, musicVolume: this.desiredMusicVolume);
		}

		public void LoadSessionState(SessionState sessionState)
		{
			this.saveAndLoadData.LoadSessionState(sessionState: sessionState);
		}

		public int? LoadSoundVolume()
		{
			return this.saveAndLoadData.LoadSoundVolume();
		}

		public void LoadMusicVolume()
		{
			int? musicVolume = this.saveAndLoadData.LoadMusicVolume();

			if (musicVolume.HasValue)
			{
				this.desiredMusicVolume = musicVolume.Value;
				this.currentMusicVolume = musicVolume.Value;
			}
		}
	}
}
