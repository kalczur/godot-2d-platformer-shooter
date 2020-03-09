using Godot;
using System;

public class EnemyGun : Node2D
{

    protected Vector2 scale = new Vector2(-1, 1);
    protected PackedScene bulletScene = GD.Load<PackedScene>("res://scene/Bullet.tscn");
    protected Bullet bulletNode = new Bullet();
    protected int dmg;
    public override void _PhysicsProcess(float delta)
    {
        var sprite = GetNode<Sprite>("Sprite");
        var lookVec = GetTree().Root.GetNode<Player>("StageOne/Player").GlobalPosition - GlobalPosition;
        GlobalRotation = Mathf.Atan2(lookVec.y, lookVec.x);

        sprite.Scale = scale;
        scale.y = lookVec.x > 0 ? 1 : -1;
    }
    public void shot()
    {
        bulletNode = (Bullet)bulletScene.Instance();
        bulletNode.Position = GetNode<Position2D>("Position2D").GlobalPosition;
        GetTree().Root.GetNode("StageOne").AddChild(bulletNode);
        bulletNode.dmg = dmg;
    }
}