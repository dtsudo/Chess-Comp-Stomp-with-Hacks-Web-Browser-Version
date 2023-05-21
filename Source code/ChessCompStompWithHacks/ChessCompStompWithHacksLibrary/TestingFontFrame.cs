
namespace ChessCompStompWithHacksLibrary
{
	using DTLibrary;
	using System.Collections.Generic;

	public class TestingFontFrame : IFrame<GameImage, GameFont, GameSound, GameMusic>
	{
		private GlobalState globalState;
		private SessionState sessionState;

		public TestingFontFrame(GlobalState globalState, SessionState sessionState)
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
				return new TestingFrame(globalState: this.globalState, sessionState: this.sessionState);

			if (keyboardInput.IsPressed(Key.Enter) && !previousKeyboardInput.IsPressed(Key.Enter))
				return new TestingFontFrame2(globalState: this.globalState, sessionState: this.sessionState);

			return this;
		}
		
		public void ProcessMusic()
		{
			this.globalState.ProcessMusic();
		}

		public void Render(IDisplayOutput<GameImage, GameFont> displayOutput)
		{
			DTColor red = new DTColor(255, 0, 0);

			displayOutput.DrawRectangle(
				x: 51,
				y: 590,
				width: 836,
				height: 60,
				color: red,
				fill: false);

			displayOutput.DrawText(
				x: 50,
				y: 650,
				text: "Line 1 ABCDEFGHIJKLMNOPQRSTUVWXYZ ABCDEFGHIJKLMNOPQRSTUVWXYZ ABCDEFGHIJKLMNOPQRSTUVWXYZ"
					+ "\n" + "Line 2"
					+ "\n" + "Line 3"
					+ "\n" + "Line 4",
				font: GameFont.GameFont12Pt,
				color: DTColor.Black());

			displayOutput.DrawRectangle(
				x: 51,
				y: 479,
				width: 632,
				height: 71,
				color: red,
				fill: false);

			displayOutput.DrawText(
				x: 50,
				y: 550,
				text: "Line 1 abcdefghijklmnopqrstuvwxyz abcdefghijklmnopqrstuvwxyz"
					+ "\n" + "Line 2"
					+ "\n" + "Line 3"
					+ "\n" + "Line 4",
				font: GameFont.GameFont14Pt,
				color: DTColor.Black());

			displayOutput.DrawRectangle(
				x: 51,
				y: 364,
				width: 714,
				height: 85,
				color: red,
				fill: false);

			displayOutput.DrawText(
				x: 50,
				y: 450,
				text: "Line 1 Chess Comp Stomp with Hacks Chess Comp Stomp with Hacks"
					+ "\n" + "Line 2"
					+ "\n" + "Line 3"
					+ "\n" + "Line 4",
				font: GameFont.GameFont16Pt,
				color: DTColor.Black());

			displayOutput.DrawRectangle(
				x: 51,
				y: 259,
				width: 790,
				height: 90,
				color: red,
				fill: false);

			displayOutput.DrawText(
				x: 50,
				y: 350,
				text: "Line 1 Chess Comp Stomp with Hacks Chess Comp Stomp with Hacks"
					+ "\n" + "Line 2"
					+ "\n" + "Line 3"
					+ "\n" + "Line 4",
				font: GameFont.GameFont18Pt,
				color: DTColor.Black());

			displayOutput.DrawRectangle(
				x: 51,
				y: 144,
				width: 874,
				height: 104,
				color: red,
				fill: false);

			displayOutput.DrawText(
				x: 50,
				y: 250,
				text: "Line 1 Chess Comp Stomp with Hacks Chess Comp Stomp with Hacks"
					+ "\n" + "Line 2"
					+ "\n" + "Line 3"
					+ "\n" + "Line 4",
				font: GameFont.GameFont20Pt,
				color: DTColor.Black());
		}

		public void RenderMusic(IMusicOutput<GameMusic> musicOutput)
		{
			this.globalState.RenderMusic(musicOutput: musicOutput);
		}
	}
}
