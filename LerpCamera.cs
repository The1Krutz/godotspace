using Godot;

/// <summary>
/// template
/// </summary>
public class LerpCamera : Camera {
  // Signals

  // Exports
  [Export]
  public float LerpSpeed = 3.0f;
  [Export]
  public NodePath TargetPath;
  [Export]
  public Vector3 Offset = Vector3.Zero;

  // Public Fields

  // Backing Fields

  // Private Fields
  private Spatial target;

  // Constructor

  // Lifecycle Hooks
  public override void _Ready() {
    if (TargetPath != null) {
      target = GetNode<Spatial>(TargetPath);
    }
  }

  public override void _PhysicsProcess(float delta) {
    if (target == null) {
      return;
    }
    Transform targetPosition = target.GlobalTransform.Translated(Offset);
    GlobalTransform = GlobalTransform.InterpolateWith(targetPosition, LerpSpeed * delta);
    LookAt(target.GlobalTransform.origin, Vector3.Up);
  }

  // Public Functions

  // Private Functions
}