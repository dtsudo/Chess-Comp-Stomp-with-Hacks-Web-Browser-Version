
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System.Collections.Generic;

	public class SessionState
	{
		private class Data
		{
			public Data()
			{
				this.ResearchedHacks = new HashSet<Hack>();
				this.CompletedObjectives = new HashSet<Objective>();
				this.WasPlayerWhiteInPreviousGame = null;
				this.HasShownAIHackMessage = false;
				this.HasShownFinalBattleMessage = false;
				this.HasShownFinalBattleVictoryPanel = false;
				this.NumberOfWins = 0;
				this.StartTime = null;

				this.GameLogic = null;
				this.MostRecentGameLogic = null;
			}

			/// <summary>
			/// Null if player hasn't started playing yet
			/// or if player chose to "clear saved data".
			/// </summary>
			public long? StartTime;
			public int NumberOfWins;

			// Null if player hasn't played any games
			public bool? WasPlayerWhiteInPreviousGame;
			public bool HasShownAIHackMessage;
			public bool HasShownFinalBattleMessage;
			public bool HasShownFinalBattleVictoryPanel;
			public HashSet<Hack> ResearchedHacks;
			public HashSet<Objective> CompletedObjectives;

			public GameLogic GameLogic;
			public GameLogic MostRecentGameLogic;
		}

		public enum AIHackLevel
		{
			Initial,
			UpgradedOnce,
			UpgradedTwice,
			UpgradedThrice,
			FinalBattle
		}

		private ITimer timer;
		private Data data;

		public const int NUMBER_OF_HACK_POINTS_PER_WIN = 5;
		public const int NUMBER_OF_HACK_POINTS_PER_OBJECTIVE = 10;

		public SessionState(ITimer timer)
		{
			this.timer = timer;
			this.data = new Data();
		}
		
		public void ClearData()
		{
			this.data = new Data();
		}

		public void Debug_AddWin()
		{
			this.data.NumberOfWins++;
		}

		public GameLogic GetGameLogic()
		{
			return this.data.GameLogic;
		}

		public GameLogic GetMostRecentGameLogic()
		{
			return this.data.MostRecentGameLogic;
		}

		public void AddCompletedObjectives(HashSet<Objective> completedObjectives)
		{
			foreach (Objective completedObjective in completedObjectives)
			{
				this.data.CompletedObjectives.Add(completedObjective);
			}
		}

		public HashSet<Objective> GetCompletedObjectives()
		{
			return new HashSet<Objective>(this.data.CompletedObjectives);
		}
		
		public void StartNewGame()
		{
			this.data.StartTime = this.timer.GetNumberOfMicroSeconds();
		}

		public bool HasStarted
		{
			get
			{
				return this.data.StartTime != null;
			}
		}

		public HashSet<Hack> GetResearchedHacks()
		{
			return new HashSet<Hack>(this.data.ResearchedHacks);
		}

		public void AddResearchedHack(Hack hack)
		{
			this.data.ResearchedHacks.Add(hack);
		}

		/// <summary>
		/// Includes both used and unused hack points
		/// </summary>
		public int GetTotalNumberOfHackPoints()
		{
			int initialNumberOfHackPoints = 10;
			
			int numberOfPointsFromWins = this.data.NumberOfWins * NUMBER_OF_HACK_POINTS_PER_WIN;

			int numberOfPointsFromObjectives;
			
			if (this.data.CompletedObjectives.Contains(Objective.WinFinalBattle))
				numberOfPointsFromObjectives = (this.data.CompletedObjectives.Count - 1) * NUMBER_OF_HACK_POINTS_PER_OBJECTIVE;
			else
				numberOfPointsFromObjectives = this.data.CompletedObjectives.Count * NUMBER_OF_HACK_POINTS_PER_OBJECTIVE;

			return initialNumberOfHackPoints + numberOfPointsFromWins + numberOfPointsFromObjectives;
		}

		public int GetUnusedHackPoints()
		{
			int points = this.GetTotalNumberOfHackPoints();
			foreach (Hack hack in this.data.ResearchedHacks)
				points = points - hack.GetHackCost();

			return points;
		}

		public void ResetResearchedHacks()
		{
			this.data.ResearchedHacks = new HashSet<Hack>();
		}

		public bool HasShownFinalBattleVictoryPanel()
		{
			return this.data.HasShownFinalBattleVictoryPanel;
		}

		public void SetShownFinalBattleVictoryPanel()
		{
			this.data.HasShownFinalBattleVictoryPanel = true;
		}

		public void CompleteGame(bool didPlayerWin)
		{
			if (didPlayerWin)
				this.data.NumberOfWins++;

			this.data.MostRecentGameLogic = this.data.GameLogic;
			this.data.GameLogic = null;
		}

		public IFrame<ChessImage, ChessFont, ChessSound, ChessMusic> StartGame(bool isFinalBattle, GlobalState globalState)
		{
			bool isPlayerWhite;

			if (this.data.WasPlayerWhiteInPreviousGame == null)
				isPlayerWhite = true;
			else
				isPlayerWhite = !this.data.WasPlayerWhiteInPreviousGame.Value;

			if (isFinalBattle)
			{
				this.data.GameLogic = new GameLogic(
					globalState: globalState,
					isPlayerWhite: isPlayerWhite,
					researchedHacks: new DTImmutableList<Hack>(this.data.ResearchedHacks),
					aiHackLevel: AIHackLevel.FinalBattle);
				this.data.MostRecentGameLogic = this.data.GameLogic;

				this.data.WasPlayerWhiteInPreviousGame = isPlayerWhite;
				if (this.data.HasShownFinalBattleMessage)
					return new ChessFrame(globalState: globalState, sessionState: this);

				this.data.HasShownFinalBattleMessage = true;
				return AIMessageFrame.GetFinalBattleMessageFrame(globalState: globalState, sessionState: this);
			}

			AIHackLevel aiHackLevel;

			if (!this.data.HasShownAIHackMessage && this.data.ResearchedHacks.Count == 0 || this.data.NumberOfWins <= 1)
				aiHackLevel = AIHackLevel.Initial;
			else if (this.data.NumberOfWins <= 3)
				aiHackLevel = AIHackLevel.UpgradedOnce;
			else if (this.data.NumberOfWins <= 5)
				aiHackLevel = AIHackLevel.UpgradedTwice;
			else
				aiHackLevel = AIHackLevel.UpgradedThrice;

			this.data.GameLogic = new GameLogic(
				globalState: globalState,
				isPlayerWhite: isPlayerWhite,
				researchedHacks: new DTImmutableList<Hack>(this.data.ResearchedHacks),
				aiHackLevel: aiHackLevel);
			this.data.MostRecentGameLogic = this.data.GameLogic;
			
			this.data.WasPlayerWhiteInPreviousGame = isPlayerWhite;

			if (aiHackLevel != AIHackLevel.Initial && !this.data.HasShownAIHackMessage)
			{
				this.data.HasShownAIHackMessage = true;
				return AIMessageFrame.GetAIHackMessageFrame(globalState: globalState, sessionState: this);
			}

			return new ChessFrame(globalState: globalState, sessionState: this);
		}
	}
}
