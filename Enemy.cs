using Godot;
using System;

public class Enemy : RigidBody2D {

    private float _enemyWidth;
    private float extent;
    private int MovementLoc;

    [Export]
    private float MovementAmount = 75;

    public override void _Ready() {
        _enemyWidth = GetNode<Sprite>("Sprite").Texture.GetSize().x / 4;
        extent = _enemyWidth / 2;
    }

    public override void _Process(float delta) {
    
    }

    private void _on_VisibilityNotifier2D_screen_exited() {
        QueueFree();
    }

    private void _on_Movement_timeout() {
        if (MovementLoc == 0) {
            Position += new Vector2(MovementAmount, 0);
            MovementLoc = 1;
        }
        else if (MovementLoc == 1) {
            Position += new Vector2(0, (_enemyWidth + extent));
            MovementLoc = 2;
        }
        else if (MovementLoc == 2) {
            Position += new Vector2(-MovementAmount, 0);
            MovementLoc = 3;
        }
        else if (MovementLoc == 3) {
            Position += new Vector2(0, -(_enemyWidth + extent));
            MovementLoc = 0;
        }
    }
}
