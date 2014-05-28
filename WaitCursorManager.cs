using UnityEngine;
using Com.YouRehab;
using SpiroTiger;

/// <summary>
/// Changes the cursor during the loading of a level.
/// </summary>
/// 
/// \version 1.0.0
/// \date 2014-02-20
/// \author Kaspar Manz kaspar.manz@yourehab.com
public class WaitCursorManager : MonoBehaviour
{
  public Texture2D CursorTexture;
  readonly CursorMode mode = CursorMode.Auto;
  Vector2 center;

  void Awake ()
  {
    DontDestroyOnLoad (this);
    center = new Vector2 (CursorTexture.width / 2, CursorTexture.height / 2);
  }

  void Start ()
  {
    EventsBroadcaster.Instance.GameStateChanged += HandleGameStateChanged;
  }

  void HandleGameStateChanged (object sender, GameStateEventArgs e)
  {
    switch (e.NewState) {
    case GameState.LevelLoadingStarts:
      Cursor.SetCursor (CursorTexture, center, mode);
      break;

    case GameState.LevelLoadingComplete:
      Cursor.SetCursor (null, Vector2.zero, mode);
      break;
    }
  }

  void OnDestroy ()
  {
    EventsBroadcaster.Instance.GameStateChanged -= HandleGameStateChanged;
  }
}

