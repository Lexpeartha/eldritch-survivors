using Godot;
using System;

namespace SurvivorsGame.Source.Resources;

[GlobalClass]
public partial class StraightMotion : ProjectileMotion
{
  [Export] public float Speed = 600;
  
  public override Vector2 UpdateMovement(Vector2 direction, float delta)
  {
    return direction * Speed * delta;
  }
}