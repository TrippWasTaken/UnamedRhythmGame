using System;
using System.IO;
using Godot;

public partial class EditorContrainer : Control
{
    private FileDialog openDialog;
    private Conducter conducter;

    private void onFileSelected(string selectedFile)
    {
        GD.Print("file selected: ", selectedFile);

        if (conducter != null)
        {
            conducter.Stream._Set(selectedFile, false);

            GD.Print(conducter.Stream);
            conducter.InitializeProperties();
        }
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        openDialog = GetNode<FileDialog>("ImportDialog");
        conducter = GetTree().CurrentScene.GetNode<Conducter>("Conducter");
        openDialog.Popup();
        openDialog.FileSelected += (file) => onFileSelected(file);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) { }
}
