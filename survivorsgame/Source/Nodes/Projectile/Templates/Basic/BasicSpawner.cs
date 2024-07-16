using Godot;
using System;
using SurvivorsGame.Source.Resources;

namespace SurvivorsGame.Source.Nodes;

public sealed partial class BasicSpawner : ProjectileSpawner
{
	private PackedScene _basicProjectileScene;
	private Timer _fireRateTimer;

	public override void _Ready()
	{
		_basicProjectileScene = GD.Load<PackedScene>("res://Source/Nodes/Projectile/Templates/Basic/BasicProjectile.tscn");
		_fireRateTimer = GetNode<Timer>("FirerateTimer");
	}

	public override void _PhysicsProcess(double delta)
	{
		if (Input.IsActionJustPressed("action_cast") && _fireRateTimer.IsStopped()) // TODO: Get via signal
		{
			Fire();
			_fireRateTimer.Start();
		}
	}

	protected override void HandleFiring(Vector2 direction, ProjectileMotion[] motions)
	{
		if (SpawnerNode == null)
			return;
		
		var projectile = _basicProjectileScene.Instantiate<Projectile>();
		projectile.Setup(GlobalPosition, direction, motions);
		SpawnerNode.AddChild(projectile);
	}
}
