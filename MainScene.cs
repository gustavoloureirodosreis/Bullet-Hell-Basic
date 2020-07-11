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

    private float RandRange(float min, float max) {
        return (float)_random.NextDouble() * (max-min) + min;
    }

    private void SpawnEnemies() {
        double _enemiesPerRow = Math.Round(_screenSize.x / (_enemySpacing * 1.2f));
        var _startingPos = new Vector2(0, _enemySpacing);

        // Spawn enemies
        for (int r = 0; r < numOfRows; r++) {
            for (int i = 0; i < _enemiesPerRow; i++) {
                var enemyInstance = (RigidBody2D)Enemy.Instance();
                AddChild(enemyInstance);

                enemyInstance.Position = _startingPos + new Vector2(_enemySpacing * (i+1), _enemySpacing * r);
                Enemies.Add(enemyInstance);
            }
        }

        // Start Timer
        GetNode<Timer>("EnemyShoot").Start();
    }

    private void _on_HUD_StartGame() {
        Score = 0;
        GetNode<HUD>("HUD").UpdateScore(Score);

        // Set start position
        var player = GetNode<Player>("Player");
        var startPosition = GetNode<Position2D>("StartPosition");

        player.Start(startPosition.Position);

        // Spawn Enemies
        SpawnEnemies();

        // Start Score
        GetNode<Timer>("ScoreTimer").Start();
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

    private void GameOver() {
        GetNode<Timer>("ScoreTimer").Stop();
        GetNode<Timer>("EnemyShoot").Stop();
        GetNode<HUD>("HUD").ShowGameOver();

        GetTree().CallGroup("bullets", "queue_free");

        foreach (var enemy in Enemies) {
            enemy.QueueFree();
        }

        Enemies.Clear();
    }

}
