using Godot;
using System;

public class Bullet : Area2D
{
    private const int speed = 500;
    private Vector2 velocity = new Vector2();
    public Vector2 lookVec = new Vector2();
    private float x;
    private float y;
    public int dmg;
    public override void _Ready()
    {
        x = lookVec.Normalized().x * speed;
        y = lookVec.Normalized().y * speed;
    }

    public override void _PhysicsProcess(float delta)
    {

        velocity.x = (x * delta);
        velocity.y = (y * delta);
        Translate(velocity);
    }

    public void _on_VisibilityNotifier2D_screen_exited()
    {
        QueueFree();
    }

    public void _on_Bullet_body_entered(System.Object body)
    {
        if (body is Enemy)
        {
            Enemy tmpEnemy = (Enemy)body;
            tmpEnemy.hit(dmg);
        }
        else if (body is Player)
        {
            Player tmpEnemy = (Player)body;
            tmpEnemy.hit(dmg);
        }

        QueueFree();
    }
}
