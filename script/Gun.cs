using Godot;
using System;

public class Gun : Node2D
{
    [Export]
    public float damage = 20;
    [Export]
    public int bulletSpeed = 500;
    protected PackedScene bulletScene = GD.Load<PackedScene>("res://scene/Bullet.tscn");
    protected Bullet bulletNode = new Bullet();

    AudioStreamPlayer shootSound;

    AnimatedSprite animatedSprite = new AnimatedSprite();
    public bool ready;

    public override void _Ready()
    {
        animatedSprite = GetNode("FireAnimatedSprite") as AnimatedSprite;
        shootSound = GetNode("ShootSound") as AudioStreamPlayer;
        shootSound.VolumeDb = DataManager.GetMasterVolume() * DataManager.GetEffectsVolume();
    }
    public virtual void shot(Vector2 where, float damage)
    {
        animatedSprite.Play("Fire");
        shootSound.Play();
        bulletNode = (Bullet)bulletScene.Instance();
        bulletNode.damage = damage;
        bulletNode.speed = bulletSpeed;
        bulletNode.lookVector = where;
        bulletNode.Position = GetNode<Position2D>("Position2D").GlobalPosition;
        GetTree().Root.GetNode("Gameplay").AddChild(bulletNode);
        ready = false;
    }
    public void _on_Reload_timeout()
    {
        ready = true;
    }
    public void _on_FireAnimatedSprite_animation_finished()
    {
        animatedSprite.Stop();
        animatedSprite.Frame = 0;
    }

}
