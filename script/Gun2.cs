using Godot;
using System;

public class Gun2 : Gun
{
  public override void _Ready()
  {
    bulletSpeed = 500;
    damage = 20;
  }
}