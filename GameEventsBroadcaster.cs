using System;
using System.Collections;

namespace Net.Xeophin.Utils
{
  /// <summary>
  /// Class serves as a template for a notification hub that can be used within the
  /// game to trigger specific events.
  /// </summary>
  /// 
  /// \version 0.2.0
  /// \author Kaspar Manz <code@xeophin.net>
  /// \date 2014-07-03
  /// 
  public class GameEventsBroadcaster<T> where T : GameEventsBroadcaster<T>, new()
  {
    #region Singleton

    static readonly T instance = new T ();


    protected GameEventsBroadcaster ()
    {
    }


    public static T Instance { get { return instance; } }

    #endregion

    #region Game State Changes
    /// <summary>
    /// Occurs when a general game state change occurs. Catch-all event, use when you
    /// look for several events at once or are not really sure what you're
    /// looking for.
    /// </summary>
    public event EventHandler<GameStateEventArgs> GameStateChanged;


    protected virtual void OnGameStateChanged (object sender, GameStateEventArgs e)
    {
      var handler = GameStateChanged;
      if (handler != null)
        handler (this, e);
    }


    /// <summary>
    /// Raises the game state changed event. This is the main entry point for all
    /// game state event changes.
    /// </summary>
    /// <param name="sender">Sender object.</param>
    /// <param name="e">The event arguments.</param>
    /// <remarks>
    /// This handles all game state changes, so you can just use one function to
    /// call all the events.
    /// 
    /// This method will then call more specific event invocators, so that objects
    /// dependent on those specific events can listen to those, and don't have
    /// to check whether they have to do something themselves.
    /// </remarks>
    public void RaiseGameStateChanged (object sender, GameStateEventArgs e)
    {
      // General Game state event.
      OnGameStateChanged (sender, e);

      // More specific events will go here
      switch (e.NewState) {

      // Pausing the game
      case GameState.EnterPause:
        OnPauseGame (sender, e);
        break;
      case GameState.ExitPause:
        OnUnpauseGame (sender, e);
        break;

      // Getting and ceding control of the game
      case GameState.PlayerReceivesControl:
        OnPlayerReceivesControl (sender, e);
        break;

      case GameState.PlayerCedesControl:
        OnPlayerCedesControl (sender, e);
        break;

      // Setup has completed
      case GameState.SetupCompleted:
        OnSetupComplete (sender, e);
        break;

      // Prepare shutdown
      case GameState.PrepareShutdown:
        OnPrepareShutdown (sender, e);
        break;

      // Level loading
      case GameState.LevelLoadingStarts:
        OnLevelLoadingStarts (sender, e);
        break;

      case GameState.LevelLoadingComplete:
        OnLevelLoadingComplete (sender, e);
        break;

      // Timer functions
      case GameState.TimerStarted:
        OnTimerStarted (sender, e);
        break;

      case GameState.TimerCompleted:
        OnTimerCompleted (sender, e);
        break;
      }
        

    }

    #endregion

    #region Pausing the Game
    public event EventHandler<GameStateEventArgs> PauseGame;


    protected virtual void OnPauseGame (object sender, GameStateEventArgs e)
    {
      var handler = PauseGame;
      if (handler != null)
        handler (sender, e);
    }


    public event EventHandler<GameStateEventArgs> UnpauseGame;


    public virtual void OnUnpauseGame (object sender, GameStateEventArgs e)
    {
      var handler = UnpauseGame;
      if (handler != null)
        handler (sender, e);
    }
    #endregion

    #region Control over the Game
    /// <summary>
    /// Occurs when player receives control over the game.
    /// </summary>
    public event EventHandler<GameStateEventArgs> PlayerReceivesControl;


    protected virtual void OnPlayerReceivesControl (object sender, GameStateEventArgs e)
    {
      var handler = PlayerReceivesControl;
      if (handler != null)
        handler (sender, e);
    }


    /// <summary>
    /// Occurs when player cedes control over the game.
    /// </summary>
    public event EventHandler<GameStateEventArgs> PlayerCedesControl;


    protected virtual void OnPlayerCedesControl (object sender, GameStateEventArgs e)
    {
      var handler = PlayerCedesControl;
      if (handler != null)
        handler (sender, e);
    }
    #endregion

    #region Setup Complete
    public event EventHandler<GameStateEventArgs> SetupComplete;


    protected virtual void OnSetupComplete (object sender, GameStateEventArgs e)
    {
      var handler = SetupComplete;
      if (handler != null)
        handler (sender, e);
    }
    #endregion

