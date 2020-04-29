using Godot;
using System;

public sealed class Platform : KinematicBody2D
{
    private int speed = 50;
    private Vector2 velocity = new Vector2();
    Random random = new Random();
    public override void _PhysicsProcess(float delta)
    {
        velocity.x = -speed;
        MoveAndCollide(velocity * delta);

        // gdy platforma wyjdzie poza lewą część ekranu
        // pojawia się z prawej strony w loswym miejscu osi Y
        // (symuluje losowe gnerowanie platform)
        if (GlobalPosition.x <= -340)
        {
            GlobalPosition = new Vector2(2260, (float)GD.RandRange(100, 900));
            Scale = new Vector2((float)GD.RandRange(0.7, 1), 1);
        }
    }
}
