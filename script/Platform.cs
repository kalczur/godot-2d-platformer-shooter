using Godot;
using System;

public class Platform : KinematicBody2D
{
  private int speed = 50;
  private Vector2 velocity = new Vector2();
  public override void _PhysicsProcess(float delta)
  {
    velocity.x = speed;
    MoveAndSlide(velocity);

    if (GlobalPosition.x > 1430)
      GlobalPosition = new Vector2(-150, (float)GD.RandRange(70, 600));
  }

}
