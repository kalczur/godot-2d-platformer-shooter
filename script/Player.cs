using Godot;
using System;

public class Player : Character
{
  bool onGround = false;
  int jumpPower = -1000;
  int jupmsCnt = 3;
  int currentGun = 2;
  public override void _Ready()
  {
    speed = 400;
    hp = 200;
    baseHp = 200;
    hpBar = (ColorRect)GetTree().Root.GetNode("StageOne/PlayerHpBar/Green");
    velocity = new Vector2();
    floor = new Vector2(0, -1);
    gravity = 50;
    gun_1 = (PackedScene)GD.Load("res://scene/Gun_1.tscn");
    gun_2 = (PackedScene)GD.Load("res://scene/Gun_2.tscn");
    gunNode = (Gun)gun_2.Instance();
    AddChild(gunNode);
    gunNode.Name = "gun";
    currentGun = 2;
    gunSprite = gunSprite = gunNode.GetNode<Sprite>("Sprite");
    baseSizeHpBar = hpBar.RectSize;
  }
  public override void _PhysicsProcess(float delta)
  {

    if (Input.IsActionPressed("goRight"))
      velocity.x = speed;
    else if (Input.IsActionPressed("goLeft"))
      velocity.x = -speed;
    else
      velocity.x = 0;

    if (Input.IsActionJustPressed("jump"))
      if (jupmsCnt < 3)
      {
        jupmsCnt++;
        velocity.y = jumpPower;
        onGround = false;
      }

    if (Input.IsActionJustPressed("gun_1") && currentGun != 1)
    {
      GetNode("gun").Free();
      gunNode = (Gun)gun_1.Instance();
      gunNode.Name = "gun";
      AddChild(gunNode);
      currentGun = 1;
      gunSprite = gunNode.GetNode<Sprite>("Sprite");
    }
    if (Input.IsActionJustPressed("gun_2") && currentGun != 2)
    {
      GetNode("gun").Free();
      gunNode = (Gun)gun_2.Instance();
      gunNode.Name = "gun";
      AddChild(gunNode);
      currentGun = 2;
      gunSprite = gunNode.GetNode<Sprite>("Sprite");
    }
    velocity.y += gravity;
    velocity = MoveAndSlide(velocity, floor);

    if (IsOnFloor())
    {
      if (!onGround)
      {
        onGround = true;
        jupmsCnt = 0;
      }
    }
    else
      if (onGround)
    {
      onGround = false;
      jupmsCnt = 1;
    }

    lookVec = GetGlobalMousePosition() - GlobalPosition;
    gunNode.GlobalRotation = Mathf.Atan2(lookVec.y, lookVec.x);
    scale.y = lookVec.x > 0 ? 1 : -1;
    gunSprite.Scale = scale;

    if (Input.IsActionJustPressed("shot") && gunNode.ready)
      gunNode.shot(lookVec, gunNode.dmg);

  }
  protected override void kill()
  {
    GetTree().ReloadCurrentScene();
  }
}
