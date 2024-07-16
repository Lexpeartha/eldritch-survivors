using System.Diagnostics;
using Godot;
using SurvivorsGame.Library.Traits;
using SurvivorsGame.Source.Systems;

namespace SurvivorsGame.Source.Nodes;

public partial class Player : CharacterBody2D, IDamageable
{
  private DamageSystem _dmgSystem;
  [Export] public int Hp { get; set; }
  
  [Signal] public delegate void OnHpChangedEventHandler(int newHp);
  [Signal] public delegate void OnPlayerDeathEventHandler();
  
  public override void _EnterTree()
  {
    Debug.Assert(Hp > 0, "Player's HP must be greater than 0");
    GD.Print(Hp);
  }

  public override void _Ready()
  {
    _dmgSystem = GetNode<DamageSystem>("%DamageSystem");

    _dmgSystem.OnDamageSpawned += FinalizeTakingDamage;
  }

  public override void _PhysicsProcess(double delta)
  {
    Vector2 input = Input.GetVector("move_left", "move_right", "move_up", "move_down");
    
    Velocity = Velocity.Lerp(input * 1000, 0.15f);
    
    MoveAndSlide();
  }
  
  public void StartDamageTakingProcess(int initialDmgAmount)
  {
    _dmgSystem.StartCycle(initialDmgAmount);
  }
  
  public void StartHpHealingProcess(int initialHealAmount)
  {
    // _dmgSystem.StartCycle(initialHealAmount);
  }

  public void FinalizeTakingDamage(DamageData dmgData)
  {
    var damageableInstance = (this as IDamageable);
    damageableInstance.TakeDamage(dmgData.Damage);
    EmitSignal(SignalName.OnHpChanged, damageableInstance.Hp);
    if (damageableInstance.IsAlive) return;
    EmitSignal(SignalName.OnPlayerDeath);
  }
}