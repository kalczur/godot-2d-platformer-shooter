using Godot;
using System.Linq;
using System.Collections.Generic;

public sealed class Scoreboard : Node2D
{
    private int yPositionSpawn = 0;
    private DynamicFont dynamicFont = new DynamicFont();
    private const string SCORE_FILE_PATH = "user://score.save";
    private const string AMATIC_FONT_FILE_PATH = "res://font/Amatic-Bold.ttf";

    public override void _Ready()
    {
        dynamicFont.FontData = ResourceLoader.Load(AMATIC_FONT_FILE_PATH) as DynamicFontData;
        dynamicFont.Size = 80;

        ReadFile(SCORE_FILE_PATH);
    }
    private void ReadFile(string path)
    {
        Dictionary<string, uint> scoreDictionary = new Dictionary<string, uint>();
        var file = new File();

        if (!file.FileExists(path))
        {
            for (int i = 0; i < 10; i++)
                AddElement($"{i + 1}.");
            return;
        }

        file.Open(path, File.ModeFlags.Read);

        while (!file.EofReached())
        {
            var content = file.GetLine();
            string[] separate = content.Split(':');
            scoreDictionary.Add(separate[0], uint.Parse(separate[1]));
        }

        //wypisuje do 10 najlepszych wyników
        for (int i = 0; i < 10; i++)
        {
            if (scoreDictionary.Count > i)
                AddElement($"{i + 1}. {scoreDictionary.OrderByDescending(key => key.Value).ElementAt(i).Key} :   {scoreDictionary.OrderByDescending(key => key.Value).ElementAt(i).Value}");
            else
                AddElement($"{i + 1}.");
        }

        file.Close();
    }
    private void AddElement(string stringLabel)
    {
        Label label = new Label();

        label.RectSize = new Vector2(1000, 80);
        label.RectPosition = new Vector2(440, yPositionSpawn += 80);
        label.Align = Godot.Label.AlignEnum.Center;
        label.AddFontOverride("font", dynamicFont);
        label.Text = stringLabel;

        AddChild(label);
    }

    public void _on_Back_pressed()
    {
        GetParent().GetNode<Camera2D>("TitleScreenCamera").Offset = new Vector2(0, 0);
    }
}
