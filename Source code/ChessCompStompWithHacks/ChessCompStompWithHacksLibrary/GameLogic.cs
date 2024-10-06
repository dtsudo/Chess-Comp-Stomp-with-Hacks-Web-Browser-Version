﻿
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class GameLogic
	{
		private class Coordinates
		{
			public int ChessPiecesRendererX { get; private set; }
			public int ChessPiecesRendererY { get; private set; }
			public int ChessPieceScalingFactor { get; private set; }

			public int NukeRendererX { get; private set; }
			public int NukeRendererY { get; private set; }
			
			public int MoveTrackerRendererX { get; private set; }
			public int MoveTrackerRendererY { get; private set; }

			public int NukeRendererScalingFactorScaled { get; private set; }

			public Coordinates(DisplayType displayType)
			{
				switch (displayType)
				{
					case DisplayType.Desktop:
						this.ChessPiecesRendererX = 25 + 136 + 25;
						this.ChessPiecesRendererY = 50;
						this.ChessPieceScalingFactor = GameImageUtil.DesktopChessPieceScalingFactor;
						this.NukeRendererX = 25;
						this.NukeRendererY = 50;
						this.MoveTrackerRendererX = 720;
						this.MoveTrackerRendererY = 208;
						this.NukeRendererScalingFactorScaled = 128;
						break;
					case DisplayType.MobileLandscape:
						this.ChessPiecesRendererX = 160;
						this.ChessPiecesRendererY = 10;
						this.ChessPieceScalingFactor = GameImageUtil.MobileChessPieceScalingFactor;
						this.NukeRendererX = 10;
						this.NukeRendererY = 10;
						this.MoveTrackerRendererX = 850;
						this.MoveTrackerRendererY = 383;
						this.NukeRendererScalingFactorScaled = 128;
						break;
					case DisplayType.MobilePortrait:
						this.ChessPiecesRendererX = 10;
						this.ChessPiecesRendererY = 210;
						this.ChessPieceScalingFactor = GameImageUtil.MobileChessPieceScalingFactor;
						this.NukeRendererX = 160;
						this.NukeRendererY = 10;
						this.MoveTrackerRendererX = 10;
						this.MoveTrackerRendererY = 10;
						this.NukeRendererScalingFactorScaled = 64;
						break;
					default:
						throw new Exception();
				}
			}
		}

		private class ChessPiecesRendererMouse : IMouse
		{
			private TranslatedMouse mouse;

			public ChessPiecesRendererMouse(IMouse mouse, Coordinates coordinates)
			{
				this.mouse = new TranslatedMouse(mouse: mouse, xOffset: -coordinates.ChessPiecesRendererX, yOffset: -coordinates.ChessPiecesRendererY);
			}

			public int GetX()
			{
				return this.mouse.GetX();
			}

			public int GetY()
			{
				return this.mouse.GetY();
			}

			public bool IsLeftMouseButtonPressed()
			{
				return this.mouse.IsLeftMouseButtonPressed();
			}

			public bool IsRightMouseButtonPressed()
			{
				return this.mouse.IsRightMouseButtonPressed();
			}
		}

		private class NukeRendererMouse : IMouse
		{
			private TranslatedMouse mouse;

			public NukeRendererMouse(IMouse mouse, Coordinates coordinates)
			{
				this.mouse = new TranslatedMouse(mouse: mouse, xOffset: -coordinates.NukeRendererX, yOffset: -coordinates.NukeRendererY);
			}

			public int GetX()
			{
				return this.mouse.GetX();
			}

			public int GetY()
			{
				return this.mouse.GetY();
			}

			public bool IsLeftMouseButtonPressed()
			{
				return this.mouse.IsLeftMouseButtonPressed();
			}

			public bool IsRightMouseButtonPressed()
			{
				return this.mouse.IsRightMouseButtonPressed();
			}
		}

		private class MoveTrackerRendererMouse : IMouse
		{
			private TranslatedMouse mouse;

			public MoveTrackerRendererMouse(IMouse mouse, Coordinates coordinates)
			{
				this.mouse = new TranslatedMouse(mouse: mouse, xOffset: -coordinates.MoveTrackerRendererX, yOffset: -coordinates.MoveTrackerRendererY);
			}

			public int GetX()
			{
				return this.mouse.GetX();
			}

			public int GetY()
			{
				return this.mouse.GetY();
			}

			public bool IsLeftMouseButtonPressed()
			{
				return this.mouse.IsLeftMouseButtonPressed();
			}

			public bool IsRightMouseButtonPressed()
			{
				return this.mouse.IsRightMouseButtonPressed();
			}
		}
		
		private GlobalState globalState;
		private GameState gameState;
		private MoveTracker moveTracker;
		private MoveTrackerRenderer moveTrackerRenderer;
		private int? moveTrackerRendererPositionIndex;

		private ChessPiecesRenderer chessPiecesRenderer;
		private ChessPiecesRendererPieceAnimation chessPiecesRendererPieceAnimation;
		private PromotionPanel promotionPanel;
		private int promotionPanelX;
		private int promotionPanelY;
		private NukeRenderer nukeRenderer;
		private List<DisplayMove> promotionMoves;

		private int nukeLaunchSoundCooldown;

		// player clicked on a square (containing a player piece)
		// -> should show locations that the piece can move to
		private ChessSquare clickedSquare;

		// player clicked-and-held on a square 
		// -> should show locations that the piece can move to (if the square contains player piece)
		// -> should show piece on cursor (if the square contains player piece)
		private ChessSquare clickedAndHeldSquare;

		private bool isNukeInProgress;
		private bool isNukeLiftingOff;
		private bool isNukeInFlight;
		private DisplayMove nukeMove;

		private bool hasClickedOnNuke;
		private bool hasClickedAndHeldOnNuke;

		private bool isPromotionPanelOpen;

		private IChessAI chessAI;
		private AIPondering aiPondering;

		private ComputeMoves.GameStatus gameStatus;
		private List<DisplayMove> possibleMoves;

		private int aiElapsedTimeThinking;

		// Non-null when the player has clicked on (but not yet released the mouse button)
		private Move.PromotionType? clickedPromotionPiece;

		private bool isFinalBattle;

		private IMouse previousMouseInput;

		private Coordinates previousCoordinates;

		public GameLogic(
			GlobalState globalState,
			bool isPlayerWhite,
			DTImmutableList<Hack> researchedHacks,
			SessionState.AIHackLevel aiHackLevel,
			ColorTheme colorTheme,
			DisplayType displayType)
		{
			Coordinates coordinates = new Coordinates(displayType: displayType);
			this.previousCoordinates = coordinates;

			this.globalState = globalState;
			this.gameState = NewGameCreation.CreateNewGame(
				isPlayerWhite: isPlayerWhite,
				researchedHacks: researchedHacks,
				aiHackLevel: aiHackLevel);
			this.moveTracker = new MoveTracker(colorTheme: colorTheme);
			this.moveTrackerRenderer = MoveTrackerRenderer.GetMoveTrackerRenderer(moveTracker: this.moveTracker, colorTheme: colorTheme);
			this.moveTrackerRendererPositionIndex = null;

			this.chessPiecesRenderer = ChessPiecesRenderer.GetChessPiecesRenderer(
				pieces: this.gameState.Board,
				kingInDangerSquare: null,
				previousMoveSquares: DTImmutableList<ChessSquare>.EmptyList(),
				renderFromWhitePerspective: this.gameState.IsPlayerWhite,
				colorTheme: colorTheme);
			this.chessPiecesRendererPieceAnimation = ChessPiecesRendererPieceAnimation.GetChessPiecesRendererPieceAnimation();
			this.promotionPanel = PromotionPanel.GetPromotionPanel(isWhite: this.gameState.IsPlayerWhite, colorTheme: colorTheme);
			this.promotionPanelX = 0;
			this.promotionPanelY = 0;
			this.nukeRenderer = NukeRenderer.GetNukeRenderer(
				hasNukeAbility: this.gameState.Abilities.HasTacticalNuke,
				hasUsedNuke: this.gameState.HasUsedNuke,
				isNukeSelected: false,
				isHoverOverNuke: null,
				turnCount: this.gameState.TurnCount,
				timer: globalState.Timer,
				colorTheme: colorTheme,
				isClickingOnNuke: false);
			this.promotionMoves = null;

			this.clickedSquare = null;
			this.clickedAndHeldSquare = null;

			this.isNukeInProgress = false;
			this.isNukeLiftingOff = false;
			this.isNukeInFlight = false;
			this.nukeMove = null;

			this.hasClickedOnNuke = false;
			this.hasClickedAndHeldOnNuke = false;

			this.isPromotionPanelOpen = false;

			this.chessAI = null;
			this.aiPondering = null;

			ComputeMoves.Result result = ComputeMoves.GetMoves(gameState: this.gameState);
			this.gameStatus = result.GameStatus;
			this.possibleMoves = DisplayMove.GetDisplayMoves(moves: result.Moves, gameState: this.gameState);
			
			this.aiElapsedTimeThinking = 0;

			this.clickedPromotionPiece = null;

			this.isFinalBattle = aiHackLevel == SessionState.AIHackLevel.FinalBattle;

			this.previousMouseInput = new EmptyMouse();

			this.nukeLaunchSoundCooldown = 0;
		}

		public class Result
		{
			public Result(
				ComputeMoves.GameStatus gameStatus,
				List<Objective> completedObjectives,
				bool isPlayerWhite,
				bool isFinalBattle)
			{
				this.GameStatus = gameStatus;
				this.CompletedObjectives = completedObjectives;
				this.IsPlayerWhite = isPlayerWhite;
				this.IsFinalBattle = isFinalBattle;
			}

			public ComputeMoves.GameStatus GameStatus { get; private set; }
			public List<Objective> CompletedObjectives { get; private set; }
			public bool IsPlayerWhite { get; private set; }
			public bool IsFinalBattle { get; private set; }
		}

		public void ProcessExtraTime(int milliseconds)
		{
			if (this.chessAI != null)
				this.chessAI.CalculateBestMove(millisecondsToThink: milliseconds);
			else if (this.aiPondering != null)
				this.aiPondering.CalculateBestMove(millisecondsToThink: milliseconds);
		}

		private class PlayerMoveInfo
		{
			public PlayerMoveInfo(
				DisplayMove displayMove,
				bool shouldMoveBeInstant)
			{
				this.DisplayMove = displayMove;
				this.ShouldMoveBeInstant = shouldMoveBeInstant;
			}

			public DisplayMove DisplayMove { get; private set; }
			public bool ShouldMoveBeInstant { get; private set; }
		}

		private static PlayerMoveInfo GetPlayerMove(
			IMouse mouseInput,
			IMouse previousMouseInput,
			bool isPromotionPanelOpen,
			Move.PromotionType? clickedPromotionPiece,
			List<DisplayMove> possibleMoves,
			List<DisplayMove> promotionMoves,
			bool isPlayerWhite,
			bool isNukeInFlight,
			int promotionPanelX,
			int promotionPanelY,
			ChessSquare clickedSquare,
			ChessSquare clickedAndHeldSquare,
			bool hasClickedOnNuke,
			bool hasClickedAndHeldOnNuke,
			IDisplayProcessing<GameImage> displayProcessing,
			bool isMobileDisplayType,
			Coordinates coordinates)
		{
			ChessSquare hoverSquare = ChessPiecesRenderer.GetHoverSquare(
				mouseInput: new ChessPiecesRendererMouse(mouse: mouseInput, coordinates: coordinates), 
				renderFromWhitePerspective: isPlayerWhite,
				chessPieceScalingFactor: coordinates.ChessPieceScalingFactor,
				displayProcessing: displayProcessing);

			if (isNukeInFlight)
				return new PlayerMoveInfo(displayMove: null, shouldMoveBeInstant: false);

			if (isPromotionPanelOpen)
			{
				if (!mouseInput.IsLeftMouseButtonPressed() && previousMouseInput.IsLeftMouseButtonPressed())
				{
					if (clickedPromotionPiece != null)
					{
						Move.PromotionType? hoverOverSquare = PromotionPanel.IsHoverOverSquare(
							promotionPanelX: promotionPanelX,
							promotionPanelY: promotionPanelY,
							mouse: mouseInput,
							displayProcessing: displayProcessing,
							isMobileDisplayType: isMobileDisplayType);

						if (hoverOverSquare != null && clickedPromotionPiece.Value == hoverOverSquare.Value)
							return new PlayerMoveInfo(displayMove: promotionMoves.Single(x => x.Promotion.Value == hoverOverSquare.Value), shouldMoveBeInstant: false);
					}
				}
				return new PlayerMoveInfo(displayMove: null, shouldMoveBeInstant: false);
			}
			
			if (!mouseInput.IsLeftMouseButtonPressed() && previousMouseInput.IsLeftMouseButtonPressed())
			{
				if (clickedSquare == null && clickedAndHeldSquare != null && hoverSquare != null && !hasClickedOnNuke)
				{
					List<DisplayMove> moves = possibleMoves.Where(x => !x.IsNuke
						&& x.StartingFile.Value == clickedAndHeldSquare.File
						&& x.StartingRank.Value == clickedAndHeldSquare.Rank
						&& x.EndingFile == hoverSquare.File
						&& x.EndingRank == hoverSquare.Rank).ToList();

					if (moves.Count == 1)
						return new PlayerMoveInfo(displayMove: moves[0], shouldMoveBeInstant: true);
				}

				if (clickedSquare != null && clickedAndHeldSquare != null && hoverSquare != null && hoverSquare.Equals(clickedAndHeldSquare))
				{
					List<DisplayMove> moves = possibleMoves.Where(x => !x.IsNuke
						&& x.StartingFile.Value == clickedSquare.File
						&& x.StartingRank.Value == clickedSquare.Rank
						&& x.EndingFile == hoverSquare.File
						&& x.EndingRank == hoverSquare.Rank).ToList();

					if (moves.Count == 1)
						return new PlayerMoveInfo(displayMove: moves[0], shouldMoveBeInstant: false);
				}

				if (clickedSquare != null && clickedAndHeldSquare != null && hoverSquare != null && clickedSquare.Equals(clickedAndHeldSquare))
				{
					List<DisplayMove> moves = possibleMoves.Where(x => !x.IsNuke
						&& x.StartingFile.Value == clickedAndHeldSquare.File
						&& x.StartingRank.Value == clickedAndHeldSquare.Rank
						&& x.EndingFile == hoverSquare.File
						&& x.EndingRank == hoverSquare.Rank).ToList();

					if (moves.Count == 1)
						return new PlayerMoveInfo(displayMove: moves[0], shouldMoveBeInstant: true);
				}

				if (hasClickedOnNuke && clickedAndHeldSquare != null && hoverSquare != null && (hoverSquare.Equals(clickedAndHeldSquare) || isMobileDisplayType))
				{
					List<DisplayMove> moves = possibleMoves.Where(x => x.IsNuke && x.EndingFile == hoverSquare.File && x.EndingRank == hoverSquare.Rank).ToList();
					if (moves.Count == 1)
						return new PlayerMoveInfo(displayMove: moves[0], shouldMoveBeInstant: false);
				}

				if (hasClickedAndHeldOnNuke && hoverSquare != null)
				{
					List<DisplayMove> moves = possibleMoves.Where(x => x.IsNuke && x.EndingFile == hoverSquare.File && x.EndingRank == hoverSquare.Rank).ToList();
					if (moves.Count == 1)
						return new PlayerMoveInfo(displayMove: moves[0], shouldMoveBeInstant: false);
				}
			}

			return new PlayerMoveInfo(displayMove: null, shouldMoveBeInstant: false);
		}

		private static ChessSquare GetClickedSquare(
			IMouse mouseInput,
			IMouse previousMouseInput,
			ChessSquarePieceArray board,
			bool isPlayerWhite,
			IDisplayProcessing<GameImage> displayProcessing,
			List<DisplayMove> possibleMoves,
			ChessSquare clickedSquare,
			ChessSquare clickedAndHeldSquare,
			bool hasNukeAbility,
			bool hasUsedNuke,
			bool isNukeInFlight,
			Coordinates coordinates)
		{
			ChessSquare hoverSquare = ChessPiecesRenderer.GetHoverSquare(
				mouseInput: new ChessPiecesRendererMouse(mouse: mouseInput, coordinates: coordinates),
				renderFromWhitePerspective: isPlayerWhite,
				chessPieceScalingFactor: coordinates.ChessPieceScalingFactor,
				displayProcessing: displayProcessing);

			if (mouseInput.IsLeftMouseButtonPressed() && !previousMouseInput.IsLeftMouseButtonPressed())
			{
				if (hoverSquare != null)
				{
					ChessSquarePiece piece = board.GetPiece(hoverSquare);
					if (piece != ChessSquarePiece.Empty && piece.IsWhite() == isPlayerWhite)
					{
						if (clickedSquare != null && !clickedSquare.Equals(hoverSquare))
						{
							List<DisplayMove> moves = possibleMoves.Where(x => !x.IsNuke
								&& x.StartingFile.Value == clickedSquare.File
								&& x.StartingRank.Value == clickedSquare.Rank
								&& x.EndingFile == hoverSquare.File
								&& x.EndingRank == hoverSquare.Rank).ToList();

							if (moves.Count == 0)
								return null;
						}
					}
				}

				if (hasNukeAbility && !hasUsedNuke && !isNukeInFlight)
				{
					if (NukeRenderer.IsHoverOverNuke(mouse: new NukeRendererMouse(mouse: mouseInput, coordinates: coordinates), scalingFactorScaled: coordinates.NukeRendererScalingFactorScaled))
						return null;
				}
			}

			if (!mouseInput.IsLeftMouseButtonPressed() && previousMouseInput.IsLeftMouseButtonPressed())
			{
				if (clickedSquare == null && clickedAndHeldSquare != null && hoverSquare != null && hoverSquare.Equals(clickedAndHeldSquare))
				{
					ChessSquarePiece piece = board.GetPiece(hoverSquare);
					if (piece != ChessSquarePiece.Empty && piece.IsWhite() == isPlayerWhite)
						return hoverSquare;
				}
								
				if (clickedSquare != null && clickedAndHeldSquare != null && hoverSquare != null && hoverSquare.Equals(clickedAndHeldSquare)
					&& !clickedSquare.Equals(hoverSquare))
				{
					List<DisplayMove> moves = possibleMoves.Where(x => !x.IsNuke
						&& x.StartingFile.Value == clickedSquare.File
						&& x.StartingRank.Value == clickedSquare.Rank
						&& x.EndingFile == hoverSquare.File
						&& x.EndingRank == hoverSquare.Rank).ToList();

					if (moves.Count == 0)
					{
						ChessSquarePiece piece = board.GetPiece(hoverSquare);
						if (piece != ChessSquarePiece.Empty && piece.IsWhite() == isPlayerWhite)
							return hoverSquare;
					}
				}

				return null;
			}

			return clickedSquare;
		}

		private static ChessSquare GetClickedAndHeldSquare(
			IMouse mouseInput,
			IMouse previousMouseInput,
			bool isPlayerWhite,
			IDisplayProcessing<GameImage> displayProcessing,
			ChessSquare clickedAndHeldSquare,
			bool isPromotionPanelOpen,
			int promotionPanelX,
			int promotionPanelY,
			Coordinates coordinates,
			bool isMobileDisplayType)
		{
			ChessSquare hoverSquare = ChessPiecesRenderer.GetHoverSquare(
				mouseInput: new ChessPiecesRendererMouse(mouse: mouseInput, coordinates: coordinates),
				renderFromWhitePerspective: isPlayerWhite,
				chessPieceScalingFactor: coordinates.ChessPieceScalingFactor,
				displayProcessing: displayProcessing);

			if (mouseInput.IsLeftMouseButtonPressed() && !previousMouseInput.IsLeftMouseButtonPressed())
			{
				if (hoverSquare != null)
				{
					bool isHoverOverPanel = PromotionPanel.IsHoverOverPanel(
						promotionPanelX: promotionPanelX,
						promotionPanelY: promotionPanelY,
						mouse: mouseInput,
						isMobileDisplayType: isMobileDisplayType);
					if (!isPromotionPanelOpen || !isHoverOverPanel)
						return hoverSquare;
				}
			}

			if (!mouseInput.IsLeftMouseButtonPressed() && previousMouseInput.IsLeftMouseButtonPressed())
				return null;

			return clickedAndHeldSquare;
		}

		private class PromotionPanelInfo
		{
			public PromotionPanelInfo(
				bool isPromotionPanelOpen,
				int promotionPanelX,
				int promotionPanelY,
				List<DisplayMove> promotionMoves)
			{
				this.IsPromotionPanelOpen = isPromotionPanelOpen;
				this.PromotionPanelX = promotionPanelX;
				this.PromotionPanelY = promotionPanelY;
				this.PromotionMoves = promotionMoves;
			}

			public bool IsPromotionPanelOpen { get; private set; }
			public int PromotionPanelX { get; private set; }
			public int PromotionPanelY { get; private set; }
			public List<DisplayMove> PromotionMoves { get; private set; }
		}

		private static int GetPromotionPanelX(int mouseX, Coordinates coordinates, IDisplayProcessing<GameImage> displayProcessing, bool isMobileDisplayType)
		{
			int panelWidth = isMobileDisplayType ? PromotionPanel.PROMOTION_PANEL_WIDTH_MOBILE : PromotionPanel.PROMOTION_PANEL_WIDTH_DESKTOP;
			int imageWidth = displayProcessing.GetWidth(GameImage.WhitePawn) * coordinates.ChessPieceScalingFactor / 128;

			int returnValue = mouseX;

			int panelRightEdge = returnValue + panelWidth;

			int chessboardRightEdge = coordinates.ChessPiecesRendererX + 8 * imageWidth;

			if (panelRightEdge + 5 > chessboardRightEdge)
			{
				int offset = panelRightEdge + 5 - chessboardRightEdge;
				returnValue = returnValue - offset;
			}

			return returnValue;
		}

		private static PromotionPanelInfo GetPromotionPanelInfo(
			bool isPromotionPanelOpen,
			int promotionPanelX,
			int promotionPanelY,
			List<DisplayMove> promotionMoves,
			IMouse mouseInput,
			IMouse previousMouseInput,
			ChessSquare clickedSquare,
			ChessSquare clickedAndHeldSquare,
			bool isPlayerWhite,
			IDisplayProcessing<GameImage> displayProcessing,
			List<DisplayMove> possibleMoves,
			bool isNukeInFlight,
			bool hasClickedOnNuke,
			Coordinates coordinates,
			bool isMobileDisplayType)
		{
			if (isNukeInFlight || hasClickedOnNuke)
				return new PromotionPanelInfo(isPromotionPanelOpen: false, promotionPanelX: 0, promotionPanelY: 0, promotionMoves: null);

			ChessSquare hoverSquare = ChessPiecesRenderer.GetHoverSquare(
				mouseInput: new ChessPiecesRendererMouse(mouse: mouseInput, coordinates: coordinates),
				renderFromWhitePerspective: isPlayerWhite,
				chessPieceScalingFactor: coordinates.ChessPieceScalingFactor,
				displayProcessing: displayProcessing);

			if (isPromotionPanelOpen)
			{
				if (mouseInput.IsLeftMouseButtonPressed() && !previousMouseInput.IsLeftMouseButtonPressed())
				{
					bool isHoverOverPanel = PromotionPanel.IsHoverOverPanel(promotionPanelX: promotionPanelX, promotionPanelY: promotionPanelY, mouse: mouseInput, isMobileDisplayType: isMobileDisplayType);
					if (!isHoverOverPanel)
						return new PromotionPanelInfo(isPromotionPanelOpen: false, promotionPanelX: 0, promotionPanelY: 0, promotionMoves: null);
				}
			}
			
			if (!mouseInput.IsLeftMouseButtonPressed() && previousMouseInput.IsLeftMouseButtonPressed())
			{
				if (clickedSquare == null && clickedAndHeldSquare != null && hoverSquare != null && !hoverSquare.Equals(clickedAndHeldSquare))
				{
					List<DisplayMove> moves = possibleMoves.Where(x => !x.IsNuke
						&& x.StartingFile.Value == clickedAndHeldSquare.File
						&& x.StartingRank.Value == clickedAndHeldSquare.Rank
						&& x.EndingFile == hoverSquare.File
						&& x.EndingRank == hoverSquare.Rank).ToList();

					if (moves.Count > 0 && moves[0].Promotion != null)
						return new PromotionPanelInfo(isPromotionPanelOpen: true, promotionPanelX: GetPromotionPanelX(mouseInput.GetX(), coordinates: coordinates, displayProcessing: displayProcessing, isMobileDisplayType: isMobileDisplayType), promotionPanelY: mouseInput.GetY(), promotionMoves: moves);
				}
				else if (clickedSquare != null && clickedAndHeldSquare != null && hoverSquare != null && hoverSquare.Equals(clickedAndHeldSquare)
					&& !clickedSquare.Equals(hoverSquare))
				{
					List<DisplayMove> moves = possibleMoves.Where(x => !x.IsNuke
						&& x.StartingFile.Value == clickedSquare.File
						&& x.StartingRank.Value == clickedSquare.Rank
						&& x.EndingFile == hoverSquare.File
						&& x.EndingRank == hoverSquare.Rank).ToList();

					if (moves.Count > 0 && moves[0].Promotion != null)
						return new PromotionPanelInfo(isPromotionPanelOpen: true, promotionPanelX: GetPromotionPanelX(mouseInput.GetX(), coordinates: coordinates, displayProcessing: displayProcessing, isMobileDisplayType: isMobileDisplayType), promotionPanelY: mouseInput.GetY(), promotionMoves: moves);
				}
				else if (clickedSquare != null && clickedAndHeldSquare != null && hoverSquare != null && clickedSquare.Equals(clickedAndHeldSquare))
				{
					List<DisplayMove> moves = possibleMoves.Where(x => !x.IsNuke
						&& x.StartingFile.Value == clickedAndHeldSquare.File
						&& x.StartingRank.Value == clickedAndHeldSquare.Rank
						&& x.EndingFile == hoverSquare.File
						&& x.EndingRank == hoverSquare.Rank).ToList();

					if (moves.Count > 0 && moves[0].Promotion != null)
						return new PromotionPanelInfo(isPromotionPanelOpen: true, promotionPanelX: GetPromotionPanelX(mouseInput.GetX(), coordinates: coordinates, displayProcessing: displayProcessing, isMobileDisplayType: isMobileDisplayType), promotionPanelY: mouseInput.GetY(), promotionMoves: moves);
				}
			}

			return new PromotionPanelInfo(isPromotionPanelOpen: isPromotionPanelOpen, promotionPanelX: promotionPanelX, promotionPanelY: promotionPanelY, promotionMoves: promotionMoves);
		}

		private static Move.PromotionType? GetClickedPromotionPiece(
			bool isPromotionPanelOpen,
			int promotionPanelX,
			int promotionPanelY,
			IMouse mouseInput,
			IMouse previousMouseInput,
			Move.PromotionType? clickedPromotionPiece,
			bool isMobileDisplayType,
			IDisplayProcessing<GameImage> displayProcessing)
		{
			if (isPromotionPanelOpen)
			{
				if (mouseInput.IsLeftMouseButtonPressed() && !previousMouseInput.IsLeftMouseButtonPressed())
				{
					Move.PromotionType? hoverOverSquare = PromotionPanel.IsHoverOverSquare(
						promotionPanelX: promotionPanelX,
						promotionPanelY: promotionPanelY,
						mouse: mouseInput,
						displayProcessing: displayProcessing,
						isMobileDisplayType: isMobileDisplayType);

					return hoverOverSquare;
				}

				if (!mouseInput.IsLeftMouseButtonPressed() && previousMouseInput.IsLeftMouseButtonPressed())
					return null;

				return clickedPromotionPiece;
			}

			return null;
		}

		private static bool GetHasClickedOnNuke(
			ChessSquarePieceArray board,
			List<DisplayMove> possibleMoves,
			bool isPromotionPanelOpen,
			IMouse mouseInput,
			IMouse previousMouseInput,
			bool hasNukeAbility,
			bool hasUsedNuke,
			bool isNukeInFlight,
			bool hasClickedOnNuke,
			bool hasClickedAndHeldOnNuke,
			int promotionPanelX,
			int promotionPanelY,
			bool isPlayerWhite,
			IDisplayProcessing<GameImage> displayProcessing,
			Coordinates coordinates,
			bool isMobileDisplayType)
		{
			if (!hasNukeAbility)
				return false;

			if (hasUsedNuke)
				return false;

			if (isNukeInFlight)
				return false;

			if (!mouseInput.IsLeftMouseButtonPressed() && previousMouseInput.IsLeftMouseButtonPressed())
			{
				if (isPromotionPanelOpen && PromotionPanel.IsHoverOverPanel(promotionPanelX: promotionPanelX, promotionPanelY: promotionPanelY, mouse: mouseInput, isMobileDisplayType: isMobileDisplayType))
					return false;

				if (!hasClickedOnNuke && hasClickedAndHeldOnNuke && NukeRenderer.IsHoverOverNuke(mouse: new NukeRendererMouse(mouse: mouseInput, coordinates: coordinates), scalingFactorScaled: coordinates.NukeRendererScalingFactorScaled))
					return true;

				return false;
			}

			if (hasClickedOnNuke && mouseInput.IsLeftMouseButtonPressed() && !previousMouseInput.IsLeftMouseButtonPressed())
			{
				ChessSquare hoverSquare = ChessPiecesRenderer.GetHoverSquare(
					mouseInput: new ChessPiecesRendererMouse(mouse: mouseInput, coordinates: coordinates),
					renderFromWhitePerspective: isPlayerWhite,
					chessPieceScalingFactor: coordinates.ChessPieceScalingFactor,
					displayProcessing: displayProcessing);

				if (hoverSquare != null && !possibleMoves.Any(x => x.IsNuke && x.EndingFile == hoverSquare.File && x.EndingRank == hoverSquare.Rank))
				{
					ChessSquarePiece piece = board.GetPiece(hoverSquare);
					if (piece != ChessSquarePiece.Empty && piece.IsWhite() == isPlayerWhite)
						return false;
				}
			}

			return hasClickedOnNuke;
		}

		private static bool GetHasClickedAndHeldOnNuke(
			IMouse mouseInput,
			IMouse previousMouseInput,
			bool isPromotionPanelOpen,
			int promotionPanelX,
			int promotionPanelY,
			bool hasClickedAndHeldOnNuke,
			bool hasNukeAbility,
			bool hasUsedNuke,
			bool isNukeInFlight,
			Coordinates coordinates,
			bool isMobileDisplayType)
		{
			if (!hasNukeAbility)
				return false;

			if (hasUsedNuke)
				return false;

			if (isNukeInFlight)
				return false;

			if (mouseInput.IsLeftMouseButtonPressed() && !previousMouseInput.IsLeftMouseButtonPressed())
			{
				if (NukeRenderer.IsHoverOverNuke(mouse: new NukeRendererMouse(mouse: mouseInput, coordinates: coordinates), scalingFactorScaled: coordinates.NukeRendererScalingFactorScaled))
				{
					bool isHoverOverPanel = PromotionPanel.IsHoverOverPanel(
						promotionPanelX: promotionPanelX,
						promotionPanelY: promotionPanelY,
						mouse: mouseInput,
						isMobileDisplayType: isMobileDisplayType);
					if (!isPromotionPanelOpen || !isHoverOverPanel)
						return true;
				}
			}

			if (!mouseInput.IsLeftMouseButtonPressed() && previousMouseInput.IsLeftMouseButtonPressed())
				return false;

			return hasClickedAndHeldOnNuke;
		}

		public Result ProcessNextFrame(
			IMouse mouseInput,
			IDisplayProcessing<GameImage> displayProcessing,
			ISoundOutput<GameSound> soundOutput,
			int elapsedMicrosPerFrame,
			bool isMobileDisplayType)
		{
			IMouse previousMouseInput = this.previousMouseInput;
			this.previousMouseInput = new CopiedMouse(mouse: mouseInput);

			DisplayType displayType;

			if (!isMobileDisplayType)
				displayType = DisplayType.Desktop;
			else if (displayProcessing.IsMobileInLandscapeOrientation())
				displayType = DisplayType.MobileLandscape;
			else
				displayType = DisplayType.MobilePortrait;

			Coordinates coordinates = new Coordinates(displayType: displayType);

			if (this.isPromotionPanelOpen)
			{
				if (coordinates.ChessPiecesRendererX != this.previousCoordinates.ChessPiecesRendererX
					|| coordinates.ChessPiecesRendererY != this.previousCoordinates.ChessPiecesRendererY
					|| coordinates.ChessPieceScalingFactor != this.previousCoordinates.ChessPieceScalingFactor)
				{
					int xOffset = this.promotionPanelX - this.previousCoordinates.ChessPiecesRendererX;
					int yOffset = this.promotionPanelY - this.previousCoordinates.ChessPiecesRendererY;

					this.promotionPanelX = coordinates.ChessPiecesRendererX + xOffset * coordinates.ChessPieceScalingFactor / this.previousCoordinates.ChessPieceScalingFactor;
					this.promotionPanelY = coordinates.ChessPiecesRendererY + yOffset * coordinates.ChessPieceScalingFactor / this.previousCoordinates.ChessPieceScalingFactor;

					int promotionPanelWidth = isMobileDisplayType ? PromotionPanel.PROMOTION_PANEL_WIDTH_MOBILE : PromotionPanel.PROMOTION_PANEL_WIDTH_DESKTOP;
					int imageWidth = displayProcessing.GetWidth(GameImage.WhitePawn) * coordinates.ChessPieceScalingFactor / 128;

					int promotionPanelRightEdge = this.promotionPanelX + promotionPanelWidth;

					int chessboardRightEdge = coordinates.ChessPiecesRendererX + 8 * imageWidth;

					if (promotionPanelRightEdge + 5 > chessboardRightEdge)
					{
						int offset = promotionPanelRightEdge + 5 - chessboardRightEdge;
						this.promotionPanelX = this.promotionPanelX - offset;
					}
				}
			}

			this.previousCoordinates = coordinates;

			if (this.gameStatus != ComputeMoves.GameStatus.InProgress)
			{				
				this.chessPiecesRenderer = UpdateChessPiecesRenderer(
					chessPiecesRenderer: this.chessPiecesRenderer,
					gameState: this.gameState,
					isPlayerWhite: this.gameState.IsPlayerWhite,
					isPlayerTurn: this.gameState.IsPlayerTurn(),
					clickedSquare: null,
					clickedAndHeldSquare: null,
					moves: this.possibleMoves,
					mouseInput: mouseInput,
					displayProcessing: displayProcessing,
					mostRecentMove: this.moveTracker.GetMostRecentMove(),
					isPromotionPanelOpen: this.isPromotionPanelOpen,
					promotionPanelX: this.promotionPanelX,
					promotionPanelY: this.promotionPanelY,
					elapsedMicrosPerFrame: elapsedMicrosPerFrame,
					hasClickedOnNuke: false,
					hasClickedAndHeldOnNuke: false,
					isNukeInFlight: this.isNukeInFlight,
					turnCount: this.gameState.TurnCount,
					coordinates: coordinates,
					displayType: displayType);

				this.chessPiecesRendererPieceAnimation = this.chessPiecesRendererPieceAnimation.ProcessFrame(elapsedMicrosPerFrame: elapsedMicrosPerFrame);

				this.nukeRenderer = this.nukeRenderer.ProcessFrame(
					hasUsedNuke: this.gameState.HasUsedNuke,
					isNukeSelected: false,
					isHoverOverNuke: null,
					turnCount: this.gameState.TurnCount,
					elapsedMicrosPerFrame: elapsedMicrosPerFrame,
					mouseInput: new EmptyMouse());

				int? originalMoveTrackerRendererPositionIndex = this.moveTrackerRendererPositionIndex;

				this.moveTrackerRendererPositionIndex = MoveTrackerRenderer.GetHoverOverMove(mouseInput: new MoveTrackerRendererMouse(mouse: mouseInput, coordinates: coordinates), isMobileDisplayType: isMobileDisplayType);
				this.moveTrackerRenderer = this.moveTrackerRenderer.ProcessFrame(moveTracker: this.moveTracker, hoverPositionIndex: this.moveTrackerRendererPositionIndex, elapsedMicrosPerFrame: elapsedMicrosPerFrame);
				
				if (this.moveTrackerRendererPositionIndex.HasValue)
				{
					if (originalMoveTrackerRendererPositionIndex == null || this.moveTrackerRendererPositionIndex.Value != originalMoveTrackerRendererPositionIndex.Value)
					{
						MoveTracker.MoveInfo moveTrackerMoveInfo = MoveTrackerRenderer.GetMoveInfoForHover(positionIndex: this.moveTrackerRendererPositionIndex.Value, moveTracker: this.moveTracker, isMobileDisplayType: isMobileDisplayType);
						if (moveTrackerMoveInfo != null)
							soundOutput.PlaySound(sound: GameSound.Woosh);
					}
				}
				
				return new Result(
					gameStatus: this.gameStatus,
					completedObjectives: new List<Objective>(),
					isPlayerWhite: this.gameState.IsPlayerWhite,
					isFinalBattle: this.isFinalBattle);
			}

			if (this.aiPondering != null)
				this.aiPondering.CalculateBestMove(millisecondsToThink: 5);
						
			bool hasPlayerJustMoved = false;

			List<Objective> completedObjectives = new List<Objective>();

			if (this.isNukeInFlight == false && this.isNukeInProgress)
			{
				if (this.chessPiecesRenderer.HasNukeFinished())
					this.isNukeInProgress = false;
			}

			if (this.isNukeInFlight)
			{
				if (this.isNukeLiftingOff)
				{
					this.nukeLaunchSoundCooldown -= elapsedMicrosPerFrame;

					if (this.nukeLaunchSoundCooldown <= 0)
					{
						this.nukeLaunchSoundCooldown += 200 * 1000;
						if (this.nukeLaunchSoundCooldown <= 0)
							this.nukeLaunchSoundCooldown = 0;

						soundOutput.PlaySound(sound: GameSound.NukeLaunch);
					}
				}

				if (this.isNukeLiftingOff && this.nukeRenderer.HasNukeFlownOffScreen())
				{
					this.isNukeLiftingOff = false;
					this.chessPiecesRenderer = this.chessPiecesRenderer.LandNuke(
						nukeCenter: new ChessSquare(this.nukeMove.EndingFile, this.nukeMove.EndingRank));
				}

				if (this.chessPiecesRenderer.HasNukeLanded())
				{
					this.isNukeInFlight = false;

					this.moveTracker = this.moveTracker.AddMove(originalGameState: this.gameState, move: this.nukeMove.Move, timer: this.globalState.Timer);
					HashSet<Objective> newlyCompletedObjectives = ObjectiveChecker.GetCompletedObjectives(originalGameState: this.gameState, move: this.nukeMove.Move, isFinalBattle: this.isFinalBattle);
					foreach (Objective objective in newlyCompletedObjectives)
						completedObjectives.Add(objective);
					this.chessPiecesRendererPieceAnimation = this.chessPiecesRendererPieceAnimation.AddMove(originalGameState: this.gameState, displayMove: this.nukeMove, shouldMoveBeInstant: true);
					this.gameState = MoveImplementation.ApplyMove(gameState: this.gameState, move: this.nukeMove.Move);
					ComputeMoves.Result result = ComputeMoves.GetMoves(gameState: this.gameState);
					this.gameStatus = result.GameStatus;
					this.possibleMoves = DisplayMove.GetDisplayMoves(moves: result.Moves, gameState: this.gameState);
					this.nukeMove = null;

					soundOutput.PlaySound(sound: GameSound.NukeExplosion);

					if (this.clickedSquare != null)
					{
						ChessSquarePiece piece = this.gameState.Board.GetPiece(this.clickedSquare);
						if (piece == ChessSquarePiece.Empty || piece.IsWhite() != this.gameState.IsPlayerWhite)
							this.clickedSquare = null;
					}
				}
			}
			else if (!this.gameState.IsPlayerTurn())
			{
				if (!this.isNukeInProgress)
				{
					if (this.chessAI == null)
					{
						if (this.aiPondering != null)
							this.chessAI = this.aiPondering.GetAIForGameState(newGameState: this.gameState);
						else
							this.chessAI = new CompositeAI(gameState: this.gameState, timer: this.globalState.Timer, random: this.globalState.Rng, logger: this.globalState.Logger, useDebugAI: this.globalState.UseDebugAI);
						this.aiPondering = null;
						this.aiElapsedTimeThinking = 0;
					}

					this.aiElapsedTimeThinking = this.aiElapsedTimeThinking + elapsedMicrosPerFrame;

					int amountOfTimeElapsedMillis = this.aiElapsedTimeThinking / 1000;

					if (!this.chessAI.HasFinishedCalculation() && this.chessAI.GetDepthOfBestMoveFoundSoFar() < 4 && amountOfTimeElapsedMillis > 250)
						this.chessAI.CalculateBestMove(millisecondsToThink: 10);

					bool shouldAIMove;
					if (this.globalState.UseDebugAI)
						shouldAIMove = this.chessAI.HasFinishedCalculation() && amountOfTimeElapsedMillis > 10 || amountOfTimeElapsedMillis > 1500;
					else
						shouldAIMove = this.chessAI.HasFinishedCalculation() && amountOfTimeElapsedMillis > 500 || amountOfTimeElapsedMillis > 1500;

					if (shouldAIMove)
					{
						Move move = this.chessAI.GetBestMoveFoundSoFar();
						this.moveTracker = this.moveTracker.AddMove(originalGameState: this.gameState, move: move, timer: this.globalState.Timer);
						HashSet<Objective> newlyCompletedObjectives = ObjectiveChecker.GetCompletedObjectives(originalGameState: this.gameState, move: move, isFinalBattle: this.isFinalBattle);
						foreach (Objective objective in newlyCompletedObjectives)
							completedObjectives.Add(objective);
						this.chessPiecesRendererPieceAnimation = this.chessPiecesRendererPieceAnimation.AddMove(originalGameState: this.gameState, move: move, shouldMoveBeInstant: false);
						this.gameState = MoveImplementation.ApplyMove(gameState: this.gameState, move: move);
						ComputeMoves.Result result = ComputeMoves.GetMoves(gameState: this.gameState);
						this.gameStatus = result.GameStatus;
						this.possibleMoves = DisplayMove.GetDisplayMoves(moves: result.Moves, gameState: this.gameState);

						soundOutput.PlaySound(sound: GameSound.AIMove);

						if (this.clickedSquare != null)
						{
							ChessSquarePiece piece = this.gameState.Board.GetPiece(this.clickedSquare);
							if (piece == ChessSquarePiece.Empty || piece.IsWhite() != this.gameState.IsPlayerWhite)
								this.clickedSquare = null;
						}
						
						this.chessAI = null;
						this.aiPondering = new AIPondering(gameState: this.gameState, timer: this.globalState.Timer, random: this.globalState.Rng, logger: this.globalState.Logger, useDebugAI: this.globalState.UseDebugAI);
					}
				}
			}
			else
			{
				PlayerMoveInfo playerMoveInfo = GetPlayerMove(
					mouseInput: mouseInput,
					previousMouseInput: previousMouseInput,
					isPromotionPanelOpen: this.isPromotionPanelOpen,
					clickedPromotionPiece: this.clickedPromotionPiece,
					possibleMoves: this.possibleMoves,
					promotionMoves: this.promotionMoves,
					isPlayerWhite: this.gameState.IsPlayerWhite,
					isNukeInFlight: this.isNukeInFlight,
					promotionPanelX: this.promotionPanelX,
					promotionPanelY: this.promotionPanelY,
					clickedSquare: this.clickedSquare,
					clickedAndHeldSquare: this.clickedAndHeldSquare,
					hasClickedOnNuke: this.hasClickedOnNuke,
					hasClickedAndHeldOnNuke: this.hasClickedAndHeldOnNuke,
					displayProcessing: displayProcessing,
					isMobileDisplayType: isMobileDisplayType,
					coordinates: coordinates);

				DisplayMove playerMove = playerMoveInfo.DisplayMove;

				if (playerMove != null)
				{
					if (playerMove.IsNuke)
					{
						this.isNukeInProgress = true;
						this.isNukeInFlight = true;
						this.isNukeLiftingOff = true;
						this.nukeMove = playerMove;
						this.nukeRenderer = this.nukeRenderer.LaunchNuke();
					}
					else
					{
						this.moveTracker = this.moveTracker.AddMove(originalGameState: this.gameState, move: playerMove.Move, timer: this.globalState.Timer);
						HashSet<Objective> newlyCompletedObjectives = ObjectiveChecker.GetCompletedObjectives(originalGameState: this.gameState, move: playerMove.Move, isFinalBattle: this.isFinalBattle);
						foreach (Objective objective in newlyCompletedObjectives)
							completedObjectives.Add(objective);
						this.chessPiecesRendererPieceAnimation = this.chessPiecesRendererPieceAnimation.AddMove(originalGameState: this.gameState, displayMove: playerMove, shouldMoveBeInstant: playerMoveInfo.ShouldMoveBeInstant);
						this.gameState = MoveImplementation.ApplyMove(gameState: this.gameState, displayMove: playerMove);
						ComputeMoves.Result result = ComputeMoves.GetMoves(gameState: this.gameState);
						this.gameStatus = result.GameStatus;
						this.possibleMoves = DisplayMove.GetDisplayMoves(moves: result.Moves, gameState: this.gameState);
						hasPlayerJustMoved = true;
						this.clickedSquare = null;
						this.clickedAndHeldSquare = null;
						this.isNukeInProgress = false;
						this.isNukeLiftingOff = false;
						this.isNukeInFlight = false;
						this.nukeMove = null;
						this.hasClickedOnNuke = false;
						this.hasClickedAndHeldOnNuke = false;
						this.isPromotionPanelOpen = false;
						this.clickedPromotionPiece = null;
						this.promotionMoves = null;
						this.chessAI = null;

						soundOutput.PlaySound(sound: GameSound.PlayerMove);
					}
				}
			}

			if (!hasPlayerJustMoved)
			{
				ChessSquare newClickedSquare = GetClickedSquare(
					mouseInput: mouseInput,
					previousMouseInput: previousMouseInput,
					board: this.gameState.Board,
					isPlayerWhite: this.gameState.IsPlayerWhite,
					displayProcessing: displayProcessing,
					possibleMoves: this.possibleMoves,
					clickedSquare: this.clickedSquare,
					clickedAndHeldSquare: this.clickedAndHeldSquare,
					hasNukeAbility: this.gameState.Abilities.HasTacticalNuke,
					hasUsedNuke: this.gameState.HasUsedNuke,
					isNukeInFlight: this.isNukeInFlight,
					coordinates: coordinates);

				ChessSquare newClickedAndHeldSquare = GetClickedAndHeldSquare(
					mouseInput: mouseInput,
					previousMouseInput: previousMouseInput,
					isPlayerWhite: this.gameState.IsPlayerWhite,
					displayProcessing: displayProcessing,
					clickedAndHeldSquare: this.clickedAndHeldSquare,
					isPromotionPanelOpen: this.isPromotionPanelOpen,
					promotionPanelX: this.promotionPanelX,
					promotionPanelY: this.promotionPanelY,
					coordinates: coordinates,
					isMobileDisplayType: isMobileDisplayType);

				PromotionPanelInfo promotionPanelInfo = GetPromotionPanelInfo(
					isPromotionPanelOpen: this.isPromotionPanelOpen,
					promotionPanelX: this.promotionPanelX,
					promotionPanelY: this.promotionPanelY,
					promotionMoves: this.promotionMoves,
					mouseInput: mouseInput,
					previousMouseInput: previousMouseInput,
					clickedSquare: this.clickedSquare,
					clickedAndHeldSquare: this.clickedAndHeldSquare,
					isPlayerWhite: this.gameState.IsPlayerWhite,
					displayProcessing: displayProcessing,
					possibleMoves: this.possibleMoves,
					isNukeInFlight: this.isNukeInFlight,
					hasClickedOnNuke: this.hasClickedOnNuke,
					coordinates: coordinates,
					isMobileDisplayType: isMobileDisplayType);

				Move.PromotionType? newClickedPromotionPiece = GetClickedPromotionPiece(
					isPromotionPanelOpen: this.isPromotionPanelOpen,
					promotionPanelX: this.promotionPanelX,
					promotionPanelY: this.promotionPanelY,
					mouseInput: mouseInput,
					previousMouseInput: previousMouseInput,
					clickedPromotionPiece: this.clickedPromotionPiece,
					isMobileDisplayType: isMobileDisplayType,
					displayProcessing: displayProcessing);

				bool newHasClickedOnNuke = GetHasClickedOnNuke(
					board: this.gameState.Board,
					possibleMoves: this.possibleMoves,
					isPromotionPanelOpen: this.isPromotionPanelOpen,
					mouseInput: mouseInput,
					previousMouseInput: previousMouseInput,
					hasNukeAbility: this.gameState.Abilities.HasTacticalNuke,
					hasUsedNuke: this.gameState.HasUsedNuke,
					isNukeInFlight: this.isNukeInFlight,
					hasClickedOnNuke: this.hasClickedOnNuke,
					hasClickedAndHeldOnNuke: this.hasClickedAndHeldOnNuke,
					promotionPanelX: this.promotionPanelX,
					promotionPanelY: this.promotionPanelY,
					isPlayerWhite: this.gameState.IsPlayerWhite,
					displayProcessing: displayProcessing,
					coordinates: coordinates,
					isMobileDisplayType: isMobileDisplayType);

				bool newHasClickedAndHeldOnNuke = GetHasClickedAndHeldOnNuke(
					mouseInput: mouseInput,
					previousMouseInput: previousMouseInput,
					isPromotionPanelOpen: this.isPromotionPanelOpen,
					promotionPanelX: this.promotionPanelX,
					promotionPanelY: this.promotionPanelY,
					hasClickedAndHeldOnNuke: this.hasClickedAndHeldOnNuke,
					hasNukeAbility: this.gameState.Abilities.HasTacticalNuke,
					hasUsedNuke: this.gameState.HasUsedNuke,
					isNukeInFlight: this.isNukeInFlight,
					coordinates: coordinates,
					isMobileDisplayType: isMobileDisplayType);

				this.clickedSquare = newClickedSquare;
				this.clickedAndHeldSquare = newClickedAndHeldSquare;
				this.isPromotionPanelOpen = promotionPanelInfo.IsPromotionPanelOpen;
				this.promotionPanelX = promotionPanelInfo.PromotionPanelX;
				this.promotionPanelY = promotionPanelInfo.PromotionPanelY;
				this.promotionMoves = promotionPanelInfo.PromotionMoves;
				this.clickedPromotionPiece = newClickedPromotionPiece;
				this.hasClickedOnNuke = newHasClickedOnNuke;
				this.hasClickedAndHeldOnNuke = newHasClickedAndHeldOnNuke;
			}

			this.chessPiecesRenderer = UpdateChessPiecesRenderer(
				chessPiecesRenderer: this.chessPiecesRenderer,
				gameState: this.gameState,
				isPlayerWhite: this.gameState.IsPlayerWhite,
				isPlayerTurn: this.gameState.IsPlayerTurn(),
				clickedSquare: this.clickedSquare,
				clickedAndHeldSquare: this.clickedAndHeldSquare,
				moves: this.possibleMoves,
				mouseInput: mouseInput,
				displayProcessing: displayProcessing,
				mostRecentMove: this.moveTracker.GetMostRecentMove(),
				isPromotionPanelOpen: this.isPromotionPanelOpen,
				promotionPanelX: this.promotionPanelX,
				promotionPanelY: this.promotionPanelY,
				elapsedMicrosPerFrame: elapsedMicrosPerFrame,
				hasClickedOnNuke: this.hasClickedOnNuke,
				hasClickedAndHeldOnNuke: this.hasClickedAndHeldOnNuke,
				isNukeInFlight: this.isNukeInFlight,
				turnCount: this.gameState.TurnCount,
				coordinates: coordinates,
				displayType: displayType);

			this.chessPiecesRendererPieceAnimation = this.chessPiecesRendererPieceAnimation.ProcessFrame(elapsedMicrosPerFrame: elapsedMicrosPerFrame);

			Move.PromotionType? promotionPanelHoverSquare = PromotionPanel.IsHoverOverSquare(
				promotionPanelX: this.promotionPanelX,
				promotionPanelY: this.promotionPanelY,
				mouse: mouseInput,
				displayProcessing: displayProcessing,
				isMobileDisplayType: isMobileDisplayType);
			this.promotionPanel = this.promotionPanel.ProcessFrame(
				isOpen: this.isPromotionPanelOpen,
				x: this.promotionPanelX,
				y: this.promotionPanelY,
				hoverSquare: promotionPanelHoverSquare,
				selectedSquare: this.clickedPromotionPiece);

			this.nukeRenderer = this.nukeRenderer.ProcessFrame(
				hasUsedNuke: this.gameState.HasUsedNuke,
				isNukeSelected: this.hasClickedOnNuke || this.hasClickedAndHeldOnNuke,
				isHoverOverNuke: NukeRenderer.IsHoverOverNuke(mouse: new NukeRendererMouse(mouse: mouseInput, coordinates: coordinates), scalingFactorScaled: coordinates.NukeRendererScalingFactorScaled)
					? new Tuple<int, int>((new NukeRendererMouse(mouse: mouseInput, coordinates: coordinates)).GetX(), (new NukeRendererMouse(mouse: mouseInput, coordinates: coordinates)).GetY())
					: null,
				turnCount: this.gameState.TurnCount,
				elapsedMicrosPerFrame: elapsedMicrosPerFrame,
				mouseInput: new NukeRendererMouse(mouse: mouseInput, coordinates: coordinates));

			bool isHoverOverPromotionPanel = this.isPromotionPanelOpen && PromotionPanel.IsHoverOverPanel(
				promotionPanelX: this.promotionPanelX,
				promotionPanelY: this.promotionPanelY,
				mouse: mouseInput,
				isMobileDisplayType: isMobileDisplayType);

			int? oldMoveTrackerRendererPositionIndex = this.moveTrackerRendererPositionIndex;

			this.moveTrackerRendererPositionIndex = isHoverOverPromotionPanel ? null : MoveTrackerRenderer.GetHoverOverMove(mouseInput: new MoveTrackerRendererMouse(mouse: mouseInput, coordinates: coordinates), isMobileDisplayType: isMobileDisplayType);
			this.moveTrackerRenderer = this.moveTrackerRenderer.ProcessFrame(moveTracker: this.moveTracker, hoverPositionIndex: this.moveTrackerRendererPositionIndex, elapsedMicrosPerFrame: elapsedMicrosPerFrame);

			if (this.moveTrackerRendererPositionIndex.HasValue)
			{
				if (oldMoveTrackerRendererPositionIndex == null || this.moveTrackerRendererPositionIndex.Value != oldMoveTrackerRendererPositionIndex.Value)
				{
					MoveTracker.MoveInfo moveTrackerMoveInfo = MoveTrackerRenderer.GetMoveInfoForHover(positionIndex: this.moveTrackerRendererPositionIndex.Value, moveTracker: this.moveTracker, isMobileDisplayType: isMobileDisplayType);
					if (moveTrackerMoveInfo != null)
						soundOutput.PlaySound(sound: GameSound.Woosh);
				}
			}

			return new Result(
				gameStatus: this.gameStatus,
				completedObjectives: new List<Objective>(completedObjectives),
				isPlayerWhite: this.gameState.IsPlayerWhite,
				isFinalBattle: this.isFinalBattle);
		}

		private static ChessPiecesRenderer UpdateChessPiecesRenderer(
			ChessPiecesRenderer chessPiecesRenderer,
			GameState gameState,
			int turnCount,
			bool isPlayerWhite,
			bool isPlayerTurn,
			ChessSquare clickedSquare,
			ChessSquare clickedAndHeldSquare,
			List<DisplayMove> moves,
			IMouse mouseInput,
			IDisplayProcessing<GameImage> displayProcessing,
			MoveTracker.MoveInfo mostRecentMove,
			bool isPromotionPanelOpen,
			int promotionPanelX,
			int promotionPanelY,
			int elapsedMicrosPerFrame,
			bool hasClickedOnNuke,
			bool hasClickedAndHeldOnNuke,
			bool isNukeInFlight,
			Coordinates coordinates,
			DisplayType displayType)
		{
			ChessSquare selectedPiece;
			if (clickedSquare != null)
				selectedPiece = clickedSquare;
			else if (clickedAndHeldSquare != null)
			{
				ChessSquarePiece piece = gameState.Board.GetPiece(clickedAndHeldSquare);
				if (piece != ChessSquarePiece.Empty && piece.IsWhite() == isPlayerWhite)
					selectedPiece = clickedAndHeldSquare;
				else
					selectedPiece = null;
			}
			else
				selectedPiece = null;

			HashSet<ChessSquare> possibleMoves = new HashSet<ChessSquare>();
			if (isPlayerTurn && !isNukeInFlight)
			{
				if (clickedSquare != null)
				{
					var movesAtThisSquare = moves.Where(m => !m.IsNuke
						&& m.StartingFile.Value == clickedSquare.File
						&& m.StartingRank.Value == clickedSquare.Rank).ToList();

					foreach (var move in movesAtThisSquare)
						possibleMoves.Add(new ChessSquare(move.EndingFile, move.EndingRank));
				}
				else if (clickedAndHeldSquare != null)
				{
					var movesAtThisSquare = moves.Where(m => !m.IsNuke
						&& m.StartingFile.Value == clickedAndHeldSquare.File
						&& m.StartingRank.Value == clickedAndHeldSquare.Rank).ToList();

					foreach (var move in movesAtThisSquare)
						possibleMoves.Add(new ChessSquare(move.EndingFile, move.EndingRank));
				}
			}

			ChessSquare hoverSquare = ChessPiecesRenderer.GetHoverSquare(
				mouseInput: new ChessPiecesRendererMouse(mouse: mouseInput, coordinates: coordinates),
				renderFromWhitePerspective: isPlayerWhite,
				chessPieceScalingFactor: coordinates.ChessPieceScalingFactor,
				displayProcessing: displayProcessing);
			bool isHoverOverPanel = PromotionPanel.IsHoverOverPanel(promotionPanelX: promotionPanelX, promotionPanelY: promotionPanelY, mouse: mouseInput, isMobileDisplayType: displayType != DisplayType.Desktop);

			ChessPiecesRenderer.HoverPieceInfo hoverPiece = null;
			if (!hasClickedOnNuke)
			{
				if (clickedSquare == null && clickedAndHeldSquare != null)
				{
					ChessSquarePiece piece = gameState.Board.GetPiece(clickedAndHeldSquare);
					if (piece != ChessSquarePiece.Empty && piece.IsWhite() == isPlayerWhite)
						hoverPiece = new ChessPiecesRenderer.HoverPieceInfo(
							chessSquarePiece: piece,
							x: (new ChessPiecesRendererMouse(mouse: mouseInput, coordinates: coordinates)).GetX(),
							y: (new ChessPiecesRendererMouse(mouse: mouseInput, coordinates: coordinates)).GetY());
				}
				else if (clickedSquare != null && clickedAndHeldSquare != null && clickedSquare.Equals(clickedAndHeldSquare))
				{
					ChessSquarePiece piece = gameState.Board.GetPiece(clickedAndHeldSquare);
					if (piece != ChessSquarePiece.Empty && piece.IsWhite() == isPlayerWhite)
						hoverPiece = new ChessPiecesRenderer.HoverPieceInfo(
							chessSquarePiece: piece,
							x: (new ChessPiecesRendererMouse(mouse: mouseInput, coordinates: coordinates)).GetX(),
							y: (new ChessPiecesRendererMouse(mouse: mouseInput, coordinates: coordinates)).GetY());
				}
			}

			ChessPiecesRenderer.PotentialNukeSquaresInfo potentialNukeSquaresInfo = null;
			if ((hasClickedOnNuke || hasClickedAndHeldOnNuke) && hoverSquare != null && turnCount > TacticalNukeUtil.NumberOfMovesPlayedBeforeNukeIsAvailable && isPlayerTurn)
			{
				if (displayType == DisplayType.Desktop || mouseInput.IsLeftMouseButtonPressed())
				{
					bool isValidSquare = moves.Any(x => x.IsNuke && x.EndingFile == hoverSquare.File && x.EndingRank == hoverSquare.Rank);
					potentialNukeSquaresInfo = new ChessPiecesRenderer.PotentialNukeSquaresInfo(nukeCenter: hoverSquare, isNukeLocationValid: isValidSquare);
				}
			}

			return chessPiecesRenderer.ProcessFrame(
				pieces: gameState.Board,
				kingInDangerSquare: ChessPiecesRendererUtil.GetKingInDangerSquare(gameState: gameState),
				previousMoveSquares: mostRecentMove != null
					? ChessPiecesRendererUtil.GetPreviousMoveSquares(originalGameState: mostRecentMove.OriginalGameState, move: mostRecentMove.Move)
					: DTImmutableList<ChessSquare>.EmptyList(),
				selectedPieceSquare: selectedPiece,
				possibleMoveSquares: new DTImmutableList<ChessSquare>(possibleMoves),
				potentialNukeSquaresInfo: potentialNukeSquaresInfo,
				hoverSquare: (!isPromotionPanelOpen || !isHoverOverPanel) && (displayType == DisplayType.Desktop || mouseInput.IsLeftMouseButtonPressed()) ? hoverSquare : null,
				hoverPieceInfo: hoverPiece,
				elapsedMicrosPerFrame: elapsedMicrosPerFrame);
		}

		public void Render(
			IDisplayOutput<GameImage, GameFont> displayOutput,
			bool isMobileDisplayType)
		{
			DisplayType displayType;

			if (!isMobileDisplayType)
				displayType = DisplayType.Desktop;
			else if (displayOutput.IsMobileInLandscapeOrientation())
				displayType = DisplayType.MobileLandscape;
			else
				displayType = DisplayType.MobilePortrait;

			Coordinates coordinates = new Coordinates(displayType: displayType);

			this.moveTrackerRenderer.Render(
				displayOutput: new TranslatedDisplayOutput<GameImage, GameFont>(displayOutput, coordinates.MoveTrackerRendererX, coordinates.MoveTrackerRendererY),
				isMobileDisplayType: isMobileDisplayType);

			MoveTracker.MoveInfo moveInfo;

			if (this.moveTrackerRendererPositionIndex.HasValue)
				moveInfo = MoveTrackerRenderer.GetMoveInfoForHover(positionIndex: this.moveTrackerRendererPositionIndex.Value, moveTracker: this.moveTracker, isMobileDisplayType: isMobileDisplayType);
			else
				moveInfo = null;

			int nukeRendererEndingY;
			int nukeRendererScalingFactorScaled;

			switch (displayType)
			{
				case DisplayType.Desktop:
					nukeRendererEndingY = GlobalConstants.DESKTOP_WINDOW_HEIGHT + displayOutput.GetHeight(GameImage.Nuke_RocketFire) * 2 * coordinates.NukeRendererScalingFactorScaled / 128;
					nukeRendererScalingFactorScaled = coordinates.NukeRendererScalingFactorScaled;
					break;
				case DisplayType.MobileLandscape:
					nukeRendererEndingY = displayOutput.GetMobileScreenHeight() + displayOutput.GetHeight(GameImage.Nuke_RocketFire) * 2 * coordinates.NukeRendererScalingFactorScaled / 128;
					nukeRendererScalingFactorScaled = coordinates.NukeRendererScalingFactorScaled;
					break;
				case DisplayType.MobilePortrait:
					nukeRendererEndingY = displayOutput.GetMobileScreenHeight() + displayOutput.GetHeight(GameImage.Nuke_RocketFire) * 2 * coordinates.NukeRendererScalingFactorScaled / 128;
					nukeRendererScalingFactorScaled = coordinates.NukeRendererScalingFactorScaled;
					break;
				default:
					throw new Exception();
			}

			if (moveInfo == null || moveInfo.NewGameState.TurnCount == this.gameState.TurnCount)
			{
				this.chessPiecesRenderer.Render(
					displayOutput: new TranslatedDisplayOutput<GameImage, GameFont>(displayOutput, coordinates.ChessPiecesRendererX, coordinates.ChessPiecesRendererY),
					chessPiecesRendererPieceAnimation: this.chessPiecesRendererPieceAnimation,
					chessPieceScalingFactor: coordinates.ChessPieceScalingFactor,
					isMobileDisplayType: isMobileDisplayType);
				this.nukeRenderer.Render(
					endingY: nukeRendererEndingY,
					scalingFactorScaled: nukeRendererScalingFactorScaled,
					displayOutput: new TranslatedDisplayOutput<GameImage, GameFont>(displayOutput, coordinates.NukeRendererX, coordinates.NukeRendererY),
					isMobileDisplayType: isMobileDisplayType);
				this.promotionPanel.Render(displayOutput: displayOutput, isMobileDisplayType: isMobileDisplayType); // must render after moveTrackerRenderer to ensure panel is on top
			}
			else
			{
				moveInfo.NewStateChessPiecesRenderer.Render(
					displayOutput: new TranslatedDisplayOutput<GameImage, GameFont>(displayOutput, coordinates.ChessPiecesRendererX, coordinates.ChessPiecesRendererY), 
					chessPiecesRendererPieceAnimation: ChessPiecesRendererPieceAnimation.EmptyChessPiecesRendererPieceAnimation(),
					chessPieceScalingFactor: coordinates.ChessPieceScalingFactor,
					isMobileDisplayType: isMobileDisplayType);
				moveInfo.NewStateNukeRenderer.Render(
					endingY: nukeRendererEndingY,
					scalingFactorScaled: nukeRendererScalingFactorScaled,
					displayOutput: new TranslatedDisplayOutput<GameImage, GameFont>(displayOutput, coordinates.NukeRendererX, coordinates.NukeRendererY),
					isMobileDisplayType: isMobileDisplayType);
			}
		}
	}
}
