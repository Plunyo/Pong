using Godot;
using System;

public partial class Ball : CharacterBody2D
{
    [Export] Timer ResetTimer;

    [Export] float Speed = 400f;

    Vector2 velocity = Vector2.Zero;
    Vector2 direction = Vector2.Zero;

    private Area2D collisionDetector;

    public override void _Ready()
    {
        // Initialize the collision detector
        collisionDetector = GetNode<Area2D>("CollisionDetector");

        // Connect Signals
        collisionDetector.BodyEntered += OnBodyEntered;

        Global.Instance.ResetSignal += OnResetSignal;

        // Randomize Initial Ball Direction
        GD.Randomize();

        var directions = new Vector2[] 
        {
            new Vector2(-1, -1),
            new Vector2(-1, 1),
            new Vector2(1, -1),
            new Vector2(1, 1)
        };
        
        direction = directions[GD.Randi() % directions.Length].Normalized();
    }

    public override void _PhysicsProcess(double delta)
    {
        velocity = direction * Speed;

        Velocity = velocity;

        if (ResetTimer.IsStopped()) { MoveAndSlide(); }

    }

    private void OnBodyEntered(Node2D otherBody)
    {
        if (otherBody.IsInGroup("Paddle"))
        {
            direction.X = -direction.X;
        }
        else if (otherBody.IsInGroup("Map"))
        {
            direction.Y = -direction.Y;
        }

        Speed += otherBody.IsInGroup("Paddle") || otherBody.IsInGroup("Map") ? 5 : 0;
    }

    private void OnResetSignal()
    {
		GlobalPosition = new Vector2(GetViewportRect().Size.X / 2, GetViewportRect().Size.Y / 2);

        ResetTimer.Start();
    }
}
