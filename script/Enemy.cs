using Godot;
using System;
public class Enemy : KinematicBody2D
{
    private const int speed = 100;
    private const int gravity = 50;
    private Vector2 velocity = new Vector2();
    private Vector2 floor = new Vector2(0, -1);
    public int hp = 100;
    private PackedScene gun_1 = (PackedScene)GD.Load("res://scene/Gun_1.tscn");
    private Gun gunNode = new Gun();
    public override void _Ready()
    {
        gunNode = (Gun)gun_1.Instance();
        AddChild(gunNode);
    }
    public override void _PhysicsProcess(float delta)
    {
        if (hp <= 0)
        {
            kill();
        }

        var vecToPlayer = GetTree().Root.GetNode<KinematicBody2D>("StageOne/Player").GlobalPosition - GlobalPosition;

        if (Math.Abs(vecToPlayer.x) > 200)
            velocity.x = vecToPlayer.x > 0 ? speed : -speed;
        else
            velocity.x = 0;

        velocity.y += gravity;
        MoveAndSlide(velocity, floor);
        if (IsOnWall())
            velocity.y = -600;
    }
    private void _on_Timer_timeout()
    {
        gunNode.shot();
    }
    public void hit(int dmg)
    {
        hp -= dmg;
        velocity.y = -300;
    }
    public void kill()
    {
        QueueFree();
    }
}
