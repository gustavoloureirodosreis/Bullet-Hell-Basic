using Godot;
using System;
using System.Collections.Generic;

public class MainScene : Node2D {

    [Export]
    public PackedScene Enemy;
    [Export]
    public PackedScene Bullet;
    [Export]
    public int numOfRows;
    [Export]
    public int minShootTime;
    [Export]
    public int maxShootTime;
    [Export]
    public float minBulletVelocity;
    [Export]
    public float maxBulletVelocity;
    
    private Vector2 _screenSize;
    private float _enemySpacing = 65;
    private List<RigidBody2D> Enemies = new List<RigidBody2D>();
    private Random _random = new Random();
    private int Score;

    public override void _Ready() {
        _screenSize = GetViewport().Size;
        GetNode<Player>("Player").Hide();
        GetNode<Timer>("EnemyShoot").WaitTime = RandRange(1, 2);
    }

    public override void _Process(float delta) {
        
    }

    private float RandRange(float min, float max) {
        return (float)_random.NextDouble() * (max-min) + min;
    }

    private void _on_HUD_StartGame() {

    }

    private void _on_EnemyShoot_timeout() {
        foreach (var enemy in Enemies) {
            var bulletInstance = (RigidBody2D)Bullet.Instance();
            AddChild(bulletInstance);

            float direction = 33;
            bulletInstance.GlobalPosition = enemy.GlobalPosition;
            bulletInstance.Rotation = direction;
            bulletInstance.LinearVelocity = new Vector2(RandRange(minBulletVelocity, maxBulletVelocity), 0).Rotated(direction);

            GetNode<Timer>("EnemyShoot").WaitTime = RandRange(minShootTime, maxShootTime);

        }
    }

    private void _on_ScoreTimer_timeout() {
        Score++;
        GetNode<HUD>("HUD").UpdateScore(Score);
    }

}
