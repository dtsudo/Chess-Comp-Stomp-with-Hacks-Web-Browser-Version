
namespace ChessCompStompWithHacksEngine
{
	using DTLibrary;
	using System;
	using System.Collections.Generic;
	
	public class AIPondering
	{
		private class PonderingLogger : IDTLogger
		{
			private IDTLogger underlyingLogger;
			private bool beginLogging;

			public PonderingLogger(IDTLogger underlyingLogger)
			{
				this.underlyingLogger = underlyingLogger;
				this.beginLogging = false;
			}

			public void BeginLogging()
			{
				this.beginLogging = true;
			}

			public void Write(string str)
			{
				if (this.beginLogging)
					this.underlyingLogger.Write(str);
			}

			public void WriteLine(string str)
			{
				this.Write(str + "\n");
			}

			public void WriteLine()
			{
				this.Write("\n");
			}
		}

		private Dictionary<GameState, IChessAI> mapping;
		private ITimer timer;
		private IDTRandom random;
		private IDTLogger logger;
		private PonderingLogger ponderingLogger;
		private bool useDebugAI;

		private bool hasFinishedPopulatingAIsToProcess;
		private List<Move> potentialPlayerMovesList;
		private GameState originalGameState;

		private List<IChessAI> AIsToProcess;
		private List<IChessAI> AIsCompleted;
		private int searchDepth;
		private int searchTime;

		public AIPondering(GameState gameState, ITimer timer, IDTRandom random, IDTLogger logger, bool useDebugAI)
		{
			PonderingLogger ponderingLogger = new PonderingLogger(underlyingLogger: logger);

			this.mapping = new Dictionary<GameState, IChessAI>();
			this.timer = timer;
			this.random = random;
			this.logger = logger;
			this.ponderingLogger = ponderingLogger;
			this.useDebugAI = useDebugAI;

			this.AIsToProcess = new List<IChessAI>();
			this.AIsCompleted = new List<IChessAI>();
			this.searchDepth = 2;
			this.searchTime = 0;

			this.hasFinishedPopulatingAIsToProcess = false;
			this.potentialPlayerMovesList = ComputeMoves.GetMoves(gameState: gameState).Moves;
			this.originalGameState = gameState;
		}

		public IChessAI GetAIForGameState(GameState newGameState)
		{
			this.ponderingLogger.BeginLogging();

			if (this.mapping.ContainsKey(newGameState))
				return this.mapping[newGameState];

			return new CompositeAI(gameState: newGameState, timer: this.timer, random: this.random, logger: this.ponderingLogger, useDebugAI: this.useDebugAI);
		}

		public void CalculateBestMove(int millisecondsToThink)
		{
			this.searchTime += millisecondsToThink;
			
			if (!this.hasFinishedPopulatingAIsToProcess)
			{
				long startingTimeMillis = this.timer.GetNumberOfMicroSeconds() / 1000L;
				int count = 0;

				while (true)
				{
					if (this.potentialPlayerMovesList.Count == 0)
					{
						this.hasFinishedPopulatingAIsToProcess = true;
						return;
					}

					count++;
					if (count == 3)
					{
						count = 0;
						long currentTimeMillis = this.timer.GetNumberOfMicroSeconds() / 1000L;
						long elapsedTimeAsLong = currentTimeMillis - startingTimeMillis;
						int elapsedTimeMillis = (int)elapsedTimeAsLong;

						if (elapsedTimeMillis >= millisecondsToThink)
							return;
					}

					Move move = this.potentialPlayerMovesList[this.potentialPlayerMovesList.Count - 1];

					GameState newGameState = MoveImplementation.ApplyMove(gameState: this.originalGameState, move: move);

					ComputeMoves.Result newGameStateMoveResult = ComputeMoves.GetMoves(gameState: newGameState);

					if (newGameStateMoveResult.GameStatus == ComputeMoves.GameStatus.InProgress)
					{
						IChessAI ai = new CompositeAI(gameState: newGameState, timer: this.timer, random: this.random, logger: this.ponderingLogger, useDebugAI: this.useDebugAI);
						this.mapping[newGameState] = ai;
						this.AIsToProcess.Add(ai);
					}

					this.potentialPlayerMovesList.RemoveAt(this.potentialPlayerMovesList.Count - 1);
				}
			}

			if (this.AIsToProcess.Count == 0)
			{
				if (this.searchDepth <= 200 && this.AIsCompleted.Count > 0)
				{
					this.AIsToProcess = this.AIsCompleted;
					this.AIsCompleted = new List<IChessAI>();

					this.logger.WriteLine("Finished depth: " + this.searchDepth.ToStringCultureInvariant() + " (" + this.searchTime.ToStringCultureInvariant() + " ms)");

					this.searchDepth = this.searchDepth + 2;
				}
			}

			if (this.AIsToProcess.Count > 0)
			{
				IChessAI ai = this.AIsToProcess[this.AIsToProcess.Count - 1];
				ai.CalculateBestMove(millisecondsToThink: millisecondsToThink);

				if (ai.HasFinishedCalculation())
					this.AIsToProcess.RemoveAt(this.AIsToProcess.Count - 1);
				else
				{
					if (ai.GetDepthOfBestMoveFoundSoFar() >= this.searchDepth)
					{
						this.AIsToProcess.RemoveAt(this.AIsToProcess.Count - 1);
						this.AIsCompleted.Add(ai);
					}
				}
			}
		}
	}
}
