using Godot;
using System;

public class GameplayBackground : ParallaxBackground
{
  private Sprite back1;
  private Sprite back2;
  private Sprite fore1;
  private Sprite fore2;

  private Vector2 position;
  public override void _Ready()
  {
    back1 = GetNode("Back1/Sprite") as Sprite;
    back2 = GetNode("Back2/Sprite") as Sprite;
    fore1 = GetNode("Fore1/Sprite") as Sprite;
    fore2 = GetNode("Fore2/Sprite") as Sprite;
  }

  public override void _Process(float delta)
  {
    back1.Offset = new Vector2(back1.Offset.x - 0.5f, -100);
    back2.Offset = new Vector2(back2.Offset.x - 0.5f, -100);
    fore1.Offset = new Vector2(fore1.Offset.x - 1, -100);
    fore2.Offset = new Vector2(fore2.Offset.x - 1, -100);

    if (back1.Offset.x == -1280)
      back1.Offset = new Vector2(1280, -100);
    if (back2.Offset.x == -1280)
      back2.Offset = new Vector2(1280, -100);

    if (fore1.Offset.x == -1280)
      fore1.Offset = new Vector2(1280, -100);
    if (fore2.Offset.x == -1280)
      fore2.Offset = new Vector2(1280, -100);
  }
}
