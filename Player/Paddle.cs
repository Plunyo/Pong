using Godot;
using System;

public partial class Paddle : CharacterBody2D
{
    [Export] StringName upAction;
    [Export] StringName downAction;

    [Export] float speed = 800.0f;

    float fixedX;

    Vector2 direction;

    public override void _Ready()
    {
        fixedX = GlobalPosition.X;

        // Connect Signals
        Global.Instance.ResetSignal += OnResetSignal;
    }

    public override void _Process(double delta)
    {
        direction = Vector2.Zero;

        direction.Y = Input.GetAxis(upAction, downAction);

        Velocity = direction * speed;

        MoveAndSlide();

        GlobalPosition = new Vector2(fixedX, GlobalPosition.Y);
    }

    private void OnResetSignal()
    {
        GlobalPosition = new Vector2(GlobalPosition.X, GetViewportRect().Size.Y / 2);
    }
}
