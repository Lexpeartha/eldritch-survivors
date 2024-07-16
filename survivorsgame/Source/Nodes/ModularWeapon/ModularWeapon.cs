using Godot;
using System;
using System.Collections.Generic;
using SurvivorsGame.Source.Resources;

namespace SurvivorsGame.Source.Nodes;

public partial class ModularWeapon : Node2D
{
  // a bit confusing since emitter and spawner are almost used interchangeably
  // but the spawner is configuration, which was created from the emitter
  private PackedScene _emitterConfigScene;
  [Export]
  public PackedScene EmitterConfigScene
  {
    get => _emitterConfigScene;
    set => SetEmitterConfig(value);
  }
  private PackedScene _projectileSpawnerScene;
  [Export]
  public PackedScene ProjectileSpawnerScene
  {
    get => _projectileSpawnerScene;
    set => SetSpawner(value);
  }

  [Export] public ProjectileMotion[] ProjectileMotions = [];
  protected List<ProjectileMotion> _motions = []; // TODO: do something about this...

  public override void _Ready()
  {
    SynchronizeExports();
  }

  public void SynchronizeExports()
  {
    if (ProjectileMotions == null || ProjectileMotions.Length == 0) return;
    
    _motions.Clear();
    foreach (ProjectileMotion motion in ProjectileMotions)
    {
      _motions.Add(motion);
    }
  }
  
  public List<ProjectileMotion> GetMotions()
  {
    return _motions;
  }

  public void ClearEmitters()
  {
    foreach (Node child in GetChildren())
    {
      if (child is not ProjectileSpawner) continue;
      RemoveChild(child);
      child.QueueFree();
    }
  }

  public void AddNewSpawners()
  {
    var config = EmitterConfigScene.InstantiateOrNull<Node2D>();
    if (config == null) return;

    foreach (Node child in config.GetChildren())
    {
      if (child is not WeaponLocation weaponLocation) continue;
      var newSpawner = ProjectileSpawnerScene.Instantiate<ProjectileSpawner>();
      newSpawner.Position = weaponLocation.Position;
      newSpawner.Rotation = weaponLocation.Rotation;
      
      newSpawner.ModularWeaponSystem = this;
      newSpawner.SetSpawnerNode(GetTree().Root); // TODO: use something else
      AddChild(newSpawner);
    }
    
    config.Free();
  }
  
  public void AddProjectileMotion(ProjectileMotion motion, bool allowDuplicates = false)
  {
    if (!allowDuplicates)
    {
       bool hasDuplicate = _motions.Exists(m => m.GetScript().Equals(motion.GetScript()));
       if (hasDuplicate) return;
    }
    
    _motions.Add(motion);
  }
  
  private async void SetEmitterConfig(PackedScene value)
  {
    _emitterConfigScene = value;
    if (!IsInsideTree())
      await ToSignal(this, Node.SignalName.Ready);
    
    ClearEmitters();
    AddNewSpawners();
  }

  private void SetSpawner(PackedScene value)
  {
    _projectileSpawnerScene = value;
    SetEmitterConfig(EmitterConfigScene);
  }
}