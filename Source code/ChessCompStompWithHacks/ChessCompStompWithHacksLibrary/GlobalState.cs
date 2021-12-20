
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
			bool isWebBrowserVersion,
			bool debugMode,
			int? initialMusicVolume,
			bool showSoundAndMusicVolumePicker)
		{
			this.Fps = fps;
			this.Rng = rng;
			this.GuidGenerator = guidGenerator;
			this.Logger = logger;
			this.Timer = timer;
			this.IsWebBrowserVersion = isWebBrowserVersion;
			this.DebugMode = debugMode;
			this.desiredMusicVolume = initialMusicVolume ?? GlobalState.DEFAULT_VOLUME;
			this.currentMusicVolume = this.desiredMusicVolume;

			int elapsedMicrosPerFrame = 1000 * 1000 / fps;

			this.MusicPlayer = new MusicPlayer(elapsedMicrosPerFrame: elapsedMicrosPerFrame);
			this.ElapsedMicrosPerFrame = elapsedMicrosPerFrame;

			this.ShowSoundAndMusicVolumePicker = showSoundAndMusicVolumePicker;
		}

		public int Fps { get; private set; }
		public IDTRandom Rng { get; private set; }
		public GuidGenerator GuidGenerator { get; private set; }
		public IDTLogger Logger { get; private set; }
		public ITimer Timer { get; private set; }
		public bool IsWebBrowserVersion { get; private set; }
		public bool DebugMode { get; private set; }
		public bool ShowSoundAndMusicVolumePicker { get; private set; }

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

		public void RenderMusic(IMusicOutput<ChessMusic> musicOutput)
		{
			this.MusicPlayer.RenderMusic(musicOutput: musicOutput, userVolume: this.currentMusicVolume);
		}
	}
}
