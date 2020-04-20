using Godot;
using System;

public class Gameplay : Node2D
{
  PackedScene enemy1 = GD.Load("res://scene/Enemy1.tscn") as PackedScene;
  PackedScene enemy2 = GD.Load("res://scene/Enemy2.tscn") as PackedScene;
  PackedScene enemy3 = GD.Load("res://scene/Enemy3.tscn") as PackedScene;
  PackedScene firstAidKit = GD.Load("res://scene/FirstAidKit.tscn") as PackedScene;
  PackedScene c4 = GD.Load("res://scene/C4.tscn") as PackedScene;
  PackedScene deadScreenScene = GD.Load("res://scene/DeadScreen.tscn") as PackedScene;
  AudioStreamPlayer backgroundMusic;
  Random random = new Random();
  Player playerNode;
  float zoomScale = 0.00005f;
  float playerDistanceFromCentre;
  Camera2D camera;
  Label score;
  Timer enemySpawnTimer;

  public override void _Ready()
  {
    camera = GetNode("GameplayCamera") as Camera2D;
    playerNode = GetNode("Player") as Player;
    score = GetNode("Score") as Label;
    enemySpawnTimer = GetNode("EnemySpawnTimer") as Timer;
    backgroundMusic = GetNode("MusicBackground") as AudioStreamPlayer;
    DataManager.ReadSaveFile();
    backgroundMusic.VolumeDb = DataManager.GetMasterVolume() * DataManager.GetMusicVolume();
  }
  public override void _Process(float delta)
  {
  	// w zależności od położenia gracza
  	// kamera się przlibliża lub oddala
    playerDistanceFromCentre = (float)Math.Sqrt((float)Math.Pow((playerNode.GlobalPosition.x - 960), 2) + Math.Pow(playerNode.GlobalPosition.y - 540, 2));
    var zoom = (0.95f + zoomScale * playerDistanceFromCentre) > 1 ? 1 : (0.95f + zoomScale * playerDistanceFromCentre);
    camera.Zoom = new Vector2(zoom, zoom);
  }

  private void Spawn(PackedScene packedScene)
  {
    var sceneNode = packedScene.Instance() as Node2D;
    sceneNode.Position = new Vector2((float)GD.RandRange(40, (GetViewportRect().Size.x) - 40), -100);
    AddChild(sceneNode);
  }
  public void ShowDeadScreen()
  {
    score.Visible = false;
    GetNode<Node2D>("PlayerHpBar").Visible = false;
    GetNode<Timer>("AddPointTimer").Stop();
    GetNode<Timer>("EnemySpawnTimer").Stop();
    GetNode<Timer>("FirstAidKitSpawnTimer").Stop();
    GetNode<Timer>("C4SpawnTimer").Stop();
    GetNode<AudioStreamPlayer>("MusicBackground").Stop();
    var deadScreen = deadScreenScene.Instance() as DeadScreen;
    deadScreen.score = score.Text;
    AddChild(deadScreen);
  }
  public void _on_EnemySpawnTimer_timeout()
  {
  	// gdy timer skończy odliczanie pojawia się
  	// losowy jeden z trzech przeciwników

    int randomNumber = random.Next(100);
    if (randomNumber < 33)
      Spawn(enemy1);
    else if (randomNumber < 66)
      Spawn(enemy2);
    else
      Spawn(enemy3);

  }
  public void _on_FirstAidKitSpawnTimer_timeout()
  {
    Spawn(firstAidKit);
  }
  public void _on_C4SpawnTimer_timeout()
  {
    Spawn(c4);
  }
  public void _on_AddPointTimer_timeout()
  {
  	// w czasie gry co sekunde dodajs się punkt,
  	// muzyka w tle przyśpiesza oraz przeciwnicy
  	// coraz szybciej się pojawiają

    score.Text = $"{uint.Parse(score.Text) + 1}";
    if (enemySpawnTimer.WaitTime > 1)
      enemySpawnTimer.WaitTime -= 0.01f;
    backgroundMusic.PitchScale += 0.001f;
  }
  public void ResetGame()
  {
    GetTree().ReloadCurrentScene();
    enemySpawnTimer.WaitTime = 4;
    GetNode<Timer>("AddPointTimer").Start();
  }
}
