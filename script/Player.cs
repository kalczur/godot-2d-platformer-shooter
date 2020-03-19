using Godot;
using System;

public class Player : Character
{
  bool onGround = false;
  int jumpPower = -1000;
  int jupmCount = 3;
  int currentGun = 2;
  int screenShakePower;
  public override void _Ready()
  {
    speed = 400;
    hp = 200;
    baseHp = 200;
    hpBar = GetTree().Root.GetNode("Gameplay/PlayerHpBar/Green") as ColorRect;
    velocity = new Vector2();
    floor = new Vector2(0, -1);
    gravity = 40;
    gun1 = GD.Load("res://scene/Gun1.tscn") as PackedScene;
    gun2 = GD.Load("res://scene/Gun2.tscn") as PackedScene;
    gunNode = gun2.Instance() as Gun;
    AddChild(gunNode);
    gunNode.Name = "gun";
    currentGun = 2;
    charcterSprite = GetNode<Sprite>("Sprite");
    gunSprite = gunNode.GetNode<Sprite>("Sprite");
    baseSizeHpBar = hpBar.RectSize;
  }
  public override void _PhysicsProcess(float delta)
  {

    if (Input.IsActionPressed("goRight"))
    {
      charcterSprite.FlipH = false;
      velocity.x = speed;
    }

    else if (Input.IsActionPressed("goLeft"))
    {
      charcterSprite.FlipH = true;
      velocity.x = -speed;
    }
    else
      velocity.x = 0;

    if (Input.IsActionJustPressed("jump"))
      if (jupmCount < 3)
      {
        jupmCount++;
        velocity.y = jumpPower;
        onGround = false;
      }

    if (Input.IsActionJustPressed("gun_1") && currentGun != 1)
    {
      GetNode("gun").Free();
      gunNode = gun1.Instance() as Gun;
      gunNode.Name = "gun";
      AddChild(gunNode);
      currentGun = 1;
      gunSprite = gunNode.GetNode("Sprite") as Sprite;
    }
    if (Input.IsActionJustPressed("gun_2") && currentGun != 2)
    {
      GetNode("gun").Free();
      gunNode = gun2.Instance() as Gun;
      gunNode.Name = "gun";
      AddChild(gunNode);
      currentGun = 2;
      gunSprite = gunNode.GetNode("Sprite") as Sprite;
    }

    if (jupmCount > 0)
      velocity.y += gravity;
    else
      velocity.y = gravity * 10;

    if (Input.IsActionPressed("shot") && gunNode.ready)
    {
      charcterSprite.FlipH = lookVector.x < 0 ? true : false;
      velocity = lookVector.Normalized() * -speed * 2;
      gunNode.shot(lookVector, gunNode.damage);
    }

    velocity = MoveAndSlide(velocity, floor);

    if (IsOnFloor())
    {
      if (!onGround)
      {
        onGround = true;
        jupmCount = 0;
      }
    }
    else
      if (onGround)
    {
      onGround = false;
      jupmCount = 1;
    }

    lookVector = GetGlobalMousePosition() - GlobalPosition;
    gunNode.GlobalRotation = Mathf.Atan2(lookVector.y, lookVector.x);
    scale.y = lookVector.x > 0 ? 1 : -1;
    gunSprite.Scale = scale;
  }
  public override void Hit(float damgae)
  {
    hp -= damgae;
    UpdateHp();
    screenShakePower = damgae < 40 ? 1 : 2;
    GetParent().GetNode<ScreenShake>("ScreenShake").ScreenShakeStart(screenShakePower, screenShakePower * 10, screenShakePower * 100);

    if (hp < 1)
      Kill();
  }
  public void Heal(float healHp)
  {
    hp = hp + healHp > baseHp ? baseHp : hp + healHp;
    UpdateHp();
  }
  private void UpdateHp()
  {
    hpBar.SetSize(new Vector2((hp / baseHp) * baseSizeHpBar.x, baseSizeHpBar.y));
  }
  public override void Kill()
  {
    GetTree().ReloadCurrentScene();
  }
}
