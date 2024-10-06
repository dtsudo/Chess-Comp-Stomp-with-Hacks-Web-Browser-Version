
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System;
	using System.Collections.Generic;

	public class AIMessageMobileFrame : IFrame<GameImage, GameFont, GameSound, GameMusic>
	{
		private GlobalState globalState;
		private SessionState sessionState;
		private IFrame<GameImage, GameFont, GameSound, GameMusic> underlyingFrame;
		private string message;
		private int messageXOffset;
		private int messageYOffset;

		private Button confirmButton;

		private int widthOfUnderlyingFrame;
		private int heightOfUnderlyingFrame;
		private bool renderUnderlyingFrame;

		private AIMessageType aiMessageType;

		private int panelX;
		private int panelY;
		private int panelWidth;
		private int panelHeight;

		public static IFrame<GameImage, GameFont, GameSound, GameMusic> GetAIHackMessageFrame(
			GlobalState globalState,
			SessionState sessionState,
			IDisplayProcessing<GameImage> display)
		{
			return new AIMessageMobileFrame(
				globalState: globalState,
				sessionState: sessionState,
				underlyingFrame: new ChessMobileFrame(globalState: globalState, sessionState: sessionState, display: display),
				message: "If you're going to hack, then I'm hacking too!",
				messageXOffset: 27,
				messageYOffset: 90,
				aiMessageType: AIMessageType.AIHackMessage,
				display: display);
		}

		public static IFrame<GameImage, GameFont, GameSound, GameMusic> GetFinalBattleMessageFrame(
			GlobalState globalState,
			SessionState sessionState,
			IDisplayProcessing<GameImage> display)
		{
			return new AIMessageMobileFrame(
				globalState: globalState,
				sessionState: sessionState,
				underlyingFrame: new ChessMobileFrame(globalState: globalState, sessionState: sessionState, display: display),
				message: "I have 23 queens. Good luck; have fun!  :)",
				messageXOffset: 27,
				messageYOffset: 90,
				aiMessageType: AIMessageType.FinalBattleMessage,
				display: display);
		}

		private AIMessageMobileFrame(
			GlobalState globalState, 
			SessionState sessionState, 
			IFrame<GameImage, GameFont, GameSound, GameMusic> underlyingFrame, 
			string message,
			int messageXOffset,
			int messageYOffset,
			AIMessageType aiMessageType,
			IDisplayProcessing<GameImage> display)
		{
			this.widthOfUnderlyingFrame = display.GetMobileScreenWidth();
			this.heightOfUnderlyingFrame = display.GetMobileScreenHeight();
			this.renderUnderlyingFrame = true;

			this.aiMessageType = aiMessageType;

			this.globalState = globalState;
			this.sessionState = sessionState;
			this.underlyingFrame = underlyingFrame;
			this.message = message;
			this.messageXOffset = messageXOffset;
			this.messageYOffset = messageYOffset;
			
			this.confirmButton = new Button(
				x: 0,
				y: 0,
				width: 225,
				height: 100,
				backgroundColor: new DTColor(200, 200, 200),
				hoverColor: ColorThemeUtil.GetHoverColor(colorTheme: sessionState.GetColorTheme()),
				clickColor: ColorThemeUtil.GetClickColor(colorTheme: sessionState.GetColorTheme()),
				text: "OK",
				textXOffset: 85,
				textYOffset: 29,
				font: GameFont.GameFont32Pt,
				isMobileDisplayType: true);

			this.UpdateCoordinates(display: display);
		}

		private void UpdateCoordinates(IDisplayProcessing<GameImage> display)
		{
			bool isLandscape = display.IsMobileInLandscapeOrientation();

			this.panelWidth = isLandscape ? 800 : 650;
			this.panelHeight = 350;

			this.panelX = (display.GetMobileScreenWidth() - this.panelWidth) / 2;
			this.panelY = (display.GetMobileScreenHeight() - this.panelHeight) / 2;

			this.confirmButton.SetX(this.panelX + (this.panelWidth - 225) / 2);
			this.confirmButton.SetY(this.panelY + 50);
		}

		public string GetClickUrl()
		{
			return null;
		}

		public HashSet<string> GetCompletedAchievements()
		{
			return new HashSet<string>();
		}

		public void ProcessExtraTime(int milliseconds)
		{
		}

		public IFrame<GameImage, GameFont, GameSound, GameMusic> ProcessDisplayType(DisplayType displayType, IDisplayProcessing<GameImage> displayProcessing)
		{
			if (displayType == DisplayType.Desktop)
			{
				switch (this.aiMessageType)
				{
					case AIMessageType.AIHackMessage:
						return AIMessageDesktopFrame.GetAIHackMessageFrame(globalState: this.globalState, sessionState: this.sessionState);
					case AIMessageType.FinalBattleMessage:
						return AIMessageDesktopFrame.GetFinalBattleMessageFrame(globalState: this.globalState, sessionState: this.sessionState);
					default:
						throw new Exception();
				}
			}

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
			this.UpdateCoordinates(display: displayProcessing);

			if (displayProcessing.GetMobileScreenWidth() != this.widthOfUnderlyingFrame)
				this.renderUnderlyingFrame = false;
			if (displayProcessing.GetMobileScreenHeight() != this.heightOfUnderlyingFrame)
				this.renderUnderlyingFrame = false;

			GameMusic music = GameMusicUtil.GetGameMusic(colorTheme: this.sessionState.GetColorTheme());
			this.globalState.MusicPlayer.SetMusic(music: music, volume: 100);

			bool isConfirmClicked = this.confirmButton.ProcessFrame(
				mouseInput: mouseInput,
				previousMouseInput: previousMouseInput);

			if (isConfirmClicked)
			{
				soundOutput.PlaySound(sound: GameSound.Click);
				return this.underlyingFrame;
			}
			
			return this;
		}
		
		public void ProcessMusic()
		{
			this.underlyingFrame.ProcessMusic();
		}

		public void Render(IDisplayOutput<GameImage, GameFont> displayOutput)
		{
			displayOutput.DrawRectangle(
				x: 0,
				y: 0,
				width: displayOutput.GetMobileScreenWidth(),
				height: displayOutput.GetMobileScreenHeight(),
				color: new DTColor(223, 220, 217),
				fill: true);

			if (this.renderUnderlyingFrame)
				this.underlyingFrame.Render(display: displayOutput);

			displayOutput.DrawRectangle(
				x: 0,
				y: 0,
				width: displayOutput.GetMobileScreenWidth(),
				height: displayOutput.GetMobileScreenHeight(),
				color: new DTColor(r: 0, g: 0, b: 0, alpha: 64),
				fill: true);

			displayOutput.DrawRectangle(
				x: this.panelX,
				y: this.panelY,
				width: this.panelWidth,
				height: this.panelHeight,
				color: DTColor.White(),
				fill: true);

			displayOutput.DrawRectangle(
				x: this.panelX,
				y: this.panelY,
				width: this.panelWidth,
				height: this.panelHeight,
				color: DTColor.Black(),
				fill: false);

			displayOutput.DrawText(
				x: this.panelX + this.panelWidth / 2 - 219,
				y: this.panelY + this.panelHeight - 27,
				text: "Message from the AI",
				font: GameFont.GameFont32Pt,
				color: DTColor.Black());

			displayOutput.DrawText(
				x: this.panelX + this.messageXOffset,
				y: this.panelY + this.panelHeight - this.messageYOffset,
				text: this.message,
				font: GameFont.GameFont20Pt,
				color: DTColor.Black());

			this.confirmButton.Render(displayOutput: displayOutput);
		}

		public void RenderMusic(IMusicOutput<GameMusic> musicOutput)
		{
			this.underlyingFrame.RenderMusic(musicOutput: musicOutput);
		}
	}
}
