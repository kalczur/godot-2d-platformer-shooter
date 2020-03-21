using Godot;
using System;

public class Exit : Button
{
  public override void _Pressed()
  {
    GetTree().Quit();
  }
}