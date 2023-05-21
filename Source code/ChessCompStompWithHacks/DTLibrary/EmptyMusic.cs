
namespace DTLibrary
{
	public class EmptyMusic<MusicEnum> : IMusic<MusicEnum>
	{
		public void DisposeMusic()
		{
		}

		public bool LoadMusic()
		{
			return true;
		}

		public void PlayMusic(MusicEnum music, int volume)
		{
		}

		public void StopMusic()
		{
		}
	}
}
