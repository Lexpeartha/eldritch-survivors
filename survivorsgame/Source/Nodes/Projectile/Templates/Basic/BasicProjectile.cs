using Godot;
using System;
using SurvivorsGame.Library.Traits;

namespace SurvivorsGame.Source.Nodes;

public sealed partial class BasicProjectile : Projectile
{
	private ProjectileTrailLine _trail;

	public override void _Ready()
	{
		_trail = GetNode<ProjectileTrailLine>("%TrailLine");
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 movement = UpdateMovement((float)delta);
		
		KinematicCollision2D collision = MoveAndCollide(movement);

		if (collision == null)
			return;
		
		OnImpact(collision.GetCollider());
	}

	public override async void OnImpact(GodotObject collider)
	{
		// TODO: Rework this to work with damage system
		SetPhysicsProcess(false);
		CollisionMask = 0;
		CollisionLayer = 0;
		_trail.Stop();

		Tween tween = CreateTween();
		if (collider is IDamageable damageable)
		{
			tween.SetEase(Tween.EaseType.OutIn).SetTrans(Tween.TransitionType.Quart);
			tween.TweenProperty(this, "modulate", Modulate * 3, 0.2f);
			damageable.TakeDamage(1);
		} else
		{
			tween.SetParallel();
			tween.TweenProperty(this, "scale", Vector2.Zero, 0.2f); // TODO: look into this not affecting trail
		}
		tween.Play();

		await ToSignal(tween, Tween.SignalName.Finished);

		CallDeferred(Node.MethodName.QueueFree);
	}

	public void OnScreenExited()
	{
		CallDeferred(Node.MethodName.QueueFree);
	}
	
	protected override void PostSetup() {}
}
