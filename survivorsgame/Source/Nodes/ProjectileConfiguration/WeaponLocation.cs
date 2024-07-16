using Godot;

namespace SurvivorsGame.Source.Nodes;

[Tool]
public partial class WeaponLocation : Marker2D
{
	[ExportGroup("Debug")]
	[Export] public float HandleWidth = 1.2f;
	[Export] public float MarkWidth = 2.5f;
	
	public override void _Draw()
	{
		if (Engine.IsEditorHint())
		{
			Vector2 arrowCenterPoint = Vector2.Up * 85;
			DrawLine(Vector2.Zero, arrowCenterPoint, Colors.Aquamarine, HandleWidth);
			
			int markRadius = 12;
			DrawLine(
				arrowCenterPoint - new Vector2(markRadius, markRadius),
				arrowCenterPoint + new Vector2(markRadius, markRadius),
				Colors.Gold, MarkWidth
				);
			DrawLine(
				arrowCenterPoint - new Vector2(markRadius, -markRadius),
				arrowCenterPoint + new Vector2(markRadius, -markRadius),
				Colors.Gold, MarkWidth
				);
		}
	}
}
