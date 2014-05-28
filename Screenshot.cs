using UnityEngine;
using System;

namespace Net.Xeophin.Utils
{
  public class Screenshot : MonoBehaviour
  {
    // Use this for initialization
    void Start ()
    {
    
    }
    // Update is called once per frame
    void Update ()
    {
      if (Input.GetKeyUp (KeyCode.F1)) {
        Application.CaptureScreenshot (string.Format ("{0}.png", DateTime.Now.ToString ("yyyyMMdd-HHmmss")), 2);
        Debug.Log ("Take Screenshot");
      }
    }
  }
}
