using Godot;
using System;

public class ScreenShake : Node2D
{
  public int currentShakePriority = 0;
  private Tween tween = new Tween();
  public override void _Ready()
  {
    tween = GetNode("Tween") as Tween;
  }

  public void MoveCamera(Vector2 vector2)
  {
    GetParent().GetNode<Camera2D>("GameplayCamera").Offset = new Vector2((float)GD.RandRange(-vector2.x, vector2.x), (float)GD.RandRange(-vector2.y, vector2.y));
  }
  public void ScreenShakeStart(int shakeLength, int shakePower, int shakePriority)
  {
    currentShakePriority = shakePriority;
    tween.InterpolateMethod(this, "MoveCamera", new Vector2(shakePower, shakePower), new Vector2(0, 0), shakeLength, Tween.TransitionType.Sine, Tween.EaseType.Out, 0);
    tween.Start();

  }
}
