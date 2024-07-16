using Godot;
using System;
using SurvivorsGame.Source.Nodes;

namespace SurvivorsGame.Source.Resources;

[GlobalClass]
public abstract partial class ProjectileMotion : Resource
{
  protected Projectile _projectile { get; set; }
  
  public void SetProjectile(Projectile projectile)
  {
    _projectile = projectile;
  }
  
  public virtual Vector2 UpdateMovement(Vector2 direction, float delta)
  {
    return Vector2.Zero;
  }
}
