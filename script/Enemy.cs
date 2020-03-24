using Godot;
using System;
public class Enemy : Character
{
  [Export]
  public int enemyPoints = 150;
  public override void _Ready()
  {
    isDead = false;
    gravity = 40;
    baseHp = hp;
    velocity = new Vector2();
    hpBar = GetNode("HpBar/Green") as ColorRect;
    gunNode = GetNode("Gun") as Gun;
    gunSprite = gunNode.GetNode("Sprite") as Sprite;
    baseSizeHpBar = hpBar.RectSize;
    score = GetTree().Root.GetNode("Gameplay/Score") as Label;
    charcterAnimatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
  }
  public override void _PhysicsProcess(float delta)
  {
    if (!isDead)
    {
      lookVector = GetTree().Root.GetNode<KinematicBody2D>("Gameplay/Player").GlobalPosition - GlobalPosition;

      if (Math.Abs(lookVector.x) > 80)
        velocity.x = lookVector.x > 0 ? speed : -speed;
      else
        velocity.x = lookVector.x > 0 ? -speed : speed;

      if (IsOnFloor())
        charcterAnimatedSprite.Play("Runing");
      else
        charcterAnimatedSprite.Play("Jump Loop");

      MoveAndSlide(velocity, floor);
      velocity.y = gravity * 10;

      if (IsOnWall())
        velocity.y -= gravity * 20;

      gunNode.GlobalRotation = Mathf.Atan2(lookVector.y, lookVector.x);

      scale.y = lookVector.x > 0 ? 1 : -1;
      gunSprite.Scale = scale;
      charcterAnimatedSprite.FlipH = scale.y < 0 ? true : false;

      if (IsOnFloor() && gunNode.ready)
        gunNode.shot(lookVector, gunNode.damage);
    }
    else
    {
      velocity.y = gravity * 2;
      velocity.x = -50;
      MoveAndCollide(velocity * delta);
    }
  }
  public override void UpdateHp(float damgae)
  {
    hp += damgae;
    hpBar.SetSize(new Vector2((hp / baseHp) * baseSizeHpBar.x, baseSizeHpBar.y));
    charcterAnimatedSprite.Play("Hurt");
    if (hp < 1)
    {
      score.Text = $"{uint.Parse(score.Text) + enemyPoints}";
      Kill();
    }
  }
  public void _on_Timer_timeout()
  {
    QueueFree();
  }
}
