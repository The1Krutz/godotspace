using Godot;

public enum DegreeOfFreedom {
  Roll,
  Pitch,
  Yaw,
  Forward,
  Vertical,
  Lateral
}

/// <summary>
/// template
/// </summary>
public class PlayerFlightController : RigidBody {
  // Signals
  [Signal]
  public delegate void UpdateInput(DegreeOfFreedom axis, float value);
  [Signal]
  public delegate void UpdateVelocity(DegreeOfFreedom axis, float value);

  // Exports
  [Export]
  public float PitchSpeed = 10.0f;
  [Export]
  public float RollSpeed = 10.0f;
  [Export]
  public float YawSpeed = 10.0f;
  [Export]
  public float VerticalSpeed = 10.0f;
  [Export]
  public float LateralSpeed = 10.0f;
  [Export]
  public float ForwardSpeed = 10.0f;

  // Public Fields
  public bool ActivePlayer;

  // Backing Fields

  // Private Fields
  private float pitchInput;
  private float rollInput;
  private float yawInput;
  private float verticalInput;
  private float lateralInput;
  private float forwardInput;
  private bool emergencyBrake;

  // Constructor

  // Lifecycle Hooks
  public override void _Ready() {
    // Physics sleep for rigidbodies is an important performance optimization. Disable it sparingly.
    CanSleep = false;
  }

  public override void _IntegrateForces(PhysicsDirectBodyState state) {
    GetInput();
    EmitInputs();
    EmitVelocities();

    if (emergencyBrake) {
      LinearVelocity = Vector3.Zero;
      AngularVelocity = Vector3.Zero;
    } else {
      AddCentralForce(-Transform.basis.z * forwardInput * ForwardSpeed);
      AddCentralForce(Transform.basis.y * verticalInput * VerticalSpeed);
      AddCentralForce(Transform.basis.x * lateralInput * LateralSpeed);

      AddTorque(-Transform.basis.z * rollInput * RollSpeed);
      AddTorque(Transform.basis.x * pitchInput * PitchSpeed);
      AddTorque(Transform.basis.y * -yawInput * YawSpeed);
    }
  }

  // Public Functions

  // Private Functions
  private void GetInput() {
    if (!ActivePlayer) {
      return;
    }

    emergencyBrake = Input.IsActionPressed("emergency_brake");

    forwardInput = Input.GetActionStrength("slide_forward") - Input.GetActionStrength("slide_backward");
    lateralInput = Input.GetActionStrength("slide_right") - Input.GetActionStrength("slide_left");
    verticalInput = Input.GetActionStrength("slide_up") - Input.GetActionStrength("slide_down");

    rollInput = Input.GetActionStrength("roll_right") - Input.GetActionStrength("roll_left");
    pitchInput = Input.GetActionStrength("pitch_up") - Input.GetActionStrength("pitch_down");
    yawInput = Input.GetActionStrength("yaw_right") - Input.GetActionStrength("yaw_left");
  }

  private void EmitInputs() {
    EmitSignal(nameof(UpdateInput), DegreeOfFreedom.Roll, rollInput);
    EmitSignal(nameof(UpdateInput), DegreeOfFreedom.Pitch, pitchInput);
    EmitSignal(nameof(UpdateInput), DegreeOfFreedom.Yaw, yawInput);

    EmitSignal(nameof(UpdateInput), DegreeOfFreedom.Forward, forwardInput);
    EmitSignal(nameof(UpdateInput), DegreeOfFreedom.Lateral, lateralInput);
    EmitSignal(nameof(UpdateInput), DegreeOfFreedom.Vertical, verticalInput);
  }

  private void EmitVelocities() {
    Vector3 localAngularVelocity = GlobalTransform.basis.XformInv(AngularVelocity);
    EmitSignal(nameof(UpdateVelocity), DegreeOfFreedom.Roll, localAngularVelocity.z);
    EmitSignal(nameof(UpdateVelocity), DegreeOfFreedom.Pitch, localAngularVelocity.x);
    EmitSignal(nameof(UpdateVelocity), DegreeOfFreedom.Yaw, localAngularVelocity.y);

    Vector3 localLinearVelocity = GlobalTransform.basis.XformInv(LinearVelocity);
    EmitSignal(nameof(UpdateVelocity), DegreeOfFreedom.Forward, -localLinearVelocity.z);
    EmitSignal(nameof(UpdateVelocity), DegreeOfFreedom.Lateral, localLinearVelocity.x);
    EmitSignal(nameof(UpdateVelocity), DegreeOfFreedom.Vertical, localLinearVelocity.y);
  }
}
