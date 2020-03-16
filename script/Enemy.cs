using Godot;
using System;
public class Enemy : Character
{
  public override void _Ready()
  {
    speed = 100;
    gravity = 50;
    velocity = new Vector2();
    hp = 100;
    baseHp = 100;
    hpBar = (ColorRect)GetNode("HpBar/Green");
    PackedScene gun_1 = (PackedScene)GD.Load("res://scene/Gun_1.tscn");
    gunNode = (Gun)gun_1.Instance();
    gunSprite = gunNode.GetNode<Sprite>("Sprite");
    AddChild(gunNode);
    baseSizeHpBar = hpBar.RectSize;
  }
  public override void _PhysicsProcess(float delta)
  {
    lookVec = GetTree().Root.GetNode<KinematicBody2D>("StageOne/Player").GlobalPosition - GlobalPosition;

    if (Math.Abs(lookVec.x) > 200)
      velocity.x = lookVec.x > 0 ? speed : -speed;
    else
      velocity.x = 0;

    velocity.y += gravity;
    MoveAndSlide(velocity, floor);
    if (IsOnWall())
      velocity.y = -600;

    gunNode.GlobalRotation = Mathf.Atan2(lookVec.y, lookVec.x);
    scale.y = lookVec.x > 0 ? 1 : -1;
    gunSprite.Scale = scale;

    if (gunNode.ready)
      gunNode.shot(lookVec, gunNode.dmg);
  }
  protected override void kill()
  {
    QueueFree();
  }
}
