using Godot;

public partial class TorchPurple : Sprite2D
{
	[Export]
	public float Speed = 100f; // Speed at which the obstacle moves.
	public CollisionShape2D CollisionShape;

	public override void _Ready()
	{
		// Get the collision shape from the scene if not assigned.
		CollisionShape = GetNode<CollisionShape2D>("CollisionShape2D");
	}

	public override void _Process(double delta)
	{
		// Move the obstacle to the right.
		Position += new Vector2(Speed * (float)delta, 0);

		// Wrap around when it goes off-screen.
		if (Position.X > GetViewportRect().Size.X)
		{
			Position = new Vector2(-Texture.GetWidth(), Position.Y);
		}
	}
}
