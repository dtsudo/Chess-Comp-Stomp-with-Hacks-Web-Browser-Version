
namespace ChessCompStompWithHacksLibrary
{
	using DTLibrary;
	using System;

	public class SoundAndMusicVolumePicker
	{
		private SoundVolumePicker soundVolumePicker;
		private MusicVolumePicker musicVolumePicker;
		private int scalingFactor;
		
		public SoundAndMusicVolumePicker(int xPos, int yPos, int initialSoundVolume, int initialMusicVolume, int elapsedMicrosPerFrame, int scalingFactor)
		{
			this.soundVolumePicker = new SoundVolumePicker(xPos: xPos, yPos: yPos + 50 * scalingFactor, initialVolume: initialSoundVolume, scalingFactor: scalingFactor);
			this.musicVolumePicker = new MusicVolumePicker(xPos: xPos, yPos: yPos, initialVolume: initialMusicVolume, scalingFactor: scalingFactor);
			this.scalingFactor = scalingFactor;
		}

		public void ProcessFrame(
			IMouse mouseInput,
			IMouse previousMouseInput)
		{
			this.soundVolumePicker.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput);
			this.musicVolumePicker.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput);
		}
		
		public void SetX(int x)
		{
			this.soundVolumePicker.SetX(x: x);
			this.musicVolumePicker.SetX(x: x);
		}

		public void SetY(int y)
		{
			this.soundVolumePicker.SetY(y: y + 50 * scalingFactor);
			this.musicVolumePicker.SetY(y: y);
		}

		/// <summary>
		/// Returns a number from 0 to 100 (both inclusive)
		/// </summary>
		public int GetCurrentSoundVolume()
		{
			return this.soundVolumePicker.GetCurrentSoundVolume();
		}

		/// <summary>
		/// Returns a number from 0 to 100 (both inclusive)
		/// </summary>
		public int GetCurrentMusicVolume()
		{
			return this.musicVolumePicker.GetCurrentMusicVolume();
		}

		public void Render(IDisplayOutput<GameImage, GameFont> displayOutput)
		{
			this.soundVolumePicker.Render(displayOutput: displayOutput);
			this.musicVolumePicker.Render(displayOutput: displayOutput);
		}
	}
}
