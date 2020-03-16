using Godot;
using System;

public class Gun_1 : Gun
{
  public override void _Ready()
  {
    bulletSpeed = 350;
    dmg = 20;
  }
}
