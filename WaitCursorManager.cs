using UnityEngine;
using Net.Xeophin.Utils;

namespace Net.Xeophin.Utils
{
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
      EventsBroadcaster.Instance.LevelLoadingStarts += HandleLevelLoadingStarts;
      EventsBroadcaster.Instance.LevelLoadingComplete += HandleLevelLoadingComplete;
    }


    void HandleLevelLoadingComplete (object sender, GameStateEventArgs e)
    {
      Cursor.SetCursor (null, Vector2.zero, mode);
    }


    void HandleLevelLoadingStarts (object sender, GameStateEventArgs e)
    {
      Cursor.SetCursor (CursorTexture, center, mode);
    }



    void OnDestroy ()
    {
      EventsBroadcaster.Instance.LevelLoadingStarts -= HandleLevelLoadingStarts;
      EventsBroadcaster.Instance.LevelLoadingComplete -= HandleLevelLoadingComplete;
    }
  }
}

