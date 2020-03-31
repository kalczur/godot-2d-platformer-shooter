using Godot;
using System;

public class Scoreboard : Node2D
{
  private int yPositionSpawn = 0;
  DynamicFont dynamicFont = new DynamicFont();
  public override void _Ready()
  {
    dynamicFont.FontData = ResourceLoader.Load("res://font/Amatic-Bold.ttf") as DynamicFontData;
    dynamicFont.Size = 80;

    var saveGame = new File();
    if (!saveGame.FileExists("user://score.save"))
      return;

    saveGame.Open("user://score.save", File.ModeFlags.Read);

    for (int i = 1; i < 11; i++)
    {
      if (!saveGame.EofReached())
      {
        var content = saveGame.GetLine();
        string[] separate = content.Split(':');
        addElement($"{i}.{separate[0]}:  {separate[1]}");
      }
      else
        addElement($"{i}.");
    }
    saveGame.Close();
  }
  public void _on_Back_pressed()
  {
    GetParent().GetNode<Camera2D>("TitleScreenCamera").Offset = new Vector2(0, 0);
  }
  private void addElement(string stringLabel)
  {
    Label label = new Label();

    label.RectSize = new Vector2(1000, 80);
    label.RectPosition = new Vector2(440, yPositionSpawn += 80);
    label.Align = Godot.Label.AlignEnum.Center;
    label.AddFontOverride("font", dynamicFont);
    label.Text = stringLabel;

    AddChild(label);
  }
}
