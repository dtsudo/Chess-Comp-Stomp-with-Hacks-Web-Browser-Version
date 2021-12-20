
namespace DTLibrary
{
	using System;

	public interface ISoundOutput<SoundEnum>
	{
		/// <summary>
		/// Must be repeatedly invoked until it returns true before invoking PlaySound()
		/// </summary>
		bool LoadSounds();

		/// <summary>
		/// Volume ranges from 0 to 100 (both inclusive)
		/// </summary>
		void SetSoundVolume(int volume);

		int GetSoundVolume();

		/// <summary>
		/// Plays the specified sound.
		/// </summary>
		void PlaySound(SoundEnum sound);

		void ProcessFrame();

		/// <summary>
		/// Note that DisposeSounds() must be called, and it gets called even
		/// if LoadSounds() is never invoked (or was invoked but never returned true)
		/// 
		/// This function must be idempotent (and not fail if called multiple times).
		/// </summary>
		void DisposeSounds();
	}
}
