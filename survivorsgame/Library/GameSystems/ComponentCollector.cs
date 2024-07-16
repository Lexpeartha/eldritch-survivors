using System.Diagnostics;
using Godot;

namespace SurvivorsGame.Library.GameSystems;

// ALTERNATIVE: AddUserSignal();

// POTENTIAL PROBLEMS:
// - Game can lag, maybe try deferring?
// - Call order determinism?

[GlobalClass]
public abstract partial class ComponentCollector : Node
{
  [Signal]
  public delegate void OnComponentAddedEventHandler(Component component);

  [Signal]
  public delegate void OnComponentRemovedEventHandler(Component component);

  [Export] public StringName GroupName { get; set; }

  public override void _EnterTree()
  {
    Debug.Assert(GroupName != null, "GroupName must be set");
    Debug.Assert(!GroupName.IsEmpty, "GroupName must be set");
  }

  public override void _Ready()
  {
    foreach (Node child in GetChildren())
    {
      if (child is not Component component) continue;
      component.AddToGroup(GroupName);
    }
  }

  public void AddComponent(Component component)
  {
    if (!IsInsideTree()) return;
    
    AddChild(component);
    component.AddToGroup(GroupName);
    EmitSignal(SignalName.OnComponentAdded, component);
  }

  public void RemoveComponent(Component component)
  {
    if (!IsInsideTree()) return;
    
    RemoveChild(component);
    component.RemoveFromGroup(GroupName);
    EmitSignal(SignalName.OnComponentRemoved, component);
  }
  
  protected void InvokeMethod(StringName method, params Variant[] args)
  {
    GetTree().CallGroup(GroupName, method, args);
  }
}