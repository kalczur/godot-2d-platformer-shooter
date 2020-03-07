using Godot;
using System;

public class Trail : Line2D
{
    [Export]
    public NodePath targetPath = new NodePath();

    public override void _Ready()
    {

    }

    public override void _Process(float delta)
    {
        // var target = GetNodePath(targetPath);
        GlobalPosition = new Vector2(0, 0);
        GlobalRotation = 0;
        // var point = target.GlobalPosition;
        //AddPoint(point);
    }

}
