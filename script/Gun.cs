using Godot;
using System;

public class Gun : Node2D
{
  [Export]
  public float damage = 20;
  [Export]
  public int bulletSpeed = 500;
  protected PackedScene bulletScene = GD.Load<PackedScene>("res://scene/Bullet.tscn");
  protected Bullet bulletNode = new Bullet();

  public bool ready;
  public virtual void shot(Vector2 where, float damage)
  {
    bulletNode = (Bullet)bulletScene.Instance();
    bulletNode.damage = damage;
    bulletNode.speed = bulletSpeed;
    bulletNode.lookVector = where;
    bulletNode.Position = GetNode<Position2D>("Position2D").GlobalPosition;
    GetTree().Root.GetNode("Gameplay").AddChild(bulletNode);
    ready = false;
  }
  public void _on_Reload_timeout()
  {
    ready = true;
  }
}
