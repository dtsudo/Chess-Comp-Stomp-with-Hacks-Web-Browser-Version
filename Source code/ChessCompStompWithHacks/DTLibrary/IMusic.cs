
namespace DTLibrary
{
	public interface IMusicOutput<MusicEnum>
	{
		/// <summary>
		/// Volume ranges from 0 to 100 (both inclusive)
		/// </summary>
		void PlayMusic(MusicEnum music, int volume);

		void StopMusic();
	}

	public interface IMusicProcessing
	{
		/// <summary>
		/// Must be repeatedly invoked until it returns true before invoking PlayMusic() or StopMusic()
		/// </summary>
		bool LoadMusic();
	}

	public interface IMusicCleanup
	{
		/// <summary>
		/// Note that DisposeMusic() must be called, and it gets called even
		/// if LoadMusic() is never invoked (or was invoked but never returned true)
		/// 
		/// This function must be idempotent (and not fail if called multiple times).
		/// </summary>
		void DisposeMusic();
	}

	public interface IMusic<MusicEnum> : IMusicOutput<MusicEnum>, IMusicProcessing, IMusicCleanup
	{
	}
}
