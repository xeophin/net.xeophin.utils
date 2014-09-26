using NSubstitute;
using NUnit.Framework;
using Net.Xeophin.Utils;
using System;

/// <summary>
/// Events broadcaster tests. Currently not working, more to come.
/// </summary>
[TestFixture]
public class EventsBroadcasterTest
{
  [Test]
  public void TestFadeIn ()
  {
    EventsBroadcaster geb = new EventsBroadcaster ();
    bool wasCalled = false;

    geb.ReadyForFadeIn += (sender, e) => wasCalled = true;

    geb.RaiseGameStateChanged (this, new GameStateEventArgs (GameState.ReadyForFadeIn));
    Assert.That (wasCalled);
  }


  [Test]
  [ExpectedException]
  public void WrongEnum ()
  {
    EventsBroadcaster geb = new EventsBroadcaster ();

    geb.RaiseGameStateChanged (this, new GameStateEventArgs (Enum.Parse (typeof(GameState), "Blubdiblub")));
  }
}
