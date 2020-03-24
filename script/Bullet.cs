using Godot;
using System;

public class Bullet : Area2D
{
  public int speed;
  private Vector2 velocity = new Vector2();
  public Vector2 lookVector = new Vector2();
  private float x;
  private float y;
  public float damage;
  public override void _Ready()
  {
    x = lookVector.Normalized().x * speed;
    y = lookVector.Normalized().y * speed;
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
      Character character = body as Character;
      character.UpdateHp(damage);
    }
    QueueFree();
  }
  public void _on_VisibilityNotifier2D_screen_exited()
  {
    QueueFree();
  }
}
