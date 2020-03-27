using Godot;
using System;

public class DeadScreen : Control
{
  public string score;

  public override void _Ready()
  {
    GetNode<Label>("Score").Text = score;
  }
  public void _on_Save_pressed()
  {
    var playerName = GetNode<TextEdit>("TextEdit").Text;
    var saveGame = new File();
    saveGame.Open("user://scoreBoard.save", File.ModeFlags.ReadWrite);
    var content = saveGame.GetAsText();
    saveGame.StoreString($"{content}\n{playerName}:{score}");
    saveGame.Close();
  }
  public void _on_Retry_pressed()
  {
    QueueFree();
    GetTree().Root.GetNode<Gameplay>("Gameplay").ResetGame();
  }

  public void _on_Exit_pressed()
  {
    GetTree().ChangeScene("res://scene/TitleScreen.tscn");
  }
}
