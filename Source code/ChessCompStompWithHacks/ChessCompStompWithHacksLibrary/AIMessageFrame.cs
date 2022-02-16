
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System;
	
	public class AIMessageFrame : IFrame<ChessImage, ChessFont, ChessSound, ChessMusic>
	{
		private GlobalState globalState;
		private SessionState sessionState;
		private IFrame<ChessImage, ChessFont, ChessSound, ChessMusic> underlyingFrame;
		private string message;
		private int messageXOffset;
		private int messageYOffset;

		private Button confirmButton;

		private const int PANEL_WIDTH = 480;
		private const int PANEL_HEIGHT = 200;

		private const int PANEL_X = (ChessCompStompWithHacks.WINDOW_WIDTH - PANEL_WIDTH) / 2;
		private const int PANEL_Y = (ChessCompStompWithHacks.WINDOW_HEIGHT - PANEL_HEIGHT) / 2;

		public static IFrame<ChessImage, ChessFont, ChessSound, ChessMusic> GetAIHackMessageFrame(
			GlobalState globalState,
			SessionState sessionState)
		{
			return new AIMessageFrame(
				globalState: globalState,
				sessionState: sessionState,
				underlyingFrame: new ChessFrame(globalState: globalState, sessionState: sessionState),
				message: "If you're going to hack, then I'm hacking too!",
				messageXOffset: 29,
				messageYOffset: 114);
		}

		public static IFrame<ChessImage, ChessFont, ChessSound, ChessMusic> GetFinalBattleMessageFrame(
			GlobalState globalState,
			SessionState sessionState)
		{
			return new AIMessageFrame(
				globalState: globalState,
				sessionState: sessionState,
				underlyingFrame: new ChessFrame(globalState: globalState, sessionState: sessionState),
				message: "I have 23 queens. Good luck; have fun!  :)",
				messageXOffset: 50,
				messageYOffset: 114);
		}

		private AIMessageFrame(
			GlobalState globalState, 
			SessionState sessionState, 
			IFrame<ChessImage, ChessFont, ChessSound, ChessMusic> underlyingFrame, 
			string message,
			int messageXOffset,
			int messageYOffset)
		{
			this.globalState = globalState;
			this.sessionState = sessionState;
			this.underlyingFrame = underlyingFrame;
			this.message = message;
			this.messageXOffset = messageXOffset;
			this.messageYOffset = messageYOffset;

			int buttonWidth = 150;

			this.confirmButton = new Button(
				x: PANEL_X + (PANEL_WIDTH - buttonWidth) / 2,
				y: PANEL_Y + 20,
				width: buttonWidth,
				height: 40,
				backgroundColor: new DTColor(200, 200, 200),
				hoverColor: ColorThemeUtil.GetHoverColor(colorTheme: sessionState.GetColorTheme()),
				clickColor: ColorThemeUtil.GetClickColor(colorTheme: sessionState.GetColorTheme()),
				text: "OK",
				textXOffset: 57,
				textYOffset: 8,
				font: ChessFont.ChessFont20Pt);
		}

		public void ProcessExtraTime(int milliseconds)
		{
		}

		public IFrame<ChessImage, ChessFont, ChessSound, ChessMusic> GetNextFrame(
			IKeyboard keyboardInput,
			IMouse mouseInput,
			IKeyboard previousKeyboardInput,
			IMouse previousMouseInput,
			IDisplayProcessing<ChessImage> displayProcessing,
			ISoundOutput<ChessSound> soundOutput,
			IMusicProcessing musicProcessing)
		{
			ChessMusic music = ChessMusicUtil.GetChessMusic(colorTheme: this.sessionState.GetColorTheme());
			this.globalState.MusicPlayer.SetMusic(music: music, volume: 100);

			bool isConfirmClicked = this.confirmButton.ProcessFrame(
				mouseInput: mouseInput,
				previousMouseInput: previousMouseInput);

			if (isConfirmClicked)
			{
				soundOutput.PlaySound(sound: ChessSound.Click);
				return this.underlyingFrame;
			}
			
			return this;
		}
		
		public void ProcessMusic()
		{
			this.underlyingFrame.ProcessMusic();
		}

		public void Render(IDisplayOutput<ChessImage, ChessFont> displayOutput)
		{
			this.underlyingFrame.Render(display: displayOutput);

			displayOutput.DrawRectangle(
				x: 0,
				y: 0,
				width: ChessCompStompWithHacks.WINDOW_WIDTH,
				height: ChessCompStompWithHacks.WINDOW_HEIGHT,
				color: new DTColor(r: 0, g: 0, b: 0, alpha: 64),
				fill: true);

			displayOutput.DrawRectangle(
				x: PANEL_X,
				y: PANEL_Y,
				width: PANEL_WIDTH - 1,
				height: PANEL_HEIGHT - 1,
				color: DTColor.White(),
				fill: true);

			displayOutput.DrawRectangle(
				x: PANEL_X,
				y: PANEL_Y,
				width: PANEL_WIDTH,
				height: PANEL_HEIGHT,
				color: DTColor.Black(),
				fill: false);

			displayOutput.DrawText(
				x: PANEL_X + 104,
				y: PANEL_Y + 172,
				text: "Message from the AI",
				font: ChessFont.ChessFont20Pt,
				color: DTColor.Black());

			displayOutput.DrawText(
				x: PANEL_X + this.messageXOffset,
				y: PANEL_Y + this.messageYOffset,
				text: this.message,
				font: ChessFont.ChessFont14Pt,
				color: DTColor.Black());

			this.confirmButton.Render(displayOutput: displayOutput);
		}

		public void RenderMusic(IMusicOutput<ChessMusic> musicOutput)
		{
			this.underlyingFrame.RenderMusic(musicOutput: musicOutput);
		}
	}
}
