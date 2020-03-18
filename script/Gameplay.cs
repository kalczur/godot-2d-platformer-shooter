using Godot;
using System;

public class Gameplay : Node2D
{
  PackedScene enemy = GD.Load("res://scene/Enemy.tscn") as PackedScene;
  public override void _Ready()
  {

  }
  private void SpawnEnemy()
  {

    var enemyNode = enemy.Instance() as Enemy;
    enemyNode.Position = new Vector2((float)GD.RandRange(40, (GetViewportRect().Size.x) - 40), -100);
    AddChild(enemyNode);


  }
  public void _on_EnemySpawnTimer_timeout()
  {
    SpawnEnemy();
  }
}
