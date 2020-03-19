using Godot;
using System;

public class FirstAidKit : RigidBody2D
{
  public void _on_Area2D_body_entered(System.Object body)
  {
    if (body is Player)
    {
      Player player = body as Player;
      player.Heal(50f);
      QueueFree();
    }
    else if (body is Lava)
      QueueFree();
  }

  public void _on_VisibilityNotifier2D_screen_exited()
  {
    QueueFree();
  }
}