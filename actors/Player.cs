using Godot;

/// <summary>
/// template
/// </summary>
public class Player : Node {
  // Signals

  // Exports

  // Public Fields

  // Backing Fields

  // Private Fields

  // Constructor

  // Lifecycle Hooks
  public override void _Ready() {
    PlayerFlightController temp = GetOwner<PlayerFlightController>();

    if (temp != null) {
      temp.ActivePlayer = true;
    }
  }

  // Public Functions

  // Private Functions
}