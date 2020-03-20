using Godot;
using System;

public class C4 : RigidBody2D
{
  public void _on_Area2D_body_entered(System.Object body)
  {
    if (body is Character)
    {
      Character character = body as Character;
      character.Hit(50f);
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