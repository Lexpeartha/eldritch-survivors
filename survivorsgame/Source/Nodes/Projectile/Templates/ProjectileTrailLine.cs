using Godot;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace SurvivorsGame.Source.Nodes;

public enum ProjectileTrailType
{
	Trailing,
	Rigid,
}

public partial class ProjectileTrailLine : Line2D
{
	[Export] public ProjectileTrailType TrailType = ProjectileTrailType.Trailing;
	[Export] public int MaxTrailPoints = 16;
	
	private Node2D _owner;
	private LinkedList<Vector2> _trailPoints;
	private bool _stopped = false;
	
	public override void _Ready()
	{
		Node owner = Owner;
		Debug.Assert(owner is Node2D, "Owner must be have a position");
		_owner = owner as Node2D;
		
		_trailPoints = [];
		ClearPoints();

		// TODO: add something better here
		if (TrailType == ProjectileTrailType.Rigid)
		{
			WidthCurve = null;
			BeginCapMode = LineCapMode.Box;
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		if (_stopped)
		{
			if (_trailPoints.Count != 0)
				_trailPoints.RemoveLast();
			SetPoints(_trailPoints.ToArray());
			return;
		}
		
		Vector2 position = GetTrailPoint();
		_trailPoints.AddFirst(position);
		
		if (_trailPoints.Count > MaxTrailPoints)
			_trailPoints.RemoveLast();
		
		ClearPoints();
		SetPoints(_trailPoints.ToArray());
	}
	
	public void Stop()
	{
		_stopped = true;
	}
	
	private Vector2 GetTrailPoint()
	{
		return _owner.GlobalPosition;
	}
}
