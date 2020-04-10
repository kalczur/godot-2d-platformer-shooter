using Godot;
using Newtonsoft.Json;

public class DataManager : Node
{
  private const string SAVE_PATH = "user://config.json";

  private static OptionDataModel data = new OptionDataModel();

  public override void _Ready()
  {
    ReadSaveFile();
  }

  public static void Save()
  {
    WriteSaveFile();
  }

  public static void ReadSaveFile()
  {
    string jsonString = null;
    var saveFile = OpenSaveFile(File.ModeFlags.Read);
    if (saveFile != null)
    {
      jsonString = saveFile.GetLine();
      try
      {
        data = Deserialize(jsonString);
      }
      catch
      {
        data = new OptionDataModel();
      }
      saveFile.Close();
    }
  }

  public static void SetFullScreen(bool isChecked)
  {
    data.FullScreen = isChecked;
  }
  public static void SetMasterVolume(float masterVolume)
  {
    data.MasterVolume = masterVolume;
  }
  public static void SetMusicVolume(float musicVolume)
  {
    data.MusicVolume = musicVolume;
  }
  public static void SetEffectsVolume(float effectsVolume)
  {
    data.EffectsVolume = effectsVolume;
  }

  public static bool GetFullScreen() => data.FullScreen;
  public static float GetMasterVolume() => data.MasterVolume;
  public static float GetMusicVolume() => data.MusicVolume;
  public static float GetEffectsVolume() => data.EffectsVolume;

  private static void WriteSaveFile()
  {
    var saveFile = OpenSaveFile(File.ModeFlags.Write);
    if (saveFile != null)
    {
      var json = JsonConvert.SerializeObject(data);
      saveFile.StoreLine(json);
      saveFile.Close();
    }
  }

  private static File OpenSaveFile(File.ModeFlags flag = File.ModeFlags.Read)
  {
    var saveFile = new File();
    var error = saveFile.Open(SAVE_PATH, flag);
    if (error == 0)
    {
      return saveFile;
    }
    return null;
  }

  private static OptionDataModel Deserialize(string json)
  {
    return JsonConvert.DeserializeObject<OptionDataModel>(json);
  }
}