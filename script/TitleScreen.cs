using Godot;
using System;

public class TitleScreen : Control
{
  public override void _Ready()
  {

  }
  public void _on_NewGame_pressed()
  {
    GetTree().ChangeScene("res://scene/Gameplay.tscn");
  }


  public void _on_Exit_pressed()
  {
    GetTree().Quit();
  }
}
