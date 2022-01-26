using Godot;

/// <summary>
/// template
/// </summary>
public class Executioner : KinematicBody {
  // Signals

  // Exports
  [Export]
  public float MaxSpeed = 50.0f;
  [Export]
  public float Acceleration = 0.6f;

  // Public Fields

  // Backing Fields

  // Private Fields
  private Vector3 velocity = Vector3.Zero;
  private float forwardSpeed;

  // Constructor

  // Lifecycle Hooks
  public override void _PhysicsProcess(float delta) {
    GetInput(delta);
    velocity = Transform.basis.z * forwardSpeed;
    MoveAndCollide(velocity * delta);
  }

  // Public Functions

  // Private Functions
  private void GetInput(float delta) {
    if (Input.IsActionPressed("throttle_up")) {
      forwardSpeed = Mathf.Lerp(forwardSpeed, MaxSpeed, Acceleration * delta);
    }
    if (Input.IsActionPressed("throttle_down")) {
      forwardSpeed = Mathf.Lerp(forwardSpeed, 0, Acceleration * delta);
    }
  }
}