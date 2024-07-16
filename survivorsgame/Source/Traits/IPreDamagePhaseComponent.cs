using SurvivorsGame.Source.Systems;

namespace SurvivorsGame.Source.Traits;

public interface IPreDamagePhaseComponent
{
  public void OnPreDamagePhase(DamageData damageData);
}