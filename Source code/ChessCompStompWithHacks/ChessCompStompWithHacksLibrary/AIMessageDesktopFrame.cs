﻿
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System;
	using System.Collections.Generic;

	public class AIMessageDesktopFrame : IFrame<GameImage, GameFont, GameSound, GameMusic>
	{
		private GlobalState globalState;
		private SessionState sessionState;
		private IFrame<GameImage, GameFont, GameSound, GameMusic> underlyingFrame;
		private string message;
		private int messageXOffset;
		private int messageYOffset;

		private Button confirmButton;

		private AIMessageType aiMessageType;

		private const int PANEL_WIDTH = 480;
		private const int PANEL_HEIGHT = 200;

		private const int PANEL_X = (GlobalConstants.DESKTOP_WINDOW_WIDTH - PANEL_WIDTH) / 2;
		private const int PANEL_Y = (GlobalConstants.DESKTOP_WINDOW_HEIGHT - PANEL_HEIGHT) / 2;

		public static IFrame<GameImage, GameFont, GameSound, GameMusic> GetAIHackMessageFrame(
			GlobalState globalState,
			SessionState sessionState)
		{
			return new AIMessageDesktopFrame(
				globalState: globalState,
				sessionState: sessionState,
				underlyingFrame: new ChessDesktopFrame(globalState: globalState, sessionState: sessionState),
				message: "If you're going to hack, then I'm hacking too!",
				messageXOffset: 29,
				messageYOffset: 114,
				aiMessageType: AIMessageType.AIHackMessage);
		}

		public static IFrame<GameImage, GameFont, GameSound, GameMusic> GetFinalBattleMessageFrame(
			GlobalState globalState,
			SessionState sessionState)
		{
			return new AIMessageDesktopFrame(
				globalState: globalState,
				sessionState: sessionState,
				underlyingFrame: new ChessDesktopFrame(globalState: globalState, sessionState: sessionState),
				message: "I have 23 queens. Good luck; have fun!  :)",
				messageXOffset: 50,
				messageYOffset: 114,
				aiMessageType: AIMessageType.FinalBattleMessage);
		}

		private AIMessageDesktopFrame(
			GlobalState globalState, 
			SessionState sessionState, 
			IFrame<GameImage, GameFont, GameSound, GameMusic> underlyingFrame, 
			string message,
			int messageXOffset,
			int messageYOffset,
			AIMessageType aiMessageType)
		{
			this.globalState = globalState;
			this.sessionState = sessionState;
			this.underlyingFrame = underlyingFrame;
			this.message = message;
			this.messageXOffset = messageXOffset;
			this.messageYOffset = messageYOffset;

			int buttonWidth = 150;

			this.aiMessageType = aiMessageType;

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
				font: GameFont.GameFont20Pt,
				isMobileDisplayType: false);
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
			if (displayType != DisplayType.Desktop)
			{
				switch (this.aiMessageType)
				{
					case AIMessageType.AIHackMessage:
						return AIMessageMobileFrame.GetAIHackMessageFrame(globalState: this.globalState, sessionState: this.sessionState, display: displayProcessing);
					case AIMessageType.FinalBattleMessage:
						return AIMessageMobileFrame.GetFinalBattleMessageFrame(globalState: this.globalState, sessionState: this.sessionState, display: displayProcessing);
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
			this.underlyingFrame.Render(display: displayOutput);

			displayOutput.DrawRectangle(
				x: 0,
				y: 0,
				width: GlobalConstants.DESKTOP_WINDOW_WIDTH,
				height: GlobalConstants.DESKTOP_WINDOW_HEIGHT,
				color: new DTColor(r: 0, g: 0, b: 0, alpha: 64),
				fill: true);

			displayOutput.DrawRectangle(
				x: PANEL_X,
				y: PANEL_Y,
				width: PANEL_WIDTH,
				height: PANEL_HEIGHT,
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
				font: GameFont.GameFont20Pt,
				color: DTColor.Black());

			displayOutput.DrawText(
				x: PANEL_X + this.messageXOffset,
				y: PANEL_Y + this.messageYOffset,
				text: this.message,
				font: GameFont.GameFont14Pt,
				color: DTColor.Black());

			this.confirmButton.Render(displayOutput: displayOutput);
		}

		public void RenderMusic(IMusicOutput<GameMusic> musicOutput)
		{
			this.underlyingFrame.RenderMusic(musicOutput: musicOutput);
		}
	}
}
