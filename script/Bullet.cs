using Godot;
using System;

public class Bullet : Area2D
{
    private const int speed = 500;
    private Vector2 velocity = new Vector2();
    public float x;
    public float y;
    public override void _Ready()
    {
        var lookVec = GetGlobalMousePosition() - GlobalPosition;
        x = lookVec.Normalized().x * speed;
        y = lookVec.Normalized().y * speed;
    }

    public override void _PhysicsProcess(float delta)
    {

        velocity.x = (x * delta);
        velocity.y = (y * delta);
        Translate(velocity);
    }
    public void _on_VisibilityNotifier2D_screen_exited()
    {
        QueueFree();
    }
}
