using Godot;
using System;

public class Gameplay : Node2D
{
  PackedScene enemy1 = GD.Load("res://scene/Enemy1.tscn") as PackedScene;
  PackedScene enemy2 = GD.Load("res://scene/Enemy2.tscn") as PackedScene;
  PackedScene enemy3 = GD.Load("res://scene/Enemy3.tscn") as PackedScene;
  PackedScene firstAidKit = GD.Load("res://scene/FirstAidKit.tscn") as PackedScene;
  PackedScene c4 = GD.Load("res://scene/C4.tscn") as PackedScene;
  Random random = new Random();
  Label score;
  Timer enemySpawnTimer;

  public override void _Ready()
  {
    score = GetNode("Score") as Label;
    enemySpawnTimer = GetNode("EnemySpawnTimer") as Timer;
  }

  private void Spawn(PackedScene packedScene)
  {
    var sceneNode = packedScene.Instance() as Node2D;
    sceneNode.Position = new Vector2((float)GD.RandRange(40, (GetViewportRect().Size.x) - 40), -100);
    AddChild(sceneNode);
  }
  public void _on_EnemySpawnTimer_timeout()
  {
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
    score.Text = $"{uint.Parse(score.Text) + 1}";
    if (enemySpawnTimer.WaitTime > 1)
      enemySpawnTimer.WaitTime -= 0.01f;
  }
}
