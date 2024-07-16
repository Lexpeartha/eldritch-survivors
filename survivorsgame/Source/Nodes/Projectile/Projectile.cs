using Godot;
using System.Collections.Generic;
using SurvivorsGame.Source.Resources;

namespace SurvivorsGame.Source.Nodes;

public abstract partial class Projectile : CharacterBody2D
{
	protected List<ProjectileMotion> _motions = [];
	protected Vector2 _direction = Vector2.Up;
	protected bool _isSetupFinished = false;

	public override void _Ready()
	{
		MotionMode = MotionModeEnum.Floating;
	}
	
	public Vector2 UpdateMovement(float delta)
	{
		var movementVector = Vector2.Zero;
    
		if (_motions.Count == 0)
			return movementVector;

		foreach (var motion in _motions)
		{
			movementVector += motion.UpdateMovement(_direction, delta);
		}

		return movementVector;
	}

	public void Setup(Vector2 position, Vector2 direction, ProjectileMotion[] motions)
	{
		Position = position;
		_direction = direction;

		if (!_isSetupFinished)
		{
			foreach (ProjectileMotion motion in motions)
			{
				ProjectileMotion newMotion = (ProjectileMotion) motion.Duplicate(); // TODO: better motion adding
				newMotion.Set("Projectile", this);
				_motions.Add(newMotion);
			}
			_isSetupFinished = true;
		}
		
		PostSetup();
	}

	public abstract void OnImpact(GodotObject collider);
	
	protected abstract void PostSetup();
}
