using Godot;
using System;

public partial class ChaDemo : CharacterBody2D
{
	[Export]
	public float MoveDuration = 0.2f; // Time it takes to move to the next square.
	private Vector2 _currentTarget; // The current target position.
	private Label _gameOverLabel; // Reference to the GameOver label.

	public override void _Ready()
	{
		// Initialize the character to be centered on a 64px grid.
		_currentTarget = new Vector2(
			Mathf.Round(Position.X / 64) * 64,
			Mathf.Round(Position.Y / 64) * 64
		);
		Position = _currentTarget;

		// Find the GameOverLabel node in the CanvasLayer.
		_gameOverLabel = GetNode<Label>("../CanvasLayer/UI/GameOverLabel");

		// Hide the GameOver label initially.
		_gameOverLabel.Visible = false;
	}

	public override void _Process(double delta)
	{
		
		if (IsOnTarget())
		{
			
			Vector2 direction = Vector2.Zero;

			// Check for input to determine movement direction.
			if (Input.IsActionJustPressed("ui_right"))
				direction.X += 1;
			if (Input.IsActionJustPressed("ui_left"))
				direction.X -= 1;
			if (Input.IsActionJustPressed("ui_down"))
				direction.Y += 1;
			if (Input.IsActionJustPressed("ui_up"))
				direction.Y -= 1;

			// If there's a direction, calculate the new target.
			if (direction != Vector2.Zero)
			{
				_currentTarget += direction * 64;
				MoveToTarget();
			}

			// Perform collision detection
			var collision = MoveAndCollide(direction * 64 * (float)delta);
			if (collision != null)
			{
				// Handle collision (e.g., end the game)
				
				GameOver();
			}
			
		}
	}

	private bool IsOnTarget()
	{
		// Check if the character is at the current target position.
		return Position == _currentTarget;
	}

	private void MoveToTarget()
	{
		// Create a tween to animate the character's movement.
		Tween tween = GetTree().CreateTween();
		tween.TweenProperty(this, "position", _currentTarget, MoveDuration)
			.SetTrans(Tween.TransitionType.Sine)
			.SetEase(Tween.EaseType.InOut);
	}

	private void GameOver()
	{
		GetTree().ChangeSceneToFile("res://GameOver.tscn");
		// Pause the game
		GetTree().Paused = true;

		// Show Game Over popup
		
		
	}

	//private void ShowGameOverPopup()
	//{
		//// Show the Game Over label (unhide it)
		//_gameOverLabel.Visible = true;
	//}
}
