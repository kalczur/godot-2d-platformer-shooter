using Godot;
using System;

public class Player : KinematicBody2D
{
    private const int speed = 400;
    private Vector2 velocity = new Vector2();
    private Vector2 floor = new Vector2(0, -1);
    private bool onGround = false;
    private const int gravity = 50;
    private const int jumpPower = -1000;
    private int jupmsCnt = 3;
    private PackedScene gun_1 = (PackedScene)GD.Load("res://scene/Gun_1.tscn");
    private PackedScene gun_2 = (PackedScene)GD.Load("res://scene/Gun_2.tscn");
    private Gun gunNode = new Gun();
    private int currentGun = 2;

    public override void _Ready()
    {
        gunNode = (Gun)gun_2.Instance();
        AddChild(gunNode);
        gunNode.Name = "gun";
        currentGun = 2;
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
        }
        if (Input.IsActionJustPressed("gun_2") && currentGun != 2)
        {
            GetNode("gun").Free();
            gunNode = (Gun)gun_2.Instance();
            gunNode.Name = "gun";
            AddChild(gunNode);
            currentGun = 2;
        }
        if (Input.IsActionJustPressed("f"))
        {
            kill();
        }
        if (Input.IsActionJustPressed("shot"))
        {
            gunNode.shot();
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

    }
    private void kill()
    {
        GetTree().ReloadCurrentScene();
    }


}
