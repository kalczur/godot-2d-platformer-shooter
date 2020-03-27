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
  public void _on_Options_pressed()
  {
    GetNode<Camera2D>("TitleScreenCamera").Offset = new Vector2(-1920, 0);
  }
  public void _on_Scoreboard_pressed()
  {
    GetNode<Camera2D>("TitleScreenCamera").Offset = new Vector2(1920, 0);
  }
  public void _on_Exit_pressed()
  {
    GetTree().Quit();
  }
}
