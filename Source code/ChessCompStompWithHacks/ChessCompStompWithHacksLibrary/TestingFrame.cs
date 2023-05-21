
namespace ChessCompStompWithHacksLibrary
{
	using DTLibrary;
	using System;
	using System.Collections.Generic;

	public class TestingFrame : IFrame<GameImage, GameFont, GameSound, GameMusic>
	{
		private GlobalState globalState;
		private SessionState sessionState;

		public TestingFrame(GlobalState globalState, SessionState sessionState)
		{
			this.globalState = globalState;
			this.sessionState = sessionState;
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
				return new TitleScreenFrame(globalState: this.globalState, sessionState: this.sessionState);

			if (keyboardInput.IsPressed(Key.One) && !previousKeyboardInput.IsPressed(Key.One))
				return new TestingKeyboardFrame(globalState: this.globalState, sessionState: this.sessionState);

			if (keyboardInput.IsPressed(Key.Two) && !previousKeyboardInput.IsPressed(Key.Two))
				return new TestingMouseFrame(globalState: this.globalState, sessionState: this.sessionState);

			if (keyboardInput.IsPressed(Key.Three) && !previousKeyboardInput.IsPressed(Key.Three))
				return new TestingFontFrame(globalState: this.globalState, sessionState: this.sessionState);

			if (keyboardInput.IsPressed(Key.Four) && !previousKeyboardInput.IsPressed(Key.Four))
				return new TestingSoundFrame(globalState: this.globalState, sessionState: this.sessionState);

			if (keyboardInput.IsPressed(Key.Five) && !previousKeyboardInput.IsPressed(Key.Five))
				return new TestingMusicFrame(globalState: this.globalState, sessionState: this.sessionState);

			return this;
		}
		
		public void ProcessMusic()
		{
			this.globalState.ProcessMusic();
		}

		public void Render(IDisplayOutput<GameImage, GameFont> displayOutput)
		{
			displayOutput.DrawText(
				x: 50,
				y: GlobalConstants.WINDOW_HEIGHT - 50,
				text: "1) Test keyboard"
					+ "\n" + "2) Test mouse"
					+ "\n" + "3) Test font"
					+ "\n" + "4) Test sound"
					+ "\n" + "5) Test music",
				font: GameFont.GameFont16Pt,
				color: DTColor.Black());
		}

		public void RenderMusic(IMusicOutput<GameMusic> musicOutput)
		{
			this.globalState.RenderMusic(musicOutput: musicOutput);
		}
	}
}
