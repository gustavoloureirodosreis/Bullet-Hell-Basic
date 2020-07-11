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

    }

    public override void _Process(float delta) {

    }
}
