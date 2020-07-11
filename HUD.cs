using Godot;
using System;

public class HUD : CanvasLayer {

    [Signal]
    public delegate void StartGame();

    public override void _Ready() {
        
    }

    private void _on_btnStart_pressed() {
        var message = GetNode<Label>("Message");
        message.Text = "";

        GetNode<Button>("btnStart").Hide();
        EmitSignal("StartGame");
    }

    public void UpdateScore(int score) {
        GetNode<Label>("Score").Text = score.ToString() + " seconds";

    }

    async public void ShowGameOver() {
        var message = GetNode<Label>("Message");
        message.Text = "Restart?";

        await ToSignal(GetTree().CreateTimer(1), "timeout");

        var button = GetNode<Button>("btnStart");
        button.Text = "Restart";
        button.Show();
    }
}
