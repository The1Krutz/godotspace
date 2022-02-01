using Godot;

/// <summary>
/// template
/// </summary>
public class Insurgent : RigidBody {
  // Signals

  // Exports
  [Export]
  public bool Active;
  [Export]
  public float MaxSpeed = 50.0f;
  [Export]
  public float Acceleration = 0.6f;
  [Export]
  public float PitchSpeed = 31.0f;
  [Export]
  public float RollSpeed = 7.5f;
  [Export]
  public float YawSpeed = 20.0f;

  // Public Fields

  // Backing Fields

  // Private Fields
  private float throttle;
  private float pitchInput;
  private float rollInput;
  private float yawInput;

  // Constructor

  // Lifecycle Hooks
  public override void _Ready() {
    // Physics sleep for rigidbodies is an important performance optimization. Disable it sparingly.
    CanSleep = false;
  }

  public override void _IntegrateForces(PhysicsDirectBodyState state) {
    GetInput(state.Step);

    LinearVelocity = Transform.basis.z * throttle;
    AddTorque(-Transform.basis.z * rollInput * RollSpeed);
    AddTorque(Transform.basis.x * pitchInput * PitchSpeed);
    AddTorque(Transform.basis.y * yawInput * YawSpeed);
  }

  public override void _PhysicsProcess(float delta) {
    base._PhysicsProcess(delta);
    GD.Print(Mode, Sleeping);
  }

  // Public Functions

  // Private Functions
  private void GetInput(float delta) {
    if (!Active) {
      return;
    }

    if (Input.IsActionPressed("throttle_up")) {
      throttle = Mathf.Lerp(throttle, MaxSpeed, Acceleration * delta);
    }
    if (Input.IsActionPressed("throttle_down")) {
      throttle = Mathf.Lerp(throttle, 0, Acceleration * delta);
    }

    pitchInput = Input.GetActionStrength("pitch_up") - Input.GetActionStrength("pitch_down");
    rollInput = Input.GetActionStrength("roll_left") - Input.GetActionStrength("roll_right");
    yawInput = Input.GetActionStrength("yaw_left") - Input.GetActionStrength("yaw_right");
  }
}