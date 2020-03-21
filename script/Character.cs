using Godot;
using System;

public class Character : KinematicBody2D
{
  [Export]
  public float hp;
  [Export]
  protected int speed;
  public float baseHp;
  protected int gravity;
  protected Vector2 velocity = new Vector2();
  protected Vector2 floor = new Vector2(0, -1);
  protected Vector2 scale = new Vector2(-1, 1);
  protected Vector2 lookVector = new Vector2();
  protected Sprite gunSprite = new Sprite();
  public ColorRect hpBar = new ColorRect();
  public Vector2 baseSizeHpBar = new Vector2();
  public bool isDead;
  protected PackedScene gun1;
  protected PackedScene gun2;
  protected PackedScene gun3;
  protected Gun gunNode = new Gun();
  protected AnimatedSprite charcterAnimatedSprite;
  protected Label score;
  public virtual void Hit(float damgae) { }

  public void _on_AnimatedSprite_animation_finished()
  {
    if (charcterAnimatedSprite.Animation == "Dying")
      Kill();
  }
  public void Kill()
  {
    isDead = true;
    velocity.y = gravity;
    charcterAnimatedSprite.Play("Dying");
    GetNode<CollisionShape2D>("CollisionShape2D").Shape = null;

    GetNode<Timer>("Timer").Start();
  }
}
