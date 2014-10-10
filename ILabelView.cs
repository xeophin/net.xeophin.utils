using UnityEngine;
using System.Reflection;

namespace Net.Xeophin.Utils
{

  /// <summary>
  /// A simple interface for a view class that provides a string label that can be updated
  /// by a controller class.
  /// </summary>
  public interface ILabelView
  {
    string Label { set; }
  }
}
