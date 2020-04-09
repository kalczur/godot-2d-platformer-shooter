using Godot;
using System;
using System.Linq;
using System.Collections.Generic;
public class Scoreboard : Node2D
{
  private int yPositionSpawn = 0;
  DynamicFont dynamicFont = new DynamicFont();
  public override void _Ready()
  {
    dynamicFont.FontData = ResourceLoader.Load("res://font/Amatic-Bold.ttf") as DynamicFontData;
    dynamicFont.Size = 80;

    Dictionary<string, uint> scoreDictionary = new Dictionary<string, uint>();
    var saveGame = new File();

    if (!saveGame.FileExists("user://score.save"))
      return;
    saveGame.Open("user://score.save", File.ModeFlags.Read);

    while (!saveGame.EofReached())
    {
      var content = saveGame.GetLine();
      string[] separate = content.Split(':');
      scoreDictionary.Add(separate[0], uint.Parse(separate[1]));
    }

    for (int i = 0; i < 10; i++)
    {
      if (scoreDictionary.Count > i)
        addElement($"{i + 1}. {scoreDictionary.OrderByDescending(key => key.Value).ElementAt(i).Key} :   {scoreDictionary.OrderByDescending(key => key.Value).ElementAt(i).Value}");
      else
        addElement($"{i + 1}");
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
