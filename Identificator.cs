using System;
using UnityEngine;

/// <summary>
/// Provides a GUID for game objects.
/// </summary>
public class Identificator : MonoBehaviour
{
  /// <summary>
  /// The identifier.
  /// </summary>
  public Guid Id;

  /// <summary>
  /// Create a new GUID on awake.
  /// </summary>
  void Awake ()
  {
    Id = Guid.NewGuid ();
  }
}

