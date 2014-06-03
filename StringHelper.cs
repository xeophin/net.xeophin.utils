
public static class StringHelper
{
  /// <summary>
  /// Capitalizes the first letter.
  /// </summary>
  /// <returns>The string with the first letter capitalised.</returns>
  /// <param name="s">The string to capitalise.</param>
  /// 
  /// <remarks>Based on the code from http://www.dotnetperls.com/uppercase-first-letter.</remarks>
  public static string CapitalizeFirstLetter (this string s)
  {
    if (string.IsNullOrEmpty (s)) {
      return string.Empty;
    }
    char[] a = s.ToCharArray ();
    a [0] = char.ToUpper (a [0]);
    return new string (a);
  }
}

