using Godot;
using System.Diagnostics;
using System.Collections.Generic;
using SurvivorsGame.Library.GameSystems;
using SurvivorsGame.Source.Data;
using SurvivorsGame.Source.Traits;

namespace SurvivorsGame.Source.Systems;

public sealed partial class DamageData : RefCounted
{
  private int _damage;
  private HashSet<StringName> _tags;
  
  public int Damage => _damage;
  
  public void SetDamage(int damage)
  {
    _damage = damage;
  }
  
  public bool AddTag(StringName tag)
  {
    _tags ??= [];
    return _tags.Add(tag);
  }
  
  public void SetTags(IEnumerable<StringName> tags)
  {
    _tags = [..tags];
  }
  
  public bool HasTag(StringName tag)
  {
    return _tags.Contains(tag);
  }
}

[GlobalClass]
public partial class DamageSystem : ComponentCollector
{
  [Signal]
  public delegate void OnDamageSpawnedEventHandler(DamageData dmgData);

  private DamageData _dmgData;
  
  public DamageData StartCycle(int initialDmgAmount)
  {
    SetStartingDamage(initialDmgAmount);
    InvokeMethod(StringNameCache.Traits.OnPreDamagePhase, _dmgData);
    
    InvokeMethod(StringNameCache.Traits.OnDamagePhase, _dmgData);
    
    InvokeMethod(StringNameCache.Traits.OnPostDamagePhase, _dmgData);
    return FinalizeCycle();
  }

  private DamageData FinalizeCycle()
  {
    Debug.Assert(_dmgData != null, "DamageData must be set before applying damage");
    EmitSignal(SignalName.OnDamageSpawned, _dmgData);
    return _dmgData;
  }
  
  private void SetStartingDamage(int damage)
  {
    _dmgData = new DamageData();
    _dmgData.SetDamage(damage);
  }
}