
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System.Collections.Generic;

	public class SessionState
	{
		private class Data
		{
			public const int MAX_NUMBER_OF_WINS = 50000; // arbitrary

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

				this.ObjectivesThatWereAlreadyCompletedPriorToCurrentGame = new HashSet<Objective>();

				this.ColorTheme = ColorTheme.Initial;
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
			public HashSet<Objective> ObjectivesThatWereAlreadyCompletedPriorToCurrentGame;

			public ColorTheme ColorTheme;

			public GameLogic GameLogic;
			public GameLogic MostRecentGameLogic;

			/// <summary>
			/// Can possibly throw DTDeserializationException
			/// </summary>
			public static Data TryDeserializeEverythingExceptGameLogic(ByteList.Iterator iterator)
			{
				Data data = new Data();

				data.StartTime = iterator.TryPopNullableLong();

				data.NumberOfWins = iterator.TryPopInt();
				
				if (data.NumberOfWins < 0 || data.NumberOfWins > MAX_NUMBER_OF_WINS)
					throw new DTDeserializationException();

				data.WasPlayerWhiteInPreviousGame = iterator.TryPopNullableBool();

				data.HasShownAIHackMessage = iterator.TryPopBool();

				data.HasShownFinalBattleMessage = iterator.TryPopBool();
				
				data.HasShownFinalBattleVictoryPanel = iterator.TryPopBool();

				HashSet<int> researchedHackIds = iterator.TryPopIntSet();

				if (researchedHackIds == null)
					throw new DTDeserializationException();

				data.ResearchedHacks = new HashSet<Hack>();
				foreach (int researchedHackId in researchedHackIds)
				{
					Hack? hack = HackUtil.GetHackFromHackId(hackId: researchedHackId);

					if (hack == null)
						throw new DTDeserializationException();

					data.ResearchedHacks.Add(hack.Value);
				}
				
				HashSet<int> completedObjectiveIds = iterator.TryPopIntSet();

				if (completedObjectiveIds == null)
					throw new DTDeserializationException();

				data.CompletedObjectives = new HashSet<Objective>();
				foreach (int completedObjectiveId in completedObjectiveIds)
				{
					Objective? objective = ObjectiveUtil.GetObjectiveFromObjectiveId(objectiveId: completedObjectiveId);

					if (objective == null)
						throw new DTDeserializationException();

					data.CompletedObjectives.Add(objective.Value);
				}

				data.ObjectivesThatWereAlreadyCompletedPriorToCurrentGame = new HashSet<Objective>(data.CompletedObjectives);

				ColorTheme? colorTheme = ColorThemeUtil.GetColorThemeFromColorThemeId(iterator.TryPopInt());
				if (colorTheme == null)
					throw new DTDeserializationException();

				data.ColorTheme = colorTheme.Value;

				return data;
			}

			public void SerializeEverythingExceptGameLogic(ByteList.Builder list)
			{
				list.AddNullableLong(this.StartTime);

				list.AddInt(this.NumberOfWins);

				list.AddNullableBool(this.WasPlayerWhiteInPreviousGame);

				list.AddBool(this.HasShownAIHackMessage);

				list.AddBool(this.HasShownFinalBattleMessage);

				list.AddBool(this.HasShownFinalBattleVictoryPanel);
				
				HashSet<int> researchedHackIds = new HashSet<int>();
				foreach (Hack researchedHack in this.ResearchedHacks)
				{
					researchedHackIds.Add(researchedHack.GetHackId());
				}
				list.AddIntSet(researchedHackIds);
				
				HashSet<int> completedObjectiveIds = new HashSet<int>();
				foreach (Objective completedObjective in this.CompletedObjectives)
				{
					completedObjectiveIds.Add(completedObjective.GetObjectiveId());
				}
				list.AddIntSet(completedObjectiveIds);

				list.AddInt(this.ColorTheme.GetColorThemeId());
			}
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

		public void SerializeEverythingExceptGameLogic(ByteList.Builder list)
		{
			this.data.SerializeEverythingExceptGameLogic(list: list);
		}

		/// <summary>
		/// Can possibly throw DTDeserializationException
		/// </summary>
		public void TryDeserializeEverythingExceptGameLogic(ByteList.Iterator iterator)
		{
			Data data = Data.TryDeserializeEverythingExceptGameLogic(iterator: iterator);
			this.data = data;
		}

		public void ClearData()
		{
			this.data = new Data();
		}

		public void Debug_AddWin()
		{
			if (this.data.NumberOfWins < Data.MAX_NUMBER_OF_WINS)
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

		public ColorTheme GetColorTheme()
		{
			return this.data.ColorTheme;
		}

		/// <summary>
		/// Returns true iff at least one objective was actually newly completed
		/// </summary>
		public bool AddCompletedObjectives(HashSet<Objective> completedObjectives)
		{
			bool hasCompletedANewObjective = false;

			foreach (Objective completedObjective in completedObjectives)
			{
				bool didAdd = this.data.CompletedObjectives.Add(completedObjective);

				if (didAdd)
					hasCompletedANewObjective = true;
			}

			return hasCompletedANewObjective;
		}

		public HashSet<Objective> GetCompletedObjectives()
		{
			return new HashSet<Objective>(this.data.CompletedObjectives);
		}

		public HashSet<string> GetCompletedAchievements()
		{
			int numberOfCompletedObjectives = this.data.CompletedObjectives.Count;

			HashSet<string> completedAchievements = new HashSet<string>();

			if (numberOfCompletedObjectives >= 1)
				completedAchievements.Add("completed_1_objective");

			for (int i = 2; i <= numberOfCompletedObjectives; i++)
				completedAchievements.Add("completed_" + i.ToStringCultureInvariant() + "_objectives");

			return completedAchievements;
		}

		public HashSet<Objective> GetObjectivesThatWereAlreadyCompletedPriorToCurrentGame()
		{
			return new HashSet<Objective>(this.data.ObjectivesThatWereAlreadyCompletedPriorToCurrentGame);
		}

		public void StartNewSession()
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
			{
				if (this.data.NumberOfWins < Data.MAX_NUMBER_OF_WINS)
					this.data.NumberOfWins++;
			}

			this.data.MostRecentGameLogic = this.data.GameLogic;
			this.data.GameLogic = null;
		}

		public bool WillPlayerBeWhiteNextGame()
		{
			if (this.data.WasPlayerWhiteInPreviousGame == null)
				return true;
			return !this.data.WasPlayerWhiteInPreviousGame.Value;
		}

		public IFrame<GameImage, GameFont, GameSound, GameMusic> StartGame(bool isFinalBattle, GlobalState globalState, AIHackLevel? aiHackLevelOverride)
		{
			this.data.ObjectivesThatWereAlreadyCompletedPriorToCurrentGame = new HashSet<Objective>(this.data.CompletedObjectives);

			bool isPlayerWhite = this.WillPlayerBeWhiteNextGame();

			if (isFinalBattle)
			{
				this.data.ColorTheme = ColorTheme.Final;

				this.data.GameLogic = new GameLogic(
					globalState: globalState,
					isPlayerWhite: isPlayerWhite,
					researchedHacks: new DTImmutableList<Hack>(this.data.ResearchedHacks),
					aiHackLevel: AIHackLevel.FinalBattle,
					colorTheme: this.data.ColorTheme);
				this.data.MostRecentGameLogic = this.data.GameLogic;

				this.data.WasPlayerWhiteInPreviousGame = isPlayerWhite;
				if (this.data.HasShownFinalBattleMessage)
					return new ChessFrame(globalState: globalState, sessionState: this);

				this.data.HasShownFinalBattleMessage = true;
				return AIMessageFrame.GetFinalBattleMessageFrame(globalState: globalState, sessionState: this);
			}

			AIHackLevel aiHackLevel;

			if (aiHackLevelOverride.HasValue)
			{
				aiHackLevel = aiHackLevelOverride.Value;
			}
			else if (!this.data.HasShownAIHackMessage && this.data.ResearchedHacks.Count == 0 || this.data.NumberOfWins <= 1)
			{
				aiHackLevel = AIHackLevel.Initial;
			}
			else if (this.data.NumberOfWins <= 3)
			{
				aiHackLevel = AIHackLevel.UpgradedOnce;
				if (this.data.ColorTheme == ColorTheme.Initial)
					this.data.ColorTheme = ColorTheme.Progress1;
			}
			else if (this.data.NumberOfWins <= 5)
			{
				aiHackLevel = AIHackLevel.UpgradedTwice;
				if (this.data.ColorTheme == ColorTheme.Initial || this.data.ColorTheme == ColorTheme.Progress1)
					this.data.ColorTheme = ColorTheme.Progress2;
			}
			else
			{
				aiHackLevel = AIHackLevel.UpgradedThrice;
				if (this.data.ColorTheme == ColorTheme.Initial || this.data.ColorTheme == ColorTheme.Progress1 || this.data.ColorTheme == ColorTheme.Progress2)
					this.data.ColorTheme = ColorTheme.Progress3;
			}

			this.data.GameLogic = new GameLogic(
				globalState: globalState,
				isPlayerWhite: isPlayerWhite,
				researchedHacks: new DTImmutableList<Hack>(this.data.ResearchedHacks),
				aiHackLevel: aiHackLevel,
				colorTheme: this.data.ColorTheme);
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
