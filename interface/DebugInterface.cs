using Godot;

/// <summary>
/// template
/// </summary>
public class DebugInterface : Control {
  // Signals

  // Exports

  // Public Fields

  // Backing Fields

  // Private Fields
  private AxisBar roll;

  // Constructor

  // Lifecycle Hooks
  public override void _Ready() {
    roll = GetNode<AxisBar>("BarList/Roll");
  }

  // Public Functions
  public void OnInputUpdate(DegreeOfFreedom axis, float newValue) {
    // GD.Print(axis, newValue);

    if (axis == DegreeOfFreedom.Roll) {
      roll.UpdateBarValue(newValue);
    }

  }

  // Private Functions
}