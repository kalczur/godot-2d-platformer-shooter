using Godot;
using System;

public class Gun_2 : Node2D
{
    private Vector2 scale = new Vector2(-1, 1);
    public override void _PhysicsProcess(float delta)
    {
        var sprite = GetNode<Sprite>("Sprite");
        var lookVec = GetGlobalMousePosition() - GlobalPosition;
        GlobalRotation = Mathf.Atan2(lookVec.y, lookVec.x);

        sprite.Scale = scale;
        scale.y = lookVec.x > 0 ? 1 : -1;
    }

}