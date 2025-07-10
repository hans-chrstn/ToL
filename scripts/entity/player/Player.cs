using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export] public float WALK_SPEED = 300.0f;
	[Export] public float RUN_SPEED = 350.0f;

	private AnimatedSprite2D _anim;
	public override void _Ready()
	{
		_anim = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	public void GetInput(double delta)
	{
		Vector2 input_direction = Input.GetVector("left", "right", "up", "down");

		float speed = Input.IsActionPressed("sprint") ? RUN_SPEED : WALK_SPEED;
		Vector2 velocity = input_direction * speed;

		if (Input.IsActionPressed("attack"))
		{
			velocity = Vector2.Zero;
			_anim.Play("Attack");
		}
		else
		{
			if (input_direction == Vector2.Zero)
			{
				_anim.Play("Idle");
			}
			else
			{
				_anim.Play(speed == RUN_SPEED ? "Run" : "Walk");
				if (input_direction.X != 0)
				{
					_anim.FlipH = input_direction.X < 0;
				}
			}
		}

		Velocity = velocity;
	}

	public override void _PhysicsProcess(double delta)
	{
		GetInput(delta);
		MoveAndSlide();
	}
}
