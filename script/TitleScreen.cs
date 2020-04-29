using Godot;

public sealed class TitleScreen : Control
{
    private AudioStreamPlayer backgroundMusic = new AudioStreamPlayer();

    public override void _Ready()
    {
        backgroundMusic = GetNode("AudioStreamPlayer") as AudioStreamPlayer;
    }
    public void ChangeMusicVolume(float db, float scale)
    {
        backgroundMusic.VolumeDb = db * scale;
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
