﻿
namespace ChessCompStompWithHacksLibrary
{
	using DTLibrary;
	using System.Collections.Generic;

	public class TestingKeyboardDesktopFrame : IFrame<GameImage, GameFont, GameSound, GameMusic>
	{
		private GlobalState globalState;
		private SessionState sessionState;

		private int x;
		private int y;

		public TestingKeyboardDesktopFrame(GlobalState globalState, SessionState sessionState)
		{
			this.globalState = globalState;
			this.sessionState = sessionState;

			this.x = 50;
			this.y = 50;
		}

		public void ProcessExtraTime(int milliseconds)
		{
		}

		public string GetClickUrl()
		{
			return null;
		}

		public HashSet<string> GetCompletedAchievements()
		{
			return new HashSet<string>();
		}

		public IFrame<GameImage, GameFont, GameSound, GameMusic> ProcessDisplayType(DisplayType displayType, IDisplayProcessing<GameImage> displayProcessing)
		{
			if (displayType != DisplayType.Desktop)
				return TitleScreenFrame.GetTitleScreenFrame(globalState: this.globalState, sessionState: this.sessionState, displayType: displayType, display: displayProcessing);

			return this;
		}

		public IFrame<GameImage, GameFont, GameSound, GameMusic> GetNextFrame(
			IKeyboard keyboardInput,
			IMouse mouseInput,
			IKeyboard previousKeyboardInput,
			IMouse previousMouseInput,
			IDisplayProcessing<GameImage> displayProcessing,
			ISoundOutput<GameSound> soundOutput,
			IMusicProcessing musicProcessing)
		{
			if (keyboardInput.IsPressed(Key.Esc) && !previousKeyboardInput.IsPressed(Key.Esc))
				return new TestingDesktopFrame(globalState: this.globalState, sessionState: this.sessionState);

			int delta = this.globalState.ElapsedMicrosPerFrame / 2000;

			if (keyboardInput.IsPressed(Key.LeftArrow))
				this.x -= delta;
			if (keyboardInput.IsPressed(Key.RightArrow))
				this.x += delta;
			if (keyboardInput.IsPressed(Key.DownArrow))
				this.y -= delta;
			if (keyboardInput.IsPressed(Key.UpArrow))
				this.y += delta;

			if (this.x < 0)
				this.x = 0;
			if (this.x > GlobalConstants.DESKTOP_WINDOW_WIDTH)
				this.x = GlobalConstants.DESKTOP_WINDOW_WIDTH;
			if (this.y < 0)
				this.y = 0;
			if (this.y > GlobalConstants.DESKTOP_WINDOW_HEIGHT)
				this.y = GlobalConstants.DESKTOP_WINDOW_HEIGHT;

			return this;
		}
		
		public void ProcessMusic()
		{
			this.globalState.ProcessMusic();
		}

		public void Render(IDisplayOutput<GameImage, GameFont> displayOutput)
		{
			displayOutput.DrawRectangle(
				x: 0,
				y: 0,
				width: GlobalConstants.DESKTOP_WINDOW_WIDTH,
				height: GlobalConstants.DESKTOP_WINDOW_HEIGHT,
				color: new DTColor(223, 220, 217),
				fill: true);

			displayOutput.DrawRectangle(
				x: this.x - 5,
				y: this.y - 5,
				width: 11,
				height: 11,
				color: DTColor.Black(),
				fill: true);
		}

		public void RenderMusic(IMusicOutput<GameMusic> musicOutput)
		{
			this.globalState.RenderMusic(musicOutput: musicOutput);
		}
	}
}
