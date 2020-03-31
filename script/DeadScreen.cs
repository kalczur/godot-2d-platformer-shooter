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
    if (saveGame.FileExists("user://score.save"))
    {
      saveGame.Open("user://score.save", File.ModeFlags.ReadWrite);
      var content = saveGame.GetAsText();
      saveGame.StoreString($"{content}\n{playerName}:{score}");
    }
    else
    {
      saveGame.Open("user://score.save", File.ModeFlags.Write);
      saveGame.StoreString($"{playerName}:{score}");
    }
    saveGame.Close();

    GetTree().ChangeScene("res://scene/TitleScreen.tscn");
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
