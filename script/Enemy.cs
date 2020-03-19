using Godot;
using System;
public class Enemy : Character
{
  public override void _Ready()
  {
    speed = 100;
    gravity = 500;
    velocity = new Vector2();
    hp = 100;
    baseHp = 100;
    hpBar = GetNode("HpBar/Green") as ColorRect;
    PackedScene gun1 = GD.Load("res://scene/Gun1.tscn") as PackedScene;
    gunNode = gun1.Instance() as Gun;
    gunSprite = gunNode.GetNode("Sprite") as Sprite;
    AddChild(gunNode);
    baseSizeHpBar = hpBar.RectSize;
    score = GetTree().Root.GetNode("Gameplay/Score") as Label;
    charcterSprite = GetNode<Sprite>("Sprite");
  }
  public override void _PhysicsProcess(float delta)
  {
    lookVector = GetTree().Root.GetNode<KinematicBody2D>("Gameplay/Player").GlobalPosition - GlobalPosition;

    if (Math.Abs(lookVector.x) > 80)
      velocity.x = lookVector.x > 0 ? speed : -speed;
    else
      velocity.x = lookVector.x > 0 ? -speed : speed;

    MoveAndSlide(velocity, floor);
    velocity.y = gravity;

    if (IsOnWall())
      velocity.y = -gravity * 2;

    gunNode.GlobalRotation = Mathf.Atan2(lookVector.y, lookVector.x);
    scale.y = lookVector.x > 0 ? 1 : -1;
    gunSprite.Scale = scale;
    charcterSprite.FlipH = scale.y < 0 ? true : false;

    if (gunNode.ready)
      gunNode.shot(lookVector, gunNode.damage);
  }
  public override void Kill()
  {
    QueueFree();
  }
}
