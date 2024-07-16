using SurvivorsGame.Source.Systems;

namespace SurvivorsGame.Source.Traits;

public interface IDamagePhaseComponent
{
  public void OnDamagePhase(DamageData damageData);
}