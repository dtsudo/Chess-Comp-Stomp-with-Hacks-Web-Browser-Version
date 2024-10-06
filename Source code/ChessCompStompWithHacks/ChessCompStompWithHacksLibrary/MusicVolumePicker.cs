
namespace ChessCompStompWithHacksLibrary
{
	using DTLibrary;

	public class MusicVolumePicker
	{
		private int _xPos;
		private int _yPos;

		private int _currentVolume;
		private int _unmuteVolume;

		private bool _isDraggingVolumeSlider;
		private int _scalingFactor;

		public MusicVolumePicker(int xPos, int yPos, int initialVolume, int scalingFactor)
		{
			this._xPos = xPos;
			this._yPos = yPos;

			this._currentVolume = initialVolume;
			this._unmuteVolume = this._currentVolume;

			this._isDraggingVolumeSlider = false;

			this._scalingFactor = scalingFactor;
		}

		public void ProcessFrame(
			IMouse mouseInput,
			IMouse previousMouseInput)
		{
			int mouseX = mouseInput.GetX();
			int mouseY = mouseInput.GetY();

			if (mouseInput.IsLeftMouseButtonPressed()
				&& !previousMouseInput.IsLeftMouseButtonPressed()
				&& this._xPos <= mouseX
				&& mouseX <= this._xPos + 40 * this._scalingFactor
				&& this._yPos <= mouseY
				&& mouseY <= this._yPos + 50 * this._scalingFactor)
			{
				if (this._currentVolume == 0)
				{
					this._currentVolume = this._unmuteVolume == 0 ? GlobalState.DEFAULT_VOLUME : this._unmuteVolume;
					this._unmuteVolume = this._currentVolume;
				}
				else
				{
					this._unmuteVolume = this._currentVolume;
					this._currentVolume = 0;
				}
			}

			if (mouseInput.IsLeftMouseButtonPressed()
				&& !previousMouseInput.IsLeftMouseButtonPressed()
				&& this._xPos + 50 * this._scalingFactor <= mouseX
				&& mouseX <= this._xPos + 150 * this._scalingFactor
				&& this._yPos + 10 * this._scalingFactor <= mouseY
				&& mouseY <= this._yPos + 40 * this._scalingFactor)
			{
				this._isDraggingVolumeSlider = true;
			}

			if (this._isDraggingVolumeSlider && mouseInput.IsLeftMouseButtonPressed())
			{
				int volume = (mouseX - (this._xPos + 50 * this._scalingFactor)) / this._scalingFactor;
				if (volume < 0)
					volume = 0;
				if (volume > 100)
					volume = 100;

				this._currentVolume = volume;
				this._unmuteVolume = this._currentVolume;
			}

			if (!mouseInput.IsLeftMouseButtonPressed())
				this._isDraggingVolumeSlider = false;
		}

		public void SetX(int x)
		{
			this._xPos = x;
		}

		public void SetY(int y)
		{
			this._yPos = y;
		}

		/// <summary>
		/// Returns a number from 0 to 100 (both inclusive)
		/// </summary>
		public int GetCurrentMusicVolume()
		{
			return this._currentVolume;
		}

		public void Render(IDisplayOutput<GameImage, GameFont> displayOutput)
		{
			if (this._currentVolume > 0)
				displayOutput.DrawImageRotatedClockwise(GameImage.MusicOn, x: this._xPos, y: this._yPos, degreesScaled: 0, scalingFactorScaled: 128 * this._scalingFactor / 2);
			else
				displayOutput.DrawImageRotatedClockwise(GameImage.MusicOff, x: this._xPos, y: this._yPos, degreesScaled: 0, scalingFactorScaled: 128 * this._scalingFactor / 2);

			displayOutput.DrawRectangle(
				x: this._xPos + 50 * this._scalingFactor,
				y: this._yPos + 10 * this._scalingFactor,
				width: 100 * this._scalingFactor,
				height: 30 * this._scalingFactor + 1,
				color: DTColor.Black(),
				fill: false);

			if (this._currentVolume > 0)
				displayOutput.DrawRectangle(
					x: this._xPos + 50 * this._scalingFactor,
					y: this._yPos + 10 * this._scalingFactor,
					width: this._currentVolume * this._scalingFactor,
					height: 30 * this._scalingFactor + 1,
					color: DTColor.Black(),
					fill: true);
		}
	}
}
