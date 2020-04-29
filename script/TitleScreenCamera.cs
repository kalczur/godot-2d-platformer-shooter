using Godot;
using System;

public sealed class TitleScreenCamera : Camera2D
{
    bool whereIs = true;

    public override void _Process(float delta)
    {
        //przybliżanie i oddalanie kamery w czasie
        float tempZoom = Zoom.x;
        if (whereIs)
        {
            Zoom = new Vector2(tempZoom - 0.0001f, tempZoom - 0.0001f);
            if (tempZoom < 0.9)
                whereIs = false;
        }
        else
        {
            Zoom = new Vector2(tempZoom + 0.001f, tempZoom + 0.001f);
            if (tempZoom >= 0.999)
                whereIs = true;
        }
    }
}
