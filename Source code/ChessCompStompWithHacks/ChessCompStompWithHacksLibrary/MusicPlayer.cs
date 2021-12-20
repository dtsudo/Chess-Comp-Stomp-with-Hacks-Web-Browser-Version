
namespace ChessCompStompWithHacksLibrary
{
	using DTLibrary;

	public class MusicPlayer
	{
		/// <summary>
		/// The current music being played, or null if no music is playing.
		/// 
		/// This may not be the same as intendedMusic since it takes a while
		/// to fade out an existing music and fade in a new one
		/// </summary>
		private ChessMusic? currentMusic;

		/// <summary>
		/// The intended music that should eventually play, or null if we should fade out all music
		/// </summary>
		private ChessMusic? intendedMusic;
		
		/// <summary>
		/// From 0 to 100 * 1024 (both inclusive)
		/// 
		/// Normally, this value is 100 * 1024.
		/// However, when fading in/out, this value will decrease to represent the drop in music volume.
		/// </summary>
		private int currentFadeInAndOutVolumeMillis;
		
		/// <summary>
		/// From 0 to 100.
		/// 
		/// For this.currentMusic, the intended volume at which the music should be played.
		/// We allow this to be set since we might want to play a particular music at a different
		/// volume depending on circumstances (e.g. maybe the music should be played softer when
		/// the game is paused)
		/// </summary>
		private int currentMusicVolume;

		/// <summary>
		/// From 0 to 100.
		/// 
		/// For this.intendedMusic, the intended volume at which the music should be played.
		/// </summary>
		private int intendedMusicVolume;
		
		private int elapsedMicrosPerFrame;

		public MusicPlayer(int elapsedMicrosPerFrame)
		{
			this.currentMusic = null;
			this.intendedMusic = null;
			this.currentFadeInAndOutVolumeMillis = 0;
			this.currentMusicVolume = 0;
			this.intendedMusicVolume = 0;

			this.elapsedMicrosPerFrame = elapsedMicrosPerFrame;
		}

		private void DecreaseCurrentFadeInAndOutVolumeMillis()
		{
			this.currentFadeInAndOutVolumeMillis = this.currentFadeInAndOutVolumeMillis - this.elapsedMicrosPerFrame / 10;
			if (this.currentFadeInAndOutVolumeMillis < 0)
				this.currentFadeInAndOutVolumeMillis = 0;
		}

		private void IncreaseCurrentFadeInAndOutVolumeMillis()
		{
			this.currentFadeInAndOutVolumeMillis = this.currentFadeInAndOutVolumeMillis + this.elapsedMicrosPerFrame / 10;
			if (this.currentFadeInAndOutVolumeMillis > 100 * 1024)
				this.currentFadeInAndOutVolumeMillis = 100 * 1024;
		}

		public void ProcessFrame()
		{
			if (this.intendedMusic == null)
			{
				if (this.currentMusic != null)
				{
					this.DecreaseCurrentFadeInAndOutVolumeMillis();
					if (this.currentFadeInAndOutVolumeMillis == 0)
						this.currentMusic = null;
				}

				return;
			}

			if (this.currentMusic == null)
			{
				this.currentMusic = this.intendedMusic;
				this.currentFadeInAndOutVolumeMillis = 0;
				this.currentMusicVolume = this.intendedMusicVolume;
				return;
			}

			if (this.currentMusic.Value != this.intendedMusic.Value)
			{
				this.DecreaseCurrentFadeInAndOutVolumeMillis();
				if (this.currentFadeInAndOutVolumeMillis == 0)
					this.currentMusic = null;
				return;
			}

			if (this.currentMusicVolume < this.intendedMusicVolume)
			{
				int delta = this.elapsedMicrosPerFrame / 5000;
				if (delta == 0)
					delta = 1;
				this.currentMusicVolume = this.currentMusicVolume + delta;
				if (this.currentMusicVolume > this.intendedMusicVolume)
					this.currentMusicVolume = this.intendedMusicVolume;
			}

			if (this.currentMusicVolume > this.intendedMusicVolume)
			{
				int delta = this.elapsedMicrosPerFrame / 5000;
				if (delta == 0)
					delta = 1;
				this.currentMusicVolume = this.currentMusicVolume - delta;
				if (this.currentMusicVolume < this.intendedMusicVolume)
					this.currentMusicVolume = this.intendedMusicVolume;
			}

			this.IncreaseCurrentFadeInAndOutVolumeMillis();
		}

		public void SetMusic(ChessMusic music, int volume)
		{
			this.intendedMusic = music;
			this.intendedMusicVolume = volume;
		}

		public void StopMusic()
		{
			this.intendedMusic = null;
		}
		
		public void RenderMusic(
			IMusicOutput<ChessMusic> musicOutput,
			// From 0 to 100
			int userVolume)
		{
			if (this.currentMusic != null)
				musicOutput.PlayMusic(
					music: this.currentMusic.Value,
					volume: ((this.currentFadeInAndOutVolumeMillis * this.currentMusicVolume / 100) >> 10) * userVolume / 100);
			else
				musicOutput.StopMusic();
		}
	}
}
