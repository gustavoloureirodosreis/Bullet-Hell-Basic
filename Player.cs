using Godot;
using System;

public class Player : Area2D {

    [Export]
    public int moveSpeed = 300;
    [Export]
    public float moveVectorAmount = 2f;
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
            velocity.x += moveVectorAmount;
        }
        if(Input.IsActionPressed("ui_left")) {
            velocity.x -= moveVectorAmount;
        }

        if (velocity.Length() > 0) {
            velocity = velocity.Normalized() * moveSpeed;
        }

        Position += velocity * delta;
        Position = new Vector2(
            x: Mathf.Clamp(Position.x, 0 + extent, _screenSize.x - extent),
            y: Mathf.Clamp(Position.y, 0, _screenSize.y)
        );
    }

    private void _on_Player_body_entered(object body) {
        Hide();
        EmitSignal("Hit");
        GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("disabled", true);

        try {
            // var b = ()
        } catch (Exception) {
            GD.Print("ERRO!");
        }
    }
}
