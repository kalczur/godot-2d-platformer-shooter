using Godot;
using System;

public class Lava : Area2D
{

  public void _on_Lava_body_entered(System.Object body)
  {
    if (body is Character)
    {
      Character character = body as Character;
      character.Kill();
    }
  }

}
