using System;
using Godot;

public partial class Conducter : AudioStreamPlayer
{
    private double _timeBegin;
    private double _timeDelay;

    [Export]
    private static double songBPM = 170;
    double secPerBeat;
    double firstBeatOffset = 1200000.0;
    double timeTillNextBeat;
    double songBeatFloat;
    int songBeatInt;
    int clickTrack = 0;
    public int currBar = 0;
    private RichTextLabel testLabel;
    private AudioStreamPlayer click1,
        click2;

    public override void _Ready()
    {
        testLabel = this.GetNode<RichTextLabel>("RichTextLabel");
        click1 = this.GetNode<AudioStreamPlayer>("AudioStreamPlayer");
        click2 = this.GetNode<AudioStreamPlayer>("AudioStreamPlayer2");
        secPerBeat = 60.0d / songBPM;
        _timeBegin = Time.GetTicksUsec();
        _timeDelay = AudioServer.GetTimeToNextMix() + AudioServer.GetOutputLatency();
        VolumeDb = -15.0f;
        Play();
        GD.Print(_timeBegin);
    }

    public override void _Process(double delta)
    {
        double songPosition = -firstBeatOffset + Time.GetTicksUsec() - _timeBegin;
        double timeInSec = songPosition / 1000000.0d;
        songBeatFloat = timeInSec / secPerBeat;
        songBeatInt = (int)Math.Floor(songBeatFloat);

        if (Playing)
        {
            if (clickTrack <= songBeatInt)
            {
                click1.Play();
                clickTrack++;
            }
            if (songBeatInt % 4 == 0 && currBar <= songBeatInt / 4)
            {
                click2.Play();
                currBar++;
            }
        }
        testLabel.Text =
            $"bar: {currBar}\ttime: {timeInSec:F2} \tBeat: {songBeatInt} \tBPM: {songBPM}";
    }
}
