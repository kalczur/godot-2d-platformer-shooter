using Godot;
using System.IO;

public class DeadScreen : Control
{
    public string score;
    private bool found = false;
    private const string SCORE_FILE_PATH = "user://score.save";

    public override void _Ready()
    {
        GetNode<Label>("Score").Text = score;
    }
    public void _on_Save_pressed()
    {
        var playerName = GetNode<TextEdit>("TextEdit").Text;
        var saveGame = new Godot.File();
        if (saveGame.FileExists(SCORE_FILE_PATH))
        {
            saveGame.Open("user://score.save", Godot.File.ModeFlags.ReadWrite);
            string[] content = saveGame.GetAsText().Split("\n");

            //sprawdza czy gracz już istnieje na liście
            //jeśli znajdzie gracza i nowy wynik jest lepszy to go nadpisuje
            for (int i = 0; i < content.Length; i++)
            {
                string[] separated = content[i].Split(':');
                if (separated[0] == playerName)
                {
                    found = true;
                    if (uint.Parse(separated[1]) < uint.Parse(score))
                    {
                        content[i] = $"{playerName}:{score}";
                        saveGame.StoreString($"{string.Join("\n", content)}");
                        break;
                    }
                }
            }
            if (!found)
                saveGame.StoreString($"{string.Join("\n", content)}\n{playerName}:{(score != "" ? score : "0")}");
        }
        else
        {
            saveGame.Open("user://score.save", Godot.File.ModeFlags.Write);
            saveGame.StoreString($"{playerName}:{(score != "" ? score : "0")}");
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
