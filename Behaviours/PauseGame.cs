using UnityEngine;
using SpiroTiger;
using RespirationTrackerLib;
using Net.Xeophin.Utils;

public class PauseGame : UnitySingleton<PauseGame>
{
  bool isPaused;

  public bool IsPaused {
    get {
      return isPaused;
    }
    set {
      if (value && !isPaused) {
        EventsBroadcaster.Instance.OnPauseGame (new GameStateEventArgs (this));
        Data.Instance.PauseTraining ();
      } else if (!value && isPaused) {
        EventsBroadcaster.Instance.OnUnpauseGame (new GameStateEventArgs (this));
        Data.Instance.ResumeTraining ();
      }
      isPaused = value;
    }
  }
  // Update is called once per frame
  void Update ()
  {
    if (Input.GetKeyUp (KeyCode.Escape)) {
      IsPaused = !IsPaused;
    }
  }
}

