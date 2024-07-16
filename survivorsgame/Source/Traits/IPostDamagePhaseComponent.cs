using SurvivorsGame.Source.Systems;

namespace SurvivorsGame.Source.Traits;

public interface IPostDamagePhaseComponent
{
  public void OnPostDamagePhase(DamageData damageData);
}