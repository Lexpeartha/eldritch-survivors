using Godot;
using System;
using System.Collections;

namespace SurvivorsGame.Source.Resources;

[GlobalClass]
public partial class SineMotion : ProjectileMotion
{
  [Export] public float Amplitude = 80;
  [Export] public float Frequency = 2;
  
  private float _elapsedTime = 0;
  
  public override Vector2 UpdateMovement(Vector2 direction, float delta)
  {
    _elapsedTime += delta;

    float wobble = Mathf.Sin(_elapsedTime * Frequency * Mathf.Pi) * Amplitude;
    
    // TODO: Make this alternate between left and right
    var travelDirection = new Vector2(-direction.Y, direction.X);
    
    return travelDirection * wobble * delta;
  }
}