
namespace DTLibrary
{
	using System.Collections.Generic;

	public interface IFrame<ImageEnum, FontEnum, SoundEnum, MusicEnum>
	{
		IFrame<ImageEnum, FontEnum, SoundEnum, MusicEnum> GetNextFrame(IKeyboard keyboardInput, IMouse mouseInput, IKeyboard previousKeyboardInput, IMouse previousMouseInput, IDisplayProcessing<ImageEnum> displayProcessing, ISoundOutput<SoundEnum> soundOutput, IMusicProcessing musicProcessing);
		void ProcessMusic();
		void Render(IDisplayOutput<ImageEnum, FontEnum> display);
		void RenderMusic(IMusicOutput<MusicEnum> musicOutput);
		void ProcessExtraTime(int milliseconds);

		string GetClickUrl();
		HashSet<string> GetCompletedAchievements();
	}
}
