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
public class Striker : RigidBody {
  // Signals
  [Signal]
  public delegate void UpdateInput(DegreeOfFreedom axis, float value);
  [Signal]
  public delegate void UpdateVelocity(DegreeOfFreedom axis, float value);

  // Exports
  [Export]
  public bool Active;
  [Export]
  public bool InvertPitch = true;
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

  // Backing Fields

  // Private Fields
  private float pitchInput;
  private float rollInput;
  private float yawInput;
  private float verticalInput;
  private float lateralInput;
  private float forwardInput;

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

    AddCentralForce(-Transform.basis.z * forwardInput * ForwardSpeed);
    AddCentralForce(Transform.basis.x * lateralInput * LateralSpeed);
    AddCentralForce(Transform.basis.y * verticalInput * VerticalSpeed);

    AddTorque(-Transform.basis.z * rollInput * RollSpeed);
    AddTorque(Transform.basis.x * pitchInput * PitchSpeed);
    AddTorque(Transform.basis.y * yawInput * YawSpeed);
  }

  // Public Functions

  // Private Functions
  private void GetInput() {
    if (!Active) {
      return;
    }

    forwardInput = Input.GetActionStrength("slide_backward") - Input.GetActionStrength("slide_forward");
    lateralInput = Input.GetActionStrength("slide_left") - Input.GetActionStrength("slide_right");
    verticalInput = Input.GetActionStrength("slide_up") - Input.GetActionStrength("slide_down");

    rollInput = Input.GetActionStrength("roll_left") - Input.GetActionStrength("roll_right");
    pitchInput = Input.GetActionStrength("pitch_up") - Input.GetActionStrength("pitch_down");
    yawInput = Input.GetActionStrength("yaw_left") - Input.GetActionStrength("yaw_right");

    if (InvertPitch) {
      pitchInput *= -1;
    }
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
    
  }
}