    #region Prepare Shutdown
    public event EventHandler<GameStateEventArgs> PrepareShutdown;


    protected virtual void OnPrepareShutdown (object sender, GameStateEventArgs e)
    {
      var handler = PrepareShutdown;
      if (handler != null)
        handler (sender, e);
    }
    #endregion

    #region Level Loading
    public event EventHandler<GameStateEventArgs> LevelLoadingStarts;


    protected virtual void OnLevelLoadingStarts (object sender, GameStateEventArgs e)
    {
      var handler = LevelLoadingStarts;
      if (handler != null)
        handler (sender, e);
    }


    public event EventHandler<GameStateEventArgs> LevelLoadingComplete;


    protected virtual void OnLevelLoadingComplete (object sender, GameStateEventArgs e)
    {
      var handler = LevelLoadingComplete;
      if (handler != null)
        handler (sender, e);
    }
    #endregion

    #region Timer
    public event EventHandler<GameStateEventArgs> TimerStarted;


    protected virtual void OnTimerStarted (object sender, GameStateEventArgs e)
    {
      var handler = TimerStarted;
      if (handler != null)
        handler (sender, e);
    }


    public event EventHandler<GameStateEventArgs> TimerCompleted;


    protected virtual void OnTimerCompleted (object sender, GameStateEventArgs e)
    {
      var handler = TimerCompleted;
      if (handler != null)
        handler (sender, e);
    }
    #endregion

    //TODO More events should come here.
  }



  public class GameStateEventArgs : EventArgs
  {
    public readonly GameState NewState;
    public readonly GameState OldState;
    public readonly object OriginalSender;


    public GameStateEventArgs (object originalSender)
    {
      this.OriginalSender = originalSender;
    }


    public GameStateEventArgs (GameState newState, object originalSender)
    {
      this.NewState = newState;
      this.OriginalSender = originalSender;
    }


    public GameStateEventArgs (GameState newState)
    {
      this.NewState = newState;
    }


    public GameStateEventArgs (GameState newState, GameState oldState)
    {
      this.NewState = newState;
      this.OldState = oldState;
    }
  }


  /// <summary>
  /// Various states the game can be in.
  /// </summary>
  public enum GameState
  {
    /// <summary>
    /// Default (empty) state.
    /// </summary>
    Init,
    /// <summary>
    /// The game loading complete.
    /// </summary>
    GameLoadingComplete,
    /// <summary>
    /// Game entered a menu state.
    /// </summary>
    EnterMenu,
    /// <summary>
    /// Game exited from a menu state.
    /// </summary>
    ExitMenu,
    /// <summary>
    /// Game entered a level scene.
    /// </summary>
    EnterLevel,
    /// <summary>
    /// Game exited from a level scene.
    /// </summary>
    ExitLevel,
    /// <summary>
    /// Begin transition to pause.
    /// </summary>
    EnterPause,
    /// <summary>
    /// Begin transition to end pause.
    /// </summary>
    ExitPause,
    /// <summary>
    /// Scene has finished loading, ready to be faded in.
    /// </summary>
    ReadyForFadeIn,
    /// <summary>
    /// Scene has finished fading in.
    /// </summary>
    FadeInComplete,
    /// <summary>
    /// Scene is ready to be faded out.
    /// </summary>
    ReadyForFadeOut,
    /// <summary>
    /// Scene has faded out.
    /// </summary>
    FadeOutComplete,
    /// <summary>
    /// Level loading starts.
    /// </summary>
    LevelLoadingStarts,
    /// <summary>
    /// Level loading complete.
    /// </summary>
    LevelLoadingComplete,
    /// <summary>
    /// The player receives control over the game.
    /// </summary>
    PlayerReceivesControl,
    /// <summary>
    /// The player cedes control over the game.
    /// </summary>
    ///<remarks>
    /// This can also mean a "Pause" or menu screen, when the player
    /// actually still has control over what happens.
    /// </remarks>
    PlayerCedesControl,
    /// <summary>
    /// The setup of the game has been completed, the settings should
    /// now be finalised and actual gameplay will begin shortly.
    /// </summary>
    SetupCompleted,
    /// <summary>
    /// The player has indicated that they want to quit the game. All
    /// data should be cleaned up, saved to the appropriate places and
    /// the game prepared for shutdown.
    /// </summary>
    PrepareShutdown,
    /// <summary>
    /// The timer started.
    /// </summary>
    TimerStarted,
    /// <summary>
    /// A timer has run out of time.
    /// </summary>
    TimerCompleted
  }
}

