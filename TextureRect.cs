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
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var averagePeakProvider = new AveragePeakProvider(0.5f); // e.g. 4

        StandardWaveFormRendererSettings myRendererSettings = new StandardWaveFormRendererSettings
        {
            Width = 3000,
            TopHeight = 120,
            BottomHeight = 0,
            TopPeakPen = Pens.White,
            BottomPeakPen = Pens.White,
            BackgroundColor = System.Drawing.Color.Transparent,
        };

        WaveFormRenderer renderer = new WaveFormRenderer();
        var path = new AudioFileReader("./testSong1.mp3");

        System.Drawing.Image image = renderer.Render(path, averagePeakProvider, myRendererSettings);

        MemoryStream ms = new MemoryStream();
        image.Save(ms, ImageFormat.Bmp);

        var testArr = ms.ToArray();

        var img = new Godot.Image();

        img.LoadBmpFromBuffer(testArr);
        this.Texture = ImageTexture.CreateFromImage(img);
        GD.Print(testArr);
    }

    public override void _Process(double delta) { }
}
