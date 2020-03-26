using Godot;
using System;

public class DeadScreen : Control
{
  public override void _Ready()
  {
    var score = GetNode("Score") as Label;
    //score.Text = GetTree().Root.GetNode<Label>("Gameplay/Score").Text;
  }
  public void _on_Retry_pressed()
  {
    GetTree().ChangeScene("res://scene/Gameplay.tscn");
  }

  public void _on_Exit_pressed()
  {
    GetTree().Quit();
  }
}
