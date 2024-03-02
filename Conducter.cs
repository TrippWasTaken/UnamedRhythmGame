using System;
using Godot;

public partial class Conducter : AudioStreamPlayer
{
    private double _timeBegin;
    private double _timeDelay;

    [Export]
    public double SongBPM { get; private set; } = 170.0;
    double secPerBeat;
    public double firstBeatOffset = 1200000.0;
    double timeTillNextBeat;
    double songBeatFloat;
    public int SongBeatInt { get; private set; }
    int clickTrack = 0;
    public int CurrBar { get; private set; } = 0;
    private RichTextLabel testLabel;
    private AudioStreamPlayer click1,
        click2;
    double timeInSec;
    public double SongPosition { get; private set; }

    public void PlayBackStart()
    {
        _timeBegin = Time.GetTicksUsec();
        _timeDelay = AudioServer.GetTimeToNextMix() + AudioServer.GetOutputLatency();
        GD.Print($"Audio Play time begin: {_timeBegin} with delay: {_timeDelay}");
        Play();
    }

    public void InitializeProperties()
    {
        secPerBeat = 60.0d / SongBPM;
        SongPosition = 0;
        timeInSec = 0;
        CurrBar = 0;
        clickTrack = 0;
        VolumeDb = -15.0f;
    }

    public override void _Ready()
    {
        testLabel = GetTree().CurrentScene.GetNode<RichTextLabel>("InfoLabel");
        click1 = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
        click2 = GetNode<AudioStreamPlayer>("AudioStreamPlayer2");
    }

    public override void _Process(double delta)
    {
        if (testLabel != null)
        {
            testLabel.Text =
                $"bar: {CurrBar}\ttime: {timeInSec:F2} \tBeat: {SongBeatInt} \tBPM: {SongBPM}";
        }
        if (Playing)
        {
            SongPosition = -firstBeatOffset + Time.GetTicksUsec() - _timeBegin;
            timeInSec = SongPosition / 1000000.0d;
            songBeatFloat = timeInSec / secPerBeat;
            SongBeatInt = (int)Math.Floor(songBeatFloat);

            if (clickTrack <= SongBeatInt)
            {
                click1.Play();
                clickTrack++;
            }
            if (SongBeatInt % 4 == 0 && CurrBar <= SongBeatInt / 4)
            {
                click2.Play();
                CurrBar++;
            }
        }
    }
}
