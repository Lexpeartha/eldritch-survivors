using Godot;
using SurvivorsGame.Source.Data;
using SurvivorsGame.Library.GameSystems;
using SurvivorsGame.Source.Traits;

namespace SurvivorsGame.Source.Systems.Components;

[GlobalClass]
public sealed partial class IceComponent : Component, IPreDamagePhaseComponent, IDamagePhaseComponent
{
  public void OnPreDamagePhase(DamageData damageData)
  {
    damageData.AddTag(StringNameCache.ItemTags.Ice);
  }

  public void OnDamagePhase(DamageData damageData)
  {
    if (damageData.HasTag(StringNameCache.ItemTags.Ice))
    {
      damageData.SetDamage(damageData.Damage * 2);
    }
  }
}