
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class ChessTestingFrame : IFrame<ChessImage, ChessFont, ChessSound, ChessMusic>
	{
		private GlobalState globalState;
		private SessionState sessionState;

		public ChessTestingFrame(GlobalState globalState, SessionState sessionState)
		{
			this.globalState = globalState;
			this.sessionState = sessionState;
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
			return this;
		}
		
		public void ProcessMusic()
		{
			this.globalState.ProcessMusic();
		}
		
		public void Render(IDisplayOutput<ChessImage, ChessFont> displayOutput)
		{
		}

		public void RenderMusic(IMusicOutput<ChessMusic> musicOutput)
		{
			this.globalState.RenderMusic(musicOutput: musicOutput);
		}
	}
}
