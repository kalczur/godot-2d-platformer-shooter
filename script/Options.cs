using Godot;
using System;

public class Options : Node2D
{
  public void _on_Back_pressed()
  {
    GetParent().GetNode<Camera2D>("TitleScreenCamera").Offset = new Vector2(0, 0);
  }
}
