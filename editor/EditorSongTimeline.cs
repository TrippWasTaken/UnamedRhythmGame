using System;
using Godot;

public partial class EditorSongTimeline : HFlowContainer
{
    private Conducter conducter;

    [Export]
    public RichTextLabel editorSongInfoText;

    [Export]
    public ProgressBar progressBar;

    [Export]
    public Button playStopButton;

    private int infoBar;
    private int infoBeat;

    private double infoTime;
    private double infoBPM;
    private TimeSpan infoTimeSpan;
    private string infoTimeFormat;

    private double progressTime;

    private void LabelText()
    {
        infoTimeSpan = TimeSpan.FromMilliseconds(infoTime / 1000);
        infoTimeFormat = infoTimeSpan.ToString(@"mm\:ss\.ffff");
        editorSongInfoText.Text =
            $"Time: {infoTimeFormat}\nBar: {infoBar}\t\tBeat: {infoBeat}\n{infoBPM} BPM";
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        conducter = GetTree().CurrentScene.GetNode<Conducter>("Conducter");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (conducter.Playing)
        {
            infoTime = conducter.SongPosition;
            infoBar = conducter.CurrBar;
            infoBeat = conducter.SongBeatInt;
            infoBPM = conducter.SongBPM;
            LabelText();

            progressTime = conducter.GetPlaybackPosition() / conducter.Stream.GetLength() * 100;
            progressBar.Value = progressTime;

            GD.Print(progressTime, progressBar.Value);
        }
    }
}
