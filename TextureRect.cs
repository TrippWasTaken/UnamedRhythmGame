using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Godot;
using NAudio;
using NAudio.Wave;
using NAudio.WaveFormRenderer;

public partial class TextureRect : Godot.TextureRect
{
    long songLength;
    AudioFileReader songFile;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var averagePeakProvider = new AveragePeakProvider(0.5f); // e.g. 4
        songFile = new AudioFileReader("./testSong1.mp3");
        songLength = songFile.Length;
        GD.Print("Hello test len: ", (int)songLength);

        // 65230816
        StandardWaveFormRendererSettings myRendererSettings = new StandardWaveFormRendererSettings
        {
            Width = (int)(songLength / 10000),
            TopHeight = 120,
            BottomHeight = 0,
            TopPeakPen = Pens.White,
            BottomPeakPen = Pens.White,
            BackgroundColor = System.Drawing.Color.Transparent,
        };

        WaveFormRenderer renderer = new WaveFormRenderer();
        System.Drawing.Image image = renderer.Render(
            songFile,
            averagePeakProvider,
            myRendererSettings
        );
        MemoryStream ms = new MemoryStream();
        image.Save(ms, ImageFormat.Bmp);
        byte[] testArr = ms.ToArray();
        Godot.Image img = new();
        img.LoadBmpFromBuffer(testArr);
        Texture = ImageTexture.CreateFromImage(img);
        GD.Print(testArr);
    }

    public override void _Process(double delta) { }
}
