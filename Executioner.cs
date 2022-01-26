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
  [Export]
  public float PitchSpeed = 1.5f;
  [Export]
  public float RollSpeed = 1.9f;
  [Export]
  public float YawSpeed = 1.25f;
  [Export]
  public float InputResponse = 8.0f;

  // Public Fields

  // Backing Fields

  // Private Fields
  private Vector3 velocity = Vector3.Zero;
  private float forwardSpeed;
  private float pitchInput;
  private float rollInput;
  private float yawInput;

  // Constructor

  // Lifecycle Hooks
  public override void _PhysicsProcess(float delta) {
    GetInput(delta);

    Transform transform = Transform;
    transform.basis = transform.basis.Rotated(Transform.basis.z, rollInput * RollSpeed * delta);
    transform.basis = transform.basis.Rotated(Transform.basis.x, pitchInput * PitchSpeed * delta);
    transform.basis = transform.basis.Rotated(Transform.basis.y, yawInput * RollSpeed * delta);
    transform.basis = transform.basis.Orthonormalized();
    Transform = transform;

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

    pitchInput = Mathf.Lerp(pitchInput,
                            Input.GetActionStrength("pitch_up") - Input.GetActionStrength("pitch_down"),
                            InputResponse * delta);
    rollInput = Mathf.Lerp(rollInput,
                           Input.GetActionStrength("roll_left") - Input.GetActionStrength("roll_right"),
                           InputResponse * delta);
    yawInput = Mathf.Lerp(yawInput,
                          Input.GetActionStrength("yaw_left") - Input.GetActionStrength("yaw_right"),
                          InputResponse * delta);
  }
}