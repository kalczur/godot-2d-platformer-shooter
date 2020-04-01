using Godot;
using System;

public class TitleScreenCamera : Camera2D
{
  bool where = true;

  public override void _Process(float delta)
  {
    float tempZoom = Zoom.x;
    if (where)
    {
      Zoom = new Vector2(tempZoom - 0.0001f, tempZoom - 0.0001f);
      if (tempZoom < 0.9)
        where = false;
    }
    else
    {
      Zoom = new Vector2(tempZoom + 0.001f, tempZoom + 0.001f);
      if (tempZoom >= 0.999)
        where = true;
    }
  }
}
