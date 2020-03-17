
using Godot;
using System;

public class Gun1 : Gun
{
  public override void _Ready()
  {
    bulletSpeed = 350;
    damage = 20;
  }
}
