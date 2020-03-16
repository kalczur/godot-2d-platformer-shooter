using Godot;
using System;

public class NewGame : Button
{
  public override void _Ready()
  {
    GD.Print("ready");
  }

  public override void _Pressed()
  {
    GetTree().ChangeScene("res://scene/StageOne.tscn");
  }
}
