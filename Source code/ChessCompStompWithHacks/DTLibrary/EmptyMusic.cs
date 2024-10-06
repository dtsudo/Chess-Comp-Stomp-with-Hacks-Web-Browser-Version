
namespace DTLibrary
{
	using System;

	public class EmptyMusic<MusicEnum> : IMusic<MusicEnum>
	{
		public void DisposeMusic()
		{
		}

		public bool LoadMusic()
		{
			return true;
		}

		public int GetNumElementsLoaded()
		{
			return 0;
		}

		public int? GetNumTotalElementsToLoad()
		{
			return 0;
		}

		public void PlayMusic(MusicEnum music, int volume)
		{
		}

		public void StopMusic()
		{
		}
	}
}
