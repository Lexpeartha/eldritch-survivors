using System;

namespace SurvivorsGame.Library.Traits;

public interface IDamageable
{
  public int Hp { get; protected set; }
  public bool IsAlive => Hp > 0;

  public void StartDamageTakingProcess(int initialDmgAmount);
  public void StartHpHealingProcess(int initialHealAmount);
  
  protected internal void TakeDamage(int damage)
  {
    Hp = Math.Clamp(Hp - damage, 0, Hp);
  }

  protected internal void HealHp(int amount)
  {
    Hp = Math.Clamp(Hp + amount, 0, Hp);
  }
}