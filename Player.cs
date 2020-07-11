using Godot;
using System;

public class Player : Area2D {

    [Export]
    public int moveSpeed = 300;
    private Vector2 _screenSize;
    private float extent;

    [Signal]
    public delegate void Hit();

    public override void _Ready() {
        _screenSize = GetViewport().Size;
        extent = GetNode<Sprite>("Sprite").Texture.GetSize().x / 2;
    }

    private void Start(Vector2 _position) {
        Position = _position;
        Show();
        GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
    }

    public override void _Process(float delta) {
        var velocity = new Vector2();

        if(Input.IsActionPressed("ui_right")) {
            velocity.x += 2f;
        }
    }
}
