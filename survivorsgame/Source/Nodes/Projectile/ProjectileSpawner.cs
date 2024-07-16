using Godot;
using SurvivorsGame.Source.Resources;

namespace SurvivorsGame.Source.Nodes;

public abstract partial class ProjectileSpawner : Node2D
{
	[Export] public int DamageOnCollision = 4;
	[Export] public ModularWeapon ModularWeaponSystem;

	[Signal] public delegate void OnProjectileSpawnedEventHandler();
	
	protected Node SpawnerNode; // ?????
	
	public override void _Ready()
	{
		SpawnerNode = GetTree().Root;
	}
	
	public void SetSpawnerNode(Node node)
	{
		SpawnerNode = node;
	}

	public void Fire()
	{
		HandleFiring(Vector2.Up.Rotated(GlobalRotation), ModularWeaponSystem.GetMotions().ToArray());
		EmitSignal(SignalName.OnProjectileSpawned);
	}

	protected abstract void HandleFiring(Vector2 direction, ProjectileMotion[] motions);
}
