using Godot;
using System;

public class Item : RigidBody2D
{
  [Export]
  public float hpValue = 50;
  protected AnimatedSprite animatedSprite;
  public override void _Ready()
  {
    animatedSprite = GetNode("AnimatedSprite") as AnimatedSprite;
  }
  public virtual void _on_Area2D_body_entered(System.Object body)
  {
    if (body is Player)
    {
      Player player = body as Player;
      player.Heal(hpValue);
      Remove();
    }
    else if (body is Lava)
      Remove();
  }
  protected void Remove()
  {
    animatedSprite.Play("Remove");
    GetNode<CollisionShape2D>("CollisionShape2D").Shape = null;
    GetNode<CollisionShape2D>("Area2D/CollisionShape2D").Shape = null;
    GravityScale = 0;
  }
  public void _on_VisibilityNotifier2D_screen_exited()
  {
    QueueFree();
  }
  public void _on_AnimatedSprite_animation_finished()
  {
    if (animatedSprite.Animation == "Remove")
      QueueFree();
  }
}
