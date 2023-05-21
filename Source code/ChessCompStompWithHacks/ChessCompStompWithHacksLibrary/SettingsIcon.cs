
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System.Collections.Generic;

	public class SettingsIcon
	{
		private bool isHover;
		private bool isClicked;

		public SettingsIcon()
		{
			this.isHover = false;
			this.isClicked = false;
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

			int settingsIconWidth = displayProcessing.GetWidth(GameImage.Gear);
			int settingsIconHeight = displayProcessing.GetHeight(GameImage.Gear);

			bool isHover = GlobalConstants.WINDOW_WIDTH - settingsIconWidth <= mouseX
				&& mouseX <= GlobalConstants.WINDOW_WIDTH
				&& GlobalConstants.WINDOW_HEIGHT - settingsIconHeight <= mouseY
				&& mouseY <= GlobalConstants.WINDOW_HEIGHT;

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
			displayOutput.DrawImage(
				image: this.isClicked ? GameImage.GearSelected : (this.isHover ? GameImage.GearHover : GameImage.Gear),
				x: GlobalConstants.WINDOW_WIDTH - displayOutput.GetWidth(GameImage.Gear),
				y: GlobalConstants.WINDOW_HEIGHT - displayOutput.GetHeight(GameImage.Gear));
		}
	}
}
