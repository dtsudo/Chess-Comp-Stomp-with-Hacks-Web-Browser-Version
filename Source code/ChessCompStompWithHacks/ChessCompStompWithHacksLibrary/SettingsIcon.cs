
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System.Collections.Generic;

	public class SettingsIcon
	{
		private bool isHover;
		private bool isClicked;

		private bool isMobileDisplayType;

		public SettingsIcon(bool isMobileDisplayType)
		{
			this.isHover = false;
			this.isClicked = false;

			this.isMobileDisplayType = isMobileDisplayType;
		}

		public class SettingsIconStatus
		{
			public SettingsIconStatus(bool hasClicked, bool isHover)
			{
				this.HasClicked = hasClicked;
				this.IsHover = isHover;
			}

			public bool HasClicked { get; private set; }
			public bool IsHover { get; private set; }
		}

		public SettingsIconStatus ProcessFrame(
			IMouse mouseInput,
			IMouse previousMouseInput,
			bool ignoreMouse,
			IDisplayProcessing<GameImage> displayProcessing)
		{
			int mouseX = mouseInput.GetX();
			int mouseY = mouseInput.GetY();

			int scalingFactor = this.isMobileDisplayType ? 2 : 1;

			int settingsIconWidth = displayProcessing.GetWidth(GameImage.Gear) / 2 * scalingFactor;
			int settingsIconHeight = displayProcessing.GetHeight(GameImage.Gear) / 2 * scalingFactor;

			int windowWidth = this.isMobileDisplayType ? displayProcessing.GetMobileScreenWidth() : GlobalConstants.DESKTOP_WINDOW_WIDTH;
			int windowHeight = this.isMobileDisplayType ? displayProcessing.GetMobileScreenHeight() : GlobalConstants.DESKTOP_WINDOW_HEIGHT;

			bool isHover = windowWidth - settingsIconWidth <= mouseX
				&& mouseX <= windowWidth
				&& windowHeight - settingsIconHeight <= mouseY
				&& mouseY <= windowHeight;

			this.isHover = isHover && !ignoreMouse;

			if (mouseInput.IsLeftMouseButtonPressed() && !previousMouseInput.IsLeftMouseButtonPressed() && !ignoreMouse)
			{
				if (isHover)
					this.isClicked = true;
			}
			
			if (this.isClicked && !mouseInput.IsLeftMouseButtonPressed() && previousMouseInput.IsLeftMouseButtonPressed())
			{
				this.isClicked = false;

				if (isHover && !ignoreMouse)
					return new SettingsIconStatus(hasClicked: true, isHover: this.isHover);
			}

			return new SettingsIconStatus(hasClicked: false, isHover: this.isHover);
		}

		public void Render(IDisplayOutput<GameImage, GameFont> displayOutput)
		{
			int scalingFactor = this.isMobileDisplayType ? 2 : 1;

			int windowWidth = this.isMobileDisplayType ? displayOutput.GetMobileScreenWidth() : GlobalConstants.DESKTOP_WINDOW_WIDTH;
			int windowHeight = this.isMobileDisplayType ? displayOutput.GetMobileScreenHeight() : GlobalConstants.DESKTOP_WINDOW_HEIGHT;

			displayOutput.DrawImageRotatedClockwise(
				image: this.isClicked ? GameImage.GearSelected : (this.isHover && !this.isMobileDisplayType ? GameImage.GearHover : GameImage.Gear),
				x: windowWidth - displayOutput.GetWidth(GameImage.Gear) * scalingFactor / 2,
				y: windowHeight - displayOutput.GetHeight(GameImage.Gear) * scalingFactor / 2,
				degreesScaled: 0,
				scalingFactorScaled: scalingFactor * 128 / 2);
		}
	}
}
