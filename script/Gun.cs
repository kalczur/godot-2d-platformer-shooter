using Godot;
using System;

public class Gun : Node2D
{

    protected Vector2 scale = new Vector2(-1, 1);
    protected PackedScene bulletScene = GD.Load<PackedScene>("res://scene/Bullet.tscn");
    protected Area2D bulletNode = new Area2D();
    public override void _PhysicsProcess(float delta)
    {
        var sprite = GetNode<Sprite>("Sprite");
        var lookVec = GetGlobalMousePosition() - GlobalPosition;
        GlobalRotation = Mathf.Atan2(lookVec.y, lookVec.x);

        sprite.Scale = scale;
        scale.y = lookVec.x > 0 ? 1 : -1;
    }
    public virtual void shot() { }
}