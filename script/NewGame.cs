using Godot;
using System;

public class NewGame : Button
{
  public override void _Ready()
  {
  }

  public override void _Pressed()
  {
    GetTree().ChangeScene("res://scene/Gameplay.tscn");
  }
}
