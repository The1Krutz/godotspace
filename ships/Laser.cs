using System;
using Godot;

/// <summary>
/// template
/// </summary>
public class Laser : Spatial {
  // Signals

  // Exports

  // Public Fields

  // Backing Fields

  // Private Fields
  private RayCast aimCast;

  // Constructor

  // Lifecycle Hooks
  public override void _Ready() {
    aimCast = GetNode<RayCast>("RayCast");
  }

  public override void _PhysicsProcess(float delta) {
    if (Input.IsActionPressed("fire_primary")) {
      if (aimCast.IsColliding()) {
        Godot.Object target = aimCast.GetCollider();

        GD.Print("hit something!", target);
        // TODO: apply damage
      }
    }

    if (Input.IsActionJustPressed("fire_primary")) {
      ToggleBeam(true);
    }
    if (Input.IsActionJustReleased("fire_primary")) {
      ToggleBeam(false);
    }
  }

  // Public Functions

  // Private Functions
  private void ToggleBeam(bool value) {
    aimCast.Enabled = value;
    // TODO: toggle something to show the beam
  }
}