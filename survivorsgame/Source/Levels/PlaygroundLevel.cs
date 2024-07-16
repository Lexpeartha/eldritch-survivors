using Godot;
using SurvivorsGame.Library.Traits;
using SurvivorsGame.Source.Nodes;

public partial class PlaygroundLevel : Node2D
{
  private Player _player;
  private Timer _testTimer;

  public override void _Ready()
  {
    _player = GetNode<Player>("%Player");
    _testTimer = GetNode<Timer>("%TestTimer");

    _testTimer.Timeout += () => { _player.StartDamageTakingProcess(1); };
    // _player.OnPlayerDeath += () => { GetTree().Quit(); };
  }
}