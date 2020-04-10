using Godot;
using System;
using SC = System.Collections.Generic;
using GC = Godot.Collections;

public class test : Node2D
{
  string jsonData;
  public override void _Ready()
  {
    var parseResult = GetJSONParseResult("user://test.json");
    var sceneData = parseResult.Result as GC.Array;
    for (int i = 0; i < sceneData.Count; i++)
    {
      var Data = sceneData[i] as GC.Dictionary;
      jsonData = Data["scene" + i] as String;
      GD.Print(jsonData);
    }
  }
  private JSONParseResult GetJSONParseResult(string localFileName)
  {
    var file = new File();
    file.Open(localFileName, Godot.File.ModeFlags.Read);  // 1 is read only
    var text = file.GetAsText();
    file.Close();

    var result = JSON.Parse(text);

    if (result.Error != 0)
    {
      GD.Print($"There was an error: {result.Error}");
      return null;
    }
    else
    {
      GD.Print("JSON file read ok.");
      return result;
    }
  }
}
