using Godot;
using System;

public class Gun_2 : Gun
{
  public override void _Ready()
  {
    bulletSpeed = 500;
    dmg = 24;
  }
}