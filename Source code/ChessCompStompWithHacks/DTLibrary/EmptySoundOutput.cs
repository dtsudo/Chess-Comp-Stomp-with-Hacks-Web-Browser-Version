
namespace DTLibrary
{
	using System;

	public class EmptySoundOutput<SoundEnum> : ISoundOutput<SoundEnum>
	{
		private int soundVolume;

		public EmptySoundOutput()
		{
			this.soundVolume = 100;
		}

		public bool LoadSounds()
		{
			return true;
		}

		public void SetSoundVolume(int volume)
		{
			this.soundVolume = volume;
		}

		public void SetSoundVolumeImmediately(int volume)
		{
			this.soundVolume = volume;
		}

		public int GetSoundVolume()
		{
			return this.soundVolume;
		}

		public void PlaySound(SoundEnum sound)
		{
		}

		public void ProcessFrame()
		{
		}

		public void DisposeSounds()
		{
		}
	}
}
