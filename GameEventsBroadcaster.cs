using System;
using System.Collections.Generic;


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
  /// 
  /// \version 0.3.0
  /// \author Kaspar Manz <code@xeophin.net>
  /// \date 2014-09-26
  /// <remarks>Redesigned class to use a dictionary for collecting all possible
  /// event handlers, so I don't need to use that massive switch statement.
  /// </remarks>
  public class GameEventsBroadcaster<T>:IGameEventProvider where T : GameEventsBroadcaster<T>, new()
  {
    #region Singleton

    static readonly T instance = new T ();


    protected GameEventsBroadcaster ()
    {
      // Setup the game state dictionary with all possible values
      var allStates = (GameState[])Enum.GetValues (typeof(GameState));
      foreach (var item in allStates)
        gameStateEvents.Add (item, null);
    }


    public static T Instance { get { return instance; } }

    #endregion

    /// <summary>
    /// The game state events dictionary 
    /// - this collects all event handlers for the complete enum list.
    /// </summary>
    Dictionary<GameState,EventHandler<GameStateEventArgs>> gameStateEvents = new Dictionary<GameState, EventHandler<GameStateEventArgs>> ();

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

      // Call more specific events
      var handler = gameStateEvents [e.NewState];
      if (handler != null)
        handler (sender, e);

    }

    #endregion


    #region Pausing the Game
    public event EventHandler<GameStateEventArgs> PauseGame {
      add {
        lock (gameStateEvents) {
          gameStateEvents [GameState.EnterPause] += value;
        }
      }
      remove {
        lock (gameStateEvents) {
          gameStateEvents [GameState.EnterPause] -= value;
        }
      }
    }


    public event EventHandler<GameStateEventArgs> UnpauseGame {
      add {
        lock (gameStateEvents) {
          gameStateEvents [GameState.ExitPause] += value;
        }
      }
      remove {
        lock (gameStateEvents) {
          gameStateEvents [GameState.ExitPause] -= value;
        }
      }
    }



    #endregion

    #region Control over the Game
    public event EventHandler<GameStateEventArgs> PlayerReceivesControl {
      add {
        lock (gameStateEvents) {
          gameStateEvents [GameState.PlayerReceivesControl] += value;
        }
      }
      remove {
        lock (gameStateEvents) {
          gameStateEvents [GameState.PlayerReceivesControl] -= value;
        }
      }
    }



    public event EventHandler<GameStateEventArgs> PlayerCedesControl {
      add {
        lock (gameStateEvents) {
          gameStateEvents [GameState.PlayerCedesControl] += value;
        }
      }
      remove {
        lock (gameStateEvents) {
          gameStateEvents [GameState.PlayerCedesControl] -= value;
        }
      }
    }


    #endregion

    #region Setup Complete
    public event EventHandler<GameStateEventArgs> SetupComplete {
      add {
        lock (gameStateEvents) {
          gameStateEvents [GameState.SetupCompleted] += value;
        }
      }
      remove {
        lock (gameStateEvents) {
          gameStateEvents [GameState.SetupCompleted] -= value;
        }
      }
    }



    #endregion

    #region Shutdown
    public event EventHandler<GameStateEventArgs> PrepareShutdown {
      add {

        lock (gameStateEvents) {
          gameStateEvents [GameState.PrepareShutdown] += value;
        }
      }
      remove {
        lock (gameStateEvents) {
          gameStateEvents [GameState.PrepareShutdown] -= value;
        }
      }
    }

    #endregion

    #region Level Loading
    public event EventHandler<GameStateEventArgs> LevelLoadingStarts {
      add {
        Add (GameState.LevelLoadingStarts, value);
      }
      remove {
        Remove (GameState.LevelLoadingStarts, value);
      }
    }



    public event EventHandler<GameStateEventArgs> LevelLoadingComplete {
      add {
        Add (GameState.LevelLoadingComplete, value);
      }
      remove {
        Remove (GameState.LevelLoadingComplete, value);
      }
    }

   
    #endregion

    #region Timer
    public event EventHandler<GameStateEventArgs> TimerStarted {
      add {
        Add (GameState.TimerStarted, value);
      }
      remove {
        Remove (GameState.TimerStarted, value);
      }
    }





    public event EventHandler<GameStateEventArgs> TimerCompleted {
      add {
        Add (GameState.TimerCompleted, value);
      }
      remove {
        Remove (GameState.TimerCompleted, value);
      }
    }



    #endregion

    #region Fades
    public event EventHandler<GameStateEventArgs> FadeOutComplete {
      add {
        lock (gameStateEvents) {
          gameStateEvents [GameState.FadeOutComplete] += value;
        }
      }
      remove {
        lock (gameStateEvents) {
          gameStateEvents [GameState.FadeOutComplete] -= value;
        }
      }
    }


    public event EventHandler<GameStateEventArgs> ReadyForFadeOut {
      add {
        lock (gameStateEvents) {
          gameStateEvents [GameState.ReadyForFadeOut] += value;
        }
      }
      remove {
        lock (gameStateEvents) {
          gameStateEvents [GameState.ReadyForFadeOut] -= value;
        }
      }
    }


    public event EventHandler<GameStateEventArgs> FadeInComplete {
      add {
        lock (gameStateEvents) {
          gameStateEvents [GameState.FadeInComplete] += value;
        }
      }
      remove {
        lock (gameStateEvents) {
          gameStateEvents [GameState.FadeInComplete] -= value;
        }
      }
    }


    public event EventHandler<GameStateEventArgs> ReadyForFadeIn {
      add {
        lock (gameStateEvents) {
          gameStateEvents [GameState.ReadyForFadeIn] += value;
        }
      }
      remove {
        lock (gameStateEvents) {
          gameStateEvents [GameState.ReadyForFadeIn] -= value;
        }
      }
    }

    #endregion

    //TODO More events should come here.

    #region Helper classes
    void Add (GameState state, EventHandler<GameStateEventArgs> handler)
    {
      lock (gameStateEvents) {
        gameStateEvents [state] += handler;
      }
    }


    void Remove (GameState state, EventHandler<GameStateEventArgs> handler)
    {
      lock (gameStateEvents) {
        gameStateEvents [state] -= handler;
      }
    }
    #endregion
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
    /// Execute the actual shutdown - gives another hook to do stuff when shutting down.
    /// </summary>
    Shutdown,
    /// <summary>
    /// The timer started.
    /// </summary>
    TimerStarted,
    /// <summary>
    /// A timer has run out of time.
    /// </summary>
    TimerCompleted
  }


  public interface IGameEventProvider
  {
    event EventHandler<GameStateEventArgs> PrepareShutdown;
  }
}

