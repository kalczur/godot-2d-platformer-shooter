using Godot;
public class Options : Node2D
{
  private float scale = 1;
  private TitleScreen titleScreen;
  private HSlider masterSLider;
  private HSlider effectsSLider;
  private HSlider musicSLider;

  public override void _Ready()
  {
    titleScreen = GetTree().Root.GetNode("TitleScreen") as TitleScreen;
    masterSLider = GetNode("MasterVolume/HSlider") as HSlider;
    effectsSLider = GetNode("EffectsVolume/HSlider") as HSlider;
    musicSLider = GetNode("MusicVolume/HSlider") as HSlider;
    DataManager.ReadSaveFile();
    Initialize();
  }
  private void Initialize()
  {
    masterSLider.Value = DataManager.GetMasterVolume() != 0 ? 2f - DataManager.GetMasterVolume() : 1;
    scale = 2f - (float)masterSLider.Value;
    effectsSLider.Value = DataManager.GetEffectsVolume() != 0 ? DataManager.GetEffectsVolume() : -50;
    musicSLider.Value = DataManager.GetMusicVolume() != 0 ? DataManager.GetMusicVolume() : -50;
    UpdateTitleScreenMusciVolume((float)musicSLider.Value, scale);
    DataManager.Save();
  }
  public void _on_MasterValueHSlider_value_changed(float value)
  {
    scale = 2f - value;
    UpdateTitleScreenMusciVolume((float)musicSLider.Value, scale);
  }
  public void _on_MusicValueHSlider_value_changed(float value)
  {
    UpdateTitleScreenMusciVolume((float)musicSLider.Value, scale);
  }
  public void _on_FullScreenCheckBox_toggled(bool isPressed)
  {
    // TO DO
    // Nie wiem czy jest sens to robic
  }
  public void _on_Back_pressed()
  {
    GetParent().GetNode<Camera2D>("TitleScreenCamera").Offset = new Vector2(0, 0);
    DataManager.SetMasterVolume(scale);
    DataManager.SetEffectsVolume((float)effectsSLider.Value);
    DataManager.SetMusicVolume((float)musicSLider.Value);
    DataManager.Save();
  }

  private void UpdateTitleScreenMusciVolume(float db, float scale)
  {
    titleScreen.ChangeMusicVolume(db, scale);
  }
}
