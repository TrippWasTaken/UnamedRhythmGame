using System;
using Godot;

public partial class PlayStopButton : Button
{
    private Conducter conducter;

    // Called when the node enters the scene tree for the first time.
    private void ButtonToggle(bool pressed)
    {
        if (pressed)
        {
            GD.Print("you should be playing");
            Text = "Stop";
            PlayToggle(true);
        }
        else
        {
            Text = "Play";
            PlayToggle(false);
        }
    }

    private void PlayToggle(bool play)
    {
        if (play)
        {
            conducter.InitializeProperties();
            conducter.PlayBackStart();
        }
        else
        {
            conducter.Stop();
        }
    }

    public override void _Ready()
    {
        conducter = GetTree().CurrentScene.GetNode<Conducter>("Conducter");
        Pressed += () => ButtonToggle(ButtonPressed);
        ButtonToggle(false);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) { }
}
