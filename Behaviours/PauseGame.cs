using UnityEngine;
using Net.Xeophin.Utils;

/// <summary>
/// Used to Pause/Unpause the game.
/// </summary>
public class PauseGame : UnitySingleton<PauseGame>
{
  #region Public data

  /// <summary>
  /// The keyboard button to use to pause/unpause the game
  /// </summary>
  public KeyCode PauseButton = KeyCode.Escape;


  /// <summary>
  /// Gets or sets a value indicating whether this instance is paused.
  /// </summary>
  /// <value><c>true</c> if this instance is paused; otherwise, <c>false</c>.</value>
  public bool IsPaused {
    get {
      return isPaused;
    }
    set {
      if (value && !isPaused) {
        EventsBroadcaster.Instance.RaiseGameStateChanged (this, new GameStateEventArgs (GameState.EnterPause));
      } else if (!value && isPaused) {
        EventsBroadcaster.Instance.RaiseGameStateChanged (this, new GameStateEventArgs (GameState.ExitPause));
      }
      isPaused = value;
    }
  }

  #endregion

  #region Private data

  bool isPaused;

  #endregion

  #region MonoBehaviour

  /// <summary>
  /// Checks whether the pause button has been pressed.
  /// </summary>
  void Update ()
  {
    if (Input.GetKeyUp (PauseButton)) {
      IsPaused = !IsPaused;
    }
  }

  #endregion
}

