using System.Diagnostics;
using Godot;

namespace SurvivorsGame.Library.GameSystems;

[GlobalClass]
public abstract partial class Component : Node
{
  protected ComponentCollector _collector;

  public override void _Ready()
  {
    Node parent = GetParent();
    Debug.Assert(parent is ComponentCollector, "Component must be a child of a ComponentCollector");

    _collector = parent as ComponentCollector;
    CallDeferred(nameof(OnInitializationFinished));
  }

  protected virtual void OnInitializationFinished() {}
}