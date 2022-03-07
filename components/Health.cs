using System;
using Godot;

/// <summary>
/// template
/// </summary>
public class Health : Node {
  // Signals
  [Signal]
  public delegate void HealthChanged(float health);
  [Signal]
  public delegate void HealthDepleted();

  // Exports
  [Export]
  public float MaxHealth = 9.0f;

  // Public Fields
  public float CurrentHealth;

  // Backing Fields

  // Private Fields

  // Constructor

  // Lifecycle Hooks
  public override void _Ready() {
    CurrentHealth = MaxHealth;
    EmitSignal(nameof(HealthChanged), CurrentHealth);
  }

  // Public Functions
  public void TakeDamage(float amount) {
    CurrentHealth = Math.Max(CurrentHealth - amount, 0);
    if (CurrentHealth <= 0) {
      // do something about this
    }
    EmitSignal(nameof(HealthChanged), CurrentHealth);
  }

  public void HealDamage(float amount) {
    CurrentHealth = Math.Min(CurrentHealth + amount, MaxHealth);
    EmitSignal(nameof(HealthChanged), CurrentHealth);
  }

  // Private Functions
}