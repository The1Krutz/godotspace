using System;
using Godot;

/// <summary>
/// template
/// </summary>
public class AxisBar : HBoxContainer {
  // Signals

  // Exports
  [Export(hint: PropertyHint.Enum)]
  public DegreeOfFreedom Axis;

  // Public Fields

  // Backing Fields

  // Private Fields
  private Label barLabel;
  private TextureProgress progressBar;

  // Constructor

  // Lifecycle Hooks
  public override void _Ready() {
    barLabel = GetNode<Label>("Count/Value");
    progressBar = GetNode<TextureProgress>("TextureProgress");

    GetNode<Label>("Count/Axis").Text = Enum.GetName(typeof(DegreeOfFreedom), Axis);
  }

  // Public Functions
  public void UpdateBarValue(float value) {
    progressBar.Value = value;
  }

  public void UpdateTextValue(float value) {
    barLabel.Text = Math.Round(value, 2).ToString();
  }

  // Private Functions
}