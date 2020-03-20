using Godot;
using System;

public class Gameplay : Node2D
{
  PackedScene enemy = GD.Load("res://scene/Enemy.tscn") as PackedScene;
  PackedScene firstAidKit = GD.Load("res://scene/FirstAidKit.tscn") as PackedScene;
  PackedScene c4 = GD.Load("res://scene/C4.tscn") as PackedScene;

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
  private void SpawnC4()
  {
    var c4Node = c4.Instance() as FirstAidKit;
    c4Node.Position = new Vector2((float)GD.RandRange(40, (GetViewportRect().Size.x) - 40), -100);
    AddChild(c4Node);
  }
  public void _on_EnemySpawnTimer_timeout()
  {
    SpawnEnemy();
  }
  public void _on_FirstAidKitSpawnTimer_timeout()
  {
    SpawnFirstAidKit();
  }
  public void _on_C4pawnTimer_timeout()
  {
    SpawnC4();
  }
}
