using Godot;
using System;

public class Platform : KinematicBody2D
{
  private int speed = 50;
  private Vector2 velocity = new Vector2();
  public override void _PhysicsProcess(float delta)
  {
    velocity.x = -speed;
    MoveAndCollide(velocity * delta);


    if (GlobalPosition.x < -150)
      GlobalPosition = new Vector2(1430, (float)GD.RandRange(50, 600));
  }
}
