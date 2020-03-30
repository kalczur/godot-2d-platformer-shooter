using Godot;
using System;

public class Player : Character
{
  bool onGround = false;
  int jumpPower = -1000;
  int jupmCount = 3;
  int currentGun = 2;
  int screenShakePower;
  bool canPlayStepSound = true;
  AudioStreamPlayer jumpSound;
  AudioStreamPlayer hitSound;
  AudioStreamPlayer stepSound;
  public override void _Ready()
  {
    baseHp = hp;
    isDead = false;
    hpBar = GetTree().Root.GetNode("Gameplay/PlayerHpBar/Green") as ColorRect;
    velocity = new Vector2();
    floor = new Vector2(0, -1);
    gravity = 40;
    gun1 = GD.Load("res://scene/Gun1.tscn") as PackedScene;
    gun2 = GD.Load("res://scene/Gun2.tscn") as PackedScene;
    gun3 = GD.Load("res://scene/Gun3.tscn") as PackedScene;
    gunNode = gun2.Instance() as Gun;
    AddChild(gunNode);
    gunNode.Name = "gun";
    currentGun = 2;
    charcterAnimatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
    gunSprite = gunNode.GetNode<Sprite>("Sprite");
    baseSizeHpBar = hpBar.RectSize;
    jumpSound = GetNode("JumpSound") as AudioStreamPlayer;
    stepSound = GetNode("StepSound") as AudioStreamPlayer;
    hitSound = GetNode("HitSound") as AudioStreamPlayer;
    dieSound = GetNode("DieSound") as AudioStreamPlayer;
  }
  public override void _PhysicsProcess(float delta)
  {
    if (!isDead)
    {
      if (Input.IsActionPressed("goRight"))
      {
        charcterAnimatedSprite.FlipH = false;
        charcterAnimatedSprite.Play("Runing");
        velocity.x = speed;
      }

      else if (Input.IsActionPressed("goLeft"))
      {
        charcterAnimatedSprite.FlipH = true;
        charcterAnimatedSprite.Play("Runing");
        velocity.x = -speed;
      }
      else
      {
        velocity.x = 0;
        if (onGround)
          charcterAnimatedSprite.Play("Idle");
      }

      if (velocity.x != 0 && IsOnFloor())
      {
        if (canPlayStepSound)
        {
          stepSound.Play();
          canPlayStepSound = false;
        }
      }

      if (Input.IsActionJustPressed("jump"))
        if (jupmCount < 3)
        {
          jupmCount++;
          velocity.y = jumpPower;
          onGround = false;
          charcterAnimatedSprite.Play("Jump Start");
          jumpSound.Play();
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
      if (Input.IsActionJustPressed("gun_3") && currentGun != 3)
      {
        GetNode("gun").Free();
        gunNode = gun3.Instance() as Gun;
        gunNode.Name = "gun";
        AddChild(gunNode);
        currentGun = 3;
        gunSprite = gunNode.GetNode("Sprite") as Sprite;
      }

      if (jupmCount > 0)
        velocity.y += gravity;
      else
        velocity.y = gravity * 10;

      if (Input.IsActionPressed("shot") && gunNode.ready)
      {
        charcterAnimatedSprite.FlipH = lookVector.x < 0 ? true : false;
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
      {
        charcterAnimatedSprite.Play("Jump Loop");
        if (onGround)
        {
          onGround = false;
          jupmCount = 1;
          if (velocity.y < 0)
            charcterAnimatedSprite.Play("Jump Start");
          else
            charcterAnimatedSprite.Play("Jump Loop");
        }
      }

      lookVector = GetGlobalMousePosition() - GlobalPosition;
      gunNode.GlobalRotation = Mathf.Atan2(lookVector.y, lookVector.x);
      scale.y = lookVector.x > 0 ? 1 : -1;
      gunSprite.Scale = scale;
    }
    else
    {
      velocity.y = onGround == true ? 0 : gravity * 2;
      velocity.x = -50;
      MoveAndCollide(velocity * delta);
    }
  }
  public override void UpdateHp(float damgae)
  {
    if (damgae > 0)
      hp = hp + damgae > baseHp ? baseHp : hp + damgae;
    else
    {
      hp += damgae;
      charcterAnimatedSprite.Play("Hurt");
      screenShakePower = damgae < -40 ? 2 : 1;
      hitSound.Play();
      GetParent().GetNode<ScreenShake>("ScreenShake").ScreenShakeStart(screenShakePower, screenShakePower * 10, screenShakePower * 100);

      if (hp < 1)
        Kill();
    }
    hpBar.SetSize(new Vector2((hp / baseHp) * baseSizeHpBar.x, baseSizeHpBar.y));
  }
  public void _on_Timer_timeout()
  {
    GetTree().Root.GetNode<Gameplay>("Gameplay").ShowDeadScreen();
  }
  public void _on_StepSound_finished()
  {
    canPlayStepSound = true;
  }
}
