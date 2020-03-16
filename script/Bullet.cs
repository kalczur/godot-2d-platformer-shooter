using Godot;
using System;

public class Bullet : Area2D
{
  public int speed;
  private Vector2 velocity = new Vector2();
  public Vector2 lookVec = new Vector2();
  private float x;
  private float y;
  public float dmg;
  public override void _Ready()
  {
    x = lookVec.Normalized().x * speed;
    y = lookVec.Normalized().y * speed;
  }

  public override void _PhysicsProcess(float delta)
  {
    velocity.x = (x * delta);
    velocity.y = (y * delta);
    Translate(velocity);
  }
  public void _on_Bullet_body_entered(System.Object body)
  {
    if (body is Character)
    {
      Character character = (Character)body;
      character.hit(dmg);
    }
    QueueFree();
  }
  public void _on_VisibilityNotifier2D_screen_exited()
  {
    QueueFree();
  }
}
