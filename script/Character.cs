using Godot;
using System;

public class Character : KinematicBody2D
{
  protected int speed;
  protected int gravity;
  protected Vector2 velocity = new Vector2();
  protected Vector2 floor = new Vector2(0, -1);
  protected Vector2 scale = new Vector2(-1, 1);
  protected Vector2 lookVector = new Vector2();
  protected Sprite gunSprite = new Sprite();
  public float hp;
  public float baseHp;
  public ColorRect hpBar = new ColorRect();
  public Vector2 baseSizeHpBar = new Vector2();
  protected PackedScene gun1;
  protected PackedScene gun2;
  protected PackedScene gun3;
  protected Gun gunNode = new Gun();
  protected Sprite charcterSprite;
  protected Label score;
  public virtual void Hit(float damgae)
  {
    hp -= damgae;
    velocity.y += -600;
    hpBar.SetSize(new Vector2((hp / baseHp) * baseSizeHpBar.x, baseSizeHpBar.y));
    if (hp < 1)
    {
      score.Text = $"{uint.Parse(score.Text) + 155}";
      Kill();
    }
  }
  public virtual void Kill() { }
}
