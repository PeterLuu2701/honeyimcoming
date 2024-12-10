using Godot;

public partial class ObstacleSpawner : Node2D
{
	[Export]
	public PackedScene ObstacleScene; // Scene for the obstacle.

	[Export]
	public int ObstacleCount = 5; // Number of obstacles to spawn.

	[Export]
	public float XSpacing = 128f; // Horizontal spacing between obstacles.

	[Export]
	public float InitialY = 200f; // Y position of the obstacles.

	public override void _Ready()
	{
		// Ensure the ObstacleScene is assigned.
		if (ObstacleScene == null)
		{
			GD.PrintErr("ObstacleScene is not assigned.");
			return;
		}

		// Spawn and position obstacles.
		for (int i = 0; i < ObstacleCount; i++)
		{
			var obstacleInstance = (Sprite2D)ObstacleScene.Instantiate();
			AddChild(obstacleInstance);
			obstacleInstance.Position = new Vector2(i * XSpacing, InitialY);
		}
	}
}
