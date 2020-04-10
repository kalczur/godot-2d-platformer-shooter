public class OptionDataModel
{
  public bool FullScreen { get; set; }
  public float MasterVolume { get; set; } //tu mnożnik ogólnej
  public float EffectsVolume { get; set; } //+- db do efektów
  public float MusicVolume { get; set; } //+- db do muzyki
}
