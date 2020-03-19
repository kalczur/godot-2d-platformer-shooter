using Godot;
using System;

public class Gameplay : Node2D
{
  PackedScene enemy = GD.Load("res://scene/Enemy.tscn") as PackedScene;
  PackedScene firstAidKit = GD.Load("res://scene/FirstAidKit.tscn") as PackedScene;

  private void SpawnEnemy()
  {
    var enemyNode = enemy.Instance() as Enemy;
    enemyNode.Position = new Vector2((float)GD.RandRange(40, (GetViewportRect().Size.x) - 40), -100);
    AddChild(enemyNode);
  }
  private void SpawnFirstAidKit()
  {
    var firstAidKitNode = firstAidKit.Instance() as FirstAidKit;
    firstAidKitNode.Position = new Vector2((float)GD.RandRange(40, (GetViewportRect().Size.x) - 40), -100);
    AddChild(firstAidKitNode);
  }
  public void _on_EnemySpawnTimer_timeout()
  {
    SpawnEnemy();
  }
  public void _on_FirstAidKitSpawnTimer_timeout()
  {
    SpawnFirstAidKit();
  }
}
