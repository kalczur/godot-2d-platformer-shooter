using Godot;
using System;

public class Gun_2 : Gun
{
    public override void shot()
    {
        bulletNode = (Area2D)bulletScene.Instance();
        bulletNode.Position = GetNode<Position2D>("Position2D").GlobalPosition;
        GetTree().Root.GetNode("StageOne").AddChild(bulletNode);
    }
}