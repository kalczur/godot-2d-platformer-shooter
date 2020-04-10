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
  protected float effectsVolume;
  protected Vector2 velocity;
  protected Vector2 floor = new Vector2(0, -1);
  protected Vector2 scale = new Vector2(-1, 1);
  protected Vector2 lookVector;
  protected Sprite gunSprite;
  public ColorRect hpBar;
  public Vector2 baseSizeHpBar;
  public bool isDead;
  protected PackedScene gun1;
  protected PackedScene gun2;
  protected PackedScene gun3;
  protected Gun gunNode;
  protected AnimatedSprite charcterAnimatedSprite;
  protected Label score;
  protected AudioStreamPlayer dieSound;
  public virtual void UpdateHp(float damgae) { }

  public void _on_AnimatedSprite_animation_finished()
  {
    if (charcterAnimatedSprite.Animation == "Dying")
      GetNode<Timer>("Timer").Start();
  }
  public void Kill()
  {
    isDead = true;
    velocity.y = gravity;
    charcterAnimatedSprite.Play("Dying");
    dieSound.Play();
    GetNode<CollisionShape2D>("CollisionShape2D").Shape = null;
  }
}
