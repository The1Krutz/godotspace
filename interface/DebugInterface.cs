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
  private AxisBar pitch;
  private AxisBar yaw;
  private AxisBar forward;
  private AxisBar vertical;
  private AxisBar lateral;

  // Constructor

  // Lifecycle Hooks
  public override void _Ready() {
    roll = GetNode<AxisBar>("BarList/Roll");
    pitch = GetNode<AxisBar>("BarList/Pitch");
    yaw = GetNode<AxisBar>("BarList/Yaw");
    forward = GetNode<AxisBar>("BarList/Forward");
    vertical = GetNode<AxisBar>("BarList/Vertical");
    lateral = GetNode<AxisBar>("BarList/Lateral");
  }

  // Public Functions
  public void OnInputUpdate(DegreeOfFreedom axis, float newValue) {
    switch (axis) {
      case DegreeOfFreedom.Roll:
        roll.UpdateBarValue(newValue);
        break;
      case DegreeOfFreedom.Pitch:
        pitch.UpdateBarValue(newValue);
        break;
      case DegreeOfFreedom.Yaw:
        yaw.UpdateBarValue(newValue);
        break;
      case DegreeOfFreedom.Forward:
        forward.UpdateBarValue(newValue);
        break;
      case DegreeOfFreedom.Vertical:
        vertical.UpdateBarValue(newValue);
        break;
      case DegreeOfFreedom.Lateral:
        lateral.UpdateBarValue(newValue);
        break;
    }
  }

  public void OnVelocityUpdate(DegreeOfFreedom axis, float newValue) {
    switch (axis) {
      case DegreeOfFreedom.Roll:
        roll.UpdateTextValue(newValue);
        break;
      case DegreeOfFreedom.Pitch:
        pitch.UpdateTextValue(newValue);
        break;
      case DegreeOfFreedom.Yaw:
        yaw.UpdateTextValue(newValue);
        break;
      case DegreeOfFreedom.Forward:
        forward.UpdateTextValue(newValue);
        break;
      case DegreeOfFreedom.Vertical:
        vertical.UpdateTextValue(newValue);
        break;
      case DegreeOfFreedom.Lateral:
        lateral.UpdateTextValue(newValue);
        break;
    }
  }

  // Private Functions
}