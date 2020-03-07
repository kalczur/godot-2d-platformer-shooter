using Godot;
using System;

public class Enemy : KinematicBody2D
{
    private const int speed = 100;
    private const int gravity = 50;
    private Vector2 velocity = new Vector2();
    public override void _Ready()
    {

    }
    public override void _PhysicsProcess(float delta)
    {
        var vecToPlayer = GetTree().Root.GetNode<KinematicBody2D>("StageOne/Player").GlobalPosition - GlobalPosition;

        if (Math.Abs(vecToPlayer.x) > 100)
            velocity.x = vecToPlayer.x > 0 ? speed : -speed;
        else
            velocity.x = 0;

        velocity.y += gravity;
        MoveAndSlide(velocity);
    }
}
