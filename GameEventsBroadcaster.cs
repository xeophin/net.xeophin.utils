using System;

namespace Net.Xeophin.Utils
{
  /// <summary>
  /// Class that catches all logging related events and turns them into logging data.
  /// </summary>
  /// 
  /// <remarks>
  /// If necessary, this class can be expanded and adapted to other logging formats.
  /// </remarks>
  public class GameEventsBroadcaster<T> where T : GameEventsBroadcaster<T>, new()
  {
    #region Singleton

    static readonly T instance = new T ();

    protected GameEventsBroadcaster ()
    {
    }

    public static T Instance {
      get {
        return instance; 
      }
    }

    #endregion

    #region Game State Changes

    public event EventHandler<GameStateEventArgs> GameStateChanged;

    /// <summary>
    /// Raises the game state changed event.
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    public virtual void RaiseGameStateChanged (object sender, GameStateEventArgs e)
    {
      GameStateChanged (sender, e);
    }

    #endregion

    public event EventHandler<GameStateEventArgs> PauseGame;

    public virtual void OnPauseGame (GameStateEventArgs e)
    {
      var handler = PauseGame;
      if (handler != null)
        handler (this, e);
    }

    public event EventHandler<GameStateEventArgs> UnpauseGame;

    public virtual void OnUnpauseGame (GameStateEventArgs e)
    {
      var handler = UnpauseGame;
      if (handler != null)
        handler (this, e);
    }
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
    StartPlaytime,
    EndPlaytime,
    /// <summary>
    /// Begin transition to pause.
    /// </summary>
    EnterPause,
    /// <summary>
    /// The game is paused.
    /// </summary>
    GamePaused,
    /// <summary>
    /// Begin transition to end pause.
    /// </summary>
    ExitPause,
    /// <summary>
    /// Scene has finished loading, ready to be faded in.
    /// </summary>
    ReadyForFadeIn,
    /// <summary>
    /// Scene is ready to be faded out.
    /// </summary>
    ReadyForFadeOut,
    /// <summary>
    /// The game is running now.
    /// </summary>
    GameRunning,
    /// <summary>
    /// Level loading starts.
    /// </summary>
    LevelLoadingStarts,
    /// <summary>
    /// Level loading complete.
    /// </summary>
    LevelLoadingComplete
  }
}

